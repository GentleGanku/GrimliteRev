using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;

namespace Grimoire.UI
{
    public partial class AboutForm : DarkForm
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://paypal.me/wispsatan");
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
        {
            bool latest = false;
            //int version = version whatever  
            if (latest)
            {
                //lblUpdate = $"Grimlite is up to date ({version})"
            }
            else
            {
                //int latestVersion = latest version or whatever
                //bool latestStatus
                //lblUpdate = $"Latest {latestVersion}, status: {latestStatus ? "Released" : "Unreleased"}"
                
                // I'll handle the Released or Unreleased with color (green = released, red = unreleased)
            }

        }

        private void rtbCredits_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
