import { defineCollection } from 'astro:content';
import { z } from 'astro/zod';
import { docsLoader } from '@astrojs/starlight/loaders';
import { docsSchema } from '@astrojs/starlight/schema';

// A feature stop on a release showcase (src/components/showcase/Stops.astro). Stops follow
// where the feature sits in the app, from the Main Menu to the Bot Manager.
const stop = z.object({
	title: z.string(),
	// Where to find it in the app, such as "Main Menu > Settings".
	where: z.string(),
	blurb: z.string(),
	// What the image will show. Until the art exists, this is written on the placeholder.
	image: z.string(),
	// Set while the wording still has to be checked by the maintainer. Not rendered.
	review: z.boolean().optional(),
});

export const collections = {
	docs: defineCollection({
		loader: docsLoader(),
		schema: docsSchema({
			extend: z.object({
				// Looping video shown behind the page title (see src/components/PageTitle.astro).
				// Paths are relative to public/, without the site base. Kept as the short form of
				// `heroMedia.video` + `heroMedia.poster`.
				titleVideo: z.object({ src: z.string(), poster: z.string() }).optional(),
				// Art behind a showcase or home hero (src/components/showcase/HeroMedia.astro): a
				// video (with a still `poster`), or an image. Paths are relative to public/, without
				// the site base. Without a file, `placeholder` says what the art should show and is
				// written on a placeholder backdrop.
				heroMedia: z
					.object({
						video: z.string().optional(),
						image: z.string().optional(),
						poster: z.string().optional(),
						placeholder: z.string().optional(),
					})
					.optional(),
				// Release tag whose files get download buttons under the title, from
				// src/data/releases.json (see src/components/DownloadButtons.astro).
				release: z.string().optional(),
				// Showcase header and highlights (see src/components/showcase/). `home` is the home
				// page poster, `release` a major release, `build` a lighter header for older builds.
				showcase: z
					.object({
						kind: z.enum(['home', 'release', 'build']),
						kicker: z.string().optional(),
						stamp: z.string().optional(),
						intro: z.string().optional(),
						presenting: z
							.object({
								kicker: z.string(),
								before: z.string(),
								mark: z.string(),
								after: z.string(),
								review: z.boolean().optional(),
							})
							.optional(),
						stops: z.array(stop).optional(),
						note: z
							.object({
								tag: z.string(),
								title: z.string(),
								body: z.string(),
								review: z.boolean().optional(),
							})
							.optional(),
					})
					.optional(),
			}),
		}),
	}),
};
