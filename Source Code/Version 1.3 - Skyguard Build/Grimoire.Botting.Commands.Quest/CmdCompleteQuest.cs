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
            await instance.WaitUntil(() => !Player.Quests.IsInProgress(Quest.Id));
        }

        public override string ToString()
        {
            return "Complete quest: " + ((Quest.ItemId != null && Quest.ItemId != "0") ? $"{Quest.Id}, {Quest.ItemId}" : Quest.Id.ToString());
        }
    }

    public class CmdCompleteQuest2 : IBotCommand
    {
        public string QuestID
        {
            get;
            set;
        }

        public string ItemID
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            var quest = int.Parse(instance.IsVar(QuestID) ? Configuration.Tempvariable[instance.GetVar(QuestID)] : QuestID);
            var item = (ItemID != null ? (instance.IsVar(ItemID) ? Configuration.Tempvariable[instance.GetVar(ItemID)] : ItemID) : null);
            if (!Player.Quests.CanComplete(quest))
            {
                return;
            }
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete));
            BotData.BotState = BotData.State.Quest;
            Player.Quests.GetQuest(quest);
            if (instance.Configuration.ExitCombatBeforeQuest || (Player.CurrentState == Player.State.InCombat))
            {
                if (Player.CurrentState == Player.State.InCombat)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(2000);
                }
            }
            Player.CompleteQuest(quest, item);
            await instance.WaitUntil(() => !Player.Quests.IsInProgress(quest));
        }

        public override string ToString()
        {
            return $"Complete quest: {((ItemID != null && ItemID != "0") ? $"{QuestID}, {ItemID}" : QuestID)}";
        }
    }

}