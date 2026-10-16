using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerAuraValIsGreaterThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayerAuraValIsGreaterThan()
        {
            Tag = "Player Aura";
            Text = "Player Aura's value is greater than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!Player.auraComparison("Self", "Greater", instance.Value(Value1), int.Parse(instance.Value(Value2))))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player Aura's value is greater than: {Value1}, {Value2}";
        }

        #endregion
    }
}