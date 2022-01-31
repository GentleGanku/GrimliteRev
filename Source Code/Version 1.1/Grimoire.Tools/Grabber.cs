using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Grimoire.Botting;
//using BrowserForm = Grimoire.UI.BrowserForm;
using System.Diagnostics;
using System.Dynamic;

namespace Grimoire.Tools
{
    public static class Grabber
    {
        public static void GrabQuests(TreeView tree)
        {
            List<Quest> list = Player.Quests.QuestTree?.OrderBy((Quest q) => q.Id).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (Quest item in list)
                {
                    TreeNode treeNode = tree.Nodes.Add($"{item.Id} - {item.Name}");
                    treeNode.Nodes.Add($"ID: {item.Id}");
                    treeNode.Nodes.Add($"Description: {item.Description}");
                    treeNode.ContextMenuStrip = MenuQuest(item.Id);
                    List<InventoryItem> requiredItems = item.RequiredItems;
                    if (requiredItems != null && requiredItems.Count > 0)
                    {
                        TreeNode treeNode2 = treeNode.Nodes.Add("Required items");
                        treeNode2.ContextMenuStrip = MenuItems(item.RequiredItems);
                        foreach (InventoryItem requiredItem in item.RequiredItems)
                        {
                            TreeNode treeNode3 = treeNode2.Nodes.Add(requiredItem.Name);
                            treeNode3.ContextMenuStrip = MenuItem(requiredItem);
                            treeNode3.Nodes.Add($"ID: {requiredItem.Id}");
                            treeNode3.Nodes.Add($"Quantity: {requiredItem.Quantity}");
                            treeNode3.Nodes.Add("Temporary: " + (requiredItem.IsTemporary ? "Yes" : "No"));
                            treeNode3.Nodes.Add($"Description: {requiredItem.Description}");
                        }
                    }
                    List<InventoryItem> rewards = item.Rewards;
                    if (rewards != null && rewards.Count > 0)
                    {
                        TreeNode treeNode4 = treeNode.Nodes.Add("Rewards");
                        treeNode4.ContextMenuStrip = MenuItems(item.Rewards);
                        foreach (InventoryItem reward in item.Rewards)
                        {
                            TreeNode treeNode5 = treeNode4.Nodes.Add(reward.Name);
                            treeNode5.ContextMenuStrip = MenuItem(reward);
                            treeNode5.Nodes.Add($"ID: {reward.Id}");
                            treeNode5.Nodes.Add($"Quantity: {reward.Quantity}");
                            treeNode5.Nodes.Add(string.Concat($"Drop chance: ", reward.DropChance.Contains("100") ? "Guaranteed" : reward.DropChance + "%"));
                            ItemBase reward2 = item.oRewards.Find(x => x.Name == reward.Name);
                            treeNode5.Nodes.Add($"Category: {reward2.Category}");
                            treeNode5.Nodes.Add($"Description: {reward2.Description}");
                            if (!string.IsNullOrEmpty(reward2.File))
                            {
                                treeNode5.ContextMenuStrip = MenuItem(reward2);
                                treeNode5.Nodes.Add($"sFile: {reward2.File}");
                                treeNode5.Nodes.Add($"sLink: {reward2.Link}");
                            }
                        }
                    }
                }
            }
        }

