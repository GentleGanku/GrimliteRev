using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdLoad : IBotCommand
    {
        public int ShopId
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.LoadShop));
            Shop.ResetShopInfo();
            Shop.Load(ShopId);
            await instance.WaitUntil(() => Shop.IsShopLoaded);
        }

        public override string ToString()
        {
            return "Load Shop: " + ShopId;
        }
    }
}