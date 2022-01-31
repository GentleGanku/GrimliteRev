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
            var Value1 = ItemName;
            InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals((instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), StringComparison.OrdinalIgnoreCase) && i.IsEquippable);
            if (item == null)
            {
                return;
            }
            while (instance.IsRunning && !IsEquipped(item.Id))
            {
                if (!Safe)
                {
                    if (item.Category == "Item")
                        Player.EquipPotion(item.Id, item.Description, item.File, item.Name);
                    else
                        Player.Equip(item.Id);
                    return;
                }

                BotData.BotState = BotData.State.Transaction;
                while (instance.IsRunning && Player.CurrentState == Player.State.InCombat)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(1000);
                }
                await instance.WaitUntil(() => World.IsActionAvailable(LockActions.EquipItem));
                if (item.Category == "Item")
                    Player.EquipPotion(item.Id, item.Description, item.File, item.Name);
                else
                    Player.Equip(item.Id);
            }
        }

        public bool IsEquipped(int ItemID)
        {
            return Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.IsEquipped && it.Id == ItemID) != null;
        }

        public override string ToString()
        {
            return (Safe ? "Safe" : "Unsafe") + " Equip: " + ItemName;
        }
    }
}