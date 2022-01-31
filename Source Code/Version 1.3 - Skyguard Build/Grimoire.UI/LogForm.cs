using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class LogForm : DarkForm
    {
        public class DebugLogger : TraceListener
        {
            private LogForm log;

            public DebugLogger(LogForm log)
            {
                this.log = log;
            }

            public override void Write(string message)
            {
                log.AppendDebug(message);
            }

            public override void WriteLine(string message)
            {
                log.AppendDebug(message + "\r\n");
            }
        }

        public static DebugLogger logRec;

        private IContainer components;

        public TextBox iable;

        private DarkButton btnClear;

        private DarkButton btnSave;

        private FlatTabControl.FlatTabControl tabLogs;

        private TabPage tabLogDebug;

        private TabPage tabLogScript;

        public DarkTextBox txtLogDebug;

        public DarkTextBox txtLogScript;

        private TabPage tabLogDrops;

        private TabPage tabLogChat;

        private DarkTextBox txtLogDrops;

        private ContextMenuStrip contextMenuStrip1;

        private ToolStripMenuItem changeFontToolStripMenuItem;

        private ToolStripMenuItem changeColorToolStripMenuItem;

        private ColorDialog colorDialog1;

        private TextBox txtLogChat;

        public TextBox SelectedLog
        {
            get
            {
                if (tabLogs.SelectedIndex == 0)
                {
                    return txtLogDebug;
                }
                else if (tabLogs.SelectedIndex == 1)
                {
                    return txtLogScript;
                }
                else if (tabLogs.SelectedIndex == 2)
                {
                    return txtLogDrops;
                }
                else //(tabLogs.SelectedIndex == 3)
                {
                    return txtLogChat;
                }
            }
        }

        public static LogForm Instance
        {
            get;
        }

        public LogForm()
        {
            InitializeComponent();
            logRec = new DebugLogger(this);
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            FormClosing += LogForm_FormClosing;
            string font = Config.Load(Application.StartupPath + "\\config.cfg").Get("font");
            float? fontSize = float.Parse(Config.Load(Application.StartupPath + "\\config.cfg").Get("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            if (font != null && fontSize != null)
            {
                this.Font = new Font(font, (float)fontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        //
        // Append Debug
        //
        public void AppendDebug(string text)
        {
            if (text.Contains("{CLEAR}"))
                txtLogDebug.Clear();
            if (txtLogDebug.InvokeRequired)
            {
                txtLogDebug.Invoke((Action)delegate
                {
                    txtLogDebug.AppendText(text);
                });
            }
            else
                txtLogDebug.AppendText(text);
        }

        //
        // Append Drops
        //
        public void AppendDrops(string text)
        {
            if (text.Contains("{CLEAR}"))
                txtLogDrops.Clear();
            if (txtLogDrops.InvokeRequired)
            {
                txtLogDrops.Invoke((Action)delegate
                {
                    txtLogDrops.AppendText(text);
                });
            }
            else
                txtLogDrops.AppendText(text);
        }

        //
        // Append Chat
        //
        public void AppendChat(string text)
        {
            if (text.Contains("{CLEAR}"))
                txtLogChat.Clear();
            if (txtLogChat.InvokeRequired)
            {
                txtLogChat.Invoke((Action)delegate
                {
                    txtLogChat.AppendText(text);
                });
            }
            else
                txtLogChat.AppendText(text);
        }
        //
        // Append Script
        //
        public void AppendScript(string text, bool ignoreInvoke = false)
        {
            if (text.Contains("{CLEAR}"))
                txtLogScript.Clear();
            if (txtLogScript.InvokeRequired)
            {
                txtLogScript.Invoke((Action)delegate
                {
                    txtLogScript.AppendText(text);
                });
            }
            else
                txtLogScript.AppendText(text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SelectedLog.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, SelectedLog.Text);
                }
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
            this.txtLogDebug = new DarkUI.Controls.DarkTextBox();
            this.btnClear = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.tabLogs = new FlatTabControl.FlatTabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabLogDebug = new System.Windows.Forms.TabPage();
            this.tabLogScript = new System.Windows.Forms.TabPage();
            this.txtLogScript = new DarkUI.Controls.DarkTextBox();
            this.tabLogDrops = new System.Windows.Forms.TabPage();
            this.txtLogDrops = new DarkUI.Controls.DarkTextBox();
            this.tabLogChat = new System.Windows.Forms.TabPage();
            this.txtLogChat = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tabLogs.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabLogDebug.SuspendLayout();
            this.tabLogScript.SuspendLayout();
            this.tabLogDrops.SuspendLayout();
            this.tabLogChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLogDebug
            // 
            this.txtLogDebug.CausesValidation = false;
            this.txtLogDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogDebug.Location = new System.Drawing.Point(3, 3);
            this.txtLogDebug.Multiline = true;
            this.txtLogDebug.Name = "txtLogDebug";
            this.txtLogDebug.ReadOnly = true;
            this.txtLogDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogDebug.Size = new System.Drawing.Size(414, 208);
            this.txtLogDebug.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnClear.BackColorUseGeneric = false;
            this.btnClear.Checked = false;
            this.btnClear.Location = new System.Drawing.Point(4, 249);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(207, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSave.BackColorUseGeneric = false;
            this.btnSave.Checked = false;
            this.btnSave.Location = new System.Drawing.Point(217, 249);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(207, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabLogs
            // 
            this.tabLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabLogs.ContextMenuStrip = this.contextMenuStrip1;
            this.tabLogs.Controls.Add(this.tabLogDebug);
            this.tabLogs.Controls.Add(this.tabLogScript);
            this.tabLogs.Controls.Add(this.tabLogDrops);
            this.tabLogs.Controls.Add(this.tabLogChat);
            this.tabLogs.Location = new System.Drawing.Point(0, 0);
            this.tabLogs.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.SelectedIndex = 0;
            this.tabLogs.Size = new System.Drawing.Size(428, 243);
            this.tabLogs.TabIndex = 3;
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
            this.tabLogDebug.Controls.Add(this.txtLogDebug);
            this.tabLogDebug.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabLogDebug.Location = new System.Drawing.Point(4, 25);
            this.tabLogDebug.Name = "tabLogDebug";
            this.tabLogDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogDebug.Size = new System.Drawing.Size(420, 214);
            this.tabLogDebug.TabIndex = 0;
            this.tabLogDebug.Text = "Debug";
            // 
            // tabLogScript
            // 
            this.tabLogScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogScript.Controls.Add(this.txtLogScript);
            this.tabLogScript.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabLogScript.Location = new System.Drawing.Point(4, 25);
            this.tabLogScript.Name = "tabLogScript";
            this.tabLogScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogScript.Size = new System.Drawing.Size(420, 214);
            this.tabLogScript.TabIndex = 1;
            this.tabLogScript.Text = "Script";
            // 
            // txtLogScript
            // 
            this.txtLogScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogScript.Location = new System.Drawing.Point(3, 3);
            this.txtLogScript.Multiline = true;
            this.txtLogScript.Name = "txtLogScript";
            this.txtLogScript.ReadOnly = true;
            this.txtLogScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogScript.Size = new System.Drawing.Size(414, 208);
            this.txtLogScript.TabIndex = 1;
            // 
            // tabLogDrops
            // 
            this.tabLogDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogDrops.Controls.Add(this.txtLogDrops);
            this.tabLogDrops.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabLogDrops.Location = new System.Drawing.Point(4, 25);
            this.tabLogDrops.Name = "tabLogDrops";
            this.tabLogDrops.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogDrops.Size = new System.Drawing.Size(420, 214);
            this.tabLogDrops.TabIndex = 2;
            this.tabLogDrops.Text = "Drops";
            // 
            // txtLogDrops
            // 
            this.txtLogDrops.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogDrops.Location = new System.Drawing.Point(3, 3);
            this.txtLogDrops.Multiline = true;
            this.txtLogDrops.Name = "txtLogDrops";
            this.txtLogDrops.ReadOnly = true;
            this.txtLogDrops.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogDrops.Size = new System.Drawing.Size(414, 208);
            this.txtLogDrops.TabIndex = 2;
            // 
            // tabLogChat
            // 
            this.tabLogChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabLogChat.Controls.Add(this.txtLogChat);
            this.tabLogChat.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabLogChat.Location = new System.Drawing.Point(4, 25);
            this.tabLogChat.Name = "tabLogChat";
            this.tabLogChat.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogChat.Size = new System.Drawing.Size(420, 214);
            this.tabLogChat.TabIndex = 3;
            this.tabLogChat.Text = "Chat";
            // 
            // txtLogChat
            // 
            this.txtLogChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.txtLogChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogChat.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtLogChat.Location = new System.Drawing.Point(3, 3);
            this.txtLogChat.Multiline = true;
            this.txtLogChat.Name = "txtLogChat";
            this.txtLogChat.ReadOnly = true;
            this.txtLogChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogChat.Size = new System.Drawing.Size(414, 208);
            this.txtLogChat.TabIndex = 2;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 284);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.tabLogs);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.Name = "LogForm";
            this.Text = "Debug Log";
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
            this.ResumeLayout(false);

        }

        static LogForm()
        {
            Instance = new LogForm();
        }

        private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cdlg = new ColorDialog();
            cdlg.ShowDialog();
            this.ForeColor = cdlg.Color;
        }

        private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fdlg = new FontDialog();
            fdlg.ShowDialog();
            this.Font = fdlg.Font;
        }
    }
}