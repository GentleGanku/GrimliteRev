using DarkUI.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Grimoire.UI
{
    partial class PluginManager
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
            this.gbLoaded = new DarkUI.Controls.DarkGroupBox();
            this.btnUnload = new DarkUI.Controls.DarkButton();
            this.txtDesc = new DarkUI.Controls.DarkTextBox();
            this.lblAuthor = new DarkUI.Controls.DarkLabel();
            this.lstLoaded = new System.Windows.Forms.ListBox();
            this.gbLoad = new DarkUI.Controls.DarkGroupBox();
            this.btnBrowse = new DarkUI.Controls.DarkButton();
            this.btnLoad = new DarkUI.Controls.DarkButton();
            this.txtPlugin = new DarkUI.Controls.DarkTextBox();
            this.treePlugins = new System.Windows.Forms.TreeView();
            this.gbLoaded.SuspendLayout();
            this.gbLoad.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLoaded
            // 
            this.gbLoaded.Controls.Add(this.btnUnload);
            this.gbLoaded.Controls.Add(this.txtDesc);
            this.gbLoaded.Controls.Add(this.lblAuthor);
            this.gbLoaded.Controls.Add(this.lstLoaded);
            this.gbLoaded.Location = new System.Drawing.Point(12, 213);
            this.gbLoaded.Name = "gbLoaded";
            this.gbLoaded.Size = new System.Drawing.Size(292, 260);
            this.gbLoaded.TabIndex = 12;
            this.gbLoaded.TabStop = false;
            this.gbLoaded.Text = "Loaded plugins";
            // 
            // btnUnload
            // 
            this.btnUnload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnUnload.BackColorUseGeneric = false;
            this.btnUnload.Checked = false;
            this.btnUnload.Location = new System.Drawing.Point(148, 231);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(135, 23);
            this.btnUnload.TabIndex = 3;
            this.btnUnload.Text = "Unload selected plugin";
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // txtDesc
            // 
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDesc.Location = new System.Drawing.Point(6, 112);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ReadOnly = true;
            this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDesc.Size = new System.Drawing.Size(277, 113);
            this.txtDesc.TabIndex = 2;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblAuthor.Location = new System.Drawing.Point(3, 96);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(92, 13);
            this.lblAuthor.TabIndex = 1;
            this.lblAuthor.Text = "Plugin created by:";
            // 
            // lstLoaded
            // 
            this.lstLoaded.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstLoaded.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLoaded.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lstLoaded.FormattingEnabled = true;
            this.lstLoaded.Location = new System.Drawing.Point(6, 19);
            this.lstLoaded.Name = "lstLoaded";
            this.lstLoaded.Size = new System.Drawing.Size(277, 67);
            this.lstLoaded.TabIndex = 0;
            this.lstLoaded.SelectedIndexChanged += new System.EventHandler(this.lstLoaded_SelectedIndexChanged);
            // 
            // gbLoad
            // 
            this.gbLoad.Controls.Add(this.btnBrowse);
            this.gbLoad.Controls.Add(this.btnLoad);
            this.gbLoad.Controls.Add(this.txtPlugin);
            this.gbLoad.Location = new System.Drawing.Point(12, 12);
            this.gbLoad.Name = "gbLoad";
            this.gbLoad.Size = new System.Drawing.Size(292, 51);
            this.gbLoad.TabIndex = 11;
            this.gbLoad.TabStop = false;
            this.gbLoad.Text = "Load plugin";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBrowse.BackColorUseGeneric = false;
            this.btnBrowse.Checked = false;
            this.btnBrowse.Location = new System.Drawing.Point(200, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 20);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoad.BackColorUseGeneric = false;
            this.btnLoad.Checked = false;
            this.btnLoad.Location = new System.Drawing.Point(231, 19);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(55, 20);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtPlugin
            // 
            this.txtPlugin.Location = new System.Drawing.Point(6, 19);
            this.txtPlugin.Name = "txtPlugin";
            this.txtPlugin.Size = new System.Drawing.Size(188, 20);
            this.txtPlugin.TabIndex = 4;
            // 
            // treePlugins
            // 
            this.treePlugins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.treePlugins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treePlugins.ForeColor = System.Drawing.Color.Gainsboro;
            this.treePlugins.HotTracking = true;
            this.treePlugins.Location = new System.Drawing.Point(12, 70);
            this.treePlugins.Name = "treePlugins";
            this.treePlugins.Size = new System.Drawing.Size(292, 136);
            this.treePlugins.TabIndex = 13;
            this.treePlugins.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treePlugins_AfterExpand);
            this.treePlugins.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePlugins_AfterSelect);
            // 
            // PluginManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(316, 485);
            this.Controls.Add(this.treePlugins);
            this.Controls.Add(this.gbLoaded);
            this.Controls.Add(this.gbLoad);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginManager";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Plugin Manager";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginManager_FormClosing);
            this.Load += new System.EventHandler(this.PluginManager_Load);
            this.gbLoaded.ResumeLayout(false);
            this.gbLoaded.PerformLayout();
            this.gbLoad.ResumeLayout(false);
            this.gbLoad.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DarkGroupBox gbLoaded;
        public DarkButton btnUnload;
        public DarkTextBox txtDesc;
        public DarkLabel lblAuthor;
        public ListBox lstLoaded;
        public DarkGroupBox gbLoad;
        public DarkButton btnBrowse;
        public DarkButton btnLoad;
        public DarkTextBox txtPlugin;
        private TreeView treePlugins;
    }
}
