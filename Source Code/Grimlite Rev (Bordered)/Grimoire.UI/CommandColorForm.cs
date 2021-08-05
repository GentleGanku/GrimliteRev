using DarkUI.Forms;
using Grimoire.Botting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class CommandColorForm : DarkForm
    {
        public CommandColorForm()
        {
            InitializeComponent();
        }

        private void btnLabelColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                string x = comboBox1.SelectedItem.ToString().Replace("Cmd", "") + "Color";
                c.Set(x, colorDialog1.Color.ToArgb().ToString("X"));
                c.Save();
                Dictionary<string, Color> allColors = BotManager.Instance.CurrentColors;
                if (allColors.ContainsKey(x))
                    allColors[x] = colorDialog1.Color;
            }
        }

        private void CommandColorForm_Load(object sender, EventArgs e)
        {
            var type = typeof(IBotCommand); // Get the type of our interface
            var types = AppDomain.CurrentDomain.GetAssemblies() // Get the assemblies associated with our project
                .SelectMany(s => s.GetTypes()) // Get all the types
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface); // Filter to find any type that can be assigned to an IModule

            var typeList = types as Type[] ?? types.ToArray(); // Convert to an array
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Index");
            comboBox1.Items.Add("Variable");
            comboBox1.Items.Add("ExtendedVariable");
            foreach (var t in typeList)
            {
                var i = t.ToString().Split('.');
                comboBox1.Items.Add(i[i.Count() - 1]);
            }
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string font = c.Get("font");
            //float fontSize = float.Parse(Config.Load(Application.StartupPath + "\\config.cfg").Get("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            if (font != null)
                this.Font = new Font(font, 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            trackBar1.Value = int.Parse(c.Get("lstCommandsFontSize") ?? "60");

        }

        private int GetColor(Control ctr)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string ctrName = ctr.Name.ToString().Remove(0, 3);
            string WindowText = SystemColors.WindowText.ToArgb().ToString("X");
            try
            {
                return int.Parse(c.Get(ctrName) ?? WindowText, System.Globalization.NumberStyles.HexNumber);
            }
            catch { }
            return int.Parse(SystemColors.WindowText.ToString());
        }

        private int GetColor(string ctr)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string WindowText = SystemColors.WindowText.ToArgb().ToString("X");
            try
            {
                return int.Parse(c.Get(ctr) ?? WindowText, System.Globalization.NumberStyles.HexNumber);
            }
            catch { }
            return int.Parse(SystemColors.WindowText.ToString());
        }


        private bool GetChecked(CheckBox ctr)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string ctrName = ctr.Name.ToString().Remove(0, 3);
            try
            {
                return bool.Parse(c.Get(ctrName + "Centered") ?? "false");
            }
            catch { }
            return false;
        }

        private bool GetChecked(string ctr)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            try
            {
                return bool.Parse(c.Get(ctr + "Centered") ?? "false");
            }
            catch { }
            return false;
        }

        private void CommandColorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void chkLabelCentered_CheckedChanged(object sender, EventArgs e)
        {
            string x = "";
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            switch (((CheckBox)sender).Name.Replace("chk", ""))
            {
                case "Label": x = "LabelCentered";
                    break;
                case "Kill": x = "KillCentered";
                    break;
                case "Index": x = "IndexCentered";
                    break;
            }
            c.Set(x, ((CheckBox)sender).Checked.ToString());
            c.Save();
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (!(e.Index > -1))
                return;
            int Index = e.Index;// + 1;
            try
            {
                e.DrawBackground();
                SolidBrush color = new SolidBrush(BotManager.Instance.GetCurrentColor(comboBox1.Items[Index].ToString().Replace("Cmd", "") + "Color"));
                e.Graphics.DrawString(comboBox1.Items[Index].ToString(), this.Font, color, e.Bounds);
            }
            catch
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                checkBox1.Checked = GetChecked(comboBox1.Items[comboBox1.SelectedIndex].ToString().Replace("Cmd", ""));
            }
            catch
            {

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string x = comboBox1.SelectedItem.ToString().Replace("Cmd", "") + "Centered";
            c.Set(x, ((CheckBox)sender).Checked.ToString());
            c.Save();
            Dictionary<string, bool> allCentered = BotManager.Instance.CurrentCentered;
            if (allCentered.ContainsKey(x))
                allCentered[x] = ((CheckBox)sender).Checked;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                BotManager.Instance.lstCommands.ItemHeight = trackBar1.Value / 4;
                BotManager.Instance.lstCommands.Font = new Font(BotManager.Instance.lstCommands.Font.FontFamily, BotManager.Instance.lstCommands.ItemHeight - (float)6.5, FontStyle.Regular);
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            c.Set("CommandsSize", trackBar1.Value.ToString());
        }

        private void btnRandomColors_Click(object sender, EventArgs e)
        {
            var type = typeof(IBotCommand); // Get the type of our interface
            var types = AppDomain.CurrentDomain.GetAssemblies() // Get the assemblies associated with our project
                .SelectMany(s => s.GetTypes()) // Get all the types
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface); // Filter to find any type that can be assigned to an IModule

            var typeList = types as Type[] ?? types.ToArray(); // Convert to an array
            comboBox1.Items.Clear();
            var random = new Random(); // Make sure this is out of the loop!
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            //foreach (var t in typeList)
            //{
            //    var i = t.ToString().Split('.');
            //    var item = i[i.Count() - 1];
            //    string x = item.Replace("Cmd", "") + "Color";
            //    c.Set(x, string.Format("FF{0:X6}", random.Next(0x1000000)));
            //    c.Save();
            //}
            //int mixR = random.Next(254);
            //int mixG = random.Next(254);
            //int mixB = random.Next(254);
            int mixR = 255;
            int mixG = 255;
            int mixB = 255;
            if (string.IsNullOrEmpty(txtRGB.Text))
                txtRGB.Text = "255, 255, 255";
            try
            {
                if (txtRGB.Text.Contains("#"))
                {
                    Color color = ColorTranslator.FromHtml(txtRGB.Text);
                    mixR = Convert.ToInt16(color.R);
                    mixG = Convert.ToInt16(color.G);
                    mixB = Convert.ToInt16(color.B);
                }
                else if (txtRGB.Text.Contains(","))
                {
                    try
                    {
                        string[] textRGB = txtRGB.Text.Split(',');
                        int.TryParse(textRGB[0], out mixR);
                        int.TryParse(textRGB[1], out mixG);
                        int.TryParse(textRGB[2], out mixB);
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception E)
            {

            }
            foreach (var t in typeList)
            {
                var i = t.ToString().Split('.');
                var item = i[i.Count() - 1];
                string x = item.Replace("Cmd", "") + "Color";
                //c.Set(x, Extensions.generatePastelHex(random, mixR, mixG, mixB));
                c.Set(x, Extensions.generatePastelHex(random, mixR, mixG, mixB));
            }
            c.Save();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var type = typeof(IBotCommand); // Get the type of our interface
            var types = AppDomain.CurrentDomain.GetAssemblies() // Get the assemblies associated with our project
                .SelectMany(s => s.GetTypes()) // Get all the types
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface); // Filter to find any type that can be assigned to an IModule

            var typeList = types as Type[] ?? types.ToArray(); // Convert to an array
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Index");
            comboBox1.Items.Add("Variable");
            comboBox1.Items.Add("ExtendedVariable");
            foreach (var t in typeList)
            {
                var i = t.ToString().Split('.');
                comboBox1.Items.Add(i[i.Count() - 1]);
            }
        }

        private void btnReloadColors_Click(object sender, EventArgs e)
        {
            BotManager.Instance.CurrentCentered.Clear();
            BotManager.Instance.CurrentColors.Clear();
        }
    }
}
