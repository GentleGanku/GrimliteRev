using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdInInventory : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdInInventory()
        {
            Tag = "Item";
            Text = "Is in inventory";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!Player.Inventory.ContainsItem(instance.Value(Value1), instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Item is in inventory: {Value1}, {Value2}";
        }

        #endregion
    }
}