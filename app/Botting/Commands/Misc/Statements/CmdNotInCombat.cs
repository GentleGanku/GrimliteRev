using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotInCombat : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdNotInCombat()
        {
            Tag = "This player";
            Text = "Not in combat";
        }

        #endregion

        #region Methods

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
            return "This player is not in combat state";
        }

        #endregion
    }
}