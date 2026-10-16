using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdLoad2 : IBotCommand
    {
        #region Properties

        public string ShopId
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await Shop.Load(int.Parse(instance.Value(ShopId)));
        }

        public override string ToString()
        {
            return "Load Shop: " + ShopId;
        }

        #endregion
    }
}
