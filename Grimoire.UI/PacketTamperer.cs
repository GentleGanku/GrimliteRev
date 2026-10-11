using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Game;
using Grimoire.Networking;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class PacketTamperer : DarkForm
    {
        private IContainer components;

        private RichTextBox txtSend;

        public RichTextBox txtReceive;

        private DarkButton btnToServer;

        public DarkCheckBox chkCapturePackets;
        private SplitContainer splitContainer1;
        private DarkPanel panel1;
        private DarkButton btnToClient;
        private DarkButton btnClearCaptured;
        private TableLayoutPanel tableLayoutPanel1;

        public static PacketTamperer Instance
        {
            get;
        } = new PacketTamperer();

        private PacketTamperer()
        {
            InitializeComponent();
        }

        private void PacketTamperer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
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
            this.txtSend = new System.Windows.Forms.RichTextBox();
            this.txtReceive = new System.Windows.Forms.RichTextBox();
            this.btnToServer = new DarkUI.Controls.DarkButton();
            this.chkCapturePackets = new DarkUI.Controls.DarkCheckBox();
            this.btnToClient = new DarkUI.Controls.DarkButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new DarkUI.Controls.DarkPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClearCaptured = new DarkUI.Controls.DarkButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSend
            // 
            this.txtSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.txtSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSend.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtSend.Location = new System.Drawing.Point(0, 0);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(536, 100);
            this.txtSend.TabIndex = 0;
            this.txtSend.Text = "";
            // 
            // txtReceive
            // 
            this.txtReceive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.txtReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceive.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtReceive.Location = new System.Drawing.Point(0, 0);
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(536, 228);
            this.txtReceive.TabIndex = 1;
            this.txtReceive.Text = "";
            // 
            // btnToServer
            // 
            this.btnToServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnToServer.BackColorUseGeneric = false;
            this.btnToServer.Checked = false;
            this.btnToServer.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnToServer.Location = new System.Drawing.Point(258, 3);
            this.btnToServer.Name = "btnToServer";
            this.btnToServer.Size = new System.Drawing.Size(88, 23);
            this.btnToServer.TabIndex = 3;
            this.btnToServer.Text = "Send to server";
            this.btnToServer.Click += new System.EventHandler(this.btnToServer_Click);
            // 
            // chkCapturePackets
            // 
            this.chkCapturePackets.AutoSize = true;
            this.chkCapturePackets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCapturePackets.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkCapturePackets.Location = new System.Drawing.Point(0, 0);
            this.chkCapturePackets.Name = "chkCapturePackets";
            this.chkCapturePackets.Size = new System.Drawing.Size(124, 23);
            this.chkCapturePackets.TabIndex = 5;
            this.chkCapturePackets.Text = "Capture packets";
            this.chkCapturePackets.CheckedChanged += new System.EventHandler(this.chkCapturePackets_CheckedChanged);
            // 
            // btnToClient
            // 
            this.btnToClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnToClient.BackColorUseGeneric = false;
            this.btnToClient.Checked = false;
            this.btnToClient.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnToClient.Location = new System.Drawing.Point(352, 3);
            this.btnToClient.Name = "btnToClient";
            this.btnToClient.Size = new System.Drawing.Size(87, 23);
            this.btnToClient.TabIndex = 6;
            this.btnToClient.Text = "Send to client";
            this.btnToClient.Click += new System.EventHandler(this.btnToClient_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(16, 41);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtSend);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtReceive);
            this.splitContainer1.Size = new System.Drawing.Size(536, 332);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkCapturePackets);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(124, 23);
            this.panel1.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClearCaptured, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnToClient, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnToServer, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(536, 29);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // btnClearCaptured
            // 
            this.btnClearCaptured.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearCaptured.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnClearCaptured.BackColorUseGeneric = false;
            this.btnClearCaptured.Checked = false;
            this.btnClearCaptured.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnClearCaptured.Location = new System.Drawing.Point(445, 3);
            this.btnClearCaptured.Name = "btnClearCaptured";
            this.btnClearCaptured.Size = new System.Drawing.Size(88, 23);
            this.btnClearCaptured.TabIndex = 11;
            this.btnClearCaptured.Text = "Clear captured";
            this.btnClearCaptured.Click += new System.EventHandler(this.btnClearCaptured_Click);
            // 
            // PacketTamperer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(561, 385);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.Name = "PacketTamperer";
            this.Text = "Packet Tamperer";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketTamperer_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}