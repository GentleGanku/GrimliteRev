using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Grimoire.UI
{
    public class PacketSpammer : DarkForm
    {
        private IContainer components;
        public DarkListBox lstPackets;

        public DarkTextBox txtPacket;

        public DarkButton btnAdd;

        public DarkButton btnClear;

        public DarkButton btnLoad;

        public DarkButton btnSave;

        public DarkButton btnStartAndStop;

        public DarkNumericUpDown numDelay;

        private DarkButton btnSend;

        private TableLayoutPanel tableLayoutPanel1;

        public DarkButton btnRemove;

        public static PacketSpammer Instance
        {
            get;
        } = new PacketSpammer();

        public PacketSpammer()
        {
            InitializeComponent();
        }

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

        private void PacketSpammer_Shown(object sender, EventArgs e)
        {
            Root.SetDRM(Instance, !Root.Instance.enableCapture);
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstPackets.SelectedIndex;
            if (selectedIndex > -1)
            {
                lstPackets.Items.RemoveAt(selectedIndex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstPackets = new DarkUI.Controls.DarkListBox(this.components);
            this.txtPacket = new DarkUI.Controls.DarkTextBox();
            this.btnAdd = new DarkUI.Controls.DarkButton();
            this.btnClear = new DarkUI.Controls.DarkButton();
            this.btnLoad = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnStartAndStop = new DarkUI.Controls.DarkButton();
            this.numDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.btnSend = new DarkUI.Controls.DarkButton();
            this.btnRemove = new DarkUI.Controls.DarkButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstPackets
            // 
            this.lstPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPackets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstPackets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPackets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstPackets.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPackets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lstPackets.FormattingEnabled = true;
            this.lstPackets.ItemHeight = 18;
            this.lstPackets.Location = new System.Drawing.Point(14, 12);
            this.lstPackets.Name = "lstPackets";
            this.lstPackets.Size = new System.Drawing.Size(316, 128);
            this.lstPackets.TabIndex = 0;
            // 
            // txtPacket
            // 
            this.txtPacket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPacket.Location = new System.Drawing.Point(14, 144);
            this.txtPacket.Name = "txtPacket";
            this.txtPacket.Size = new System.Drawing.Size(316, 20);
            this.txtPacket.TabIndex = 27;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAdd.BackColorUseGeneric = false;
            this.btnAdd.Checked = false;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(89, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 20);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnClear.BackColorUseGeneric = false;
            this.btnClear.Checked = false;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Location = new System.Drawing.Point(243, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(76, 20);
            this.btnClear.TabIndex = 29;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoad.BackColorUseGeneric = false;
            this.btnLoad.Checked = false;
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoad.Location = new System.Drawing.Point(243, 29);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(76, 22);
            this.btnLoad.TabIndex = 30;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSave.BackColorUseGeneric = false;
            this.btnSave.Checked = false;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(163, 29);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 22);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStartAndStop
            // 
            this.btnStartAndStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnStartAndStop.BackColorUseGeneric = false;
            this.btnStartAndStop.Checked = false;
            this.btnStartAndStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartAndStop.Location = new System.Drawing.Point(89, 29);
            this.btnStartAndStop.Name = "btnStartAndStop";
            this.btnStartAndStop.Size = new System.Drawing.Size(68, 22);
            this.btnStartAndStop.TabIndex = 32;
            this.btnStartAndStop.Text = "Start";
            this.btnStartAndStop.Click += new System.EventHandler(this.btnStartAndStop_Click);
            // 
            // numDelay
            // 
            this.numDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numDelay.Location = new System.Drawing.Point(3, 3);
            this.numDelay.LoopValues = false;
            this.numDelay.Maximum = new decimal(new int[] {
            61000,
            0,
            0,
            0});
            this.numDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(80, 20);
            this.numDelay.TabIndex = 34;
            this.numDelay.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSend.BackColorUseGeneric = false;
            this.btnSend.Checked = false;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Location = new System.Drawing.Point(3, 29);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 22);
            this.btnSend.TabIndex = 35;
            this.btnSend.Text = "Send once";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnRemove.BackColorUseGeneric = false;
            this.btnRemove.Checked = false;
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemove.Location = new System.Drawing.Point(163, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(74, 20);
            this.btnRemove.TabIndex = 36;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.numDelay, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRemove, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnLoad, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSend, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnStartAndStop, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 165);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.14815F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.85185F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(322, 54);
            this.tableLayoutPanel1.TabIndex = 37;
            // 
            // PacketSpammer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(346, 230);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.txtPacket);
            this.Controls.Add(this.lstPackets);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.Name = "PacketSpammer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Packet Spammer";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketSpammer_FormClosing);
            this.Shown += new System.EventHandler(this.PacketSpammer_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}