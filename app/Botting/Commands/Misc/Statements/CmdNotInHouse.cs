using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotInHouse : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdNotInHouse()
        {
            Tag = "Item";
            Text = "Is not in house";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.House.ContainsItem(instance.Value(Value1), instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item is not in house: " + Value1 + ", " + Value2;
        }

        #endregion
    }
}