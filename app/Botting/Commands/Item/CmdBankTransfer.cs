using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBankTransfer : IBotCommand
    {
        public bool TransferFromBank
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            if (TransferFromBank)
            {
                await Player.Bank.TransferFromBank(instance.Value(ItemName));
            }
            else
            {
                await Player.Bank.TransferToBank(instance.Value(ItemName));
            }
        }

        public override string ToString()
        {
            return (TransferFromBank ? "Bank -> Inventory: " : "Inventory -> Bank: ") + ItemName;
        }
    }
}