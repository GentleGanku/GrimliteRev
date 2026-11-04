// The Roadmap's items (src/data/roadmap.json), shared by the Roadmap page and Home's "Coming in".
//
// Features move through four lanes in order: Proposed (suggestions, the most-voted move up),
// Up next (planned, each tagged with its version; untagged ones default to `next`, the version
// after the latest release), In progress and Ready (lane id `released`: done and ready for
// release). Miscellaneous, what keeps it running well (fixes after game updates, speed,
// stability), has two: Now and Ready (lane id `completed`). Visitors' suggestions come from
// the counter Worker and join Proposed.
//
// Once a version is out (GitHub has the release and its changelog page, with `release: <tag>`,
// exists), its Ready items leave the Roadmap: their `changelog` lines are added to that page
// under ADDITIONS, PATCHES and OTHERS (MarkdownContent.astro), a feature's final vote count
// after its last line. A Ready item without `changelog` lines fails the build at that point,
// so nothing silently disappears.
//
// For a local preview, `ROADMAP_SAMPLE=1 npm run dev` reads src/data/roadmap.sample.json
// instead: labelled sample items, local only (.git/info/exclude), never on the live site.
import { getCollection } from 'astro:content';
import { findRelease, votes } from './metrics';
import live from './roadmap.json';

export type Section = 'features' | 'misc';
export type Lane = 'proposed' | 'up-next' | 'in-progress' | 'released' | 'now' | 'completed';

export interface RoadmapItem {
	id: string;
	title: string;
	description: string;
	section: Section;
	lane: Lane;
	version?: string;
	// Who suggested it, kept when a visitor's suggestion is adopted; shown as "Suggested by".
	author?: string;
	review?: string;
	// The item's lines for its version's changelog, needed once it's Ready.
	changelog?: Partial<Record<ChangelogHeading, string[]>>;
}

export type ChangelogHeading = 'ADDITIONS' | 'PATCHES' | 'OTHERS';
export const CHANGELOG_HEADINGS: ChangelogHeading[] = ['ADDITIONS', 'PATCHES', 'OTHERS'];

interface RoadmapData {
	next: string;
	items: RoadmapItem[];
}

const sampleFiles = import.meta.glob<RoadmapData>('./roadmap.sample.json', { eager: true, import: 'default' });
const sample = process.env.ROADMAP_SAMPLE ? Object.values(sampleFiles)[0] : undefined;

export const isSample = sample !== undefined;
export const roadmap: RoadmapData = sample ?? (live as RoadmapData);

export const LANES: Record<Section, { id: Lane; title: string; note: string; empty: string }[]> = {
	features: [
		{ id: 'proposed', title: 'Proposed', note: 'Ideas, sorted by votes.', empty: 'No proposals yet. Suggest the first one.' },
		{ id: 'up-next', title: 'Up next', note: `Planned for the next release.`, empty: `Nothing is planned for the next release yet.` },
		{ id: 'in-progress', title: 'In progress', note: 'Currently being built!', empty: 'Nothing is being built right now.' },
		{ id: 'released', title: 'Ready', note: 'Done, ready to go!', empty: 'Nothing is ready for release yet.' },
	],
	misc: [
		{ id: 'now', title: 'Now', note: 'Currently being built!', empty: 'Nothing is being worked on right now.' },
		{ id: 'completed', title: 'Ready', note: 'Finished, ready to go!', empty: 'Nothing is finished yet.' },
	],
};

// The version a card shows: its own, or `next` while it's up next or being built.
export const versionOf = (item: RoadmapItem) =>
	item.version ?? (item.lane === 'up-next' || item.lane === 'in-progress' ? roadmap.next : undefined);

const isReady = (item: RoadmapItem) => item.lane === 'released' || item.lane === 'completed';

// The versions that are out: GitHub has the release and the changelog page for it exists.
let shipped: Promise<Set<string>> | undefined;
export function shippedVersions() {
	shipped ??= getCollection('docs', (entry) => entry.id.startsWith('changelog/') && Boolean(entry.data.release)).then(
		(pages) => new Set(pages.map((page) => page.data.release!).filter((tag) => findRelease(tag))),
	);
	return shipped;
}

const hasLines = (item: RoadmapItem) => CHANGELOG_HEADINGS.some((h) => (item.changelog?.[h]?.length ?? 0) > 0);

// The Ready items whose version is out, checked so that each one has changelog lines.
async function movedItems() {
	const out = await shippedVersions();
	const moved = roadmap.items.filter((item) => isReady(item) && out.has(versionOf(item) ?? ''));
	const missing = moved.filter((item) => !hasLines(item)).map((item) => item.id);
	if (missing.length > 0) {
		throw new Error(
			`Roadmap items Ready for a released version have no changelog lines in roadmap.json: ${missing.join(', ')}`,
		);
	}
	return moved;
}

// The items the Roadmap page still shows.
export async function boardItems() {
	const moved = new Set(await movedItems());
	return roadmap.items.filter((item) => !moved.has(item));
}

export interface ChangelogLine {
	text: string;
	// A feature's vote count, on its last line.
	votes?: number;
}

// A released version's lines from the Roadmap, by heading, in roadmap.json order.
export async function roadmapChangelog(version: string) {
	const lines: Record<ChangelogHeading, ChangelogLine[]> = { ADDITIONS: [], PATCHES: [], OTHERS: [] };
	for (const item of await movedItems()) {
		if (versionOf(item) !== version) continue;
		let last: ChangelogLine | undefined;
		for (const heading of CHANGELOG_HEADINGS) {
			for (const text of item.changelog?.[heading] ?? []) {
				last = { text };
				lines[heading].push(last);
			}
		}
		if (item.section === 'features' && last) last.votes = votes[item.id] ?? 0;
	}
	return lines;
}
