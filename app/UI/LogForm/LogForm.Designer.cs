using DarkUI.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Grimoire.UI
{
    partial class LogForm
    {
        private IContainer components;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtLogDebug = new DarkUI.Controls.DarkTextBox();
            this.btnClear = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.tabLogs = new VisualStudioTabControl.VisualStudioTabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabLogDebug = new System.Windows.Forms.TabPage();
            this.tabLogScript = new System.Windows.Forms.TabPage();
            this.txtLogScript = new DarkUI.Controls.DarkTextBox();
            this.tabLogDrops = new System.Windows.Forms.TabPage();
            this.txtLogDrops = new DarkUI.Controls.DarkTextBox();
            this.tabLogChat = new System.Windows.Forms.TabPage();
            this.txtLogChat = new DarkUI.Controls.DarkTextBox();
            this.tabLogBot = new System.Windows.Forms.TabPage();
            this.txtLogBot = new DarkUI.Controls.DarkTextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabLogs.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabLogDebug.SuspendLayout();
            this.tabLogScript.SuspendLayout();
            this.tabLogDrops.SuspendLayout();
            this.tabLogChat.SuspendLayout();
            this.tabLogBot.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLogDebug
            // 
            this.txtLogDebug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogDebug.CausesValidation = false;
            this.txtLogDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogDebug.Location = new System.Drawing.Point(3, 3);
            this.txtLogDebug.Multiline = true;
            this.txtLogDebug.Name = "txtLogDebug";
            this.txtLogDebug.ReadOnly = true;
            this.txtLogDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogDebug.Size = new System.Drawing.Size(412, 211);
            this.txtLogDebug.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnClear.BackColorUseGeneric = false;
            this.btnClear.Checked = false;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Location = new System.Drawing.Point(0, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(207, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSave.BackColorUseGeneric = false;
            this.btnSave.Checked = false;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(207, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabLogs
            // 
            this.tabLogs.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.tabLogs.AllowDrop = true;
            this.tabLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabLogs.BackTabColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(61)))), ((int)(((byte)(78)))));
            this.tabLogs.ClosingButtonColor = System.Drawing.Color.WhiteSmoke;
            this.tabLogs.ClosingMessage = null;
            this.tabLogs.ContextMenuStrip = this.contextMenuStrip1;
            this.tabLogs.Controls.Add(this.tabLogDebug);
            this.tabLogs.Controls.Add(this.tabLogScript);
            this.tabLogs.Controls.Add(this.tabLogDrops);
            this.tabLogs.Controls.Add(this.tabLogChat);
            this.tabLogs.Controls.Add(this.tabLogBot);
            this.tabLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabLogs.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogs.HorizontalLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.tabLogs.HotTrack = true;
            this.tabLogs.ItemSize = new System.Drawing.Size(48, 16);
            this.tabLogs.Location = new System.Drawing.Point(0, 0);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.SelectedIndex = 0;
            this.tabLogs.SelectedTextColor = System.Drawing.Color.White;
            this.tabLogs.ShowClosingButton = false;
            this.tabLogs.ShowClosingMessage = false;
            this.tabLogs.Size = new System.Drawing.Size(428, 243);
            this.tabLogs.TabIndex = 3;
            this.tabLogs.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFontToolStripMenuItem,
            this.changeColorToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(148, 48);
            // 
            // changeFontToolStripMenuItem
            // 
            this.changeFontToolStripMenuItem.Name = "changeFontToolStripMenuItem";
            this.changeFontToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.changeFontToolStripMenuItem.Text = "Change Font";
            this.changeFontToolStripMenuItem.Click += new System.EventHandler(this.changeFontToolStripMenuItem_Click);
            // 
            // changeColorToolStripMenuItem
            // 
            this.changeColorToolStripMenuItem.Name = "changeColorToolStripMenuItem";
            this.changeColorToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.changeColorToolStripMenuItem.Text = "Change Color";
            this.changeColorToolStripMenuItem.Click += new System.EventHandler(this.changeColorToolStripMenuItem_Click);
            // 
            // tabLogDebug
            // 
            this.tabLogDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabLogDebug.Controls.Add(this.txtLogDebug);
            this.tabLogDebug.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabLogDebug.Location = new System.Drawing.Point(4, 20);
            this.tabLogDebug.Name = "tabLogDebug";
            this.tabLogDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogDebug.Size = new System.Drawing.Size(420, 219);
            this.tabLogDebug.TabIndex = 0;
            this.tabLogDebug.Text = "Debug";
            // 
            // tabLogScript
            // 
            this.tabLogScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogScript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabLogScript.Controls.Add(this.txtLogScript);
            this.tabLogScript.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabLogScript.Location = new System.Drawing.Point(4, 20);
            this.tabLogScript.Name = "tabLogScript";
            this.tabLogScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogScript.Size = new System.Drawing.Size(420, 219);
            this.tabLogScript.TabIndex = 1;
            this.tabLogScript.Text = "Script";
            // 
            // txtLogScript
            // 
            this.txtLogScript.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogScript.Location = new System.Drawing.Point(3, 3);
            this.txtLogScript.Multiline = true;
            this.txtLogScript.Name = "txtLogScript";
            this.txtLogScript.ReadOnly = true;
            this.txtLogScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogScript.Size = new System.Drawing.Size(412, 211);
            this.txtLogScript.TabIndex = 1;
            // 
            // tabLogDrops
            // 
            this.tabLogDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogDrops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabLogDrops.Controls.Add(this.txtLogDrops);
            this.tabLogDrops.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabLogDrops.Location = new System.Drawing.Point(4, 20);
            this.tabLogDrops.Name = "tabLogDrops";
            this.tabLogDrops.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogDrops.Size = new System.Drawing.Size(420, 219);
            this.tabLogDrops.TabIndex = 2;
            this.tabLogDrops.Text = "Drops";
            // 
            // txtLogDrops
            // 
            this.txtLogDrops.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogDrops.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogDrops.Location = new System.Drawing.Point(3, 3);
            this.txtLogDrops.Multiline = true;
            this.txtLogDrops.Name = "txtLogDrops";
            this.txtLogDrops.ReadOnly = true;
            this.txtLogDrops.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogDrops.Size = new System.Drawing.Size(412, 211);
            this.txtLogDrops.TabIndex = 2;
            // 
            // tabLogChat
            // 
            this.tabLogChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabLogChat.Controls.Add(this.txtLogChat);
            this.tabLogChat.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabLogChat.Location = new System.Drawing.Point(4, 20);
            this.tabLogChat.Name = "tabLogChat";
            this.tabLogChat.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogChat.Size = new System.Drawing.Size(420, 219);
            this.tabLogChat.TabIndex = 3;
            this.tabLogChat.Text = "Chat";
            // 
            // txtLogChat
            // 
            this.txtLogChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogChat.Location = new System.Drawing.Point(3, 3);
            this.txtLogChat.Multiline = true;
            this.txtLogChat.Name = "txtLogChat";
            this.txtLogChat.ReadOnly = true;
            this.txtLogChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogChat.Size = new System.Drawing.Size(412, 211);
            this.txtLogChat.TabIndex = 2;
            // 
            // tabLogBot
            // 
            this.tabLogBot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogBot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabLogBot.Controls.Add(this.txtLogBot);
            this.tabLogBot.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabLogBot.Location = new System.Drawing.Point(4, 20);
            this.tabLogBot.Name = "tabLogBot";
            this.tabLogBot.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogBot.Size = new System.Drawing.Size(420, 219);
            this.tabLogBot.TabIndex = 4;
            this.tabLogBot.Text = "Bot";
            // 
            // txtLogBot
            // 
            this.txtLogBot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogBot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogBot.Location = new System.Drawing.Point(3, 3);
            this.txtLogBot.Multiline = true;
            this.txtLogBot.Name = "txtLogBot";
            this.txtLogBot.ReadOnly = true;
            this.txtLogBot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogBot.Size = new System.Drawing.Size(412, 211);
            this.txtLogBot.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Location = new System.Drawing.Point(4, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 23);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Location = new System.Drawing.Point(217, 249);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(207, 23);
            this.panel2.TabIndex = 5;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 284);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.tabLogs);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.Name = "LogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Logs";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LogForm_Load);
            this.tabLogs.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabLogDebug.ResumeLayout(false);
            this.tabLogDebug.PerformLayout();
            this.tabLogScript.ResumeLayout(false);
            this.tabLogScript.PerformLayout();
            this.tabLogDrops.ResumeLayout(false);
            this.tabLogDrops.PerformLayout();
            this.tabLogChat.ResumeLayout(false);
            this.tabLogChat.PerformLayout();
            this.tabLogBot.ResumeLayout(false);
            this.tabLogBot.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DarkTextBox txtLogDebug;
        public DarkTextBox txtLogScript;
        private DarkButton btnClear;
        private DarkButton btnSave;
        private VisualStudioTabControl.VisualStudioTabControl tabLogs;
        private TabPage tabLogDebug;
        private TabPage tabLogScript;
        private TabPage tabLogDrops;
        private TabPage tabLogChat;
        private DarkTextBox txtLogDrops;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem changeFontToolStripMenuItem;
        private ToolStripMenuItem changeColorToolStripMenuItem;
        private ColorDialog colorDialog1;
        private TabPage tabLogBot;
        private DarkTextBox txtLogBot;
        private Panel panel1;
        private Panel panel2;
        private DarkTextBox txtLogChat;
    }
}
