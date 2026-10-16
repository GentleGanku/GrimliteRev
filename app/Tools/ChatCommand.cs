using Grimoire.Botting;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Grimoire.Tools
{
    public static class ChatCommand
    {

        private static Dictionary<string, string> ChatCommands = new Dictionary<string, string>
        {
                { "Help"             , "/help|Brings up information about all chat commands into the chat."},
                { "Combat"           , "/combat NAME|Finds and continuously attacks the monster with the NAME (* = any) on randomized skills. Using this without the NAME will stop the combat."},
                { "Provoke"          , "/provoke ID|Provokes the monster with the monster map ID (up to 100 monsters: \"/provoke 1,2,3\"). Using this without the ID will stop the provoke."},
                { "LoadQuest"        , "/loadquest ID|Opens the quest with the ID on the Quest menu (up to 30 quests: \"/loadquest 1,2,3\"). Using this without the ID will instead open loaded quests."},
                { "AcceptQuest"      , "/acceptquest ID|Accepts the quest with the ID (up to 10 quests: \"/acceptquest 1,2,3\"). Using this without the ID will instead accept loaded quests."},
                { "CompleteQuest"    , "/completequest ID|Completes the quest with the ID (up to 10 quests: \"/completequest 1,2,3\"). Using this without the ID will instead complete loaded quests."},
                { "PickDrop"         , "/pickdrop NAME|Picks up the item drop with the NAME. Using this without the NAME will instead pick up all item drops on the menu, and the rejected ones."},
                { "RejectDrop"       , "/rejectdrop NAME|Rejects the item drop with the NAME on the drop menu. Using this without the NAME will instead reject all item drops on the menu."},
                { "Shop"             , "/shop ID|Opens the shop with the ID."},
                { "MapItem"          , "/mapitem ID|Gets the map item with the ID."},
                { "CharPage"         , "/charpage NAME|Opens the character page with the NAME."},
                { "CharLink"         , "/charlink NAME|Opens the full character page with the NAME on your default browser."},
                { "Wiki"             , "/wiki NAME|Opens the wiki page with the NAME on your default browser."},
                { "FPS"              , "/fps NUMBER|Sets the game's FPS with the NUMBER. Using this without the NUMBER will instead open/close the FPS box."},
                { "Relogin"          , "/relogin|Relogs your character on the same server."},
                { "Mute"             , "/mute|Mutes or unmutes your character (depends on the condition)."}
        };

        public static async void ExecuteCommand(string text)
        {
            string command = text.Contains(" ") ? text.Split(' ')[0] : text;
            string parameter = text.Contains(" ") ? text.Split(new[] { ' ' }, 2)[1] : "";
            switch (command)
            {
                case "help":
                    foreach (string cmd in ChatCommands.Keys)
                    {
                        World.SendClientPacket($"%xt%server%-1%{ChatCommands[cmd].Split('|')[0]} - {ChatCommands[cmd].Split('|')[1]}%");
                    }
                    break;
                case "combat":
                    Combat(parameter, string.IsNullOrEmpty(parameter));
                    break;
                case "provoke":
                    Provoke(parameter.Split(',').ToList(), string.IsNullOrEmpty(parameter));
                    break;
                case "loadquest":
                    LoadQuest(parameter.Split(',').ToList(), string.IsNullOrEmpty(parameter));
                    break;
                case "acceptquest":
                    AcceptQuest(parameter.Split(',').ToList(), string.IsNullOrEmpty(parameter));
                    break;
                case "completequest":
                    CompleteQuest(parameter.Split(',').ToList(), string.IsNullOrEmpty(parameter));
                    break;
                case "shop":
                    if (!string.IsNullOrEmpty(parameter))
                    {
                        Shop.Load(int.Parse(parameter));
                    }
                    break;
                case "mapitem":
                    if (!string.IsNullOrEmpty(parameter))
                    {
                        await Engine.WaitUntil(() => World.IsActionAvailable(LockActions.GetMapItem), null, 10, 500);
                        Player.GetMapItem(int.Parse(parameter));
                    }
                    break;
                case "charpage":
                    if (!string.IsNullOrEmpty(parameter))
                    {
                        Flash.Call("DisplayCharPage", parameter);
                    }
                    break;
                case "charlink":
                    Process.Start("https://www.aq.com/aw-character.asp?id=" + parameter);
                    break;
                case "wiki":
                    Process.Start("http://aqwwiki.wikidot.com/" + parameter);
                    break;
                case "fps":
                    if (string.IsNullOrEmpty(parameter))
                    {
                        World.FPSBox = !World.FPSBox;
                        return;
                    }
                    Flash.Call("SetFPS", int.Parse(parameter));
                    break;
                case "pickdrop":
                    PickDrop(parameter);
                    break;
                case "rejectdrop":
                    Flash.Call("RejectDrop", parameter);
                    break;
                case "relogin":
                    Relogin();
                    break;
                case "mute":
                    Player.ToggleMute();
                    break;
            }
        }

        private static IBotEngine Engine => BotManager.Instance.ActiveBotEngine;

        private static CmdKill cmdCombat { get; set; }

        private static bool ShouldCombat = false;

        private static bool ShouldProvoke = false;

        public static async void Combat(string target, bool isNull)
        {
            if (BotManager.Instance.IsHandleCreated && !BotManager.Instance.btnBotStart.Visible)
            {
                return;
            }
            if (!ShouldCombat)
            {
                if (isNull)
                {
                    return;
                }
                if (World.Monsters.Count == 0)
                {
                    World.GameMessage("There are no monster in this map.");
                    return;
                }
                bool random = target == "*";
                Monster monster = null;
                if (!random)
                {
                    monster = World.Monsters.Find(m => m.Name.Equals(target, StringComparison.OrdinalIgnoreCase));
                    if (monster == null)
                    {
                        World.GameMessage("The corresponding monster does not exist.");
                        return;
                    }
                    if (!Player.Cell.Equals(monster.Cell))
                    {
                        Cell cell = Flash.Call<Cell>("GetCellInfo", monster.Cell);
                        await Task.Delay(10);
                        Player.MoveToCell(cell.Name, cell.Pads[0]);
                    }
                }
                Engine.Configuration = new Configuration()
                {
                    WaitForAllSkills = false,
                    WaitForSkill = false,
                    Skills = new List<Skill>(),
                    SkillDelay = 100
                };
                cmdCombat = new CmdKill
                {
                    Monster = random ? "*" : monster.Name
                };
                ShouldCombat = true;
                while (ShouldCombat && !BotUtilities.ActiveBot)
                {
                    await cmdCombat.Execute(Engine);
                    await Task.Delay(500);
                }
                if (BotUtilities.ActiveBot)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                }
            }
            else
            {
                await Task.Delay(10);
                ShouldCombat = false;
                Player.MoveToCell(Player.Cell, Player.Pad);
                if (!isNull)
                {
                    Combat(target, false);
                }
            }
        }

        public static async void Provoke(List<string> ids, bool isNull)
        {
            if (!ShouldProvoke)
            {
                if (isNull)
                {
                    return;
                }
                ShouldProvoke = true;
                string parameter = string.Join("%", ids);
                while (ShouldProvoke)
                {
                    World.SendPacket($"%xt%zm%aggroMon%{World.RoomId}%{parameter}%");
                    await Task.Delay(350);
                }
            }
            else
            {
                ShouldProvoke = false;
                Player.MoveToCell(Player.Cell, Player.Pad);
                if (!isNull)
                {
                    Provoke(ids, false);
                }
            }
        }

        private static async void LoadQuest(List<string> ids, bool isNull)
        {
            if (!isNull)
            {
                Player.Quests.Load(ids.ConvertAll<int>((string q) => int.Parse(q)));
            }
            else
            {
                List<Quest> IDs = (from x in Player.Quests.LoadedQuests?.GroupBy((Quest q) => q.Id)
                                  select x.First()).ToList();
                if (IDs.Count == 0)
                {
                    return;
                }
                Player.Quests.Load(IDs.ConvertAll<int>((Quest q) => q.Id));
            }
        }

        private static async void AcceptQuest(List<string> ids, bool isNull)
        {
            if (!isNull)
            {
                List<int> IDs = ids.ConvertAll<int>((string q) => int.Parse(q));
                IDs = (from x in IDs?.GroupBy((int id) => Player.Quests.LoadedQuests.Find((Quest q) => q.Id == id) == null)
                       select x.First()).ToList();
                if (IDs.Count > 0)
                {
                    Player.Quests.Get(IDs);
                    await Engine.WaitUntil(() => Player.Quests.LoadedQuests.Find((Quest q) => q.Id == IDs[0]) != null, null, 10, 500);
                }
                foreach (string id in ids)
                {
                    await Engine.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), null, 10, 500);
                    Player.AcceptQuest(int.Parse(id));
                }
            }
            else
            {
                List<Quest> IDs = (from x in Player.Quests.LoadedQuests?.GroupBy((Quest q) => q.Id)
                                   select x.First()).ToList();
                if (IDs.Count == 0)
                {
                    return;
                }
                foreach (Quest q in IDs)
                {
                    await Engine.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), null, 10, 500);
                    Player.Quests.Accept(q.Id);
                }
            }
        }

        private static async void CompleteQuest(List<string> ids, bool isNull)
        {
            if (!isNull)
            {
                List<int> IDs = ids.ConvertAll<int>((string q) => int.Parse(q));
                IDs = (from x in IDs?.GroupBy((int id) => Player.Quests.LoadedQuests.Find((Quest q) => q.Id == id) == null)
                       select x.First()).ToList();
                if (IDs.Count > 0)
                {
                    Player.Quests.Get(IDs);
                    await Engine.WaitUntil(() => Player.Quests.LoadedQuests.Find((Quest q) => q.Id == IDs[0]) != null, null, 10, 500);
                }
                foreach (string id in ids)
                {
                    await Engine.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete), null, 10, 500);
                    Player.CompleteQuest(int.Parse(id));
                }
            }
            else
            {
                List<Quest> IDs = (from x in Player.Quests.LoadedQuests?.GroupBy((Quest q) => q.Id)
                                   select x.First()).ToList();
                if (IDs.Count == 0)
                {
                    return;
                }
                foreach (Quest q in IDs)
                {
                    await Engine.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete), null, 10, 500);
                    Player.CompleteQuest(q.Id);
                }
            }
        }

        private static async void PickDrop(string parameter)
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                World.DropStack.GetDrop(parameter);
            }
            else
            {
                foreach (InventoryItem drop in World.DropStack)
                {
                    World.DropStack.GetDrop(parameter);
                    await Task.Delay(10);
                }
            }
        }

        public static async void Relogin()
        {
            Server server = Server.List.Find((Server s) => s.Name.Equals(World.ServerName(), StringComparison.OrdinalIgnoreCase));
            Player.Logout();
            Root.Instance.login_cts = new CancellationTokenSource();
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => AutoRelogin.LoginLabel, () => !Root.Instance.login_cts.IsCancellationRequested, 10, 1000);
            AutoRelogin.LoginExecute();
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !AutoRelogin.IsClientLoading("Account"), () => !Root.Instance.login_cts.IsCancellationRequested, 10, 500);
            AutoRelogin.ForceLogin(server, Root.Instance.login_cts);
        }

    }
}