// @ts-check
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import worker from './src/data/worker.json' with { type: 'json' };

// Counts a page view in the counter Worker (worker/src/index.js). Local dev servers aren't
// counted.
const countPageView = `if (!/^(localhost|127\\.0\\.0\\.1)$/.test(location.hostname)) navigator.sendBeacon?.(${JSON.stringify(`${worker.url}/hit`)}, location.pathname);`;

export default defineConfig({
	site: 'https://gentleganku.github.io',
	base: '/GrimliteRev',
	integrations: [
		starlight({
			title: 'Grimlite Rev',
			routeMiddleware: './src/routeData.ts',
			components: {
				Header: './src/components/Header.astro',
				SiteTitle: './src/components/SiteTitle.astro',
				PageTitle: './src/components/PageTitle.astro',
				MarkdownContent: './src/components/MarkdownContent.astro',
				Footer: './src/components/Footer.astro',
			},
			favicon: '/favicon.png',
			customCss: ['./src/styles/theme.css', './src/styles/fixes.css'],
			head: [
				{ tag: 'script', content: countPageView },
			],
			social: [
				{ icon: 'github', label: 'GitHub', href: 'https://github.com/GentleGanku/GrimliteRev' },
				{ icon: 'discord', label: 'Discord', href: 'https://discord.gg/NZ6vmDTu44' },
			],
			sidebar: [
				'roadmap',
				'metrics',
				{
					label: 'Changelogs',
					items: [
						'changelog/v1-4-0',
						{
							label: 'v1.3',
							items: [
								'changelog/v1-3/1-3-4',
								'changelog/v1-3/skymeister-build',
								'changelog/v1-3/skywalker-build',
								'changelog/v1-3/skyguard-build',
							],
						},
					],
				},
			],
		}),
	],
});
