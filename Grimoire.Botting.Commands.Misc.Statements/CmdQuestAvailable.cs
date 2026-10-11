using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdQuestAvailable : StatementCommand, IBotCommand
    {
        public CmdQuestAvailable()
        {
            Tag = "Quest";
            Text = "Quest is available";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.Quests.IsAvailable(int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Quest is available: " + Value1;
        }
    }
}