using Grimoire.Tools;
using Grimoire.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Game.Data
{
    public class House
    {
        #region Properties

        public List<InventoryItem> Items
        {
            get
            {
                return Flash.Call<List<InventoryItem>>("GetHouseItems", new string[0]);
            }
        }

        public int TotalSlots
        {
            get
            {
                return Flash.Call<int>("HouseSlots", new string[0]);
            }
        }

        #endregion

        #region Methods

        public bool ContainsItem(string itemName, string quantity = "*")
        {
            InventoryItem inventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(itemName.UnifyQuotes(), StringComparison.OrdinalIgnoreCase));
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

        #endregion
    }
}