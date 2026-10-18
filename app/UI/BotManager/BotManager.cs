using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.Utils;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager : DarkForm
    {
        #region Fields

        public static LogForm Log;
        public Dictionary<string, Color> CurrentColors = new Dictionary<string, Color>();
        public Dictionary<string, bool> CurrentCentered = new Dictionary<string, bool>();
        public int LastIndexedSearch = 0;
        public string SKeyword = "";
        public List<int> Filtered = new List<int>();

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            SerializationBinder = BotFileBinder.Instance
        };

        private readonly JsonSerializerSettings _saveSerializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            SerializationBinder = BotFileBinder.Instance
        };

        // The same object as Bot.Instance, so settings and counters written through either one reach the running bot.
        private IBotEngine _activeBotEngine = Bot.Instance;
        private List<StatementCommand> _statementCommands;
        private Dictionary<string, string> _defaultControlText;
        private Panel[] _panels;
        private string _customName;
        private string _customGuild;
        private string _customClassName;
        private DarkLabel lblPackets;
        private bool customCommandsEnabled = true;

        #endregion

        #region Constructors

        public BotManager()
        {
            InitializeComponent();
        }

        static BotManager()
        {
            Instance = new BotManager();
            Log = new LogForm();
        }

        #endregion

        #region Properties

        public static BotManager Instance
        {
            get;
        }

        public IBotEngine ActiveBotEngine
        {
            get
            {
                return _activeBotEngine;
            }
            set
            {
                if (_activeBotEngine.IsRunning)
                {
                    throw new InvalidOperationException("Cannot set a new bot engine while the current one is running");
                }

                _activeBotEngine = value ?? throw new ArgumentNullException("value");
            }
        }

        public string CustomName
        {
            get
            {
                return _customName;
            }

            set
            {
                _customName = value;
                Flash.Call("ChangeName", _customName);
            }
        }

        public string CustomGuild
        {
            get
            {
                return _customGuild;
            }
            set
            {
                _customGuild = value;
                Flash.Call("ChangeGuild", txtGuild.Text);
            }
        }

        public string CustomClassName
        {
            get
            {
                return _customClassName;
            }

            set
            {
                _customClassName = value;
                Flash.Call("ChangeClassName", _customClassName);
            }
        }

        // Draws the command list with the custom command colors when enabled; the link is green when on, grey when off.
        public bool CustomCommandsEnabled
        {
            get
            {
                return customCommandsEnabled;
            }
            set
            {
                customCommandsEnabled = value;
                ControlInvoke(lnkCustomCommands, () =>
                {
                    Color color = value ? Color.LimeGreen : Color.DarkGray;
                    lnkCustomCommands.LinkColor = color;
                    lnkCustomCommands.ActiveLinkColor = color;
                    lnkCustomCommands.VisitedLinkColor = color;
                    lstCommands.Invalidate();
                });
            }
        }

        private ListBox SelectedList
        {
            get
            {
                switch (cbLists.SelectedIndex)
                {
                    case 1:
                        return lstSkills;

                    case 2:
                        return lstQuests;

                    case 3:
                        return lstDrops;

                    case 4:
                        return lstBoosts;

                    case 5:
                        return lstItems;

                    default:
                        return lstCommands;
                }
            }
        }

        #endregion

        #region Methods

        public void BotManager_Load(object sender, EventArgs e)
        {
            lstBoosts.DisplayMember = "Text";
            lstQuests.DisplayMember = "Text";
            lstSkills.DisplayMember = "Text";
            cbBoosts.DisplayMember = "Name";
            cbServers.DisplayMember = "Name";
            lstItems.DisplayMember = "Text";
            cbStatement.DisplayMember = "Text";
            cbLists.SelectedIndex = 0;
            _statementCommands = JsonConvert.DeserializeObject<List<StatementCommand>>(Resources.statementcmds, _serializerSettings);
            _defaultControlText = JsonConvert.DeserializeObject<Dictionary<string, string>>(Resources.defaulttext, _serializerSettings);
            string font = Config.Instance.GetValue<string>("font");
            lstCommands.ItemHeight = int.Parse(Config.Instance.GetValue<string>("CommandsSize") ?? "60");
            lstCommands.Font = new Font(font, lstCommands.ItemHeight / 4 - (float)6.5, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstCommands.ItemHeight = lstCommands.ItemHeight / 4;
            if (font != null)
            {
                Font = new Font(font, 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
            foreach (DarkTextBox nameBox in new[] { txtMonster, txtMonsterSkillCmd, txtKillFMon, txtKillFItem, txtItem, txtWhitelist, txtShopItem, txtSwapInv, txtSwapBank, txtSoundItem })
            {
                nameBox.TextChanged += NameBox_TextChanged;
            }
        }

        public void OnServersLoaded(Server[] servers)
        {
            if (servers != null && servers.Length != 0 && cbServers.Items.Count <= 1)
            {
                Server.List = servers.ToList();
                cbServers.Items.AddRange(servers);
                cbServers.SelectedIndex = 0;
                Root.Instance.toolStripComboBoxLoginServer.Items.AddRange(servers);
                Root.Instance.serverCatch();
            }
        }

        public void OnConfigurationChanged(Configuration config)
        {
            if (InvokeRequired)
            {
                Invoke((Action)delegate
                {
                    ApplyConfiguration(config);
                });
            }
            else
            {
                ApplyConfiguration(config);
            }
        }

        public void OnIndexChanged(int index)
        {
            if (index > -1)
            {
                if (InvokeRequired)
                {
                    Invoke((Action)delegate
                    {
                        lstCommands.SelectedIndex = index;
                    });
                }
                else
                {
                    lstCommands.SelectedIndex = index;
                }
            }
        }

        public void OnSkillIndexChanged(int index)
        {
            if (index > -1 && index < lstSkills.Items.Count)
            {
                Invoke((Action)delegate
                {
                    lstSkills.SelectedIndex = index;
                });
            }
        }

        public void OnIsRunningChanged(bool IsRunning)
        {
            Invoke((Action)delegate
            {
                if (!IsRunning)
                {
                    ActiveBotEngine.IsRunningChanged -= OnIsRunningChanged;
                    ActiveBotEngine.IndexChanged -= OnIndexChanged;
                    ActiveBotEngine.ConfigurationChanged -= OnConfigurationChanged;
                }
                BotStateChanged(IsRunning);
            });
        }

        public async void btnBotStart_ClickAsync(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn && lstCommands.Items.Count > 0)
            {
                btnBotStop.Enabled = false;
                btnBotPause.Enabled = false;
                CustomCommandToggle(false);
                SelectionModeToggle(false);
                OnBotExecute(true);
                BotStateChanged(IsRunning: true);
                await Task.Delay(2000);
                Root.Instance.BotStateChanged(IsRunning: true);
                btnBotPause.Enabled = true;
                btnBotStop.Enabled = true;
            }
        }

        public async void btnBotStop_Click(object sender, EventArgs e)
        {
            btnBotStart.Enabled = false;
            CustomCommandToggle(true);
            ActiveBotEngine.Stop();
            SelectionModeToggle(true);
            BotStateChanged(IsRunning: false);
            await Task.Delay(2000);
            Root.Instance.BotStateChanged(IsRunning: false);
            UpdateStartEnabled();
        }

        public void BotStateChanged(bool IsRunning)
        {
            if (IsRunning)
            {
                btnBotStart.Hide();
                btnBotStop.Show();
                btnBotPause.Show();
                btnBotResume.Show();
            }
            else
            {
                btnBotStop.Hide();
                btnBotPause.Hide();
                btnBotResume.Hide();
                btnBotStart.Show();
            }
            btnUp.Enabled = !IsRunning;
            btnDown.Enabled = !IsRunning;
            btnClear.Enabled = !IsRunning;
            btnRemove.Enabled = !IsRunning;
            btnSearchCmd.Enabled = !IsRunning;
            txtSearchCmd.Enabled = !IsRunning;
            btnLoad.Enabled = !IsRunning;
        }

        /// <summary>
        /// Enables Start only while no bot runs and the command list has commands. Called after every change to the
        /// command list; the main menu's Start checks the same when the Bot menu opens.
        /// </summary>
        public void UpdateStartEnabled()
        {
            btnBotStart.Enabled = !ActiveBotEngine.IsRunning && lstCommands.Items.Count > 0;
        }

        public void ControlInvoke(Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public void ButtonToggle(bool Check)
        {
            btnUp.Enabled = Check;
            btnDown.Enabled = Check;
            btnClear.Enabled = Check;
            btnRemove.Enabled = Check;
            btnSearchCmd.Enabled = Check;
            txtSearchCmd.Enabled = Check;
        }

        public void OnBotExecute(bool Type)
        {
            ActiveBotEngine.IsRunningChanged += OnIsRunningChanged;
            ActiveBotEngine.IndexChanged += OnIndexChanged;
            ActiveBotEngine.ConfigurationChanged += OnConfigurationChanged;
            if (Type)
            {
                ActiveBotEngine.Start(GenerateConfiguration());
            }
            else
            {
                ActiveBotEngine.Resume(GenerateConfiguration());
            }
        }

        private void TextboxEnter(object sender, EventArgs e)
        {
            DarkTextBox textBox = (DarkTextBox)sender;
            textBox.Clear();
        }

        private void TextboxLeave(object sender, EventArgs e)
        {
            DarkTextBox textBox = (DarkTextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text) && _defaultControlText.TryGetValue(textBox.Name, out string value))
            {
                textBox.Text = value;
            }
        }

        /// <summary>
        /// Straightens curly apostrophes and quotes as they are typed or pasted into a monster or item name box. Each
        /// replacement is one character for one, so the caret stays where it was.
        /// </summary>
        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            DarkTextBox textBox = (DarkTextBox)sender;
            string unified = textBox.Text.UnifyQuotes();
            if (unified != textBox.Text)
            {
                int caret = textBox.SelectionStart;
                textBox.Text = unified;
                textBox.SelectionStart = caret;
            }
        }

        private void BotManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.FormOwnerClosing)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }
                e.Cancel = true;
                Hide();
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void GetAllCommands<T>(ListBox lb)
        {
            lb.Items.Clear();
            T[] allItems = lstCommands.Items.OfType<T>().ToArray();
            string[] allStrings = new string[allItems.Count()];
            for (int i = 0; i < allItems.Count(); i++)
                allStrings[i] = allItems[i].ToString();
            lb.Items.AddRange(allStrings);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabBots)
            {
                this.txtSaved.Text = Config.Instance.GetValue<string>("botsDirectory") ?? Path.Combine(Application.StartupPath, "Bots");
                UpdateTree();
            }
            else if (e.TabPage == tabMisc)
            {
                GetAllCommands<CmdLabel>(lbLabels);
            }
        }

        private void TogglePages()
        {
            Size p1 = splitContainer2.Panel1.ClientSize;
            int p1w = splitContainer2.Panel1.ClientSize.Width;
            Size p2 = splitContainer2.Panel2.ClientSize;
            int p2w = splitContainer2.Panel2.ClientSize.Width;
            if (mainTabControl.Visible)
            {
                this.ClientSize = new Size(p1w, ClientSize.Height);
                mainTabControl.Visible = false;
                splitContainer2.Panel2Collapsed = true;
            }
            else
            {
                this.ClientSize = new Size(p1w + 530, ClientSize.Height);
                splitContainer2.Panel2Collapsed = false;
                mainTabControl.Visible = true;
            }
        }

        private void toggleTabpagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TogglePages();
        }

        private async void btnBotPause_Click(object sender, EventArgs e)
        {
            btnBotPause.Enabled = false;
            btnBotResume.Enabled = false;
            btnBotPause.Visible = false;
            btnBotResume.Visible = true;
            CustomCommandToggle(true);
            ActiveBotEngine.Pause();
            ButtonToggle(true);
            SelectionModeToggle(true);
            await Task.Delay(2000);
            btnBotResume.Enabled = true;
        }

        private async void btnBotResume_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn && lstCommands.Items.Count > 0)
            {
                btnBotResume.Enabled = false;
                btnBotPause.Enabled = false;
                btnBotStop.Enabled = false;
                btnBotPause.Visible = true;
                btnBotResume.Visible = false;
                CustomCommandToggle(false);
                ButtonToggle(false);
                SelectionModeToggle(false);
                OnBotExecute(false);
                await Task.Delay(2000);
                btnBotPause.Enabled = true;
                btnBotStop.Enabled = true;
            }
        }

        private void lnkShortcuts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Root.Instance.ShowForm(ShortcutsForm.Instance);
        }

        #endregion
    }
}