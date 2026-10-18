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
                    return true;
                }
            }
            LastError = grimoirePlugin?.LastError;
            return false;
        }

        /// <summary>Loads every DLL in <paramref name="paths"/>; one that fails is logged and doesn't stop the rest.</summary>
        public bool LoadRange(string[] paths)
        {
            bool allLoaded = true;
            foreach (string path in paths.Where(p => p.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)))
            {
                if (!LoadPlugins(path))
                {
                    allLoaded = false;
                    LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Couldn't load the plugin {Path.GetFileName(path)}: {LastError}\r\n");
                }
            }
            return allLoaded;
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
                LoadOrSelect(text, "Grimoire", MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// Loads the plugin at <paramref name="dllPath"/> and selects it in the loaded list. A plugin that is already
        /// loaded, at startup or from this window, is only selected, so it never runs twice.
        /// </summary>
        private void LoadOrSelect(string dllPath, string errorTitle, MessageBoxIcon errorIcon)
        {
            string fullPath = Path.GetFullPath(dllPath);
            GrimoirePlugin grimoirePlugin = GrimoirePlugin.LoadedPlugins.Find(p => string.Equals(Path.GetFullPath(p.FilePath), fullPath, StringComparison.OrdinalIgnoreCase));
            if (grimoirePlugin == null)
            {
                grimoirePlugin = new GrimoirePlugin(dllPath);
                if (!grimoirePlugin.Load())
                {
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, grimoirePlugin.LastError, errorTitle, MessageBoxButtons.OK, errorIcon);
                    return;
                }
            }
            txtPlugin.Clear();
            lstLoaded.Items.Clear();
            ListBox.ObjectCollection items = lstLoaded.Items;
            object[] items2 = GrimoirePlugin.LoadedPlugins.ToArray();
            items.AddRange(items2);
            lstLoaded.SelectedItem = grimoirePlugin;
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
                LoadOrSelect(text, "Error Detected!", MessageBoxIcon.Error);
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