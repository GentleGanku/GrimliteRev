using DarkUI.Forms;
using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Grimoire.UI
{
    public partial class PacketSpammer : DarkForm
    {
        #region Constructors

        public PacketSpammer()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public static PacketSpammer Instance
        {
            get;
        } = new PacketSpammer();

        #endregion

        #region Methods

        public void btnClear_Click(object sender, EventArgs e)
        {
            lstPackets.Items.Clear();
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtPacket.Text.Length > 0)
            {
                lstPackets.Items.Add(txtPacket.Text);
                txtPacket.Clear();
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (lstPackets.Items.Count > 0)
            {
                SaveConfig();
            }
        }

        public void btnLoad_Click(object sender, EventArgs e)
        {
            lstPackets.Items.Clear();
            LoadConfig();
        }

        public async void btnStartAndStop_Click(object sender, EventArgs e)
        {
            if (btnStartAndStop.Text == "Start" && lstPackets.Items.Count > 0 && Player.IsLoggedIn && Player.IsAlive)
            {
                btnStartAndStop.Text = "Stop";
                btnStartAndStop.Enabled = false;
                SetButtonsEnabled(enabled: false);
                List<string> packets = lstPackets.Items.Cast<string>().ToList();
                int delay = (int)numDelay.Value;
                Spammer.Instance.IndexChanged += IndexChanged;
                Spammer.Instance.Start(packets, delay);
                await Task.Delay(2000);
                btnStartAndStop.Enabled = true;

            }
            else if (btnStartAndStop.Text == "Stop" && lstPackets.Items.Count > 0)
            {
                btnStartAndStop.Text = "Start";
                btnStartAndStop.Enabled = false;
                Spammer.Instance.Stop();
                Spammer.Instance.IndexChanged -= IndexChanged;
                SetButtonsEnabled(enabled: true);
                await Task.Delay(2000);
                btnStartAndStop.Enabled = true;
            }
        }

        public async void btnSend_Click(object sender, EventArgs e)
        {
            if (txtPacket.TextLength > 0 && Player.IsLoggedIn && Player.IsAlive)
            {
                btnSend.Enabled = false;
                await Proxy.Instance.SendToServer(txtPacket.Text);
                btnSend.Enabled = true;
            }
        }

        internal void SetButtonsEnabled(bool enabled)
        {
            btnAdd.Enabled = enabled;
            btnRemove.Enabled = enabled;
            btnClear.Enabled = enabled;
            btnLoad.Enabled = enabled;
        }

        internal void IndexChanged(int index)
        {
            lstPackets.Invoke((Action)delegate
            {
                lstPackets.SelectedIndex = index;
            });
        }

        private void PacketSpammer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.FormOwnerClosing)
            {
                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;
                e.Cancel = true;
                Hide();
            }
        }

        private void SaveConfig()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Save Spammer File";
                openFileDialog.Filter = "XML files|*.xml";
                openFileDialog.CheckFileExists = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(openFileDialog.FileName))
                    {
                        xmlWriter.WriteStartElement("spammer");
                        foreach (string item in lstPackets.Items)
                        {
                            xmlWriter.WriteElementString("packet", item);
                        }
                        xmlWriter.WriteElementString("author", "Author");
                        xmlWriter.WriteElementString("spamspeed", numDelay.Value.ToString());
                        xmlWriter.WriteEndElement();
                    }
                }
            }
        }

        private void LoadConfig()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load Spammer File";
                openFileDialog.Filter = "XML files|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (XElement item in XElement.Load(openFileDialog.FileName).Nodes())
                    {
                        if (item.Name == "packet")
                        {
                            lstPackets.Items.Add(item.Value);
                        }
                        else if (item.Name == "spamspeed")
                        {
                            decimal text = decimal.Parse(item.Name.ToString());
                            numDelay.Value = text;
                        }
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstPackets.SelectedIndex;
            if (selectedIndex > -1)
            {
                lstPackets.Items.RemoveAt(selectedIndex);
            }
        }

        #endregion
    }
}