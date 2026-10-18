using DarkUI.Forms;
using Grimoire.Networking;
using System;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class PacketLogger : DarkForm
    {
        #region Constructors

        private PacketLogger()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public static PacketLogger Instance
        {
            get;
        } = new PacketLogger();

        #endregion

        #region Methods

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPackets.Clear();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtPackets.Text.Length > 0)
            {
                Clipboard.SetText(txtPackets.Text);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Proxy.Instance._catchXtPackets = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Proxy.Instance._catchXtPackets = true;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void txtPackets_TextChanged(object sender, EventArgs e)
        {
            bool hasText = txtPackets.TextLength > 0;
            btnClear.Enabled = hasText;
            btnCopy.Enabled = hasText;
        }

        private void PacketLogger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.FormOwnerClosing)
            {
                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;
                e.Cancel = true;
                Hide();
            }
        }

        #endregion
    }
}