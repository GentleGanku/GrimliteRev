using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdSell : IBotCommand
    {
        #region Properties

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
            await Shop.SellItem(instance.Value(ItemName));
        }

        public override string ToString()
        {
            return "Sell: " + ItemName;
        }

        #endregion
    }
}