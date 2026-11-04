// The Roadmap's items (src/data/roadmap.json), shared by the Roadmap page and Home's "Coming in".
//
// Features move through four lanes in order: Proposed (suggestions, the most-voted move up),
// Up next (slated for `next`, the version after the latest release), In progress and Released
// (done and ready for release). Miscellaneous, what keeps it running well (fixes after game
// updates, speed, stability), has two: Now and Completed. Visitors' suggestions come from the
// counter Worker and join Proposed.
//
// For a local preview, `ROADMAP_SAMPLE=1 npm run dev` reads src/data/roadmap.sample.json
// instead: labelled sample items, local only (.git/info/exclude), never on the live site.
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
	review?: string;
}

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
		{ id: 'up-next', title: 'Up next', note: `Planned for ${roadmap.next}.`, empty: `Nothing is planned for ${roadmap.next} yet.` },
		{ id: 'in-progress', title: 'In progress', note: 'Being built now.', empty: 'Nothing is being built right now.' },
		{ id: 'released', title: 'Released', note: 'Done, ready to ship.', empty: 'Nothing is ready for release yet.' },
	],
	misc: [
		{ id: 'now', title: 'Now', note: 'Being worked on.', empty: 'Nothing is being worked on right now.' },
		{ id: 'completed', title: 'Completed', note: 'Finished.', empty: 'Nothing is finished yet.' },
	],
};

// The version a card shows: its own, or `next` while it's up next or being built.
export const versionOf = (item: RoadmapItem) =>
	item.version ?? (item.lane === 'up-next' || item.lane === 'in-progress' ? roadmap.next : undefined);
