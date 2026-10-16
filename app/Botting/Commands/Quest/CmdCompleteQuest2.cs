using Grimoire.Game;
using Grimoire.UI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Quest
{
    public class CmdCompleteQuest2 : IBotCommand
    {
        #region Properties

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

        private int _questID
        {
            get
            {
                return int.Parse(BotManager.Instance.ActiveBotEngine.Value(QuestID));
            }
        }

        private string _itemID
        {
            get
            {
                return (!string.IsNullOrEmpty(ItemID)) ? BotManager.Instance.ActiveBotEngine.Value(ItemID) : null;
            }
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            if (Player.Quests.LoadedQuests.Find(q => q.Id == _questID) == null)
            {
                Player.Quests.GetQuest(_questID);
                await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find(q => q.Id == _questID) != null, null, 6, 500);
            }
            if (!Player.Quests.CanComplete(_questID))
            {
                Game.Data.Quest q = Player.Quests.LoadedQuests.FirstOrDefault((Game.Data.Quest lQuest) => lQuest.Id == _questID);
                LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Failed to complete the quest: \"{q.Name}\" - ID {q.Id}.\r\n");
                return;
            }
            BotData.BotState = BotData.State.Quest;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete), null, 10, 500);
            if (instance.Configuration.ExitCombatBeforeQuest)
            {
                await BotUtilities.MoveOutOfCombat();
            }
            Player.CompleteQuest(_questID, _itemID);
            await instance.WaitUntil(() => !Player.Quests.IsInProgress(_questID), null, 6, 500);
        }

        public override string ToString()
        {
            return $"Complete quest: {((ItemID != null && ItemID != "0") ? $"{QuestID}, {ItemID}" : QuestID)}";
        }

        #endregion
    }
}
