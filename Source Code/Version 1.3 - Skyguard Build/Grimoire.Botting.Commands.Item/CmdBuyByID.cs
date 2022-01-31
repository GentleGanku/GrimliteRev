using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuyByID : IBotCommand
    {
        public string ItemID
        {
            get;
            set;
        }

        public string ShopID
        {
            get;
            set;
        }

        public string ShopItemID
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem));
            Shop.ResetShopInfo();
            Shop.Load(int.Parse(instance.IsVar(ShopID) ? Configuration.Tempvariable[instance.GetVar(ShopID)] : ShopID));
            await instance.WaitUntil(() => Shop.IsShopLoaded);
            Shop.BuyItemById(int.Parse(instance.IsVar(ItemID) ? Configuration.Tempvariable[instance.GetVar(ItemID)] : ItemID), int.Parse(instance.IsVar(ShopID) ? Configuration.Tempvariable[instance.GetVar(ShopID)] : ShopID), int.Parse(instance.IsVar(ShopItemID) ? Configuration.Tempvariable[instance.GetVar(ShopItemID)] : ShopItemID));
        }

        public override string ToString()
        {
            return $"Buy item by ID: {ItemID}, {ShopID}, {ShopItemID}";
        }
    }
}
