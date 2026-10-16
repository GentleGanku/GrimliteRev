using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdUseBoost : IBotCommand
    {
        public string ItemName { get; set; }

        public async Task Execute(IBotEngine instance)
        {
            InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(instance.Value(ItemName), StringComparison.OrdinalIgnoreCase) && i.Category == "ServerUse");
            if ((item == null) || Player.HasActiveBoost(item.Name))
            {
                return;
            }
            BotData.BotState = BotData.State.Transaction;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.EquipItem), null, 10, 500);
            Player.UseBoost(item.Id);
            await instance.WaitUntil(() => Player.HasActiveBoost(item.Name), null, 6, 500);
        }

        public override string ToString()
        {
            return ("Use boost: " + ItemName);
        }
    }
}