namespace Grimoire.UI
{
    partial class PortalLoaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortalLoaderForm));
            this.grdBots = new DarkUI.Controls.DarkDataGridView();
            this.btnDownload = new DarkUI.Controls.DarkButton();
            this.btnLoad = new DarkUI.Controls.DarkButton();
            this.btnRefresh = new DarkUI.Controls.DarkButton();
            this.btnShowBot = new DarkUI.Controls.DarkButton();
            this.lblText = new DarkUI.Controls.DarkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grdBots)).BeginInit();
            this.SuspendLayout();
            // 
            // grdBots
            // 
            this.grdBots.ColumnHeadersHeight = 4;
            this.grdBots.Location = new System.Drawing.Point(12, 25);
            this.grdBots.Name = "grdBots";
            this.grdBots.RowHeadersWidth = 41;
            this.grdBots.Size = new System.Drawing.Size(417, 363);
            this.grdBots.TabIndex = 0;
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnDownload.BackColorUseGeneric = false;
            this.btnDownload.Checked = false;
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(12, 394);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(100, 21);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoad.BackColorUseGeneric = false;
            this.btnLoad.Checked = false;
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Location = new System.Drawing.Point(118, 394);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 21);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnRefresh.BackColorUseGeneric = false;
            this.btnRefresh.Checked = false;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(329, 394);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 21);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            // 
            // btnShowBot
            // 
            this.btnShowBot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnShowBot.BackColorUseGeneric = false;
            this.btnShowBot.Checked = false;
            this.btnShowBot.ForeColor = System.Drawing.Color.White;
            this.btnShowBot.Location = new System.Drawing.Point(223, 394);
            this.btnShowBot.Name = "btnShowBot";
            this.btnShowBot.Size = new System.Drawing.Size(100, 21);
            this.btnShowBot.TabIndex = 4;
            this.btnShowBot.Text = "Show Bot";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblText.Location = new System.Drawing.Point(9, 9);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(200, 13);
            this.lblText.TabIndex = 5;
            this.lblText.Text = "Select a bot to either download or load it.";
            // 
            // PortalLoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(441, 425);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnShowBot);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.grdBots);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PortalLoaderForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Portal Loader";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.grdBots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkUI.Controls.DarkDataGridView grdBots;
        private DarkUI.Controls.DarkButton btnDownload;
        private DarkUI.Controls.DarkButton btnLoad;
        private DarkUI.Controls.DarkButton btnRefresh;
        private DarkUI.Controls.DarkButton btnShowBot;
        private DarkUI.Controls.DarkLabel lblText;
    }
}