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
        #region Fields

        private bool _switching;

        #endregion

        #region Constructors

        public PacketSpammer()
        {
            InitializeComponent();
            UpdateButtons();
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
            UpdateButtons();
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtPacket.Text.Length > 0)
            {
                lstPackets.Items.Add(txtPacket.Text);
                txtPacket.Clear();
                UpdateButtons();
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
            UpdateButtons();
        }

        public async void btnStartAndStop_Click(object sender, EventArgs e)
        {
            if (btnStartAndStop.Text == "Start" && lstPackets.Items.Count > 0 && Player.IsLoggedIn && Player.IsAlive)
            {
                btnStartAndStop.Text = "Stop";
                _switching = true;
                UpdateButtons();
                List<string> packets = lstPackets.Items.Cast<string>().ToList();
                int delay = (int)numDelay.Value;
                Spammer.Instance.IndexChanged += IndexChanged;
                Spammer.Instance.Start(packets, delay);
                await Task.Delay(2000);
                _switching = false;
                UpdateButtons();
            }
            else if (btnStartAndStop.Text == "Stop" && lstPackets.Items.Count > 0)
            {
                btnStartAndStop.Text = "Start";
                _switching = true;
                Spammer.Instance.Stop();
                Spammer.Instance.IndexChanged -= IndexChanged;
                UpdateButtons();
                await Task.Delay(2000);
                _switching = false;
                UpdateButtons();
            }
        }

        /// <summary>
        /// Sends the packet selected in the list once.
        /// </summary>
        public async void btnSend_Click(object sender, EventArgs e)
        {
            if (lstPackets.SelectedIndex > -1 && Player.IsLoggedIn && Player.IsAlive)
            {
                btnSend.Enabled = false;
                await Proxy.Instance.SendToServer((string)lstPackets.SelectedItem);
                UpdateButtons();
            }
        }

        /// <summary>
        /// Enables each button only when it can act. Remove and Send Once need a selected packet; Clear, Start and Save
        /// need packets in the list. While the spammer runs the list can't be changed, and Start/Stop stays disabled for
        /// the two seconds after a click.
        /// </summary>
        internal void UpdateButtons()
        {
            bool running = btnStartAndStop.Text == "Stop";
            bool selected = lstPackets.SelectedIndex > -1;
            bool hasPackets = lstPackets.Items.Count > 0;
            btnAdd.Enabled = !running;
            btnLoad.Enabled = !running;
            btnRemove.Enabled = !running && selected;
            btnClear.Enabled = !running && hasPackets;
            btnSend.Enabled = selected;
            btnSave.Enabled = hasPackets;
            btnStartAndStop.Enabled = !_switching && (running || hasPackets);
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
            UpdateButtons();
        }

        private void lstPackets_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        #endregion
    }
}