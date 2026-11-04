// Adds a released version's Roadmap lines (roadmap.ts, roadmapChangelog) to its changelog page,
// as list items at the end of the list under each `### ADDITIONS`, `### PATCHES` and
// `### OTHERS` heading. A heading the page doesn't have is added, before the next one it does
// have, or at the end. The rendered Markdown is Starlight's: each heading sits in a
// `.sl-heading-wrapper` div, followed by its list. Lines are plain text and are escaped.
import { CHANGELOG_HEADINGS, type ChangelogHeading, type ChangelogLine } from './roadmap';

const escape = (text: string) =>
	text.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');

const CHEVRON = '<svg viewBox="0 0 24 24" aria-hidden="true"><path d="m6 15 6-6 6 6"></path></svg>';

function item(line: ChangelogLine) {
	const votes =
		line.votes === undefined
			? ''
			: ` <span class="gr-roadmap-votes">${CHEVRON}${line.votes.toLocaleString('en-US')} vote${line.votes === 1 ? '' : 's'}<span class="sr-only"> on the Roadmap</span></span>`;
	return `<li>${escape(line.text)}${votes}</li>\n`;
}

// Where a heading's wrapper starts, or -1.
function headingAt(html: string, heading: ChangelogHeading) {
	const match = new RegExp(`<h3[^>]*>\\s*${heading}:?\\s*</h3>`, 'i').exec(html);
	if (!match) return -1;
	const wrapper = html.lastIndexOf('<div class="sl-heading-wrapper', match.index);
	return wrapper !== -1 && match.index - wrapper < 120 ? wrapper : match.index;
}

// The end of the list that follows a heading (before its closing tag), if it comes before the
// next heading; nested lists are skipped.
function listEnd(html: string, from: number) {
	const after = html.indexOf('</h3>', from) + 5;
	const next = html.slice(after).search(/<h[1-6][\s>]|<div class="sl-heading-wrapper/);
	const limit = next === -1 ? html.length : after + next;
	const start = html.indexOf('<ul', after);
	if (start === -1 || start > limit) return -1;
	const tags = /<ul[\s>]|<\/ul>/g;
	tags.lastIndex = start;
	let depth = 0;
	for (let m = tags.exec(html); m; m = tags.exec(html)) {
		depth += m[0] === '</ul>' ? -1 : 1;
		if (depth === 0) return m.index;
	}
	return -1;
}

export function addChangelogLines(html: string, lines: Record<ChangelogHeading, ChangelogLine[]>) {
	let out = html;
	CHANGELOG_HEADINGS.forEach((heading, i) => {
		if (lines[heading].length === 0) return;
		const items = lines[heading].map(item).join('');
		const at = headingAt(out, heading);
		const end = at === -1 ? -1 : listEnd(out, at);
		if (end !== -1) {
			out = out.slice(0, end) + items + out.slice(end);
			return;
		}
		const section = `<h3 id="${heading.toLowerCase()}">${heading}</h3>\n<ul>\n${items}</ul>\n`;
		if (at !== -1) {
			// The heading is there but has no list yet: the list goes right after its wrapper.
			const close = out.indexOf('</div>', out.indexOf('</h3>', at));
			const after = close === -1 ? out.indexOf('</h3>', at) + 5 : close + 6;
			out = out.slice(0, after) + `\n<ul>\n${items}</ul>\n` + out.slice(after);
			return;
		}
		const later = CHANGELOG_HEADINGS.slice(i + 1)
			.map((h) => headingAt(out, h))
			.find((pos) => pos !== -1);
		out = later === undefined ? out + section : out.slice(0, later) + section + out.slice(later);
	});
	return out;
}
