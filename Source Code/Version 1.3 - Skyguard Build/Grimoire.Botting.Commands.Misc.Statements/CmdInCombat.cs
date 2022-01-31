using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdInCombat : StatementCommand, IBotCommand
    {
        public CmdInCombat()
        {
            Tag = "This player";
            Text = "Is in combat";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.CurrentState != Player.State.InCombat)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Is in combat";
        }
    }
}