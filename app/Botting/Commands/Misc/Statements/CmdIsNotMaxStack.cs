using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIsNotMaxStack : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdIsNotMaxStack()
        {
            Tag = "Item";
            Text = "Is not maxed in inventory";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.Inventory.ContainsMaxItem(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Item is not maxed in inventory: {Value1}";
        }

        #endregion
    }
}
