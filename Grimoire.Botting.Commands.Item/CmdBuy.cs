using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuy : IBotCommand
    {
        public int ShopId
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem));
            Shop.ResetShopInfo();
            Shop.Load(ShopId);
            await instance.WaitUntil(() => Shop.IsShopLoaded);
            InventoryItem i = Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals((instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName), StringComparison.OrdinalIgnoreCase));
            if (i != null)
            {
                Shop.BuyItem(instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName);
                await instance.WaitUntil(() => Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals((instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName), StringComparison.OrdinalIgnoreCase)).Quantity != i.Quantity);
            }
            else
            {
                Shop.BuyItem(instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName);
                await instance.WaitUntil(() => Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals((instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName), StringComparison.OrdinalIgnoreCase)) != null);
            }
        }

        public override string ToString()
        {
            return $"Buy item: {ItemName}";
        }
    }

    public class CmdBuy2 : IBotCommand
    {
        public string ShopId
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem));
            Shop.ResetShopInfo();
            Shop.Load(int.Parse(instance.IsVar(ShopId) ? Configuration.Tempvariable[instance.GetVar(ShopId)] : ShopId));
            await instance.WaitUntil(() => Shop.IsShopLoaded);
            InventoryItem i = Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals((instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName), StringComparison.OrdinalIgnoreCase));
            if (i != null)
            {
                Shop.BuyItem(instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName);
                await instance.WaitUntil(() => Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals((instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName), StringComparison.OrdinalIgnoreCase)).Quantity != i.Quantity);
            }
            else
            {
                Shop.BuyItem(instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName);
                await instance.WaitUntil(() => Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals((instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName), StringComparison.OrdinalIgnoreCase)) != null);
            }
        }

        public override string ToString()
        {
            return $"Buy item: {ItemName}";
        }
    }
}