using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Quest
{
    public class CmdCompleteQuest : IBotCommand
    {
        public Game.Data.Quest Quest
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete));
            if (!Player.Quests.CanComplete(Quest.Id))
            {
                return;
            }
            //BotData.BotState = BotData.State.Quest;
            string pCell = Player.Cell;
            string pPad = Player.Pad;
            /*
            int tried = 0;
            while (instance.Configuration.EnsureComplete && Quest.IsInProgress && tried++ < instance.Configuration.EnsureTries)
            {
                if (instance.Configuration.ExitCombatBeforeQuest && Player.CurrentState == Player.State.InCombat)
                {
                    Player.MoveToCell("Blank", "Left");
                    await Task.Delay(300);
                    Player.MoveToCell("Blank", "Left");
                    await Task.Delay(300);
                }
                await instance.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete));
                Quest.Complete();
                await Task.Delay(400);
            }
            */
            if (instance.Configuration.ExitCombatBeforeQuest)
            {
                while (instance.IsRunning && Player.CurrentState == Player.State.InCombat)
                {
                    BotData.BotState = BotData.State.Quest;
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(1000);
                }
            }
            if (Player.CurrentState == Player.State.InCombat)
            {
                await Task.Delay(1250);
            }
            Quest.Complete();
            await instance.WaitUntil(() => !Player.Quests.IsInProgress(Quest.Id));
            /*
            if (instance.Configuration.ExitCombatBeforeQuest && Player.Cell != pCell)
            {
                Player.MoveToCell(pCell, pPad);
            }
            */
        }

        public override string ToString()
        {
            return "Complete quest: " + ((Quest.ItemId != null && Quest.ItemId != "0") ? $"{Quest.Id}:{Quest.ItemId}" : Quest.Id.ToString());
        }
    }
}