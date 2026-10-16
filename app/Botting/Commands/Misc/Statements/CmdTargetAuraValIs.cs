using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetAuraValIs : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdTargetAuraValIs()
        {
            Tag = "Target Aura";
            Text = "Target Aura's value is";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!Player.auraComparison("Target", "Equal", instance.Value(Value1), int.Parse(instance.Value(Value2))))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target Aura's value is: {Value1}, {Value2}";
        }

        #endregion
    }
}