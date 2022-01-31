using DarkUI.Controls;

namespace Grimoire.UI
{
    partial class EyeDropper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.eyeDropper1 = new Unity3.Eyedropper.EyeDropper();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new DarkUI.Controls.DarkTextBox();
            this.SuspendLayout();
            // 
            // eyeDropper1
            // 
            this.eyeDropper1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.eyeDropper1.Location = new System.Drawing.Point(10, 8);
            this.eyeDropper1.MaximumSize = new System.Drawing.Size(22, 22);
            this.eyeDropper1.MinimumSize = new System.Drawing.Size(22, 22);
            this.eyeDropper1.Name = "eyeDropper1";
            this.eyeDropper1.PixelPreviewSize = new System.Drawing.Size(150, 150);
            this.eyeDropper1.PixelPreviewZoom = 15F;
            this.eyeDropper1.PreviewLocation = new System.Drawing.Point(-120, 20);
            this.eyeDropper1.PreviewPositionStyle = Unity3.Eyedropper.EyeDropper.ePreviewPositionStyle.BottomLeft;
            this.eyeDropper1.SelectedColor = System.Drawing.Color.Empty;
            this.eyeDropper1.ShowColorPreview = false;
            this.eyeDropper1.Size = new System.Drawing.Size(22, 22);
            this.eyeDropper1.TabIndex = 0;
            this.eyeDropper1.Text = "eyeDropper1";
            this.eyeDropper1.ScreenCaptured += new Unity3.Eyedropper.EyeDropper.ScreenCapturedArgs(this.eyeDropper1_ScreenCaptured);
            this.eyeDropper1.EndScreenCapture += new System.EventHandler(this.eyeDropper1_EndScreenCapture);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.richTextBox1.Location = new System.Drawing.Point(0, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(235, 104);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(43, -3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 37);
            this.textBox1.TabIndex = 2;
            // 
            // EyeDropper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(235, 138);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.eyeDropper1);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EyeDropper";
            this.Text = " Eye Dropper";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EyeDropper_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        public Unity3.Eyedropper.EyeDropper eyeDropper1;
        private DarkTextBox textBox1;
    }
}