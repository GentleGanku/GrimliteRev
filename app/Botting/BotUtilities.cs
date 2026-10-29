using AxShockwaveFlashObjects;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Game;
using Grimoire.UI;
using Grimoire.Game.Data;
using Grimoire.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public static class BotUtilities
    {
        #region Fields

        public static AxShockwaveFlash flash;

        #endregion

        #region Properties

        public static bool ActiveBot
        {
            get;
            set;
        } = false;

        public static bool BusyClient
        {
            get;
            set;
        } = false;

        public static bool HasLoadedQuests
        {
            get;
            set;
        }

        public static bool ShouldUseSkill
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public static async Task WaitUntil(this IBotEngine instance, Func<bool> condition, Func<bool> prerequisite = null, int timeout = 15, int delay = 1000, int finalDelay = 0)
        {
            int iterations = 0;
            while ((prerequisite ?? (() => instance.IsRunning && Player.IsLoggedIn && Player.IsAlive))() && !condition() && (iterations < timeout || timeout == -1))
            {
                await Task.Delay(delay);
                iterations++;
            }
            await Task.Delay(finalDelay);
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

        /// <summary>
        /// Collects the IDs of the quests the bot's commands use. A command without a quest ID (Quest list Clear), a
        /// variable, or a value that isn't a number is skipped.
        /// </summary>
        public async static Task GetBotQuests(this IBotEngine instance)
        {
            PropertyInfo commandProperty;
            BotData.BotQuests = new List<int>();
            foreach (IBotCommand command in instance.Configuration.Commands)
            {
                if (command.GetType().ToString().Contains("Quest"))
                {
                    commandProperty = command.GetType().GetProperty("Quest") ?? command.GetType().GetProperty("QuestID") ?? command.GetType().GetProperty("Value1");
                    string value = commandProperty?.GetValue(command)?.ToString();
                    if (!string.IsNullOrWhiteSpace(value) && !value.StartsWith("[") && int.TryParse(instance.Value(value), out int questId))
                    {
                        BotData.BotQuests.Add(questId);
                    }
                }
            }
            BotData.BotQuests.AddRange(instance.Configuration.Quests.ConvertAll(q => q.Id));
            BotData.BotQuests = (from x in BotData.BotQuests?.GroupBy(id => id)
                                 select x.First()).ToList();
        }

        public async static Task LoadBotQuests(this IBotEngine instance)
        {
            if (!HasLoadedQuests)
            {
                await GetBotQuests(instance);
                List<List<int>> botQuestsList = BotData.BotQuests.Select((x, i) => new { Index = i, Value = x })
                                                    .GroupBy(x => x.Index / 30)
                                                    .Select(x => x.Select(v => v.Value).ToList())
                                                    .ToList();
                List<int> unacceptedQuests = BotData.BotQuests?.Where(id => Player.Quests.LoadedQuests.Any(q => q.Id == id) && instance.Configuration.Quests.Any(q => q.Id == id && !q.IsInProgress)).ToList();
                if (unacceptedQuests.Count > 0)
                {
                    foreach (int id in unacceptedQuests)
                    {
                        await instance.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), () => instance.IsRunning, 10, 500);
                        Player.Quests.Accept(id);
                    }
                }
                foreach (List<int> botQuests in botQuestsList)
                {
                    if (botQuestsList.Count > 1)
                    {
                        await Task.Delay(1500);
                    }
                    Player.Quests.Get(botQuests);
                    await instance.WaitUntil(() => Player.Quests.LoadedQuests.Any(q => q.Id == botQuests[0]), () => instance.IsRunning, 10, 500);
                }
                HasLoadedQuests = true;
            }
        }

        public static void AddConfigSkills(Configuration config)
        {
            if (config.Skills.Count > 0)
            {
                for (int i = 0; i < config.Skills.Count; i++)
                {
                    if (config.Skills[i].Type == Skill.SkillType.Label)
                    {
                        string label = config.Skills[i].Text.ToUpper();
                        if (BotData.SkillSet.ContainsKey(label))
                        {
                            LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] The skill set label {config.Skills[i].Text} appears more than once; the first one is used.\r\n");
                        }
                        else
                        {
                            BotData.SkillSet.Add(label, i);
                        }
                    }
                }
            }
        }

        public static void MoveToSelfCell()
        {
            Player.MoveToCell(Player.Cell, Player.Pad);
        }

        public async static void MoveToBotCell()
        {
            try
            {
                if (BotData.BotCell != null && !Player.Cell.Equals(BotData.BotCell, StringComparison.OrdinalIgnoreCase))
                {
                    Player.MoveToCell(BotData.BotCell, BotData.BotPad);
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running BotUtilities.MoveToBotCell", ex);
            }
        }

        public async static Task MoveOutOfCombat()
        {
            if (Player.CurrentState == Player.State.InCombat)
            {
                Player.MoveToCell(Player.Cell, Player.Pad);
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.CurrentState != Player.State.InCombat, null, 10, 500);
            }
        }

        #endregion
    }
}