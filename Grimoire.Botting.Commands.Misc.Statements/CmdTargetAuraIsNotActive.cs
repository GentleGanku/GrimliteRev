using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetAuraIsNotActive : StatementCommand, IBotCommand
    {
        public CmdTargetAuraIsNotActive()
        {
            Tag = "Target Aura";
            Text = "Target Aura is not active";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.isAuraActive("Target", instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target Aura is not active: {Value1}";
        }
    }
}