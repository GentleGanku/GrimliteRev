// Draws the "Total downloads" chart that README.md embeds from the Pages site.
// Runs before every build (npm "prebuild") and writes public/metrics/:
//   downloads-light.svg, downloads-dark.svg  the chart in GitHub's two themes
//   history.json                             the recorded monthly totals
//
// GitHub keeps only a running download total per release file, with no dates, so the
// chart has two parts:
//   - By release date: on the first run, each existing release's total is frozen as a
//     step at its publish date. It is labelled as counted on that day, because the
//     downloads behind each step happened over years, not on the step's date.
//   - Recorded monthly: every build stores the all-release total for the current month,
//     replacing that month's previous value, so there is one point per month from the
//     first run on.
// The history is read back from the live site, so recording needs no commits. If it
// can't be read in CI, the build fails and the last good site stays up; otherwise the
// next deploy would publish an empty history.
import { mkdir, writeFile } from 'node:fs/promises';

const REPO = 'GentleGanku/GrimliteRev';
const HISTORY_URL = 'https://gentleganku.github.io/GrimliteRev/metrics/history.json';
const OUT_DIR = new URL('../public/metrics/', import.meta.url);

// GitHub's own page colors, so the chart sits on the README without a box around it.
const THEMES = {
	light: { text: '#1f2328', muted: '#59636e', line: '#d6143a', grid: '#d1d9e0' },
	dark: { text: '#f0f6fc', muted: '#9198a1', line: '#f64061', grid: '#3d444d' },
};

const FONT = "-apple-system, BlinkMacSystemFont, 'Segoe UI', 'Noto Sans', Helvetica, Arial, sans-serif";
const WIDTH = 720;
const HEIGHT = 330;
const PLOT = { left: 70, right: 690, top: 110, bottom: 290 };

