using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Game;
using Grimoire;
using Grimoire.Game.Data;
using DarkUI.Forms;

namespace Grimoire.UI
{
    public partial class BankForm : DarkForm
    {
        public BankForm()
        {
            InitializeComponent();
        }

        private void button2_ClickAsync(object sender, EventArgs e)
        {
            List<InventoryItem> inventory = Player.Inventory.Items;

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select AC or Non-AC", "Error!", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            foreach (InventoryItem n in inventory)
            {
                if (n.IsAcItem && comboBox2.SelectedIndex == 0 && !n.IsEquipped)
                {
                    Player.Bank.TransferToBank(n.Name);
                    Task.Delay(70);
                }
                else if (Player.Bank.AvailableSlots > 0 && !n.IsAcItem && comboBox2.SelectedIndex == 1 && !n.IsEquipped)
                {
                    Player.Bank.TransferToBank(n.Name);
                    Task.Delay(70);
                }
            }
            return;
        }

        private void button1_ClickAsync(object sender, EventArgs e)
        {
            List<InventoryItem> inventory = Player.Inventory.Items;
            string[] wep = new string[9]
            {
                "Sword",
                "Axe",
                "Dagger",
                "Gun",
                "Bow",
                "Mace",
                "Polearm",
                "Staff",
                "Wand",
            };
            object box1 = comboBox1.SelectedItem;
            object box2 = comboBox2.SelectedItem;
            bool isAC = false;
            if (box1 == null)
            {
                MessageBox.Show("Please select Item type", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (box2 == null)
            {
                MessageBox.Show("Please select AC or Non-AC", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox2.SelectedIndex == 0)
            {
                isAC = true;
            }
            foreach (InventoryItem n in inventory)
            {
                bool flag = n.IsAcItem == isAC && !n.IsEquipped && n.Name.ToLower() != "treasure potion";

                if (checkBox1.Checked)
                {
                    if (box1.ToString() == "Weapons" && !wep.Contains(n.Category) && flag)
                    {
                        Player.Bank.TransferToBank(n.Name);
                        Task.Delay(70);
                    }
                    else if (n.Category != box1.ToString() && flag)
                    {
                        Player.Bank.TransferToBank(n.Name);
                        Task.Delay(70);
                    }
                }
                else
                {
                    if (box1.ToString() == "Weapons" && wep.Contains(n.Category) && flag)
                    {
                        Player.Bank.TransferToBank(n.Name);
                        Task.Delay(70);
                    }
                    else if (n.Category == box1.ToString() && flag)
                    {
                        Player.Bank.TransferToBank(n.Name);
                        Task.Delay(70);
                    }
                }
            }
            return;
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
