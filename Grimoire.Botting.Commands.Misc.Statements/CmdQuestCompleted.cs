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

        public Task Execute(IBotEngine instance)
        {
            if (!Player.Quests.CanComplete(int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Quest can be turned in: " + Value1;
        }
    }
}