using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuy : IBotCommand
    {
        #region Properties

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

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem), null, 10, 500);
            await BotUtilities.MoveOutOfCombat();
            Shop.ResetShopInfo();
            Shop.Load(ShopId);
            await instance.WaitUntil(() => Shop.IsShopLoaded);
            await Shop.BuyItem(instance.Value(ItemName));
        }

        public override string ToString()
        {
            return $"Buy item: {ItemName}";
        }

        #endregion
    }

    public class CmdBuy2 : IBotCommand
    {
        #region Properties

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
            await Shop.Load(int.Parse(instance.Value(ShopId)));
            await Shop.BuyItem(instance.Value(ItemName), Manual);
        }

        public override string ToString()
        {
            return "Buy item: " + ItemName;
        }

        #endregion
    }
}