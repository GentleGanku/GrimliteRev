using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuyByID : IBotCommand
    {
        #region Properties

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

        public bool Manual
        {
            get;
            set;
        } = false;

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await Shop.Load(int.Parse(instance.Value(ShopID)));
            await Shop.BuyItemById(int.Parse(instance.Value(ItemID)), int.Parse(instance.Value(ShopID)), int.Parse(instance.Value(ShopItemID)), Manual);
        }

        public override string ToString()
        {
            return $"Buy item by ID: {ItemID}, {ShopID}, {ShopItemID}";
        }

        #endregion
    }
}
