using DarkUI.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Grimoire.UI
{
    partial class Loaders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loaders));
            this.txtLoaders = new DarkUI.Controls.DarkTextBox();
            this.cbLoad = new DarkUI.Controls.DarkComboBox();
            this.btnLoad = new DarkUI.Controls.DarkButton();
            this.cbGrab = new DarkUI.Controls.DarkComboBox();
            this.btnGrab = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.treeGrabbed = new System.Windows.Forms.TreeView();
            this.panel1 = new DarkUI.Controls.DarkPanel();
            this.panel2 = new DarkUI.Controls.DarkPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnLoad1 = new DarkUI.Controls.DarkButton();
            this.btnForceAccept = new DarkUI.Controls.DarkButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLoaders
            // 
            this.txtLoaders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoaders.Location = new System.Drawing.Point(52, 12);
            this.txtLoaders.Name = "txtLoaders";
            this.txtLoaders.Size = new System.Drawing.Size(176, 20);
            this.txtLoaders.TabIndex = 29;
            // 
            // cbLoad
            // 
            this.cbLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLoad.FormattingEnabled = true;
            this.cbLoad.Items.AddRange(new object[] {
            "Hair Shop",
            "Shop",
            "Quest",
            "Armor Customizer"});
            this.cbLoad.Location = new System.Drawing.Point(52, 38);
            this.cbLoad.Name = "cbLoad";
            this.cbLoad.Size = new System.Drawing.Size(176, 21);
            this.cbLoad.TabIndex = 30;
            this.cbLoad.SelectedIndexChanged += new System.EventHandler(this.cbLoad_SelectedIndexChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoad.BackColorUseGeneric = false;
            this.btnLoad.Checked = false;
            this.btnLoad.Location = new System.Drawing.Point(52, 62);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(176, 23);
            this.btnLoad.TabIndex = 31;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cbGrab
            // 
            this.cbGrab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGrab.FormattingEnabled = true;
            this.cbGrab.Items.AddRange(new object[] {
            "Shop Items",
            "Quests (ID, Items, Drop Rates)",
            "Inventory Items",
            "Temporary Inventory Items",
            "Bank Items",
            "Monsters"});
            this.cbGrab.Location = new System.Drawing.Point(12, 301);
            this.cbGrab.Name = "cbGrab";
            this.cbGrab.Size = new System.Drawing.Size(257, 21);
            this.cbGrab.TabIndex = 33;
            // 
            // btnGrab
            // 
            this.btnGrab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGrab.BackColorUseGeneric = false;
            this.btnGrab.Checked = false;
            this.btnGrab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGrab.Location = new System.Drawing.Point(0, 0);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(129, 26);
            this.btnGrab.TabIndex = 34;
            this.btnGrab.Text = "Grab";
            this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSave.BackColorUseGeneric = false;
            this.btnSave.Checked = false;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 26);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // treeGrabbed
            // 
            this.treeGrabbed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeGrabbed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.treeGrabbed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeGrabbed.ForeColor = System.Drawing.Color.Gainsboro;
            this.treeGrabbed.LabelEdit = true;
            this.treeGrabbed.LineColor = System.Drawing.Color.Gainsboro;
            this.treeGrabbed.Location = new System.Drawing.Point(12, 94);
            this.treeGrabbed.Name = "treeGrabbed";
            this.treeGrabbed.Size = new System.Drawing.Size(257, 201);
            this.treeGrabbed.TabIndex = 38;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 26);
            this.panel1.TabIndex = 39;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGrab);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(129, 26);
            this.panel2.TabIndex = 40;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(12, 329);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(257, 26);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 41;
            // 
            // btnLoad1
            // 
            this.btnLoad1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoad1.BackColorUseGeneric = false;
            this.btnLoad1.Checked = false;
            this.btnLoad1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoad1.Location = new System.Drawing.Point(0, 0);
            this.btnLoad1.Margin = new System.Windows.Forms.Padding(0);
            this.btnLoad1.Name = "btnLoad1";
            this.btnLoad1.Size = new System.Drawing.Size(128, 23);
            this.btnLoad1.TabIndex = 31;
            this.btnLoad1.Text = "Load";
            this.btnLoad1.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnForceAccept
            // 
            this.btnForceAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnForceAccept.BackColorUseGeneric = false;
            this.btnForceAccept.Checked = false;
            this.btnForceAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnForceAccept.Location = new System.Drawing.Point(128, 0);
            this.btnForceAccept.Margin = new System.Windows.Forms.Padding(0);
            this.btnForceAccept.Name = "btnForceAccept";
            this.btnForceAccept.Size = new System.Drawing.Size(128, 23);
            this.btnForceAccept.TabIndex = 31;
            this.btnForceAccept.Text = "Force Accept";
            this.btnForceAccept.Click += new System.EventHandler(this.btnForceAccept_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnLoad1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnForceAccept, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 62);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 23);
            this.tableLayoutPanel1.TabIndex = 42;
            this.tableLayoutPanel1.Visible = false;
            // 
            // Loaders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 360);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.treeGrabbed);
            this.Controls.Add(this.cbGrab);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.cbLoad);
            this.Controls.Add(this.txtLoaders);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Loaders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loaders and Grabbers";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Loaders_FormClosing);
            this.Load += new System.EventHandler(this.Loaders_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkTextBox txtLoaders;
        private DarkComboBox cbLoad;
        private DarkButton btnLoad;
        private DarkComboBox cbGrab;
        private DarkButton btnGrab;
        private DarkButton btnSave;
        private DarkPanel panel1;
        private DarkPanel panel2;
        private SplitContainer splitContainer1;
        private DarkButton btnLoad1;
        private DarkButton btnForceAccept;
        private TableLayoutPanel tableLayoutPanel1;
        private TreeView treeGrabbed;
    }
}
