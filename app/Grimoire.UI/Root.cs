using AxShockwaveFlashObjects;
using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Tools;
using Grimoire.Utils;
using System;
using System.Drawing;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using Properties;

namespace Grimoire.UI
{
    public class Root : DarkForm
    {
        private IContainer components;

        private NotifyIcon nTray;

        private DarkProgressBar prgLoader;
        public MenuStrip MenuMain;
        public CancellationTokenSource login_cts;
        private Panel panel1;
        public RichTextBox rtbPing;
        private DarkComboBox cbCells;
        private DarkComboBox2 cbPads;
        private DarkButton btnGetCurrentCell;
        private DarkButton btnJump;
        public DarkMenuStrip MenuStrip1;
        private ToolStripMenuItem grimliteToolStripMenuItem;
        private ToolStripMenuItem botToolStripMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        public ToolStripMenuItem stopToolStripMenuItem;
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
        public ToolStripMenuItem pingMonitorToolStripMenuItem;
        private ToolStripMenuItem FPSToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox2;
        public ToolStripMenuItem loginToolStripMenuItem;
        public ToolStripComboBox toolStripComboBoxLoginServer;
        private ToolStripMenuItem DPSMeterToolStripMenuItem;
        private ToolStripMenuItem setsToolStripMenuItem;
        private ToolStripMenuItem commandeditornodeToolStripMenuItem;
        private ToolStripMenuItem packetsToolStripMenuItem;
        private ToolStripMenuItem snifferToolStripMenuItem;
        private ToolStripMenuItem spammerToolStripMenuItem;
        private ToolStripMenuItem tampererToolStripMenuItem;
        public ToolStripMenuItem optionsToolStripMenuItem;
        public ToolStripMenuItem infRangeToolStripMenuItem;
        public ToolStripMenuItem provokeToolStripMenuItem1;
        public ToolStripMenuItem enemyMagnetToolStripMenuItem;
        public ToolStripMenuItem lagKillerToolStripMenuItem;
        public ToolStripMenuItem hidePlayersToolStripMenuItem;
        public ToolStripMenuItem skipCutscenesToolStripMenuItem;
        public ToolStripMenuItem disableAnimationsToolStripMenuItem;
        private ToolStripMenuItem bankToolStripMenuItem1;
        private ToolStripMenuItem reloadToolStripMenuItem;
        public ToolStripMenuItem pluginsStrip;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem discordToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem botRequestToolStripMenuItem;
        private ToolStripMenuItem grimoireSuggestionsToolStripMenuItem;
        private ToolStripMenuItem googleFormToolStripMenuItem;
        private ToolStripMenuItem googleDocsToolStripMenuItem;
        private ToolStripMenuItem getBotsToolStripMenuItem;
        public ToolStripMenuItem provokeAllMonsterInMapToolStripMenuItem;
        public ToolStripMenuItem untargetSelfToolStripMenuItem;
        public ToolStripMenuItem autosaveStateToolStripMenuItem;
        private ToolStripMenuItem walkSpeedToolStripMenuItem;
        private ToolStripMenuItem enableCaptureToolStripMenuItem;
        private System.Threading.CancellationTokenSource _cts;
        private DarkPanel GamePanel;

        public static Root Instance
        {
            get;
            private set;
        }

        public AxShockwaveFlash Client => Game;

        private AxShockwaveFlash Game;

        public static string PluginsPath { get; private set; } = Path.Combine(System.Windows.Forms.Application.StartupPath, "Plugins");

        public static PluginManager PreloadedPlugins { get; private set; }

        public static bool LightMode { get; set; } = false;

        public static bool LeanMode { get; set; } = false;

        public Root()
        {
            InitializeComponent();
            PreloadedPlugins = new PluginManager();
            Instance = this;
            pingMonitorToggle();
            clientHeaderToggle();
            triggerToggle();
        }

