// @ts-check
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import starlightCelestiaTheme from 'starlight-theme-celestia';
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
			components: {
				SiteTitle: './src/components/SiteTitle.astro',
				SocialIcons: './src/components/SocialIcons.astro',
				PageTitle: './src/components/PageTitle.astro',
			},
			favicon: '/favicon.png',
			customCss: ['./src/styles/accent.css', './src/styles/fixes.css'],
			head: [
				{ tag: 'script', content: countPageView },
			],
			social: [
				{ icon: 'github', label: 'GitHub', href: 'https://github.com/GentleGanku/GrimliteRev' },
				{ icon: 'discord', label: 'Discord', href: 'https://discord.gg/NZ6vmDTu44' },
			],
			sidebar: [
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
			plugins: [
				starlightCelestiaTheme({
					stylingSystem: 'css',
					nav: [
						{ label: 'Changelogs', href: '/GrimliteRev/changelog/v1-4-0/' },
						{ label: 'Releases', href: 'https://github.com/GentleGanku/GrimliteRev/releases' },
						{ label: 'Ko-fi', href: 'https://ko-fi.com/gentleganku' },
					],
				}),
			],
		}),
	],
});
