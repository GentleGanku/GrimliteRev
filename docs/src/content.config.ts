import { defineCollection } from 'astro:content';
import { z } from 'astro/zod';
import { docsLoader } from '@astrojs/starlight/loaders';
import { docsSchema } from '@astrojs/starlight/schema';

export const collections = {
	docs: defineCollection({
		loader: docsLoader(),
		schema: docsSchema({
			extend: z.object({
				// Looping video shown behind the page title (see src/components/PageTitle.astro).
				// Paths are relative to public/, without the site base.
				titleVideo: z.object({ src: z.string(), poster: z.string() }).optional(),
			}),
		}),
	}),
};
