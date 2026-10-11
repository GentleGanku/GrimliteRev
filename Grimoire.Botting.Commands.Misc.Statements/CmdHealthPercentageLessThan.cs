using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdHealthPercentageLessThan : StatementCommand, IBotCommand
    {
        public CmdHealthPercentageLessThan()
        {
            Tag = "This player";
            Text = "Health percentage is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if ((Player.Health / (double)Player.HealthMax * 100.0) >= int.Parse(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Health percentage is less than: " + Value1;
        }
    }
}