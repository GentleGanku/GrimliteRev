using DarkUI.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class AboutForm : DarkForm
    {
        public AboutForm()
        {
            InitializeComponent();
            lblUpdate.Text = $"v{Program.version} ({Program.releaseDate})";
        }

        private async void AboutForm_Shown(object sender, EventArgs e)
        {
            await Program.CheckForLatestBuild();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://ko-fi.com/gentleganku");
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
