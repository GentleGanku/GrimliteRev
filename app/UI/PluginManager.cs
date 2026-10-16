using DarkUI.Forms;
using Grimoire.Tools.Plugins;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class PluginManager : DarkForm
    {
        #region Fields

        private string path = Application.StartupPath + "\\Plugins";
        private string Plugintext;

        #endregion

        #region Constructors

        public PluginManager()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public static PluginManager Instance
        {
            get;
        } = new PluginManager();

        public string LastError
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public bool LoadPlugins(string path)
        {
            GrimoirePlugin grimoirePlugin = null;
            if (File.Exists(path))
            {
                grimoirePlugin = new GrimoirePlugin(path);
                if (grimoirePlugin.Load())
                {
                    GrimoirePlugin.LoadedPlugins.Add(grimoirePlugin);
                    return true;
                }
            }
            LastError = grimoirePlugin?.LastError;
            return false;
        }

        public bool LoadRange(string[] paths)
        {
            return paths.All(LoadPlugins);
        }

        private void PluginManager_Load(object sender, EventArgs e)
        {
            lstLoaded.DisplayMember = "Name";
            if (!Directory.Exists(path))
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
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, grimoirePlugin.LastError, "Grimoire", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, grimoirePlugin.LastError, "Error Detected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, grimoirePlugin.LastError, "Error Detected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #endregion
    }
}