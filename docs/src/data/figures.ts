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
