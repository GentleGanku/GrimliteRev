using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetAuraValIsNot : StatementCommand, IBotCommand
    {
        public CmdTargetAuraValIsNot()
        {
            Tag = "Target Aura";
            Text = "Target Aura's value is not";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.auraComparison("Target", "Equal", (instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), int.Parse(instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target Aura's value is not: {Value1}, {Value2}";
        }
    }
}