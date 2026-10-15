# Grimlite Rev

A bot client for AdventureQuest Worlds. Grimlite Rev is a revision of Grimlite, which is based on Grimoire (the original iteration).

Bug reports can go either to the [Discord server](https://discord.gg/NZ6vmDTu44) or the repository's Issues.

[Download](https://github.com/GentleGanku/GrimliteRev/releases/latest) · [Changelogs](https://gentleganku.github.io/GrimliteRev/) · [Discord](https://discord.gg/NZ6vmDTu44) · [Ko-fi](https://ko-fi.com/gentleganku)

## **Status**

**Grimlite Rev** returns with a new update to address several issues arose after the recent **Game Release (9 October 2026)**. This update, Version 1.4.0, introduces visual features, bug patches, and adjustments while retiring a couple of old disfunctional features.

Current plan is to have several updates up for release to keep **Grimlite Rev** up with the current trend. No promises, but expect more visual features, new bot commands, and experiments.

The changelog for every version is on the [project site](https://gentleganku.github.io/GrimliteRev/).

## **Requirements**

- Windows with [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48). 
- [Clean Flash plugin](https://gitlab.com/cleanflash/installer/-/releases), ActiveX version. 

## **How to Use**

1. Download a package from the [latest release](https://github.com/GentleGanku/GrimliteRev/releases/latest), depending on how you look at your computer: 
   1. `Grimlite.Rev-v1.4.0-x64.zip` if the computer looks modern and nice, or 
   2. `Grimlite.Rev-v1.4.0-x86.zip` if the computer looks very old or seems slow at running things.
2. Extract the zip file.
3. Open the `Grimlite Rev` folder and run `Grimoire.exe`.

The Bot Manager opens and saves `.gbot` files in the `Bots` folder by default. Plugin DLLs go in the `Plugins` folder.

If you're not sure of which one to download, try x64 first, and then switch to x86 if Grimlite Rev reports that Flash is missing.

## **Build**

The repository has three parts:

- `app/`: the application client.
- `game-client/`: the game client that loads the game into the application.
- `docs/`: the project site.

### **App** 

Open `app/Project Grimlite.sln` in Visual Studio, or run the build script which builds x86 and x64:

```
powershell -ExecutionPolicy Bypass -File app/build.ps1
powershell -ExecutionPolicy Bypass -File app/build.ps1 -Configuration Release
```

Builds go to `app/bin/<platform>/<configuration>/`. A Release build also writes the release zips to `app/bin/Packages/`.

### **Game Client**

The compiled `game-client/grimlite-rev.swf` is already in the repository, so you only need to rebuild it after changing the ActionScript in `game-client/grimoire/`. Build it from `game-client/` with `mxmlc` from the HARMAN AIR SDK:

```
mxmlc +configname=flex -source-path+=. -target-player=32.0 -swf-version=43 -default-size 960 550 -default-frame-rate 30 -default-background-color=0x000000 -use-network=false -default-script-limits 256 30 -output grimlite-rev.swf grimoire/Root.as
```

### **Site**

In `docs/`, run `npm install`, then `npm run dev`. `npm run build` needs internet access to also draw the download chart from the GitHub API.

## Metrics

<div align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="https://gentleganku.github.io/GrimliteRev/metrics/downloads-dark.svg">
    <img alt="Total downloads over time, counted from GitHub release files" src="https://gentleganku.github.io/GrimliteRev/metrics/downloads-light.svg">
  </picture>
  <br><br>
  <a href="https://star-history.com/#GentleGanku/GrimliteRev&Date">
    <picture>
      <source media="(prefers-color-scheme: dark)" srcset="https://api.star-history.com/svg?repos=GentleGanku/GrimliteRev&type=Date&theme=dark">
      <img alt="Star history of GentleGanku/GrimliteRev" src="https://api.star-history.com/svg?repos=GentleGanku/GrimliteRev&type=Date">
    </picture>
  </a>
</div>

## Contributors and Credits

- Grimlite Rev team: [Ganku](https://github.com/GentleGanku)
- Previous Grimlite team: [wisp/satan](https://github.com/wispie), [cat](https://github.com/0zl)
- Previous Grimoire team: [Biney](https://www.mpgh.net/forum/member.php?u=4062680), [EmperorArts](https://www.mpgh.net/forum/member.php?u=2374072)
- Thanks to the [Grimlite contributors](https://github.com/0zl/meow/graphs/contributors) and [Maark](https://github.com/vtrsz) for [their Grimlite Rev fork](https://github.com/vtrsz/GrimliteRev).

## License

Grimlite Rev is released under CC0 1.0 Universal. See [LICENSE](LICENSE).
