// Formatting shared by the Metrics page and the home page's metrics overview.
export const number = (n: number) => n.toLocaleString('en-US');

export const longDate = (iso: string) =>
	new Date(`${iso.slice(0, 10)}T00:00:00Z`).toLocaleDateString('en-GB', {
		day: 'numeric',
		month: 'long',
		year: 'numeric',
		timeZone: 'UTC',
	});

const regions = new Intl.DisplayNames(['en'], { type: 'region' });

// A country's English name from the two-letter code Cloudflare records, or the code itself
// when it isn't a country (Cloudflare uses XX for unknown and T1 for Tor).
export function countryName(code: string) {
	if (code === 'XX') return 'Unknown';
	if (code === 'T1') return 'Tor network';
	try {
		return regions.of(code) ?? code;
	} catch {
		return code;
	}
}

// A round step for a chart's y axis that gives about four gridlines up to `max`.
export function niceStep(max: number) {
	const rough = Math.max(max, 1) / 4;
	const power = 10 ** Math.floor(Math.log10(rough));
	return [1, 2, 2.5, 5, 10].map((m) => m * power).find((s) => s >= rough)!;
}

// Axis labels: 400000 -> "400k".
export const compact = (n: number) => (n >= 1e6 ? `${n / 1e6}M` : n >= 1e3 ? `${n / 1e3}k` : String(n));
