using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotInCombat : StatementCommand, IBotCommand
    {
        public CmdNotInCombat()
        {
            Tag = "This player";
            Text = "Not in combat";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.CurrentState == Player.State.InCombat)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Is not in combat";
        }
    }
}