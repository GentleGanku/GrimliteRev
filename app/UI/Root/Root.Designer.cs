using DarkUI.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Grimoire.UI
{
    partial class Root
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Root));
            this.nTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.prgLoader = new DarkUI.Controls.DarkProgressBar();
            this.MenuMain = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbPing = new System.Windows.Forms.RichTextBox();
            this.cbCells = new DarkUI.Controls.DarkComboBox();
            this.cbPads = new DarkUI.Controls.DarkComboBox2();
            this.btnGetCurrentCell = new DarkUI.Controls.DarkButton();
            this.btnJump = new DarkUI.Controls.DarkButton();
            this.MenuStrip1 = new DarkUI.Controls.DarkMenuStrip();
            this.grimliteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.botToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastTravelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadersgrabbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cosmeticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eyeDropperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.notepadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pingMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxLoginServer = new System.Windows.Forms.ToolStripComboBox();
            this.DPSMeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandeditornodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snifferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spammerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tampererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.provokeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.enemyMagnetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lagKillerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hidePlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skipCutscenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableAnimationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.untargetSelfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.walkSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.camouflageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supportMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GamePanel = new DarkUI.Controls.DarkPanel();
            this.panel1.SuspendLayout();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nTray
            // 
            this.nTray.Icon = ((System.Drawing.Icon)(resources.GetObject("nTray.Icon")));
            this.nTray.Text = "Grimlite Rev";
            this.nTray.Visible = true;
            this.nTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nTray_MouseClick);
            // 
            // prgLoader
            // 
            this.prgLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.prgLoader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.prgLoader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.prgLoader.Location = new System.Drawing.Point(12, 276);
            this.prgLoader.Name = "prgLoader";
            this.prgLoader.Size = new System.Drawing.Size(936, 23);
            this.prgLoader.TabIndex = 21;
            // 
            // MenuMain
            // 
            this.MenuMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.MenuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.MenuMain.Location = new System.Drawing.Point(0, 0);
            this.MenuMain.Name = "MenuMain";
            this.MenuMain.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.MenuMain.Size = new System.Drawing.Size(202, 24);
            this.MenuMain.TabIndex = 37;
            this.MenuMain.Text = "pluginHolder";
            this.MenuMain.Visible = false;
            this.MenuMain.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.pluginAdded);
            this.MenuMain.ItemRemoved += new System.Windows.Forms.ToolStripItemEventHandler(this.pluginRemoved);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.panel1.Controls.Add(this.rtbPing);
            this.panel1.Controls.Add(this.cbCells);
            this.panel1.Controls.Add(this.cbPads);
            this.panel1.Controls.Add(this.btnGetCurrentCell);
            this.panel1.Controls.Add(this.btnJump);
            this.panel1.Controls.Add(this.MenuStrip1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel1.Size = new System.Drawing.Size(960, 24);
            this.panel1.TabIndex = 39;
            // 
            // rtbPing
            // 
            this.rtbPing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbPing.AutoWordSelection = true;
            this.rtbPing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.rtbPing.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbPing.ForeColor = System.Drawing.Color.Gainsboro;
            this.rtbPing.Location = new System.Drawing.Point(662, 4);
            this.rtbPing.Margin = new System.Windows.Forms.Padding(0);
            this.rtbPing.Multiline = false;
            this.rtbPing.Name = "rtbPing";
            this.rtbPing.ReadOnly = true;
            this.rtbPing.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbPing.Size = new System.Drawing.Size(56, 15);
            this.rtbPing.TabIndex = 37;
            this.rtbPing.Text = "";
            this.rtbPing.Visible = false;
            // 
            // cbCells
            // 
            this.cbCells.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCells.FormattingEnabled = true;
            this.cbCells.Location = new System.Drawing.Point(719, 1);
            this.cbCells.MaxDropDownItems = 50;
            this.cbCells.Name = "cbCells";
            this.cbCells.onUnique = false;
            this.cbCells.Size = new System.Drawing.Size(82, 21);
            this.cbCells.TabIndex = 18;
            this.cbCells.DropDown += new System.EventHandler(this.cbCells_Click);
            // 
            // cbPads
            // 
            this.cbPads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPads.FormattingEnabled = true;
            this.cbPads.ItemHeight = 15;
            this.cbPads.Location = new System.Drawing.Point(802, 1);
            this.cbPads.MaxDropDownItems = 20;
            this.cbPads.Name = "cbPads";
            this.cbPads.onUnique = false;
            this.cbPads.Size = new System.Drawing.Size(75, 21);
            this.cbPads.Sorted = true;
            this.cbPads.TabIndex = 19;
            this.cbPads.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbPads_DrawItem);
            this.cbPads.DropDown += new System.EventHandler(this.cbPads_Click);
            // 
            // btnGetCurrentCell
            // 
            this.btnGetCurrentCell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetCurrentCell.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGetCurrentCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGetCurrentCell.BackColorUseGeneric = false;
            this.btnGetCurrentCell.Checked = false;
            this.btnGetCurrentCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnGetCurrentCell.Location = new System.Drawing.Point(878, 1);
            this.btnGetCurrentCell.Name = "btnGetCurrentCell";
            this.btnGetCurrentCell.Size = new System.Drawing.Size(18, 21);
            this.btnGetCurrentCell.TabIndex = 36;
            this.btnGetCurrentCell.Text = "<";
            this.btnGetCurrentCell.Click += new System.EventHandler(this.btnGetCurrentCell_Click);
            // 
            // btnJump
            // 
            this.btnJump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJump.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnJump.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnJump.BackColorUseGeneric = false;
            this.btnJump.Checked = false;
            this.btnJump.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnJump.Location = new System.Drawing.Point(895, 1);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(62, 21);
            this.btnJump.TabIndex = 28;
            this.btnJump.Text = "Jump";
            this.btnJump.Click += new System.EventHandler(this.btnJump_Click);
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.MenuStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.MenuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grimliteToolStripMenuItem,
            this.botToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.packetsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.bankToolStripMenuItem1,
            this.pluginsStrip,
            this.settingsToolStripMenuItem,
            this.discordToolStripMenuItem,
            this.supportMeToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.MenuStrip1.Size = new System.Drawing.Size(960, 24);
            this.MenuStrip1.TabIndex = 20;
            this.MenuStrip1.Text = "darkMenuStrip1";
            this.MenuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuMain_MouseDown);
            // 
            // grimliteToolStripMenuItem
            // 
            this.grimliteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.grimliteToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grimliteToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.grimliteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.grimliteToolStripMenuItem.Name = "grimliteToolStripMenuItem";
            this.grimliteToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.grimliteToolStripMenuItem.Text = "About";
            this.grimliteToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.grimliteToolStripMenuItem.Click += new System.EventHandler(this.grimliteToolStripMenuItem_Click);
            // 
            // botToolStripMenuItem
            // 
            this.botToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.botToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.botToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.managerToolStripMenuItem,
            this.loadBotToolStripMenuItem});
            this.botToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.botToolStripMenuItem.Name = "botToolStripMenuItem";
            this.botToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.botToolStripMenuItem.Text = "Bot";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.startToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.stopToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // managerToolStripMenuItem
            // 
            this.managerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.managerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.managerToolStripMenuItem.Name = "managerToolStripMenuItem";
            this.managerToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.managerToolStripMenuItem.Text = "Manager";
            this.managerToolStripMenuItem.Click += new System.EventHandler(this.managerToolStripMenuItem_Click);
            // 
            // loadBotToolStripMenuItem
            // 
            this.loadBotToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.loadBotToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.loadBotToolStripMenuItem.Name = "loadBotToolStripMenuItem";
            this.loadBotToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.loadBotToolStripMenuItem.Text = "Load Bot";
            this.loadBotToolStripMenuItem.Click += new System.EventHandler(this.loadBotToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fastTravelsToolStripMenuItem,
            this.loadersgrabbersToolStripMenuItem,
            this.hotkeysToolStripMenuItem,
            this.pluginManagerToolStripMenuItem,
            this.cosmeticsToolStripMenuItem,
            this.bankToolStripMenuItem,
            this.eyeDropperToolStripMenuItem,
            this.logsToolStripMenuItem1,
            this.notepadToolStripMenuItem1,
            this.pingMonitorToolStripMenuItem,
            this.FPSToolStripMenuItem,
            this.loginToolStripMenuItem,
            this.DPSMeterToolStripMenuItem,
            this.setsToolStripMenuItem,
            this.commandeditornodeToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // fastTravelsToolStripMenuItem
            // 
            this.fastTravelsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.fastTravelsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.fastTravelsToolStripMenuItem.Name = "fastTravelsToolStripMenuItem";
            this.fastTravelsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.fastTravelsToolStripMenuItem.Text = "Fast Travels";
            this.fastTravelsToolStripMenuItem.Click += new System.EventHandler(this.fastTravelsToolStripMenuItem_Click);
            // 
            // loadersgrabbersToolStripMenuItem
            // 
            this.loadersgrabbersToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.loadersgrabbersToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.loadersgrabbersToolStripMenuItem.Name = "loadersgrabbersToolStripMenuItem";
            this.loadersgrabbersToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.loadersgrabbersToolStripMenuItem.Text = "Loaders/Grabbers";
            this.loadersgrabbersToolStripMenuItem.Click += new System.EventHandler(this.loadersgrabbersToolStripMenuItem_Click);
            // 
            // hotkeysToolStripMenuItem
            // 
            this.hotkeysToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.hotkeysToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.hotkeysToolStripMenuItem.Name = "hotkeysToolStripMenuItem";
            this.hotkeysToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.hotkeysToolStripMenuItem.Text = "Keyboard Shortcuts";
            this.hotkeysToolStripMenuItem.Click += new System.EventHandler(this.hotkeysToolStripMenuItem_Click);
            // 
            // pluginManagerToolStripMenuItem
            // 
            this.pluginManagerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.pluginManagerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.pluginManagerToolStripMenuItem.Name = "pluginManagerToolStripMenuItem";
            this.pluginManagerToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.pluginManagerToolStripMenuItem.Text = "Plugin Manager";
            this.pluginManagerToolStripMenuItem.Click += new System.EventHandler(this.pluginManagerToolStripMenuItem_Click);
            // 
            // cosmeticsToolStripMenuItem
            // 
            this.cosmeticsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.cosmeticsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.cosmeticsToolStripMenuItem.Name = "cosmeticsToolStripMenuItem";
            this.cosmeticsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.cosmeticsToolStripMenuItem.Text = "Cosmetics Panel";
            this.cosmeticsToolStripMenuItem.Click += new System.EventHandler(this.cosmeticsToolStripMenuItem_Click);
            // 
            // bankToolStripMenuItem
            // 
            this.bankToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.bankToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.bankToolStripMenuItem.Name = "bankToolStripMenuItem";
            this.bankToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.bankToolStripMenuItem.Text = "Bank Items";
            this.bankToolStripMenuItem.Click += new System.EventHandler(this.bankToolStripMenuItem_Click);
            // 
            // eyeDropperToolStripMenuItem
            // 
            this.eyeDropperToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.eyeDropperToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.eyeDropperToolStripMenuItem.Name = "eyeDropperToolStripMenuItem";
            this.eyeDropperToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.eyeDropperToolStripMenuItem.Text = "Eye Dropper";
            this.eyeDropperToolStripMenuItem.Click += new System.EventHandler(this.eyeDropperToolStripMenuItem_Click_1);
            // 
            // logsToolStripMenuItem1
            // 
            this.logsToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.logsToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.logsToolStripMenuItem1.Name = "logsToolStripMenuItem1";
            this.logsToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.logsToolStripMenuItem1.Text = "Logs";
            this.logsToolStripMenuItem1.Click += new System.EventHandler(this.logsToolStripMenuItem1_Click);
            // 
            // notepadToolStripMenuItem1
            // 
            this.notepadToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.notepadToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.notepadToolStripMenuItem1.Name = "notepadToolStripMenuItem1";
            this.notepadToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.notepadToolStripMenuItem1.Text = "Notepad";
            this.notepadToolStripMenuItem1.Click += new System.EventHandler(this.notepadToolStripMenuItem1_Click);
            // 
            // pingMonitorToolStripMenuItem
            // 
            this.pingMonitorToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.pingMonitorToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.pingMonitorToolStripMenuItem.Name = "pingMonitorToolStripMenuItem";
            this.pingMonitorToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.pingMonitorToolStripMenuItem.Text = "Ping Monitor";
            this.pingMonitorToolStripMenuItem.Click += new System.EventHandler(this.pingMonitorToolStripMenuItem_Click);
            // 
            // FPSToolStripMenuItem
            // 
            this.FPSToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.FPSToolStripMenuItem.CheckOnClick = true;
            this.FPSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2});
            this.FPSToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.FPSToolStripMenuItem.Name = "FPSToolStripMenuItem";
            this.FPSToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.FPSToolStripMenuItem.Text = "Set FPS";
            this.FPSToolStripMenuItem.Click += new System.EventHandler(this.FPSToolStripMenuItem_Click);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.toolStripTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox2.Text = "60";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.loginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxLoginServer});
            this.loginToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.loginToolStripMenuItem.Text = "Immediate Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // toolStripComboBoxLoginServer
            // 
            this.toolStripComboBoxLoginServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.toolStripComboBoxLoginServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripComboBoxLoginServer.Name = "toolStripComboBoxLoginServer";
            this.toolStripComboBoxLoginServer.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxLoginServer.Text = "Server";
            this.toolStripComboBoxLoginServer.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxLoginServer_Click);
            // 
            // DPSMeterToolStripMenuItem
            // 
            this.DPSMeterToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.DPSMeterToolStripMenuItem.Enabled = false;
            this.DPSMeterToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.DPSMeterToolStripMenuItem.Name = "DPSMeterToolStripMenuItem";
            this.DPSMeterToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.DPSMeterToolStripMenuItem.Text = "DPS Meter";
            this.DPSMeterToolStripMenuItem.Visible = false;
            this.DPSMeterToolStripMenuItem.Click += new System.EventHandler(this.dPSMeterToolStripMenuItem_Click);
            // 
            // setsToolStripMenuItem
            // 
            this.setsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.setsToolStripMenuItem.Enabled = false;
            this.setsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.setsToolStripMenuItem.Name = "setsToolStripMenuItem";
            this.setsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.setsToolStripMenuItem.Text = "Sets";
            this.setsToolStripMenuItem.Visible = false;
            this.setsToolStripMenuItem.Click += new System.EventHandler(this.setsToolStripMenuItem_Click);
            // 
            // commandeditornodeToolStripMenuItem
            // 
            this.commandeditornodeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.commandeditornodeToolStripMenuItem.Enabled = false;
            this.commandeditornodeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.commandeditornodeToolStripMenuItem.Name = "commandeditornodeToolStripMenuItem";
            this.commandeditornodeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.commandeditornodeToolStripMenuItem.Text = "commandeditornode";
            this.commandeditornodeToolStripMenuItem.Visible = false;
            this.commandeditornodeToolStripMenuItem.Click += new System.EventHandler(this.commandeditornodeToolStripMenuItem_Click);
            // 
            // packetsToolStripMenuItem
            // 
            this.packetsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.packetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snifferToolStripMenuItem,
            this.spammerToolStripMenuItem,
            this.tampererToolStripMenuItem});
            this.packetsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.packetsToolStripMenuItem.Name = "packetsToolStripMenuItem";
            this.packetsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.packetsToolStripMenuItem.Text = "Packets";
            // 
            // snifferToolStripMenuItem
            // 
            this.snifferToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.snifferToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.snifferToolStripMenuItem.Name = "snifferToolStripMenuItem";
            this.snifferToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.snifferToolStripMenuItem.Text = "Sniffer";
            this.snifferToolStripMenuItem.Click += new System.EventHandler(this.snifferToolStripMenuItem_Click);
            // 
            // spammerToolStripMenuItem
            // 
            this.spammerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.spammerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.spammerToolStripMenuItem.Name = "spammerToolStripMenuItem";
            this.spammerToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.spammerToolStripMenuItem.Text = "Spammer";
            this.spammerToolStripMenuItem.Click += new System.EventHandler(this.spammerToolStripMenuItem_Click);
            // 
            // tampererToolStripMenuItem
            // 
            this.tampererToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tampererToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.tampererToolStripMenuItem.Name = "tampererToolStripMenuItem";
            this.tampererToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.tampererToolStripMenuItem.Text = "Tamperer";
            this.tampererToolStripMenuItem.Click += new System.EventHandler(this.tampererToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infRangeToolStripMenuItem,
            this.provokeToolStripMenuItem1,
            this.enemyMagnetToolStripMenuItem,
            this.lagKillerToolStripMenuItem,
            this.hidePlayersToolStripMenuItem,
            this.skipCutscenesToolStripMenuItem,
            this.disableAnimationsToolStripMenuItem,
            this.untargetSelfToolStripMenuItem,
            this.walkSpeedToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // infRangeToolStripMenuItem
            // 
            this.infRangeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.infRangeToolStripMenuItem.CheckOnClick = true;
            this.infRangeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.infRangeToolStripMenuItem.Name = "infRangeToolStripMenuItem";
            this.infRangeToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.infRangeToolStripMenuItem.Text = "Infinite Range";
            this.infRangeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.infRangeToolStripMenuItem_Click);
            // 
            // provokeToolStripMenuItem1
            // 
            this.provokeToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.provokeToolStripMenuItem1.CheckOnClick = true;
            this.provokeToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.provokeToolStripMenuItem1.Name = "provokeToolStripMenuItem1";
            this.provokeToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.provokeToolStripMenuItem1.Text = "Provoke Monsters in Cell";
            this.provokeToolStripMenuItem1.CheckedChanged += new System.EventHandler(this.provokeToolStripMenuItem1_Click);
            this.provokeToolStripMenuItem1.Click += new System.EventHandler(this.provokeToolStripMenuItem1_Clicked);
            // 
            // enemyMagnetToolStripMenuItem
            // 
            this.enemyMagnetToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.enemyMagnetToolStripMenuItem.CheckOnClick = true;
            this.enemyMagnetToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.enemyMagnetToolStripMenuItem.Name = "enemyMagnetToolStripMenuItem";
            this.enemyMagnetToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.enemyMagnetToolStripMenuItem.Text = "Target Magnet";
            this.enemyMagnetToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enemyMagnetToolStripMenuItem_Click);
            // 
            // lagKillerToolStripMenuItem
            // 
            this.lagKillerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lagKillerToolStripMenuItem.CheckOnClick = true;
            this.lagKillerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.lagKillerToolStripMenuItem.Name = "lagKillerToolStripMenuItem";
            this.lagKillerToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.lagKillerToolStripMenuItem.Text = "Lag Killer";
            this.lagKillerToolStripMenuItem.CheckedChanged += new System.EventHandler(this.lagKillerToolStripMenuItem_Click);
            // 
            // hidePlayersToolStripMenuItem
            // 
            this.hidePlayersToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.hidePlayersToolStripMenuItem.CheckOnClick = true;
            this.hidePlayersToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.hidePlayersToolStripMenuItem.Name = "hidePlayersToolStripMenuItem";
            this.hidePlayersToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.hidePlayersToolStripMenuItem.Text = "Hide Players";
            this.hidePlayersToolStripMenuItem.CheckedChanged += new System.EventHandler(this.hidePlayersToolStripMenuItem_Click);
            // 
            // skipCutscenesToolStripMenuItem
            // 
            this.skipCutscenesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.skipCutscenesToolStripMenuItem.CheckOnClick = true;
            this.skipCutscenesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.skipCutscenesToolStripMenuItem.Name = "skipCutscenesToolStripMenuItem";
            this.skipCutscenesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.skipCutscenesToolStripMenuItem.Text = "Skip Cutscenes";
            this.skipCutscenesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.skipCutscenesToolStripMenuItem_Click);
            // 
            // disableAnimationsToolStripMenuItem
            // 
            this.disableAnimationsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.disableAnimationsToolStripMenuItem.CheckOnClick = true;
            this.disableAnimationsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.disableAnimationsToolStripMenuItem.Name = "disableAnimationsToolStripMenuItem";
            this.disableAnimationsToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.disableAnimationsToolStripMenuItem.Text = "Disable Skill Animations";
            this.disableAnimationsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.disableAnimationsToolStripMenuItem_Click);
            // 
            // untargetSelfToolStripMenuItem
            // 
            this.untargetSelfToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.untargetSelfToolStripMenuItem.CheckOnClick = true;
            this.untargetSelfToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.untargetSelfToolStripMenuItem.Name = "untargetSelfToolStripMenuItem";
            this.untargetSelfToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.untargetSelfToolStripMenuItem.Text = "Untarget Self";
            this.untargetSelfToolStripMenuItem.CheckedChanged += new System.EventHandler(this.untargetSelfToolStripMenuItem_Click);
            // 
            // walkSpeedToolStripMenuItem
            // 
            this.walkSpeedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.walkSpeedToolStripMenuItem.CheckOnClick = true;
            this.walkSpeedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.walkSpeedToolStripMenuItem.Name = "walkSpeedToolStripMenuItem";
            this.walkSpeedToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.walkSpeedToolStripMenuItem.Text = "Fast Walk Speed";
            this.walkSpeedToolStripMenuItem.CheckedChanged += new System.EventHandler(this.walkSpeedToolStripMenuItem_Click);
            // 
            // bankToolStripMenuItem1
            // 
            this.bankToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.bankToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem});
            this.bankToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.bankToolStripMenuItem1.Name = "bankToolStripMenuItem1";
            this.bankToolStripMenuItem1.Size = new System.Drawing.Size(45, 20);
            this.bankToolStripMenuItem1.Text = "Bank";
            this.bankToolStripMenuItem1.Click += new System.EventHandler(this.bankToolStripMenuItem1_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.reloadToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // pluginsStrip
            // 
            this.pluginsStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.pluginsStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.pluginsStrip.Name = "pluginsStrip";
            this.pluginsStrip.Size = new System.Drawing.Size(58, 20);
            this.pluginsStrip.Text = "Plugins";
            this.pluginsStrip.Click += new System.EventHandler(this.pluginsStrip_Click);
            //
            // settingsToolStripMenuItem
            //
            this.settingsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.camouflageToolStripMenuItem});
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            //
            // camouflageToolStripMenuItem
            //
            this.camouflageToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.camouflageToolStripMenuItem.CheckOnClick = true;
            this.camouflageToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.camouflageToolStripMenuItem.Name = "camouflageToolStripMenuItem";
            this.camouflageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.camouflageToolStripMenuItem.Text = "Camouflage";
            this.camouflageToolStripMenuItem.Click += new System.EventHandler(this.camouflageToolStripMenuItem_Click);
            //
            // discordToolStripMenuItem
            //
            this.discordToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.discordToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.discordToolStripMenuItem.Name = "discordToolStripMenuItem";
            this.discordToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.discordToolStripMenuItem.Text = "Discord";
            this.discordToolStripMenuItem.Click += new System.EventHandler(this.discordToolStripMenuItem_Click);
            //
            // supportMeToolStripMenuItem
            //
            this.supportMeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.supportMeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.supportMeToolStripMenuItem.Name = "supportMeToolStripMenuItem";
            this.supportMeToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.supportMeToolStripMenuItem.Text = "Support Me!";
            this.supportMeToolStripMenuItem.Click += new System.EventHandler(this.supportMeToolStripMenuItem_Click);
            // 
            // GamePanel
            // 
            this.GamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GamePanel.ForeColor = System.Drawing.Color.Transparent;
            this.GamePanel.Location = new System.Drawing.Point(0, 24);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(960, 551);
            this.GamePanel.TabIndex = 40;
            // 
            // Root
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(960, 575);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.prgLoader);
            this.Controls.Add(this.GamePanel);
            this.Controls.Add(this.MenuMain);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Root";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grimlite Rev";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Root_FormClosing);
            this.Load += new System.EventHandler(this.Root_Load);
            this.Shown += new System.EventHandler(this.Root_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customTravel_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MenuStrip MenuMain;
        public RichTextBox rtbPing;
        public DarkMenuStrip MenuStrip1;
        public ToolStripMenuItem stopToolStripMenuItem;
        public ToolStripMenuItem pingMonitorToolStripMenuItem;
        public ToolStripMenuItem loginToolStripMenuItem;
        public ToolStripComboBox toolStripComboBoxLoginServer;
        public ToolStripMenuItem optionsToolStripMenuItem;
        public ToolStripMenuItem infRangeToolStripMenuItem;
        public ToolStripMenuItem provokeToolStripMenuItem1;
        public ToolStripMenuItem enemyMagnetToolStripMenuItem;
        public ToolStripMenuItem lagKillerToolStripMenuItem;
        public ToolStripMenuItem hidePlayersToolStripMenuItem;
        public ToolStripMenuItem skipCutscenesToolStripMenuItem;
        public ToolStripMenuItem disableAnimationsToolStripMenuItem;
        public ToolStripMenuItem pluginsStrip;
        public ToolStripMenuItem settingsToolStripMenuItem;
        public ToolStripMenuItem camouflageToolStripMenuItem;
        public ToolStripMenuItem untargetSelfToolStripMenuItem;
        private NotifyIcon nTray;
        private DarkProgressBar prgLoader;
        private Panel panel1;
        private DarkComboBox cbCells;
        private DarkComboBox2 cbPads;
        private DarkButton btnGetCurrentCell;
        private DarkButton btnJump;
        private ToolStripMenuItem grimliteToolStripMenuItem;
        private ToolStripMenuItem botToolStripMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem managerToolStripMenuItem;
        private ToolStripMenuItem loadBotToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem fastTravelsToolStripMenuItem;
        private ToolStripMenuItem loadersgrabbersToolStripMenuItem;
        private ToolStripMenuItem hotkeysToolStripMenuItem;
        private ToolStripMenuItem pluginManagerToolStripMenuItem;
        private ToolStripMenuItem cosmeticsToolStripMenuItem;
        private ToolStripMenuItem bankToolStripMenuItem;
        private ToolStripMenuItem eyeDropperToolStripMenuItem;
        private ToolStripMenuItem logsToolStripMenuItem1;
        private ToolStripMenuItem notepadToolStripMenuItem1;
        private ToolStripMenuItem FPSToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox2;
        private ToolStripMenuItem DPSMeterToolStripMenuItem;
        private ToolStripMenuItem setsToolStripMenuItem;
        private ToolStripMenuItem commandeditornodeToolStripMenuItem;
        private ToolStripMenuItem packetsToolStripMenuItem;
        private ToolStripMenuItem snifferToolStripMenuItem;
        private ToolStripMenuItem spammerToolStripMenuItem;
        private ToolStripMenuItem tampererToolStripMenuItem;
        private ToolStripMenuItem bankToolStripMenuItem1;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem discordToolStripMenuItem;
        private ToolStripMenuItem supportMeToolStripMenuItem;
        private ToolStripMenuItem walkSpeedToolStripMenuItem;
        private DarkPanel GamePanel;
    }
}
