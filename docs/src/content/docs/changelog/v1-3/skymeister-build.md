---
title: Grimlite Rev v1.3 Skymeister Build
sidebar:
  label: Skymeister Build
release: v1.3-Skymeister
---

## 8 May 2022:

- Bug fixes and updates to backend code

## 5 May 2022:

- Fixed and improved the Bank Items system (however it will still require you to relog if you wish to put the recently-unbanked items back to the bank, as those items' coin tag isn't registered).
- Fixed the Quest List system.
- Fixed the Kill For Item/TempItem bot command.
- Fixed the Kill bot commands not using skills properly on some cases.
- Fixed the Chat Command for Combat not executing properly on some cases.
- Fixed the Anti-Counter Attack function not executing properly on some cases.
- Fixed the Anti-AFK function not cancelling AFK state on some cases.
- Fixed the bot commands with the Cell prompt for registering the incorrect Cell value.
- Fixed the bot commands with the Quest prompt for not loading the targeted quest if it hasn't been pre-loaded.
- Improved backend code.
- Quest Preload function will no longer scan a Quest-related bot command if it has a Variable value.
- Ping Monitor will no longer send a ping scan (as it cancels AFK state) whenever the player goes AFK.

## 2 May 2022:

- Fixed the Buy Item commands not executing properly for unstackable items.
- Fixed the Skill Set system using randomized skills when there's a custom Skill Set being used.
- Fixed the Manual Buy Item functions for not executing properly.
- Fixed the Quest List/Drop List commands for not executing properly.
- Fixed the Anti-Counter Attack function for not executing on some cases.
- Improved the Quest List system.
- Improved backend code.
- Reworked the Move to Cell function. Now moves to an empty cell (a cell without any monster) if the targeted cell is either Wait or Blank. If such empty cell doesn't exist, it will instead move to Enter cell.
- Auto Relogin and Immediate Login will now connect you to the server with the highest player count (next to the potentially full servers). This only applies if the targeted server is a potentially full one.

## 7 April 2022:

- All panels beside the main one is now always on-top.
- Application's main panel now opens on top of windows with a different behaviour.
- Fixed a backend property for not working properly.
- Fixed the Pads' dropdown list returning an error when you're in the loading screen.
- Fixed the Bot Manager panel not hiding itself properly when you start the bot without opening the panel first.
- Fixed the Provoke in Map command not utilizing the custom aggromon packet.
- Fixed the Ping Monitor function executing even when the Ping box is not visible.
- Ping Monitor's timer is now on 10 seconds, instead of 6 seconds.

## 6 April 2022:

- Application now does a Flash plugin check, and sends a message if the plugin has not been installed. Executed only when you open the client.
- Application's panels do no longer stay (always) on top of the currently occupied windows. Instead, it opens on top of them upon launch and can be side-tabbed after. This excludes the Bot Manager.
- Ping Monitor now monitors over your ping (latency time) to the game server, instead of monitoring the game server itself. Timer is 6 seconds.
- Clicking a Search on Wiki button will now open a wiki link on the default browser.
- Clicking the Character Page button by the game’s interface menu will now open a character page of your account on the default browser.
- Sending a Join command with "1e99" as the room number by the chat box will now cause of joining a map but with randomized room number, ranging from 1001 to 99999. (ex. "/join battleon-1e99")
- Turning on the Lag Killer will now send an in-game message box indicating about the active mode and how you can turn it off. Only applies for one time. 
- Removed the prompt where it apparently gives a false-positive virus result upon opening the application (happens on Windows Defender).
- Removed the Packet Spammer's Stop button. Function is now integrated into the Start button and can be used after the Packet Spammer starts.
- Fixed the Custom Travel's chat trigger for not working properly.
- Fixed the Packet Spammer's Load function for not loading the saved value of the packet spam delay.
- Fixed the Application's panels for not opening properly after being minimized (hidden) and then getting closed from the Taskbar.
- Fixed the Kill/Killfor command for not canceling auto attack properly when a Counter Attack charge is active.
- Minor changes to UI.
- Significant backend changes. 
- Setting up the FPS Cap will now send an in-game message that temporarily appears and indicates about the old FPS Cap and the new FPS Cap.
- Opening the Pads dropdown will now give you a list of pads that can be used in your currently occupied cell and highlight them with the Green color. (Preset pads are still available on the dropdown)
- Monster Grabber now gives you a list of monsters that are available in the whole map, instead of just your currently occupied cell.
- Added Drops section to the Monster Grabber. This section utilizes the wiki. Works by loading the wiki and then fill the section with item drops from the wiki scan. This section will not be available (section being empty) if the grabber returns an error for it.
- The following statement commands will now skip an index when they return an error instead of executing the command: Player in Cell, Available Monsters, Visible Monsters.
- Application's process now gets killed automatically after exit.
- Fixed the Player in Cell statement commands for not working properly when a monster has died once.
- Improved the Quest Preload function. Now scans every Quest-related commands, the Quest List configuration, and the tryQuestComplete packet, while ignoring Quest IDs that already loaded. Additionally, it can now manage up to more than 30 Quest IDs. Occurs only before the bot starts executing commands.
- Attack/UseSkill functions (not the commands) will now adapt to the Counter Attack charge, stopping auto attack and skills from being used by the bot. (Means that they're now usable with plugins when there's a Counter Attack charge)
- Improved the Kill/KillFor command's functionalities. Now supports Variable (including all values on the commands) and the 5th skill/Potion slot (when using random skills, if no Skill Set is used).
- Optimized most of the bot commands' functionality on their execution (that plays in-game). Some commands will now wait for certain conditions, if they’re required for proper command execution.
- Improved some of the bot commands' line for better readability.
- Minor bug fixes.
- Added a new feature: Chat Command. Type “/help” in chat to bring up information about the Chat Commands.
- Fixed the Provoke toggle commands for not provoking the monsters properly when the Overall Bot Delay is minimal.
- Fixed the Move to Cell function. Now converts Wait value to Blank value (originally from Blank to Wait). Additionally, it will now keep moving to the corresponding cell/pad until it moves to the said cell (the corresponding one).
- Added a new option: Exit Combat upon Stop Bot. Located at the Bot Manager. (When enabled) Whenever you stop the bot, it will rejoin the current cell to exit the combat state.
- Added a new bot option: Relogin upon 3 Consecutive Item Buy Failures. Located at the Bot Manager. (When enabled) Automatically relogs into the game whenever the bot detects the Item Buy failure 3 times in a row, under 15 seconds.
- Added a new parameter for the Buy Item commands: Manual. (When enabled from the command) Opens the loaded shop, searches and buys the corresponding item by executing the Click Events.
- The following options can now be saved to the BotClientConfig file: Hide Players in Yulgar, Anti-AFK, Anonymous Room, Anonymous User, and Exit Combat upon Stop Bot.
