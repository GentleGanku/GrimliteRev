using DarkUI.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Grimoire.UI
{
    public partial class AboutForm : DarkForm
    {
        public AboutForm()
        {
            InitializeComponent();
            if (!Program.access)
                darkLabel7.Text = "(OUTDATED)";
            else
                darkLabel7.Text = $"({Program.versionDate})";
        }

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.FormOwnerClosing)
            {
                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;
                e.Cancel = true;
                Hide();
            }
        }

        private void pbCatGithub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/0zl");
        }

        private void pbsatanGithub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/wispie");
        }

        private void pbBineyMPGH_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.mpgh.net/forum/member.php?u=4062680");
        }

        private void pbEmperorMPGH_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.mpgh.net/forum/member.php?u=2374072");
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
        }

        private void AboutForm_Shown(object sender, EventArgs e)
        {
            Program.CheckVersion();
            Root.SetDRM(Instance, !Root.Instance.enableCapture);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://paypal.me/GentleGanku");
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void rtbCredits_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/GentleGanku");
        }
    }
}
