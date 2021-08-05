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
            await instance.WaitUntil(() => Player.Quests.QuestTree.Any((Game.Data.Quest q) => q.Id == Quest.Id));
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest));
            Quest.Accept();
            await instance.WaitUntil(() => Player.Quests.IsInProgress(Quest.Id));
        }

        public override string ToString()
        {
            return $"Accept quest: {Quest.Id}";
        }
    }
}