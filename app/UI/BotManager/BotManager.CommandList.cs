using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Game.Data;
using Grimoire.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        public void AddCommand(IBotCommand cmd, bool Insert = false)
        {
            if (Insert)
            {
                lstCommands.Items.Insert((lstCommands.SelectedIndex > -1) ? lstCommands.SelectedIndex : lstCommands.Items.Count, cmd);
            }
            else
            {
                lstCommands.Items.Add(cmd);
            }
        }

        public void MultiMode()
        {
            if (this.lstCommands.SelectionMode != SelectionMode.MultiExtended)
            {
                this.lstCommands.SelectionMode = SelectionMode.MultiExtended;
                this.lstItems.SelectionMode = SelectionMode.MultiExtended;
                this.lstSkills.SelectionMode = SelectionMode.MultiExtended;
                this.lstQuests.SelectionMode = SelectionMode.MultiExtended;
                this.lstDrops.SelectionMode = SelectionMode.MultiExtended;
                this.lstBoosts.SelectionMode = SelectionMode.MultiExtended;
            }
            else
            {
                this.lstCommands.SelectionMode = SelectionMode.One;
                this.lstItems.SelectionMode = SelectionMode.One;
                this.lstSkills.SelectionMode = SelectionMode.One;
                this.lstQuests.SelectionMode = SelectionMode.One;
                this.lstDrops.SelectionMode = SelectionMode.One;
                this.lstBoosts.SelectionMode = SelectionMode.One;
            }
        }

        public void SelectionModeToggle(bool Type)
        {
            if (Type)
            {
                this.lstCommands.SelectionMode = SelectionMode.MultiExtended;
                this.lstItems.SelectionMode = SelectionMode.MultiExtended;
                this.lstSkills.SelectionMode = SelectionMode.MultiExtended;
                this.lstQuests.SelectionMode = SelectionMode.MultiExtended;
                this.lstDrops.SelectionMode = SelectionMode.MultiExtended;
                this.lstBoosts.SelectionMode = SelectionMode.MultiExtended;
            }
            else
            {
                this.lstCommands.SelectionMode = SelectionMode.One;
                this.lstItems.SelectionMode = SelectionMode.One;
                this.lstSkills.SelectionMode = SelectionMode.One;
                this.lstQuests.SelectionMode = SelectionMode.One;
                this.lstDrops.SelectionMode = SelectionMode.One;
                this.lstBoosts.SelectionMode = SelectionMode.One;
            }
        }

        private void lstCommands_DoubleClick(object sender, EventArgs e)
        {
            if (lstCommands.Items.Count <= 0)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Load bot";
                    openFileDialog.InitialDirectory = Config.Instance.GetValue<string>("botsDirectory") ?? Path.Combine(Application.StartupPath, "Bots");
                    openFileDialog.Filter = "Grimoire bots|*.gbot";
                    openFileDialog.DefaultExt = ".gbot";
                    if (openFileDialog.ShowDialog() == DialogResult.OK && TryDeserializeLoadBot(File.ReadAllText(openFileDialog.FileName), out Configuration config))
                    {
                        ApplyConfiguration(config);
                    }
                }
            }
            else if (lstCommands.SelectedIndex > -1)
            {
                int selectedIndex = lstCommands.SelectedIndex;
                object command = lstCommands.Items[selectedIndex];
                string editedJson;
                try
                {
                    editedJson = UserFriendlyCommandEditor.Show(command);
                }
                catch 
                {
                    editedJson = RawCommandEditor.Show(JsonConvert.SerializeObject(command, Formatting.Indented, _serializerSettings)); 
                }
                if (editedJson != null)
                {
                    try
                    {
                        IBotCommand item = (IBotCommand)JsonConvert.DeserializeObject(editedJson, command.GetType());
                        lstCommands.Items.Remove(command);
                        lstCommands.Items.Insert(selectedIndex, item);
                    }
                    catch (Exception ex)
                    {
                        ErrorReporter.Log("applying an edited command", ex);
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (SelectedList.SelectedItem != null)
            {
                int selectedIndex = SelectedList.SelectedIndex;
                if (selectedIndex > -1)
                {
                    _RemoveListBoxItem();
                }
            }
            GetAllCommands<CmdLabel>(lbLabels);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            _MoveListBoxDown();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            _MoveListBoxUp();
        }

        private void _RemoveListBoxItem()
        {
            SelectedList.BeginUpdate();
            for (int x = SelectedList.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = SelectedList.SelectedIndices[x];
                SelectedList.Items.RemoveAt(idx);
            }
            SelectedList.EndUpdate();
        }

        private void _MoveListBoxUp()
        {
            SelectedList.BeginUpdate();
            int numberOfSelectedItems = SelectedList.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                if (SelectedList.SelectedIndices[i] > 0)
                {
                    int indexToInsertIn = SelectedList.SelectedIndices[i] - 1;
                    SelectedList.Items.Insert(indexToInsertIn, SelectedList.SelectedItems[i]);
                    SelectedList.Items.RemoveAt(indexToInsertIn + 2);
                    SelectedList.SelectedItem = SelectedList.Items[indexToInsertIn];
                }
            }
            SelectedList.EndUpdate();
        }

        private void _MoveListBoxDown()
        {
            SelectedList.BeginUpdate();
            int numberOfSelectedItems = SelectedList.SelectedItems.Count;
            // when going down, instead of moving through the selected items from top to bottom
            // we'll go from bottom to top, it's easier to handle this way.
            for (int i = numberOfSelectedItems - 1; i >= 0; i--)
            {
                if (SelectedList.SelectedIndices[i] < SelectedList.Items.Count - 1)
                {
                    int indexToInsertIn = SelectedList.SelectedIndices[i] + 2;
                    SelectedList.Items.Insert(indexToInsertIn, SelectedList.SelectedItems[i]);
                    SelectedList.Items.RemoveAt(indexToInsertIn - 2);
                    SelectedList.SelectedItem = SelectedList.Items[indexToInsertIn - 1];
                }
            }
            SelectedList.EndUpdate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Are you sure you wanna clear this command list?", "Clear Button", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                SelectedList.Items.Clear();
            }
        }

        private void clearAllCommandLists_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Are you sure you wanna clear all command lists?", "Clear Button", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                lstBoosts.Items.Clear();
                lstCommands.Items.Clear();
                lstDrops.Items.Clear();
                lstItems.Items.Clear();
                lstQuests.Items.Clear();
                lstSkills.Items.Clear();
            }
        }

        private void cbLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoosts.Visible = SelectedList == lstBoosts;
            lstCommands.Visible = SelectedList == lstCommands;
            lstDrops.Visible = SelectedList == lstDrops;
            lstItems.Visible = SelectedList == lstItems;
            lstQuests.Visible = SelectedList == lstQuests;
            lstSkills.Visible = SelectedList == lstSkills;
        }

        private void lstBoxs_KeyPress(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Up)
            {
                _MoveListBoxUp();
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Down)
            {
                _MoveListBoxDown();
                e.Handled = true;
            }
            else if ((ModifierKeys == Keys.Control && e.KeyCode == Keys.Delete) || (ModifierKeys == Keys.Control && e.KeyCode == Keys.R))
            {
                btnRemove.PerformClick();
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.D && SelectedList.SelectedIndex > -1)
            {
                var selectedItems = SelectedList.SelectedItems;
                for (int i = 0; selectedItems.Count > i; i++)
                {
                    SelectedList.Items.Insert(SelectedList.SelectedIndex + selectedItems.Count + i, selectedItems[i]);
                }
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.C && SelectedList.SelectedIndex > -1)
            {
                Clipboard.Clear();
                Configuration items = new Configuration
                {
                    Commands = lstCommands.SelectedItems.Cast<IBotCommand>().ToList(),
                    Skills = lstSkills.SelectedItems.Cast<Skill>().ToList(),
                    Quests = lstQuests.SelectedItems.Cast<Quest>().ToList(),
                    Boosts = lstBoosts.SelectedItems.Cast<InventoryItem>().ToList(),
                    Drops = lstDrops.SelectedItems.Cast<string>().ToList(),
                    Items = lstItems.SelectedItems.Cast<string>().ToList()
                };
                Clipboard.SetText(JsonConvert.SerializeObject(items, Formatting.Indented, _saveSerializerSettings));
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
            {
                TryDeserialize(Clipboard.GetText(), out Configuration config);
                List<IBotCommand> commands = config.Commands;
                if (commands != null && commands.Count > 0)
                {
                    List<IBotCommand> items = lstCommands.Items.Cast<IBotCommand>().ToList();
                    int selectedIndex = lstCommands.SelectedIndex;
                    lstCommands.SelectedIndex = -1;
                    items.InsertRange(++selectedIndex, commands);
                    lstCommands.Items.Clear();
                    lstCommands.Items.AddRange(items.ToArray());
                    for (int i = 0; i < commands.Count; i++)
                        lstCommands.SelectedIndex = selectedIndex + i;
                }
                List<Skill> skills = config.Skills;
                if (skills != null && skills.Count > 0)
                {
                    ListBox.ObjectCollection items = lstSkills.Items;
                    object[] array = config.Skills.ToArray();
                    items.AddRange(array);
                }
                List<Quest> quests = config.Quests;
                if (quests != null && quests.Count > 0)
                {
                    ListBox.ObjectCollection items = lstQuests.Items;
                    object[] array = config.Quests.ToArray();
                    items.AddRange(array);
                }
                List<InventoryItem> boosts = config.Boosts;
                if (boosts != null && boosts.Count > 0)
                {
                    ListBox.ObjectCollection items = lstBoosts.Items;
                    object[] array = config.Boosts.ToArray();
                    items.AddRange(array);
                }
                List<string> drops = config.Drops;
                if (drops != null && drops.Count > 0)
                {
                    ListBox.ObjectCollection items = lstDrops.Items;
                    object[] array = config.Drops.ToArray();
                    items.AddRange(array);
                }
                List<string> item = config.Items;
                if (item != null && item.Count > 0)
                {
                    ListBox.ObjectCollection items = lstItems.Items;
                    object[] array = config.Items.ToArray();
                    items.AddRange(array);
                }
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.S)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Save bot";
                    saveFileDialog.InitialDirectory = Config.Instance.GetValue<string>("botsDirectory") ?? Path.Combine(Application.StartupPath, "Bots");
                    saveFileDialog.DefaultExt = ".gbot";
                    saveFileDialog.Filter = "Grimoire bots|*.gbot";
                    saveFileDialog.CheckFileExists = false;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Configuration configuration = SaveConfiguration();
                        try
                        {
                            File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(configuration, Formatting.Indented, _saveSerializerSettings));
                        }
                        catch (Exception ex)
                        {
                            DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Unable to save bot: " + ex.Message);
                        }
                    }
                }
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.X)
            {
                Clipboard.Clear();
                Configuration items = new Configuration
                {
                    Commands = lstCommands.SelectedItems.Cast<IBotCommand>().ToList(),
                };
                Clipboard.SetText(JsonConvert.SerializeObject(items, Formatting.Indented, _saveSerializerSettings));
                btnRemove.PerformClick();
                e.Handled = true;
            }
        }

        private void lstCommands_DragDrop(object sender, DragEventArgs e)
        {
            Configuration config;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1)
            {
                return;
            }
            if (TryDeserialize(File.ReadAllText(files[0]), out config))
            {
                ApplyConfiguration(config);
                GetAllCommands<CmdLabel>(lbLabels);
            }
        }

        private void lstCommands_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void lstCommands_Click(object sender, EventArgs e)
        {

        }

        private void lstCommands_MouseEnter(object sender, EventArgs e)
        {
            if (lstCommands.Items.Count <= 0)
            {
                Color c1 = lstCommands.BackColor;
                Color c2 = Color.FromArgb(c1.A, (int)(c1.R * 0.8), (int)(c1.G * 0.8), (int)(c1.B * 0.8));
                lstCommands.BackColor = c2;
            }
        }

        private void lstCommands_MouseLeave(object sender, EventArgs e)
        {
            Color lstCommandsBackColor = Color.FromArgb(28, 32, 40);
            lstCommands.BackColor = lstCommandsBackColor;
        }

        private void multilineToggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MultiMode();
        }

        private void lstCommands_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSearchCmd_Click(object sender, EventArgs e)
        {
            string Keyword = this.txtSearchCmd.Text.ToLower();
            ListBox.ObjectCollection lists = SelectedList.Items;
            if (Keyword != SKeyword)
            {
                SKeyword = "";
                LastIndexedSearch = 0;
                Filtered.Clear();
            }
            for (int i = 0; i < lists.Count; i++)
            {
                if (Keyword == SKeyword)
                {
                    Console.WriteLine("Using Cached List.");
                    break;
                }
                if (lists[i].ToString().ToLower().Contains($@"{Keyword}"))
                {
                    Filtered.Add(i);
                }
            }
            SKeyword = Keyword;
            if (Filtered.Count > 0)
            {
                SelectedList.SelectedIndex = -1;
                SelectedList.SelectedIndex = Filtered[LastIndexedSearch];
                SelectedList.TopIndex = Filtered[LastIndexedSearch];
                LastIndexedSearch++;
                if (LastIndexedSearch >= Filtered.Count)
                {
                    LastIndexedSearch = 0;
                }
            }
        }

        private void btnSearchCmd_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchCmd_Click(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void btnSearchCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.F)
            {
                txtSearchCmd.Focus();
                txtSearchCmd.Clear();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        #endregion
    }
}
