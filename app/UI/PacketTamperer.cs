using DarkUI.Forms;
using Grimoire.Game;
using Grimoire.Networking;
using System;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class PacketTamperer : DarkForm
    {
        #region Constructors

        private PacketTamperer()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public static PacketTamperer Instance
        {
            get;
        } = new PacketTamperer();

        #endregion

        #region Methods

        private void PacketTamperer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.FormOwnerClosing)
            {
                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;
                e.Cancel = true;
                Hide();
            }
        }

        private void chkCapturePackets_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCapturePackets.Checked)
            {
                Proxy.Instance._catchAllPackets = true;
            }
            else
            {
                Proxy.Instance._catchAllPackets = false;
            }
        }

        private async void btnToClient_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSend.Text))
            {
                btnToClient.Enabled = false;
                bool json = txtSend.Text.StartsWith("{");
                World.SendClientPacket(txtSend.Text, json ? "json" : "str");
                btnToClient.Enabled = true;
            }
        }

        private async void btnToServer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSend.Text))
            {
                btnToServer.Enabled = false;
                bool json = txtSend.Text.StartsWith("{");
                World.SendPacket(txtSend.Text, json ? "Json" : "String");
                btnToServer.Enabled = true;
            }
        }

        private void btnClearCaptured_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
        }

        #endregion
    }
}