using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerAuraValIsLessThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayerAuraValIsLessThan()
        {
            Tag = "Player Aura";
            Text = "Player Aura's value is less than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!Player.auraComparison("Self", "Less", instance.Value(Value1), int.Parse(instance.Value(Value2))))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player Aura's value is less than: {Value1}, {Value2}";
        }

        #endregion
    }
}