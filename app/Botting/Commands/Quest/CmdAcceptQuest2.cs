using Grimoire.UI;
using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Quest
{
    public class CmdAcceptQuest2 : IBotCommand
    {
        #region Properties

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

        #endregion

        #region Methods

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

        #endregion
    }
}
