using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIsMaxStack : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdIsMaxStack()
        {
            Tag = "Item";
            Text = "Is maxed in inventory";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!Player.Inventory.ContainsMaxItem(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Item is maxed in inventory: {Value1}";
        }

        #endregion
    }
}
