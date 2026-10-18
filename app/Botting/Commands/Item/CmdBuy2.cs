using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Utils;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuy2 : IBotCommand
    {
        #region Fields

        private string _itemName;

        #endregion

        #region Properties

        public string ShopId
        {
            get;
            set;
        }

        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                _itemName = value.UnifyQuotes();
            }
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
