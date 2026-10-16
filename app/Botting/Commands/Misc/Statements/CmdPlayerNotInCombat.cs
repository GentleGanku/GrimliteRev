using Grimoire.Tools;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerNotInCombat : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayerNotInCombat()
        {
            Tag = "Player";
            Text = "Player is not in combat";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Flash.Call<bool>("CheckPlayerInCombat", instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player is not in combat state: {Value1}";
        }

        #endregion
    }
}