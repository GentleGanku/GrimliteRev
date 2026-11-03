// Browser-tab titles (Starlight route middleware, registered in astro.config.mjs). Pages read
// "<Page> | Grimlite Rev". Home is just the site name, instead of Starlight's "Grimlite Rev |
// Grimlite Rev", and changelogs drop their leading "Grimlite Rev" and say what they are:
// "v1.4.0 changelog | Grimlite Rev". The visible page titles and og:title don't change.
import { defineRouteMiddleware } from '@astrojs/starlight/route-data';

const SITE = 'Grimlite Rev';

export const onRequest = defineRouteMiddleware((context) => {
	const route = context.locals.starlightRoute;
	const { id, data } = route.entry;
	const page = data.title.startsWith(`${SITE} `) ? data.title.slice(SITE.length + 1) : data.title;
	const text = id === 'index' || id === '' ? SITE : id.startsWith('changelog/') ? `${page} changelog | ${SITE}` : `${page} | ${SITE}`;
	const title = route.head.find((entry) => entry.tag === 'title');
	if (title) title.content = text;
});
