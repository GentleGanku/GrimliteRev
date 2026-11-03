// Whether Home's hero art follows the reader down the page (see FollowRail.astro): only for a
// video, or a placeholder marked `follow` that stands in for one. A static image stays in the
// hero.
import { getEntry } from 'astro:content';

export async function heroFollows() {
	const media = (await getEntry('docs', 'index'))?.data.heroMedia;
	if (!media || (media.image && !media.video)) return false;
	return media.follow ?? media.video !== undefined;
}
