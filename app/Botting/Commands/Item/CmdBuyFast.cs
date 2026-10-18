using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Utils;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuyFast : IBotCommand
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
            await Shop.BuyItem(instance.Value(ItemName), Manual);
        }

        public override string ToString()
        {
            return "Buy item fast: " + ItemName;
        }

        #endregion
    }
}