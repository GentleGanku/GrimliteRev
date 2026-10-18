using AxShockwaveFlashObjects;
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
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Properties;

namespace Grimoire.UI
{
    public partial class Root : DarkForm
    {
        #region Fields

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public static List<Form> MainForms = new List<Form>()
        {
            Root.Instance,
            AboutForm.Instance,
            BotManager.Instance,
            CommandColorForm.Instance,
            CosmeticForm.Instance,
            Hotkeys.Instance,
            Loaders.Instance,
            LogForm.Instance,
            PacketLogger.Instance,
            PacketSpammer.Instance,
            PacketTamperer.Instance,
            PluginManager.Instance,
            RawCommandEditor.Instance,
            Travel.Instance,
            UserFriendlyCommandEditor.Instance
        };

        public CancellationTokenSource login_cts;
        private System.Threading.CancellationTokenSource _cts;
        private AxShockwaveFlash Game;

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

        // Tracked separately because panel1.Visible reads false until the window is shown, and the constructor applies Camouflage before that.
        private bool menuVisible = true;

        private Icon defaultIcon;
        private Icon defaultTrayIcon;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates the main window. The NewBadge.Attach list holds the menu items that changed in this version; replace
        /// it on every release.
        /// </summary>
        public Root()
        {
            InitializeComponent();
            PreloadedPlugins = new PluginManager();
            Instance = this;
            pingMonitorToggle();
            defaultIcon = Icon;
            defaultTrayIcon = nTray.Icon;
            camouflageToolStripMenuItem.Checked = BotClientConfig.Instance.GetValue<bool>("clientHeaderToggle");
            ApplyCamouflage(camouflageToolStripMenuItem.Checked);
            triggerToggle();
            NewBadge.Attach(grimliteToolStripMenuItem, botToolStripMenuItem, managerToolStripMenuItem, settingsToolStripMenuItem, camouflageToolStripMenuItem, discordToolStripMenuItem, supportMeToolStripMenuItem);
        }

        #endregion

        #region Properties

        public static Root Instance
        {
            get;
            private set;
        }

        public static string PluginsPath
        {
            get;
            private set;
        } = Path.Combine(System.Windows.Forms.Application.StartupPath, "Plugins");

        public static PluginManager PreloadedPlugins
        {
            get;
            private set;
        }

        public static bool LightMode
        {
            get;
            set;
        } = false;

        public static bool LeanMode
        {
            get;
            set;
        } = false;

        public AxShockwaveFlash Client
        {
            get
            {
                return Game;
            }
        }

