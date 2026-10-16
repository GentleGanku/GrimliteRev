using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerAuraIsNotActive : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayerAuraIsNotActive()
        {
            Tag = "Player Aura";
            Text = "Player Aura is not active";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.isAuraActive("Self", instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player Aura is not active: {Value1}";
        }

        #endregion
    }
}