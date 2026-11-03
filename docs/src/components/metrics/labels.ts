// Places a chart's value labels so they never cover each other, a point, a line or an axis
// label: the same method as the README chart (scripts/downloads-chart.mjs). Each label tries
// six spots around its point (above, below, right, left, above-left, above-right), then the same
// six twice further out, and takes the first free one. A label with no free spot is left out
// unless it's marked `always`.
export interface Box {
	x0: number;
	y0: number;
	x1: number;
	y1: number;
}

export interface Spot {
	x: number;
	y: number;
	anchor: 'start' | 'middle' | 'end';
}

export class LabelPlacer {
	private taken: Box[] = [];
	private bounds: Box;

	constructor(bounds: Box) {
		this.bounds = bounds;
	}

	block(x0: number, y0: number, w: number, h: number) {
		this.taken.push({ x0, y0, x1: x0 + w, y1: y0 + h });
	}

	// A straight segment blocks a chain of small boxes along it, so a diagonal doesn't block its
	// whole bounding box.
	line(x1: number, y1: number, x2: number, y2: number) {
		const steps = Math.max(1, Math.ceil(Math.hypot(x2 - x1, y2 - y1) / 4));
		for (let i = 0; i <= steps; i++) {
			const px = x1 + ((x2 - x1) * i) / steps;
			const py = y1 + ((y2 - y1) * i) / steps;
			this.block(px - 2, py - 2, 4, 4);
		}
	}

	point(px: number, py: number, r = 5) {
		this.block(px - r, py - r, r * 2, r * 2);
	}

	// `w` and `h` are the label's size; the text baseline is placed `h - 3` below its top.
	// A crowded point gets two wider rings of the same spots before its label is dropped.
	place(px: number, py: number, w: number, h: number, always = false): Spot | null {
		const ring = (gap: number) =>
			[
				{ anchor: 'middle', x: px, x0: px - w / 2, y0: py - gap - h },
				{ anchor: 'middle', x: px, x0: px - w / 2, y0: py + gap },
				{ anchor: 'start', x: px + gap, x0: px + gap, y0: py - h / 2 },
				{ anchor: 'end', x: px - gap, x0: px - gap - w, y0: py - h / 2 },
				{ anchor: 'end', x: px - 4, x0: px - 4 - w, y0: py - gap - h },
				{ anchor: 'start', x: px + 4, x0: px + 4, y0: py - gap - h },
			] as const;
		const spots = [...ring(8), ...ring(22), ...ring(36)];
		const boxes = spots.map((s) => ({ s, b: { x0: s.x0, y0: s.y0, x1: s.x0 + w, y1: s.y0 + h } }));
		const free = boxes.find(({ b }) => !this.overlaps(b)) ?? (always ? boxes[0] : undefined);
		if (!free) return null;
		this.taken.push(free.b);
		return { x: free.s.x, y: free.b.y0 + h - 3, anchor: free.s.anchor };
	}

	private overlaps(b: Box) {
		const o = this.bounds;
		if (b.x0 < o.x0 || b.x1 > o.x1 || b.y0 < o.y0 || b.y1 > o.y1) return true;
		return this.taken.some((t) => b.x0 < t.x1 && b.x1 > t.x0 && b.y0 < t.y1 && b.y1 > t.y0);
	}
}
