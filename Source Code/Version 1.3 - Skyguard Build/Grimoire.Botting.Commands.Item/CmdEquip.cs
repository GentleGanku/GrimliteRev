using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdEquip : IBotCommand
    {
        public string ItemName
        {
            get;
            set;
        }

        public bool Safe
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals((instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName), StringComparison.OrdinalIgnoreCase) && i.IsEquippable);
            if (item == null)
            {
                return;
            }
            BotData.BotState = BotData.State.Transaction;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.EquipItem));
            if (!IsEquipped(item.Id))
            {
                if (Safe)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(2000);
                }
                if (item.Category == "Item")
                {
                    Player.EquipPotion(item.Id, item.Description, item.File, item.Name);
                }
                else
                {
                    Player.Equip(item.Id);
                }
            }
        }

        public bool IsEquipped(int ItemID)
        {
            return Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.IsEquipped && it.Id == ItemID) != null;
        }

        public override string ToString()
        {
            return (Safe ? "Safe " : null) + "Equip: " + ItemName;
        }
    }
}