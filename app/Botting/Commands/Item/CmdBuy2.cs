using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
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
