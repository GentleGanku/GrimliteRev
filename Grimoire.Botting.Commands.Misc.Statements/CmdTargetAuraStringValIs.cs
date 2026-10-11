using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetAuraStringValIs : StatementCommand, IBotCommand
    {
        public CmdTargetAuraStringValIs()
        {
            Tag = "Target Aura";
            Text = "Target Aura's value is (string)";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.isAuraWithStrValActive("Target", (instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), (instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target Aura's value is (string): {Value1}, {Value2}";
        }
    }
}