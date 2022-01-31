using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
    public partial class Set : DarkForm
    {
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Include,
            //NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        };

        public Set()
        {
            InitializeComponent();
        }

        private void ApplyConfig(SetItem config)
        {
            listBox1.Items.Clear();
            List<ISetInterface> Set = config.Set;
            if (Set != null && Set.Count > 0)
            {
                ListBox.ObjectCollection items = listBox1.Items;
                object[] array = config.Set.ToArray();
                items.AddRange(array);
            }
        }

        private SetItem GenerateConfig()
        {
            return new SetItem
            {
                Set = listBox1.Items.Cast<ISetInterface>().ToList(),
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            string t1 = ((InventoryItem)comboBox1.SelectedItem).Name;
            string t2 = ((InventoryItem)comboBox1.SelectedItem).Category;
            if(InventoryItem.Weapons.Contains(t2))
                t2 = "Weapon";
            AddSetItem(new Item()
            {  Name = t1,
               Type = t2
            });
        }

        private string path = Application.StartupPath + "\\Sets";

        private void Set_Load(object sender, EventArgs e)
        {
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //UpdateTree();
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
            foreach (string item2 in Directory.EnumerateFiles(path, "*.gset", SearchOption.TopDirectoryOnly))
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
            foreach (string item2 in Directory.EnumerateFiles(path, "*.gset", SearchOption.TopDirectoryOnly))
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
            //treeView1.Nodes.Clear();
            //AddTreeNodes(treeView1, path);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string text;
            if (File.Exists(text = Path.Combine(path, e.Node.FullPath)))
            {
                TryDeserialize(File.ReadAllText(text), out SetItem config);
                ApplyConfig(config);
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
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

        private void Set_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn || Player.Inventory.Items == null)
                return;
            if (comboBox1.Items.Count > 0)
                comboBox1.Items.Clear();
            comboBox1.Items.Add("");
            comboBox1.Items.Add("[Weapons]");
            comboBox1.Items.Add("");
            foreach (InventoryItem s in Player.Inventory.Items)
            {
                if (s.IsWeapon)
                {
                    comboBox1.Items.Add(s);
                }
            }
            comboBox1.Items.Add("");
            comboBox1.Items.Add("[Classes]");
            comboBox1.Items.Add("");
            foreach (InventoryItem s in Player.Inventory.Items)
            {
                if (s.Category == "Class")
                {
                    comboBox1.Items.Add(s);
                }
            }
            comboBox1.Items.Add("");
            comboBox1.Items.Add("[Armors]");
            comboBox1.Items.Add("");
            foreach (InventoryItem s in Player.Inventory.Items)
            {
                if (s.Category == "Armor")
                {
                    comboBox1.Items.Add(s);
                }
            }
            comboBox1.Items.Add("");
            comboBox1.Items.Add("[Helmets]");
            comboBox1.Items.Add("");
            foreach (InventoryItem s in Player.Inventory.Items)
            {
                if (s.Category == "Helm")
                {
                    comboBox1.Items.Add(s);
                }
            }
            comboBox1.Items.Add("");
            comboBox1.Items.Add("[Capes]");
            comboBox1.Items.Add("");
            foreach (InventoryItem s in Player.Inventory.Items)
            {
                if (s.Category == "Cape")
                {
                    comboBox1.Items.Add(s);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save Set";
            if(!Directory.Exists(Application.StartupPath + "\\Sets"))
                Directory.CreateDirectory(Application.StartupPath + "\\Sets");
            saveFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "Sets");
            saveFileDialog1.DefaultExt = ".gset";
            saveFileDialog1.Filter = "Grimoire sets|*.gset";
            saveFileDialog1.CheckFileExists = false;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SetItem d = GenerateConfig();
                try
                {
                    File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(d, Formatting.Indented, _serializerSettings));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to save bot: " + ex.Message);
                }
            }
        }

        private bool TryDeserialize(string json, out SetItem config)
        {
            try
            {
                config = JsonConvert.DeserializeObject<SetItem>(json);
                return true;
            }
            catch
            {
            }
            config = null;
            return false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Load Set";
            if (!Directory.Exists(Application.StartupPath + "\\Sets"))
                Directory.CreateDirectory(Application.StartupPath + "\\Sets");
            openFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "Sets");
            openFileDialog1.DefaultExt = ".gset";
            openFileDialog1.Filter = "Grimoire sets|*.gset";
            if (openFileDialog1.ShowDialog() == DialogResult.OK && TryDeserialize(File.ReadAllText(openFileDialog1.FileName), out SetItem config))
            {
                ApplyConfig(config);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "" || comboBox1.SelectedItem.ToString().Contains("["))
            {
                comboBox1.SelectedItem = comboBox1.SelectedIndex++;
            }
        }

        private void AddSetItem(ISetInterface cmd)
        {
            listBox1.Items.Add(cmd);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }

    public interface ISetInterface
    {
        
    }

    public class SetItem
    {
        public List<ISetInterface> Set = new List<ISetInterface>();
    }

    public class Item : ISetInterface
    {
        public string Type { get; set; }

        public string Name  { get; set; }

        public override string ToString()
        {
            return $"[{Type}] : {Name}";
        }
    }
}
