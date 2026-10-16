using Grimoire.Tools;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerInCombat : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayerInCombat()
        {
            Tag = "Player";
            Text = "Player is in combat";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!Flash.Call<bool>("CheckPlayerInCombat", instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player is in combat state: {Value1}";
        }

        #endregion
    }
}