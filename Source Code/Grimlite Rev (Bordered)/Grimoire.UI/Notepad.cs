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
using DarkUI.Forms;
using Grimoire.UI;

namespace Grimoire.UI
{
    public partial class Notepad : DarkForm
    {
        public Notepad()
        {
            InitializeComponent();
        }

        private void Notepad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private string font = System.Configuration.ConfigurationManager.AppSettings.Get("Font");

        private float? fontSize = float.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("FontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

        private void Notepad_Load(object sender, EventArgs e)
        {
            if (font != null && fontSize != null)
            {
                this.Font = new Font(font, (float)fontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
            this.richTextBox1.ContextMenuStrip = Context();
        }

        private ContextMenuStrip Context()
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Font"
            };
            toolStripMenuItem.Click += delegate (object S, EventArgs E)
            {
                FontDialog fdlg = new FontDialog();
                fdlg.ShowDialog();
                if(richTextBox1.SelectedText == null)
                {
                    richTextBox1.Font = fdlg.Font;
                }
                else
                {
                    richTextBox1.SelectionFont = fdlg.Font;
                }
            };
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem
            {
                Text = "Color"
            };
            toolStripMenuItem1.Click += delegate (object S, EventArgs E)
            {
                ColorDialog cdlg = new ColorDialog();
                cdlg.ShowDialog();
                if (richTextBox1.SelectedText == null)
                {
                    richTextBox1.ForeColor = cdlg.Color;
                }
                else
                {
                    richTextBox1.SelectionColor = cdlg.Color;
                }
            };
            contextMenuStrip.Items.Add(toolStripMenuItem);
            contextMenuStrip.Items.Add(toolStripMenuItem1);
            return contextMenuStrip;
        }

        private void Notepad_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Notepad_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string fileLoc in files) // Code to read the contents of the text file
                {
                    if (File.Exists(fileLoc))
                        using (TextReader tr = new StreamReader(fileLoc))
                        {
                            this.richTextBox1.Text += $"\r\n{tr.ReadToEnd()}\r\n";
                        }
                }
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(ModifierKeys == Keys.Control && e.KeyCode == Keys.S)
            {
                string text = this.richTextBox1.Text;
                SaveFileDialog save = new SaveFileDialog
                {
                    Filter = "txt files (*.txt)|*.txt|gbot files (*.gbot)|*.gbot|All files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };
                if (save.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(save.FileName, text);
                }
            }
            if(e.KeyCode == Keys.Back && richTextBox1.Text.Length < 1)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
