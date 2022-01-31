using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdQuestNotAvailable : StatementCommand, IBotCommand
    {
        public CmdQuestNotAvailable()
        {
            Tag = "Quest";
            Text = "Quest is not available";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Quests.IsAvailable(int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Quest is not available: " + Value1;
        }
    }
}