        /// <summary>
        /// Adds the minimize box, system menu and sizing border to the window style (WS_MINIMIZEBOX 0x20000, WS_SYSMENU
        /// 0x80000, WS_SIZEBOX 0x40000).
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                {
                    cp.Style |= 0x20000 | 0x80000 | 0x40000;
                }
                return cp;
            }
        }

        private bool customTravel
        {
            get;
            set;
        } = true;

        private bool travelInProgress
        {
            get;
            set;
        } = false;

        #endregion

        #region Methods

        [DllImportAttribute("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

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
                startToolStripMenuItem.Enabled = BotManager.Instance.lstCommands.Items.Count > 0;
                loadBotToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
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
            SetMenuVisible(!menuVisible);
        }

        /// <summary>
        /// Shows or hides the menu bar. A normal window grows or shrinks by the menu's height so the game keeps its
        /// size; a maximized one keeps its size.
        /// </summary>
        public void SetMenuVisible(bool visible)
        {
            if (menuVisible == visible)
            {
                return;
            }
            menuVisible = visible;
            int menuHeight = panel1.Height;
            panel1.Visible = visible;
            GamePanel.Dock = visible ? DockStyle.None : DockStyle.Fill;
            if (WindowState == FormWindowState.Normal)
            {
                ClientSize = new Size(ClientSize.Width, ClientSize.Height + (visible ? menuHeight : -menuHeight));
            }
            if (visible)
            {
                GamePanel.SetBounds(0, menuHeight, ClientSize.Width, ClientSize.Height - menuHeight);
                GamePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            prgLoader.Top += visible ? menuHeight : -menuHeight;
        }

        // Camouflage makes the client look like the official Artix Games Launcher: no title, the launcher icon on the window
        // and tray, the game's own backgrounds (-1 in the bridge's getBackgroundIndex and setBackgroundIndex), and no main menu.
        // The menu holds Camouflage's own toggle, so Ctrl+Shift+M (ProcessCmdKey) shows and hides it while Camouflage is on.
        public void ApplyCamouflage(bool enabled)
        {
            this.Text = enabled ? "" : "Grimlite Rev";
            this.Icon = enabled ? Resources.aeIcon : defaultIcon;
            nTray.Icon = enabled ? Resources.aeIcon : defaultTrayIcon;
            nTray.Text = enabled ? "" : "Grimlite Rev";
            Flash.Call("setBackgroundIndex", enabled ? -1 : 0);
            SetMenuVisible(!enabled);
        }

        public void triggerToggle()
        {
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

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift | Keys.M) && camouflageToolStripMenuItem.Checked)
            {
                SetMenuVisible(!menuVisible);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Root_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            Hotkeys.Instance.LoadHotkeys();
            Directory.CreateDirectory(PluginsPath);
            PreloadedPlugins.LoadRange(Directory.GetFiles(PluginsPath));
            InitializeGame();
            Proxy.InitializeProxyHandlers();
            Task.Factory.StartNew(Proxy.Instance.ClientExecute, TaskCreationOptions.LongRunning);
        }

        private async void Root_Shown(object sender, EventArgs e)
        {
            Program.CheckForFlashPlayer();
            await Program.CheckForLatestBuild();
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

        private void discordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "This opens a new tab on your default browser. Proceed?", "Discord Server", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://discord.gg/NZ6vmDTu44");
            }
        }

        private void supportMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "This opens a new tab on your default browser. Proceed?", "Support Me!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://ko-fi.com/gentleganku");
            }
        }

        private void setsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Set.Instance);
        }

        private void nTray_MouseClick(object sender, MouseEventArgs e)
        {
            ShowForm(this);
        }

        private void logsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(LogForm.Instance);
        }

        private void infRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.MenuOptions.InfiniteRange = infRangeToolStripMenuItem.Checked;
            OptionsManager.Refresh();
        }

        private async void provokeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OptionsManager.MenuOptions.ProvokeMonsters = provokeToolStripMenuItem1.Checked;
            OptionsManager.Refresh();
        }

        private async void provokeToolStripMenuItem1_Clicked(object sender, EventArgs e)
        {
            if (!provokeToolStripMenuItem1.Checked && (Player.CurrentState == Player.State.InCombat))
            {
                BotUtilities.MoveToSelfCell();
            }
        }

        private async void enemyMagnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.MenuOptions.EnemyMagnet = enemyMagnetToolStripMenuItem.Checked;
            OptionsManager.Refresh();
        }

        private void lagKillerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.MenuOptions.LagKiller = lagKillerToolStripMenuItem.Checked;
            OptionsManager.Refresh();
        }

        private async void skipCutscenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.MenuOptions.SkipCutscenes = skipCutscenesToolStripMenuItem.Checked;
            OptionsManager.Refresh();
        }

        private void walkSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsManager.MenuOptions.WalkSpeed = walkSpeedToolStripMenuItem.Checked ? 40 : 8;
            OptionsManager.Refresh();
        }

        private void MenuMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Start is only enabled while no bot runs and the command list has commands. It is checked each time the Bot
        /// menu opens, so commands added or removed anywhere, plugins included, are counted.
        /// </summary>
        private void botToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            startToolStripMenuItem.Enabled = !BotManager.Instance.ActiveBotEngine.IsRunning && BotManager.Instance.lstCommands.Items.Count > 0;
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
            BotManager.Instance.UpdateStartEnabled();
        }

        private void grimliteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(AboutForm.Instance);
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
                catch (Exception ex)
                {
                    ErrorReporter.Log("logging in to the selected server", ex);
                }
                loginBoxToggle(true);
            }
        }

        private void camouflageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BotClientConfig.Instance.SetValue("clientHeaderToggle", camouflageToolStripMenuItem.Checked.ToString());
            ApplyCamouflage(camouflageToolStripMenuItem.Checked);
            if (camouflageToolStripMenuItem.Checked)
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "The main menu is now hidden. Press Ctrl+Shift+M to show or hide it while Camouflage is on. To turn Camouflage off, show the menu and uncheck Settings > Camouflage.", "Camouflage", MessageBoxIcon.Information);
            }
        }

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

        #endregion
    }
}
