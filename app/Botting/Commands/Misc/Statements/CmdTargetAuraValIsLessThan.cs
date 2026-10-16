using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetAuraValIsLessThan : StatementCommand, IBotCommand
    {
        public CmdTargetAuraValIsLessThan()
        {
            Tag = "Target Aura";
            Text = "Target Aura's value is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.auraComparison("Target", "Less", instance.Value(Value1), int.Parse(instance.Value(Value2))))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target Aura's value is less than: {Value1}, {Value2}";
        }
    }
}