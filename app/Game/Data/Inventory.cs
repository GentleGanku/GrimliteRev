using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Game.Data
{
    public class Inventory
    {
        #region Properties

        public List<InventoryItem> Items
        {
            get
            {
                return Flash.Call<List<InventoryItem>>("GetInventoryItems", new string[0]);
            }
        }

        public int MaxSlots
        {
            get
            {
                return Flash.Call<int>("InventorySlots", new string[0]);
            }
        }

        public int UsedSlots
        {
            get
            {
                return Flash.Call<int>("UsedInventorySlots", new string[0]);
            }
        }

        public int AvailableSlots
        {
            get
            {
                return MaxSlots - UsedSlots;
            }
        }

        public int UsedMiscSlots
        {
            get
            {
                return Flash.Call<int>("UsedMiscSlots", new string[0]);
            }
        }

        public int AvailableMiscSlots
        {
            get
            {
                return MaxSlots - UsedMiscSlots;
            }
        }

        #endregion

        #region Methods

        public bool ContainsItem(string name, string quantity = "*")
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

        #endregion
    }
}
