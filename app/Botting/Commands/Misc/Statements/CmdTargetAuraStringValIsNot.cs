using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetAuraStringValIsNot : StatementCommand, IBotCommand
    {
        public CmdTargetAuraStringValIsNot()
        {
            Tag = "Target Aura";
            Text = "Target Aura's value is not (string)";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.isAuraWithStrValActive("Target", instance.Value(Value1), instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target Aura's value is not (string): {Value1}, {Value2}";
        }
    }
}