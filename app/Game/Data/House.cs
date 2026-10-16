using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Game.Data
{
    public class House
    {
        public List<InventoryItem> Items => Flash.Call<List<InventoryItem>>("GetHouseItems", new string[0]);

        public int TotalSlots => Flash.Call<int>("HouseSlots", new string[0]);

        public bool ContainsItem(string itemName, string quantity = "*")
        {
            InventoryItem inventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
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
    }
}