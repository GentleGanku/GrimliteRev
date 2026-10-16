using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdHealthPercentageLessThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdHealthPercentageLessThan()
        {
            Tag = "This player";
            Text = "Health percentage is less than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if ((Player.Health / (double)Player.HealthMax * 100.0) >= int.Parse(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Health percentage is less than: " + Value1;
        }

        #endregion
    }
}