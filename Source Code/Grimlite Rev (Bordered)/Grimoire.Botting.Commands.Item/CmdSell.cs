using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdSell : IBotCommand
    {
        public string ItemName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            var Value1 = ItemName;
            BotData.BotState = BotData.State.Transaction;

            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.SellItem));
            InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals((instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                Shop.SellItem(instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName);
                await instance.WaitUntil(() => !Player.Inventory.ContainsItem(item.Name, item.Quantity.ToString()));
            }
        }

        public override string ToString()
        {
            return "Sell: " + ItemName;
        }
    }
}