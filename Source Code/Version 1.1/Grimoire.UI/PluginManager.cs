using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Tools.Plugins;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class PluginManager : DarkForm
    {
        private IContainer components;
        public DarkGroupBox gbLoaded;
        public DarkButton btnUnload;
        public DarkTextBox txtDesc;
        public DarkLabel lblAuthor;
        public ListBox lstLoaded;
        public DarkGroupBox gbLoad;
        public DarkButton btnBrowse;
        public DarkButton btnLoad;
        private TreeView treePlugins;
        public DarkTextBox txtPlugin;

        public static PluginManager Instance
        {
            get;
        } = new PluginManager();

        public PluginManager()
        {
            InitializeComponent();
        }

        private string path = Application.StartupPath + "\\Plugins";

        private void PluginManager_Load(object sender, EventArgs e)
        {
            lstLoaded.DisplayMember = "Name";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            UpdateTree();
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
            foreach (string item2 in Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly))
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
            foreach (string item2 in Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly))
            {
                string add2 = Path.GetFileName(item2);
                if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
                {
                    tree.Nodes.Add(add2);
                }
            }
        }

        private void UpdateTree()
        {
            treePlugins.Nodes.Clear();
            AddTreeNodes(treePlugins, path);
        }

        private void treePlugins_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string text;
            if (File.Exists(text = Path.Combine(path, e.Node.FullPath)))
            {
                GrimoirePlugin grimoirePlugin = new GrimoirePlugin(text);
                if (grimoirePlugin.Load())
                {
                    txtPlugin.Clear();
                    lstLoaded.Items.Clear();
                    ListBox.ObjectCollection items = lstLoaded.Items;
                    object[] items2 = GrimoirePlugin.LoadedPlugins.ToArray();
                    items.AddRange(items2);
                    lstLoaded.SelectedItem = grimoirePlugin;
                }
                else
                {
                    MessageBox.Show(grimoirePlugin.LastError, "Grimoire", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void treePlugins_AfterExpand(object sender, TreeViewEventArgs e)
        {
            string text;
            if (Directory.Exists(text = Path.Combine(path, e.Node.FullPath)))
            {
                AddTreeNodes(e.Node, text);
                if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "Loading...")
                {
                    e.Node.Nodes.RemoveAt(0);
                }
            }
        }

        private string Plugintext;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load Grimoire plugin";
                openFileDialog.Filter = "Dynamic Link Library|*.dll";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPlugin.Text = openFileDialog.SafeFileName;
                    Plugintext = openFileDialog.FileName;
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string text;
            if (File.Exists(text = Plugintext))
            {
                GrimoirePlugin grimoirePlugin = new GrimoirePlugin(text);
                if (grimoirePlugin.Load())
                {
                    txtPlugin.Clear();
                    lstLoaded.Items.Clear();
                    ListBox.ObjectCollection items = lstLoaded.Items;
                    object[] items2 = GrimoirePlugin.LoadedPlugins.ToArray();
                    items.AddRange(items2);
                    lstLoaded.SelectedItem = grimoirePlugin;
                }
                else
                {
                    MessageBox.Show(grimoirePlugin.LastError, "Grimoire", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            int selectedIndex;
            if ((selectedIndex = lstLoaded.SelectedIndex) > -1)
            {
                GrimoirePlugin grimoirePlugin = GrimoirePlugin.LoadedPlugins[selectedIndex];
                if (grimoirePlugin.Unload())
                {
                    lstLoaded.Items.RemoveAt(selectedIndex);
                    lblAuthor.Text = "Plugin created by:";
                    txtDesc.Clear();
                }
                else
                {
                    MessageBox.Show(grimoirePlugin.LastError, "Grimoire", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void lstLoaded_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex;
            if ((selectedIndex = lstLoaded.SelectedIndex) > -1)
            {
                GrimoirePlugin grimoirePlugin = GrimoirePlugin.LoadedPlugins[selectedIndex];
                lblAuthor.Text = "Plugin created by: " + grimoirePlugin.Author;
                txtDesc.Text = grimoirePlugin.Description;
            }
        }

        private void PluginManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.gbLoaded = new DarkUI.Controls.DarkGroupBox();
            this.btnUnload = new DarkUI.Controls.DarkButton();
            this.txtDesc = new DarkUI.Controls.DarkTextBox();
            this.lblAuthor = new DarkUI.Controls.DarkLabel();
            this.lstLoaded = new System.Windows.Forms.ListBox();
            this.gbLoad = new DarkUI.Controls.DarkGroupBox();
            this.btnBrowse = new DarkUI.Controls.DarkButton();
            this.btnLoad = new DarkUI.Controls.DarkButton();
            this.txtPlugin = new DarkUI.Controls.DarkTextBox();
            this.treePlugins = new System.Windows.Forms.TreeView();
            this.gbLoaded.SuspendLayout();
            this.gbLoad.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLoaded
            // 
            this.gbLoaded.Controls.Add(this.btnUnload);
            this.gbLoaded.Controls.Add(this.txtDesc);
            this.gbLoaded.Controls.Add(this.lblAuthor);
            this.gbLoaded.Controls.Add(this.lstLoaded);
            this.gbLoaded.Location = new System.Drawing.Point(12, 213);
            this.gbLoaded.Name = "gbLoaded";
            this.gbLoaded.Size = new System.Drawing.Size(292, 267);
            this.gbLoaded.TabIndex = 12;
            this.gbLoaded.TabStop = false;
            this.gbLoaded.Text = "Loaded plugins";
            // 
            // btnUnload
            // 
            this.btnUnload.Checked = false;
            this.btnUnload.Location = new System.Drawing.Point(148, 238);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(135, 23);
            this.btnUnload.TabIndex = 3;
            this.btnUnload.Text = "Unload selected plugin";
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(6, 120);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDesc.Size = new System.Drawing.Size(277, 112);
            this.txtDesc.TabIndex = 2;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblAuthor.Location = new System.Drawing.Point(6, 104);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(92, 13);
            this.lblAuthor.TabIndex = 1;
            this.lblAuthor.Text = "Plugin created by:";
            // 
            // lstLoaded
            // 
            this.lstLoaded.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lstLoaded.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLoaded.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lstLoaded.FormattingEnabled = true;
            this.lstLoaded.Location = new System.Drawing.Point(6, 19);
            this.lstLoaded.Name = "lstLoaded";
            this.lstLoaded.ScrollAlwaysVisible = true;
            this.lstLoaded.Size = new System.Drawing.Size(277, 67);
            this.lstLoaded.TabIndex = 0;
            this.lstLoaded.SelectedIndexChanged += new System.EventHandler(this.lstLoaded_SelectedIndexChanged);
            // 
            // gbLoad
            // 
            this.gbLoad.Controls.Add(this.btnBrowse);
            this.gbLoad.Controls.Add(this.btnLoad);
            this.gbLoad.Controls.Add(this.txtPlugin);
            this.gbLoad.Location = new System.Drawing.Point(12, 12);
            this.gbLoad.Name = "gbLoad";
            this.gbLoad.Size = new System.Drawing.Size(292, 51);
            this.gbLoad.TabIndex = 11;
            this.gbLoad.TabStop = false;
            this.gbLoad.Text = "Load plugin";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Checked = false;
            this.btnBrowse.Location = new System.Drawing.Point(200, 17);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 23);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Checked = false;
            this.btnLoad.Location = new System.Drawing.Point(231, 17);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(55, 23);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtPlugin
            // 
            this.txtPlugin.Location = new System.Drawing.Point(6, 19);
            this.txtPlugin.Name = "txtPlugin";
            this.txtPlugin.Size = new System.Drawing.Size(188, 20);
            this.txtPlugin.TabIndex = 4;
            // 
            // treePlugins
            // 
            this.treePlugins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.treePlugins.ForeColor = System.Drawing.Color.Gainsboro;
            this.treePlugins.Location = new System.Drawing.Point(12, 70);
            this.treePlugins.Name = "treePlugins";
            this.treePlugins.Size = new System.Drawing.Size(292, 136);
            this.treePlugins.TabIndex = 13;
            this.treePlugins.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treePlugins_AfterExpand);
            this.treePlugins.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePlugins_AfterSelect);
            // 
            // PluginManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(316, 485);
            this.Controls.Add(this.treePlugins);
            this.Controls.Add(this.gbLoaded);
            this.Controls.Add(this.gbLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.MaximizeBox = false;
            this.Name = "PluginManager";
            this.Text = "Plugin Manager";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginManager_FormClosing);
            this.Load += new System.EventHandler(this.PluginManager_Load);
            this.gbLoaded.ResumeLayout(false);
            this.gbLoaded.PerformLayout();
            this.gbLoad.ResumeLayout(false);
            this.gbLoad.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}