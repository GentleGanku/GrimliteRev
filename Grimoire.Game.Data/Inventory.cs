using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Game.Data
{
    public class Inventory
    {
        public List<InventoryItem> Items => Flash.Call<List<InventoryItem>>("GetInventoryItems", new string[0]);

        public int MaxSlots => Flash.Call<int>("InventorySlots", new string[0]);

        public int UsedSlots => Flash.Call<int>("UsedInventorySlots", new string[0]);

        public int AvailableSlots => MaxSlots - UsedSlots;

        public bool ContainsItem(string name, string quantity)
        {
            InventoryItem inventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (inventoryItem != null)
            {
                if (!(quantity == "*"))
                {
                    return inventoryItem.Quantity >= int.Parse(quantity);
                }
                return true;
            }
            return false;
        }

        public bool ContainsMaxItem(string name)
        {
            InventoryItem inventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (inventoryItem != null)
            {
                return inventoryItem.Quantity >= inventoryItem.MaxStack;
            }
            return false;
        }
    }
}
