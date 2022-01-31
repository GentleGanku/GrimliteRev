using DarkUI.Controls;

namespace Grimoire.UI
{
    partial class CosmeticForm
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
            this.components = new System.ComponentModel.Container();
            this.cbPlayer = new DarkUI.Controls.DarkComboBox();
            this.btnGrabCosm = new DarkUI.Controls.DarkButton();
            this.lbItems = new DarkUI.Controls.DarkListBox(this.components);
            this.btnCopyAll = new DarkUI.Controls.DarkButton();
            this.btnEquipSelected = new DarkUI.Controls.DarkButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClear = new DarkUI.Controls.DarkButton();
            this.btnRemove = new DarkUI.Controls.DarkButton();
            this.linkHelm = new System.Windows.Forms.LinkLabel();
            this.linkArmor = new System.Windows.Forms.LinkLabel();
            this.linkWeapon = new System.Windows.Forms.LinkLabel();
            this.linkPet = new System.Windows.Forms.LinkLabel();
            this.linkCape = new System.Windows.Forms.LinkLabel();
            this.btnRefresh = new DarkUI.Controls.DarkButton();
            this.linkClass = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new DarkUI.Controls.DarkLabel();
            this.label2 = new DarkUI.Controls.DarkLabel();
            this.label3 = new DarkUI.Controls.DarkLabel();
            this.label4 = new DarkUI.Controls.DarkLabel();
            this.label5 = new DarkUI.Controls.DarkLabel();
            this.label6 = new DarkUI.Controls.DarkLabel();
            this.txtArmor1 = new DarkUI.Controls.DarkTextBox();
            this.txtHelm1 = new DarkUI.Controls.DarkTextBox();
            this.txtWeapon1 = new DarkUI.Controls.DarkTextBox();
            this.txtClass1 = new DarkUI.Controls.DarkTextBox();
            this.txtCape1 = new DarkUI.Controls.DarkTextBox();
            this.txtPet1 = new DarkUI.Controls.DarkTextBox();
            this.btnHelmSet = new DarkUI.Controls.DarkButton();
            this.btnArmorSet = new DarkUI.Controls.DarkButton();
            this.btnClassSet = new DarkUI.Controls.DarkButton();
            this.btnWeaponSet = new DarkUI.Controls.DarkButton();
            this.btnPetSet = new DarkUI.Controls.DarkButton();
            this.btnCapeSet = new DarkUI.Controls.DarkButton();
            this.txtHelm2 = new DarkUI.Controls.DarkTextBox();
            this.txtArmor2 = new DarkUI.Controls.DarkTextBox();
            this.txtClass2 = new DarkUI.Controls.DarkTextBox();
            this.txtWeapon2 = new DarkUI.Controls.DarkTextBox();
            this.txtPet2 = new DarkUI.Controls.DarkTextBox();
            this.txtCape2 = new DarkUI.Controls.DarkTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtOff2 = new DarkUI.Controls.DarkTextBox();
            this.txtOff1 = new DarkUI.Controls.DarkTextBox();
            this.btnSetOffhand = new DarkUI.Controls.DarkButton();
            this.label7 = new DarkUI.Controls.DarkLabel();
            this.btnSaveSet = new DarkUI.Controls.DarkButton();
            this.btnLoadSet = new DarkUI.Controls.DarkButton();
            this.btnGroundSet = new DarkUI.Controls.DarkButton();
            this.txtGround2 = new DarkUI.Controls.DarkTextBox();
            this.txtGround1 = new DarkUI.Controls.DarkTextBox();
            this.darkLabel1 = new DarkUI.Controls.DarkLabel();
            this.linkGround = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPlayer
            // 
            this.cbPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPlayer.FormattingEnabled = true;
            this.cbPlayer.Location = new System.Drawing.Point(42, 14);
            this.cbPlayer.Name = "cbPlayer";
            this.cbPlayer.Size = new System.Drawing.Size(266, 21);
            this.cbPlayer.TabIndex = 0;
            // 
            // btnGrabCosm
            // 
            this.btnGrabCosm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrabCosm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGrabCosm.BackColorUseGeneric = false;
            this.btnGrabCosm.Checked = false;
            this.btnGrabCosm.Location = new System.Drawing.Point(310, 14);
            this.btnGrabCosm.Name = "btnGrabCosm";
            this.btnGrabCosm.Size = new System.Drawing.Size(94, 21);
            this.btnGrabCosm.TabIndex = 1;
            this.btnGrabCosm.Text = "Grab";
            this.btnGrabCosm.Click += new System.EventHandler(this.btnGrabCosm_Click);
            // 
            // lbItems
            // 
            this.lbItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lbItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbItems.FormattingEnabled = true;
            this.lbItems.ItemHeight = 18;
            this.lbItems.Location = new System.Drawing.Point(12, 266);
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(512, 182);
            this.lbItems.TabIndex = 2;
            this.lbItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbItems_KeyDown);
            this.lbItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbItems_MouseDoubleClick);
            // 
            // btnCopyAll
            // 
            this.btnCopyAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCopyAll.BackColorUseGeneric = false;
            this.btnCopyAll.Checked = false;
            this.btnCopyAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopyAll.Location = new System.Drawing.Point(3, 3);
            this.btnCopyAll.Name = "btnCopyAll";
            this.btnCopyAll.Size = new System.Drawing.Size(122, 23);
            this.btnCopyAll.TabIndex = 4;
            this.btnCopyAll.Text = "Equip All";
            this.btnCopyAll.Click += new System.EventHandler(this.btnCopyAll_Click);
            // 
            // btnEquipSelected
            // 
            this.btnEquipSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnEquipSelected.BackColorUseGeneric = false;
            this.btnEquipSelected.Checked = false;
            this.btnEquipSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEquipSelected.Location = new System.Drawing.Point(131, 3);
            this.btnEquipSelected.Name = "btnEquipSelected";
            this.btnEquipSelected.Size = new System.Drawing.Size(122, 23);
            this.btnEquipSelected.TabIndex = 5;
            this.btnEquipSelected.Text = "Equip Selected";
            this.btnEquipSelected.Click += new System.EventHandler(this.btnEquipSelected_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnCopyAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEquipSelected, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRemove, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 448);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(512, 29);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnClear.BackColorUseGeneric = false;
            this.btnClear.Checked = false;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Location = new System.Drawing.Point(259, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(122, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear All";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnRemove.BackColorUseGeneric = false;
            this.btnRemove.Checked = false;
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemove.Location = new System.Drawing.Point(387, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(122, 23);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // linkHelm
            // 
            this.linkHelm.AutoSize = true;
            this.linkHelm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkHelm.LinkColor = System.Drawing.Color.Gainsboro;
            this.linkHelm.Location = new System.Drawing.Point(3, 0);
            this.linkHelm.Name = "linkHelm";
            this.linkHelm.Size = new System.Drawing.Size(66, 14);
            this.linkHelm.TabIndex = 8;
            this.linkHelm.TabStop = true;
            this.linkHelm.Text = "Grab Helm";
            this.linkHelm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkHelm.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkItems_LinkClicked);
            // 
            // linkArmor
            // 
            this.linkArmor.AutoSize = true;
            this.linkArmor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkArmor.LinkColor = System.Drawing.Color.Gainsboro;
            this.linkArmor.Location = new System.Drawing.Point(75, 0);
            this.linkArmor.Name = "linkArmor";
            this.linkArmor.Size = new System.Drawing.Size(66, 14);
            this.linkArmor.TabIndex = 8;
            this.linkArmor.TabStop = true;
            this.linkArmor.Text = "Grab Armor";
            this.linkArmor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkArmor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkItems_LinkClicked);
            // 
            // linkWeapon
            // 
            this.linkWeapon.AutoSize = true;
            this.linkWeapon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkWeapon.LinkColor = System.Drawing.Color.Gainsboro;
            this.linkWeapon.Location = new System.Drawing.Point(219, 0);
            this.linkWeapon.Name = "linkWeapon";
            this.linkWeapon.Size = new System.Drawing.Size(66, 14);
            this.linkWeapon.TabIndex = 8;
            this.linkWeapon.TabStop = true;
            this.linkWeapon.Text = "Grab Wep";
            this.linkWeapon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkWeapon.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkItems_LinkClicked);
            // 
            // linkPet
            // 
            this.linkPet.AutoSize = true;
            this.linkPet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkPet.LinkColor = System.Drawing.Color.Gainsboro;
            this.linkPet.Location = new System.Drawing.Point(291, 0);
            this.linkPet.Name = "linkPet";
            this.linkPet.Size = new System.Drawing.Size(66, 14);
            this.linkPet.TabIndex = 8;
            this.linkPet.TabStop = true;
            this.linkPet.Text = "Grab Pet";
            this.linkPet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkPet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkItems_LinkClicked);
            // 
            // linkCape
            // 
            this.linkCape.AutoSize = true;
            this.linkCape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkCape.LinkColor = System.Drawing.Color.Gainsboro;
            this.linkCape.Location = new System.Drawing.Point(363, 0);
            this.linkCape.Name = "linkCape";
            this.linkCape.Size = new System.Drawing.Size(66, 14);
            this.linkCape.TabIndex = 8;
            this.linkCape.TabStop = true;
            this.linkCape.Text = "Grab Cape";
            this.linkCape.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkCape.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkItems_LinkClicked);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnRefresh.BackColorUseGeneric = false;
            this.btnRefresh.Checked = false;
            this.btnRefresh.Location = new System.Drawing.Point(12, 14);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(28, 21);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "⟳";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // linkClass
            // 
            this.linkClass.AutoSize = true;
            this.linkClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkClass.LinkColor = System.Drawing.Color.Gainsboro;
            this.linkClass.Location = new System.Drawing.Point(147, 0);
            this.linkClass.Name = "linkClass";
            this.linkClass.Size = new System.Drawing.Size(66, 14);
            this.linkClass.TabIndex = 8;
            this.linkClass.TabStop = true;
            this.linkClass.Text = "Grab Class";
            this.linkClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkClass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkItems_LinkClicked);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.Controls.Add(this.linkHelm, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.linkArmor, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.linkCape, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.linkClass, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.linkPet, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.linkWeapon, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.linkGround, 6, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 41);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(508, 14);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "Helm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Armor";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "Class";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label4.Location = new System.Drawing.Point(3, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "Weapon";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label5.Location = new System.Drawing.Point(3, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Pet";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label6.Location = new System.Drawing.Point(3, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "Cape";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtArmor1
            // 
            this.txtArmor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArmor1.Location = new System.Drawing.Point(60, 26);
            this.txtArmor1.Margin = new System.Windows.Forms.Padding(2);
            this.txtArmor1.Name = "txtArmor1";
            this.txtArmor1.Size = new System.Drawing.Size(240, 20);
            this.txtArmor1.TabIndex = 12;
            // 
            // txtHelm1
            // 
            this.txtHelm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHelm1.Location = new System.Drawing.Point(60, 2);
            this.txtHelm1.Margin = new System.Windows.Forms.Padding(2);
            this.txtHelm1.Name = "txtHelm1";
            this.txtHelm1.Size = new System.Drawing.Size(240, 20);
            this.txtHelm1.TabIndex = 12;
            // 
            // txtWeapon1
            // 
            this.txtWeapon1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWeapon1.Location = new System.Drawing.Point(60, 74);
            this.txtWeapon1.Margin = new System.Windows.Forms.Padding(2);
            this.txtWeapon1.Name = "txtWeapon1";
            this.txtWeapon1.Size = new System.Drawing.Size(240, 20);
            this.txtWeapon1.TabIndex = 12;
            // 
            // txtClass1
            // 
            this.txtClass1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClass1.Location = new System.Drawing.Point(60, 50);
            this.txtClass1.Margin = new System.Windows.Forms.Padding(2);
            this.txtClass1.Name = "txtClass1";
            this.txtClass1.Size = new System.Drawing.Size(240, 20);
            this.txtClass1.TabIndex = 12;
            // 
            // txtCape1
            // 
            this.txtCape1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCape1.Location = new System.Drawing.Point(60, 146);
            this.txtCape1.Margin = new System.Windows.Forms.Padding(2);
            this.txtCape1.Name = "txtCape1";
            this.txtCape1.Size = new System.Drawing.Size(240, 20);
            this.txtCape1.TabIndex = 12;
            // 
            // txtPet1
            // 
            this.txtPet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPet1.Location = new System.Drawing.Point(60, 122);
            this.txtPet1.Margin = new System.Windows.Forms.Padding(2);
            this.txtPet1.Name = "txtPet1";
            this.txtPet1.Size = new System.Drawing.Size(240, 20);
            this.txtPet1.TabIndex = 12;
            // 
            // btnHelmSet
            // 
            this.btnHelmSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnHelmSet.BackColorUseGeneric = false;
            this.btnHelmSet.Checked = false;
            this.btnHelmSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHelmSet.Location = new System.Drawing.Point(452, 1);
            this.btnHelmSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnHelmSet.Name = "btnHelmSet";
            this.btnHelmSet.Size = new System.Drawing.Size(59, 22);
            this.btnHelmSet.TabIndex = 1;
            this.btnHelmSet.Text = "Set";
            this.btnHelmSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnArmorSet
            // 
            this.btnArmorSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnArmorSet.BackColorUseGeneric = false;
            this.btnArmorSet.Checked = false;
            this.btnArmorSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnArmorSet.Location = new System.Drawing.Point(452, 25);
            this.btnArmorSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnArmorSet.Name = "btnArmorSet";
            this.btnArmorSet.Size = new System.Drawing.Size(59, 22);
            this.btnArmorSet.TabIndex = 1;
            this.btnArmorSet.Text = "Set";
            this.btnArmorSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnClassSet
            // 
            this.btnClassSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnClassSet.BackColorUseGeneric = false;
            this.btnClassSet.Checked = false;
            this.btnClassSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClassSet.Location = new System.Drawing.Point(452, 49);
            this.btnClassSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnClassSet.Name = "btnClassSet";
            this.btnClassSet.Size = new System.Drawing.Size(59, 22);
            this.btnClassSet.TabIndex = 1;
            this.btnClassSet.Text = "Set";
            this.btnClassSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnWeaponSet
            // 
            this.btnWeaponSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnWeaponSet.BackColorUseGeneric = false;
            this.btnWeaponSet.Checked = false;
            this.btnWeaponSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWeaponSet.Location = new System.Drawing.Point(452, 73);
            this.btnWeaponSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnWeaponSet.Name = "btnWeaponSet";
            this.btnWeaponSet.Size = new System.Drawing.Size(59, 22);
            this.btnWeaponSet.TabIndex = 1;
            this.btnWeaponSet.Text = "Set";
            this.btnWeaponSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnPetSet
            // 
            this.btnPetSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnPetSet.BackColorUseGeneric = false;
            this.btnPetSet.Checked = false;
            this.btnPetSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPetSet.Location = new System.Drawing.Point(452, 121);
            this.btnPetSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnPetSet.Name = "btnPetSet";
            this.btnPetSet.Size = new System.Drawing.Size(59, 22);
            this.btnPetSet.TabIndex = 1;
            this.btnPetSet.Text = "Set";
            this.btnPetSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnCapeSet
            // 
            this.btnCapeSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCapeSet.BackColorUseGeneric = false;
            this.btnCapeSet.Checked = false;
            this.btnCapeSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCapeSet.Location = new System.Drawing.Point(452, 145);
            this.btnCapeSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnCapeSet.Name = "btnCapeSet";
            this.btnCapeSet.Size = new System.Drawing.Size(59, 22);
            this.btnCapeSet.TabIndex = 1;
            this.btnCapeSet.Text = "Set";
            this.btnCapeSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // txtHelm2
            // 
            this.txtHelm2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHelm2.Location = new System.Drawing.Point(304, 2);
            this.txtHelm2.Margin = new System.Windows.Forms.Padding(2);
            this.txtHelm2.Name = "txtHelm2";
            this.txtHelm2.Size = new System.Drawing.Size(145, 20);
            this.txtHelm2.TabIndex = 13;
            // 
            // txtArmor2
            // 
            this.txtArmor2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArmor2.Location = new System.Drawing.Point(304, 26);
            this.txtArmor2.Margin = new System.Windows.Forms.Padding(2);
            this.txtArmor2.Name = "txtArmor2";
            this.txtArmor2.Size = new System.Drawing.Size(145, 20);
            this.txtArmor2.TabIndex = 13;
            // 
            // txtClass2
            // 
            this.txtClass2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClass2.Location = new System.Drawing.Point(304, 50);
            this.txtClass2.Margin = new System.Windows.Forms.Padding(2);
            this.txtClass2.Name = "txtClass2";
            this.txtClass2.Size = new System.Drawing.Size(145, 20);
            this.txtClass2.TabIndex = 13;
            // 
            // txtWeapon2
            // 
            this.txtWeapon2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWeapon2.Location = new System.Drawing.Point(304, 74);
            this.txtWeapon2.Margin = new System.Windows.Forms.Padding(2);
            this.txtWeapon2.Name = "txtWeapon2";
            this.txtWeapon2.Size = new System.Drawing.Size(145, 20);
            this.txtWeapon2.TabIndex = 13;
            // 
            // txtPet2
            // 
            this.txtPet2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPet2.Location = new System.Drawing.Point(304, 122);
            this.txtPet2.Margin = new System.Windows.Forms.Padding(2);
            this.txtPet2.Name = "txtPet2";
            this.txtPet2.Size = new System.Drawing.Size(145, 20);
            this.txtPet2.TabIndex = 13;
            // 
            // txtCape2
            // 
            this.txtCape2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCape2.Location = new System.Drawing.Point(304, 146);
            this.txtCape2.Margin = new System.Windows.Forms.Padding(2);
            this.txtCape2.Name = "txtCape2";
            this.txtCape2.Size = new System.Drawing.Size(145, 20);
            this.txtCape2.TabIndex = 13;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.06897F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.93103F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.Controls.Add(this.darkLabel1, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.btnCapeSet, 3, 6);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.txtCape1, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.txtPet1, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.txtCape2, 2, 6);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtClass1, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnPetSet, 3, 5);
            this.tableLayoutPanel3.Controls.Add(this.txtPet2, 2, 5);
            this.tableLayoutPanel3.Controls.Add(this.txtClass2, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtArmor1, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtHelm1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnClassSet, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtArmor2, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnArmorSet, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnHelmSet, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtHelm2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtWeapon1, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtWeapon2, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.btnWeaponSet, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtOff2, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.txtOff1, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.btnSetOffhand, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.btnGroundSet, 3, 7);
            this.tableLayoutPanel3.Controls.Add(this.txtGround2, 2, 7);
            this.tableLayoutPanel3.Controls.Add(this.txtGround1, 1, 7);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 67);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 8;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(512, 193);
            this.tableLayoutPanel3.TabIndex = 14;
            // 
            // txtOff2
            // 
            this.txtOff2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOff2.Location = new System.Drawing.Point(304, 98);
            this.txtOff2.Margin = new System.Windows.Forms.Padding(2);
            this.txtOff2.Name = "txtOff2";
            this.txtOff2.Size = new System.Drawing.Size(145, 20);
            this.txtOff2.TabIndex = 13;
            // 
            // txtOff1
            // 
            this.txtOff1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOff1.Location = new System.Drawing.Point(60, 98);
            this.txtOff1.Margin = new System.Windows.Forms.Padding(2);
            this.txtOff1.Name = "txtOff1";
            this.txtOff1.Size = new System.Drawing.Size(240, 20);
            this.txtOff1.TabIndex = 12;
            // 
            // btnSetOffhand
            // 
            this.btnSetOffhand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetOffhand.BackColorUseGeneric = false;
            this.btnSetOffhand.Checked = false;
            this.btnSetOffhand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetOffhand.Location = new System.Drawing.Point(452, 97);
            this.btnSetOffhand.Margin = new System.Windows.Forms.Padding(1);
            this.btnSetOffhand.Name = "btnSetOffhand";
            this.btnSetOffhand.Size = new System.Drawing.Size(59, 22);
            this.btnSetOffhand.TabIndex = 1;
            this.btnSetOffhand.Text = "Set";
            this.btnSetOffhand.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label7.Location = new System.Drawing.Point(3, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 24);
            this.label7.TabIndex = 11;
            this.label7.Text = "Offhand";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSaveSet
            // 
            this.btnSaveSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSaveSet.BackColorUseGeneric = false;
            this.btnSaveSet.Checked = false;
            this.btnSaveSet.Location = new System.Drawing.Point(405, 14);
            this.btnSaveSet.Name = "btnSaveSet";
            this.btnSaveSet.Size = new System.Drawing.Size(59, 21);
            this.btnSaveSet.TabIndex = 15;
            this.btnSaveSet.Text = "Save";
            this.btnSaveSet.Click += new System.EventHandler(this.btnSaveSet_Click);
            // 
            // btnLoadSet
            // 
            this.btnLoadSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoadSet.BackColorUseGeneric = false;
            this.btnLoadSet.Checked = false;
            this.btnLoadSet.Location = new System.Drawing.Point(465, 14);
            this.btnLoadSet.Name = "btnLoadSet";
            this.btnLoadSet.Size = new System.Drawing.Size(59, 21);
            this.btnLoadSet.TabIndex = 15;
            this.btnLoadSet.Text = "Load";
            this.btnLoadSet.Click += new System.EventHandler(this.btnLoadSet_Click);
            // 
            // btnGroundSet
            // 
            this.btnGroundSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGroundSet.BackColorUseGeneric = false;
            this.btnGroundSet.Checked = false;
            this.btnGroundSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGroundSet.Location = new System.Drawing.Point(452, 169);
            this.btnGroundSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnGroundSet.Name = "btnGroundSet";
            this.btnGroundSet.Size = new System.Drawing.Size(59, 23);
            this.btnGroundSet.TabIndex = 14;
            this.btnGroundSet.Text = "Set";
            this.btnGroundSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // txtGround2
            // 
            this.txtGround2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGround2.Location = new System.Drawing.Point(304, 170);
            this.txtGround2.Margin = new System.Windows.Forms.Padding(2);
            this.txtGround2.Name = "txtGround2";
            this.txtGround2.Size = new System.Drawing.Size(145, 20);
            this.txtGround2.TabIndex = 15;
            // 
            // txtGround1
            // 
            this.txtGround1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGround1.Location = new System.Drawing.Point(60, 170);
            this.txtGround1.Margin = new System.Windows.Forms.Padding(2);
            this.txtGround1.Name = "txtGround1";
            this.txtGround1.Size = new System.Drawing.Size(240, 20);
            this.txtGround1.TabIndex = 16;
            // 
            // darkLabel1
            // 
            this.darkLabel1.AutoSize = true;
            this.darkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(3, 168);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(52, 25);
            this.darkLabel1.TabIndex = 17;
            this.darkLabel1.Text = "Ground";
            this.darkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkGround
            // 
            this.linkGround.AutoSize = true;
            this.linkGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkGround.LinkColor = System.Drawing.Color.Gainsboro;
            this.linkGround.Location = new System.Drawing.Point(435, 0);
            this.linkGround.Name = "linkGround";
            this.linkGround.Size = new System.Drawing.Size(70, 14);
            this.linkGround.TabIndex = 9;
            this.linkGround.TabStop = true;
            this.linkGround.Text = "Grab Ground";
            this.linkGround.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkGround.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkItems_LinkClicked);
            // 
            // CosmeticForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(536, 484);
            this.Controls.Add(this.btnLoadSet);
            this.Controls.Add(this.btnSaveSet);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lbItems);
            this.Controls.Add(this.btnGrabCosm);
            this.Controls.Add(this.cbPlayer);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.Name = "CosmeticForm";
            this.Text = "SWF Cosmetics";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CosmeticForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.LinkLabel linkHelm;
        private System.Windows.Forms.LinkLabel linkArmor;
        private System.Windows.Forms.LinkLabel linkWeapon;
        private System.Windows.Forms.LinkLabel linkPet;
        private System.Windows.Forms.LinkLabel linkCape;
        private System.Windows.Forms.LinkLabel linkClass;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DarkComboBox cbPlayer;
        private DarkButton btnGrabCosm;
        private DarkListBox lbItems;
        private DarkButton btnCopyAll;
        private DarkButton btnEquipSelected;
        private DarkButton btnClear;
        private DarkButton btnRefresh;
        private DarkLabel label1;
        private DarkLabel label2;
        private DarkLabel label3;
        private DarkLabel label4;
        private DarkLabel label5;
        private DarkLabel label6;
        private DarkTextBox txtArmor1;
        private DarkTextBox txtHelm1;
        private DarkTextBox txtWeapon1;
        private DarkTextBox txtClass1;
        private DarkTextBox txtCape1;
        private DarkTextBox txtPet1;
        private DarkButton btnHelmSet;
        private DarkButton btnArmorSet;
        private DarkButton btnClassSet;
        private DarkButton btnWeaponSet;
        private DarkButton btnPetSet;
        private DarkButton btnCapeSet;
        private DarkTextBox txtHelm2;
        private DarkTextBox txtArmor2;
        private DarkTextBox txtClass2;
        private DarkTextBox txtWeapon2;
        private DarkTextBox txtPet2;
        private DarkTextBox txtCape2;
        private DarkButton btnSaveSet;
        private DarkButton btnLoadSet;
        private DarkTextBox txtOff2;
        private DarkTextBox txtOff1;
        private DarkButton btnSetOffhand;
        private DarkLabel label7;
        private DarkButton btnRemove;
        private System.Windows.Forms.LinkLabel linkGround;
        private DarkLabel darkLabel1;
        private DarkButton btnGroundSet;
        private DarkTextBox txtGround2;
        private DarkTextBox txtGround1;
    }
}