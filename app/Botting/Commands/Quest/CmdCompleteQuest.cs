using Grimoire.Game;
using Grimoire.UI;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Quest
{
    public class CmdCompleteQuest : IBotCommand
    {
        #region Properties

        public Game.Data.Quest Quest
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete), null, 6, 500);
            if (!Player.Quests.CanComplete(Quest.Id))
            {
                LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Failed to complete the quest: \"{Quest.Name}\" - ID {Quest.Id}.\r\n");
                return;
            }
            BotData.BotState = BotData.State.Quest;
            string pCell = Player.Cell;
            string pPad = Player.Pad;
            if (instance.Configuration.ExitCombatBeforeQuest)
            {
                while (instance.IsRunning && Player.CurrentState == Player.State.InCombat)
                {
                    BotData.BotState = BotData.State.Quest;
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(2500);
                }
            }
            if (Player.CurrentState == Player.State.InCombat)
            {
                await Task.Delay(2500);
            }
            Quest.Complete();
            await instance.WaitUntil(() => !Player.Quests.IsInProgress(Quest.Id), null, 6, 500);
        }

        public override string ToString()
        {
            return "Complete quest: " + ((Quest.ItemId != null && Quest.ItemId != "0") ? $"{Quest.Id}, {Quest.ItemId}" : Quest.Id.ToString());
        }

        #endregion
    }

}