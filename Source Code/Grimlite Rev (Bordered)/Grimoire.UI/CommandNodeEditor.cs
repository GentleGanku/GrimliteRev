using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class NodeEditor : Form
    {
        public class TypedCommand
        {
            public string Name;
            public Type CommandType;
            public override string ToString()
            {
                return Name;
            }
        }

        public NodeEditor()
        {
            InitializeComponent();
        }

        public Type lb_item = null;

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lb_item = null;

            if (listBox1.Items.Count == 0)
            {
                return;
            }

            int index = listBox1.IndexFromPoint(e.X, e.Y);
            TypedCommand item = listBox1.Items[index] as TypedCommand;
            DragDropEffects dde1 = DoDragDrop(item.CommandType, DragDropEffects.All);
        }

        private void listBox1_DragLeave(object sender, EventArgs e)
        {
            MessageBox.Show(JsonConvert.SerializeObject(listBox1.SelectedItem));
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            lb_item = null;
        }

        public static NodeEditor Instance = new NodeEditor();

        private void Form1_Load(object sender, EventArgs e)
        {
            var classes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(Grimoire.Botting.IBotCommand).IsAssignableFrom(p) && p.GetProperties().Length > 0);

            foreach (var c in classes)
            {
                var cmd = new TypedCommand();
                cmd.Name = c.Name;
                cmd.CommandType = c;

                listBox1.Items.Add(cmd);
            }

            return;

            int y = 30;

            foreach (var obj in classes) {
                var node = new CommandNode();

                var properties = obj.GetType().GetProperties();
                var propList = properties.ToList();

                node.Name = obj.Name;

                foreach (var prop in propList)
                {
                    y += 30;
                    var label = new Label();
                    label.Location = new Point(13, y);
                    label.Text = prop.Name;

                    var textBox = new TextBox();
                    textBox.Location = new Point(43, y);
                    textBox.Text = "text for " + prop.Name + " here";

                    node.Controls.Add(textBox);
                    node.Controls.Add(label);
                }

                this.splitContainer1.Panel2.Controls.Add(node);
            }
        }

        

        private void splitContainer1_Panel2_DragDrop(object sender, DragEventArgs e)
        {
            if (lb_item == null) return;

            var node = new CommandNode();
            var obj = lb_item;

            //var type = Activator.CreateInstance(obj);

            var properties = obj.GetProperties();
            var propList = properties.ToList();

            var y = 13;
            foreach (var prop in propList)
            {
                var label = new Label();
                label.Location = new Point(13, y);
                label.Text = prop.Name;

                var textBox = new TextBox();
                textBox.Location = new Point(73, y);
                textBox.Text = "text for " + prop.Name + " here";

                y += 30;

                node.Controls.Add(textBox);
                node.Controls.Add(label);
            }
            y += 13;

            node.Size = new Size(200, y);
            node.activeControl = panel1;
            node.previousPosition = new Point(e.X, e.Y);
            panel1.Controls.Add(node);

            lb_item = null;

        }

        private void splitContainer1_Panel2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
            lb_item = ((TypedCommand)listBox1.SelectedItem).CommandType;
        }
    }
}
