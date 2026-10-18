using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Utils;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class LogForm : DarkForm
    {
        #region Fields

        public static DebugLogger logRec;

        #endregion

        #region Constructors

        public LogForm()
        {
            InitializeComponent();
            logRec = new DebugLogger(this);
        }

        static LogForm()
        {
            Instance = new LogForm();
        }

        #endregion

        #region Properties

        public static LogForm Instance
        {
            get;
        }

        public TextBox SelectedLog
        {
            get
            {
                if (tabLogs.SelectedTab == tabLogDebug)
                    return txtLogDebug;
                else if (tabLogs.SelectedTab == tabLogScript)
                    return txtLogScript;
                else if (tabLogs.SelectedTab == tabLogDrops)
                    return txtLogDrops;
                else if (tabLogs.SelectedTab == tabLogChat)
                    return txtLogChat;
                else if (tabLogs.SelectedTab == tabLogBot)
                    return txtLogBot;
                return null;
            }
        }

        #endregion

        #region Methods

        public void AppendDebug(string text)
        {
            Append(txtLogDebug, text);
        }

        public void AppendDrops(string text)
        {
            Append(txtLogDrops, text);
        }

        public void AppendChat(string text)
        {
            Append(txtLogChat, text);
        }

        public void AppendScript(string text, bool ignoreInvoke = false)
        {
            Append(txtLogScript, text);
        }

        public void AppendBot(string text, bool ignoreInvoke = false)
        {
            Append(txtLogBot, text);
        }

        // Callers include the bot's worker thread; Clear and the append both run on the UI thread, in call order.
        private static void Append(TextBoxBase box, string text)
        {
            void Write()
            {
                if (text.Contains("{CLEAR}"))
                {
                    box.Clear();
                }
                box.AppendCapped(text);
            }
            if (box.InvokeRequired)
            {
                box.Invoke((Action)Write);
            }
            else
            {
                Write();
            }
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            FormClosing += LogForm_FormClosing;
            string font = Config.Instance.GetValue<string>("font");
            float? fontSize = float.Parse(Config.Instance.GetValue<string>("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            if (font != null && fontSize != null)
            {
                this.Font = new Font(font, (float)fontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.FormOwnerClosing)
            {
                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;
                e.Cancel = true;
                Hide();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SelectedLog.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Log";
                saveFileDialog.DefaultExt = ".txt";
                saveFileDialog.Filter = "Text documents|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, SelectedLog.Text);
                }
            }
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

        #endregion

        #region Classes

        public class DebugLogger : TraceListener
        {
            #region Fields

            private LogForm log;

            #endregion

            #region Constructors

            public DebugLogger(LogForm log)
            {
                this.log = log;
            }

            #endregion

            #region Methods

            public override void Write(string message)
            {
                log.AppendDebug(message);
            }

            public override void WriteLine(string message)
            {
                log.AppendDebug(message + "\r\n");
            }

            #endregion
        }

        #endregion
    }
}