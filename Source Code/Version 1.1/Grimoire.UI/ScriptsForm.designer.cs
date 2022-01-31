namespace Grimoire.UI
{
    partial class ScriptsForm
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
            this.btnLoadScript = new System.Windows.Forms.Button();
            this.btnEditScript = new System.Windows.Forms.Button();
            this.btnGetScripts = new System.Windows.Forms.Button();
            this.btnStartScript = new System.Windows.Forms.Button();
            this.btnConvertGbot = new System.Windows.Forms.Button();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.txtRunOnExit = new System.Windows.Forms.TextBox();
            this.chkRunOnExit = new System.Windows.Forms.CheckBox();
            this.btnClearEventHandlers = new System.Windows.Forms.Button();
            this.btnLoadGbot = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoadScript
            // 
            this.btnLoadScript.Location = new System.Drawing.Point(12, 12);
            this.btnLoadScript.Name = "btnLoadScript";
            this.btnLoadScript.Size = new System.Drawing.Size(294, 23);
            this.btnLoadScript.TabIndex = 0;
            this.btnLoadScript.Text = "Load Script";
            this.btnLoadScript.UseVisualStyleBackColor = true;
            this.btnLoadScript.Click += new System.EventHandler(this.btnLoadScript_Click);
            // 
            // btnEditScript
            // 
            this.btnEditScript.Location = new System.Drawing.Point(12, 41);
            this.btnEditScript.Name = "btnEditScript";
            this.btnEditScript.Size = new System.Drawing.Size(172, 23);
            this.btnEditScript.TabIndex = 1;
            this.btnEditScript.Text = "Edit Script";
            this.btnEditScript.UseVisualStyleBackColor = true;
            this.btnEditScript.Click += new System.EventHandler(this.btnEditScript_Click);
            // 
            // btnGetScripts
            // 
            this.btnGetScripts.Location = new System.Drawing.Point(190, 41);
            this.btnGetScripts.Name = "btnGetScripts";
            this.btnGetScripts.Size = new System.Drawing.Size(116, 23);
            this.btnGetScripts.TabIndex = 2;
            this.btnGetScripts.Text = "Get Scripts";
            this.btnGetScripts.UseVisualStyleBackColor = true;
            this.btnGetScripts.Click += new System.EventHandler(this.btnGetScripts_Click);
            // 
            // btnStartScript
            // 
            this.btnStartScript.Location = new System.Drawing.Point(12, 70);
            this.btnStartScript.Name = "btnStartScript";
            this.btnStartScript.Size = new System.Drawing.Size(294, 23);
            this.btnStartScript.TabIndex = 3;
            this.btnStartScript.Text = "Start Script";
            this.btnStartScript.UseVisualStyleBackColor = true;
            this.btnStartScript.Click += new System.EventHandler(this.btnStartScript_Click);
            // 
            // btnConvertGbot
            // 
            this.btnConvertGbot.Location = new System.Drawing.Point(12, 99);
            this.btnConvertGbot.Name = "btnConvertGbot";
            this.btnConvertGbot.Size = new System.Drawing.Size(172, 23);
            this.btnConvertGbot.TabIndex = 4;
            this.btnConvertGbot.Text = "Convert .gbot to Script";
            this.btnConvertGbot.UseVisualStyleBackColor = true;
            this.btnConvertGbot.Click += new System.EventHandler(this.btnConvertGbot_Click);
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Location = new System.Drawing.Point(268, 99);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(38, 23);
            this.btnAdvanced.TabIndex = 5;
            this.btnAdvanced.Text = ">>";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // txtRunOnExit
            // 
            this.txtRunOnExit.Location = new System.Drawing.Point(12, 128);
            this.txtRunOnExit.Name = "txtRunOnExit";
            this.txtRunOnExit.Size = new System.Drawing.Size(205, 20);
            this.txtRunOnExit.TabIndex = 6;
            // 
            // chkRunOnExit
            // 
            this.chkRunOnExit.AutoSize = true;
            this.chkRunOnExit.Location = new System.Drawing.Point(223, 130);
            this.chkRunOnExit.Name = "chkRunOnExit";
            this.chkRunOnExit.Size = new System.Drawing.Size(83, 17);
            this.chkRunOnExit.TabIndex = 7;
            this.chkRunOnExit.Text = "Run On Exit";
            this.chkRunOnExit.UseVisualStyleBackColor = true;
            this.chkRunOnExit.CheckedChanged += new System.EventHandler(this.chkRunOnExit_CheckedChanged);
            // 
            // btnClearEventHandlers
            // 
            this.btnClearEventHandlers.Location = new System.Drawing.Point(12, 154);
            this.btnClearEventHandlers.Name = "btnClearEventHandlers";
            this.btnClearEventHandlers.Size = new System.Drawing.Size(294, 23);
            this.btnClearEventHandlers.TabIndex = 8;
            this.btnClearEventHandlers.Text = "Clear Script Event Handlers";
            this.btnClearEventHandlers.UseVisualStyleBackColor = true;
            this.btnClearEventHandlers.Click += new System.EventHandler(this.btnClearEventHandlers_Click);
            // 
            // btnLoadGbot
            // 
            this.btnLoadGbot.Location = new System.Drawing.Point(190, 99);
            this.btnLoadGbot.Name = "btnLoadGbot";
            this.btnLoadGbot.Size = new System.Drawing.Size(72, 23);
            this.btnLoadGbot.TabIndex = 9;
            this.btnLoadGbot.Text = "Load .gbot";
            this.btnLoadGbot.UseVisualStyleBackColor = true;
            this.btnLoadGbot.Click += new System.EventHandler(this.btnLoadGbot_Click);
            // 
            // ScriptsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 128);
            this.Controls.Add(this.btnLoadGbot);
            this.Controls.Add(this.btnClearEventHandlers);
            this.Controls.Add(this.chkRunOnExit);
            this.Controls.Add(this.txtRunOnExit);
            this.Controls.Add(this.btnAdvanced);
            this.Controls.Add(this.btnConvertGbot);
            this.Controls.Add(this.btnStartScript);
            this.Controls.Add(this.btnGetScripts);
            this.Controls.Add(this.btnEditScript);
            this.Controls.Add(this.btnLoadScript);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ScriptsForm";
            this.Text = " ";
            this.Load += new System.EventHandler(this.ScriptsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEditScript;
        private System.Windows.Forms.Button btnGetScripts;
        private System.Windows.Forms.Button btnConvertGbot;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.TextBox txtRunOnExit;
        private System.Windows.Forms.CheckBox chkRunOnExit;
        private System.Windows.Forms.Button btnClearEventHandlers;
        private System.Windows.Forms.Button btnLoadGbot;
        public System.Windows.Forms.Button btnLoadScript;
        public System.Windows.Forms.Button btnStartScript;
    }
}