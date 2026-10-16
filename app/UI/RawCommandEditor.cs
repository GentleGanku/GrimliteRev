using DarkUI.Controls;
using DarkUI.Forms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class RawCommandEditor : DarkForm
    {
        private IContainer components;
        private DarkButton btnOK;
        private DarkButton btnCancel;
        private DarkTextBox txtCmd;

        public static RawCommandEditor Instance = new RawCommandEditor();

        public string Input
        {
            get
            {
                return txtCmd.Text;
            }
        }

        public string Content
        {
            set
            {
                txtCmd.Text = value;
            }
        }

        public RawCommandEditor()
        {
            InitializeComponent();
        }

        private void RawCommandEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void RawCommandEditor_Load(object sender, EventArgs e)
        {
            txtCmd.Select();
        }

        private void txtCmd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    btnOK.PerformClick();
                    break;

                case Keys.Escape:
                    btnCancel.PerformClick();
                    break;
            }
        }

        public static string Show(string content)
        {
            using (RawCommandEditor rawCommandEditor = new RawCommandEditor
            {
                Content = content
            })
            {
                DialogResult result = rawCommandEditor.ShowDialog();
                if (result == DialogResult.OK)
                {
                    BotManager.Instance.LastIndexedSearch = 0;
                    return rawCommandEditor.Input;
                }
                else
                    return null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawCommandEditor));
            this.btnOK = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.txtCmd = new DarkUI.Controls.DarkTextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnOK.BackColorUseGeneric = false;
            this.btnOK.Checked = false;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(215, 164);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCancel.BackColorUseGeneric = false;
            this.btnCancel.Checked = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(134, 164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // txtCmd
            // 
            this.txtCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmd.Location = new System.Drawing.Point(12, 12);
            this.txtCmd.Multiline = true;
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(278, 148);
            this.txtCmd.TabIndex = 2;
            this.txtCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCmd_KeyDown);
            // 
            // RawCommandEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(302, 195);
            this.Controls.Add(this.txtCmd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "RawCommandEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Raw Command Editor";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RawCommandEditor_FormClosing);
            this.Load += new System.EventHandler(this.RawCommandEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}