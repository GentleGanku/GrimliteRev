using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdEquipped : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdEquipped()
        {
            Tag = "Item";
            Text = "Is equipped";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!(Player.Inventory.Items.Find((InventoryItem x) => x.Name.Equals(instance.Value(Value1), StringComparison.OrdinalIgnoreCase)) ?? new InventoryItem()).IsEquipped)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item is equipped: " + Value1;
        }

        #endregion
    }
}