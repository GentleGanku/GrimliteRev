using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdGoldLessThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdGoldLessThan()
        {
            Tag = "This player";
            Text = "Gold is less than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.Gold >= int.Parse(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Gold is less than: " + Value1;
        }

        #endregion
    }
}