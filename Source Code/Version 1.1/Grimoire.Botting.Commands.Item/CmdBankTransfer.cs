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
            var Value1 = ItemName;
            BotData.BotState = BotData.State.Others;
            if (TransferFromBank)
            {
                if (Player.Bank.ContainsItem(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
                {
                    Player.Bank.TransferFromBank(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1);
                    await instance.WaitUntil(() => !Player.Bank.ContainsItem(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1));
                }
            }
            else if (Player.Inventory.ContainsItem((instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), "*"))
            {
                Player.Bank.TransferToBank(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1);
                await instance.WaitUntil(() => !Player.Inventory.ContainsItem((instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), "*"));
            }
        }

        public override string ToString()
        {
            return (TransferFromBank ? "Bank -> Inv: " : "Inv -> Bank: ") + ItemName;
        }
    }
}