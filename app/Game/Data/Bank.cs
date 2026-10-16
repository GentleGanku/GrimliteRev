using Grimoire.Botting;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grimoire.Game.Data
{
    public class Bank
    {
        #region Fields

        public List<InventoryItem> SavedItems;

        #endregion

        #region Properties

        public List<InventoryItem> Items
        {
            get
            {
                return Flash.Call<List<InventoryItem>>("GetBankItems", new string[0]);
            }
        }

        public int AvailableSlots
        {
            get
            {
                return TotalSlots - UsedSlots;
            }
        }

        public int UsedSlots
        {
            get
            {
                return Flash.Call<int>("UsedBankSlots", new string[0]);
            }
        }

        public int TotalSlots
        {
            get
            {
                return Flash.Call<int>("BankSlots", new string[0]);
            }
        }

        public bool HasBankOpen
        {
            get
            {
                return Flash.Call<bool>("HasBankOpen", new bool[0]);
            }
        }

        public bool IsBankLoaded
        {
            get
            {
                return Flash.Call<bool>("IsBankLoaded", new string[0]);
            }
        }

        #endregion

        #region Methods

        public async Task TransferToBank(string itemName)
        {
            if (!Player.Bank.ContainsItem(itemName) && CanTransfer("Bank", itemName))
            {
                await BotUtilities.MoveOutOfCombat();
                Flash.Call("Transfer", "Bank", itemName);
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.Bank.ContainsItem(itemName), null, 6, 500);
            }
        }

        public async Task TransferFromBank(string itemName)
        {
            if (Player.Bank.ContainsItem(itemName) && CanTransfer("Inventory", itemName))
            {
                await BotUtilities.MoveOutOfCombat();
                Flash.Call("Transfer", "Inventory", itemName);
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !Player.Bank.ContainsItem(itemName), null, 6, 500);
            }
        }

        public async Task Swap(string invItemName, string bankItemName)
        {
            if (ContainsItem(bankItemName) && Player.Inventory.ContainsItem(invItemName, "*") && CanBankSwap(invItemName, bankItemName))
            {
                await BotUtilities.MoveOutOfCombat();
                Flash.Call("BankSwap", invItemName, bankItemName);
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !ContainsItem(bankItemName) && !Player.Inventory.ContainsItem(invItemName, "*"), null, 6, 500);
            }
        }

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

        public bool CanTransfer(string target, string itemName) => Flash.Call<bool>("CanTransfer", target, itemName);

        public bool CanBankSwap(string invItemName, string bankItemName) => Flash.Call<bool>("CanBankSwap", invItemName, bankItemName);

        #endregion
    }
}