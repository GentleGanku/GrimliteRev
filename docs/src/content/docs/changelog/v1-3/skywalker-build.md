---
title: Grimlite Rev v1.3 Skywalker Build
sidebar:
  label: Skywalker Build
---

## 1 February 2022:

- Application does no longer stay (always) on top of the currently occupied windows. Instead, it opens on top of them upon launch and can be side-tabbed after.
- Small changes to backend.

## 30 January 2022:

- Application now opens on top of the currently opened windows upon launch.
- Application now does a version check, and sends a message for whether you want to download the latest version or not if the build is outdated. Executed only when you open the client or the About panel.
- Logs for Anti-Counter Attack prompt has been changed to a game popup that will temporarily show in the middle of the game screen.
- “Item has (not) dropped” statement command now recognizes the Automatic Drop Accept event.
- Complete Quest command now waits till the quest is no longer in progress for 3 seconds in 0.5 second interval check, instead of being 15 seconds in 1 second interval.
- Moved the logging prompts for the bot's activities from the Debug Log to the Bot Log.
- Revamped the Unbank List system. Changed to Items List and no longer has innate functions. Added 2 new item options: (1) Take items to inventory upon Start Bot, and (2) Notify item progress upon pickup (works by showing a yellow message (popup) on the middle of the game screen with the text as the item progress). These options are available on the Item tab in the Bot Manager. 
- Added a new bot option: Relogin upon 3 consecutive Quest Completion failures. Available on the Quest tab in the Bot Manager and can be saved in the file. Relogs the player in to the game after 3 consecutive quest completion failures in under 15 seconds.
- Added a new bot command: Use Boost. Located as a dropdown item on the Item tab in the Bot Manager. Integrated with variable system. Executes the command by using a boost based on the corresponding item name, and when the boost of the said item is not active. 
- Added 4 new options to use on the bot client's main menu: Provoke All Monster, Untarget Self, Auto-save State, and Fast Walk Speed.
- Added a new parameter for Skill Command: Target Self checkbox. Available on the Combat tab in the Bot Manager. When enabled, it avoids targetting any monster with this parameter on the skill command.
- Added a new text label: Guide on Setting Up The Description. Located on the Misc2 tab in the Bot Manager. Gives you information about setting up the description in Rich Text Format for the bot's info panel. Can be clicked to see the explanation.
- Added 1 new tab to the Logs panel: Bot. Used for logging the bot's activities (when active). 
- Rewording to some of the texts on the Item tab in the Bot Manager to properly fit with their usabilities: Whitelist to Drop list, Unbank to Item list, Drop Delay to Pickup Delay, and the Items' text labels.
- Rewording to some of the bot options to properly fit with their usabilities: Infinite Attack Range to Infinite Range, Enemy Magnet to Target Magnet, and Disable Player Animations to Disable Skill Animations.
- Fixed the bot client for stuttering whenever the player gets an item drop.
- Fixed the bot client for not registering the server datas properly whenever the player logs onto the server list.
- Fixed the "Monster is (not) in room" statement command for not functioning properly.
- Fixed the Anti-Counter Attack feature for not working properly on the Skill Set (kill commands) feature.
- Fixed the Hide Players feature for not hiding players whenever the player moves to another cell.
- Fixed the Disable Player Animation feature for not disabling the player's skill animation properly.
- Changes to backend for less CPU/Memory usage.
- Changes to UI.
