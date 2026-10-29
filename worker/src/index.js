// Grimlite Rev's counter, a Cloudflare Worker with a D1 database. GitHub Pages is static, so
// this records what GitHub doesn't: where the site's downloads come from, the site's page views
// and the Roadmap's votes. Download totals are GitHub's own.
//
//   GET  /download/<tag>/<file>  counts one download with the visitor's country, then redirects
//                                to the GitHub release file. The site's download buttons point here.
//   POST /hit                    counts one page view; the body is the page path (text/plain).
//   GET  /votes                  vote counts per Roadmap item, and the items this visitor voted for.
//   POST /vote, POST /unvote     adds or takes back this visitor's vote; the body is the item id.
//   GET  /counts                 every count, read by docs/scripts/metrics.mjs at build time.
//
// Visitors are told apart by a SHA-256 hash of their IP and the secret IP_SALT. IPs are never
// stored. POST bodies are text/plain so browsers send them without a CORS preflight.

const TAG = /^v[\w.-]{1,40}$/;
const FILE = /^[\w.-]{1,100}\.(zip|rar|7z)$/;
const ITEM = /^[a-z0-9-]{1,64}$/;
const PAGE = /^\/GrimliteRev\/[\w./-]{0,200}$/;
// Link previews (Discord, Slack and the like) fetch a pasted link without downloading anything.
const BOT = /bot|crawl|spider|preview|discord|slack|telegram|whatsapp|facebookexternalhit|embed|curl\/|wget\//i;

export default {
	async fetch(request, env, ctx) {
		const url = new URL(request.url);
		const parts = url.pathname.split('/').filter(Boolean).map(decodeURIComponent);
		const route = `${request.method} /${parts[0] ?? ''}`;
		try {
			if (request.method === 'OPTIONS') return withCors(request, env, new Response(null, { status: 204 }));
			if ((route === 'GET /download' || route === 'HEAD /download') && parts.length === 3) {
				return download(request, env, ctx, parts[1], parts[2]);
			}
			if (route === 'POST /hit') return withCors(request, env, await hit(request, env));
			if (route === 'GET /votes') return withCors(request, env, await listVotes(request, env));
			if (route === 'POST /vote') return withCors(request, env, await vote(request, env, true));
			if (route === 'POST /unvote') return withCors(request, env, await vote(request, env, false));
			if (route === 'GET /counts') return withCors(request, env, await counts(env));
			return withCors(request, env, json({ error: 'Not found' }, 404));
		} catch (err) {
			console.error(err);
			return withCors(request, env, json({ error: 'Something went wrong' }, 500));
		}
	},
};

// The redirect never waits for the database, and a failed count still sends the file.
function download(request, env, ctx, tag, file) {
	if (!TAG.test(tag) || !FILE.test(file)) return json({ error: 'Unknown file' }, 404);
	const target = `${env.FILES_BASE}/${encodeURIComponent(tag)}/${encodeURIComponent(file)}`;
	const counted = request.method === 'GET' && !BOT.test(request.headers.get('User-Agent') ?? '');
	if (counted) {
		const country = request.cf?.country ?? 'XX';
		ctx.waitUntil(
			env.DB.prepare(
				`INSERT INTO downloads (tag, file, country, count) VALUES (?1, ?2, ?3, 1)
				 ON CONFLICT (tag, file, country) DO UPDATE SET count = count + 1`,
			)
				.bind(tag, file, country)
				.run()
				.catch((err) => console.error('download count failed', err)),
		);
	}
	return new Response(null, { status: 302, headers: { Location: target, 'Cache-Control': 'no-store' } });
}

async function hit(request, env) {
	const path = (await request.text()).trim();
	if (!PAGE.test(path)) return json({ error: 'Unknown page' }, 400);
	const month = new Date().toISOString().slice(0, 7);
	await env.DB.prepare(
		`INSERT INTO pageviews (path, month, count) VALUES (?1, ?2, 1)
		 ON CONFLICT (path, month) DO UPDATE SET count = count + 1`,
	)
		.bind(path, month)
		.run();
	return new Response(null, { status: 204 });
}

async function listVotes(request, env) {
	const voter = await visitor(request, env);
	const [all, mine] = await env.DB.batch([
		env.DB.prepare('SELECT item, COUNT(*) AS n FROM votes GROUP BY item'),
		env.DB.prepare('SELECT item FROM votes WHERE voter = ?1').bind(voter),
	]);
	return json({
		counts: Object.fromEntries(all.results.map((r) => [r.item, r.n])),
		mine: mine.results.map((r) => r.item),
	});
}

async function vote(request, env, add) {
	const item = (await request.text()).trim();
	if (!ITEM.test(item)) return json({ error: 'Unknown item' }, 400);
	const voter = await visitor(request, env);
	const change = add
		? env.DB.prepare('INSERT OR IGNORE INTO votes (item, voter, created) VALUES (?1, ?2, ?3)').bind(item, voter, new Date().toISOString())
		: env.DB.prepare('DELETE FROM votes WHERE item = ?1 AND voter = ?2').bind(item, voter);
	const [, count] = await env.DB.batch([change, env.DB.prepare('SELECT COUNT(*) AS n FROM votes WHERE item = ?1').bind(item)]);
	return json({ item, count: count.results[0].n, voted: add });
}

async function counts(env) {
	const [downloads, pageviews, votes] = await env.DB.batch([
		env.DB.prepare('SELECT tag, file, country, count FROM downloads'),
		env.DB.prepare('SELECT path, SUM(count) AS n FROM pageviews GROUP BY path'),
		env.DB.prepare('SELECT item, COUNT(*) AS n FROM votes GROUP BY item'),
	]);
	const files = {};
	for (const { tag, file, country, count } of downloads.results) {
		const entry = (files[`${tag}/${file}`] ??= { total: 0, countries: {} });
		entry.total += count;
		entry.countries[country] = (entry.countries[country] ?? 0) + count;
	}
	return json({
		downloads: files,
		pageviews: Object.fromEntries(pageviews.results.map((r) => [r.path, r.n])),
		votes: Object.fromEntries(votes.results.map((r) => [r.item, r.n])),
	});
}

async function visitor(request, env) {
	if (!env.IP_SALT) throw new Error('The IP_SALT secret is not set');
	const ip = request.headers.get('CF-Connecting-IP') ?? '';
	const digest = await crypto.subtle.digest('SHA-256', new TextEncoder().encode(`${env.IP_SALT}|${ip}`));
	return [...new Uint8Array(digest)].map((b) => b.toString(16).padStart(2, '0')).join('');
}

function json(data, status = 200) {
	return new Response(JSON.stringify(data), {
		status,
		headers: { 'Content-Type': 'application/json', 'Cache-Control': 'no-store' },
	});
}

// The site may read and send votes; local dev servers may too, for testing. Anything else
// (scripts, the build) gets the data without CORS headers, which only browsers enforce.
function withCors(request, env, response) {
	const origin = request.headers.get('Origin') ?? '';
	if (origin !== env.SITE_ORIGIN && !/^http:\/\/(localhost|127\.0\.0\.1)(:\d+)?$/.test(origin)) return response;
	const headers = new Headers(response.headers);
	headers.set('Access-Control-Allow-Origin', origin);
	headers.set('Access-Control-Allow-Methods', 'GET, POST, OPTIONS');
	headers.set('Access-Control-Allow-Headers', 'Content-Type');
	headers.set('Vary', 'Origin');
	return new Response(response.body, { status: response.status, headers });
}
