using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdQuestCompleted : StatementCommand, IBotCommand
    {
        public CmdQuestCompleted()
        {
            Tag = "Quest";
            Text = "Quest can be turned in";
        }

        public async Task Execute(IBotEngine instance)
        {
            if (Player.Quests.LoadedQuests.Find(q => q.Id == int.Parse(instance.Value(Value1))) == null)
            {
                Player.Quests.GetQuest(int.Parse(instance.Value(Value1)));
                await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find(q => q.Id == int.Parse(instance.Value(Value1))) != null, null, 6, 500);
            }
            if (!Player.Quests.CanComplete(int.Parse(instance.Value(Value1))))
            {
                instance.Index++;
            }
        }

        public override string ToString()
        {
            return "Quest can be turned in: " + Value1;
        }
    }
}