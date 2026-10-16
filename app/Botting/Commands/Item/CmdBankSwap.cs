using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBankSwap : IBotCommand
    {
        #region Properties

        public string BankItemName
        {
            get;
            set;
        }

        public string InventoryItemName
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            Player.Bank.Swap(instance.Value(InventoryItemName), instance.Value(BankItemName));
        }

        public override string ToString()
        {
            return "Bank <-> Inventory: " + BankItemName + "<->" + InventoryItemName;
        }

        #endregion
    }
}