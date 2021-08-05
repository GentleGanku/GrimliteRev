using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Networking;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class PacketLogger : DarkForm
    {
        private IContainer components;

        private DarkTextBox txtPackets;

        private DarkButton btnStart;

        private DarkButton btnStop;

        private DarkButton btnCopy;
        private TableLayoutPanel tableLayoutPanel1;
        private DarkButton btnClear;

        public static PacketLogger Instance
        {
            get;
        } = new PacketLogger();

        private PacketLogger()
        {
            InitializeComponent();
        }

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
            Proxy.Instance.ReceivedFromClient -= PacketCaptured;
            btnStart.Enabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            Proxy.Instance.ReceivedFromClient += PacketCaptured;
        }

        private void PacketCaptured(Networking.Message msg)
        {
            if (msg.RawContent != null && msg.RawContent.Contains("%xt%zm%"))
            {
                txtPackets.Invoke((Action)delegate
                {
                    txtPackets.AppendText(msg.RawContent + Environment.NewLine);
                });
            }
        }

        private void PacketLogger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
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
            this.txtPackets = new DarkUI.Controls.DarkTextBox();
            this.btnStart = new DarkUI.Controls.DarkButton();
            this.btnStop = new DarkUI.Controls.DarkButton();
            this.btnCopy = new DarkUI.Controls.DarkButton();
            this.btnClear = new DarkUI.Controls.DarkButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPackets
            // 
            this.txtPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPackets.Location = new System.Drawing.Point(12, 12);
            this.txtPackets.MaxLength = 2147483647;
            this.txtPackets.Multiline = true;
            this.txtPackets.Name = "txtPackets";
            this.txtPackets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPackets.Size = new System.Drawing.Size(422, 244);
            this.txtPackets.TabIndex = 15;
            // 
            // btnStart
            // 
            this.btnStart.Checked = false;
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Location = new System.Drawing.Point(318, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(101, 24);
            this.btnStart.TabIndex = 16;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Checked = false;
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Location = new System.Drawing.Point(213, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(99, 24);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Checked = false;
            this.btnCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopy.Location = new System.Drawing.Point(108, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(99, 24);
            this.btnCopy.TabIndex = 18;
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnClear
            // 
            this.btnClear.Checked = false;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Location = new System.Drawing.Point(3, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(99, 24);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStart, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStop, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCopy, 1, 0);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 259);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(422, 30);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // PacketLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(451, 291);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.txtPackets);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.MinimizeBox = false;
            this.Name = "PacketLogger";
            this.Text = "Packet logger";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketLogger_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}