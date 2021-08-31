# Grimlite Rev
A revisioned version of Grimlite, an AQW Bot Client. <br />
# How to Use:
Do the steps below.
<br />
(1) Download the Grimlite Rev Package from the main page (this repository) <br />
(2) Extract the package (RAR file), <br />
(3) Use the client through the .EXE file <br />
(4) Enjoy. <br />
# Changelogs:
###### 1 September 2021:
New:
- Added “Immediate Login” button (on Tools tab from the main menu). Used for Login (click on button), Connect to Server (select a server on the dropdown list), and Login + Connect to Server (click on dropdown and press Enter key). The Login depends on the Username and the Password from the login screen. The Server is set default to Twilly, can be configured (with the dropdown index) to other servers in the BotClientConfig.cfg file.
- Added “Immediate Login” configuration to Actions’ dropdown list in the Hotkeys panel.
- Added “Clear Captured” button (in the Packet Tamperer panel). Used for clearing the Captured Packet textbox.
- Added “Load Map SWF” button and command. Used for loading a map SWF on client-side.
- Added Aura Check statements, consists of Player Aura and Target Aura lists. They’re used for checking ability buff/debuff on the player or the target based on the commands.
- Added “Player In Combat” statements. Used for checking a player’s state on combat.
- Added “Cancel Target” command. Used for canceling the target out of the (self) player’s focus.
- Added “Cancel Auto Attack” command. Used for canceling the (self) player’s auto attack out of combat.
- Added “Monster Health” statements. Used for checking a monster’s health (greater/lesser than).
- Added “Health Percentage” statements. Used for checking the (self) player’s health percentage (greater/lesser than).
- Added “Buy Item By ID” commands. Used for buying an item by Item ID, Shop ID, and Shop Item ID. 
- Added “Stop Bot With Message” command. Used for stopping the bot automatically by command, attached with a customizable message.
- Added “Custom Class” command. Used for changing the (self) player’s class name on client-side.
- Added “Provoke All Monster” option and toggle commands. Used for drawing aggro/provoking upon all monster in the map. Provoke packet can be customized in the Custom box.
- Added “Skill Available” statements. Used for checking a skill’s availability (by using a skill index), based on the commands.
- Added “Player Count in Cell” statements. Used for checking player count in a cell.
- Added “Player’s Equipped Class” statements. Used for checking a player’s equipped class.
- Added “Get Class with Variable” statement command. Used for getting the (self) player’s equipped class’ name and sets it to a variable.
- Added built-in hotkey “CTRL + X”. Used for copying the selected index/command and then removes them from the list.
- Added built-in hotkey “CTRL + R” as an alternative for Remove function (CTRL + DEL).
- Added “End Description” text into the Quest Grabber.

Changes:
- UI changes.
- Improved backend functions.
- Improved “Auto Relogin” feature’s functions.
- Bot can now configure any text with “[Class Item]” on the commands.
- For skill command, the bot will now attack any other monsters available if the specified monster is not available.
- For skill command, you can now input to the Monster’s value with “Self-targeted” if you want to use self-targeted skills without attacking a monster.
- Kill/Killfor/UseSkillCmd can now focus on untargetable monsters.
- Revamped Packet Command. It now has “Spam On/Off” toggle commands and better packet send function with customizable delay.
- Refined the Safe Skill system, now with “Health/mana is greater/less than” conditions.
- Search Command function is now able to search through the selected command list with any keywords. It is now also bound with “CTRL + F” hotkey for immediate jump control and with Enter hotkey for immediate find control.
- “Player is in Cell” and “Player is in My Cell” statement commands now have an option (by “*”) for Any targets.
- Added an innate delay to Index commands.
- Warning message “Null Username” text has been rewritten for much clearer information.
- Increased the innate delay on “Complete Quest” command for stability improvement.
- Clear button, Get Bots button, Plugin button, and buttons in More dropdown list now has a confirmation message for action.
- Clear All Commands is now a button and has been moved to the Menu page (can be found by right clicking on any space in the Bot Manager.
- Fixed “Start” button (the one on Bot tab) not functioning properly when the Bot Manager hasn’t been opened.
- Fixed “IsMember” statement commands not functioning properly.
- Fixed Whitelist commands not functioning properly.
- Fixed Custom Commands not being able to render Centered Text.
- Fixed “Start Bot” button (on the main menu) not being able to start the bot without opening the Bot Manager first.
- Fixed Arrow “Up” button not resizing properly upon maximizing window on the Bot Manager.
- Logs panel can now register texts without it being opened.
- Stop button on the strip menu is now properly synchronized to the commands in the Bot Manager.
- Information box in the bot manager window does no longer disappear upon starting the bot.
- Load Bank function now closes the Bank after opening it.
- Return command does no longer load the Bank.
- Load Bot command does no longer load the Bank.
- Removed “Whitelist Toggle” command.
- Removed “Aggromon Toggle” command.
