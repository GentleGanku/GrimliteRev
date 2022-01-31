using Grimoire.Game;
using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
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

        public Game.Data.Quest Quest
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            int quest;
            string item;
            switch (state)
            {
                case State.Add:
                    quest = int.Parse(instance.IsVar(QuestID) ? Configuration.Tempvariable[instance.GetVar(QuestID)] : QuestID);
                    item = (ItemID != null ? (instance.IsVar(ItemID) ? Configuration.Tempvariable[instance.GetVar(ItemID)] : ItemID) : null);
                    Quest = new Game.Data.Quest
                    {
                        Id = quest,
                        ItemId = item
                    };
                    Quest.Text = (item != null) ? $"{quest}:{item}" : quest.ToString();
                    if (!BotManager.Instance.lstQuests.Items.Contains(Quest.Text))
                    {
                        Bot.Instance.Questlist.Add(quest);
                        BotManager.Instance.lstQuests.Items.Add(Quest);
                        instance.Configuration.Quests.Add(Quest);
                        Player.Quests.GetQuest(quest);
                    }
                    break;
                case State.Remove:
                    quest = int.Parse(instance.IsVar(QuestID) ? Configuration.Tempvariable[instance.GetVar(QuestID)] : QuestID);
                    int q = Bot.Instance.Questlist.IndexOf(quest);
                    BotManager.Instance.lstQuests.Items.RemoveAt(q);
                    instance.Configuration.Quests.RemoveAt(q);
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
                    return $"Add to quest list: {QuestID}, {(ItemID != null ? ItemID : null)}";
                    break;
                case State.Remove:
                    return $"Remove from quest list: {QuestID}, {(ItemID != null ? ItemID : null)}";
                    break;
                case State.Clear:
                    return "Clear quest list";
            }
            return "Quest list";
        }
    }
}