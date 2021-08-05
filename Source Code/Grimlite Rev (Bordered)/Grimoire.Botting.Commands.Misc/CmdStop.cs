using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire;
using Grimoire.Game;
using Grimoire.UI;
using Grimoire.Game.Data;
namespace Grimoire.Botting.Commands.Misc
{
    public class CmdStop : IBotCommand
    {
        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Instance.BankOnStop)
            {
                foreach (InventoryItem item in Player.Inventory.Items)
                {
                    if (!item.IsEquipped && item.IsAcItem && item.Category != "Class" && item.Name.ToLower() != "treasure potion" && Configuration.Instance.Items.Contains(item.Name))
                    {
                        Player.Bank.TransferToBank(item.Name);
                        Task.Delay(70);
                        LogForm.Instance.AppendDebug("Transferred to Bank: " + item.Name + "\r\n");
                    }
                }
                LogForm.Instance.AppendDebug("Banked all AC Items in Items list \r\n");
            }
            Task.Delay(2000);
            instance.Stop();
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Stop bot";
        }
    }
}