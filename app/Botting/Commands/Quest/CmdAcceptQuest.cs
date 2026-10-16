using Grimoire.Game;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Quest
{
    public class CmdAcceptQuest : IBotCommand
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

        #endregion
    }

}