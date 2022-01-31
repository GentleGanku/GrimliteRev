using AxShockwaveFlashObjects;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public static class BotUtilities
    {
        public static AxShockwaveFlash flash;

        public static async Task WaitUntil(this IBotEngine instance, Func<bool> condition, Func<bool> prerequisite = null, int timeout = 15, int delay = 1000)
        {
            int iterations = 0;
            while ((prerequisite ?? (() => instance.IsRunning && Player.IsLoggedIn && Player.IsAlive))() && !condition() && (iterations < timeout || timeout == -1))
            {
                await Task.Delay(delay);
                iterations++;
            }
        }

        public static bool RequiresDelay(this IBotCommand cmd)
        {
            if (cmd is StatementCommand || cmd is CmdIndex || cmd is CmdLabel || cmd is CmdGotoLabel || cmd is CmdBlank || cmd is CmdSkillSet)
                return false;
            return true;
        }

        public static bool ShouldLog(this IBotCommand cmd)
        {
            if (cmd is CmdBlank || cmd is CmdBlank2 || cmd is CmdBlank3)
                return false;
            return true;
        }

        public static string LogSpecificCmd(this IBotCommand cmd)
        {
            if (cmd is CmdLabel)
                return "On label ";
            else if (cmd is StatementCommand && !cmd.ToString().StartsWith("Set") && !cmd.ToString().StartsWith("Get") && !cmd.ToString().StartsWith("Update"))
                return "On statement - ";
            return null;
        }

        public static bool HasLoadedQuests
        {
            get;
            set;
        }

        public static void LoadAllQuests(this IBotEngine instance)
        {
            if (!HasLoadedQuests)
            {
                List<int> list = new List<int>();
                foreach (IBotCommand command in instance.Configuration.Commands)
                {
                    if (command is CmdAcceptQuest cmdAcceptQuest)
                    {
                        list.Add(cmdAcceptQuest.Quest.Id);

                    }
                    else if (command is CmdCompleteQuest cmdCompleteQuest)
                    {
                        list.Add(cmdCompleteQuest.Quest.Id);
                    }
                }
                list.AddRange(instance.Configuration.Quests.Select((Quest q) => q.Id));
                if (list.Count > 0)
                {
                    Player.Quests.Get(list);
                }
                HasLoadedQuests = true;
            }
        }

        public static bool ShouldUseSkill
        {
            get;
            set;
        }
        public async static Task MoveToSelfCell()
        {
            Player.MoveToCell(Player.Cell, Player.Pad);
        }
    }
}