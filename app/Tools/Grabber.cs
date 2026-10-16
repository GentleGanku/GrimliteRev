using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using HtmlAgilityPack;

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
                    try
                    {
                        TreeNode treeNode = tree.Nodes.Add($"{item.Id} - {item.Name}");
                        treeNode.Nodes.Add($"ID: {item.Id}");
                        treeNode.Nodes.Add($"Description: {item.Description} ");
                        treeNode.Nodes.Add($"End Description: {item.EndText} ");
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
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        public static void GrabShopItems(TreeView tree)
        {
            //List<ShopInfo> list = (from x in World.LoadedShops?.GroupBy((ShopInfo s) => s.Id)
            //                       select x.First()).ToList();
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
                            if (item2.IsEquippableNonItem || item2.IsWeapon)
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
                    treeNode.Nodes.Add($"Char Item ID: {item.CharItemId}");
                    treeNode.Nodes.Add($"Quantity: {item.Quantity}/{item.MaxStack}");
                    treeNode.Nodes.Add($"AC-tagged: {item.IsAcItem}");
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
            List<Monster> list = (from x in World.MonstersInMap?.GroupBy((Monster m) => m.Name)
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
                    treeNode.Nodes.Add($"Health: {item.MaxHealth.ToString("N", new CultureInfo("en-US")).Split('.')[0]}");
                    TreeNode treeNode2 = treeNode.Nodes.Add("Drops");
                    treeNode2.Nodes.Add("Loading..");
                    treeNode2.TreeView.MouseDown += delegate (object S, MouseEventArgs E)
                    {
                        try
                        {
                            if ((treeNode2.ForeColor != Color.Gray) && !treeNode2.IsExpanded && (treeNode2.FirstNode.Text == "Loading.."))
                            {
                                if (treeNode.TreeView.GetNodeAt(E.Location).Text == "Drops")
                                    Monster_Drops(treeNode2, item);
                            }
                        }
                        catch { }
                    };
                }
            }
        }

        private static async void Monster_Drops(TreeNode treeNode2, Monster item)
        {
            treeNode2.ForeColor = Color.Gray;
            try
            {
                var web = new HtmlWeb();
                web.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)";
                var doc = await web.LoadFromWebAsync("https://aqwwiki.wikidot.com/" + item.Name);
                HtmlNodeCollection targeted = doc.DocumentNode.SelectNodes("//div[@id='page-content']");
                bool isTabbed = false;
                if (doc.ParsedText.Contains("usually refers to"))
                {
                    foreach (var docLink in doc.DocumentNode.SelectNodes("//div[@id='page-content']//p//a[@href]"))
                    {
                        doc = await web.LoadFromWebAsync("https://aqwwiki.wikidot.com/" + docLink.Attributes["href"].Value);
                        var text = doc.DocumentNode.SelectSingleNode("//div[@id='page-content']");
                        if (text.InnerText.Contains($"Level: {item.Level}") && text.InnerText.Contains($"Total HP:") && text.InnerText.Contains($" {item.MaxHealth.ToString("N", new CultureInfo("en-US")).Split('.')[0]}"))
                        {
                            targeted = doc.DocumentNode.SelectNodes("//div[@id='page-content']");
                            break;
                        }
                    }
                }
                if (targeted[0].SelectSingleNode("div").SelectNodes("div[@class='yui-content']") != null)
                {
                    foreach (var monTab in targeted[0].SelectSingleNode("div").SelectNodes("div[@class='yui-content']")[0].ChildNodes)
                    {
                        if (monTab.InnerText.Contains(Player.Map.Substring(0, 1).ToUpper() + Player.Map.Substring(1, 2)) && monTab.InnerText.Contains($"Level: {item.Level}") && monTab.InnerText.Contains($"Total HP:") && monTab.InnerText.Contains($" {item.MaxHealth.ToString("N", new CultureInfo("en-US")).Split('.')[0]}"))
                        {
                            targeted = monTab.ChildNodes;
                            isTabbed = true;
                            break;
                        }
                    }
                }
                int indTemp = isTabbed ? targeted.GetNodeIndex(targeted.FirstOrDefault((HtmlNode n) => n.InnerText.Equals("Temporary Items Dropped:", StringComparison.OrdinalIgnoreCase))) : targeted[0].ChildNodes.GetNodeIndex(targeted[0].ChildNodes.FirstOrDefault((HtmlNode n) => n.InnerText.Equals("Temporary Items Dropped:", StringComparison.OrdinalIgnoreCase)));
                if (indTemp > -1)
                {
                    TreeNode treeNodeTemp = treeNode2.Nodes.Add("Temporary Items");
                    HtmlNodeCollection targetedChildNodes = isTabbed ? targeted[indTemp + 2].ChildNodes : targeted[0].ChildNodes[indTemp + 2].ChildNodes;
                    foreach (var node in targetedChildNodes)
                    {
                        if ((targetedChildNodes.Count == 0) || (node.InnerText.Equals("N/A", StringComparison.OrdinalIgnoreCase)) || (node.InnerText.Equals("Items Dropped:", StringComparison.OrdinalIgnoreCase)))
                        {
                            treeNodeTemp.Nodes.Add("N/A");
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(node.InnerText) && !node.OuterHtml.Contains("raresmall.png") && !node.OuterHtml.Contains("text-decoration: line-through"))
                        {
                            string nodeText = node.InnerText.Split(new string[] { node.InnerText.Contains(" (Dropped") ? " (Dropped" : node.InnerText.Contains(" (dropped") ? " (dropped" : " (During" }, StringSplitOptions.None)[0];
                            nodeText = nodeText.Split(new string[] { " x" }, StringSplitOptions.None)[0];
                            int lastInd = nodeText.LastIndexOf(' ');
                            TreeNode treeNodeTempItem = treeNodeTemp.Nodes.Add(nodeText.EndsWith(" ") ? nodeText.Remove(lastInd) : nodeText);
                            treeNodeTempItem.Nodes.Add($"Legend-tagged: " + (node.OuterHtml.Contains("legendsmall.png") ? "True" : "False"));
                            treeNodeTempItem.Nodes.Add($"Seasonal-tagged: " + (node.OuterHtml.Contains("seasonalsmall.png") ? "True" : "False"));
                            if (node.InnerText.Contains(" (Dropped") || node.InnerText.Contains(" (dropped") || node.InnerText.Contains(" (During"))
                                treeNodeTempItem.Nodes.Add($"Note: Dropped during the '{node.ChildNodes["a"].LastChild.InnerText}' quest.");
                        }
                    }
                }
                int indItem = isTabbed ? targeted.GetNodeIndex(targeted.FirstOrDefault((HtmlNode n) => n.InnerText.Equals("Items Dropped:", StringComparison.OrdinalIgnoreCase))) : targeted[0].ChildNodes.GetNodeIndex(targeted[0].ChildNodes.FirstOrDefault((HtmlNode n) => n.InnerText.Equals("Items Dropped:", StringComparison.OrdinalIgnoreCase)));
                if (indItem > -1)
                {
                    TreeNode treeNodeI = treeNode2.Nodes.Add("Items");
                    HtmlNodeCollection targetedChildNodes = isTabbed ? targeted[indItem + 2].ChildNodes : targeted[0].ChildNodes[indItem + 2].ChildNodes;
                    foreach (var node in targetedChildNodes)
                    {
                        if ((targetedChildNodes.Count == 0) || (node.InnerText.Equals("N/A", StringComparison.OrdinalIgnoreCase)))
                        {
                            treeNodeI.Nodes.Add("N/A");
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(node.InnerText) && !node.OuterHtml.Contains("raresmall.png") && !node.OuterHtml.Contains("text-decoration: line-through"))
                        {
                            string nodeText = node.ChildNodes["a"].FirstChild.InnerText;
                            int lastInd = nodeText.LastIndexOf(' ');
                            TreeNode treeNodeItem = treeNodeI.Nodes.Add(nodeText.EndsWith(" ") ? nodeText.Remove(lastInd) : nodeText);
                            treeNodeItem.ContextMenuStrip = Wiki(nodeText.EndsWith(" ") ? nodeText.Remove(lastInd) : nodeText);
                            treeNodeItem.ContextMenuStrip = MenuItem(new InventoryItem { Name = nodeText.EndsWith(" ") ? nodeText.Remove(lastInd) : nodeText });
                            treeNodeItem.Nodes.Add($"AC-tagged: " + (node.OuterHtml.Contains("acsmall.png") ? "True" : "False"));
                            treeNodeItem.Nodes.Add($"Legend-tagged: " + (node.OuterHtml.Contains("legendsmall.png") ? "True" : "False"));
                            treeNodeItem.Nodes.Add($"Seasonal-tagged: " + (node.OuterHtml.Contains("seasonalsmall.png") ? "True" : "False"));
                            if (node.InnerText.Contains(" (Dropped") || node.InnerText.Contains(" (dropped") || node.InnerText.Contains(" (During"))
                                treeNodeItem.Nodes.Add($"Note: Dropped during the '{node.ChildNodes["a"].LastChild.InnerText}' quest.");
                        }
                    }
                }
                if (treeNode2.Nodes.Count == 0)
                    treeNode2.Nodes.Add("N/A");
            }
            catch (Exception ex)
            {
                treeNode2.Nodes.Clear();
                treeNode2.Nodes.Add("The Wiki is not available at the moment.");
            }
            treeNode2.Nodes.RemoveAt(0);
            treeNode2.ForeColor = Color.Gainsboro;
        }

        private static ContextMenuStrip Wiki(string item)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Go to Wiki page"
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
                Text = "Go to Wiki page"
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
                Text = "Go to Wiki page"
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
                else if (txt == "Misc")
                    slot = "mi";
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
            if (Item.IsWeapon || Item.IsEquippableNonItem)
                contextMenuStrip.Items.Add(toolStripMenuItem2);
            return contextMenuStrip;
        }

        private static ContextMenuStrip MenuQuest(int QuestID)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Add to Quest List"
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
                Text = "Add all to Drops and Items List"
            };
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem
            {
                Text = "Add all to Drops List"
            };
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
            {
                Text = "Add all to Items list"
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
                foreach (InventoryItem Item in Items)
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
                Text = "Add to Drops and Items List"
            };
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
            {
                Text = "Add to Drops List"
            };
            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
            {
                Text = "Add to Items List"
            };
            ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem
            {
                Text = "Copy item name to Clipboard"
            };
            ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem
            {
                Text = "Go to Wiki page"
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
                Text = "Add to Drops and Items List"
            };
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
            {
                Text = "Add to Drops List"
            };
            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem
            {
                Text = "Add to Items List"
            };
            ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem
            {
                Text = "Copy item name to Clipboard"
            };
            ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem
            {
                Text = "Go to Wiki page"
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
                else if (txt == "Misc")
                    slot = "mi";
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