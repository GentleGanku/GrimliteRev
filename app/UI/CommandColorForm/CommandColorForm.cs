using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class CommandColorForm : DarkForm
    {
        #region Constructors

        public CommandColorForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void btnLabelColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                string x = comboBox1.SelectedItem.ToString().Replace("Cmd", "") + "Color";
                Config.Instance.SetValue(x, colorDialog1.Color.ToArgb().ToString("X"));
                Dictionary<string, Color> allColors = BotManager.Instance.CurrentColors;
                if (allColors.ContainsKey(x))
                {
                    allColors[x] = colorDialog1.Color;
                }
            }
        }

        private void CommandColorForm_Load(object sender, EventArgs e)
        {
            var type = typeof(IBotCommand);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            var typeList = types as Type[] ?? types.ToArray();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Index");
            comboBox1.Items.Add("Variable");
            comboBox1.Items.Add("ExtendedVariable");
            foreach (var t in typeList)
            {
                var i = t.ToString().Split('.');
                comboBox1.Items.Add(i[i.Count() - 1]);
            }
            string font = Config.Instance.GetValue<string>("font");
            if (font != null)
            {
                this.Font = new Font(font, 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
            trackBar1.Value = int.Parse(Config.Instance.GetValue<string>("lstCommandsFontSize") ?? "60");

        }

        /// <summary>
        /// Returns the saved color for <paramref name="ctr"/>, or the window text color when there is no valid saved
        /// one.
        /// </summary>
        private int GetColor(Control ctr)
        {
            string ctrName = ctr.Name.ToString().Remove(0, 3);
            string WindowText = SystemColors.WindowText.ToArgb().ToString("X");
            try
            {
                return int.Parse(Config.Instance.GetValue<string>(ctrName) ?? WindowText, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
            }
            return int.Parse(SystemColors.WindowText.ToString());
        }

        /// <summary>
        /// Returns the saved color for <paramref name="ctr"/>, or the window text color when there is no valid saved
        /// one.
        /// </summary>
        private int GetColor(string ctr)
        {
            string WindowText = SystemColors.WindowText.ToArgb().ToString("X");
            try
            {
                return int.Parse(Config.Instance.GetValue<string>(ctr) ?? WindowText, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
            }
            return int.Parse(SystemColors.WindowText.ToString());
        }

        private bool GetChecked(CheckBox ctr)
        {
            string ctrName = ctr.Name.ToString().Remove(0, 3);
            return bool.Parse(Config.Instance.GetValue<string>(ctrName + "Centered") ?? "false");
        }

        private bool GetChecked(string ctr)
        {
            return bool.Parse(Config.Instance.GetValue<string>(ctr + "Centered") ?? "false");
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
            switch (((CheckBox)sender).Name.Replace("chk", ""))
            {
                case "Label":
                    x = "LabelCentered";
                    break;
                case "Kill":
                    x = "KillCentered";
                    break;
                case "Index":
                    x = "IndexCentered";
                    break;
            }
            Config.Instance.SetValue(x, ((CheckBox)sender).Checked.ToString());
        }

        /// <summary>
        /// Draws a command name in its custom color. Painting must not throw, so the item is drawn without its custom
        /// color when that color is bad.
        /// </summary>
        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (!(e.Index > -1))
                return;
            int Index = e.Index;
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
            catch (Exception ex)
            {
                ErrorReporter.Log("reading the saved command color setting", ex);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string x = comboBox1.SelectedItem.ToString().Replace("Cmd", "") + "Centered";
            Config.Instance.SetValue(x, ((CheckBox)sender).Checked.ToString());
            Dictionary<string, bool> allCentered = BotManager.Instance.CurrentCentered;
            if (allCentered.ContainsKey(x))
            {
                allCentered[x] = ((CheckBox)sender).Checked;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                BotManager.Instance.lstCommands.ItemHeight = trackBar1.Value / 4;
                BotManager.Instance.lstCommands.Font = new Font(BotManager.Instance.lstCommands.Font.FontFamily, BotManager.Instance.lstCommands.ItemHeight - (float)6.5, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("resizing the command list", ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Config.Instance.SetValue("CommandsSize", trackBar1.Value.ToString());
        }

        /// <summary>
        /// Gives every command type a random color. One Random is used for the whole loop, because Random instances
        /// created in quick succession share a seed. With fewer than three RGB values the missing ones stay at 255, and
        /// an invalid value leaves the mix white.
        /// </summary>
        private void btnRandomColors_Click(object sender, EventArgs e)
        {
            var type = typeof(IBotCommand);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            var typeList = types as Type[] ?? types.ToArray();
            comboBox1.Items.Clear();
            var random = new Random();
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
                Config.Instance.SetValue(x, Extensions.generatePastelHex(random, mixR, mixG, mixB));
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var type = typeof(IBotCommand);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            var typeList = types as Type[] ?? types.ToArray();
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

        #endregion
    }
}
