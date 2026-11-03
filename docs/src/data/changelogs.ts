// The changelog pages in sidebar order (newest first), read from Starlight's sidebar for the
// current route, so the header and the release picker follow the `sidebar` list in
// astro.config.mjs without a second copy of it.
export interface ChangelogLink {
	label: string;
	href: string;
	isCurrent: boolean;
}

interface SidebarLink {
	type: 'link';
	label: string;
	href: string;
	isCurrent: boolean;
}

interface SidebarGroup {
	type: 'group';
	label: string;
	entries: SidebarEntry[];
}

type SidebarEntry = SidebarLink | SidebarGroup;

const flatten = (entries: SidebarEntry[]): ChangelogLink[] =>
	entries.flatMap((entry) =>
		entry.type === 'link'
			? [{ label: entry.label, href: entry.href, isCurrent: entry.isCurrent }]
			: flatten(entry.entries),
	);

export function changelogLinks(sidebar: SidebarEntry[]): ChangelogLink[] {
	const group = sidebar.find(
		(entry): entry is SidebarGroup => entry.type === 'group' && entry.label === 'Changelogs',
	);
	return group ? flatten(group.entries) : [];
}
