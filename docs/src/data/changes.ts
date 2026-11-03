// Sorts a changelog's lines into Keep a Changelog groups for the overview under a showcase
// poster (src/components/showcase/Overview.astro). The changelogs aren't written under those
// headings, so each top-level list item is sorted by its first word:
//   Added, Add, New          -> Added
//   Fixed, Fix               -> Fixed
//   Removed, Remove, Retired -> Removed
//   anything else            -> Changed ("Replaced", "Improved", "Reworked", plain sentences)
// Markdown is reduced to plain text. Lines keep their order in the changelog.
export type Group = 'added' | 'changed' | 'fixed' | 'removed';

export const GROUPS: { id: Group; label: string }[] = [
	{ id: 'added', label: 'added' },
	{ id: 'changed', label: 'changed' },
	{ id: 'fixed', label: 'fixed' },
	{ id: 'removed', label: 'removed' },
];

const VERBS: Record<string, Group> = {
	added: 'added',
	add: 'added',
	new: 'added',
	fixed: 'fixed',
	fix: 'fixed',
	removed: 'removed',
	remove: 'removed',
	retired: 'removed',
};

const plain = (line: string) =>
	line
		.replace(/\[([^\]]+)\]\([^)]*\)/g, '$1')
		.replace(/[*_`]/g, '')
		.replace(/\s+/g, ' ')
		.trim();

export function sortChanges(markdown: string | undefined): Record<Group, string[]> {
	const groups: Record<Group, string[]> = { added: [], changed: [], fixed: [], removed: [] };
	for (const raw of (markdown ?? '').split(/\r?\n/)) {
		const match = raw.match(/^[-*]\s+(.+)$/);
		if (!match) continue;
		const text = plain(match[1]);
		const first = text.split(/[\s,.:]/, 1)[0].toLowerCase();
		groups[VERBS[first] ?? 'changed'].push(text);
	}
	return groups;
}
