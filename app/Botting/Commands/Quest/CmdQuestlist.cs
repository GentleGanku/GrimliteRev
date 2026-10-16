using Grimoire.Game;
using Grimoire.UI;
using System.Threading.Tasks;
using System.Linq;

namespace Grimoire.Botting.Commands.Quest
{
    public class CmdQuestlist : IBotCommand
    {
        public enum State
        {
            Add,
            Remove,
            Clear
        }

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

        public State state
        {
            get;
            set;
        }

        private int _questID => int.Parse(BotManager.Instance.ActiveBotEngine.Value(QuestID));

        private string _itemID => (!string.IsNullOrEmpty(ItemID)) ? BotManager.Instance.ActiveBotEngine.Value(ItemID) : null;

        public async Task Execute(IBotEngine instance)
        {
            switch (state)
            {
                case State.Add:
                    if (Player.Quests.LoadedQuests.Find(q => q.Id == _questID) == null)
                    {
                        Player.Quests.GetQuest(_questID);
                        await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find(q => q.Id == _questID) != null, null, 6, 500);
                    }
                    BotManager.Instance.AddQuest(_questID, _itemID, true);
                    break;
                case State.Remove:
                    if (Player.Quests.LoadedQuests.Find(q => q.Id == _questID) == null)
                    {
                        Player.Quests.GetQuest(_questID);
                        await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find(q => q.Id == _questID) != null, null, 6, 500);
                    }
                    BotManager.Instance.RemoveQuest(_questID, _itemID, true);
                    break;
                case State.Clear:
                    BotManager.Instance.lstQuests.Items.Clear();
                    instance.Configuration.Quests.Clear();
                    break;
            }
        }

        public override string ToString()
        {
            switch (state)
            {
                case State.Add:
                    return $"Add to Quest list: {QuestID}, {(ItemID != null ? ItemID : null)}";
                    break;
                case State.Remove:
                    return $"Remove from Quest list: {QuestID}, {(ItemID != null ? ItemID : null)}";
                    break;
                case State.Clear:
                    return "Clear Quest list";
            }
            return "Quest list";
        }
    }
}