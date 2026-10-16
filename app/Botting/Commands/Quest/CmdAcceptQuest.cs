using Grimoire.UI;
using Grimoire.Game;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Quest
{
    public class CmdAcceptQuest : IBotCommand
    {
        public Game.Data.Quest Quest
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Quest;
            await instance.WaitUntil(() => Player.Quests.QuestTree.Any((Game.Data.Quest q) => q.Id == Quest.Id), null, 6, 500);
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), null, 6, 500);
            Quest.Accept();
            await instance.WaitUntil(() => Player.Quests.IsInProgress(Quest.Id), null, 6, 500);
        }

        public override string ToString()
        {
            return $"Accept quest: {Quest.Id}";
        }
    }

    public class CmdAcceptQuest2 : IBotCommand
    {
        public string QuestID
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

        public async Task Execute(IBotEngine instance)
        {
            if (Player.Quests.LoadedQuests.Find(q => q.Id == _questID) == null)
            {
                Player.Quests.GetQuest(_questID);
                await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find(q => q.Id == _questID) != null, null, 6, 500);
            }
            BotData.BotState = BotData.State.Quest;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), null, 10, 500);
            Player.AcceptQuest(_questID);
            await instance.WaitUntil(() => Player.Quests.IsInProgress(_questID), null, 6, 500);
        }

        public override string ToString()
        {
            return $"Accept quest: {QuestID}";
        }
    }

}