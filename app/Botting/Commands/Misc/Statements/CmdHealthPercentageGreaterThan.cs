using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdHealthPercentageGreaterThan : StatementCommand, IBotCommand
    {
        public CmdHealthPercentageGreaterThan()
        {
            Tag = "This player";
            Text = "Health percentage is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if ((Player.Health / (double)Player.HealthMax * 100.0) <= int.Parse(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Health percentage is greater than: " + Value1;
        }
    }
}