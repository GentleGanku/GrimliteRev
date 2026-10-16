using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdLoad : IBotCommand
    {
        #region Properties

        public int ShopId
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await Shop.Load(ShopId);
        }

        public override string ToString()
        {
            return "Load Shop: " + ShopId;
        }

        #endregion
    }
}