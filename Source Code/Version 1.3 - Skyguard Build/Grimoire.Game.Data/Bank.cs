using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Game.Data
{
    public class Bank
    {
        public List<InventoryItem> Items => Flash.Call<List<InventoryItem>>("GetBankItems", new string[0]);

        public List<InventoryItem> SavedItems;

        public int AvailableSlots => TotalSlots - UsedSlots;

        public int UsedSlots => Flash.Call<int>("UsedBankSlots", new string[0]);

        public int TotalSlots => Flash.Call<int>("BankSlots", new string[0]);

        public void TransferToBank(string itemName) => Flash.Call("Transfer", new string[2] { "Bank", itemName });

        public void TransferFromBank(string itemName) => Flash.Call("Transfer", new string[2] { "Inventory", itemName });

        public void Swap(string invItemName, string bankItemName) => Flash.Call("BankSwap", new string[2] { invItemName, bankItemName });

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

        public void Show()
        {
            Flash.Call("ShowBank", new string[0]);
        }

        public void Load()
        {
            Flash.Call("LoadBank", new string[0]);
        }

        public void LoadItems()
        {
            Flash.Call("LoadBankItems", new string[0]);
        }

        public bool HasBankOpen => Flash.Call<bool>("HasBankOpen", new bool[0]);

        public bool IsBankLoaded => Flash.Call<bool>("IsBankLoaded", new string[0]);

    }
}