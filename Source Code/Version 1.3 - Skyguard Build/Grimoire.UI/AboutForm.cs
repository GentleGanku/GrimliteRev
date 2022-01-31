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
        }

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
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
        {}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://paypal.me/GentleGanku");
        }
    }
}
