using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using Grimoire.Botting;
using static Unity3.Eyedropper.EyeDropper;

namespace Grimoire.UI
{
    public partial class EyeDropper : DarkForm
    {
        public EyeDropper()
        {
            InitializeComponent();
        }

        public static EyeDropper Instance = new EyeDropper();

        private void eyeDropper1_ScreenCaptured(Bitmap capturedPixels, Color capturedColor)
        {
            string color = string.Format(Environment.NewLine + "#{0} / {1}, {2}, {3} / #{4}",
                eyeDropper1.SelectedColor.ToArgb().ToString("X2"),
                eyeDropper1.SelectedColor.R,
                eyeDropper1.SelectedColor.G,
                eyeDropper1.SelectedColor.B,
                eyeDropper1.SelectedColor.ToArgb().ToString("X2").Substring(2));
            textBox1.BackColor = eyeDropper1.SelectedColor;
            textBox1.ForeColor = textBox1.BackColor.GetBrightness() > 0.7 ? Color.Black : Color.White;
            textBox1.Text = color;
        }

        private void eyeDropper1_EndScreenCapture(object sender, EventArgs e)
        {
            string color = string.Format("#{0} / {1}, {2}, {3} / #{4}\n", 
                eyeDropper1.SelectedColor.ToArgb().ToString("X2"), 
                eyeDropper1.SelectedColor.R, 
                eyeDropper1.SelectedColor.G, 
                eyeDropper1.SelectedColor.B,
                eyeDropper1.SelectedColor.ToArgb().ToString("X2").Substring(2));
            richTextBox1.AppendText(color, eyeDropper1.SelectedColor);
        }
    }
}
