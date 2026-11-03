// What a download link tells the "Make sure…" popup (src/components/DownloadDialog.astro) about
// its file: the name, size and kind, and the other build of the same release to offer instead
// (x86 for an x64 file and the other way round). DownloadButtons.astro and HomePoster.astro
// spread these attributes onto every download link.
//
// Download links open in a new tab: the browser starts the file there and closes the blank tab
// by itself, so the page and the popup stay in front. rel="noopener" keeps that tab from
// reaching back into this page.
import { downloadUrl, type Release, type ReleaseFile } from './metrics';

export const size = (bytes: number) =>
	bytes >= 1e6 ? `${(bytes / 1e6).toFixed(1)} MB` : `${Math.round(bytes / 1e3)} KB`;

export const LABELS: Record<string, string> = {
	x64: 'x64 (64-bit)',
	x86: 'x86 (32-bit)',
	plugins: 'Plugins',
	main: 'Grimlite Rev',
	amogus: 'Amogus',
	ipplugins: 'IP Plugins',
};

export function downloadData(release: Release, file: ReleaseFile) {
	const other = file.key === 'x64' ? 'x86' : file.key === 'x86' ? 'x64' : undefined;
	const alt = other ? release.files.find((f) => f.key === other) : undefined;
	return {
		target: '_blank',
		rel: 'noopener',
		'data-download': '',
		'data-file': file.name,
		'data-size': size(file.size),
		'data-key': file.key,
		...(alt && {
			'data-alt-href': downloadUrl(release.tag, alt),
			'data-alt-label': `${LABELS[alt.key]} build`,
			'data-alt-size': size(alt.size),
		}),
	};
}
