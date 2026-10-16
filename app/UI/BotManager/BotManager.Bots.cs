using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        public Configuration GenerateConfiguration()
        {
            return new Configuration
            {
                Author = txtAuthor.Text,
                Description = rtbInfo.Rtf,
                Commands = lstCommands.Items.Cast<IBotCommand>().ToList(),
                Skills = lstSkills.Items.Cast<Skill>().ToList(),
                Quests = lstQuests.Items.Cast<Quest>().ToList(),
                Boosts = lstBoosts.Items.Cast<InventoryItem>().ToList(),
                Drops = lstDrops.Items.Cast<string>().ToList(),
                Items = lstItems.Items.Cast<string>().ToList(),
                SkillDelay = (int)numSkillD.Value,
                ExitCombatBeforeRest = chkExitRest.Checked,
                ExitCombatBeforeQuest = chkExistQuest.Checked,
                Server = (Server)cbServers.SelectedItem,
                AutoRelogin = chkRelog.Checked,
                RelogDelay = (int)numRelogDelay.Value,
                RelogRetryUponFailure = chkRelogRetry.Checked,
                BotDelay = (int)numBotDelay.Value,
                EnablePickup = chkPickup.Checked,
                EnableRejection = chkReject.Checked,
                EnablePickupAll = chkPickupAll.Checked,
                EnablePickupAcTagged = chkPickupAcTag.Checked,
                WaitForAllSkills = chkAllSkillsCD.Checked,
                WaitForSkill = chkSkillCD.Checked,
                SkipDelayIndexIf = chkSkip.Checked,
                InfiniteAttackRange = chkInfiniteRange.Checked,
                ProvokeMonsters = chkProvoke.Checked,
                EnemyMagnet = chkMagnet.Checked,
                LagKiller = chkLag.Checked,
                HidePlayers = chkHidePlayers.Checked,
                SkipCutscenes = chkSkipCutscenes.Checked,
                DisableAnimations = chkDisableAnims.Checked,
                Untarget = chkUntarget.Checked,
                WalkSpeed = (int)numWalkSpeed.Value,
                NotifyUponDrop = lstSoundItems.Items.Cast<string>().ToList(),
                RestIfMp = chkMP.Checked,
                RestIfHp = chkHP.Checked,
                UnbankOnStart = chkUnbankOnStart.Checked,
                BankOnStop = chkBankOnStop.Checked,
                NotifyItemOnPickup = chkNotifyItemOnPickup.Checked,
                RelogUponQuestFailure = chkReloginOnQuestFailure.Checked,
                RelogUponItemBuyFailure = chkRelogUponFailBuy.Checked,
                RestMp = (int)numRestMP.Value,
                RestHp = (int)numRest.Value,
                RestartUponDeath = chkRestartDeath.Checked,
                AFK = chkAFK.Checked,
                DropDelay = (int)numDropDelay.Value
            };
        }

        public Configuration SaveConfiguration()
        {
            var compressed = DocConvert.Zip<string>(txtDescription.Text);

            return new Configuration
            {
                Author = txtAuthor.Text,
                Description = compressed.Length > txtDescription.Text.Length ? txtDescription.Text : compressed,
                Commands = lstCommands.Items.Cast<IBotCommand>().ToList(),
                Skills = lstSkills.Items.Cast<Skill>().ToList(),
                Quests = lstQuests.Items.Cast<Quest>().ToList(),
                Boosts = lstBoosts.Items.Cast<InventoryItem>().ToList(),
                Drops = lstDrops.Items.Cast<string>().ToList(),
                Items = lstItems.Items.Cast<string>().ToList(),
                SkillDelay = (int)numSkillD.Value,
                ExitCombatBeforeRest = chkExitRest.Checked,
                ExitCombatBeforeQuest = chkExistQuest.Checked,
                Server = (Server)cbServers.SelectedItem,
                AutoRelogin = chkRelog.Checked,
                RelogDelay = (int)numRelogDelay.Value,
                RelogRetryUponFailure = chkRelogRetry.Checked,
                BotDelay = (int)numBotDelay.Value,
                EnablePickup = chkPickup.Checked,
                EnableRejection = chkReject.Checked,
                EnablePickupAll = chkPickupAll.Checked,
                EnablePickupAcTagged = chkPickupAcTag.Checked,
                WaitForAllSkills = chkAllSkillsCD.Checked,
                WaitForSkill = chkSkillCD.Checked,
                SkipDelayIndexIf = chkSkip.Checked,
                InfiniteAttackRange = chkInfiniteRange.Checked,
                ProvokeMonsters = chkProvoke.Checked,
                EnemyMagnet = chkMagnet.Checked,
                LagKiller = chkLag.Checked,
                HidePlayers = chkHidePlayers.Checked,
                SkipCutscenes = chkSkipCutscenes.Checked,
                DisableAnimations = chkDisableAnims.Checked,
                Untarget = chkUntarget.Checked,
                WalkSpeed = (int)numWalkSpeed.Value,
                NotifyUponDrop = lstSoundItems.Items.Cast<string>().ToList(),
                RestIfMp = chkMP.Checked,
                RestIfHp = chkHP.Checked,
                UnbankOnStart = chkUnbankOnStart.Checked,
                BankOnStop = chkBankOnStop.Checked,
                NotifyItemOnPickup = chkNotifyItemOnPickup.Checked,
                RelogUponQuestFailure = chkReloginOnQuestFailure.Checked,
                RelogUponItemBuyFailure = chkRelogUponFailBuy.Checked,
                RestMp = (int)numRestMP.Value,
                RestHp = (int)numRest.Value,
                RestartUponDeath = chkRestartDeath.Checked,
                AFK = chkAFK.Checked,
                DropDelay = (int)numDropDelay.Value
            };
        }

        public void ApplyConfiguration(Configuration config)
        {
            if (config != null)
            {
                if (!chkMerge.Checked || ActiveBotEngine.IsRunning)
                {
                    lstCommands.Items.Clear();
                    lstBoosts.Items.Clear();
                    lstDrops.Items.Clear();
                    lstQuests.Items.Clear();
                    lstSkills.Items.Clear();
                    lstItems.Items.Clear();
                    lstSoundItems.Items.Clear();
                }
                txtSavedAuthor.Text = config.Author ?? "Author";
                txtSavedDesc.Text = DocConvert.IsCompressed(config.Description) ? DocConvert.Unzip(config.Description) : config.Description ?? "Description";
                List<IBotCommand> commands = config.Commands;
                if (commands != null && commands.Count > 0)
                {
                    ListBox.ObjectCollection items = lstCommands.Items;
                    object[] array = config.Commands.ToArray();
                    items.AddRange(array);
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
                numSkillD.Value = config.SkillDelay;
                chkExitRest.Checked = config.ExitCombatBeforeRest;
                chkExistQuest.Checked = config.ExitCombatBeforeQuest;
                if (config.Server != null)
                {
                    cbServers.SelectedIndex = cbServers.Items.Cast<Server>().ToList().FindIndex((Server s) => s.Name == config.Server.Name);
                }
                chkRelog.Checked = config.AutoRelogin;
                numRelogDelay.Value = config.RelogDelay;
                chkRelogRetry.Checked = config.RelogRetryUponFailure;
                numBotDelay.Value = config.BotDelay;
                chkPickup.Checked = config.EnablePickup;
                chkReject.Checked = config.EnableRejection;
                chkPickupAll.Checked = config.EnablePickupAll;
                chkAllSkillsCD.Checked = config.WaitForAllSkills;
                chkSkillCD.Checked = config.WaitForSkill;
                chkSkip.Checked = config.SkipDelayIndexIf;
                chkInfiniteRange.Checked = config.InfiniteAttackRange;
                chkLag.Checked = config.LagKiller;
                chkMagnet.Checked = config.EnemyMagnet;
                chkHidePlayers.Checked = config.HidePlayers;
                chkSkipCutscenes.Checked = config.SkipCutscenes;
                chkDisableAnims.Checked = config.DisableAnimations;
                chkUntarget.Checked = config.Untarget;
                numWalkSpeed.Value = (config.WalkSpeed <= 0) ? 8 : config.WalkSpeed;
                List<string> notifyUponDrop = config.NotifyUponDrop;
                if (notifyUponDrop != null && notifyUponDrop.Count > 0)
                {
                    ListBox.ObjectCollection items14 = lstSoundItems.Items;
                    object[] array = config.NotifyUponDrop.ToArray();
                    object[] items15 = array;
                    items14.AddRange(items15);
                }
                numRestMP.Value = config.RestMp;
                numRest.Value = config.RestHp;
                chkMP.Checked = config.RestIfMp;
                chkHP.Checked = config.RestIfHp;
                chkUnbankOnStart.Checked = config.UnbankOnStart;
                chkBankOnStop.Checked = config.BankOnStop;
                chkNotifyItemOnPickup.Checked = config.NotifyItemOnPickup;
                chkReloginOnQuestFailure.Checked = config.RelogUponQuestFailure;
                chkRelogUponFailBuy.Checked = config.RelogUponItemBuyFailure;
                chkRestartDeath.Checked = config.RestartUponDeath;
                chkAFK.Checked = config.AFK;
                numDropDelay.Value = (config.DropDelay < 0) ? 0 : config.DropDelay;
                var description = txtSavedDesc.Text ?? "Description";
                if (description.StartsWith("{\\rtf") || description.StartsWith("{\rtf"))
                    rtbInfo.Rtf = description;
                else
                    rtbInfo.Text = config.Description;
                LastIndexedSearch = 0;
            }
        }

        public void btnLoad_Click(object sender, EventArgs e)
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
                    GetAllCommands<CmdLabel>(lbLabels);
                }
            }
        }

        public void btnSaveDirectory_Click(object sender, EventArgs e)
        {
            Config.Instance.SetValue("botsDirectory", txtSaved.Text.ToString());
        }

        private bool TryDeserializeLoadBot(string json, out Configuration config)
        {
            try
            {
                config = JsonConvert.DeserializeObject<Configuration>(json, _saveSerializerSettings);
                return true;
            }
            catch (Exception e)
            {
                var specifiedError = e.Message;
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen, Icon = global::Properties.Resources.GrimoireIcon }, $"Failed to load the bot. You cannot load any bot that has commands not available in this client.\r\n\r\nError Message:\r\n{specifiedError}", "Load Bot", MessageBoxIcon.Error);
            }
            config = null;
            return false;
        }

        private bool TryDeserialize(string json, out Configuration config)
        {
            try
            {
                config = JsonConvert.DeserializeObject<Configuration>(json, _saveSerializerSettings);
                return true;
            }
            catch (Exception e)
            {
                var specifiedError = e.Message;
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, $"Failed to process the event.\r\n\r\nError Message:\r\n{specifiedError}", "Load Bot", MessageBoxIcon.Error);
            }
            config = null;
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
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
                        System.Windows.Forms.MessageBox.Show("Unable to save bot: " + ex.Message);
                    }
                }
            }
        }

        private void UpdateTree()
        {
            if (!string.IsNullOrEmpty(txtSaved.Text) && Directory.Exists(txtSaved.Text))
            {
                lblBots.Text = string.Format("Number of Bots: {0}", Directory.EnumerateFiles(txtSaved.Text, "*.gbot", SearchOption.AllDirectories).Count());
                treeBots.Nodes.Clear();
                AddTreeNodes(treeBots, txtSaved.Text);
            }
        }

        private void treeBots_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = Path.Combine(txtSaved.Text, e.Node.FullPath);
            if (File.Exists(path))
            {
                if (!TryDeserializeLoadBot(File.ReadAllText(path), out Configuration config))
                {
                    return;
                }
                ApplyConfiguration(config);
            }
            lblCommands.Text = $"Commands: {lstCommands.Items.Count}";
            lblSkills.Text = $"Skills: {lstSkills.Items.Count}";
            lblQuests.Text = $"Quests: {lstQuests.Items.Count}";
            lblDrops.Text = $"Drops: {lstDrops.Items.Count}";
            lblBoosts.Text = $"Boosts: {lstBoosts.Items.Count}";
            lblItems.Text = $"Items: {lstItems.Items.Count}";
        }

        private void treeBots_AfterExpand(object sender, TreeViewEventArgs e)
        {
            string path = Path.Combine(txtSaved.Text, e.Node.FullPath);
            if (Directory.Exists(path))
            {
                AddTreeNodes(e.Node, path);
                if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "Loading...")
                {
                    e.Node.Nodes.RemoveAt(0);
                }
            }
        }

        private void AddTreeNodes(TreeNode node, string path)
        {
            foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
            {
                string add = Path.GetFileName(item);
                if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
                {
                    node.Nodes.Add(add).Nodes.Add("Loading...");
                }
            }
            foreach (string item2 in Directory.EnumerateFiles(path, "*.gbot", SearchOption.TopDirectoryOnly))
            {
                string add2 = Path.GetFileName(item2);
                if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
                {
                    node.Nodes.Add(add2);
                }
            }
        }

        private void AddTreeNodes(TreeView tree, string path)
        {
            foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
            {
                string add = Path.GetFileName(item);
                if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
                {
                    tree.Nodes.Add(add).Nodes.Add("Loading...");
                }
            }
            foreach (string item2 in Directory.EnumerateFiles(path, "*.gbot", SearchOption.TopDirectoryOnly))
            {
                string add2 = Path.GetFileName(item2);
                if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
                {
                    tree.Nodes.Add(add2);
                }
            }
        }

        private void btnSavedAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSaved.Text))
            {
                string path = Path.Combine(txtSaved.Text, txtSavedAdd.Text);
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception ex)
                    {
                        DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Unable to create directory: " + ex.Message, "Grimoire", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                UpdateTree();
            }
        }

        private void txtSaved_TextChanged(object sender, EventArgs e)
        {
            UpdateTree();
        }

        private void treeBots_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        #endregion
    }
}
