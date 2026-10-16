using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdQuestNotAvailable : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdQuestNotAvailable()
        {
            Tag = "Quest";
            Text = "Quest is not available";
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            if (Player.Quests.LoadedQuests.Find(q => q.Id == int.Parse(instance.Value(Value1))) == null)
            {
                Player.Quests.GetQuest(int.Parse(instance.Value(Value1)));
                await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find(q => q.Id == int.Parse(instance.Value(Value1))) != null, null, 6, 500);
            }
            if (Player.Quests.IsAvailable(int.Parse(instance.Value(Value1))))
            {
                instance.Index++;
            }
        }

        public override string ToString()
        {
            return "Quest is not available: " + Value1;
        }

        #endregion
    }
}