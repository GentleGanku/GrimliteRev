using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdQuestNotInProgress : StatementCommand, IBotCommand
    {
        public CmdQuestNotInProgress()
        {
            Tag = "Quest";
            Text = "Quest is not in progress";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Quests.IsInProgress(int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Quest is not in progress: " + Value1;
        }
    }
}