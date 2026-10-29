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

// The counter Worker (worker/src/index.js), set in worker.json.
export const WORKER = worker.url;

// Through the Worker, which notes the visitor's country and redirects to GitHub's file.
export const downloadUrl = (tag: string, file: ReleaseFile) =>
	`${WORKER}/download/${encodeURIComponent(tag)}/${encodeURIComponent(file.name)}`;

export const findRelease = (tag: string) => releases.find((r) => r.tag === tag);
