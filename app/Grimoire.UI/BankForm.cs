using DarkUI.Forms;
using DarkUI.Controls;
using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BankForm : DarkForm
    {
        public BankForm()
        {
            InitializeComponent();
        }

        private string categoryName(string category)
        {
            string categoryName = "";
            switch (category)
            {
                case "Ground":
                    categoryName = "Misc";
                    break;
                case "Misc. Item":
                    categoryName = "Item";
                    break;
                case "Boost Item":
                    categoryName = "ServerUse";
                    break;
                default:
                    categoryName = category;
                    break;
            }
            return categoryName;
        }

        private bool shouldBank(InventoryItem item, bool IsAcTagged, string itemType)
        {
            bool boolean = false;
            switch (checkBox1.Checked)
            {
                case true:
                    if (itemType == "Weapons (all weapon types)")
                    {
                        boolean = item.IsAcItem != IsAcTagged && !item.IsWeapon;
                    }
                    else
                    {
                        boolean = item.IsAcItem != IsAcTagged && item.Category != itemType;
                    }
                    break;
                case false:
                    if (itemType == "Weapons (all weapon types)")
                    {
                        boolean = item.IsAcItem == IsAcTagged && item.IsWeapon;
                    }
                    else
                    {
                        boolean = item.IsAcItem == IsAcTagged && item.Category == itemType;
                    }
                    break;
            }
            return boolean;
        }

        private bool canBeBanked(InventoryItem item)
        {
            return (!item.Name.Equals("Treasure Potion") && !item.IsEquipped);
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn)
            {
                return;
            }
            if ((comboBox1.SelectedIndex == -1) || (comboBox2.SelectedIndex == -1))
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, (comboBox1.SelectedIndex == -1) ? "Please select the item category." : "Please select the Coin Tag (AdventureCoin or Non-AdventureCoin).", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool isAcTagged = comboBox2.SelectedIndex == 0;
            string itemCategory = categoryName(comboBox1.SelectedItem.ToString());
            List<InventoryItem> items = Player.Inventory.Items.FindAll(item => shouldBank(item, isAcTagged, itemCategory) && canBeBanked(item));
            foreach (InventoryItem item in items)
            {
                if (item.IsAcItem || (Player.Bank.AvailableSlots > 0))
                {
                    Player.Bank.TransferToBank(item.Name);
                    Task.Delay(80);
                }
            }
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn)
            {
                return;
            }
            if (comboBox2.SelectedIndex == -1)
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Please select the Coin Tag (AdventureCoin or Non-AdventureCoin).", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool isAcTagged = comboBox2.SelectedIndex == 0;
            List<InventoryItem> items = Player.Inventory.Items.FindAll(item => canBeBanked(item) && item.IsAcItem == isAcTagged);
            foreach (InventoryItem item in items)
            {
                if (item.IsAcItem || (Player.Bank.AvailableSlots > 0))
                {
                    Player.Bank.TransferToBank(item.Name);
                    Task.Delay(80);
                }
            }
        }

        private void BankForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
