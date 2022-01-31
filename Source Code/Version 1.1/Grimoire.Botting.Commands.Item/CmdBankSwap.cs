using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBankSwap : IBotCommand
    {
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

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            if (CanExecute())
            {
                Player.Bank.Swap(InventoryItemName, BankItemName);
                await instance.WaitUntil(() => !CanExecute());
            }
            bool CanExecute()
            {
                if (Player.Bank.ContainsItem(BankItemName))
                {
                    return Player.Inventory.ContainsItem(InventoryItemName, "*");
                }
                return false;
            }
        }

        public override string ToString()
        {
            return "Bank swap {" + BankItemName + ", " + InventoryItemName + "}";
        }
    }
}