        private void Root_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            Hotkeys.Instance.LoadHotkeys();
            PreloadedPlugins.LoadRange(Directory.GetFiles(PluginsPath));
            InitializeGame();
            Proxy.InitializeProxyHandlers();
            Task.Factory.StartNew(Proxy.Instance.ClientExecute, TaskCreationOptions.LongRunning);
        }

        private void InitializeGame()
        {
            Flash.flash?.Dispose();
            Game = new AxShockwaveFlash();
            Game.BeginInit();
            Game.Name = "Flash";
            Game.Dock = DockStyle.Fill;
            Game.TabIndex = 0;
            Game.FlashCall += Flash.ProcessFlashCall;
            Flash.SwfLoadProgress += OnLoadProgress;
            GamePanel.Controls.Add(Game);
            Game.Visible = false;
            Game.EndInit();
            Flash.flash = Game;
            byte[] swf = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "grimlite-rev.swf"));
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(8 + swf.Length);
                writer.Write(1432769894);
                writer.Write(swf.Length);
                writer.Write(swf);
                writer.Seek(0, SeekOrigin.Begin);
                Game.OcxState = new AxHost.State(stream, 1, false, null);
            }
        }

        private void OnLoadProgress(int progress)
        {
            if (progress < prgLoader.Maximum)
            {
                prgLoader.Value = progress;
                return;
            }
            Flash.SwfLoadProgress -= OnLoadProgress;
            Game.Visible = true;
            prgLoader.Visible = false;
        }

        private void fastTravelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Travel.Instance);
        }

        private void loadersgrabbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Loaders.Instance);
        }

        private void hotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Hotkeys.Instance);
        }

        private void pluginManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PluginManager.Instance);
        }

        private void snifferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PacketLogger.Instance);
        }

        private void spammerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PacketSpammer.Instance);
        }

        private void tampererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PacketTamperer.Instance);
        }

        public void ShowForm(Form form)
        {
            if (form.WindowState == FormWindowState.Minimized)
            {
                form.WindowState = FormWindowState.Normal;
                form.Show();
                form.BringToFront();
                form.Focus();
            }
            else if (form.Visible)
            {
                form.Hide();
            }
            else
            {
                form.Show();
                form.BringToFront();
                form.Focus();
            }
        }

        private void cbCells_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                if (cbCells.Items.Count > 0)
                {
                    cbCells.Items.Clear();
                }
                cbCells.Items.AddRange(World.Cells);
            }
        }

        private string[] presetPads = new string[]
        {
            "Spawn",
            "Center",
            "Left",
            "Right",
            "Top",
            "Bottom",
            "Up",
            "Down"
        };

        private void cbPads_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                if (cbPads.Items.Count > 0)
                {
                    cbPads.Items.Clear();
                }
                List<string> pads = World.Pads ?? new List<string>();
                if (pads.Count > 0)
                {
                    cbPads.Items.AddRange(pads.ToArray());
                }
                foreach (string pad in presetPads)
                {
                    if (pads.Find((string p) => p.Equals(pad)) == null)
                    {
                        cbPads.Items.Add(pad);
                    }
                }
            }
        }

        private void cbPads_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (!cbPads.DroppedDown && !cbPads.DrawDropdownHoverOutline)
            {
                SolidBrush brush = new SolidBrush(BackColor);
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            else
            {
                e.DrawBackground();
            }
            if (cbPads.Items.Count > e.Index && e.Index > -1)
            {
                using (SolidBrush brush2 = new SolidBrush(ForeColor))
                {
                    string s = cbPads.Items[e.Index].ToString();
                    List<string> pads = World.Pads ?? new List<string>();
                    if (pads.Count > 0 && pads.Find((string p) => p.Equals(s)) != null)
                    {
                        e.Graphics.DrawString(s, e.Font, new SolidBrush(Color.LimeGreen), e.Bounds, StringFormat.GenericDefault);
                    }
                    else
                    {
                        e.Graphics.DrawString(s, e.Font, brush2, e.Bounds, StringFormat.GenericDefault);
                    }
                }
                if (cbPads.DrawDropdownHoverOutline)
                {
                    e.DrawFocusRectangle();
                }
            }
        }

        private void Root_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hotkeys.InstalledHotkeys.ForEach(delegate (Hotkey h)
            {
                h.Uninstall();
            });
            Proxy.AppClosingToken.Cancel(true);
            KeyboardHook.Instance.Dispose();
            CommandColorForm.Instance.Dispose();
            nTray.Visible = false;
            nTray.Icon.Dispose();
            nTray.Dispose();
            Process.GetCurrentProcess().Kill();
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
            this.provokeAllMonsterInMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enemyMagnetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lagKillerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hidePlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skipCutscenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableAnimationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.untargetSelfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autosaveStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.walkSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.botRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grimoireSuggestionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleDocsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getBotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.rtbPing.Location = new System.Drawing.Point(656, 4);
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
            this.helpToolStripMenuItem,
            this.getBotsToolStripMenuItem});
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
            this.hotkeysToolStripMenuItem.Text = "Hotkeys";
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
            this.cosmeticsToolStripMenuItem.Text = "Cosmetics";
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
            this.provokeAllMonsterInMapToolStripMenuItem,
            this.enemyMagnetToolStripMenuItem,
            this.lagKillerToolStripMenuItem,
            this.hidePlayersToolStripMenuItem,
            this.skipCutscenesToolStripMenuItem,
            this.disableAnimationsToolStripMenuItem,
            this.untargetSelfToolStripMenuItem,
            this.autosaveStateToolStripMenuItem,
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
            // provokeAllMonsterInMapToolStripMenuItem
            // 
            this.provokeAllMonsterInMapToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.provokeAllMonsterInMapToolStripMenuItem.CheckOnClick = true;
            this.provokeAllMonsterInMapToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.provokeAllMonsterInMapToolStripMenuItem.Name = "provokeAllMonsterInMapToolStripMenuItem";
            this.provokeAllMonsterInMapToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.provokeAllMonsterInMapToolStripMenuItem.Text = "Provoke Monsters in Map";
            this.provokeAllMonsterInMapToolStripMenuItem.CheckedChanged += new System.EventHandler(this.provokeAllMonsterInMapToolStripMenuItem_Click);
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
            // autosaveStateToolStripMenuItem
            // 
            this.autosaveStateToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.autosaveStateToolStripMenuItem.CheckOnClick = true;
            this.autosaveStateToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.autosaveStateToolStripMenuItem.Name = "autosaveStateToolStripMenuItem";
            this.autosaveStateToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.autosaveStateToolStripMenuItem.Text = "Auto-save State";
            this.autosaveStateToolStripMenuItem.CheckedChanged += new System.EventHandler(this.autosaveStateToolStripMenuItem_Click);
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
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discordToolStripMenuItem,
            this.toolStripMenuItem1,
            this.botRequestToolStripMenuItem,
            this.grimoireSuggestionsToolStripMenuItem,
            this.enableCaptureToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helpToolStripMenuItem.Text = "More";
            // 
            // discordToolStripMenuItem
            // 
            this.discordToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.discordToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.discordToolStripMenuItem.Name = "discordToolStripMenuItem";
            this.discordToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.discordToolStripMenuItem.Text = "Discord Server";
            this.discordToolStripMenuItem.Click += new System.EventHandler(this.discordToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItem1.Text = "Bot Portal";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // botRequestToolStripMenuItem
            // 
            this.botRequestToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.botRequestToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.botRequestToolStripMenuItem.Name = "botRequestToolStripMenuItem";
            this.botRequestToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.botRequestToolStripMenuItem.Text = "Bot Request";
            this.botRequestToolStripMenuItem.Click += new System.EventHandler(this.botRequestToolStripMenuItem_Click);
            // 
            // grimoireSuggestionsToolStripMenuItem
            // 
            this.grimoireSuggestionsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.grimoireSuggestionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.googleFormToolStripMenuItem,
            this.googleDocsToolStripMenuItem});
            this.grimoireSuggestionsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.grimoireSuggestionsToolStripMenuItem.Name = "grimoireSuggestionsToolStripMenuItem";
            this.grimoireSuggestionsToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.grimoireSuggestionsToolStripMenuItem.Text = "Grimoire Suggestions";
            // 
            // googleFormToolStripMenuItem
            // 
            this.googleFormToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.googleFormToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.googleFormToolStripMenuItem.Name = "googleFormToolStripMenuItem";
            this.googleFormToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.googleFormToolStripMenuItem.Text = "Google Form";
            this.googleFormToolStripMenuItem.Click += new System.EventHandler(this.googleFormToolStripMenuItem_Click);
            // 
            // googleDocsToolStripMenuItem
            // 
            this.googleDocsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.googleDocsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.googleDocsToolStripMenuItem.Name = "googleDocsToolStripMenuItem";
            this.googleDocsToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.googleDocsToolStripMenuItem.Text = "Google Docs";
            this.googleDocsToolStripMenuItem.Click += new System.EventHandler(this.googleDocsToolStripMenuItem_Click);
            // 
            // getBotsToolStripMenuItem
            // 
            this.getBotsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.getBotsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.getBotsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.getBotsToolStripMenuItem.Name = "getBotsToolStripMenuItem";
            this.getBotsToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.getBotsToolStripMenuItem.Text = "Get Bots";
            this.getBotsToolStripMenuItem.Click += new System.EventHandler(this.getBotsToolStripMenuItem_Click);
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
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customTravel_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                string Cell = (string)this.cbCells.SelectedItem;
                string Pad = (string)this.cbPads.SelectedItem;
                Player.MoveToCell(Cell ?? Player.Cell, Pad ?? Player.Pad);
            }
        }

        private void btnGetCurrentCell_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                if (cbCells.Items.Count > 0)
                {
                    cbCells.Items.Clear();
                }
                cbCells.Items.AddRange(World.Cells);
                cbCells.Text = Player.Cell;
                cbPads.Text = Player.Pad;
            }
        }

        private void cosmeticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(CosmeticForm.Instance);
        }

        private void bankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(BankForm.Instance);
        }

        private void discordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "This opens a new tab on your default browser. Proceed?", "Join Discord Server (AQWBots)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://discord.io/AQWBots");
            }
        }

        private void botRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "This opens a new tab on your default browser. Proceed?", "Bot Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSd2NSx1ezF-6bc2jRBuTniIka5z6kA2NbmC8CRCOFtpVxcRCA/viewform");
            }
        }

        private void setsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Set.Instance);
        }

        public void BotStateChanged(bool IsRunning)
        {
            if (IsRunning)
            {
                startToolStripMenuItem.Enabled = false;
                loadBotToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
            }
            else
            {
                startToolStripMenuItem.Enabled = true;
                loadBotToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
            }
        }

        private void nTray_MouseClick(object sender, MouseEventArgs e)
        {
            ShowForm(this);
        }

        private void eyeDropperToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ShowForm(EyeDropper.Instance);
        }

        private void logsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(LogForm.Instance);
        }

        private void notepadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(Notepad.Instance);
        }

        private void infRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.InfiniteRange = infRangeToolStripMenuItem.Checked;
            BotManager.Instance.chkInfiniteRange.Checked = infRangeToolStripMenuItem.Checked;
        }

        private async void provokeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OptionsManager.ProvokeMonsters = provokeToolStripMenuItem1.Checked;
            BotManager.Instance.chkProvoke.Checked = provokeToolStripMenuItem1.Checked;
            if (provokeToolStripMenuItem1.Checked && !OptionsManager.IsRunning)
            {
                OptionsManager.ApplySettings();
            }
        }

        private async void provokeToolStripMenuItem1_Clicked(object sender, EventArgs e)
        {
            if (!provokeToolStripMenuItem1.Checked && (Player.CurrentState == Player.State.InCombat))
            {
                BotUtilities.MoveToSelfCell();
            }
        }

        private void provokeAllMonsterInMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.ProvokeAllMonster = provokeAllMonsterInMapToolStripMenuItem.Checked;
            BotManager.Instance.chkProvokeAllMon.Checked = provokeAllMonsterInMapToolStripMenuItem.Checked;
            if (!provokeAllMonsterInMapToolStripMenuItem.Checked && (Player.CurrentState == Player.State.InCombat))
            {
                BotUtilities.MoveToSelfCell();
            }
            if (provokeAllMonsterInMapToolStripMenuItem.Checked && !OptionsManager.IsRunning)
            {
                OptionsManager.ApplySettings();
            }
        }

        private async void enemyMagnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.EnemyMagnet = enemyMagnetToolStripMenuItem.Checked;
            BotManager.Instance.chkMagnet.Checked = enemyMagnetToolStripMenuItem.Checked;
            if (enemyMagnetToolStripMenuItem.Checked && !OptionsManager.IsRunning)
            {
                OptionsManager.ApplySettings();
            }
        }

        private void lagKillerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.LagKiller = lagKillerToolStripMenuItem.Checked;
            BotManager.Instance.chkLag.Checked = lagKillerToolStripMenuItem.Checked;
        }

        private void hidePlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.HidePlayers = hidePlayersToolStripMenuItem.Checked;
            BotManager.Instance.chkHidePlayers.Checked = hidePlayersToolStripMenuItem.Checked;
        }

        private async void skipCutscenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.SkipCutscenes = skipCutscenesToolStripMenuItem.Checked;
            BotManager.Instance.chkSkipCutscenes.Checked = skipCutscenesToolStripMenuItem.Checked;
            if (skipCutscenesToolStripMenuItem.Checked && !OptionsManager.IsRunning)
            {
                OptionsManager.ApplySettings();
            }
        }

        private void disableAnimationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.DisableAnimations = disableAnimationsToolStripMenuItem.Checked;
            BotManager.Instance.chkDisableAnims.Checked = disableAnimationsToolStripMenuItem.Checked;
        }

        private void untargetSelfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.Untarget = untargetSelfToolStripMenuItem.Checked;
            BotManager.Instance.chkUntarget.Checked = untargetSelfToolStripMenuItem.Checked;
            if (untargetSelfToolStripMenuItem.Checked && !OptionsManager.IsRunning)
            {
                OptionsManager.ApplySettings();
            }
        }

        private void autosaveStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager._saveState = autosaveStateToolStripMenuItem.Checked;
            BotManager.Instance.chkSaveState.Checked = autosaveStateToolStripMenuItem.Checked;
        }

        private void walkSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.WalkSpeed = walkSpeedToolStripMenuItem.Checked ? 40 : 8;
            if (walkSpeedToolStripMenuItem.Checked && !OptionsManager.IsRunning)
            {
                OptionsManager.ApplySettings();
            }
        }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void MenuMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void bankToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                Player.Bank.Show();
            }
        }

        private async void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                await Proxy.Instance.SendToServer($"%xt%zm%loadBank%{World.RoomId}%All%");
                Player.Bank.SavedItems = Flash.Call<List<InventoryItem>>("GetBankItems", new string[0]);
            }
        }

        private async void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn && BotManager.Instance.lstCommands.Items.Count > 0)
            {
                if (!BotManager.Instance.IsHandleCreated)
                {
                    ShowForm(BotManager.Instance);
                    await Task.Delay(10);
                    BotManager.Instance.Hide();
                }
                BotManager.Instance.btnBotStop.Enabled = false;
                BotManager.Instance.btnBotPause.Enabled = false;
                BotManager.Instance.CustomCommandToggle(false);
                BotManager.Instance.SelectionModeToggle(false);
                BotManager.Instance.OnBotExecute(true);
                BotManager.Instance.BotStateChanged(IsRunning: true);
                await Task.Delay(2000);
                this.BotStateChanged(IsRunning: true);
                BotManager.Instance.btnBotPause.Enabled = true;
                BotManager.Instance.btnBotStop.Enabled = true;
            }
        }

        private async void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BotManager.Instance.btnBotStart.Enabled = false;
            this.stopToolStripMenuItem.Enabled = false;
            BotManager.Instance.ActiveBotEngine.Stop();
            BotManager.Instance.CustomCommandToggle(true);
            BotManager.Instance.SelectionModeToggle(true);
            BotManager.Instance.BotStateChanged(IsRunning: false);
            await Task.Delay(2000);
            this.BotStateChanged(IsRunning: false);
            BotManager.Instance.btnBotStart.Enabled = true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                {
                    cp.Style |= 0x20000 | 0x80000 | 0x40000; //WS_MINIMIZEBOX | WS_SYSMENU | WS_SIZEBOX;
                }
                return cp;
            }
        }

        private void grimliteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(AboutForm.Instance);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "This opens a new tab on your default browser. Proceed?", "Get Bots (auqw.tk)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://auqw.tk/");
            }
        }

        private void FPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FPSToolStripMenuItem.Checked)
            {
                Flash.Call("SetFPS", FPSToolStripMenuItem.DropDownItems[0].Text);
            }
            else
            {
                Flash.Call("SetFPS", 24);
            }
        }

        private void getBotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "This opens a new tab on your default browser. Proceed?", "Get Bots (auqw.tk)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://auqw.tk/");
            }
        }

        private void pluginAdded(object sender, ToolStripItemEventArgs e)
        {
            pluginsStrip.DropDownItems.Add(e.Item);
        }

        private void pluginRemoved(object sender, ToolStripItemEventArgs e)
        {
            pluginsStrip.DropDownItems.Remove(e.Item);
        }

        private void managerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(BotManager.Instance);
        }

        private void googleDocsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "This opens a new tab on your default browser. Proceed?", "Grimoire Suggestion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://docs.google.com/document/d/1sUcCRi-GhKPdJXqt3EmU4PeNuG2LFA3ipmr3QDa2oxU/edit#");
            }
        }

        private void googleFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "This opens a new tab on your default browser. Proceed?", "Grimoire Suggestion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSetfV9zl18G9s7w_XReJ1yJNT9aZwxB1FLzU0l1UhdmXv5rIw/viewform?usp=sf_link");
            }
        }

        private void dPSMeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(DPSForm.Instance);
        }

        private void commandeditornodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(NodeEditor.Instance);
        }

        private void loadBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BotManager.Instance.btnLoad_Click(sender, e);
        }

        private void pluginsStrip_Click(object sender, EventArgs e)
        {
            if (!pluginsStrip.HasDropDownItems)
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "No plugins were found. In order to use the plugins, you may have to load them first\r\n" +
       "from the Plugin Manager (which is in Tools' dropdown list on the main menu).", "Plugin Manager", MessageBoxIcon.Error);
            }
        }

        private async void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AutoRelogin.IsTemporarilyKicked)
            {
                loginBoxToggle(false);
                login_cts = new CancellationTokenSource();
                if (Player.IsLoggedIn)
                {
                    Player.Logout();
                    await BotManager.Instance.ActiveBotEngine.WaitUntil(() => AutoRelogin.LoginLabel, () => !login_cts.IsCancellationRequested, 5, 1500);
                }
                AutoRelogin.LoginExecute();
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !AutoRelogin.IsClientLoading("Account"), () => !login_cts.IsCancellationRequested, 10, 500);
                loginBoxToggle(true);
            }
        }

        private async void toolStripComboBoxLoginServer_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxLoginServer.SelectedIndex == -1)
            {
                return;
            }
            if (AutoRelogin.AreServersLoaded)
            {
                loginBoxToggle(false);
                login_cts = new CancellationTokenSource();
                if (Player.IsLoggedIn)
                {
                    Player.Logout();
                    await BotManager.Instance.ActiveBotEngine.WaitUntil(() => AutoRelogin.LoginLabel, () => !login_cts.IsCancellationRequested, 5, 1500);
                }
                if (!AutoRelogin.ServerLabel)
                {
                    AutoRelogin.LoginExecute();
                    await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !AutoRelogin.IsClientLoading("Account"), () => !login_cts.IsCancellationRequested, 10, 500);
                }
                try
                {
                    await AutoRelogin.ForceLogin((Server)toolStripComboBoxLoginServer.SelectedItem, login_cts);
                }
                catch { }
                loginBoxToggle(true);
            }
        }

        public void loginBoxToggle(bool Type)
        {
            loginToolStripMenuItem.Enabled = Type;
            toolStripComboBoxLoginServer.Enabled = Type;
        }

        public void serverCatch()
        {
            toolStripComboBoxLoginServer.SelectedIndex = int.Parse(BotClientConfig.Instance.GetValue<string>("serverIndex") ?? "0");
            toolStripComboBoxLoginServer.SelectedItem = toolStripComboBoxLoginServer.SelectedIndex;
        }

        public void pingMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbPing.Visible = !rtbPing.Visible;
            if (rtbPing.Visible)
            {
                DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Do you wish to save this setting?", "Save Config", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    BotClientConfig.Instance.SetValue("pingMonitor", rtbPing.Visible.ToString());
                }
            }
        }

        public void pingMonitorToggle()
        {
            rtbPing.Visible = BotClientConfig.Instance.GetValue<bool>("pingMonitor");
        }

        public void Root_MenuChanged()
        {
            if (this.ClientSize.Width > 960 || this.ClientSize.Height > 575)
            {
                return;
            }
            if (this.panel1.Visible)
            {
                this.panel1.Visible = false;
                this.panel1.Size = new System.Drawing.Size(0, 0);
                this.panel1.Anchor = this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
                this.GamePanel.Dock = DockStyle.Fill;
                this.ClientSize = new System.Drawing.Size(960, 551);
                this.prgLoader.Location = new System.Drawing.Point(12, 252);
            }
            else
            {
                this.panel1.Visible = true;
                this.panel1.Size = new System.Drawing.Size(960, 24);
                this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
                this.GamePanel.Dock = DockStyle.None;
                this.GamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
                this.GamePanel.Location = new System.Drawing.Point(0, 24);
                this.ClientSize = new System.Drawing.Size(960, 575);
                this.GamePanel.Size = new System.Drawing.Size(960, 551);
                this.prgLoader.Location = new System.Drawing.Point(12, 276);
            }
        }

        public void clientHeaderToggle()
        {
            if (BotClientConfig.Instance.GetValue<bool>("clientHeaderToggle"))
            {
                this.Text = "";
                this.Icon = ((System.Drawing.Icon)(Resources.aeIcon));
            }
        }

        private bool customTravel { get; set; } = true;
        private bool travelInProgress { get; set; } = false;

        private async void customTravel_KeyPress(object sender, KeyEventArgs e)
        {
            if (Travel.Instance.chkCustomHotkeys.Checked && ModifierKeys == Keys.Shift)
            {
                if (!Player.IsLoggedIn)
                {
                    return;
                }
                if (Travel.Instance.travels.Count == 0)
                {
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                        "Failed to execute a Custom Travel. Please make sure that you have a pre-existing list.", "Custom Travels", MessageBoxIcon.Error);
                    return;
                }
                switch (e.KeyCode)
                {
                    case Keys.Right:
                        if (!travelInProgress)
                        {
                            World.GameMessage($"Executing the {Travel.Instance.travels[Travel.Instance.cbCustomTravels.SelectedIndex].Split('`')[0]} Travel in 2 seconds.");
                            await Task.Delay(2000);
                            if (!customTravel)
                            {
                                return;
                            }
                            Travel.Instance.executeCustomTravel();
                        }
                        break;
                    case Keys.Up:
                        if (!travelInProgress && Travel.Instance.cbCustomTravels.SelectedIndex <= (Travel.Instance.cbCustomTravels.Items.Count - 1) && Travel.Instance.cbCustomTravels.SelectedIndex >= 1)
                        {
                            travelInProgress = true;
                            Travel.Instance.cbCustomTravels.SelectedIndex = Travel.Instance.cbCustomTravels.SelectedIndex - 1;
                            string[] newTravel = Travel.Instance.travels[Travel.Instance.cbCustomTravels.SelectedIndex].Split('`');
                            World.GameMessage($"Changing the travel target to {newTravel[0]} Travel.");
                            travelInProgress = false;
                        }
                        break;
                    case Keys.Down:
                        if (!travelInProgress && Travel.Instance.cbCustomTravels.SelectedIndex < (Travel.Instance.cbCustomTravels.Items.Count - 1))
                        {
                            travelInProgress = true;
                            Travel.Instance.cbCustomTravels.SelectedIndex = Travel.Instance.cbCustomTravels.SelectedIndex + 1;
                            string[] newTravel = Travel.Instance.travels[Travel.Instance.cbCustomTravels.SelectedIndex].Split('`');
                            World.GameMessage($"Changing the travel target to {newTravel[0]} Travel.");
                            travelInProgress = false;
                        }
                        break;
                    case Keys.Left:
                        if (!travelInProgress)
                        {
                            travelInProgress = true;
                            customTravel = false;
                            World.GameMessage($"Travel execution has been canceled. Enabling the travel hotkeys back in 3 seconds.");
                            await Task.Delay(3000);
                            customTravel = true;
                            travelInProgress = false;
                            World.GameMessage($"Travel hotkeys has been enabled again.");
                        }
                        break;
                }
                e.Handled = true;
            }
        }

        public void triggerToggle()
        {
            BotManager.Instance.chkHideYulgarPlayers.Checked = BotClientConfig.Instance.GetValue<bool>("hideYulgarPlayers");
            BotManager.Instance.chkAntiAfk.Checked = BotClientConfig.Instance.GetValue<bool>("antiAFK");
            BotManager.Instance.chkChangeRoomTag.Checked = BotClientConfig.Instance.GetValue<bool>("anonymousRoom");
            BotManager.Instance.chkChangeChat.Checked = BotClientConfig.Instance.GetValue<bool>("anonymousUser");
            BotManager.Instance.chkExitCombatUponStop.Checked = BotClientConfig.Instance.GetValue<bool>("exitCombatUponStop");
            Travel.Instance.chkCustomHotkeys.Checked = BotClientConfig.Instance.GetValue<bool>("customTravelHotkeys");
            Travel.Instance.chkCustomChatTrigger.Checked = BotClientConfig.Instance.GetValue<bool>("customTravelTrigger");
            Travel.Instance.scanCustomTravel();
            if (Travel.Instance.travels.Count > 0)
            {
                Travel.Instance.cbCustomTravels.SelectedIndex = 0;
            }
        }

        public static List<Form> MainForms = new List<Form>()
        {
            Root.Instance,
            AboutForm.Instance,
            BankForm.Instance,
            BotManager.Instance,
            CommandColorForm.Instance,
            CosmeticForm.Instance,
            EyeDropper.Instance,
            Hotkeys.Instance,
            Loaders.Instance,
            LogForm.Instance,
            Notepad.Instance,
            PacketLogger.Instance,
            PacketSpammer.Instance,
            PacketTamperer.Instance,
            PluginManager.Instance,
            RawCommandEditor.Instance,
            Travel.Instance,
            UserFriendlyCommandEditor.Instance
        };

    }
}