        public static void GrabShopItems(TreeView tree)
        {
            List<ShopInfo> list = World.LoadedShops?.OrderBy((ShopInfo s) => s.Name).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (ShopInfo item in list)
                {
                    TreeNode treeNode = tree.Nodes.Add(item.Name);
                    treeNode.ContextMenuStrip = Wiki(item);
                    treeNode.Nodes.Add($"ID: {item.Id}");
                    treeNode.Nodes.Add($"Location: {item.Location}");
                    List<InventoryItem> items = item.Items;
                    if (items != null && items.Count > 0)
                    {
                        TreeNode treeNode2 = treeNode.Nodes.Add("Items");
                        foreach (InventoryItem item2 in item.Items)
                        {
                            TreeNode treeNode3 = treeNode2.Nodes.Add(item2.Name);
                            treeNode3.ContextMenuStrip = Wiki(item2);
                            treeNode3.Nodes.Add($"Shop item ID: {item2.ShopItemId}");
                            treeNode3.Nodes.Add($"ID: {item2.Id}");
                            treeNode3.Nodes.Add(string.Format("Cost: {0} {1}", item2.Cost, item2.IsAcItem ? "AC" : "Gold"));
                            treeNode3.Nodes.Add($"Category: {item2.Category}");
                            treeNode3.Nodes.Add($"Description: {item2.Description}");
                            if(item2.IsEquippableNonItem || item2.IsWeapon)
                            {
                                treeNode3.Nodes.Add($"sFile: {item2.File}");
                                treeNode3.Nodes.Add($"sLink: {item2.Link}");
                            }
                        }
                    }
                }
            }
        }

        public static void GrabQuestIds(TreeView tree)
        {
            List<Quest> list = Player.Quests.QuestTree?.OrderBy((Quest q) => q.Id).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (Quest item in list)
                {
                    tree.Nodes.Add($"{item.Id} - {item.Name}").ContextMenuStrip = MenuQuest(item.Id);
                }
            }
        }

        public static void GrabInventoryItems(TreeView tree)
        {
            GrabItems(tree, inventory: true);
        }

        public static void GrabBankItems(TreeView tree)
        {
            GrabItems(tree, inventory: false);
        }

        private static void GrabItems(TreeView tree, bool inventory)
        {
            List<InventoryItem> list = (inventory ? Player.Inventory.Items : Player.Bank.Items)?.OrderBy((InventoryItem i) => i.Name).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (InventoryItem item in list)
                {
                    TreeNode treeNode = tree.Nodes.Add(item.Name);
                    treeNode.ContextMenuStrip = Wiki(item);
                    treeNode.Nodes.Add($"ID: {item.Id}");
                    treeNode.Nodes.Add($"Char item id: {item.CharItemId}");
                    treeNode.Nodes.Add($"Quantity: {item.Quantity}/{item.MaxStack}");
                    treeNode.Nodes.Add($"AC tagged: {item.IsAcItem}");
                    treeNode.Nodes.Add($"Category: {item.Category}");
                    treeNode.Nodes.Add($"Description: {item.Description}");
                    if (item.IsEquippableNonItem || item.IsWeapon)
                    {
                        treeNode.Nodes.Add($"sFile: {item.File}");
                        treeNode.Nodes.Add($"sLink: {item.Link}");
                    }
                }
            }
        }

        public static void GrabTempItems(TreeView tree)
        {
            List<TempItem> list = Player.TempInventory.Items?.OrderBy((TempItem i) => i.Name).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (TempItem item in list)
                {
                    TreeNode treeNode = tree.Nodes.Add(item.Name);
                    treeNode.ContextMenuStrip = Wiki(item.Name);
                    treeNode.Nodes.Add($"ID: {item.Id}");
                    treeNode.Nodes.Add($"Quantity: {item.Quantity}");
                }
            }
        }

        public static void GrabMonsters(TreeView tree)
        {
            List<Monster> list = (from x in World.AvailableMonsters?.GroupBy((Monster m) => m.Name)
                                  select x.First()).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (Monster item in list)
                {
                    TreeNode treeNode = tree.Nodes.Add(item.Name);
                    treeNode.ContextMenuStrip = Wiki(item.Name);
                    treeNode.Nodes.Add($"ID: {item.Id}");
                    treeNode.Nodes.Add($"Race: {item.Race}");
                    treeNode.Nodes.Add($"Level: {item.Level}");
                    treeNode.Nodes.Add($"Health: {item.Health}/{item.MaxHealth}");
                }
            }
        }

        private static ContextMenuStrip Wiki(string item)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Go to Wikipage"
            };
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem
            {
                Text = "Search on Wiki"
            };
            toolStripMenuItem.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start
                Search("https://aqwwiki.wikidot.com/" + item.Replace(" ", "+"));
            };
            toolStripMenuItem1.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start
                Search("https://aqwwiki.wikidot.com/search:site/q/" + item.Replace(" ", "+"));
            };
            contextMenuStrip.Items.Add(toolStripMenuItem);
            contextMenuStrip.Items.Add(toolStripMenuItem1);
            return contextMenuStrip;
        }

        private static ContextMenuStrip Wiki(ShopInfo item)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Go to Wikipage"
            };
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem
            {
                Text = "Search on Wiki"
            };
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
            {
                Text = "Load Shop"
            };
            toolStripMenuItem.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start
                Search("https://aqwwiki.wikidot.com/" + item.Name.Replace(" ", "+"));
            };
            toolStripMenuItem1.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start
                Search("https://aqwwiki.wikidot.com/search:site/q/" + item.Name.Replace(" ", "+"));
            };
            toolStripMenuItem2.Click += delegate (object S, EventArgs E)
            {
                Shop.Load(item.Id);
            };
            contextMenuStrip.Items.Add(toolStripMenuItem);
            contextMenuStrip.Items.Add(toolStripMenuItem1);
            contextMenuStrip.Items.Add(toolStripMenuItem2);
            return contextMenuStrip;
        }

        private static ContextMenuStrip Wiki(InventoryItem Item)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Go to Wikipage"
            };
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem
            {
                Text = "Search on Wiki"
            };
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
            {
                Text = "Equip SWF"
            };
            toolStripMenuItem.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start
                Search("https://aqwwiki.wikidot.com/" + Item.Name.Replace(" ", "+"));
            };
            toolStripMenuItem1.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start
                Search("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
            };
            toolStripMenuItem2.Click += delegate
            {
                string txt = Item.Category;
                string slot;
                if (txt == "Cape")
                    slot = "ba";
                else if (txt == "Pet")
                    slot = "pe";
                else if (txt == "Armor" || txt == "Class")
                    slot = "co";
                else if (txt == "Helm")
                    slot = "he";
                else
                    slot = "Weapon";
                dynamic equip = new ExpandoObject();
                equip.sFile = Item.File;
                equip.sLink = Item.Link;
                equip.sType = txt;
                Flash.Call("SetEquip", new object[2] { slot, equip });
            };
            contextMenuStrip.Items.Add(toolStripMenuItem);
            contextMenuStrip.Items.Add(toolStripMenuItem1);
            if(Item.IsWeapon || Item.IsEquippableNonItem)
                contextMenuStrip.Items.Add(toolStripMenuItem2);
            return contextMenuStrip;
        }

        private static ContextMenuStrip MenuQuest(int QuestID)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Add to quest list"
            };
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem
            {
                Text = "Accept Quest"
            };
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
            {
                Text = "Complete Quest"
            };
            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
            {
                Text = "Load Quest"
            };
            toolStripMenuItem.Click += delegate (object S, EventArgs E)
            {
                AddQuest(S, E, QuestID);
            };
            toolStripMenuItem1.Click += delegate (object S, EventArgs E)
            {
                Player.Quests.Accept(QuestID);
            };
            toolStripMenuItem2.Click += delegate (object S, EventArgs E)
            {
                Player.Quests.Complete(QuestID);
            };
            toolStripMenuItem3.Click += delegate (object S, EventArgs E)
            {
                Player.Quests.Load(QuestID);
            };
            contextMenuStrip.Items.Add(toolStripMenuItem);
            contextMenuStrip.Items.Add(toolStripMenuItem1);
            contextMenuStrip.Items.Add(toolStripMenuItem2);
            contextMenuStrip.Items.Add(toolStripMenuItem3);
            return contextMenuStrip;
        }

        private static ContextMenuStrip MenuItems(List<InventoryItem> Items)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Add all to both"
            };
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem
            {
                Text = "Add all to whitelist"
            };
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
            {
                Text = "Add all to unbank list"
            };
            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
            {
                Text = "Search all on Wiki"
            };
            toolStripMenuItem.Click += delegate (object S, EventArgs E)
            {
                AddDrops(S, E, Items);
                AddItems(S, E, Items);
            };
            toolStripMenuItem1.Click += delegate (object S, EventArgs E)
            {
                AddDrops(S, E, Items);
            };
            toolStripMenuItem2.Click += delegate (object S, EventArgs E)
            {
                AddDrops(S, E, Items);
            };
            toolStripMenuItem3.Click += delegate (object S, EventArgs E)
            {
                foreach(InventoryItem Item in Items)
                {
                    Process.Start("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
                }
            };
            contextMenuStrip.Items.Add(toolStripMenuItem);
            contextMenuStrip.Items.Add(toolStripMenuItem1);
            contextMenuStrip.Items.Add(toolStripMenuItem2);
            contextMenuStrip.Items.Add(toolStripMenuItem3);
            return contextMenuStrip;
        }

        private static ContextMenuStrip MenuItem(InventoryItem Item)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Add to both"
            };
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
            {
                Text = "Add to whitelist"
            };
            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
            {
                Text = "Add to unbank list"
            };
            ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem
            {
                Text = "Copy item name to clipboard"
            };
            ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem
            {
                Text = "Go to Wikipage"
            };
            ToolStripMenuItem toolStripMenuItem6 = new ToolStripMenuItem
            {
                Text = "Search on Wiki"
            };
            toolStripMenuItem.Click += delegate (object S, EventArgs E)
            {
                AddDrop(S, E, Item);
                AddItem(S, E, Item);
            };
            toolStripMenuItem2.Click += delegate (object S, EventArgs E)
            {
                AddDrop(S, E, Item);
            };
            toolStripMenuItem3.Click += delegate (object S, EventArgs E)
            {
                AddItem(S, E, Item);
            };
            toolStripMenuItem4.Click += delegate
            {
                Clipboard.SetText(Item.Name);
            };
            toolStripMenuItem5.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start("https://aqwwiki.wikidot.com/" + Item.Name.Replace(" ", "+"));
                Search("https://aqwwiki.wikidot.com/" + Item.Name.Replace(" ", "+"));
            };
            toolStripMenuItem6.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
                Search("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
            };
            contextMenuStrip.Items.Add(toolStripMenuItem);
            contextMenuStrip.Items.Add(toolStripMenuItem2);
            contextMenuStrip.Items.Add(toolStripMenuItem3);
            contextMenuStrip.Items.Add(toolStripMenuItem4);
            contextMenuStrip.Items.Add(toolStripMenuItem5);
            contextMenuStrip.Items.Add(toolStripMenuItem6);
            return contextMenuStrip;
        }

        private static ContextMenuStrip MenuItem(ItemBase Item)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Add to both"
            };
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
            {
                Text = "Add to whitelist"
            };
            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
            {
                Text = "Add to unbank list"
            };
            ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem
            {
                Text = "Copy item name to clipboard"
            };
            ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem
            {
                Text = "Go to Wikipage"
            };
            ToolStripMenuItem toolStripMenuItem6 = new ToolStripMenuItem
            {
                Text = "Search on Wiki"
            };
            ToolStripMenuItem toolStripMenuItem7 = new ToolStripMenuItem
            {
                Text = "Equip SWF"
            };
            toolStripMenuItem.Click += delegate (object S, EventArgs E)
            {
                AddDrop(S, E, Item);
                AddItem(S, E, Item);
            };
            toolStripMenuItem2.Click += delegate (object S, EventArgs E)
            {
                AddDrop(S, E, Item);
            };
            toolStripMenuItem3.Click += delegate (object S, EventArgs E)
            {
                AddItem(S, E, Item);
            };
            toolStripMenuItem4.Click += delegate
            {
                Clipboard.SetText(Item.Name);
            };
            toolStripMenuItem5.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start("https://aqwwiki.wikidot.com/" + Item.Name.Replace(" ", "+"));
                Search("https://aqwwiki.wikidot.com/" + Item.Name.Replace(" ", "+"));
            };
            toolStripMenuItem6.Click += delegate (object S, EventArgs E)
            {
                //System.Diagnostics.Process.Start("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
                Search("https://aqwwiki.wikidot.com/search:site/q/" + Item.Name.Replace(" ", "+"));
            };
            toolStripMenuItem7.Click += delegate
            {
                string txt = Item.Category;
                string slot;
                if (txt == "Cape")
                    slot = "ba";
                else if (txt == "Pet")
                    slot = "pe";
                else if (txt == "Armor" || txt == "Class")
                    slot = "co";
                else if (txt == "Helm")
                    slot = "he";
                else
                    slot = "Weapon";
                dynamic equip = new ExpandoObject();
                equip.sFile = Item.File;
                equip.sLink = Item.Link;
                equip.sType = txt;
                Flash.Call("SetEquip", new object[2] { slot, equip });
            };
            contextMenuStrip.Items.Add(toolStripMenuItem);
            contextMenuStrip.Items.Add(toolStripMenuItem2);
            contextMenuStrip.Items.Add(toolStripMenuItem3);
            contextMenuStrip.Items.Add(toolStripMenuItem4);
            contextMenuStrip.Items.Add(toolStripMenuItem5);
            contextMenuStrip.Items.Add(toolStripMenuItem6);
            contextMenuStrip.Items.Add(toolStripMenuItem7);
            return contextMenuStrip;
        }

        private static void Search(string Item)
        {
            //BrowserForm.Instance.LoadUrl(Item);
            Process.Start(Item);
        }

        private static void AddDrop(object s, EventArgs e, InventoryItem Item)
        {
            if (!Item.IsTemporary)
            {
                BotManager.Instance.AddDrop(Item.Name);
            }
        }

        private static void AddItem(object s, EventArgs e, InventoryItem Item)
        {
            if (!Item.IsTemporary)
            {
                BotManager.Instance.AddItem(Item.Name);
            }
        }

        private static void AddDrops(object s, EventArgs e, List<InventoryItem> Items)
        {
            foreach (InventoryItem Item in Items)
            {
                AddDrop(s, e, Item);
            }
        }

        private static void AddItems(object s, EventArgs e, List<InventoryItem> Items)
        {
            foreach (InventoryItem Item in Items)
            {
                AddItem(s, e, Item);
            }
        }

        private static void AddDrop(object s, EventArgs e, ItemBase Item)
        {
            if (!Item.Temp)
            {
                BotManager.Instance.AddDrop(Item.Name);
            }
        }

        private static void AddItem(object s, EventArgs e, ItemBase Item)
        {
            if (!Item.Temp)
            {
                BotManager.Instance.AddItem(Item.Name);
            }
        }

        private static void AddDrops(object s, EventArgs e, List<ItemBase> Items)
        {
            foreach (ItemBase Item in Items)
            {
                AddDrop(s, e, Item);
            }
        }

        private static void AddItems(object s, EventArgs e, List<ItemBase> Items)
        {
            foreach (ItemBase Item in Items)
            {
                AddItem(s, e, Item);
            }
        }


        private static void AddQuest(object s, EventArgs e, int ID)
        {
            BotManager.Instance.AddQuest(ID);
        }
    }
}