// Build-time access to the data scripts/downloads-chart.mjs writes to public/metrics/ before a
// build. `astro dev` doesn't run that script, and it writes nothing without GitHub's data, so a
// missing file falls back to releases.json (files without counts). Paths are from the docs
// folder, where npm runs the build, because this module is bundled elsewhere.
import { readFileSync } from 'node:fs';
import { join } from 'node:path';
import fallbackReleases from './releases.json';
import worker from './worker.json';

export interface ReleaseFile {
	key: string;
	name: string;
	size: number;
	downloads?: number;
}

export interface Release {
	tag: string;
	label: string;
	published: string;
	files: ReleaseFile[];
}

function readMetrics(name: string) {
	try {
		return JSON.parse(readFileSync(join(process.cwd(), 'public', 'metrics', name), 'utf8'));
	} catch {
		return null;
	}
}

const downloads = readMetrics('downloads.json');
const releases: Release[] = downloads?.releases ?? fallbackReleases;

export const votes: Record<string, number> = readMetrics('votes.json')?.counts ?? {};

// The counter Worker (worker/src/index.js), set in worker.json.
export const WORKER = worker.url;

// Through the Worker, which notes the visitor's country and redirects to GitHub's file.
export const downloadUrl = (tag: string, file: ReleaseFile) =>
	`${WORKER}/download/${encodeURIComponent(tag)}/${encodeURIComponent(file.name)}`;

export const findRelease = (tag: string) => releases.find((r) => r.tag === tag);

// The newest release by publish date (downloads.json lists releases oldest first,
// releases.json newest first).
export const latestRelease: Release | undefined = [...releases].sort((a, b) =>
	b.published.localeCompare(a.published),
)[0];

// Every release, newest first, and the day GitHub's counts were read (undefined without them).
export const allReleases: Release[] = [...releases].sort((a, b) => b.published.localeCompare(a.published));
export const downloadsUpdated: string | undefined = downloads?.updated;

// A release's downloads over all its files, or undefined when GitHub's counts weren't read.
export const releaseDownloads = (release: Release) =>
	release.files.every((f) => f.downloads === undefined)
		? undefined
		: release.files.reduce((sum, f) => sum + (f.downloads ?? 0), 0);

// Downloads made through the site's buttons, per country code, summed over every file (the
// counter Worker's data; empty until someone downloads through the site).
export const countryTotals: Record<string, number> = {};
for (const perFile of Object.values((downloads?.countries ?? {}) as Record<string, Record<string, number>>)) {
	for (const [country, n] of Object.entries(perFile ?? {})) countryTotals[country] = (countryTotals[country] ?? 0) + n;
}

// The site's page views per path (the counter Worker's data), and the recorded download history
// behind the README chart (scripts/downloads-chart.mjs). Null when they weren't written.
export interface Views {
	updated: string;
	total: number;
	pages: Record<string, number>;
}

export interface History {
	baseline: {
		recordedFrom: string;
		releases: { tag: string; label: string; published: string; downloads: number }[];
	};
	points: { month: string; date: string; total: number }[];
}

export const views: Views | null = readMetrics('views.json');
export const history: History | null = readMetrics('history.json');

// The repository's GitHub stars: the total and the day each was given, oldest first (written by
// scripts/downloads-chart.mjs; null when GitHub couldn't be read).
export interface Stars {
	updated: string;
	total: number;
	dates: string[];
}

export const stars: Stars | null = readMetrics('stars.json');

// The Roadmap's open visitor suggestions as of the build (scripts/downloads-chart.mjs); the page
// swaps in the Worker's live list.
export interface Suggestion {
	id: string;
	title: string;
	description: string;
	author: string;
	created: string;
}

export const suggestions: Suggestion[] = readMetrics('suggestions.json')?.suggestions ?? [];

// Downloads since the latest release came out, from the recorded history (GitHub keeps no
// download dates). The baseline is the total at the release: the first recording when the
// release is no newer than it (exact for v1.4.0, released the day recording began), otherwise
// the last monthly point before the release's month. Split into the latest release and the
// older ones, per release (now minus its count at the baseline). Null without the data.
export function downloadsSinceLatest() {
	if (!history || !latestRelease) return null;
	// A release without files (v1.1) has no count; it adds nothing.
	const counted = allReleases.filter((r) => r.files.length > 0).map((r) => releaseDownloads(r));
	if (counted.length === 0 || counted.some((n) => n === undefined)) return null;
	const nowTotal = counted.reduce((sum: number, n) => sum + n!, 0);
	const published = latestRelease.published;
	const { recordedFrom, releases: baseline } = history.baseline;
	const beforeMonth = history.points.filter((p) => p.date < `${published.slice(0, 7)}-01`).at(-1);
	const fromBaseline = published <= recordedFrom || !beforeMonth;
	const baseTotal = fromBaseline ? baseline.reduce((sum, r) => sum + r.downloads, 0) : beforeMonth!.total;
	const baseDate = fromBaseline ? recordedFrom : beforeMonth!.date;
	const latestBefore = fromBaseline ? (baseline.find((r) => r.tag === latestRelease!.tag)?.downloads ?? 0) : 0;
	const total = nowTotal - baseTotal;
	const latest = releaseDownloads(latestRelease)! - latestBefore;
	return {
		total,
		latest,
		older: total - latest,
		label: latestRelease.label,
		month: new Date(`${published}T00:00:00Z`).toLocaleDateString('en-GB', { month: 'short', year: 'numeric', timeZone: 'UTC' }),
		from: baseDate,
	};
}
