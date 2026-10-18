using Grimoire.Botting;
using Grimoire.Botting.Commands.Item;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Utils;
using System;
using System.Linq;
using System.Windows.Forms;
using static Grimoire.Botting.Commands.Item.CmdWhitelist;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        public void AddDrop(string Name, bool toConfig = false)
        {
            Name = Name.UnifyQuotes();
            if (lstDrops.Items.Cast<string>().ToList().Find(i => i.Equals(Name, StringComparison.OrdinalIgnoreCase)) == null)
            {
                lstDrops.Items.Add(Name);
                if (toConfig)
                {
                    BotManager.Instance.ActiveBotEngine.Configuration.Drops.Add(Name);
                }
            }
        }

        public void RemoveDrop(string Name, bool toConfig = false)
        {
            Name = Name.UnifyQuotes();
            string item = lstDrops.Items.Cast<string>().ToList().Find(i => i.Equals(Name, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                lstDrops.Items.Remove(item);
                if (toConfig)
                {
                    BotManager.Instance.ActiveBotEngine.Configuration.Drops.Remove(item);
                }
            }
        }

        public void AddItem(string Name)
        {
            Name = Name.UnifyQuotes();
            if (!lstItems.Items.Contains(Name))
            {
                lstItems.Items.Add(Name);
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            string itemName = txtItem.Text;
            if (itemName.Length > 0 && cbItemCmds.SelectedIndex > -1)
            {
                IBotCommand cmd;
                switch (cbItemCmds.SelectedIndex)
                {
                    case 0:
                        cmd = new CmdGetDrop
                        {
                            ItemName = itemName
                        };
                        break;

                    case 1:
                        cmd = new CmdSell
                        {
                            ItemName = itemName
                        };
                        break;

                    case 2:
                        cmd = new CmdUseBoost
                        {
                            ItemName = itemName,
                        };
                        break;

                    case 3:
                        cmd = new CmdEquip
                        {
                            ItemName = itemName,
                            Safe = true
                        };
                        break;

                    case 4:
                        cmd = new CmdEquip
                        {
                            ItemName = itemName,
                            Safe = false
                        };
                        break;

                    case 5:
                        cmd = new CmdBankTransfer
                        {
                            ItemName = itemName,
                            TransferFromBank = false
                        };
                        break;

                    case 6:
                        cmd = new CmdBankTransfer
                        {
                            ItemName = itemName,
                            TransferFromBank = true
                        };
                        break;

                    case 7:
                        cmd = new CmdWhitelist
                        {
                            Item = itemName,
                            State = state.Add
                        };
                        break;

                    case 8:
                        cmd = new CmdWhitelist
                        {
                            Item = itemName,
                            State = state.Remove
                        };
                        break;

                    default:
                        cmd = new CmdGetDrop
                        {
                            ItemName = itemName
                        };
                        break;
                }
                AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnMapItem_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdMapItem2
            {
                ItemId = ((int)numMapItem.Value).ToString()
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnBoth_Click(object sender, EventArgs e)
        {
            string itemName = txtWhitelist.Text;
            if (itemName.Length > 0)
            {
                AddDrop(itemName);
                AddItem(itemName);
            }
        }

        private void btnWhitelist_Click(object sender, EventArgs e)
        {
            string itemName = txtWhitelist.Text;
            if (itemName.Length > 0)
            {
                AddDrop(itemName);
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            string bankItem = txtSwapBank.Text;
            string inventoryItem = txtSwapInv.Text;
            if (bankItem.Length > 0 && inventoryItem.Length > 0)
            {
                AddCommand(new CmdBankSwap
                {
                    InventoryItemName = inventoryItem,
                    BankItemName = bankItem
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnBoost_Click(object sender, EventArgs e)
        {
            if (cbBoosts.SelectedIndex > -1)
            {
                lstBoosts.Items.Add(cbBoosts.SelectedItem);
            }
        }

        private void cbBoosts_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                if (cbBoosts.Items.Count > 0)
                {
                    cbBoosts.Items.Clear();
                }
                cbBoosts.Items.AddRange(Player.Inventory.Items.FindAll((InventoryItem i) => i.Category == "ServerUse").ToArray());
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (txtShopItem.TextLength > 0)
            {
                AddCommand(new CmdBuy2
                {
                    ItemName = txtShopItem.Text,
                    ShopId = ((int)numShopId.Value).ToString(),
                    Manual = chkBuyManual.Checked
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnBuyFast_Click(object sender, EventArgs e)
        {
            if (txtShopItem.TextLength > 0)
            {
                AddCommand(new CmdBuyFast
                {
                    ItemName = txtShopItem.Text,
                    Manual = chkBuyManual.Checked
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnLoadShop_Click(object sender, EventArgs e)
        {
            AddCommand(new Botting.Commands.Item.CmdLoad2
            {
                ShopId = ((int)numShopId.Value).ToString()
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnUnbanklst_Click(object sender, EventArgs e)
        {
            string itemName = txtWhitelist.Text;
            if (itemName.Length > 0)
            {
                AddItem(itemName);
            }
        }

        private void numDropDelay_ValueChanged(object sender, EventArgs e)
        {
            Bot.Instance.DropDelay = (int)numDropDelay.Value;
        }

        private void btnWhitelistOn_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWhitelist
            {
                State = CmdWhitelist.state.On
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWhitelistOff_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWhitelist
            {
                State = CmdWhitelist.state.Off
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWhitelistClear_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWhitelist
            {
                State = CmdWhitelist.state.Clear
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnBuyItemByID_Click(object sender, EventArgs e)
        {
            if ((txtItemID.TextLength > 0) && (txtShopID.TextLength > 0) && (txtShopItemID.TextLength > 0))
            {
                AddCommand(new CmdBuyByID
                {
                    ItemID = txtItemID.Text,
                    ShopID = txtShopID.Text,
                    ShopItemID = txtShopItemID.Text,
                    Manual = chkBuyManual.Checked
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnBuyFastByID_Click(object sender, EventArgs e)
        {
            if ((txtItemID.TextLength > 0) && (txtShopID.TextLength > 0) && (txtShopItemID.TextLength > 0))
            {
                AddCommand(new CmdBuyFastByID
                {
                    ItemID = txtItemID.Text,
                    ShopID = txtShopID.Text,
                    ShopItemID = txtShopItemID.Text,
                    Manual = chkBuyManual.Checked
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        #endregion
    }
}
