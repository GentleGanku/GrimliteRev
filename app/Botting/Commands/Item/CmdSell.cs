using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Utils;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdSell : IBotCommand
    {
        #region Fields

        private string _itemName;

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await Shop.SellItem(instance.Value(ItemName));
        }

        public override string ToString()
        {
            return "Sell: " + ItemName;
        }

        #endregion
    }
}