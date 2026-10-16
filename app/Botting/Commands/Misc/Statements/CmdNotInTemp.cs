using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotInTemp : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdNotInTemp()
        {
            Tag = "Item";
            Text = "Is not in temp";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.TempInventory.ContainsItem(instance.Value(Value1), instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item is not in temp inventory: " + Value1 + ", " + Value2;
        }

        #endregion
    }
}