const number = (n) => n.toLocaleString('en-US');
const escape = (s) => s.replace(/[&<>"]/g, (c) => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;' })[c]);
const day = (d) => new Date(d).toISOString().slice(0, 10);
const shortDate = (d) =>
	new Date(d).toLocaleDateString('en-GB', { day: 'numeric', month: 'short', year: 'numeric', timeZone: 'UTC' });
const longDate = (d) =>
	new Date(d).toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric', timeZone: 'UTC' });

async function fetchReleases() {
	const headers = { Accept: 'application/vnd.github+json', 'User-Agent': 'grimlite-rev-docs' };
	if (process.env.GITHUB_TOKEN) headers.Authorization = `Bearer ${process.env.GITHUB_TOKEN}`;
	const res = await fetch(`https://api.github.com/repos/${REPO}/releases?per_page=100`, { headers });
	if (!res.ok) throw new Error(`GitHub API answered ${[res.status, res.statusText].filter(Boolean).join(' ')}`);
	const releases = await res.json();
	return releases
		.filter((r) => !r.draft)
		.map((r) => ({
			tag: r.tag_name,
			label: r.tag_name.replace(/-/g, ' '),
			published: day(r.published_at),
			downloads: r.assets.reduce((sum, a) => sum + a.download_count, 0),
		}))
		.sort((a, b) => a.published.localeCompare(b.published));
}

// Returns the stored history, null when none exists yet (first run), or throws when it
// can't be read. Outside CI a read failure only warns, so local builds work offline.
async function fetchHistory() {
	try {
		const res = await fetch(HISTORY_URL, { headers: { 'Cache-Control': 'no-cache' } });
		if (res.status === 404) return null;
		if (!res.ok) throw new Error(`${HISTORY_URL} answered ${res.status}`);
		return await res.json();
	} catch (err) {
		if (process.env.CI) throw err;
		console.warn(`downloads-chart: can't read the published history (${err.message}); starting a local one`);
		return null;
	}
}

function niceStep(max) {
	const rough = max / 4;
	const power = 10 ** Math.floor(Math.log10(rough));
	return [1, 2, 2.5, 5, 10].map((m) => m * power).find((s) => s >= rough);
}

const short = (n) => (n >= 1e6 ? `${+(n / 1e6).toFixed(1)}M` : n >= 1e3 ? `${+(n / 1e3).toFixed(0)}K` : String(n));

function chart(history, releases, theme) {
	const t = THEMES[theme];
	const { baseline, points } = history;
	const last = points.at(-1);
	// Releases published within a month of the first run (v1.4.0) belong to the recorded part,
	// so the dotted "not recorded" stretch always sits between the old releases and recording.
	const cutoff = Date.parse(baseline.recordedFrom) - 31 * 864e5;
	const steps = baseline.releases.filter((r) => Date.parse(r.published) < cutoff);
	const later = releases.filter((r) => !steps.some((s) => s.tag === r.tag));

	const start = Date.parse(steps[0].published);
	const end = Math.max(Date.parse(last.date), Date.parse(steps.at(-1).published)) + 45 * 864e5;
	const step = niceStep(Math.max(...points.map((p) => p.total), 1));
	const yMax = Math.ceil(Math.max(...points.map((p) => p.total), 1) / step) * step;
	const ms = (d) => (typeof d === 'number' ? d : Date.parse(d));
	const x = (d) => PLOT.left + ((ms(d) - start) / (end - start)) * (PLOT.right - PLOT.left);
	const y = (v) => PLOT.bottom - (v / yMax) * (PLOT.bottom - PLOT.top);
	const f = (n) => n.toFixed(1);

	const grid = [];
	for (let v = 0; v <= yMax; v += step) {
		grid.push(`<line x1="${PLOT.left}" x2="${PLOT.right}" y1="${f(y(v))}" y2="${f(y(v))}" stroke="${t.grid}" stroke-width="1"/>`);
		grid.push(`<text x="${PLOT.left - 8}" y="${f(y(v) + 4)}" text-anchor="end" fill="${t.muted}" font-size="11">${short(v)}</text>`);
	}
	for (let year = new Date(start).getUTCFullYear() + 1; Date.UTC(year, 0, 1) < end; year++) {
		const yx = f(x(Date.UTC(year, 0, 1)));
		grid.push(`<line x1="${yx}" x2="${yx}" y1="${PLOT.bottom}" y2="${PLOT.bottom + 5}" stroke="${t.grid}" stroke-width="1"/>`);
		grid.push(`<text x="${yx}" y="${PLOT.bottom + 19}" text-anchor="middle" fill="${t.muted}" font-size="11">${year}</text>`);
	}

	// By release date: steps, frozen at the first run.
	let sum = 0;
	let path = `M${f(x(steps[0].published))},${f(y(0))}`;
	const stepPoints = [];
	const lines = [];
	let prevX = x(steps[0].published);
	for (const r of steps) {
		const before = sum;
		path += ` H${f(x(r.published))} V${f(y((sum += r.downloads)))}`;
		lines.push([prevX, y(before), x(r.published), y(before)], [x(r.published), y(before), x(r.published), y(sum)]);
		prevX = x(r.published);
		// A release with no downloads (v1.1) sits on the previous step, so it gets no label of its own.
		stepPoints.push({ px: x(r.published), py: y(sum), total: sum, release: r, repeat: sum === before });
	}
	const lastStep = steps.at(-1);
	const parts = [
		`<path d="${path}" fill="none" stroke="${t.line}" stroke-width="2" stroke-dasharray="6 4"/>`,
		...stepPoints.map((p) => `<circle cx="${f(p.px)}" cy="${f(p.py)}" r="3" fill="${t.line}"/>`),
	];

	// Every label is placed above, below, right, left, above-left or above-right of its point,
	// in that order of preference, wherever it doesn't overlap a point, a line or another
	// label. A label that fits nowhere is left out, except the newest total, which is always shown.
	const taken = [];
	const labels = [];
	const overlaps = (b) =>
		b.x0 < 0 || b.x1 > WIDTH || b.y0 < 84 || b.y1 > PLOT.bottom - 2 ||
		taken.some((o) => b.x0 < o.x1 && b.x1 > o.x0 && b.y0 < o.y1 && b.y1 > o.y0);
	const block = (x0, y0, w, h) => taken.push({ x0, y0, x1: x0 + w, y1: y0 + h });
	function label(px, py, text, color, always = false) {
		const w = text.length * 6.2;
		const spots = [
			{ anchor: 'middle', tx: px, ty: py - 9, x0: px - w / 2 },
			{ anchor: 'middle', tx: px, ty: py + 17, x0: px - w / 2 },
			{ anchor: 'start', tx: px + 7, ty: py + 4, x0: px + 7 },
			{ anchor: 'end', tx: px - 7, ty: py + 4, x0: px - 7 - w },
			{ anchor: 'end', tx: px - 4, ty: py - 9, x0: px - 4 - w },
			{ anchor: 'start', tx: px + 4, ty: py - 9, x0: px + 4 },
		].map((s) => ({ ...s, b: { x0: s.x0, y0: s.ty - 10, x1: s.x0 + w, y1: s.ty + 2 } }));
		const spot = spots.find((s) => !overlaps(s.b)) ?? (always ? spots[0] : null);
		if (!spot) return;
		taken.push(spot.b);
		labels.push(`<text x="${f(spot.tx)}" y="${f(spot.ty)}" text-anchor="${spot.anchor}" fill="${color}" font-size="11">${escape(text)}</text>`);
	}
	const value = (n) => (n >= 1e3 ? `${+(n / 1e3).toFixed(1)}K` : number(n));

	// Not recorded: between the last release step and the first recorded month.
	const first = points[0];
	if (Date.parse(first.date) - Date.parse(lastStep.published) > 31 * 864e5) {
		const x1 = x(lastStep.published), x2 = x(first.date), y1 = y(sum), y2 = y(first.total);
		const mx = (x1 + x2) / 2, my = Math.min(y1, y2) - 8;
		parts.push(`<line x1="${f(x1)}" y1="${f(y1)}" x2="${f(x2)}" y2="${f(y2)}" stroke="${t.muted}" stroke-width="1.5" stroke-dasharray="2 4"/>`);
		parts.push(`<text x="${f(mx)}" y="${f(my)}" text-anchor="middle" fill="${t.muted}" font-size="11">not recorded</text>`);
		block(mx - 40, my - 10, 80, 12);
		lines.push([x1, y1, x2, y2]);
	}

	// Recorded monthly.
	const recorded = points.map((p) => ({ px: x(p.date), py: y(p.total), total: p.total, date: p.date }));
	if (recorded.length > 1) {
		parts.push(`<path d="${recorded.map((p, i) => `${i ? 'L' : 'M'}${f(p.px)},${f(p.py)}`).join(' ')}" fill="none" stroke="${t.line}" stroke-width="2.5"/>`);
	}
	for (const p of recorded) parts.push(`<circle cx="${f(p.px)}" cy="${f(p.py)}" r="3.5" fill="${t.line}"/>`);

	// Releases published after recording started, circled on the recorded line.
	const marked = later.map((r) => ({ release: r, at: recorded.find((p) => p.date >= r.published) ?? recorded.at(-1) }));
	for (const { at } of marked) parts.push(`<circle cx="${f(at.px)}" cy="${f(at.py)}" r="5.5" fill="none" stroke="${t.line}" stroke-width="1.5"/>`);

	// Points, lines and axis labels block labels from covering them; then labels go on,
	// most important first.
	for (let i = 1; i < recorded.length; i++) lines.push([recorded[i - 1].px, recorded[i - 1].py, recorded[i].px, recorded[i].py]);
	for (const [x1, y1, x2, y2] of lines) block(Math.min(x1, x2) - 2, Math.min(y1, y2) - 2, Math.abs(x2 - x1) + 4, Math.abs(y2 - y1) + 4);
	for (let v = 0; v <= yMax; v += step) block(PLOT.left - 40, y(v) - 7, 32, 14);
	for (const p of [...stepPoints, ...recorded]) block(p.px - 4, p.py - 4, 8, 8);
	const newest = recorded.at(-1);
	const named = new Set(marked.map((m) => m.at));
	const newestRelease = marked.findLast((m) => m.at === newest);
	label(newest.px, newest.py, newestRelease ? `${newestRelease.release.label} · ${value(newest.total)}` : value(newest.total), t.text, true);
	for (const { release, at } of marked) if (at !== newest) label(at.px, at.py, `${release.label} · ${value(at.total)}`, t.text);
	const lastStepPoint = stepPoints.at(-1);
	label(lastStepPoint.px, lastStepPoint.py, `${lastStep.label} · ${value(lastStepPoint.total)}`, t.text);
	for (const p of stepPoints.slice(0, -1)) if (!p.repeat) label(p.px, p.py, value(p.total), t.muted);
	for (const p of recorded.slice(0, -1).reverse()) if (!named.has(p)) label(p.px, p.py, value(p.total), t.muted);
	parts.push(...labels);

	// Legend, centered under the subtitle.
	const legend = [
		{ swatch: `stroke="${t.line}" stroke-width="2" stroke-dasharray="6 4"`, label: `By release date (counted ${shortDate(baseline.recordedFrom)})` },
		{ swatch: `stroke="${t.line}" stroke-width="2.5"`, label: 'Recorded monthly' },
	];
	const legendText = [
		`<line x1="148" x2="172" y1="72" y2="72" ${legend[0].swatch}/>`,
		`<text x="178" y="76" fill="${t.muted}" font-size="12">${legend[0].label}</text>`,
		`<line x1="440" x2="464" y1="72" y2="72" ${legend[1].swatch}/>`,
		`<text x="470" y="76" fill="${t.muted}" font-size="12">${legend[1].label}</text>`,
	];

	return `<svg xmlns="http://www.w3.org/2000/svg" width="${WIDTH}" height="${HEIGHT}" viewBox="0 0 ${WIDTH} ${HEIGHT}" font-family="${FONT}" role="img" aria-labelledby="title">
  <title id="title">Grimlite Rev total downloads: ${number(last.total)} as of ${longDate(last.date)}. Releases up to ${escape(lastStep.label)} are shown by release date, counted ${longDate(baseline.recordedFrom)}; totals are recorded monthly from then on.</title>
  <text x="${WIDTH / 2}" y="22" text-anchor="middle" fill="${t.text}" font-size="17" font-weight="600">Total downloads</text>
  <text x="${WIDTH / 2}" y="46" text-anchor="middle" fill="${t.muted}" font-size="13">${number(last.total)} as of ${longDate(last.date)}</text>
  ${legendText.join('\n  ')}
  ${grid.join('\n  ')}
  ${parts.join('\n  ')}
</svg>
`;
}

function unavailable(theme, reason) {
	const t = THEMES[theme];
	return `<svg xmlns="http://www.w3.org/2000/svg" width="${WIDTH}" height="64" viewBox="0 0 ${WIDTH} 64" font-family="${FONT}" role="img" aria-labelledby="title">
  <title id="title">Download counts unavailable</title>
  <text x="${WIDTH / 2}" y="22" text-anchor="middle" fill="${t.text}" font-size="17" font-weight="600">Total downloads</text>
  <text x="${WIDTH / 2}" y="46" text-anchor="middle" fill="${t.muted}" font-size="13">Download counts are unavailable right now (${escape(reason)}). See the Releases page.</text>
</svg>
`;
}

const today = day(Date.now());
let history = await fetchHistory();
let releases = null;
try {
	releases = await fetchReleases();
} catch (err) {
	console.warn(`downloads-chart: ${err.message}`);
}

if (releases) {
	const total = releases.reduce((s, r) => s + r.downloads, 0);
	history ??= {
		baseline: { recordedFrom: today, releases: releases.map(({ tag, label, published, downloads }) => ({ tag, label, published, downloads })) },
		points: [],
	};
	const month = today.slice(0, 7);
	const point = { month, date: today, total };
	if (history.points.at(-1)?.month === month) history.points[history.points.length - 1] = point;
	else history.points.push(point);
	console.log(`downloads-chart: ${number(total)} downloads, ${history.points.length} recorded month(s) since ${history.baseline.recordedFrom}`);
} else if (history) {
	console.warn('downloads-chart: keeping the published history unchanged');
}

await mkdir(OUT_DIR, { recursive: true });
if (history) await writeFile(new URL('history.json', OUT_DIR), JSON.stringify(history, null, 2) + '\n');
for (const theme of Object.keys(THEMES)) {
	const svg = history?.points.length
		? chart(history, releases ?? history.baseline.releases, theme)
		: unavailable(theme, 'GitHub release data could not be read');
	await writeFile(new URL(`downloads-${theme}.svg`, OUT_DIR), svg);
}
