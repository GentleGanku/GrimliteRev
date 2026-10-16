namespace Grimoire.UI
{
    partial class ShortcutsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public static ShortcutsForm Instance = new ShortcutsForm();

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
            this.tblShortcuts = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            //
            // tblShortcuts
            //
            this.tblShortcuts.AutoSize = true;
            this.tblShortcuts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblShortcuts.ColumnCount = 2;
            this.tblShortcuts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblShortcuts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblShortcuts.Location = new System.Drawing.Point(12, 8);
            this.tblShortcuts.Name = "tblShortcuts";
            this.tblShortcuts.Size = new System.Drawing.Size(0, 0);
            this.tblShortcuts.TabIndex = 0;
            //
            // ShortcutsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tblShortcuts);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShortcutsForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shortcuts";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShortcutsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblShortcuts;
    }
}
