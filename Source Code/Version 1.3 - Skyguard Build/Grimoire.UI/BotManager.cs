using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Botting.Commands.Item;
using Grimoire.Botting.Commands.Map;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Tools;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Grimoire.Botting.Commands.Item.CmdWhitelist;
using Extensions = Grimoire.Botting.Extensions;


namespace Grimoire.UI
{
    public class BotManager : DarkForm
    {
        private IBotEngine _activeBotEngine = new Bot();

        private List<StatementCommand> _statementCommands;

        private Dictionary<string, string> _defaultControlText;

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        };

        private readonly JsonSerializerSettings _saveSerializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        };

        #region Definitions
        private IContainer components;
        private Panel[] _panels;
        public ListBox lstCommands;
        private ListBox lstSkills;
        public ListBox lstQuests;
        public ListBox lstDrops;
        private ListBox lstBoosts;
        public static LogForm Log;
        private string _customName;
        private string _customGuild;
        private string _customClassName;
        private ListBox lstItems;
        public ListBox lstPackets;
        private VisualStudioTabControl.VisualStudioTabControl mainTabControl;
        private TabPage tabCombat;
        private DarkButton btnUseSkillSet;
        private DarkButton btnAddSkillSet;
        private DarkTextBox txtSkillSet;
        private DarkCheckBox chkSafeMP;
        private DarkButton btnRest;
        private DarkButton btnRestF;
        private DarkCheckBox chkSkillCD;
        private DarkLabel label12;
        private DarkLabel label11;
        private DarkLabel label10;
        private DarkButton btnKill;
        private DarkLabel label13;
        private DarkCheckBox chkExistQuest;
        private DarkNumericUpDown numRestMP;
        private DarkCheckBox chkMP;
        private DarkNumericUpDown numRest;
        private DarkCheckBox chkHP;
        private DarkNumericUpDown numSkillD;
        private DarkLabel label2;
        private DarkNumericUpDown numSafe;
        private DarkNumericUpDown numSkillCmd;
        private DarkCheckBox chkExitRest;
        private DarkCheckBox chkAllSkillsCD;
        private DarkTextBox txtKillFQ;
        private DarkTextBox txtKillFItem;
        private DarkTextBox txtKillFMon;
        private DarkRadioButton rbTemp;
        private DarkRadioButton rbItems;
        private DarkButton btnKillF;
        private DarkTextBox txtMonster;
        private TabPage tabMap;
        private DarkButton btnWalkCur;
        private DarkButton btnWalk;
        private DarkNumericUpDown numWalkY;
        private DarkNumericUpDown numWalkX;
        private DarkButton btnCellSwap;
        private DarkButton btnJump;
        private DarkButton btnCurrCell;
        private DarkTextBox txtPad;
        private DarkTextBox txtCell;
        private DarkButton btnJoin;
        private DarkTextBox txtJoinPad;
        private DarkTextBox txtJoinCell;
        private DarkTextBox txtJoin;
        private TabPage tabQuest;
        private DarkButton btnQuestAccept;
        private DarkButton btnQuestComplete;
        private DarkButton btnQuestAdd;
        private DarkCheckBox chkQuestItem;
        private DarkLabel label4;
        private TabPage tabMisc;
        private DarkCheckBox chkRestartDeath;
        private DarkCheckBox chkMerge;
        private DarkButton button2;
        private DarkButton btnLogout;
        private DarkTextBox txtDescription;
        private DarkTextBox txtAuthor;
        private DarkButton btnDelay;
        private DarkNumericUpDown numDelay;
        private DarkLabel label3;
        private DarkNumericUpDown numBotDelay;
        private DarkButton btnBotDelay;
        private DarkTextBox txtPlayer;
        private DarkButton btnGoto;
        private DarkButton btnRestart;
        private DarkButton btnStop;
        private DarkButton btnLoadCmd;
        private DarkCheckBox chkSkip;
        private DarkButton btnStatementAdd;
        private DarkTextBox txtStatement2;
        private DarkTextBox txtStatement1;
        private DarkComboBox cbStatement;
        private DarkComboBox cbCategories;
        private DarkTextBox txtPacket;
        private DarkButton btnServerPacket;
        private TabPage tabOptions;
        private DarkCheckBox chkEnableSettings;
        public DarkCheckBox chkDisableAnims;
        private DarkTextBox txtSoundItem;
        private DarkButton btnSoundAdd;
        private DarkButton btnSoundDelete;
        private DarkButton btnSoundTest;
        private DarkListBox lstSoundItems;
        private DarkLabel label9;
        private DarkNumericUpDown numWalkSpeed;
        private DarkLabel label8;
        public DarkCheckBox chkSkipCutscenes;
        public DarkCheckBox chkHidePlayers;
        public DarkCheckBox chkLag;
        public DarkCheckBox chkMagnet;
        public DarkCheckBox chkProvoke;
        public DarkCheckBox chkInfiniteRange;
        private DarkGroupBox grpLogin;
        public DarkComboBox cbServers;
        private DarkCheckBox chkRelogRetry;
        private DarkCheckBox chkRelog;
        private DarkNumericUpDown numRelogDelay;
        private DarkLabel label7;
        private DarkTextBox txtUsername;
        private DarkTextBox txtGuild;
        private DarkButton btnchangeName;
        private DarkButton btnchangeGuild;
        public DarkCheckBox chkGender;
        private TabPage tabBots;
        private DarkLabel lblBoosts;
        private DarkLabel lblDrops;
        private DarkLabel lblQuests;
        private DarkLabel lblSkills;
        private DarkLabel lblCommands;
        private DarkLabel lblItems;
        private DarkLabel lblPackets;
        private DarkTextBox txtSavedDesc;
        private DarkTextBox txtSavedAuthor;
        private DarkLabel lblBots;
        private TreeView treeBots;
        private DarkTextBox txtSavedAdd;
        private DarkButton btnSavedAdd;
        private DarkTextBox txtSaved;
        private DarkButton btnProvokeOn;
        private DarkButton btnProvokeOff;
        private DarkListBox lstLogText;
        private DarkButton btnLogDebug;
        private DarkButton btnLog;
        private DarkTextBox txtLog;
        public DarkCheckBox chkUntarget;
        private DarkLabel label5;
        private DarkNumericUpDown numOptionsTimer;
        private DarkLabel label6;
        private DarkButton btnWalkRdm;
        private DarkButton btnBlank;
        private DarkCheckBox chkAFK;
        private SplitContainer splitContainer1;
        private DarkButton btnClear;
        private DarkButton btnDown;
        private DarkButton btnRemove;
        private DarkButton btnUp;
        private Panel panel1;
        private SplitContainer splitContainer2;
        private DarkButton btnCurrBlank;
        private DarkButton btnSetSpawn;
        private DarkButton btnBeep;
        private DarkNumericUpDown numBeepTimes;
        private DarkButton btnSkillCmd;
        private TabPage tabItem;
        private DarkCheckBox checkBox1;
        private DarkCheckBox chkBuffup;
        private TabPage tabOptions2;
        private DarkButton btnSetUndecided;
        private DarkButton btnSetChaos;
        private DarkButton btnSetEvil;
        private DarkButton btnSetGood;
        private DarkGroupBox grpAlignment;
        private DarkGroupBox grpAccessLevel;
        private DarkButton btnSetMem;
        private DarkButton btnSetModerator;
        private DarkButton btnSetNonMem;
        private DarkCheckBox chkToggleMute;
        private DarkContextMenu BotManagerMenuStrip;
        private ToolStripMenuItem changeFontsToolStripMenuItem;
        private DarkButton btnGoDownIndex;
        private DarkButton btnGoUpIndex;
        private DarkButton btnGotoIndex;
        private DarkNumericUpDown numIndexCmd;
        private ToolStripMenuItem multilineToggleToolStripMenuItem;
        private ToolStripMenuItem toggleTabpagesToolStripMenuItem;
        private ToolStripMenuItem commandColorsToolStripMenuItem;
        private DarkButton btnChangeNameCmd;
        private DarkButton btnChangeGuildCmd;
        private DarkCheckBox chkAntiAfk;
        private DarkCheckBox chkChangeRoomTag;
        private DarkCheckBox chkChangeChat;
        private DarkButton btnSetJoinLevel;
        private DarkButton btnClientPacket;
        private DarkCheckBox chkHideYulgarPlayers;
        private DarkNumericUpDown numSetInt;
        private DarkTextBox txtSetInt;
        private DarkButton btnSetInt;
        private DarkButton btnDecreaseInt;
        private DarkButton btnIncreaseInt;
        private DarkButton btnSearchCmd;
        public DarkTextBox txtSearchCmd;
        private TabPage tabMisc2;
        private DarkGroupBox groupBox1;
        private DarkButton btnAddInfoMsg;
        private DarkButton btnAddWarnMsg;
        private DarkTextBox inputMsgClient;
        private DarkGroupBox grpPacketlist;
        private DarkButton btnReturnCmd;
        private DarkButton btnClearTempVar;
        private DarkGroupBox darkGroupBox4;
        private DarkCheckBox chkPickupAll;
        public DarkCheckBox chkPickup;
        private DarkCheckBox chkReject;
        public DarkCheckBox chkPickupAcTag;
        private DarkCheckBox chkBankOnStop;
        private DarkCheckBox chkRejectAll;
        private DarkGroupBox darkGroupBox3;
        private DarkNumericUpDown numShopId;
        private DarkButton btnLoadShop;
        private DarkTextBox txtShopItem;
        private DarkButton btnBuy;
        private DarkButton btnBuyFast;
        private DarkGroupBox darkGroupBox2;
        private DarkButton btnWhitelistClear;
        private DarkButton btnWhitelistOn;
        private DarkButton btnWhitelistOff;
        private DarkLabel label1;
        private DarkNumericUpDown numDropDelay;
        private DarkButton btnBoost;
        private DarkComboBox cbBoosts;
        private DarkNumericUpDown numMapItem;
        private DarkButton btnMapItem;
        private DarkButton btnSwap;
        private DarkTextBox txtSwapInv;
        private DarkTextBox txtSwapBank;
        private DarkButton btnWhitelist;
        private DarkButton btnBoth;
        private DarkTextBox txtWhitelist;
        private DarkButton btnItem;
        private DarkButton btnUnbanklst;
        private DarkTextBox txtItem;
        private DarkComboBox cbItemCmds;
        private DarkGroupBox darkGroupBox5;
        private DarkGroupBox darkGroupBox6;
        private TabPage tabInfo;
        private Panel panel5;
        private RichTextBox rtbInfo;
        private DarkGroupBox darkGroupBox7;
        private DarkGroupBox darkGroupBox8;
        private DarkGroupBox darkGroupBox11;
        private DarkListBox lbLabels;
        private DarkGroupBox darkGroupBox10;
        private BackgroundWorker backgroundWorker1;
        private DarkGroupBox darkGroupBox12;
        private DarkGroupBox darkGroupBox13;
        private RichTextBox richTextBox2;
        private DarkPanel darkPanel1;
        private Panel panel6;
        private DarkPanel darkPanel2;
        private DarkGroupBox darkGroupBox14;
        private DarkButton btnAttack;
        #endregion
        private SplitContainer splitContainer5;
        public DarkCheckBox colorfulCommands;
        private DarkGroupBox darkGroupBox9;
        private DarkGroupBox darkGroupBox15;
        private DarkButton cmdSetClientLevel;
        private DarkButton btnAddClientGold;
        private DarkButton btnAddClientACs;
        private DarkGroupBox darkGroupBox16;
        private DarkButton darkButton2;
        private DarkGroupBox darkGroupBox17;
        private DarkButton btnCancelTargetCmd;
        private DarkButton btnCancelAutoAttackCmd;
        private DarkTextBox txtShopItemID;
        private DarkTextBox txtShopID;
        private DarkTextBox txtItemID;
        private DarkButton btnBuyItemByID;
        private DarkButton btnBuyFastByID;
        private DarkLabel darkLabel1;
        private DarkNumericUpDown numSetFPS;
        private DarkButton btnSetFPSCmd;
        private DarkTextBox txtStopBotMessage;
        private DarkButton btnStopBotWithMessageCmd;
        private DarkTextBox txtClassName;
        private DarkButton btnSetCustomClassName;
        private DarkGroupBox darkGroupBox19;
        public DarkCheckBox chkProvokeAllMon;
        private DarkButton btnPacketlistOnCmd;
        private DarkButton btnPacketlistOffCmd;
        private DarkButton btnPacketlistClearCmd;
        private DarkButton btnPacketlistRemoveCmd;
        private DarkButton btnPacketlistSetDelayCmd;
        private DarkLabel txtPlistDelay;
        private DarkNumericUpDown numPacketlistDelay;
        private DarkButton btnPacketlistAddCmd;
        internal DarkTextBox txtPacketlist;
        private SplitContainer splitContainer4;
        public DarkButton btnGotoLabel;
        public DarkButton btnAddLabel;
        public DarkTextBox txtLabel;
        internal DarkTextBox txtCustomAggromon;
        public DarkCheckBox chkInMapCustom;
        private DarkLabel txtInMap;
        private DarkButton btnProvokeInMapOff;
        private DarkButton btnProvokeInMapOn;
        private DarkLabel txtInRoom;
        private DarkLabel darkLabel2;
        private DarkCheckBox chkSafeGreaterThan;
        private DarkCheckBox chkSafeLessThan;
        private DarkCheckBox chkSafeHP;
        private DarkLabel darkLabel3;
        private DarkLabel darkLabel6;
        private DarkLabel darkLabel5;
        private DarkGroupBox darkGroupBox21;
        private DarkGroupBox darkGroupBox20;
        private DarkButton btnQuestlistRemoveCmd;
        private DarkButton btnQuestlistAddCmd;
        internal DarkTextBox txtQuestItemID;
        internal DarkTextBox txtQuestID;
        private DarkLabel darkLabel7;
        internal DarkCheckBox chkQuestlistItemID;
        private DarkLabel txtPacketlistCommands;
        private DarkButton btnPacketlistAdd;
        private DarkCheckBox chkEnablePacketlistSpam;
        private DarkComboBox cbLists;
        public DarkButton btnBotStart;
        public DarkButton btnBotStop;
        private DarkGroupBox darkGroupBox18;
        private DarkGroupBox darkGroupBox1;
        private DarkGroupBox darkGroupBox22;
        private DarkGroupBox darkGroupBox23;
        private DarkGroupBox darkGroupBox24;
        private DarkLabel darkLabel9;
        private DarkLabel darkLabel8;
        private DarkNumericUpDown numPacketDelay;
        private DarkButton btnPacketSpamOffCmd;
        private DarkButton btnPacketSpamOnCmd;
        private DarkLabel darkLabel4;
        private ToolStripMenuItem clearAllCommandListsToolStripMenuItem;
        private Panel panel13;
        private SplitContainer splitContainer6;
        private Panel panel2;
        private Panel panel8;
        private Panel panel9;
        private Panel panel10;
        private Panel panel7;
        private Panel panel4;
        private DarkGroupBox grpMapSwf;
        private DarkTextBox txtMapSwf;
        private DarkButton btnLoadMapSwf;
        private DarkButton btnLoadMapSwfCmd;
        private DarkButton btnQuestlistClearCmd;
        private DarkGroupBox darkGroupBox29;
        private DarkButton btnAddSafe;
        private DarkButton btnAddSkill;
        private DarkNumericUpDown numSkill;
        private DarkGroupBox darkGroupBox28;
        private DarkGroupBox darkGroupBox27;
        private DarkGroupBox darkGroupBox26;
        private DarkGroupBox darkGroupBox25;
        private DarkGroupBox darkGroupBox30;
        private DarkTextBox txtMonsterSkillCmd;
        private DarkLabel txtSkillCmdHPMP;
        private DarkLabel darkLabel10;
        private DarkCheckBox chkSkillCmdWait;
        private DarkNumericUpDown numSkillCmdHPMP;
        public DarkButton btnBotPause;
        public DarkButton btnBotResume;
        internal DarkTextBox numQuestItem;
        internal DarkTextBox numQuestID;
        public DarkButton btnLoad;
        private DarkButton btnSave;
        public DarkCheckBox chkWarningMsgFilter;
        private DarkButton btnReloadMap;
        private DarkButton btnSaveDirectory;
        public DarkCheckBox chkSaveState;

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

        public string CustomName
        {
            get => _customName;

            set
            {
                _customName = value;
                Flash.Call("ChangeName", _customName);
            }
        }

        public string CustomGuild
        {
            get => _customGuild;
            set
            {
                _customGuild = value;
                Flash.Call("ChangeGuild", txtGuild.Text);
            }
        }

        public string CustomClassName
        {
            get => _customClassName;

            set
            {
                _customClassName = value;
                Flash.Call("ChangeClassName", _customClassName);
            }
        }

        public static int SliderValue
        {
            get;
            set;
        }

        public BotManager()
        {
            InitializeComponent();
        }

        public void BotManager_Load(object sender, EventArgs e)
        {
            lstBoosts.DisplayMember = "Text";
            lstQuests.DisplayMember = "Text";
            lstSkills.DisplayMember = "Text";
            cbBoosts.DisplayMember = "Name";
            cbServers.DisplayMember = "Name";
            lstItems.DisplayMember = "Text";
            lstPackets.DisplayMember = "Text";
            cbStatement.DisplayMember = "Text";
            cbLists.SelectedIndex = 0;
            _statementCommands = JsonConvert.DeserializeObject<List<StatementCommand>>(Resources.statementcmds, _serializerSettings);
            _defaultControlText = JsonConvert.DeserializeObject<Dictionary<string, string>>(Resources.defaulttext, _serializerSettings);
            OptionsManager.StateChanged += OnOptionsStateChanged;
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string font = c.Get("font");
            if (font != null)
                Font = new Font(font, 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstCommands.ItemHeight = int.Parse(c.Get("CommandsSize") ?? "60");
            lstCommands.Font = new Font(font, lstCommands.ItemHeight / 4 - (float)6.5, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstCommands.ItemHeight = lstCommands.ItemHeight / 4;
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

        public void OnServersLoaded(Server[] servers)
        {
            if (servers != null && servers.Length != 0 && cbServers.Items.Count <= 1)
            {
                BotClientConfig c = BotClientConfig.Load(System.Windows.Forms.Application.StartupPath + "\\BotClientConfig.cfg");
                string serverIndex;
                try
                {
                    serverIndex = c.Get("serverIndex");
                }
                catch { serverIndex = "-1"; }
                AutoRelogin.Servers(servers);
                cbServers.Items.AddRange(servers);
                cbServers.SelectedIndex = 0;
                Root.Instance.toolStripComboBoxLoginServer.Items.AddRange(servers);
                Root.Instance.toolStripComboBoxLoginServer.SelectedIndex = int.Parse(serverIndex);
            }
        }

        private void MoveListItem(int direction)
        {
            if (SelectedList.SelectedItem != null && SelectedList.SelectedIndex >= 0)
            {
                int num = SelectedList.SelectedIndex + direction;
                if (num >= 0 && num < SelectedList.Items.Count)
                {
                    object selectedItem = SelectedList.SelectedItem;
                    SelectedList.Items.Remove(selectedItem);
                    SelectedList.Items.Insert(num, selectedItem);
                    SelectedList.SetSelected(num, value: true);
                }
            }
        }

        private void MoveListItemByKey(int direction)
        {
            if (SelectedList.SelectedItem == null || SelectedList.SelectedIndex < 0)
            {
                return;
            }
            int num = SelectedList.SelectedIndex + direction;
            if (num >= 0 && num < SelectedList.Items.Count)
            {
                object selectedItem = SelectedList.SelectedItem;
                SelectedList.Items.Remove(selectedItem);
                SelectedList.Items.Insert(num, selectedItem);
                if (direction == 1)
                {
                    SelectedList.SetSelected(num - 1, value: true);
                }
            }
        }

        public Configuration GenerateConfiguration()
        {
            return new Configuration
            {
                Author = txtAuthor.Text,
                Description = rtbInfo.Rtf,
                Commands = lstCommands.Items.Cast<IBotCommand>().ToList(),
                Skills = lstSkills.Items.Cast<Skill>().ToList(),
                Quests = lstQuests.Items.Cast<Quest>().ToList(),
                Boosts = lstBoosts.Items.Cast<InventoryItem>().ToList(),
                Drops = lstDrops.Items.Cast<string>().ToList(),
                Items = lstItems.Items.Cast<string>().ToList(),
                Packets = lstPackets.Items.Cast<string>().ToList(),
                SkillDelay = (int)numSkillD.Value,
                ExitCombatBeforeRest = chkExitRest.Checked,
                ExitCombatBeforeQuest = chkExistQuest.Checked,
                Server = (Server)cbServers.SelectedItem,
                AutoRelogin = chkRelog.Checked,
                RelogDelay = (int)numRelogDelay.Value,
                RelogRetryUponFailure = chkRelogRetry.Checked,
                BotDelay = (int)numBotDelay.Value,
                EnablePacketSpam = false,
                EnablePickup = chkPickup.Checked,
                EnableRejection = chkReject.Checked,
                EnablePickupAll = chkPickupAll.Checked,
                EnablePickupAcTagged = chkPickupAcTag.Checked,
                EnableRejectAll = chkRejectAll.Checked,
                WaitForAllSkills = chkAllSkillsCD.Checked,
                WaitForSkill = chkSkillCD.Checked,
                SkipDelayIndexIf = chkSkip.Checked,
                InfiniteAttackRange = chkInfiniteRange.Checked,
                ProvokeMonsters = chkProvoke.Checked,
                ProvokeAllMonster = chkProvokeAllMon.Checked,
                EnemyMagnet = chkMagnet.Checked,
                LagKiller = chkLag.Checked,
                HidePlayers = chkHidePlayers.Checked,
                SkipCutscenes = chkSkipCutscenes.Checked,
                WalkSpeed = (int)numWalkSpeed.Value,
                NotifyUponDrop = lstSoundItems.Items.Cast<string>().ToList(),
                RestIfMp = chkMP.Checked,
                RestIfHp = chkHP.Checked,
                Untarget = chkUntarget.Checked,
                BankOnStop = chkBankOnStop.Checked,
                RestMp = (int)numRestMP.Value,
                RestHp = (int)numRest.Value,
                RestartUponDeath = chkRestartDeath.Checked,
                AFK = chkAFK.Checked,
                DropDelay = (int)numDropDelay.Value
            };
        }

        public Configuration SaveConfiguration()
        {
            var compressed = DocConvert.Zip<string>(txtDescription.Text);

            return new Configuration
            {
                Author = txtAuthor.Text,
                Description = compressed.Length > txtDescription.Text.Length ? txtDescription.Text : compressed,
                Commands = lstCommands.Items.Cast<IBotCommand>().ToList(),
                Skills = lstSkills.Items.Cast<Skill>().ToList(),
                Quests = lstQuests.Items.Cast<Quest>().ToList(),
                Boosts = lstBoosts.Items.Cast<InventoryItem>().ToList(),
                Drops = lstDrops.Items.Cast<string>().ToList(),
                Items = lstItems.Items.Cast<string>().ToList(),
                Packets = lstPackets.Items.Cast<string>().ToList(),
                SkillDelay = (int)numSkillD.Value,
                ExitCombatBeforeRest = chkExitRest.Checked,
                ExitCombatBeforeQuest = chkExistQuest.Checked,
                Server = (Server)cbServers.SelectedItem,
                AutoRelogin = chkRelog.Checked,
                RelogDelay = (int)numRelogDelay.Value,
                RelogRetryUponFailure = chkRelogRetry.Checked,
                BotDelay = (int)numBotDelay.Value,
                EnablePickup = chkPickup.Checked,
                EnableRejection = chkReject.Checked,
                EnablePickupAll = chkPickupAll.Checked,
                EnablePickupAcTagged = chkPickupAcTag.Checked,
                EnableRejectAll = chkRejectAll.Checked,
                WaitForAllSkills = chkAllSkillsCD.Checked,
                WaitForSkill = chkSkillCD.Checked,
                SkipDelayIndexIf = chkSkip.Checked,
                InfiniteAttackRange = chkInfiniteRange.Checked,
                ProvokeMonsters = chkProvoke.Checked,
                ProvokeAllMonster = chkProvokeAllMon.Checked,
                EnemyMagnet = chkMagnet.Checked,
                LagKiller = chkLag.Checked,
                HidePlayers = chkHidePlayers.Checked,
                SkipCutscenes = chkSkipCutscenes.Checked,
                WalkSpeed = (int)numWalkSpeed.Value,
                NotifyUponDrop = lstSoundItems.Items.Cast<string>().ToList(),
                RestIfMp = chkMP.Checked,
                RestIfHp = chkHP.Checked,
                Untarget = chkUntarget.Checked,
                BankOnStop = chkBankOnStop.Checked,
                RestMp = (int)numRestMP.Value,
                RestHp = (int)numRest.Value,
                RestartUponDeath = chkRestartDeath.Checked,
                AFK = chkAFK.Checked,
                DropDelay = (int)numDropDelay.Value
            };
        }

        public void ApplyConfiguration(Configuration config)
        {
            if (config != null)
            {
                if (!chkMerge.Checked || ActiveBotEngine.IsRunning)
                {
                    lstCommands.Items.Clear();
                    lstBoosts.Items.Clear();
                    lstDrops.Items.Clear();
                    lstQuests.Items.Clear();
                    lstSkills.Items.Clear();
                    lstItems.Items.Clear();
                    lstSoundItems.Items.Clear();
                    lstPackets.Items.Clear();
                }
                txtSavedAuthor.Text = config.Author ?? "Author";
                txtSavedDesc.Text = DocConvert.IsBase64Encoded(config.Description) ? DocConvert.Unzip(config.Description) : config.Description ?? "Description";
                List<IBotCommand> commands = config.Commands;
                if (commands != null && commands.Count > 0)
                {
                    ListBox.ObjectCollection items = lstCommands.Items;
                    object[] array = config.Commands.ToArray();
                    items.AddRange(array);
                }
                List<Skill> skills = config.Skills;
                if (skills != null && skills.Count > 0)
                {
                    ListBox.ObjectCollection items = lstSkills.Items;
                    object[] array = config.Skills.ToArray();
                    items.AddRange(array);
                }
                List<Quest> quests = config.Quests;
                if (quests != null && quests.Count > 0)
                {
                    ListBox.ObjectCollection items = lstQuests.Items;
                    object[] array = config.Quests.ToArray();
                    items.AddRange(array);
                }
                List<InventoryItem> boosts = config.Boosts;
                if (boosts != null && boosts.Count > 0)
                {
                    ListBox.ObjectCollection items = lstBoosts.Items;
                    object[] array = config.Boosts.ToArray();
                    items.AddRange(array);
                }
                List<string> drops = config.Drops;
                if (drops != null && drops.Count > 0)
                {
                    ListBox.ObjectCollection items = lstDrops.Items;
                    object[] array = config.Drops.ToArray();
                    items.AddRange(array);
                }
                List<string> item = config.Items;
                if (item != null && item.Count > 0)
                {
                    ListBox.ObjectCollection items = lstItems.Items;
                    object[] array = config.Items.ToArray();
                    items.AddRange(array);
                }
                List<string> packets = config.Packets;
                if (packets != null && packets.Count > 0)
                {
                    ListBox.ObjectCollection Packets = lstPackets.Items;
                    object[] array = config.Packets.ToArray();
                    Packets.AddRange(array);
                }
                numSkillD.Value = config.SkillDelay;
                chkExitRest.Checked = config.ExitCombatBeforeRest;
                chkExistQuest.Checked = config.ExitCombatBeforeQuest;
                if (config.Server != null)
                {
                    cbServers.SelectedIndex = cbServers.Items.Cast<Server>().ToList().FindIndex((Server s) => s.Name == config.Server.Name);
                }
                chkRelog.Checked = config.AutoRelogin;
                numRelogDelay.Value = config.RelogDelay;
                chkRelogRetry.Checked = config.RelogRetryUponFailure;
                numBotDelay.Value = config.BotDelay;
                config.EnablePacketSpam = config.EnablePacketSpam;
                chkPickup.Checked = config.EnablePickup;
                chkReject.Checked = config.EnableRejection;
                chkPickupAll.Checked = config.EnablePickupAll;
                chkRejectAll.Checked = config.EnableRejectAll;
                chkAllSkillsCD.Checked = config.WaitForAllSkills;
                chkSkillCD.Checked = config.WaitForSkill;
                chkSkip.Checked = config.SkipDelayIndexIf;
                chkInfiniteRange.Checked = config.InfiniteAttackRange;
                chkLag.Checked = config.LagKiller;
                chkMagnet.Checked = config.EnemyMagnet;
                chkHidePlayers.Checked = config.HidePlayers;
                chkSkipCutscenes.Checked = config.SkipCutscenes;
                numWalkSpeed.Value = (config.WalkSpeed <= 0) ? 8 : config.WalkSpeed;
                List<string> notifyUponDrop = config.NotifyUponDrop;
                if (notifyUponDrop != null && notifyUponDrop.Count > 0)
                {
                    ListBox.ObjectCollection items14 = lstSoundItems.Items;
                    object[] array = config.NotifyUponDrop.ToArray();
                    object[] items15 = array;
                    items14.AddRange(items15);
                }
                numRestMP.Value = config.RestMp;
                numRest.Value = config.RestHp;
                chkMP.Checked = config.RestIfMp;
                chkHP.Checked = config.RestIfHp;
                chkUntarget.Checked = config.Untarget;
                chkBankOnStop.Checked = config.BankOnStop;
                chkRestartDeath.Checked = config.RestartUponDeath;
                chkAFK.Checked = config.AFK;
                numDropDelay.Value = config.DropDelay <= 0 ? 500 : config.DropDelay;
                var description = txtSavedDesc.Text ?? "Description";
                if (description.StartsWith("{\\rtf") || description.StartsWith("{\rtf"))
                    rtbInfo.Rtf = description; 
                else
                    rtbInfo.Text = config.Description;
                LastIndexedSearch = 0;
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

        private void lstCommands_DoubleClick(object sender, EventArgs e)
        {
            if (lstCommands.Items.Count <= 0)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Load bot";
                    Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                    openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
                    string botsDirectory;
                    try
                    {
                        botsDirectory = c.Get("botsDirectory");
                        if (!string.IsNullOrEmpty(botsDirectory))
                        {
                            openFileDialog.InitialDirectory = botsDirectory;
                        }
                    }
                    catch { }
                    openFileDialog.Filter = "Grimoire bots|*.gbot";
                    openFileDialog.DefaultExt = ".gbot";
                    if (openFileDialog.ShowDialog() == DialogResult.OK && TryDeserializeLoadBot(File.ReadAllText(openFileDialog.FileName), out Configuration config))
                    {
                        ApplyConfiguration(config);
                    }
                }
            }
            else if (lstCommands.SelectedIndex > -1)
            {
                int selectedIndex = lstCommands.SelectedIndex;
                object obj = lstCommands.Items[selectedIndex];
                string text;
                try { text = UserFriendlyCommandEditor.Show(obj); }
                catch { text = RawCommandEditor.Show(JsonConvert.SerializeObject(obj, Formatting.Indented, _serializerSettings)); }
                if (text != null)
                {
                    try
                    {
                        IBotCommand item = (IBotCommand)JsonConvert.DeserializeObject(text, obj.GetType());
                        lstCommands.Items.Remove(obj);
                        lstCommands.Items.Insert(selectedIndex, item);
                    }
                    catch
                    { }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (SelectedList.SelectedItem != null)
            {
                int selectedIndex = SelectedList.SelectedIndex;
                if (selectedIndex > -1)
                {
                    _RemoveListBoxItem();
                }
            }
            GetAllCommands<CmdLabel>(lbLabels);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            _MoveListBoxDown();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            _MoveListBoxUp();
        }

        private void _RemoveListBoxItem()
        {
            SelectedList.BeginUpdate();
            for (int x = SelectedList.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = SelectedList.SelectedIndices[x];
                SelectedList.Items.RemoveAt(idx);
            }
            SelectedList.EndUpdate();
        }

        private void _MoveListBoxUp()
        {
            //MoveListItem(-1);
            SelectedList.BeginUpdate();
            int numberOfSelectedItems = SelectedList.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                // only if it's not the first item
                if (SelectedList.SelectedIndices[i] > 0)
                {
                    // the index of the item above the item that we wanna move up
                    int indexToInsertIn = SelectedList.SelectedIndices[i] - 1;
                    // insert UP the item that we want to move up
                    SelectedList.Items.Insert(indexToInsertIn, SelectedList.SelectedItems[i]);
                    // removing it from its old place
                    SelectedList.Items.RemoveAt(indexToInsertIn + 2);
                    // highlighting it in its new place
                    SelectedList.SelectedItem = SelectedList.Items[indexToInsertIn];
                }
            }
            SelectedList.EndUpdate();
        }

        private void _MoveListBoxDown()
        {
            //MoveListItem(1);
            SelectedList.BeginUpdate();
            int numberOfSelectedItems = SelectedList.SelectedItems.Count;
            // when going down, instead of moving through the selected items from top to bottom
            // we'll go from bottom to top, it's easier to handle this way.
            for (int i = numberOfSelectedItems - 1; i >= 0; i--)
            {
                // only if it's not the last item
                if (SelectedList.SelectedIndices[i] < SelectedList.Items.Count - 1)
                {
                    // the index of the item that is currently below the selected item
                    int indexToInsertIn = SelectedList.SelectedIndices[i] + 2;
                    // insert DOWN the item that we want to move down
                    SelectedList.Items.Insert(indexToInsertIn, SelectedList.SelectedItems[i]);
                    // removing it from its old place
                    SelectedList.Items.RemoveAt(indexToInsertIn - 2);
                    // highlighting it in its new place
                    SelectedList.SelectedItem = SelectedList.Items[indexToInsertIn - 1];
                }
            }
            SelectedList.EndUpdate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Are you sure you wanna clear this command list?", "Clear Button", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                SelectedList.Items.Clear();
        }

        private void clearAllCommandLists_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Are you sure you wanna clear all command lists?", "Clear Button", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                lstBoosts.Items.Clear();
                lstCommands.Items.Clear();
                lstDrops.Items.Clear();
                lstItems.Items.Clear();
                lstQuests.Items.Clear();
                lstSkills.Items.Clear();
                lstPackets.Items.Clear();
            }
        }

        private void cbLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoosts.Visible = SelectedList == lstBoosts;
            lstCommands.Visible = SelectedList == lstCommands;
            lstDrops.Visible = SelectedList == lstDrops;
            lstItems.Visible = SelectedList == lstItems;
            lstQuests.Visible = SelectedList == lstQuests;
            lstSkills.Visible = SelectedList == lstSkills;
            lstPackets.Visible = SelectedList == lstPackets;
        }

        private void BotManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                e.Cancel = false;
                Show();
                BringToFront();
                Focus();
            }
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            string monster = string.IsNullOrEmpty(txtMonster.Text) ? "*" : txtMonster.Text;
            if (txtMonster.Text == "Monster (* = any)")
            {
                monster = "*";
            }
            AddCommand(new CmdKill
            {
                Monster = monster
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnKillF_Click(object sender, EventArgs e)
        {
            if (txtKillFItem.Text.Length > 0 && txtKillFQ.Text.Length > 0)
            {
                string monster = string.IsNullOrEmpty(txtKillFMon.Text) ? "*" : txtKillFMon.Text;
                string text = txtKillFItem.Text;
                string text2 = txtKillFQ.Text;
                AddCommand(new CmdKillFor
                {
                    ItemType = (!rbItems.Checked) ? ItemType.TempItems : ItemType.Items,
                    Monster = monster,
                    ItemName = text,
                    Quantity = text2
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnAddSkill_Click(object sender, EventArgs e)
        {
            string text = numSkill.Text;
            AddSkill(new Skill
            {
                Text = text + ": " + Skill.GetSkillName(text),
                Index = text,
                Type = Skill.SkillType.Normal
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnAddSafe2_Click(object sender, EventArgs e)
        {
            string text = numSkill.Text;
            int percentage = (int)numSafe.Value;
            AddSkill(new Skill
            {
                Text = "[Safe] " + text + ": " + Skill.GetSkillName(text),
                Index = text,
                Type = Skill.SkillType.Safe,
                ComparisonType = chkSafeGreaterThan.Checked,
                SafePercentage = percentage,
                SafeHp = chkSafeHP.Checked,
                SafeMp = chkSafeMP.Checked
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnRest_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdRest(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnRestF_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdRest
            {
                Full = true
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            string text = string.IsNullOrEmpty(txtJoin.Text) ? "-" : txtJoin.Text;
            string[] split = text.Split('-');
            if (!text.Contains('-'))
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                    "Failed to add the Join Map command. You have to insert the map name AND the room numbers in order to add the command.", "Join Map Command", MessageBoxIcon.Error);
                return;
            }
            string map = string.IsNullOrEmpty(split[0]) ? "battleon" : split[0];
            string room = string.IsNullOrEmpty(split[1]) ? "1e99" : split[1];
            string cell = string.IsNullOrEmpty(txtJoinCell.Text) ? "Enter" : txtJoinCell.Text;
            string pad = string.IsNullOrEmpty(txtJoinPad.Text) ? "Spawn" : txtJoinPad.Text;
            if (text.Length > 0)
            {
                AddCommand(new CmdJoin2
                {
                    Map = map,
                    Room = room,
                    Cell = cell,
                    Pad = pad
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnCellSwap_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                DarkButton s = sender as DarkButton;
                if (s.Text == "<")
                {
                    txtJoin.Text = Player.Map + "-" + Flash.Call<string>("RoomNumber", new object[0]);
                    txtJoinCell.Text = txtCell.Text;
                    txtJoinPad.Text = txtPad.Text;
                }
                else if (s.Text == ">")
                {
                    txtCell.Text = txtJoinCell.Text;
                    txtPad.Text = txtJoinPad.Text;
                }
            }
        }

        private void btnCurrCell_Click(object sender, EventArgs e)
        {
            txtCell.Text = Player.Cell;
            txtPad.Text = Player.Pad;
        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            string cell = string.IsNullOrEmpty(txtCell.Text) ? "Enter" : txtCell.Text;
            string pad = string.IsNullOrEmpty(txtPad.Text) ? "Spawn" : txtPad.Text;
            AddCommand(new CmdMoveToCell
            {
                Cell = cell,
                Pad = pad
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWalk_Click(object sender, EventArgs e)
        {
            string x = numWalkX.Value.ToString();
            string y = numWalkY.Value.ToString();
            AddCommand(new CmdWalk
            {
                X = x,
                Y = y
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWalkCur_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                float[] position = Player.Position;
                numWalkX.Value = (decimal)position[0];
                numWalkY.Value = (decimal)position[1];
            }
        }

        private void btnWalkRdm_Click(object sender, EventArgs e)
        {
            string x = numWalkX.Value.ToString();
            string y = numWalkY.Value.ToString();
            AddCommand(new CmdWalk
            {
                Type = "Random",
                X = x,
                Y = y
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            string text = txtItem.Text;
            if (text.Length > 0 && cbItemCmds.SelectedIndex > -1)
            {
                IBotCommand cmd;
                switch (cbItemCmds.SelectedIndex)
                {
                    case 0:
                        cmd = new CmdGetDrop
                        {
                            ItemName = text
                        };
                        break;

                    case 1:
                        cmd = new CmdSell
                        {
                            ItemName = text
                        };
                        break;

                    case 2:
                        cmd = new CmdEquip
                        {
                            ItemName = text,
                            Safe = true
                        };
                        break;

                    case 3:
                        cmd = new CmdEquip
                        {
                            ItemName = text,
                            Safe = false
                        };
                        break;

                    case 4:
                        cmd = new CmdBankTransfer
                        {
                            ItemName = text,
                            TransferFromBank = false
                        };
                        break;

                    case 5:
                        cmd = new CmdBankTransfer
                        {
                            ItemName = text,
                            TransferFromBank = true
                        };
                        break;

                    case 6:
                        cmd = new CmdEquipSet
                        {
                            ItemName = text
                        };
                        break;

                    case 7:
                        cmd = new CmdWhitelist
                        {
                            Item = text,
                            State = state.Add
                        };
                        break;

                    case 8:
                        cmd = new CmdWhitelist
                        {
                            Item = text,
                            State = state.Remove
                        };
                        break;

                    default:
                        cmd = new CmdGetDrop
                        {
                            ItemName = text
                        };
                        break;
                }
                AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnMapItem_Click(object sender, EventArgs e)
        {
            int id = (int)numMapItem.Value;
            AddCommand(new CmdMapItem2
            {
                ItemId = id.ToString()
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnBoth_Click(object sender, EventArgs e)
        {
            string text = txtWhitelist.Text;
            if (text.Length > 0)
            {
                AddDrop(text);
                AddItem(text);
            }
        }

        private void btnWhitelist_Click(object sender, EventArgs e)
        {
            string text = txtWhitelist.Text;
            if (text.Length > 0)
            {
                AddDrop(text);
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            string text = txtSwapBank.Text;
            string text2 = txtSwapInv.Text;
            if (text.Length > 0 && text2.Length > 0)
            {
                AddCommand(new CmdBankSwap
                {
                    InventoryItemName = text2,
                    BankItemName = text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnBoost_Click(object sender, EventArgs e)
        {
            if (cbBoosts.SelectedIndex > -1)
            {
                lstBoosts.Items.Add(cbBoosts.SelectedItem);
            }
        }

        private void cbBoosts_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                cbBoosts.Items.Clear();
                ComboBox.ObjectCollection items = cbBoosts.Items;
                object[] array = Player.Inventory.Items.Where((InventoryItem i) => i.Category == "ServerUse").ToArray();
                object[] items2 = array;
                items.AddRange(items2);
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (txtShopItem.TextLength > 0)
            {
                int id = (int)numShopId.Value;
                AddCommand(new CmdBuy2
                {
                    ItemName = txtShopItem.Text,
                    ShopId = id.ToString()
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnQuestAdd_Click(object sender, EventArgs e)
        {
            AddQuest(int.Parse(numQuestID.Text), chkQuestItem.Checked ? numQuestItem.Text : null);
        }

        private void btnQuestComplete_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdCompleteQuest2
            {
                QuestID = numQuestID.Text,
                ItemID = chkQuestItem.Checked ? numQuestItem.Text : null,
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnQuestAccept_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdAcceptQuest2
            {
                QuestID = numQuestID.Text
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkQuestItem_CheckedChanged(object sender, EventArgs e)
        {
            numQuestItem.Enabled = chkQuestItem.Checked;
        }

        private void btnPacket_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdPacket2
            {
                Packet = txtPacket.Text,
                Delay = (int)numPacketDelay.Value
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnDelay_Click(object sender, EventArgs e)
        {
            int delay = (int)numDelay.Value;
            AddCommand(new CmdDelay
            {
                Delay = delay
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            string text = txtPlayer.Text;
            if (text.Length > 0)
            {
                AddCommand(new CmdGotoPlayer
                {
                    PlayerName = text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnBotDelay_Click(object sender, EventArgs e)
        {
            int delay = (int)numBotDelay.Value;
            AddCommand(new CmdBotDelay2
            {
                Delay = delay.ToString()
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdStop(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdRestart(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        public void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load bot";
                openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
                string botsDirectory;
                try
                {
                    Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                    botsDirectory = c.Get("botsDirectory");
                    if (!string.IsNullOrEmpty(botsDirectory))
                    {
                        openFileDialog.InitialDirectory = botsDirectory;
                    }
                }
                catch { }
                openFileDialog.Filter = "Grimoire bots|*.gbot";
                openFileDialog.DefaultExt = ".gbot";
                if (openFileDialog.ShowDialog() == DialogResult.OK && TryDeserializeLoadBot(File.ReadAllText(openFileDialog.FileName), out Configuration config))
                {
                    ApplyConfiguration(config);
                    GetAllCommands<CmdLabel>(lbLabels);
                }
            }
        }

        private bool TryDeserializeLoadBot(string json, out Configuration config)
        {
            try
            {
                config = JsonConvert.DeserializeObject<Configuration>(json, _saveSerializerSettings);
                return true;
            }
            catch (Exception e)
            {
                var specifiedError = e.Message;
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen, Icon = global::Properties.Resources.GrimoireIcon }, $"Failed to load the bot. You cannot load any bot that has commands not available in this client.\r\n\r\nError Message:\r\n{specifiedError}", "Load Bot", MessageBoxIcon.Error);
            }
            config = null;
            return false;
        }

        private bool TryDeserialize(string json, out Configuration config)
        {
            try
            {
                config = JsonConvert.DeserializeObject<Configuration>(json, _saveSerializerSettings);
                return true;
            }
            catch (Exception e)
            {
                var specifiedError = e.Message;
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, $"Failed to process the event.\r\n\r\nError Message:\r\n{specifiedError}", "Load Bot", MessageBoxIcon.Error);
            }
            config = null;
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save bot";
                Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                saveFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
                string botsDirectory;
                try
                {
                    botsDirectory = c.Get("botsDirectory");
                    if (!string.IsNullOrEmpty(botsDirectory))
                    {
                        saveFileDialog.InitialDirectory = botsDirectory;
                    }
                }
                catch { }
                saveFileDialog.DefaultExt = ".gbot";
                saveFileDialog.Filter = "Grimoire bots|*.gbot";
                saveFileDialog.CheckFileExists = false;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Configuration value = SaveConfiguration();
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _saveSerializerSettings));
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Unable to save bot: " + ex.Message);
                    }
                }
            }
        }

        private void btnLoadCmd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select bot to load";
                openFileDialog.Filter = "Grimoire bots|*.gbot";
                Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
                string botsDirectory;
                try
                {
                    botsDirectory = c.Get("botsDirectory");
                    if (!string.IsNullOrEmpty(botsDirectory))
                    {
                        openFileDialog.InitialDirectory = botsDirectory;
                    }
                }
                catch { }
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    AddCommand(new CmdLoadBot
                    {
                        BotFilePath = Extensions.MakeRelativePathFrom(Application.StartupPath, openFileDialog.FileName), // Path.GetFullPath(openFileDialog.FileName)
                        BotFileName = Path.GetFileName(openFileDialog.FileName)

                    }, (ModifierKeys & Keys.Control) == Keys.Control);
                }
            }
        }

        private void cbStatement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex > -1 && cbStatement.SelectedIndex > -1)
            {
                StatementCommand statementCommand = (StatementCommand)cbStatement.SelectedItem;
                txtStatement1.Enabled = statementCommand.Description1 != null;
                txtStatement2.Enabled = statementCommand.Description2 != null;
                txtStatement1.Text = statementCommand.Description1;
                txtStatement2.Text = statementCommand.Description2;
            }
        }

        private void btnStatementAdd_Click(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex > -1 && cbStatement.SelectedIndex > -1)
            {
                string text = txtStatement1.Text;
                string text2 = txtStatement2.Text;
                StatementCommand statementCommand = (StatementCommand)Activator.CreateInstance(cbStatement.SelectedItem.GetType());
                statementCommand.Value1 = text;
                statementCommand.Value2 = text2;
                AddCommand((IBotCommand)statementCommand, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex > -1)
            {
                cbStatement.Items.Clear();
                string text = cbCategories.SelectedItem.ToString();
                ComboBox.ObjectCollection items = cbStatement.Items;
                object[] array = _statementCommands.Where((StatementCommand s) => s.Tag == text).ToArray();
                object[] items2 = array;
                items.AddRange(items2);
            }
        }

        private void btnGotoLabel_Click(object sender, EventArgs e)
        {
            if (txtLabel.TextLength > 0)
            {
                AddCommand(new CmdGotoLabel
                {
                    Label = txtLabel.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
            GetAllCommands<CmdLabel>(lbLabels);
        }

        private void btnAddLabel_Click(object sender, EventArgs e)
        {
            if (txtLabel.TextLength > 0)
            {
                int num = lbLabels.Items.IndexOf($"[{txtLabel.Text.ToUpper()}]");
                if (num > -1)
                {
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "A label with the same name has already been added to the command list.", "Add Label", MessageBoxIcon.Error);
                    return;
                }
                AddCommand(new CmdLabel
                {
                    Name = txtLabel.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
            GetAllCommands<CmdLabel>(lbLabels);
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdLogout(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void UpdateTree()
        {
            if (!string.IsNullOrEmpty(txtSaved.Text) && Directory.Exists(txtSaved.Text))
            {
                lblBots.Text = string.Format("Number of Bots: {0}", Directory.EnumerateFiles(txtSaved.Text, "*.gbot", SearchOption.AllDirectories).Count());
                treeBots.Nodes.Clear();
                AddTreeNodes(treeBots, txtSaved.Text);
            }
        }

        private void treeBots_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = Path.Combine(txtSaved.Text, e.Node.FullPath);
            if (File.Exists(path))
            {
                if (!TryDeserializeLoadBot(File.ReadAllText(path), out Configuration config))
                {
                    return;
                }
                ApplyConfiguration(config);
            }
            lblCommands.Text = $"Commands: {lstCommands.Items.Count}";
            lblSkills.Text = $"Skills: {lstSkills.Items.Count}";
            lblQuests.Text = $"Quests: {lstQuests.Items.Count}";
            lblDrops.Text = $"Drops: {lstDrops.Items.Count}";
            lblBoosts.Text = $"Boosts: {lstBoosts.Items.Count}";
            lblItems.Text = $"Items: {lstItems.Items.Count}";
        }

        private void treeBots_AfterExpand(object sender, TreeViewEventArgs e)
        {
            string path = Path.Combine(txtSaved.Text, e.Node.FullPath);
            if (Directory.Exists(path))
            {
                AddTreeNodes(e.Node, path);
                if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "Loading...")
                {
                    e.Node.Nodes.RemoveAt(0);
                }
            }
        }

        private void AddTreeNodes(TreeNode node, string path)
        {
            foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
            {
                string add = Path.GetFileName(item);
                if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
                {
                    node.Nodes.Add(add).Nodes.Add("Loading...");
                }
            }
            foreach (string item2 in Directory.EnumerateFiles(path, "*.gbot", SearchOption.TopDirectoryOnly))
            {
                string add2 = Path.GetFileName(item2);
                if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
                {
                    node.Nodes.Add(add2);
                }
            }
        }

        private void AddTreeNodes(TreeView tree, string path)
        {
            foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
            {
                string add = Path.GetFileName(item);
                if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
                {
                    tree.Nodes.Add(add).Nodes.Add("Loading...");
                }
            }
            foreach (string item2 in Directory.EnumerateFiles(path, "*.gbot", SearchOption.TopDirectoryOnly))
            {
                string add2 = Path.GetFileName(item2);
                if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
                {
                    tree.Nodes.Add(add2);
                }
            }
        }

        private void btnSavedAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSaved.Text))
            {
                string path = Path.Combine(txtSaved.Text, txtSavedAdd.Text);
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception ex)
                    {
                        DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Unable to create directory: " + ex.Message, "Grimoire", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                UpdateTree();
            }
        }

        private void btnSoundAdd_Click(object sender, EventArgs e)
        {
            if (txtSoundItem.TextLength > 0)
            {
                lstSoundItems.Items.Add(txtSoundItem.Text);
            }
        }

        private void btnSoundDelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstSoundItems.SelectedIndex;
            if (selectedIndex > -1)
            {
                lstSoundItems.Items.RemoveAt(selectedIndex);
            }
        }

        private void btnSoundTest_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Beep();
            }
        }

        private void chkInfiniteRange_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.InfiniteRange = chkInfiniteRange.Checked;
            Root.Instance.infRangeToolStripMenuItem.Checked = chkInfiniteRange.Checked;
        }

        private void chkProvoke_Clicked(object sender, EventArgs e)
        {
            if (!chkProvoke.Checked && (Player.CurrentState == Player.State.InCombat))
            {
                BotUtilities.MoveToSelfCell();
            }
        }

        private void chkProvoke_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.ProvokeMonsters = chkProvoke.Checked;
            Root.Instance.provokeToolStripMenuItem1.Checked = chkProvoke.Checked;
        }

        private void chkProvokeAllMon_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.ProvokeAllMonster = chkProvokeAllMon.Checked;
            if (!chkProvokeAllMon.Checked && (Player.CurrentState == Player.State.InCombat))
            {
                BotUtilities.MoveToSelfCell();
            }
        }

        private void chkMagnet_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.EnemyMagnet = chkMagnet.Checked;
            Root.Instance.enemyMagnetToolStripMenuItem.Checked = chkMagnet.Checked;
        }

        private void chkLag_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.LagKiller = chkLag.Checked;
            Root.Instance.lagKillerToolStripMenuItem.Checked = chkLag.Checked;
        }

        private void chkHidePlayers_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.HidePlayers = chkHidePlayers.Checked;
            Root.Instance.hidePlayersToolStripMenuItem.Checked = chkHidePlayers.Checked;
        }

        private void chkSkipCutscenes_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.SkipCutscenes = chkSkipCutscenes.Checked;
            Root.Instance.skipCutscenesToolStripMenuItem.Checked = chkSkipCutscenes.Checked;
        }

        private void numWalkSpeed_ValueChanged(object sender, EventArgs e)
        {
            OptionsManager.WalkSpeed = (int)numWalkSpeed.Value;
        }

        private void chkDisableAnims_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.DisableAnimations = chkDisableAnims.Checked;
            Root.Instance.disableAnimationsToolStripMenuItem.Checked = chkDisableAnims.Checked;
        }

        private void OnOptionsStateChanged(bool state)
        {
            if (InvokeRequired)
            {
                Invoke((Action)delegate
                {
                    chkEnableSettings.Checked = state;
                });
            }
            else
            {
                chkEnableSettings.Checked = state;
            }
        }

        private void chkEnableSettings_Click(object sender, EventArgs e)
        {
            if (chkEnableSettings.Checked)
                OptionsManager.Start();
            else
                OptionsManager.Stop();
        }

        private void lstBoxs_KeyPress(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Up)
            {
                _MoveListBoxUp();
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Down)
            {
                _MoveListBoxDown();
                e.Handled = true;
            }
            else if ((ModifierKeys == Keys.Control && e.KeyCode == Keys.Delete) || (ModifierKeys == Keys.Control && e.KeyCode == Keys.R))
            {
                btnRemove.PerformClick();
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.D && SelectedList.SelectedIndex > -1)
            {
                var selectedItems = SelectedList.SelectedItems;
                for (int i = 0; selectedItems.Count > i; i++)
                {
                    SelectedList.Items.Insert(SelectedList.SelectedIndex + selectedItems.Count + i, selectedItems[i]);
                }
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.C && SelectedList.SelectedIndex > -1)
            {
                Clipboard.Clear();
                Configuration items = new Configuration
                {
                    Commands = lstCommands.SelectedItems.Cast<IBotCommand>().ToList(),
                    Skills = lstSkills.SelectedItems.Cast<Skill>().ToList(),
                    Quests = lstQuests.SelectedItems.Cast<Quest>().ToList(),
                    Boosts = lstBoosts.SelectedItems.Cast<InventoryItem>().ToList(),
                    Drops = lstDrops.SelectedItems.Cast<string>().ToList(),
                    Items = lstItems.SelectedItems.Cast<string>().ToList(),
                    Packets = lstPackets.SelectedItems.Cast<string>().ToList()
                };
                Clipboard.SetText(JsonConvert.SerializeObject(items, Formatting.Indented, _saveSerializerSettings));
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
            {
                TryDeserialize(Clipboard.GetText(), out Configuration config);
                List<IBotCommand> commands = config.Commands;
                if (commands != null && commands.Count > 0)
                {
                    List<IBotCommand> items = lstCommands.Items.Cast<IBotCommand>().ToList();
                    int selectedIndex = lstCommands.SelectedIndex;
                    lstCommands.SelectedIndex = -1;
                    items.InsertRange(++selectedIndex, commands);
                    lstCommands.Items.Clear();
                    lstCommands.Items.AddRange(items.ToArray());
                    for (int i = 0; i < commands.Count; i++)
                        lstCommands.SelectedIndex = selectedIndex + i;
                }
                List<Skill> skills = config.Skills;
                if (skills != null && skills.Count > 0)
                {
                    ListBox.ObjectCollection items = lstSkills.Items;
                    object[] array = config.Skills.ToArray();
                    items.AddRange(array);
                }
                List<Quest> quests = config.Quests;
                if (quests != null && quests.Count > 0)
                {
                    ListBox.ObjectCollection items = lstQuests.Items;
                    object[] array = config.Quests.ToArray();
                    items.AddRange(array);
                }
                List<InventoryItem> boosts = config.Boosts;
                if (boosts != null && boosts.Count > 0)
                {
                    ListBox.ObjectCollection items = lstBoosts.Items;
                    object[] array = config.Boosts.ToArray();
                    items.AddRange(array);
                }
                List<string> drops = config.Drops;
                if (drops != null && drops.Count > 0)
                {
                    ListBox.ObjectCollection items = lstDrops.Items;
                    object[] array = config.Drops.ToArray();
                    items.AddRange(array);
                }
                List<string> item = config.Items;
                if (item != null && item.Count > 0)
                {
                    ListBox.ObjectCollection items = lstItems.Items;
                    object[] array = config.Items.ToArray();
                    items.AddRange(array);
                }
                List<string> packets = config.Packets;
                if (packets != null && packets.Count > 0)
                {
                    ListBox.ObjectCollection items = lstPackets.Items;
                    object[] array = config.Packets.ToArray();
                    items.AddRange(array);
                }
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.S)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Save bot";
                    Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                    saveFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
                    string botsDirectory;
                    try
                    {
                        botsDirectory = c.Get("botsDirectory");
                        if (!string.IsNullOrEmpty(botsDirectory))
                        {
                            saveFileDialog.InitialDirectory = botsDirectory;
                        }
                    }
                    catch { }
                    saveFileDialog.DefaultExt = ".gbot";
                    saveFileDialog.Filter = "Grimoire bots|*.gbot";
                    saveFileDialog.CheckFileExists = false;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Configuration value = SaveConfiguration();
                        try
                        {
                            File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _saveSerializerSettings));
                        }
                        catch (Exception ex)
                        {
                            DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Unable to save bot: " + ex.Message);
                        }
                    }
                }
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.X)
            {
                Clipboard.Clear();
                Configuration items = new Configuration
                {
                    Commands = lstCommands.SelectedItems.Cast<IBotCommand>().ToList(),
                };
                Clipboard.SetText(JsonConvert.SerializeObject(items, Formatting.Indented, _saveSerializerSettings));
                btnRemove.PerformClick();
                e.Handled = true;
            }
        }

        public void AddCommand(IBotCommand cmd, bool Insert = false)
        {
            if (Insert)
            {
                lstCommands.Items.Insert((lstCommands.SelectedIndex > -1) ? lstCommands.SelectedIndex : lstCommands.Items.Count, cmd);
            }
            else
            {
                lstCommands.Items.Add(cmd);
            }
        }

        private void AddSkill(Skill skill, bool Insert)
        {
            if (Insert)
            {
                lstSkills.Items.Insert((lstSkills.SelectedIndex > -1) ? lstSkills.SelectedIndex : lstSkills.Items.Count, skill);
            }
            else
            {
                lstSkills.Items.Add(skill);
            }
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
            OnBankItemExecute();
            ActiveBotEngine.Stop();
            SelectionModeToggle(true);
            BotStateChanged(IsRunning: false);
            await Task.Delay(2000);
            Root.Instance.BotStateChanged(IsRunning: false);
            btnBotStart.Enabled = true;
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

        public void AddQuest(int QuestID, string ItemID = null)
        {
            Quest quest = new Quest
            {
                Id = QuestID,
                ItemId = ItemID
            };
            quest.Text = (quest.ItemId != null) ? $"{quest.Id}:{quest.ItemId}" : quest.Id.ToString();
            if (!lstQuests.Items.Contains(quest.Text))
            {
                lstQuests.Items.Add(quest);
            }
        }

        public void AddDrop(string Name)
        {
            if (!lstDrops.Items.Contains(Name))
            {
                lstDrops.Items.Add(Name);
                Configuration.Instance.Drops.Add(Name);
            }
        }

        public void RemoveDrop(string Name)
        {
            for (int n = lstDrops.Items.Count - 1; n >= 0; n--)
            {
                string itemDrop = Name;
                int DropIndex = lstDrops.Items.IndexOf($"{itemDrop}");
                if (lstDrops.Items[n].ToString().Contains($"{itemDrop}"))
                {
                    lstDrops.Items.RemoveAt(DropIndex);
                    Configuration.Instance.Drops.RemoveAt(DropIndex);
                }
            }
        }

        public void AddPacket(string Name)
        {
            if (!lstPackets.Items.Contains(Name))
            {
                lstPackets.Items.Add(Name);
            }
        }

        public void RemovePacket(string Name)
        {
            if (lstPackets.Items.Contains(Name))
            {
                lstPackets.Items.Remove(Name);
            }
        }

        private void btnAddSkillSet_Click(object sender, EventArgs e)
        {
            if (txtSkillSet.TextLength > 0)
            {
                AddSkill(new Skill
                {
                    Text = "[" + txtSkillSet.Text.ToUpper() + "]",
                    Type = Skill.SkillType.Label
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnUseSkillSet_Click(object sender, EventArgs e)
        {
            if (txtSkillSet.TextLength > 0)
            {
                AddCommand(new CmdSkillSet
                {
                    Name = txtSkillSet.Text.ToUpper()
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
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
            this.lstCommands = new System.Windows.Forms.ListBox();
            this.lstBoosts = new System.Windows.Forms.ListBox();
            this.lstDrops = new System.Windows.Forms.ListBox();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.lstQuests = new System.Windows.Forms.ListBox();
            this.lstSkills = new System.Windows.Forms.ListBox();
            this.lstPackets = new System.Windows.Forms.ListBox();
            this.mainTabControl = new VisualStudioTabControl.VisualStudioTabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.tabCombat = new System.Windows.Forms.TabPage();
            this.darkGroupBox30 = new DarkUI.Controls.DarkGroupBox();
            this.numSkillCmdHPMP = new DarkUI.Controls.DarkNumericUpDown();
            this.txtSkillCmdHPMP = new DarkUI.Controls.DarkLabel();
            this.darkLabel10 = new DarkUI.Controls.DarkLabel();
            this.chkSkillCmdWait = new DarkUI.Controls.DarkCheckBox();
            this.txtMonsterSkillCmd = new DarkUI.Controls.DarkTextBox();
            this.btnSkillCmd = new DarkUI.Controls.DarkButton();
            this.numSkillCmd = new DarkUI.Controls.DarkNumericUpDown();
            this.darkGroupBox29 = new DarkUI.Controls.DarkGroupBox();
            this.btnAddSafe = new DarkUI.Controls.DarkButton();
            this.btnAddSkill = new DarkUI.Controls.DarkButton();
            this.numSkill = new DarkUI.Controls.DarkNumericUpDown();
            this.txtSkillSet = new DarkUI.Controls.DarkTextBox();
            this.btnAddSkillSet = new DarkUI.Controls.DarkButton();
            this.darkLabel2 = new DarkUI.Controls.DarkLabel();
            this.btnUseSkillSet = new DarkUI.Controls.DarkButton();
            this.chkSafeGreaterThan = new DarkUI.Controls.DarkCheckBox();
            this.chkSafeLessThan = new DarkUI.Controls.DarkCheckBox();
            this.numSafe = new DarkUI.Controls.DarkNumericUpDown();
            this.chkSafeHP = new DarkUI.Controls.DarkCheckBox();
            this.label2 = new DarkUI.Controls.DarkLabel();
            this.chkSafeMP = new DarkUI.Controls.DarkCheckBox();
            this.numSkillD = new DarkUI.Controls.DarkNumericUpDown();
            this.label13 = new DarkUI.Controls.DarkLabel();
            this.darkGroupBox28 = new DarkUI.Controls.DarkGroupBox();
            this.btnCancelTargetCmd = new DarkUI.Controls.DarkButton();
            this.btnCancelAutoAttackCmd = new DarkUI.Controls.DarkButton();
            this.darkGroupBox27 = new DarkUI.Controls.DarkGroupBox();
            this.label12 = new DarkUI.Controls.DarkLabel();
            this.chkHP = new DarkUI.Controls.DarkCheckBox();
            this.numRest = new DarkUI.Controls.DarkNumericUpDown();
            this.chkMP = new DarkUI.Controls.DarkCheckBox();
            this.numRestMP = new DarkUI.Controls.DarkNumericUpDown();
            this.label10 = new DarkUI.Controls.DarkLabel();
            this.label11 = new DarkUI.Controls.DarkLabel();
            this.btnRestF = new DarkUI.Controls.DarkButton();
            this.btnRest = new DarkUI.Controls.DarkButton();
            this.darkGroupBox26 = new DarkUI.Controls.DarkGroupBox();
            this.rbItems = new DarkUI.Controls.DarkRadioButton();
            this.btnKillF = new DarkUI.Controls.DarkButton();
            this.rbTemp = new DarkUI.Controls.DarkRadioButton();
            this.txtKillFMon = new DarkUI.Controls.DarkTextBox();
            this.txtKillFItem = new DarkUI.Controls.DarkTextBox();
            this.txtKillFQ = new DarkUI.Controls.DarkTextBox();
            this.darkGroupBox25 = new DarkUI.Controls.DarkGroupBox();
            this.txtMonster = new DarkUI.Controls.DarkTextBox();
            this.btnKill = new DarkUI.Controls.DarkButton();
            this.btnAttack = new DarkUI.Controls.DarkButton();
            this.chkSkillCD = new DarkUI.Controls.DarkCheckBox();
            this.chkExistQuest = new DarkUI.Controls.DarkCheckBox();
            this.chkExitRest = new DarkUI.Controls.DarkCheckBox();
            this.chkAllSkillsCD = new DarkUI.Controls.DarkCheckBox();
            this.tabItem = new System.Windows.Forms.TabPage();
            this.darkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            this.btnMapItem = new DarkUI.Controls.DarkButton();
            this.numMapItem = new DarkUI.Controls.DarkNumericUpDown();
            this.darkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            this.chkPickupAll = new DarkUI.Controls.DarkCheckBox();
            this.chkPickup = new DarkUI.Controls.DarkCheckBox();
            this.chkReject = new DarkUI.Controls.DarkCheckBox();
            this.chkPickupAcTag = new DarkUI.Controls.DarkCheckBox();
            this.label1 = new DarkUI.Controls.DarkLabel();
            this.chkBankOnStop = new DarkUI.Controls.DarkCheckBox();
            this.numDropDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.chkRejectAll = new DarkUI.Controls.DarkCheckBox();
            this.darkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            this.darkLabel1 = new DarkUI.Controls.DarkLabel();
            this.txtShopItemID = new DarkUI.Controls.DarkTextBox();
            this.txtShopID = new DarkUI.Controls.DarkTextBox();
            this.txtItemID = new DarkUI.Controls.DarkTextBox();
            this.btnBuyItemByID = new DarkUI.Controls.DarkButton();
            this.numShopId = new DarkUI.Controls.DarkNumericUpDown();
            this.btnBuyFastByID = new DarkUI.Controls.DarkButton();
            this.btnLoadShop = new DarkUI.Controls.DarkButton();
            this.txtShopItem = new DarkUI.Controls.DarkTextBox();
            this.btnBuy = new DarkUI.Controls.DarkButton();
            this.btnBuyFast = new DarkUI.Controls.DarkButton();
            this.darkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            this.btnWhitelistClear = new DarkUI.Controls.DarkButton();
            this.btnWhitelistOn = new DarkUI.Controls.DarkButton();
            this.btnWhitelistOff = new DarkUI.Controls.DarkButton();
            this.btnBoost = new DarkUI.Controls.DarkButton();
            this.cbBoosts = new DarkUI.Controls.DarkComboBox();
            this.btnSwap = new DarkUI.Controls.DarkButton();
            this.txtSwapInv = new DarkUI.Controls.DarkTextBox();
            this.txtSwapBank = new DarkUI.Controls.DarkTextBox();
            this.btnWhitelist = new DarkUI.Controls.DarkButton();
            this.btnBoth = new DarkUI.Controls.DarkButton();
            this.txtWhitelist = new DarkUI.Controls.DarkTextBox();
            this.btnItem = new DarkUI.Controls.DarkButton();
            this.btnUnbanklst = new DarkUI.Controls.DarkButton();
            this.txtItem = new DarkUI.Controls.DarkTextBox();
            this.cbItemCmds = new DarkUI.Controls.DarkComboBox();
            this.tabMap = new System.Windows.Forms.TabPage();
            this.darkGroupBox18 = new DarkUI.Controls.DarkGroupBox();
            this.darkLabel5 = new DarkUI.Controls.DarkLabel();
            this.numWalkX = new DarkUI.Controls.DarkNumericUpDown();
            this.darkLabel6 = new DarkUI.Controls.DarkLabel();
            this.numWalkY = new DarkUI.Controls.DarkNumericUpDown();
            this.btnWalk = new DarkUI.Controls.DarkButton();
            this.btnSetSpawn = new DarkUI.Controls.DarkButton();
            this.btnWalkCur = new DarkUI.Controls.DarkButton();
            this.btnWalkRdm = new DarkUI.Controls.DarkButton();
            this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            this.txtJoin = new DarkUI.Controls.DarkTextBox();
            this.txtJoinCell = new DarkUI.Controls.DarkTextBox();
            this.txtJoinPad = new DarkUI.Controls.DarkTextBox();
            this.btnCurrBlank = new DarkUI.Controls.DarkButton();
            this.btnJoin = new DarkUI.Controls.DarkButton();
            this.txtCell = new DarkUI.Controls.DarkTextBox();
            this.txtPad = new DarkUI.Controls.DarkTextBox();
            this.btnCurrCell = new DarkUI.Controls.DarkButton();
            this.btnJump = new DarkUI.Controls.DarkButton();
            this.btnCellSwap = new DarkUI.Controls.DarkButton();
            this.button2 = new DarkUI.Controls.DarkButton();
            this.tabQuest = new System.Windows.Forms.TabPage();
            this.darkGroupBox20 = new DarkUI.Controls.DarkGroupBox();
            this.btnQuestlistClearCmd = new DarkUI.Controls.DarkButton();
            this.txtQuestItemID = new DarkUI.Controls.DarkTextBox();
            this.txtQuestID = new DarkUI.Controls.DarkTextBox();
            this.darkLabel7 = new DarkUI.Controls.DarkLabel();
            this.chkQuestlistItemID = new DarkUI.Controls.DarkCheckBox();
            this.btnQuestlistRemoveCmd = new DarkUI.Controls.DarkButton();
            this.btnQuestlistAddCmd = new DarkUI.Controls.DarkButton();
            this.darkGroupBox21 = new DarkUI.Controls.DarkGroupBox();
            this.numQuestItem = new DarkUI.Controls.DarkTextBox();
            this.numQuestID = new DarkUI.Controls.DarkTextBox();
            this.label4 = new DarkUI.Controls.DarkLabel();
            this.btnQuestAccept = new DarkUI.Controls.DarkButton();
            this.chkQuestItem = new DarkUI.Controls.DarkCheckBox();
            this.btnQuestComplete = new DarkUI.Controls.DarkButton();
            this.btnQuestAdd = new DarkUI.Controls.DarkButton();
            this.tabMisc = new System.Windows.Forms.TabPage();
            this.darkGroupBox24 = new DarkUI.Controls.DarkGroupBox();
            this.darkLabel9 = new DarkUI.Controls.DarkLabel();
            this.darkLabel8 = new DarkUI.Controls.DarkLabel();
            this.numPacketDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.btnPacketSpamOffCmd = new DarkUI.Controls.DarkButton();
            this.btnPacketSpamOnCmd = new DarkUI.Controls.DarkButton();
            this.darkLabel4 = new DarkUI.Controls.DarkLabel();
            this.txtPacket = new DarkUI.Controls.DarkTextBox();
            this.btnServerPacket = new DarkUI.Controls.DarkButton();
            this.btnClientPacket = new DarkUI.Controls.DarkButton();
            this.darkGroupBox23 = new DarkUI.Controls.DarkGroupBox();
            this.btnLoadCmd = new DarkUI.Controls.DarkButton();
            this.btnLogout = new DarkUI.Controls.DarkButton();
            this.numSetFPS = new DarkUI.Controls.DarkNumericUpDown();
            this.btnReturnCmd = new DarkUI.Controls.DarkButton();
            this.btnBlank = new DarkUI.Controls.DarkButton();
            this.btnSetFPSCmd = new DarkUI.Controls.DarkButton();
            this.btnBeep = new DarkUI.Controls.DarkButton();
            this.numDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.btnDelay = new DarkUI.Controls.DarkButton();
            this.numBeepTimes = new DarkUI.Controls.DarkNumericUpDown();
            this.darkGroupBox22 = new DarkUI.Controls.DarkGroupBox();
            this.cbCategories = new DarkUI.Controls.DarkComboBox();
            this.cbStatement = new DarkUI.Controls.DarkComboBox();
            this.txtStatement1 = new DarkUI.Controls.DarkTextBox();
            this.txtStatement2 = new DarkUI.Controls.DarkTextBox();
            this.btnStatementAdd = new DarkUI.Controls.DarkButton();
            this.btnClearTempVar = new DarkUI.Controls.DarkButton();
            this.darkGroupBox11 = new DarkUI.Controls.DarkGroupBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btnGotoLabel = new DarkUI.Controls.DarkButton();
            this.btnAddLabel = new DarkUI.Controls.DarkButton();
            this.txtLabel = new DarkUI.Controls.DarkTextBox();
            this.lbLabels = new DarkUI.Controls.DarkListBox(this.components);
            this.darkPanel2 = new DarkUI.Controls.DarkPanel();
            this.darkGroupBox14 = new DarkUI.Controls.DarkGroupBox();
            this.txtPlayer = new DarkUI.Controls.DarkTextBox();
            this.btnGoto = new DarkUI.Controls.DarkButton();
            this.darkGroupBox12 = new DarkUI.Controls.DarkGroupBox();
            this.txtSetInt = new DarkUI.Controls.DarkTextBox();
            this.btnSetInt = new DarkUI.Controls.DarkButton();
            this.numSetInt = new DarkUI.Controls.DarkNumericUpDown();
            this.btnIncreaseInt = new DarkUI.Controls.DarkButton();
            this.btnDecreaseInt = new DarkUI.Controls.DarkButton();
            this.darkGroupBox8 = new DarkUI.Controls.DarkGroupBox();
            this.numIndexCmd = new DarkUI.Controls.DarkNumericUpDown();
            this.btnGotoIndex = new DarkUI.Controls.DarkButton();
            this.btnGoUpIndex = new DarkUI.Controls.DarkButton();
            this.btnGoDownIndex = new DarkUI.Controls.DarkButton();
            this.darkGroupBox7 = new DarkUI.Controls.DarkGroupBox();
            this.txtCustomAggromon = new DarkUI.Controls.DarkTextBox();
            this.chkInMapCustom = new DarkUI.Controls.DarkCheckBox();
            this.txtInMap = new DarkUI.Controls.DarkLabel();
            this.btnProvokeInMapOff = new DarkUI.Controls.DarkButton();
            this.btnProvokeInMapOn = new DarkUI.Controls.DarkButton();
            this.txtInRoom = new DarkUI.Controls.DarkLabel();
            this.btnProvokeOff = new DarkUI.Controls.DarkButton();
            this.btnProvokeOn = new DarkUI.Controls.DarkButton();
            this.tabMisc2 = new System.Windows.Forms.TabPage();
            this.darkGroupBox13 = new DarkUI.Controls.DarkGroupBox();
            this.label3 = new DarkUI.Controls.DarkLabel();
            this.chkSkip = new DarkUI.Controls.DarkCheckBox();
            this.btnBotDelay = new DarkUI.Controls.DarkButton();
            this.numBotDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.chkRestartDeath = new DarkUI.Controls.DarkCheckBox();
            this.darkGroupBox10 = new DarkUI.Controls.DarkGroupBox();
            this.txtStopBotMessage = new DarkUI.Controls.DarkTextBox();
            this.btnStopBotWithMessageCmd = new DarkUI.Controls.DarkButton();
            this.btnStop = new DarkUI.Controls.DarkButton();
            this.btnRestart = new DarkUI.Controls.DarkButton();
            this.darkGroupBox9 = new DarkUI.Controls.DarkGroupBox();
            this.btnLoad = new DarkUI.Controls.DarkButton();
            this.txtAuthor = new DarkUI.Controls.DarkTextBox();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.txtDescription = new DarkUI.Controls.DarkTextBox();
            this.chkMerge = new DarkUI.Controls.DarkCheckBox();
            this.grpPacketlist = new DarkUI.Controls.DarkGroupBox();
            this.chkEnablePacketlistSpam = new DarkUI.Controls.DarkCheckBox();
            this.btnPacketlistAdd = new DarkUI.Controls.DarkButton();
            this.txtPacketlistCommands = new DarkUI.Controls.DarkLabel();
            this.darkLabel3 = new DarkUI.Controls.DarkLabel();
            this.btnPacketlistOnCmd = new DarkUI.Controls.DarkButton();
            this.btnPacketlistOffCmd = new DarkUI.Controls.DarkButton();
            this.btnPacketlistClearCmd = new DarkUI.Controls.DarkButton();
            this.btnPacketlistRemoveCmd = new DarkUI.Controls.DarkButton();
            this.btnPacketlistSetDelayCmd = new DarkUI.Controls.DarkButton();
            this.txtPlistDelay = new DarkUI.Controls.DarkLabel();
            this.numPacketlistDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.btnPacketlistAddCmd = new DarkUI.Controls.DarkButton();
            this.txtPacketlist = new DarkUI.Controls.DarkTextBox();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.darkGroupBox19 = new DarkUI.Controls.DarkGroupBox();
            this.chkSaveState = new DarkUI.Controls.DarkCheckBox();
            this.chkProvokeAllMon = new DarkUI.Controls.DarkCheckBox();
            this.chkProvoke = new DarkUI.Controls.DarkCheckBox();
            this.chkGender = new DarkUI.Controls.DarkCheckBox();
            this.label6 = new DarkUI.Controls.DarkLabel();
            this.chkInfiniteRange = new DarkUI.Controls.DarkCheckBox();
            this.numOptionsTimer = new DarkUI.Controls.DarkNumericUpDown();
            this.chkMagnet = new DarkUI.Controls.DarkCheckBox();
            this.chkUntarget = new DarkUI.Controls.DarkCheckBox();
            this.chkLag = new DarkUI.Controls.DarkCheckBox();
            this.chkEnableSettings = new DarkUI.Controls.DarkCheckBox();
            this.chkHidePlayers = new DarkUI.Controls.DarkCheckBox();
            this.chkDisableAnims = new DarkUI.Controls.DarkCheckBox();
            this.chkSkipCutscenes = new DarkUI.Controls.DarkCheckBox();
            this.label8 = new DarkUI.Controls.DarkLabel();
            this.numWalkSpeed = new DarkUI.Controls.DarkNumericUpDown();
            this.darkGroupBox6 = new DarkUI.Controls.DarkGroupBox();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.btnLog = new DarkUI.Controls.DarkButton();
            this.btnLogDebug = new DarkUI.Controls.DarkButton();
            this.lstLogText = new DarkUI.Controls.DarkListBox(this.components);
            this.txtLog = new DarkUI.Controls.DarkTextBox();
            this.label5 = new DarkUI.Controls.DarkLabel();
            this.txtSoundItem = new DarkUI.Controls.DarkTextBox();
            this.btnSoundAdd = new DarkUI.Controls.DarkButton();
            this.btnSoundDelete = new DarkUI.Controls.DarkButton();
            this.btnSoundTest = new DarkUI.Controls.DarkButton();
            this.lstSoundItems = new DarkUI.Controls.DarkListBox(this.components);
            this.label9 = new DarkUI.Controls.DarkLabel();
            this.grpLogin = new DarkUI.Controls.DarkGroupBox();
            this.chkAFK = new DarkUI.Controls.DarkCheckBox();
            this.cbServers = new DarkUI.Controls.DarkComboBox();
            this.chkRelogRetry = new DarkUI.Controls.DarkCheckBox();
            this.chkRelog = new DarkUI.Controls.DarkCheckBox();
            this.numRelogDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.label7 = new DarkUI.Controls.DarkLabel();
            this.tabOptions2 = new System.Windows.Forms.TabPage();
            this.chkWarningMsgFilter = new DarkUI.Controls.DarkCheckBox();
            this.grpMapSwf = new DarkUI.Controls.DarkGroupBox();
            this.btnReloadMap = new DarkUI.Controls.DarkButton();
            this.txtMapSwf = new DarkUI.Controls.DarkTextBox();
            this.btnLoadMapSwf = new DarkUI.Controls.DarkButton();
            this.btnLoadMapSwfCmd = new DarkUI.Controls.DarkButton();
            this.darkGroupBox17 = new DarkUI.Controls.DarkGroupBox();
            this.txtClassName = new DarkUI.Controls.DarkTextBox();
            this.btnSetCustomClassName = new DarkUI.Controls.DarkButton();
            this.txtUsername = new DarkUI.Controls.DarkTextBox();
            this.txtGuild = new DarkUI.Controls.DarkTextBox();
            this.btnchangeGuild = new DarkUI.Controls.DarkButton();
            this.btnChangeGuildCmd = new DarkUI.Controls.DarkButton();
            this.btnchangeName = new DarkUI.Controls.DarkButton();
            this.btnChangeNameCmd = new DarkUI.Controls.DarkButton();
            this.darkGroupBox16 = new DarkUI.Controls.DarkGroupBox();
            this.btnAddClientGold = new DarkUI.Controls.DarkButton();
            this.btnAddClientACs = new DarkUI.Controls.DarkButton();
            this.chkToggleMute = new DarkUI.Controls.DarkCheckBox();
            this.darkGroupBox15 = new DarkUI.Controls.DarkGroupBox();
            this.cmdSetClientLevel = new DarkUI.Controls.DarkButton();
            this.btnSetJoinLevel = new DarkUI.Controls.DarkButton();
            this.groupBox1 = new DarkUI.Controls.DarkGroupBox();
            this.btnAddInfoMsg = new DarkUI.Controls.DarkButton();
            this.btnAddWarnMsg = new DarkUI.Controls.DarkButton();
            this.inputMsgClient = new DarkUI.Controls.DarkTextBox();
            this.chkChangeRoomTag = new DarkUI.Controls.DarkCheckBox();
            this.grpAccessLevel = new DarkUI.Controls.DarkGroupBox();
            this.btnSetMem = new DarkUI.Controls.DarkButton();
            this.btnSetModerator = new DarkUI.Controls.DarkButton();
            this.btnSetNonMem = new DarkUI.Controls.DarkButton();
            this.chkChangeChat = new DarkUI.Controls.DarkCheckBox();
            this.chkHideYulgarPlayers = new DarkUI.Controls.DarkCheckBox();
            this.chkAntiAfk = new DarkUI.Controls.DarkCheckBox();
            this.grpAlignment = new DarkUI.Controls.DarkGroupBox();
            this.btnSetChaos = new DarkUI.Controls.DarkButton();
            this.btnSetUndecided = new DarkUI.Controls.DarkButton();
            this.btnSetGood = new DarkUI.Controls.DarkButton();
            this.btnSetEvil = new DarkUI.Controls.DarkButton();
            this.tabBots = new System.Windows.Forms.TabPage();
            this.btnSaveDirectory = new DarkUI.Controls.DarkButton();
            this.darkPanel1 = new DarkUI.Controls.DarkPanel();
            this.treeBots = new System.Windows.Forms.TreeView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtSavedDesc = new DarkUI.Controls.DarkTextBox();
            this.lblBoosts = new DarkUI.Controls.DarkLabel();
            this.lblDrops = new DarkUI.Controls.DarkLabel();
            this.lblQuests = new DarkUI.Controls.DarkLabel();
            this.lblSkills = new DarkUI.Controls.DarkLabel();
            this.lblCommands = new DarkUI.Controls.DarkLabel();
            this.lblItems = new DarkUI.Controls.DarkLabel();
            this.txtSavedAuthor = new DarkUI.Controls.DarkTextBox();
            this.lblBots = new DarkUI.Controls.DarkLabel();
            this.txtSavedAdd = new DarkUI.Controls.DarkTextBox();
            this.btnSavedAdd = new DarkUI.Controls.DarkButton();
            this.txtSaved = new DarkUI.Controls.DarkTextBox();
            this.btnSearchCmd = new DarkUI.Controls.DarkButton();
            this.txtSearchCmd = new DarkUI.Controls.DarkTextBox();
            this.colorfulCommands = new DarkUI.Controls.DarkCheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDown = new DarkUI.Controls.DarkButton();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnClear = new DarkUI.Controls.DarkButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnRemove = new DarkUI.Controls.DarkButton();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnUp = new DarkUI.Controls.DarkButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnBotStart = new DarkUI.Controls.DarkButton();
            this.btnBotPause = new DarkUI.Controls.DarkButton();
            this.btnBotStop = new DarkUI.Controls.DarkButton();
            this.btnBotResume = new DarkUI.Controls.DarkButton();
            this.panel13 = new System.Windows.Forms.Panel();
            this.cbLists = new DarkUI.Controls.DarkComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.checkBox1 = new DarkUI.Controls.DarkCheckBox();
            this.chkBuffup = new DarkUI.Controls.DarkCheckBox();
            this.BotManagerMenuStrip = new DarkUI.Controls.DarkContextMenu();
            this.changeFontsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multilineToggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleTabpagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllCommandListsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.darkButton2 = new DarkUI.Controls.DarkButton();
            this.mainTabControl.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabCombat.SuspendLayout();
            this.darkGroupBox30.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkillCmdHPMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSkillCmd)).BeginInit();
            this.darkGroupBox29.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSafe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSkillD)).BeginInit();
            this.darkGroupBox28.SuspendLayout();
            this.darkGroupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRestMP)).BeginInit();
            this.darkGroupBox26.SuspendLayout();
            this.darkGroupBox25.SuspendLayout();
            this.tabItem.SuspendLayout();
            this.darkGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMapItem)).BeginInit();
            this.darkGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDropDelay)).BeginInit();
            this.darkGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numShopId)).BeginInit();
            this.darkGroupBox2.SuspendLayout();
            this.tabMap.SuspendLayout();
            this.darkGroupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkY)).BeginInit();
            this.darkGroupBox1.SuspendLayout();
            this.tabQuest.SuspendLayout();
            this.darkGroupBox20.SuspendLayout();
            this.darkGroupBox21.SuspendLayout();
            this.tabMisc.SuspendLayout();
            this.darkGroupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPacketDelay)).BeginInit();
            this.darkGroupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSetFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeepTimes)).BeginInit();
            this.darkGroupBox22.SuspendLayout();
            this.darkGroupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.darkGroupBox14.SuspendLayout();
            this.darkGroupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSetInt)).BeginInit();
            this.darkGroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIndexCmd)).BeginInit();
            this.darkGroupBox7.SuspendLayout();
            this.tabMisc2.SuspendLayout();
            this.darkGroupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBotDelay)).BeginInit();
            this.darkGroupBox10.SuspendLayout();
            this.darkGroupBox9.SuspendLayout();
            this.grpPacketlist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPacketlistDelay)).BeginInit();
            this.tabOptions.SuspendLayout();
            this.darkGroupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOptionsTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkSpeed)).BeginInit();
            this.darkGroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.grpLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).BeginInit();
            this.tabOptions2.SuspendLayout();
            this.grpMapSwf.SuspendLayout();
            this.darkGroupBox17.SuspendLayout();
            this.darkGroupBox16.SuspendLayout();
            this.darkGroupBox15.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpAccessLevel.SuspendLayout();
            this.grpAlignment.SuspendLayout();
            this.tabBots.SuspendLayout();
            this.darkPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.BotManagerMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstCommands
            // 
            this.lstCommands.AllowDrop = true;
            this.lstCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCommands.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstCommands.FormattingEnabled = true;
            this.lstCommands.HorizontalScrollbar = true;
            this.lstCommands.IntegralHeight = false;
            this.lstCommands.ItemHeight = 15;
            this.lstCommands.Location = new System.Drawing.Point(3, 3);
            this.lstCommands.Margin = new System.Windows.Forms.Padding(0);
            this.lstCommands.Name = "lstCommands";
            this.lstCommands.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstCommands.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstCommands.Size = new System.Drawing.Size(260, 271);
            this.lstCommands.TabIndex = 1;
            this.lstCommands.Click += new System.EventHandler(this.lstCommands_Click);
            this.lstCommands.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCommands_DrawItem);
            this.lstCommands.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstCommands_DragDrop);
            this.lstCommands.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstCommands_DragEnter);
            this.lstCommands.DoubleClick += new System.EventHandler(this.lstCommands_DoubleClick);
            this.lstCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            this.lstCommands.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCommands_KeyPress);
            this.lstCommands.MouseEnter += new System.EventHandler(this.lstCommands_MouseEnter);
            this.lstCommands.MouseLeave += new System.EventHandler(this.lstCommands_MouseLeave);
            // 
            // lstBoosts
            // 
            this.lstBoosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBoosts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstBoosts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBoosts.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstBoosts.FormattingEnabled = true;
            this.lstBoosts.HorizontalScrollbar = true;
            this.lstBoosts.IntegralHeight = false;
            this.lstBoosts.Location = new System.Drawing.Point(3, 3);
            this.lstBoosts.Margin = new System.Windows.Forms.Padding(0);
            this.lstBoosts.Name = "lstBoosts";
            this.lstBoosts.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstBoosts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBoosts.Size = new System.Drawing.Size(260, 271);
            this.lstBoosts.TabIndex = 25;
            this.lstBoosts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // lstDrops
            // 
            this.lstDrops.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstDrops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDrops.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstDrops.FormattingEnabled = true;
            this.lstDrops.HorizontalScrollbar = true;
            this.lstDrops.IntegralHeight = false;
            this.lstDrops.Location = new System.Drawing.Point(3, 3);
            this.lstDrops.Margin = new System.Windows.Forms.Padding(0);
            this.lstDrops.Name = "lstDrops";
            this.lstDrops.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstDrops.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstDrops.Size = new System.Drawing.Size(260, 271);
            this.lstDrops.TabIndex = 26;
            this.lstDrops.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // lstItems
            // 
            this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstItems.FormattingEnabled = true;
            this.lstItems.HorizontalScrollbar = true;
            this.lstItems.IntegralHeight = false;
            this.lstItems.Location = new System.Drawing.Point(3, 3);
            this.lstItems.Margin = new System.Windows.Forms.Padding(0);
            this.lstItems.Name = "lstItems";
            this.lstItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstItems.Size = new System.Drawing.Size(260, 271);
            this.lstItems.TabIndex = 145;
            this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // lstQuests
            // 
            this.lstQuests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstQuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstQuests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstQuests.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstQuests.FormattingEnabled = true;
            this.lstQuests.HorizontalScrollbar = true;
            this.lstQuests.IntegralHeight = false;
            this.lstQuests.Location = new System.Drawing.Point(3, 3);
            this.lstQuests.Margin = new System.Windows.Forms.Padding(0);
            this.lstQuests.Name = "lstQuests";
            this.lstQuests.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstQuests.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstQuests.Size = new System.Drawing.Size(260, 271);
            this.lstQuests.TabIndex = 27;
            this.lstQuests.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // lstSkills
            // 
            this.lstSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSkills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstSkills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSkills.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstSkills.FormattingEnabled = true;
            this.lstSkills.HorizontalScrollbar = true;
            this.lstSkills.IntegralHeight = false;
            this.lstSkills.Location = new System.Drawing.Point(3, 3);
            this.lstSkills.Margin = new System.Windows.Forms.Padding(0);
            this.lstSkills.Name = "lstSkills";
            this.lstSkills.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstSkills.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSkills.Size = new System.Drawing.Size(260, 271);
            this.lstSkills.TabIndex = 28;
            this.lstSkills.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // lstPackets
            // 
            this.lstPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPackets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstPackets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPackets.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstPackets.FormattingEnabled = true;
            this.lstPackets.HorizontalScrollbar = true;
            this.lstPackets.IntegralHeight = false;
            this.lstPackets.Location = new System.Drawing.Point(3, 3);
            this.lstPackets.Margin = new System.Windows.Forms.Padding(0);
            this.lstPackets.Name = "lstPackets";
            this.lstPackets.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstPackets.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstPackets.Size = new System.Drawing.Size(260, 271);
            this.lstPackets.TabIndex = 150;
            this.lstPackets.Visible = false;
            this.lstPackets.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // mainTabControl
            // 
            this.mainTabControl.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.mainTabControl.AllowDrop = true;
            this.mainTabControl.BackTabColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.mainTabControl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(61)))), ((int)(((byte)(78)))));
            this.mainTabControl.ClosingButtonColor = System.Drawing.Color.WhiteSmoke;
            this.mainTabControl.ClosingMessage = null;
            this.mainTabControl.Controls.Add(this.tabInfo);
            this.mainTabControl.Controls.Add(this.tabCombat);
            this.mainTabControl.Controls.Add(this.tabItem);
            this.mainTabControl.Controls.Add(this.tabMap);
            this.mainTabControl.Controls.Add(this.tabQuest);
            this.mainTabControl.Controls.Add(this.tabMisc);
            this.mainTabControl.Controls.Add(this.tabMisc2);
            this.mainTabControl.Controls.Add(this.tabOptions);
            this.mainTabControl.Controls.Add(this.tabOptions2);
            this.mainTabControl.Controls.Add(this.tabBots);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.mainTabControl.HorizontalLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.mainTabControl.HotTrack = true;
            this.mainTabControl.ItemSize = new System.Drawing.Size(48, 16);
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.Padding = new System.Drawing.Point(3, 2);
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.SelectedTextColor = System.Drawing.Color.White;
            this.mainTabControl.ShowClosingButton = false;
            this.mainTabControl.ShowClosingMessage = false;
            this.mainTabControl.Size = new System.Drawing.Size(522, 344);
            this.mainTabControl.TabIndex = 146;
            this.mainTabControl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
            this.mainTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabInfo
            // 
            this.tabInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabInfo.Controls.Add(this.panel5);
            this.tabInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabInfo.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabInfo.Location = new System.Drawing.Point(4, 20);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(514, 320);
            this.tabInfo.TabIndex = 9;
            this.tabInfo.Text = "Info";
            this.tabInfo.ToolTipText = "The Info about the bot you\'ve loaded";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.richTextBox2);
            this.panel5.Controls.Add(this.rtbInfo);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Margin = new System.Windows.Forms.Padding(5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(506, 312);
            this.panel5.TabIndex = 0;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.ForeColor = System.Drawing.Color.Gainsboro;
            this.richTextBox2.Location = new System.Drawing.Point(3, 285);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ShortcutsEnabled = false;
            this.richTextBox2.Size = new System.Drawing.Size(58, 22);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "Test here";
            this.richTextBox2.Visible = false;
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // rtbInfo
            // 
            this.rtbInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.rtbInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rtbInfo.ForeColor = System.Drawing.Color.Gainsboro;
            this.rtbInfo.HideSelection = false;
            this.rtbInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ReadOnly = true;
            this.rtbInfo.Size = new System.Drawing.Size(506, 312);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "This is where information about a bot will be shown in Rich Text Format.";
            this.rtbInfo.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbInfo_LinkClicked);
            // 
            // tabCombat
            // 
            this.tabCombat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.tabCombat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabCombat.Controls.Add(this.darkGroupBox30);
            this.tabCombat.Controls.Add(this.darkGroupBox29);
            this.tabCombat.Controls.Add(this.darkGroupBox28);
            this.tabCombat.Controls.Add(this.darkGroupBox27);
            this.tabCombat.Controls.Add(this.darkGroupBox26);
            this.tabCombat.Controls.Add(this.darkGroupBox25);
            this.tabCombat.Controls.Add(this.chkSkillCD);
            this.tabCombat.Controls.Add(this.chkExistQuest);
            this.tabCombat.Controls.Add(this.chkExitRest);
            this.tabCombat.Controls.Add(this.chkAllSkillsCD);
            this.tabCombat.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabCombat.Location = new System.Drawing.Point(4, 20);
            this.tabCombat.Margin = new System.Windows.Forms.Padding(0);
            this.tabCombat.Name = "tabCombat";
            this.tabCombat.Padding = new System.Windows.Forms.Padding(3);
            this.tabCombat.Size = new System.Drawing.Size(514, 320);
            this.tabCombat.TabIndex = 0;
            this.tabCombat.Text = "Combat";
            // 
            // darkGroupBox30
            // 
            this.darkGroupBox30.Controls.Add(this.numSkillCmdHPMP);
            this.darkGroupBox30.Controls.Add(this.txtSkillCmdHPMP);
            this.darkGroupBox30.Controls.Add(this.darkLabel10);
            this.darkGroupBox30.Controls.Add(this.chkSkillCmdWait);
            this.darkGroupBox30.Controls.Add(this.txtMonsterSkillCmd);
            this.darkGroupBox30.Controls.Add(this.btnSkillCmd);
            this.darkGroupBox30.Controls.Add(this.numSkillCmd);
            this.darkGroupBox30.Location = new System.Drawing.Point(142, 4);
            this.darkGroupBox30.Name = "darkGroupBox30";
            this.darkGroupBox30.Size = new System.Drawing.Size(210, 70);
            this.darkGroupBox30.TabIndex = 161;
            this.darkGroupBox30.TabStop = false;
            this.darkGroupBox30.Text = "Skill Command";
            // 
            // numSkillCmdHPMP
            // 
            this.numSkillCmdHPMP.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSkillCmdHPMP.Location = new System.Drawing.Point(145, 22);
            this.numSkillCmdHPMP.LoopValues = false;
            this.numSkillCmdHPMP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSkillCmdHPMP.Name = "numSkillCmdHPMP";
            this.numSkillCmdHPMP.Size = new System.Drawing.Size(40, 20);
            this.numSkillCmdHPMP.TabIndex = 74;
            this.numSkillCmdHPMP.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // txtSkillCmdHPMP
            // 
            this.txtSkillCmdHPMP.AutoSize = true;
            this.txtSkillCmdHPMP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSkillCmdHPMP.Location = new System.Drawing.Point(142, 8);
            this.txtSkillCmdHPMP.Name = "txtSkillCmdHPMP";
            this.txtSkillCmdHPMP.Size = new System.Drawing.Size(49, 13);
            this.txtSkillCmdHPMP.TabIndex = 73;
            this.txtSkillCmdHPMP.Text = "HP / MP";
            // 
            // darkLabel10
            // 
            this.darkLabel10.AutoSize = true;
            this.darkLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel10.Location = new System.Drawing.Point(185, 24);
            this.darkLabel10.Name = "darkLabel10";
            this.darkLabel10.Size = new System.Drawing.Size(21, 13);
            this.darkLabel10.TabIndex = 72;
            this.darkLabel10.Text = "(%)";
            // 
            // chkSkillCmdWait
            // 
            this.chkSkillCmdWait.AutoSize = true;
            this.chkSkillCmdWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkSkillCmdWait.Location = new System.Drawing.Point(145, 49);
            this.chkSkillCmdWait.Name = "chkSkillCmdWait";
            this.chkSkillCmdWait.Size = new System.Drawing.Size(47, 17);
            this.chkSkillCmdWait.TabIndex = 61;
            this.chkSkillCmdWait.Text = "Wait";
            // 
            // txtMonsterSkillCmd
            // 
            this.txtMonsterSkillCmd.Location = new System.Drawing.Point(40, 19);
            this.txtMonsterSkillCmd.Name = "txtMonsterSkillCmd";
            this.txtMonsterSkillCmd.Size = new System.Drawing.Size(96, 20);
            this.txtMonsterSkillCmd.TabIndex = 27;
            this.txtMonsterSkillCmd.Text = "Monster (* = any)";
            this.txtMonsterSkillCmd.Click += new System.EventHandler(this.TextboxEnter);
            this.txtMonsterSkillCmd.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnSkillCmd
            // 
            this.btnSkillCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSkillCmd.BackColorUseGeneric = false;
            this.btnSkillCmd.Checked = false;
            this.btnSkillCmd.Location = new System.Drawing.Point(6, 38);
            this.btnSkillCmd.Name = "btnSkillCmd";
            this.btnSkillCmd.Size = new System.Drawing.Size(130, 23);
            this.btnSkillCmd.TabIndex = 38;
            this.btnSkillCmd.Text = "Add skill command";
            this.btnSkillCmd.Click += new System.EventHandler(this.btnAddSkillCmd_Click);
            // 
            // numSkillCmd
            // 
            this.numSkillCmd.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSkillCmd.Location = new System.Drawing.Point(6, 19);
            this.numSkillCmd.LoopValues = false;
            this.numSkillCmd.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSkillCmd.Name = "numSkillCmd";
            this.numSkillCmd.Size = new System.Drawing.Size(33, 20);
            this.numSkillCmd.TabIndex = 37;
            this.numSkillCmd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // darkGroupBox29
            // 
            this.darkGroupBox29.Controls.Add(this.btnAddSafe);
            this.darkGroupBox29.Controls.Add(this.btnAddSkill);
            this.darkGroupBox29.Controls.Add(this.numSkill);
            this.darkGroupBox29.Controls.Add(this.txtSkillSet);
            this.darkGroupBox29.Controls.Add(this.btnAddSkillSet);
            this.darkGroupBox29.Controls.Add(this.darkLabel2);
            this.darkGroupBox29.Controls.Add(this.btnUseSkillSet);
            this.darkGroupBox29.Controls.Add(this.chkSafeGreaterThan);
            this.darkGroupBox29.Controls.Add(this.chkSafeLessThan);
            this.darkGroupBox29.Controls.Add(this.numSafe);
            this.darkGroupBox29.Controls.Add(this.chkSafeHP);
            this.darkGroupBox29.Controls.Add(this.label2);
            this.darkGroupBox29.Controls.Add(this.chkSafeMP);
            this.darkGroupBox29.Controls.Add(this.numSkillD);
            this.darkGroupBox29.Controls.Add(this.label13);
            this.darkGroupBox29.Location = new System.Drawing.Point(142, 74);
            this.darkGroupBox29.Name = "darkGroupBox29";
            this.darkGroupBox29.Size = new System.Drawing.Size(227, 125);
            this.darkGroupBox29.TabIndex = 160;
            this.darkGroupBox29.TabStop = false;
            this.darkGroupBox29.Text = "Skill Set";
            // 
            // btnAddSafe
            // 
            this.btnAddSafe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAddSafe.BackColorUseGeneric = false;
            this.btnAddSafe.Checked = false;
            this.btnAddSafe.Location = new System.Drawing.Point(6, 34);
            this.btnAddSafe.Name = "btnAddSafe";
            this.btnAddSafe.Size = new System.Drawing.Size(99, 20);
            this.btnAddSafe.TabIndex = 43;
            this.btnAddSafe.Text = "Safe skill";
            this.btnAddSafe.Click += new System.EventHandler(this.btnAddSafe2_Click);
            // 
            // btnAddSkill
            // 
            this.btnAddSkill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAddSkill.BackColorUseGeneric = false;
            this.btnAddSkill.Checked = false;
            this.btnAddSkill.Location = new System.Drawing.Point(44, 16);
            this.btnAddSkill.Name = "btnAddSkill";
            this.btnAddSkill.Size = new System.Drawing.Size(61, 20);
            this.btnAddSkill.TabIndex = 42;
            this.btnAddSkill.Text = "Add skill";
            this.btnAddSkill.Click += new System.EventHandler(this.btnAddSkill_Click);
            // 
            // numSkill
            // 
            this.numSkill.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSkill.Location = new System.Drawing.Point(6, 16);
            this.numSkill.LoopValues = false;
            this.numSkill.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSkill.Name = "numSkill";
            this.numSkill.Size = new System.Drawing.Size(37, 20);
            this.numSkill.TabIndex = 40;
            this.numSkill.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtSkillSet
            // 
            this.txtSkillSet.Location = new System.Drawing.Point(6, 60);
            this.txtSkillSet.Name = "txtSkillSet";
            this.txtSkillSet.Size = new System.Drawing.Size(99, 20);
            this.txtSkillSet.TabIndex = 63;
            this.txtSkillSet.Text = "Skill Set Name";
            this.txtSkillSet.Click += new System.EventHandler(this.TextboxEnter);
            this.txtSkillSet.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnAddSkillSet
            // 
            this.btnAddSkillSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAddSkillSet.BackColorUseGeneric = false;
            this.btnAddSkillSet.Checked = false;
            this.btnAddSkillSet.Location = new System.Drawing.Point(6, 79);
            this.btnAddSkillSet.Name = "btnAddSkillSet";
            this.btnAddSkillSet.Size = new System.Drawing.Size(99, 20);
            this.btnAddSkillSet.TabIndex = 64;
            this.btnAddSkillSet.Text = "Add skill set";
            this.btnAddSkillSet.Click += new System.EventHandler(this.btnAddSkillSet_Click);
            // 
            // darkLabel2
            // 
            this.darkLabel2.AutoSize = true;
            this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel2.Location = new System.Drawing.Point(179, 102);
            this.darkLabel2.Name = "darkLabel2";
            this.darkLabel2.Size = new System.Drawing.Size(20, 13);
            this.darkLabel2.TabIndex = 71;
            this.darkLabel2.Text = "ms";
            // 
            // btnUseSkillSet
            // 
            this.btnUseSkillSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnUseSkillSet.BackColorUseGeneric = false;
            this.btnUseSkillSet.Checked = false;
            this.btnUseSkillSet.Location = new System.Drawing.Point(6, 98);
            this.btnUseSkillSet.Name = "btnUseSkillSet";
            this.btnUseSkillSet.Size = new System.Drawing.Size(99, 22);
            this.btnUseSkillSet.TabIndex = 65;
            this.btnUseSkillSet.Text = "Use skill set";
            this.btnUseSkillSet.Click += new System.EventHandler(this.btnUseSkillSet_Click);
            // 
            // chkSafeGreaterThan
            // 
            this.chkSafeGreaterThan.AutoSize = true;
            this.chkSafeGreaterThan.Location = new System.Drawing.Point(122, 42);
            this.chkSafeGreaterThan.Name = "chkSafeGreaterThan";
            this.chkSafeGreaterThan.Size = new System.Drawing.Size(95, 17);
            this.chkSafeGreaterThan.TabIndex = 70;
            this.chkSafeGreaterThan.Text = "is greater than:";
            this.chkSafeGreaterThan.CheckedChanged += new System.EventHandler(this.chkSafeGreaterThan_CheckedChanged);
            // 
            // chkSafeLessThan
            // 
            this.chkSafeLessThan.AutoSize = true;
            this.chkSafeLessThan.Location = new System.Drawing.Point(122, 24);
            this.chkSafeLessThan.Name = "chkSafeLessThan";
            this.chkSafeLessThan.Size = new System.Drawing.Size(80, 17);
            this.chkSafeLessThan.TabIndex = 69;
            this.chkSafeLessThan.Text = "is less than:";
            this.chkSafeLessThan.CheckedChanged += new System.EventHandler(this.chkSafeLessThan_CheckedChanged);
            // 
            // numSafe
            // 
            this.numSafe.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSafe.Location = new System.Drawing.Point(122, 62);
            this.numSafe.LoopValues = false;
            this.numSafe.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSafe.Name = "numSafe";
            this.numSafe.Size = new System.Drawing.Size(51, 20);
            this.numSafe.TabIndex = 41;
            this.numSafe.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // chkSafeHP
            // 
            this.chkSafeHP.AutoSize = true;
            this.chkSafeHP.Location = new System.Drawing.Point(122, 8);
            this.chkSafeHP.Name = "chkSafeHP";
            this.chkSafeHP.Size = new System.Drawing.Size(40, 17);
            this.chkSafeHP.TabIndex = 68;
            this.chkSafeHP.Text = "HP";
            this.chkSafeHP.CheckedChanged += new System.EventHandler(this.chkSafeHP_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label2.Location = new System.Drawing.Point(179, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "(%)";
            // 
            // chkSafeMP
            // 
            this.chkSafeMP.AutoSize = true;
            this.chkSafeMP.Location = new System.Drawing.Point(167, 8);
            this.chkSafeMP.Name = "chkSafeMP";
            this.chkSafeMP.Size = new System.Drawing.Size(41, 17);
            this.chkSafeMP.TabIndex = 58;
            this.chkSafeMP.Text = "MP";
            this.chkSafeMP.CheckedChanged += new System.EventHandler(this.chkSafeMP_CheckedChanged);
            // 
            // numSkillD
            // 
            this.numSkillD.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numSkillD.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSkillD.Location = new System.Drawing.Point(122, 100);
            this.numSkillD.LoopValues = false;
            this.numSkillD.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.numSkillD.Name = "numSkillD";
            this.numSkillD.Size = new System.Drawing.Size(51, 20);
            this.numSkillD.TabIndex = 45;
            this.numSkillD.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label13.Location = new System.Drawing.Point(119, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 13);
            this.label13.TabIndex = 53;
            this.label13.Text = "Skill delay";
            // 
            // darkGroupBox28
            // 
            this.darkGroupBox28.Controls.Add(this.btnCancelTargetCmd);
            this.darkGroupBox28.Controls.Add(this.btnCancelAutoAttackCmd);
            this.darkGroupBox28.Location = new System.Drawing.Point(356, 4);
            this.darkGroupBox28.Name = "darkGroupBox28";
            this.darkGroupBox28.Size = new System.Drawing.Size(155, 70);
            this.darkGroupBox28.TabIndex = 159;
            this.darkGroupBox28.TabStop = false;
            this.darkGroupBox28.Text = "Cancel";
            // 
            // btnCancelTargetCmd
            // 
            this.btnCancelTargetCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCancelTargetCmd.BackColorUseGeneric = false;
            this.btnCancelTargetCmd.Checked = false;
            this.btnCancelTargetCmd.Location = new System.Drawing.Point(6, 19);
            this.btnCancelTargetCmd.Name = "btnCancelTargetCmd";
            this.btnCancelTargetCmd.Size = new System.Drawing.Size(142, 23);
            this.btnCancelTargetCmd.TabIndex = 66;
            this.btnCancelTargetCmd.Text = "Cancel target (cmd)";
            this.btnCancelTargetCmd.Click += new System.EventHandler(this.btnCancelTargetCmd_Click);
            // 
            // btnCancelAutoAttackCmd
            // 
            this.btnCancelAutoAttackCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCancelAutoAttackCmd.BackColorUseGeneric = false;
            this.btnCancelAutoAttackCmd.Checked = false;
            this.btnCancelAutoAttackCmd.Location = new System.Drawing.Point(6, 43);
            this.btnCancelAutoAttackCmd.Name = "btnCancelAutoAttackCmd";
            this.btnCancelAutoAttackCmd.Size = new System.Drawing.Size(142, 23);
            this.btnCancelAutoAttackCmd.TabIndex = 67;
            this.btnCancelAutoAttackCmd.Text = "Cancel auto attack (cmd)";
            this.btnCancelAutoAttackCmd.Click += new System.EventHandler(this.btnCancelAutoAttackCmd_Click);
            // 
            // darkGroupBox27
            // 
            this.darkGroupBox27.Controls.Add(this.label12);
            this.darkGroupBox27.Controls.Add(this.chkHP);
            this.darkGroupBox27.Controls.Add(this.numRest);
            this.darkGroupBox27.Controls.Add(this.chkMP);
            this.darkGroupBox27.Controls.Add(this.numRestMP);
            this.darkGroupBox27.Controls.Add(this.label10);
            this.darkGroupBox27.Controls.Add(this.label11);
            this.darkGroupBox27.Controls.Add(this.btnRestF);
            this.darkGroupBox27.Controls.Add(this.btnRest);
            this.darkGroupBox27.Location = new System.Drawing.Point(373, 74);
            this.darkGroupBox27.Name = "darkGroupBox27";
            this.darkGroupBox27.Size = new System.Drawing.Size(138, 102);
            this.darkGroupBox27.TabIndex = 158;
            this.darkGroupBox27.TabStop = false;
            this.darkGroupBox27.Text = "Rest";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label12.Location = new System.Drawing.Point(3, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "Rest if:";
            // 
            // chkHP
            // 
            this.chkHP.AutoSize = true;
            this.chkHP.Location = new System.Drawing.Point(6, 32);
            this.chkHP.Name = "chkHP";
            this.chkHP.Size = new System.Drawing.Size(55, 17);
            this.chkHP.TabIndex = 47;
            this.chkHP.Text = "HP <=";
            // 
            // numRest
            // 
            this.numRest.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numRest.Location = new System.Drawing.Point(67, 31);
            this.numRest.LoopValues = false;
            this.numRest.Name = "numRest";
            this.numRest.Size = new System.Drawing.Size(45, 20);
            this.numRest.TabIndex = 48;
            this.numRest.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // chkMP
            // 
            this.chkMP.AutoSize = true;
            this.chkMP.Location = new System.Drawing.Point(6, 52);
            this.chkMP.Name = "chkMP";
            this.chkMP.Size = new System.Drawing.Size(56, 17);
            this.chkMP.TabIndex = 49;
            this.chkMP.Text = "MP <=";
            // 
            // numRestMP
            // 
            this.numRestMP.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numRestMP.Location = new System.Drawing.Point(67, 51);
            this.numRestMP.LoopValues = false;
            this.numRestMP.Name = "numRestMP";
            this.numRestMP.Size = new System.Drawing.Size(45, 20);
            this.numRestMP.TabIndex = 50;
            this.numRestMP.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label10.Location = new System.Drawing.Point(118, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 55;
            this.label10.Text = "%";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label11.Location = new System.Drawing.Point(118, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "%";
            // 
            // btnRestF
            // 
            this.btnRestF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnRestF.BackColorUseGeneric = false;
            this.btnRestF.Checked = false;
            this.btnRestF.Location = new System.Drawing.Point(65, 74);
            this.btnRestF.Name = "btnRestF";
            this.btnRestF.Size = new System.Drawing.Size(65, 22);
            this.btnRestF.TabIndex = 44;
            this.btnRestF.Text = "Rest fully";
            this.btnRestF.Click += new System.EventHandler(this.btnRestF_Click);
            // 
            // btnRest
            // 
            this.btnRest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnRest.BackColorUseGeneric = false;
            this.btnRest.Checked = false;
            this.btnRest.Location = new System.Drawing.Point(6, 74);
            this.btnRest.Name = "btnRest";
            this.btnRest.Size = new System.Drawing.Size(60, 22);
            this.btnRest.TabIndex = 43;
            this.btnRest.Text = "Rest";
            this.btnRest.Click += new System.EventHandler(this.btnRest_Click);
            // 
            // darkGroupBox26
            // 
            this.darkGroupBox26.Controls.Add(this.rbItems);
            this.darkGroupBox26.Controls.Add(this.btnKillF);
            this.darkGroupBox26.Controls.Add(this.rbTemp);
            this.darkGroupBox26.Controls.Add(this.txtKillFMon);
            this.darkGroupBox26.Controls.Add(this.txtKillFItem);
            this.darkGroupBox26.Controls.Add(this.txtKillFQ);
            this.darkGroupBox26.Location = new System.Drawing.Point(3, 74);
            this.darkGroupBox26.Name = "darkGroupBox26";
            this.darkGroupBox26.Size = new System.Drawing.Size(136, 125);
            this.darkGroupBox26.TabIndex = 157;
            this.darkGroupBox26.TabStop = false;
            this.darkGroupBox26.Text = "Kill For Item";
            // 
            // rbItems
            // 
            this.rbItems.AutoSize = true;
            this.rbItems.Checked = true;
            this.rbItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.rbItems.Location = new System.Drawing.Point(6, 19);
            this.rbItems.Name = "rbItems";
            this.rbItems.Size = new System.Drawing.Size(50, 17);
            this.rbItems.TabIndex = 30;
            this.rbItems.TabStop = true;
            this.rbItems.Text = "Items";
            // 
            // btnKillF
            // 
            this.btnKillF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnKillF.BackColorUseGeneric = false;
            this.btnKillF.Checked = false;
            this.btnKillF.Location = new System.Drawing.Point(6, 96);
            this.btnKillF.Name = "btnKillF";
            this.btnKillF.Size = new System.Drawing.Size(124, 21);
            this.btnKillF.TabIndex = 29;
            this.btnKillF.Text = "Kill for ...";
            this.btnKillF.Click += new System.EventHandler(this.btnKillF_Click);
            // 
            // rbTemp
            // 
            this.rbTemp.AutoSize = true;
            this.rbTemp.ForeColor = System.Drawing.Color.Gainsboro;
            this.rbTemp.Location = new System.Drawing.Point(56, 19);
            this.rbTemp.Name = "rbTemp";
            this.rbTemp.Size = new System.Drawing.Size(79, 17);
            this.rbTemp.TabIndex = 31;
            this.rbTemp.Text = "Temp items";
            // 
            // txtKillFMon
            // 
            this.txtKillFMon.Location = new System.Drawing.Point(6, 39);
            this.txtKillFMon.Name = "txtKillFMon";
            this.txtKillFMon.Size = new System.Drawing.Size(124, 20);
            this.txtKillFMon.TabIndex = 32;
            this.txtKillFMon.Text = "Monster (* = any)";
            this.txtKillFMon.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtKillFMon.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtKillFItem
            // 
            this.txtKillFItem.Location = new System.Drawing.Point(6, 58);
            this.txtKillFItem.Name = "txtKillFItem";
            this.txtKillFItem.Size = new System.Drawing.Size(124, 20);
            this.txtKillFItem.TabIndex = 33;
            this.txtKillFItem.Text = "Item name";
            this.txtKillFItem.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtKillFItem.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtKillFQ
            // 
            this.txtKillFQ.Location = new System.Drawing.Point(6, 77);
            this.txtKillFQ.Name = "txtKillFQ";
            this.txtKillFQ.Size = new System.Drawing.Size(124, 20);
            this.txtKillFQ.TabIndex = 34;
            this.txtKillFQ.Text = "Quantity (* = any)";
            this.txtKillFQ.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtKillFQ.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // darkGroupBox25
            // 
            this.darkGroupBox25.Controls.Add(this.txtMonster);
            this.darkGroupBox25.Controls.Add(this.btnKill);
            this.darkGroupBox25.Controls.Add(this.btnAttack);
            this.darkGroupBox25.Location = new System.Drawing.Point(3, 4);
            this.darkGroupBox25.Name = "darkGroupBox25";
            this.darkGroupBox25.Size = new System.Drawing.Size(136, 63);
            this.darkGroupBox25.TabIndex = 156;
            this.darkGroupBox25.TabStop = false;
            this.darkGroupBox25.Text = "Kill / Attack";
            // 
            // txtMonster
            // 
            this.txtMonster.Location = new System.Drawing.Point(6, 19);
            this.txtMonster.Name = "txtMonster";
            this.txtMonster.Size = new System.Drawing.Size(124, 20);
            this.txtMonster.TabIndex = 27;
            this.txtMonster.Text = "Monster (* = any)";
            this.txtMonster.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtMonster.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnKill
            // 
            this.btnKill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnKill.BackColorUseGeneric = false;
            this.btnKill.Checked = false;
            this.btnKill.Location = new System.Drawing.Point(6, 38);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(62, 19);
            this.btnKill.TabIndex = 54;
            this.btnKill.Text = "Kill";
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // btnAttack
            // 
            this.btnAttack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAttack.BackColorUseGeneric = false;
            this.btnAttack.Checked = false;
            this.btnAttack.Location = new System.Drawing.Point(67, 38);
            this.btnAttack.Name = "btnAttack";
            this.btnAttack.Size = new System.Drawing.Size(63, 19);
            this.btnAttack.TabIndex = 54;
            this.btnAttack.Text = "Attack";
            this.btnAttack.Click += new System.EventHandler(this.btnAttack_Click);
            // 
            // chkSkillCD
            // 
            this.chkSkillCD.AutoSize = true;
            this.chkSkillCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkSkillCD.Location = new System.Drawing.Point(3, 243);
            this.chkSkillCD.Name = "chkSkillCD";
            this.chkSkillCD.Size = new System.Drawing.Size(143, 17);
            this.chkSkillCD.TabIndex = 60;
            this.chkSkillCD.Text = "Wait for skill to cooldown";
            // 
            // chkExistQuest
            // 
            this.chkExistQuest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkExistQuest.Location = new System.Drawing.Point(3, 289);
            this.chkExistQuest.Name = "chkExistQuest";
            this.chkExistQuest.Size = new System.Drawing.Size(198, 17);
            this.chkExistQuest.TabIndex = 51;
            this.chkExistQuest.Text = "Exit combat before completing quest";
            // 
            // chkExitRest
            // 
            this.chkExitRest.AutoSize = true;
            this.chkExitRest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkExitRest.Location = new System.Drawing.Point(3, 266);
            this.chkExitRest.Name = "chkExitRest";
            this.chkExitRest.Size = new System.Drawing.Size(147, 17);
            this.chkExitRest.TabIndex = 36;
            this.chkExitRest.Text = "Exit combat before resting";
            // 
            // chkAllSkillsCD
            // 
            this.chkAllSkillsCD.AutoSize = true;
            this.chkAllSkillsCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkAllSkillsCD.Location = new System.Drawing.Point(3, 214);
            this.chkAllSkillsCD.Name = "chkAllSkillsCD";
            this.chkAllSkillsCD.Size = new System.Drawing.Size(164, 30);
            this.chkAllSkillsCD.TabIndex = 35;
            this.chkAllSkillsCD.Text = "Wait for all skills to cool down\r\nbefore attacking";
            // 
            // tabItem
            // 
            this.tabItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabItem.Controls.Add(this.darkGroupBox5);
            this.tabItem.Controls.Add(this.darkGroupBox4);
            this.tabItem.Controls.Add(this.darkGroupBox3);
            this.tabItem.Controls.Add(this.darkGroupBox2);
            this.tabItem.Controls.Add(this.btnBoost);
            this.tabItem.Controls.Add(this.cbBoosts);
            this.tabItem.Controls.Add(this.btnSwap);
            this.tabItem.Controls.Add(this.txtSwapInv);
            this.tabItem.Controls.Add(this.txtSwapBank);
            this.tabItem.Controls.Add(this.btnWhitelist);
            this.tabItem.Controls.Add(this.btnBoth);
            this.tabItem.Controls.Add(this.txtWhitelist);
            this.tabItem.Controls.Add(this.btnItem);
            this.tabItem.Controls.Add(this.btnUnbanklst);
            this.tabItem.Controls.Add(this.txtItem);
            this.tabItem.Controls.Add(this.cbItemCmds);
            this.tabItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabItem.Location = new System.Drawing.Point(4, 20);
            this.tabItem.Margin = new System.Windows.Forms.Padding(0);
            this.tabItem.Name = "tabItem";
            this.tabItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabItem.Size = new System.Drawing.Size(514, 320);
            this.tabItem.TabIndex = 1;
            this.tabItem.Text = "Item";
            // 
            // darkGroupBox5
            // 
            this.darkGroupBox5.Controls.Add(this.btnMapItem);
            this.darkGroupBox5.Controls.Add(this.numMapItem);
            this.darkGroupBox5.Location = new System.Drawing.Point(296, 182);
            this.darkGroupBox5.Name = "darkGroupBox5";
            this.darkGroupBox5.Size = new System.Drawing.Size(155, 43);
            this.darkGroupBox5.TabIndex = 161;
            this.darkGroupBox5.TabStop = false;
            this.darkGroupBox5.Text = "Map Item";
            // 
            // btnMapItem
            // 
            this.btnMapItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnMapItem.BackColorUseGeneric = false;
            this.btnMapItem.Checked = false;
            this.btnMapItem.Location = new System.Drawing.Point(67, 18);
            this.btnMapItem.Name = "btnMapItem";
            this.btnMapItem.Size = new System.Drawing.Size(82, 20);
            this.btnMapItem.TabIndex = 29;
            this.btnMapItem.Text = "Get map item";
            this.btnMapItem.Click += new System.EventHandler(this.btnMapItem_Click);
            // 
            // numMapItem
            // 
            this.numMapItem.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numMapItem.Location = new System.Drawing.Point(8, 18);
            this.numMapItem.LoopValues = false;
            this.numMapItem.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMapItem.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMapItem.Name = "numMapItem";
            this.numMapItem.Size = new System.Drawing.Size(56, 20);
            this.numMapItem.TabIndex = 30;
            this.numMapItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // darkGroupBox4
            // 
            this.darkGroupBox4.Controls.Add(this.chkPickupAll);
            this.darkGroupBox4.Controls.Add(this.chkPickup);
            this.darkGroupBox4.Controls.Add(this.chkReject);
            this.darkGroupBox4.Controls.Add(this.chkPickupAcTag);
            this.darkGroupBox4.Controls.Add(this.label1);
            this.darkGroupBox4.Controls.Add(this.chkBankOnStop);
            this.darkGroupBox4.Controls.Add(this.numDropDelay);
            this.darkGroupBox4.Controls.Add(this.chkRejectAll);
            this.darkGroupBox4.Location = new System.Drawing.Point(296, 4);
            this.darkGroupBox4.Name = "darkGroupBox4";
            this.darkGroupBox4.Size = new System.Drawing.Size(155, 172);
            this.darkGroupBox4.TabIndex = 160;
            this.darkGroupBox4.TabStop = false;
            this.darkGroupBox4.Text = "Options";
            // 
            // chkPickupAll
            // 
            this.chkPickupAll.AutoSize = true;
            this.chkPickupAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.chkPickupAll.Location = new System.Drawing.Point(11, 19);
            this.chkPickupAll.Name = "chkPickupAll";
            this.chkPickupAll.Size = new System.Drawing.Size(101, 17);
            this.chkPickupAll.TabIndex = 149;
            this.chkPickupAll.Text = "Pick up all items";
            // 
            // chkPickup
            // 
            this.chkPickup.AutoSize = true;
            this.chkPickup.Checked = true;
            this.chkPickup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPickup.Location = new System.Drawing.Point(11, 40);
            this.chkPickup.Name = "chkPickup";
            this.chkPickup.Size = new System.Drawing.Size(113, 17);
            this.chkPickup.TabIndex = 24;
            this.chkPickup.Text = "Pick up whitelisted";
            // 
            // chkReject
            // 
            this.chkReject.AutoSize = true;
            this.chkReject.Location = new System.Drawing.Point(11, 82);
            this.chkReject.Name = "chkReject";
            this.chkReject.Size = new System.Drawing.Size(129, 17);
            this.chkReject.TabIndex = 25;
            this.chkReject.Text = "Reject non-whitelisted";
            // 
            // chkPickupAcTag
            // 
            this.chkPickupAcTag.AutoSize = true;
            this.chkPickupAcTag.Location = new System.Drawing.Point(11, 61);
            this.chkPickupAcTag.Name = "chkPickupAcTag";
            this.chkPickupAcTag.Size = new System.Drawing.Size(136, 17);
            this.chkPickupAcTag.TabIndex = 157;
            this.chkPickupAcTag.Text = "Pick up AC-tagged only";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label1.Location = new System.Drawing.Point(82, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 152;
            this.label1.Text = "Drop Delay";
            // 
            // chkBankOnStop
            // 
            this.chkBankOnStop.AutoSize = true;
            this.chkBankOnStop.Location = new System.Drawing.Point(11, 103);
            this.chkBankOnStop.Name = "chkBankOnStop";
            this.chkBankOnStop.Size = new System.Drawing.Size(93, 17);
            this.chkBankOnStop.TabIndex = 148;
            this.chkBankOnStop.Text = "Bank on Stop ";
            this.chkBankOnStop.CheckedChanged += new System.EventHandler(this.chkBankOnStop_CheckedChanged);
            // 
            // numDropDelay
            // 
            this.numDropDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDropDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numDropDelay.Location = new System.Drawing.Point(11, 147);
            this.numDropDelay.LoopValues = false;
            this.numDropDelay.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numDropDelay.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numDropDelay.Name = "numDropDelay";
            this.numDropDelay.Size = new System.Drawing.Size(65, 20);
            this.numDropDelay.TabIndex = 151;
            this.numDropDelay.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numDropDelay.ValueChanged += new System.EventHandler(this.numDropDelay_ValueChanged);
            // 
            // chkRejectAll
            // 
            this.chkRejectAll.AutoSize = true;
            this.chkRejectAll.Enabled = false;
            this.chkRejectAll.Location = new System.Drawing.Point(11, 124);
            this.chkRejectAll.Name = "chkRejectAll";
            this.chkRejectAll.Size = new System.Drawing.Size(96, 17);
            this.chkRejectAll.TabIndex = 150;
            this.chkRejectAll.Text = "Reject all items";
            this.chkRejectAll.Visible = false;
            // 
            // darkGroupBox3
            // 
            this.darkGroupBox3.Controls.Add(this.darkLabel1);
            this.darkGroupBox3.Controls.Add(this.txtShopItemID);
            this.darkGroupBox3.Controls.Add(this.txtShopID);
            this.darkGroupBox3.Controls.Add(this.txtItemID);
            this.darkGroupBox3.Controls.Add(this.btnBuyItemByID);
            this.darkGroupBox3.Controls.Add(this.numShopId);
            this.darkGroupBox3.Controls.Add(this.btnBuyFastByID);
            this.darkGroupBox3.Controls.Add(this.btnLoadShop);
            this.darkGroupBox3.Controls.Add(this.txtShopItem);
            this.darkGroupBox3.Controls.Add(this.btnBuy);
            this.darkGroupBox3.Controls.Add(this.btnBuyFast);
            this.darkGroupBox3.Location = new System.Drawing.Point(6, 141);
            this.darkGroupBox3.Name = "darkGroupBox3";
            this.darkGroupBox3.Size = new System.Drawing.Size(183, 157);
            this.darkGroupBox3.TabIndex = 159;
            this.darkGroupBox3.TabStop = false;
            this.darkGroupBox3.Text = "Shop";
            // 
            // darkLabel1
            // 
            this.darkLabel1.AutoSize = true;
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(3, 89);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(76, 13);
            this.darkLabel1.TabIndex = 167;
            this.darkLabel1.Text = "Buy Item by ID";
            // 
            // txtShopItemID
            // 
            this.txtShopItemID.Location = new System.Drawing.Point(105, 106);
            this.txtShopItemID.Name = "txtShopItemID";
            this.txtShopItemID.Size = new System.Drawing.Size(72, 20);
            this.txtShopItemID.TabIndex = 166;
            this.txtShopItemID.Text = "Shop item ID";
            this.txtShopItemID.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtShopItemID.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtShopID
            // 
            this.txtShopID.Location = new System.Drawing.Point(54, 106);
            this.txtShopID.Name = "txtShopID";
            this.txtShopID.Size = new System.Drawing.Size(50, 20);
            this.txtShopID.TabIndex = 165;
            this.txtShopID.Text = "Shop ID";
            this.txtShopID.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtShopID.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtItemID
            // 
            this.txtItemID.Location = new System.Drawing.Point(6, 106);
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.Size = new System.Drawing.Size(47, 20);
            this.txtItemID.TabIndex = 163;
            this.txtItemID.Text = "Item ID";
            this.txtItemID.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtItemID.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnBuyItemByID
            // 
            this.btnBuyItemByID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBuyItemByID.BackColorUseGeneric = false;
            this.btnBuyItemByID.Checked = false;
            this.btnBuyItemByID.Location = new System.Drawing.Point(6, 129);
            this.btnBuyItemByID.Name = "btnBuyItemByID";
            this.btnBuyItemByID.Size = new System.Drawing.Size(81, 22);
            this.btnBuyItemByID.TabIndex = 162;
            this.btnBuyItemByID.Text = "Buy item";
            this.btnBuyItemByID.Click += new System.EventHandler(this.btnBuyItemByID_Click);
            // 
            // numShopId
            // 
            this.numShopId.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numShopId.Location = new System.Drawing.Point(6, 16);
            this.numShopId.LoopValues = false;
            this.numShopId.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numShopId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numShopId.Name = "numShopId";
            this.numShopId.Size = new System.Drawing.Size(59, 20);
            this.numShopId.TabIndex = 40;
            this.numShopId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnBuyFastByID
            // 
            this.btnBuyFastByID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBuyFastByID.BackColorUseGeneric = false;
            this.btnBuyFastByID.Checked = false;
            this.btnBuyFastByID.Location = new System.Drawing.Point(88, 129);
            this.btnBuyFastByID.Name = "btnBuyFastByID";
            this.btnBuyFastByID.Size = new System.Drawing.Size(89, 22);
            this.btnBuyFastByID.TabIndex = 164;
            this.btnBuyFastByID.Text = "Buy fast";
            this.btnBuyFastByID.Click += new System.EventHandler(this.btnBuyFastByID_Click);
            // 
            // btnLoadShop
            // 
            this.btnLoadShop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoadShop.BackColorUseGeneric = false;
            this.btnLoadShop.Checked = false;
            this.btnLoadShop.Location = new System.Drawing.Point(66, 16);
            this.btnLoadShop.Name = "btnLoadShop";
            this.btnLoadShop.Size = new System.Drawing.Size(111, 20);
            this.btnLoadShop.TabIndex = 134;
            this.btnLoadShop.Text = "Load shop (cmd)";
            this.btnLoadShop.Click += new System.EventHandler(this.btnLoadShop_Click);
            // 
            // txtShopItem
            // 
            this.txtShopItem.Location = new System.Drawing.Point(6, 37);
            this.txtShopItem.Name = "txtShopItem";
            this.txtShopItem.Size = new System.Drawing.Size(171, 20);
            this.txtShopItem.TabIndex = 41;
            this.txtShopItem.Text = "Shop Item";
            this.txtShopItem.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtShopItem.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnBuy
            // 
            this.btnBuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBuy.BackColorUseGeneric = false;
            this.btnBuy.Checked = false;
            this.btnBuy.Location = new System.Drawing.Point(6, 58);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(81, 22);
            this.btnBuy.TabIndex = 38;
            this.btnBuy.Text = "Buy item";
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // btnBuyFast
            // 
            this.btnBuyFast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBuyFast.BackColorUseGeneric = false;
            this.btnBuyFast.Checked = false;
            this.btnBuyFast.Location = new System.Drawing.Point(88, 58);
            this.btnBuyFast.Name = "btnBuyFast";
            this.btnBuyFast.Size = new System.Drawing.Size(89, 22);
            this.btnBuyFast.TabIndex = 133;
            this.btnBuyFast.Text = "Buy fast";
            this.btnBuyFast.Click += new System.EventHandler(this.btnBuyFast_Click);
            // 
            // darkGroupBox2
            // 
            this.darkGroupBox2.Controls.Add(this.btnWhitelistClear);
            this.darkGroupBox2.Controls.Add(this.btnWhitelistOn);
            this.darkGroupBox2.Controls.Add(this.btnWhitelistOff);
            this.darkGroupBox2.Location = new System.Drawing.Point(193, 141);
            this.darkGroupBox2.Name = "darkGroupBox2";
            this.darkGroupBox2.Size = new System.Drawing.Size(86, 74);
            this.darkGroupBox2.TabIndex = 158;
            this.darkGroupBox2.TabStop = false;
            this.darkGroupBox2.Text = "Whitelist";
            // 
            // btnWhitelistClear
            // 
            this.btnWhitelistClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWhitelistClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnWhitelistClear.BackColorUseGeneric = false;
            this.btnWhitelistClear.Checked = false;
            this.btnWhitelistClear.Location = new System.Drawing.Point(11, 41);
            this.btnWhitelistClear.Name = "btnWhitelistClear";
            this.btnWhitelistClear.Size = new System.Drawing.Size(64, 22);
            this.btnWhitelistClear.TabIndex = 154;
            this.btnWhitelistClear.Text = "Clear";
            this.btnWhitelistClear.Click += new System.EventHandler(this.btnWhitelistClear_Click);
            // 
            // btnWhitelistOn
            // 
            this.btnWhitelistOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWhitelistOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnWhitelistOn.BackColorUseGeneric = false;
            this.btnWhitelistOn.Checked = false;
            this.btnWhitelistOn.Location = new System.Drawing.Point(11, 18);
            this.btnWhitelistOn.Name = "btnWhitelistOn";
            this.btnWhitelistOn.Size = new System.Drawing.Size(31, 22);
            this.btnWhitelistOn.TabIndex = 155;
            this.btnWhitelistOn.Text = "On";
            this.btnWhitelistOn.Click += new System.EventHandler(this.btnWhitelistOn_Click);
            // 
            // btnWhitelistOff
            // 
            this.btnWhitelistOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWhitelistOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnWhitelistOff.BackColorUseGeneric = false;
            this.btnWhitelistOff.Checked = false;
            this.btnWhitelistOff.Location = new System.Drawing.Point(43, 18);
            this.btnWhitelistOff.Name = "btnWhitelistOff";
            this.btnWhitelistOff.Size = new System.Drawing.Size(32, 22);
            this.btnWhitelistOff.TabIndex = 156;
            this.btnWhitelistOff.Text = "Off";
            this.btnWhitelistOff.Click += new System.EventHandler(this.btnWhitelistOff_Click);
            // 
            // btnBoost
            // 
            this.btnBoost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBoost.BackColorUseGeneric = false;
            this.btnBoost.Checked = false;
            this.btnBoost.Location = new System.Drawing.Point(149, 113);
            this.btnBoost.Name = "btnBoost";
            this.btnBoost.Size = new System.Drawing.Size(130, 22);
            this.btnBoost.TabIndex = 37;
            this.btnBoost.Text = "Add boost";
            this.btnBoost.Click += new System.EventHandler(this.btnBoost_Click);
            // 
            // cbBoosts
            // 
            this.cbBoosts.FormattingEnabled = true;
            this.cbBoosts.Location = new System.Drawing.Point(149, 93);
            this.cbBoosts.Name = "cbBoosts";
            this.cbBoosts.Size = new System.Drawing.Size(130, 21);
            this.cbBoosts.TabIndex = 36;
            this.cbBoosts.Click += new System.EventHandler(this.cbBoosts_Click);
            // 
            // btnSwap
            // 
            this.btnSwap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSwap.BackColorUseGeneric = false;
            this.btnSwap.Checked = false;
            this.btnSwap.Location = new System.Drawing.Point(6, 109);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(137, 26);
            this.btnSwap.TabIndex = 28;
            this.btnSwap.Text = "Bank swap";
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // txtSwapInv
            // 
            this.txtSwapInv.Location = new System.Drawing.Point(6, 90);
            this.txtSwapInv.Name = "txtSwapInv";
            this.txtSwapInv.Size = new System.Drawing.Size(137, 20);
            this.txtSwapInv.TabIndex = 27;
            this.txtSwapInv.Text = "Inventory item name";
            this.txtSwapInv.Click += new System.EventHandler(this.TextboxEnter);
            this.txtSwapInv.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtSwapBank
            // 
            this.txtSwapBank.Location = new System.Drawing.Point(6, 71);
            this.txtSwapBank.Name = "txtSwapBank";
            this.txtSwapBank.Size = new System.Drawing.Size(137, 20);
            this.txtSwapBank.TabIndex = 26;
            this.txtSwapBank.Text = "Bank item name";
            this.txtSwapBank.Click += new System.EventHandler(this.TextboxEnter);
            this.txtSwapBank.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnWhitelist
            // 
            this.btnWhitelist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnWhitelist.BackColorUseGeneric = false;
            this.btnWhitelist.Checked = false;
            this.btnWhitelist.Location = new System.Drawing.Point(149, 44);
            this.btnWhitelist.Name = "btnWhitelist";
            this.btnWhitelist.Size = new System.Drawing.Size(130, 22);
            this.btnWhitelist.TabIndex = 23;
            this.btnWhitelist.Text = "Add to whitelist";
            this.btnWhitelist.Click += new System.EventHandler(this.btnWhitelist_Click);
            // 
            // btnBoth
            // 
            this.btnBoth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBoth.BackColorUseGeneric = false;
            this.btnBoth.Checked = false;
            this.btnBoth.Location = new System.Drawing.Point(149, 23);
            this.btnBoth.Name = "btnBoth";
            this.btnBoth.Size = new System.Drawing.Size(130, 22);
            this.btnBoth.TabIndex = 23;
            this.btnBoth.Text = "Add to both";
            this.btnBoth.Click += new System.EventHandler(this.btnBoth_Click);
            // 
            // txtWhitelist
            // 
            this.txtWhitelist.Location = new System.Drawing.Point(149, 4);
            this.txtWhitelist.Name = "txtWhitelist";
            this.txtWhitelist.Size = new System.Drawing.Size(130, 20);
            this.txtWhitelist.TabIndex = 22;
            this.txtWhitelist.Text = "Item name";
            this.txtWhitelist.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtWhitelist.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnItem
            // 
            this.btnItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnItem.BackColorUseGeneric = false;
            this.btnItem.Checked = false;
            this.btnItem.Location = new System.Drawing.Point(6, 43);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(137, 22);
            this.btnItem.TabIndex = 21;
            this.btnItem.Text = "Add command";
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // btnUnbanklst
            // 
            this.btnUnbanklst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnUnbanklst.BackColorUseGeneric = false;
            this.btnUnbanklst.Checked = false;
            this.btnUnbanklst.Location = new System.Drawing.Point(149, 65);
            this.btnUnbanklst.Name = "btnUnbanklst";
            this.btnUnbanklst.Size = new System.Drawing.Size(130, 22);
            this.btnUnbanklst.TabIndex = 147;
            this.btnUnbanklst.Text = "Add to Unbank list";
            this.btnUnbanklst.Click += new System.EventHandler(this.btnUnbanklst_Click);
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(6, 24);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(137, 20);
            this.txtItem.TabIndex = 20;
            this.txtItem.Text = "Item name";
            this.txtItem.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtItem.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // cbItemCmds
            // 
            this.cbItemCmds.FormattingEnabled = true;
            this.cbItemCmds.Items.AddRange(new object[] {
            "Get drop",
            "Sell",
            "Safe Equip",
            "Equip",
            "To bank from inv",
            "To inv from bank",
            "Equip Set",
            "Add to Whitelist",
            "Remove from Whitelist"});
            this.cbItemCmds.Location = new System.Drawing.Point(6, 4);
            this.cbItemCmds.Name = "cbItemCmds";
            this.cbItemCmds.Size = new System.Drawing.Size(137, 21);
            this.cbItemCmds.TabIndex = 19;
            // 
            // tabMap
            // 
            this.tabMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabMap.Controls.Add(this.darkGroupBox18);
            this.tabMap.Controls.Add(this.darkGroupBox1);
            this.tabMap.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabMap.Location = new System.Drawing.Point(4, 20);
            this.tabMap.Margin = new System.Windows.Forms.Padding(0);
            this.tabMap.Name = "tabMap";
            this.tabMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabMap.Size = new System.Drawing.Size(514, 320);
            this.tabMap.TabIndex = 2;
            this.tabMap.Text = "Map";
            // 
            // darkGroupBox18
            // 
            this.darkGroupBox18.Controls.Add(this.darkLabel5);
            this.darkGroupBox18.Controls.Add(this.numWalkX);
            this.darkGroupBox18.Controls.Add(this.darkLabel6);
            this.darkGroupBox18.Controls.Add(this.numWalkY);
            this.darkGroupBox18.Controls.Add(this.btnWalk);
            this.darkGroupBox18.Controls.Add(this.btnSetSpawn);
            this.darkGroupBox18.Controls.Add(this.btnWalkCur);
            this.darkGroupBox18.Controls.Add(this.btnWalkRdm);
            this.darkGroupBox18.Location = new System.Drawing.Point(6, 3);
            this.darkGroupBox18.Name = "darkGroupBox18";
            this.darkGroupBox18.Size = new System.Drawing.Size(131, 141);
            this.darkGroupBox18.TabIndex = 156;
            this.darkGroupBox18.TabStop = false;
            this.darkGroupBox18.Text = "Position";
            // 
            // darkLabel5
            // 
            this.darkLabel5.AutoSize = true;
            this.darkLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel5.Location = new System.Drawing.Point(6, 16);
            this.darkLabel5.Name = "darkLabel5";
            this.darkLabel5.Size = new System.Drawing.Size(48, 13);
            this.darkLabel5.TabIndex = 153;
            this.darkLabel5.Text = "Coord. X";
            // 
            // numWalkX
            // 
            this.numWalkX.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numWalkX.Location = new System.Drawing.Point(9, 32);
            this.numWalkX.LoopValues = false;
            this.numWalkX.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numWalkX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkX.Name = "numWalkX";
            this.numWalkX.Size = new System.Drawing.Size(56, 20);
            this.numWalkX.TabIndex = 35;
            this.numWalkX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // darkLabel6
            // 
            this.darkLabel6.AutoSize = true;
            this.darkLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel6.Location = new System.Drawing.Point(63, 16);
            this.darkLabel6.Name = "darkLabel6";
            this.darkLabel6.Size = new System.Drawing.Size(48, 13);
            this.darkLabel6.TabIndex = 154;
            this.darkLabel6.Text = "Coord. Y";
            // 
            // numWalkY
            // 
            this.numWalkY.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numWalkY.Location = new System.Drawing.Point(66, 32);
            this.numWalkY.LoopValues = false;
            this.numWalkY.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numWalkY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkY.Name = "numWalkY";
            this.numWalkY.Size = new System.Drawing.Size(57, 20);
            this.numWalkY.TabIndex = 36;
            this.numWalkY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnWalk
            // 
            this.btnWalk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnWalk.BackColorUseGeneric = false;
            this.btnWalk.Checked = false;
            this.btnWalk.Location = new System.Drawing.Point(9, 72);
            this.btnWalk.Name = "btnWalk";
            this.btnWalk.Size = new System.Drawing.Size(114, 22);
            this.btnWalk.TabIndex = 37;
            this.btnWalk.Text = "Walk to";
            this.btnWalk.Click += new System.EventHandler(this.btnWalk_Click);
            // 
            // btnSetSpawn
            // 
            this.btnSetSpawn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetSpawn.BackColorUseGeneric = false;
            this.btnSetSpawn.Checked = false;
            this.btnSetSpawn.Location = new System.Drawing.Point(9, 114);
            this.btnSetSpawn.MaximumSize = new System.Drawing.Size(114, 22);
            this.btnSetSpawn.MinimumSize = new System.Drawing.Size(114, 22);
            this.btnSetSpawn.Name = "btnSetSpawn";
            this.btnSetSpawn.Size = new System.Drawing.Size(114, 22);
            this.btnSetSpawn.TabIndex = 142;
            this.btnSetSpawn.Text = "Set spawnpoint";
            this.btnSetSpawn.Click += new System.EventHandler(this.btnSetSpawn_Click);
            // 
            // btnWalkCur
            // 
            this.btnWalkCur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnWalkCur.BackColorUseGeneric = false;
            this.btnWalkCur.Checked = false;
            this.btnWalkCur.Location = new System.Drawing.Point(9, 51);
            this.btnWalkCur.MaximumSize = new System.Drawing.Size(114, 22);
            this.btnWalkCur.MinimumSize = new System.Drawing.Size(114, 22);
            this.btnWalkCur.Name = "btnWalkCur";
            this.btnWalkCur.Size = new System.Drawing.Size(114, 22);
            this.btnWalkCur.TabIndex = 38;
            this.btnWalkCur.Text = "Current position";
            this.btnWalkCur.Click += new System.EventHandler(this.btnWalkCur_Click);
            // 
            // btnWalkRdm
            // 
            this.btnWalkRdm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnWalkRdm.BackColorUseGeneric = false;
            this.btnWalkRdm.Checked = false;
            this.btnWalkRdm.Location = new System.Drawing.Point(9, 93);
            this.btnWalkRdm.MaximumSize = new System.Drawing.Size(114, 22);
            this.btnWalkRdm.MinimumSize = new System.Drawing.Size(114, 22);
            this.btnWalkRdm.Name = "btnWalkRdm";
            this.btnWalkRdm.Size = new System.Drawing.Size(114, 22);
            this.btnWalkRdm.TabIndex = 142;
            this.btnWalkRdm.Text = "Walk randomly";
            this.btnWalkRdm.Click += new System.EventHandler(this.btnWalkRdm_Click);
            // 
            // darkGroupBox1
            // 
            this.darkGroupBox1.Controls.Add(this.txtJoin);
            this.darkGroupBox1.Controls.Add(this.txtJoinCell);
            this.darkGroupBox1.Controls.Add(this.txtJoinPad);
            this.darkGroupBox1.Controls.Add(this.btnCurrBlank);
            this.darkGroupBox1.Controls.Add(this.btnJoin);
            this.darkGroupBox1.Controls.Add(this.txtCell);
            this.darkGroupBox1.Controls.Add(this.txtPad);
            this.darkGroupBox1.Controls.Add(this.btnCurrCell);
            this.darkGroupBox1.Controls.Add(this.btnJump);
            this.darkGroupBox1.Controls.Add(this.btnCellSwap);
            this.darkGroupBox1.Controls.Add(this.button2);
            this.darkGroupBox1.Location = new System.Drawing.Point(143, 3);
            this.darkGroupBox1.Name = "darkGroupBox1";
            this.darkGroupBox1.Size = new System.Drawing.Size(289, 85);
            this.darkGroupBox1.TabIndex = 155;
            this.darkGroupBox1.TabStop = false;
            this.darkGroupBox1.Text = "Map";
            // 
            // txtJoin
            // 
            this.txtJoin.Location = new System.Drawing.Point(6, 19);
            this.txtJoin.Name = "txtJoin";
            this.txtJoin.Size = new System.Drawing.Size(112, 20);
            this.txtJoin.TabIndex = 26;
            this.txtJoin.Text = "battleon-1e99";
            this.txtJoin.Click += new System.EventHandler(this.TextboxEnter);
            this.txtJoin.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtJoinCell
            // 
            this.txtJoinCell.Location = new System.Drawing.Point(6, 38);
            this.txtJoinCell.Name = "txtJoinCell";
            this.txtJoinCell.Size = new System.Drawing.Size(56, 20);
            this.txtJoinCell.TabIndex = 27;
            this.txtJoinCell.Text = "Enter";
            this.txtJoinCell.Click += new System.EventHandler(this.TextboxEnter);
            this.txtJoinCell.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtJoinPad
            // 
            this.txtJoinPad.Location = new System.Drawing.Point(61, 38);
            this.txtJoinPad.Name = "txtJoinPad";
            this.txtJoinPad.Size = new System.Drawing.Size(57, 20);
            this.txtJoinPad.TabIndex = 28;
            this.txtJoinPad.Text = "Spawn";
            this.txtJoinPad.Click += new System.EventHandler(this.TextboxEnter);
            this.txtJoinPad.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnCurrBlank
            // 
            this.btnCurrBlank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCurrBlank.BackColorUseGeneric = false;
            this.btnCurrBlank.Checked = false;
            this.btnCurrBlank.Location = new System.Drawing.Point(124, 37);
            this.btnCurrBlank.Name = "btnCurrBlank";
            this.btnCurrBlank.Size = new System.Drawing.Size(43, 21);
            this.btnCurrBlank.TabIndex = 143;
            this.btnCurrBlank.Text = "Blank";
            this.btnCurrBlank.Click += new System.EventHandler(this.btnCurrBlank_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnJoin.BackColorUseGeneric = false;
            this.btnJoin.Checked = false;
            this.btnJoin.Location = new System.Drawing.Point(6, 57);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(112, 22);
            this.btnJoin.TabIndex = 29;
            this.btnJoin.Text = "Join map";
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // txtCell
            // 
            this.txtCell.Location = new System.Drawing.Point(173, 19);
            this.txtCell.Name = "txtCell";
            this.txtCell.Size = new System.Drawing.Size(54, 20);
            this.txtCell.TabIndex = 30;
            this.txtCell.Text = "Cell";
            this.txtCell.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtCell.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtPad
            // 
            this.txtPad.Location = new System.Drawing.Point(226, 19);
            this.txtPad.Name = "txtPad";
            this.txtPad.Size = new System.Drawing.Size(57, 20);
            this.txtPad.TabIndex = 31;
            this.txtPad.Text = "Pad";
            this.txtPad.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtPad.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnCurrCell
            // 
            this.btnCurrCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCurrCell.BackColorUseGeneric = false;
            this.btnCurrCell.Checked = false;
            this.btnCurrCell.Location = new System.Drawing.Point(173, 38);
            this.btnCurrCell.Name = "btnCurrCell";
            this.btnCurrCell.Size = new System.Drawing.Size(110, 20);
            this.btnCurrCell.TabIndex = 32;
            this.btnCurrCell.Text = "Get current cell";
            this.btnCurrCell.Click += new System.EventHandler(this.btnCurrCell_Click);
            // 
            // btnJump
            // 
            this.btnJump.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnJump.BackColorUseGeneric = false;
            this.btnJump.Checked = false;
            this.btnJump.Location = new System.Drawing.Point(173, 57);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(110, 22);
            this.btnJump.TabIndex = 33;
            this.btnJump.Text = "Jump";
            this.btnJump.Click += new System.EventHandler(this.btnJump_Click);
            // 
            // btnCellSwap
            // 
            this.btnCellSwap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCellSwap.BackColorUseGeneric = false;
            this.btnCellSwap.Checked = false;
            this.btnCellSwap.Location = new System.Drawing.Point(124, 57);
            this.btnCellSwap.Name = "btnCellSwap";
            this.btnCellSwap.Size = new System.Drawing.Size(43, 22);
            this.btnCellSwap.TabIndex = 34;
            this.btnCellSwap.Text = "<";
            this.btnCellSwap.Click += new System.EventHandler(this.btnCellSwap_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.button2.BackColorUseGeneric = false;
            this.button2.Checked = false;
            this.button2.Location = new System.Drawing.Point(124, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 19);
            this.button2.TabIndex = 34;
            this.button2.Text = ">";
            this.button2.Click += new System.EventHandler(this.btnCellSwap_Click);
            // 
            // tabQuest
            // 
            this.tabQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabQuest.Controls.Add(this.darkGroupBox20);
            this.tabQuest.Controls.Add(this.darkGroupBox21);
            this.tabQuest.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabQuest.Location = new System.Drawing.Point(4, 20);
            this.tabQuest.Margin = new System.Windows.Forms.Padding(0);
            this.tabQuest.Name = "tabQuest";
            this.tabQuest.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuest.Size = new System.Drawing.Size(514, 320);
            this.tabQuest.TabIndex = 3;
            this.tabQuest.Text = "Quest";
            // 
            // darkGroupBox20
            // 
            this.darkGroupBox20.Controls.Add(this.btnQuestlistClearCmd);
            this.darkGroupBox20.Controls.Add(this.txtQuestItemID);
            this.darkGroupBox20.Controls.Add(this.txtQuestID);
            this.darkGroupBox20.Controls.Add(this.darkLabel7);
            this.darkGroupBox20.Controls.Add(this.chkQuestlistItemID);
            this.darkGroupBox20.Controls.Add(this.btnQuestlistRemoveCmd);
            this.darkGroupBox20.Controls.Add(this.btnQuestlistAddCmd);
            this.darkGroupBox20.Location = new System.Drawing.Point(159, 6);
            this.darkGroupBox20.Name = "darkGroupBox20";
            this.darkGroupBox20.Size = new System.Drawing.Size(177, 124);
            this.darkGroupBox20.TabIndex = 161;
            this.darkGroupBox20.TabStop = false;
            this.darkGroupBox20.Text = "Quest List";
            // 
            // btnQuestlistClearCmd
            // 
            this.btnQuestlistClearCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnQuestlistClearCmd.BackColorUseGeneric = false;
            this.btnQuestlistClearCmd.Checked = false;
            this.btnQuestlistClearCmd.Location = new System.Drawing.Point(9, 94);
            this.btnQuestlistClearCmd.Name = "btnQuestlistClearCmd";
            this.btnQuestlistClearCmd.Size = new System.Drawing.Size(162, 22);
            this.btnQuestlistClearCmd.TabIndex = 170;
            this.btnQuestlistClearCmd.Text = "Clear quest list (cmd)";
            this.btnQuestlistClearCmd.Click += new System.EventHandler(this.btnQuestlistClearCmd_Click);
            // 
            // txtQuestItemID
            // 
            this.txtQuestItemID.Enabled = false;
            this.txtQuestItemID.Location = new System.Drawing.Point(86, 33);
            this.txtQuestItemID.Name = "txtQuestItemID";
            this.txtQuestItemID.Size = new System.Drawing.Size(85, 20);
            this.txtQuestItemID.TabIndex = 169;
            this.txtQuestItemID.Text = "1";
            // 
            // txtQuestID
            // 
            this.txtQuestID.Location = new System.Drawing.Point(9, 33);
            this.txtQuestID.Name = "txtQuestID";
            this.txtQuestID.Size = new System.Drawing.Size(78, 20);
            this.txtQuestID.TabIndex = 162;
            this.txtQuestID.Text = "1";
            // 
            // darkLabel7
            // 
            this.darkLabel7.AutoSize = true;
            this.darkLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel7.Location = new System.Drawing.Point(6, 17);
            this.darkLabel7.Name = "darkLabel7";
            this.darkLabel7.Size = new System.Drawing.Size(52, 13);
            this.darkLabel7.TabIndex = 167;
            this.darkLabel7.Text = "Quest ID:";
            // 
            // chkQuestlistItemID
            // 
            this.chkQuestlistItemID.AutoSize = true;
            this.chkQuestlistItemID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.chkQuestlistItemID.Location = new System.Drawing.Point(86, 16);
            this.chkQuestlistItemID.Name = "chkQuestlistItemID";
            this.chkQuestlistItemID.Size = new System.Drawing.Size(62, 17);
            this.chkQuestlistItemID.TabIndex = 168;
            this.chkQuestlistItemID.Text = "Item ID:";
            this.chkQuestlistItemID.Click += new System.EventHandler(this.txtQuestItemID_CheckedChanged);
            // 
            // btnQuestlistRemoveCmd
            // 
            this.btnQuestlistRemoveCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnQuestlistRemoveCmd.BackColorUseGeneric = false;
            this.btnQuestlistRemoveCmd.Checked = false;
            this.btnQuestlistRemoveCmd.Location = new System.Drawing.Point(9, 73);
            this.btnQuestlistRemoveCmd.Name = "btnQuestlistRemoveCmd";
            this.btnQuestlistRemoveCmd.Size = new System.Drawing.Size(162, 22);
            this.btnQuestlistRemoveCmd.TabIndex = 166;
            this.btnQuestlistRemoveCmd.Text = "Remove from quest list (cmd)";
            this.btnQuestlistRemoveCmd.Click += new System.EventHandler(this.btnQuestlistRemoveCmd_Click);
            // 
            // btnQuestlistAddCmd
            // 
            this.btnQuestlistAddCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnQuestlistAddCmd.BackColorUseGeneric = false;
            this.btnQuestlistAddCmd.Checked = false;
            this.btnQuestlistAddCmd.Location = new System.Drawing.Point(9, 52);
            this.btnQuestlistAddCmd.Name = "btnQuestlistAddCmd";
            this.btnQuestlistAddCmd.Size = new System.Drawing.Size(162, 22);
            this.btnQuestlistAddCmd.TabIndex = 165;
            this.btnQuestlistAddCmd.Text = "Add to quest list (cmd)";
            this.btnQuestlistAddCmd.Click += new System.EventHandler(this.btnQuestlistAddCmd_Click);
            // 
            // darkGroupBox21
            // 
            this.darkGroupBox21.Controls.Add(this.numQuestItem);
            this.darkGroupBox21.Controls.Add(this.numQuestID);
            this.darkGroupBox21.Controls.Add(this.label4);
            this.darkGroupBox21.Controls.Add(this.btnQuestAccept);
            this.darkGroupBox21.Controls.Add(this.chkQuestItem);
            this.darkGroupBox21.Controls.Add(this.btnQuestComplete);
            this.darkGroupBox21.Controls.Add(this.btnQuestAdd);
            this.darkGroupBox21.Location = new System.Drawing.Point(6, 6);
            this.darkGroupBox21.Name = "darkGroupBox21";
            this.darkGroupBox21.Size = new System.Drawing.Size(147, 124);
            this.darkGroupBox21.TabIndex = 160;
            this.darkGroupBox21.TabStop = false;
            this.darkGroupBox21.Text = "Quest";
            // 
            // numQuestItem
            // 
            this.numQuestItem.Enabled = false;
            this.numQuestItem.Location = new System.Drawing.Point(71, 33);
            this.numQuestItem.Name = "numQuestItem";
            this.numQuestItem.Size = new System.Drawing.Size(67, 20);
            this.numQuestItem.TabIndex = 163;
            this.numQuestItem.Text = "1";
            // 
            // numQuestID
            // 
            this.numQuestID.Location = new System.Drawing.Point(9, 33);
            this.numQuestID.Name = "numQuestID";
            this.numQuestID.Size = new System.Drawing.Size(63, 20);
            this.numQuestID.TabIndex = 163;
            this.numQuestID.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Quest ID:";
            // 
            // btnQuestAccept
            // 
            this.btnQuestAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnQuestAccept.BackColorUseGeneric = false;
            this.btnQuestAccept.Checked = false;
            this.btnQuestAccept.Location = new System.Drawing.Point(9, 73);
            this.btnQuestAccept.Name = "btnQuestAccept";
            this.btnQuestAccept.Size = new System.Drawing.Size(129, 22);
            this.btnQuestAccept.TabIndex = 13;
            this.btnQuestAccept.Text = "Accept command";
            this.btnQuestAccept.Click += new System.EventHandler(this.btnQuestAccept_Click);
            // 
            // chkQuestItem
            // 
            this.chkQuestItem.AutoSize = true;
            this.chkQuestItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.chkQuestItem.Location = new System.Drawing.Point(71, 16);
            this.chkQuestItem.Name = "chkQuestItem";
            this.chkQuestItem.Size = new System.Drawing.Size(62, 17);
            this.chkQuestItem.TabIndex = 9;
            this.chkQuestItem.Text = "Item ID:";
            this.chkQuestItem.CheckedChanged += new System.EventHandler(this.chkQuestItem_CheckedChanged);
            // 
            // btnQuestComplete
            // 
            this.btnQuestComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnQuestComplete.BackColorUseGeneric = false;
            this.btnQuestComplete.Checked = false;
            this.btnQuestComplete.Location = new System.Drawing.Point(9, 94);
            this.btnQuestComplete.Name = "btnQuestComplete";
            this.btnQuestComplete.Size = new System.Drawing.Size(129, 22);
            this.btnQuestComplete.TabIndex = 12;
            this.btnQuestComplete.Text = "Complete command";
            this.btnQuestComplete.Click += new System.EventHandler(this.btnQuestComplete_Click);
            // 
            // btnQuestAdd
            // 
            this.btnQuestAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnQuestAdd.BackColorUseGeneric = false;
            this.btnQuestAdd.Checked = false;
            this.btnQuestAdd.Location = new System.Drawing.Point(9, 52);
            this.btnQuestAdd.Name = "btnQuestAdd";
            this.btnQuestAdd.Size = new System.Drawing.Size(129, 22);
            this.btnQuestAdd.TabIndex = 11;
            this.btnQuestAdd.Text = "Add to quest list";
            this.btnQuestAdd.Click += new System.EventHandler(this.btnQuestAdd_Click);
            // 
            // tabMisc
            // 
            this.tabMisc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabMisc.Controls.Add(this.darkGroupBox24);
            this.tabMisc.Controls.Add(this.darkGroupBox23);
            this.tabMisc.Controls.Add(this.darkGroupBox22);
            this.tabMisc.Controls.Add(this.darkGroupBox11);
            this.tabMisc.Controls.Add(this.darkGroupBox14);
            this.tabMisc.Controls.Add(this.darkGroupBox12);
            this.tabMisc.Controls.Add(this.darkGroupBox8);
            this.tabMisc.Controls.Add(this.darkGroupBox7);
            this.tabMisc.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabMisc.Location = new System.Drawing.Point(4, 20);
            this.tabMisc.Margin = new System.Windows.Forms.Padding(0);
            this.tabMisc.Name = "tabMisc";
            this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
            this.tabMisc.Size = new System.Drawing.Size(514, 320);
            this.tabMisc.TabIndex = 4;
            this.tabMisc.Text = "Misc";
            // 
            // darkGroupBox24
            // 
            this.darkGroupBox24.Controls.Add(this.darkLabel9);
            this.darkGroupBox24.Controls.Add(this.darkLabel8);
            this.darkGroupBox24.Controls.Add(this.numPacketDelay);
            this.darkGroupBox24.Controls.Add(this.btnPacketSpamOffCmd);
            this.darkGroupBox24.Controls.Add(this.btnPacketSpamOnCmd);
            this.darkGroupBox24.Controls.Add(this.darkLabel4);
            this.darkGroupBox24.Controls.Add(this.txtPacket);
            this.darkGroupBox24.Controls.Add(this.btnServerPacket);
            this.darkGroupBox24.Controls.Add(this.btnClientPacket);
            this.darkGroupBox24.Location = new System.Drawing.Point(178, 243);
            this.darkGroupBox24.Name = "darkGroupBox24";
            this.darkGroupBox24.Size = new System.Drawing.Size(324, 71);
            this.darkGroupBox24.TabIndex = 171;
            this.darkGroupBox24.TabStop = false;
            this.darkGroupBox24.Text = "Packet Command";
            // 
            // darkLabel9
            // 
            this.darkLabel9.AutoSize = true;
            this.darkLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel9.Location = new System.Drawing.Point(228, 46);
            this.darkLabel9.Name = "darkLabel9";
            this.darkLabel9.Size = new System.Drawing.Size(37, 13);
            this.darkLabel9.TabIndex = 157;
            this.darkLabel9.Text = "Delay:";
            // 
            // darkLabel8
            // 
            this.darkLabel8.AutoSize = true;
            this.darkLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel8.Location = new System.Drawing.Point(3, 46);
            this.darkLabel8.Name = "darkLabel8";
            this.darkLabel8.Size = new System.Drawing.Size(23, 13);
            this.darkLabel8.TabIndex = 156;
            this.darkLabel8.Text = "To:";
            // 
            // numPacketDelay
            // 
            this.numPacketDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numPacketDelay.Location = new System.Drawing.Point(266, 42);
            this.numPacketDelay.LoopValues = false;
            this.numPacketDelay.Maximum = new decimal(new int[] {
            71000,
            0,
            0,
            0});
            this.numPacketDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numPacketDelay.Name = "numPacketDelay";
            this.numPacketDelay.Size = new System.Drawing.Size(52, 20);
            this.numPacketDelay.TabIndex = 155;
            this.numPacketDelay.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // btnPacketSpamOffCmd
            // 
            this.btnPacketSpamOffCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnPacketSpamOffCmd.BackColorUseGeneric = false;
            this.btnPacketSpamOffCmd.Checked = false;
            this.btnPacketSpamOffCmd.Location = new System.Drawing.Point(192, 42);
            this.btnPacketSpamOffCmd.Name = "btnPacketSpamOffCmd";
            this.btnPacketSpamOffCmd.Size = new System.Drawing.Size(30, 20);
            this.btnPacketSpamOffCmd.TabIndex = 154;
            this.btnPacketSpamOffCmd.Text = "Off";
            this.btnPacketSpamOffCmd.Click += new System.EventHandler(this.btnPacketSpamOff_Click);
            // 
            // btnPacketSpamOnCmd
            // 
            this.btnPacketSpamOnCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnPacketSpamOnCmd.BackColorUseGeneric = false;
            this.btnPacketSpamOnCmd.Checked = false;
            this.btnPacketSpamOnCmd.Location = new System.Drawing.Point(163, 42);
            this.btnPacketSpamOnCmd.Name = "btnPacketSpamOnCmd";
            this.btnPacketSpamOnCmd.Size = new System.Drawing.Size(30, 20);
            this.btnPacketSpamOnCmd.TabIndex = 153;
            this.btnPacketSpamOnCmd.Text = "On";
            this.btnPacketSpamOnCmd.Click += new System.EventHandler(this.btnPacketSpamOn_Click);
            // 
            // darkLabel4
            // 
            this.darkLabel4.AutoSize = true;
            this.darkLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel4.Location = new System.Drawing.Point(124, 46);
            this.darkLabel4.Name = "darkLabel4";
            this.darkLabel4.Size = new System.Drawing.Size(37, 13);
            this.darkLabel4.TabIndex = 152;
            this.darkLabel4.Text = "Spam:";
            // 
            // txtPacket
            // 
            this.txtPacket.Location = new System.Drawing.Point(6, 16);
            this.txtPacket.Name = "txtPacket";
            this.txtPacket.Size = new System.Drawing.Size(312, 20);
            this.txtPacket.TabIndex = 53;
            this.txtPacket.Text = "%xt%zm%cmd%1%tfer%PLAYERNAME%MAP-1e99%";
            // 
            // btnServerPacket
            // 
            this.btnServerPacket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnServerPacket.BackColorUseGeneric = false;
            this.btnServerPacket.Checked = false;
            this.btnServerPacket.Location = new System.Drawing.Point(28, 42);
            this.btnServerPacket.Name = "btnServerPacket";
            this.btnServerPacket.Size = new System.Drawing.Size(48, 20);
            this.btnServerPacket.TabIndex = 52;
            this.btnServerPacket.Text = "Server";
            this.btnServerPacket.Click += new System.EventHandler(this.btnPacket_Click);
            // 
            // btnClientPacket
            // 
            this.btnClientPacket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnClientPacket.BackColorUseGeneric = false;
            this.btnClientPacket.Checked = false;
            this.btnClientPacket.Location = new System.Drawing.Point(75, 42);
            this.btnClientPacket.Name = "btnClientPacket";
            this.btnClientPacket.Size = new System.Drawing.Size(43, 20);
            this.btnClientPacket.TabIndex = 52;
            this.btnClientPacket.Text = "Client";
            this.btnClientPacket.Click += new System.EventHandler(this.btnClientPacket_Click);
            // 
            // darkGroupBox23
            // 
            this.darkGroupBox23.Controls.Add(this.btnLoadCmd);
            this.darkGroupBox23.Controls.Add(this.btnLogout);
            this.darkGroupBox23.Controls.Add(this.numSetFPS);
            this.darkGroupBox23.Controls.Add(this.btnReturnCmd);
            this.darkGroupBox23.Controls.Add(this.btnBlank);
            this.darkGroupBox23.Controls.Add(this.btnSetFPSCmd);
            this.darkGroupBox23.Controls.Add(this.btnBeep);
            this.darkGroupBox23.Controls.Add(this.numDelay);
            this.darkGroupBox23.Controls.Add(this.btnDelay);
            this.darkGroupBox23.Controls.Add(this.numBeepTimes);
            this.darkGroupBox23.Location = new System.Drawing.Point(212, 3);
            this.darkGroupBox23.Name = "darkGroupBox23";
            this.darkGroupBox23.Size = new System.Drawing.Size(142, 175);
            this.darkGroupBox23.TabIndex = 170;
            this.darkGroupBox23.TabStop = false;
            this.darkGroupBox23.Text = "Other";
            // 
            // btnLoadCmd
            // 
            this.btnLoadCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoadCmd.BackColorUseGeneric = false;
            this.btnLoadCmd.Checked = false;
            this.btnLoadCmd.Location = new System.Drawing.Point(5, 17);
            this.btnLoadCmd.Name = "btnLoadCmd";
            this.btnLoadCmd.Size = new System.Drawing.Size(132, 22);
            this.btnLoadCmd.TabIndex = 63;
            this.btnLoadCmd.Text = "Load bot (cmd)";
            this.btnLoadCmd.Click += new System.EventHandler(this.btnLoadCmd_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLogout.BackColorUseGeneric = false;
            this.btnLogout.Checked = false;
            this.btnLogout.Location = new System.Drawing.Point(5, 63);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(132, 22);
            this.btnLogout.TabIndex = 114;
            this.btnLogout.Text = "Logout (cmd)";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // numSetFPS
            // 
            this.numSetFPS.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSetFPS.Location = new System.Drawing.Point(82, 130);
            this.numSetFPS.LoopValues = false;
            this.numSetFPS.Name = "numSetFPS";
            this.numSetFPS.Size = new System.Drawing.Size(55, 20);
            this.numSetFPS.TabIndex = 168;
            this.numSetFPS.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnReturnCmd
            // 
            this.btnReturnCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnReturnCmd.BackColorUseGeneric = false;
            this.btnReturnCmd.Checked = false;
            this.btnReturnCmd.Location = new System.Drawing.Point(5, 40);
            this.btnReturnCmd.Name = "btnReturnCmd";
            this.btnReturnCmd.Size = new System.Drawing.Size(132, 22);
            this.btnReturnCmd.TabIndex = 160;
            this.btnReturnCmd.Text = "Return (cmd)";
            this.btnReturnCmd.Click += new System.EventHandler(this.btnReturnCmd_Click);
            // 
            // btnBlank
            // 
            this.btnBlank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBlank.BackColorUseGeneric = false;
            this.btnBlank.Checked = false;
            this.btnBlank.Location = new System.Drawing.Point(5, 86);
            this.btnBlank.Name = "btnBlank";
            this.btnBlank.Size = new System.Drawing.Size(132, 22);
            this.btnBlank.TabIndex = 151;
            this.btnBlank.Text = "Blank (cmd)";
            this.btnBlank.Click += new System.EventHandler(this.btnBlank_Click);
            // 
            // btnSetFPSCmd
            // 
            this.btnSetFPSCmd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSetFPSCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetFPSCmd.BackColorUseGeneric = false;
            this.btnSetFPSCmd.Checked = false;
            this.btnSetFPSCmd.Location = new System.Drawing.Point(5, 130);
            this.btnSetFPSCmd.Name = "btnSetFPSCmd";
            this.btnSetFPSCmd.Size = new System.Drawing.Size(76, 20);
            this.btnSetFPSCmd.TabIndex = 167;
            this.btnSetFPSCmd.Text = "Set FPS";
            this.btnSetFPSCmd.Click += new System.EventHandler(this.btnSetFPSCmd_Click);
            // 
            // btnBeep
            // 
            this.btnBeep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBeep.BackColorUseGeneric = false;
            this.btnBeep.Checked = false;
            this.btnBeep.Location = new System.Drawing.Point(5, 151);
            this.btnBeep.Name = "btnBeep";
            this.btnBeep.Size = new System.Drawing.Size(76, 20);
            this.btnBeep.TabIndex = 74;
            this.btnBeep.Text = "Play sound";
            this.btnBeep.Click += new System.EventHandler(this.btnBeep_Click);
            // 
            // numDelay
            // 
            this.numDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numDelay.Location = new System.Drawing.Point(82, 109);
            this.numDelay.LoopValues = false;
            this.numDelay.Maximum = new decimal(new int[] {
            71000,
            0,
            0,
            0});
            this.numDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(55, 20);
            this.numDelay.TabIndex = 73;
            this.numDelay.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // btnDelay
            // 
            this.btnDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnDelay.BackColorUseGeneric = false;
            this.btnDelay.Checked = false;
            this.btnDelay.Location = new System.Drawing.Point(5, 109);
            this.btnDelay.Name = "btnDelay";
            this.btnDelay.Size = new System.Drawing.Size(76, 20);
            this.btnDelay.TabIndex = 74;
            this.btnDelay.Text = "Delay";
            this.btnDelay.Click += new System.EventHandler(this.btnDelay_Click);
            // 
            // numBeepTimes
            // 
            this.numBeepTimes.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numBeepTimes.Location = new System.Drawing.Point(82, 151);
            this.numBeepTimes.LoopValues = false;
            this.numBeepTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBeepTimes.Name = "numBeepTimes";
            this.numBeepTimes.Size = new System.Drawing.Size(55, 20);
            this.numBeepTimes.TabIndex = 73;
            this.numBeepTimes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // darkGroupBox22
            // 
            this.darkGroupBox22.Controls.Add(this.cbCategories);
            this.darkGroupBox22.Controls.Add(this.cbStatement);
            this.darkGroupBox22.Controls.Add(this.txtStatement1);
            this.darkGroupBox22.Controls.Add(this.txtStatement2);
            this.darkGroupBox22.Controls.Add(this.btnStatementAdd);
            this.darkGroupBox22.Controls.Add(this.btnClearTempVar);
            this.darkGroupBox22.Location = new System.Drawing.Point(5, 3);
            this.darkGroupBox22.Name = "darkGroupBox22";
            this.darkGroupBox22.Size = new System.Drawing.Size(202, 155);
            this.darkGroupBox22.TabIndex = 169;
            this.darkGroupBox22.TabStop = false;
            this.darkGroupBox22.Text = "Statement";
            // 
            // cbCategories
            // 
            this.cbCategories.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbCategories.FormattingEnabled = true;
            this.cbCategories.Items.AddRange(new object[] {
            "This player",
            "Player",
            "Player Aura",
            "Monster",
            "Target Aura",
            "Item",
            "Map",
            "Quest",
            "Misc"});
            this.cbCategories.Location = new System.Drawing.Point(6, 17);
            this.cbCategories.MaximumSize = new System.Drawing.Size(197, 0);
            this.cbCategories.MinimumSize = new System.Drawing.Size(167, 0);
            this.cbCategories.Name = "cbCategories";
            this.cbCategories.Size = new System.Drawing.Size(190, 21);
            this.cbCategories.TabIndex = 57;
            this.cbCategories.SelectedIndexChanged += new System.EventHandler(this.cbCategories_SelectedIndexChanged);
            // 
            // cbStatement
            // 
            this.cbStatement.DisplayMember = "Text";
            this.cbStatement.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbStatement.FormattingEnabled = true;
            this.cbStatement.Location = new System.Drawing.Point(6, 39);
            this.cbStatement.MaxDropDownItems = 15;
            this.cbStatement.MaximumSize = new System.Drawing.Size(197, 0);
            this.cbStatement.MinimumSize = new System.Drawing.Size(167, 0);
            this.cbStatement.Name = "cbStatement";
            this.cbStatement.Size = new System.Drawing.Size(190, 21);
            this.cbStatement.TabIndex = 58;
            this.cbStatement.SelectedIndexChanged += new System.EventHandler(this.cbStatement_SelectedIndexChanged);
            // 
            // txtStatement1
            // 
            this.txtStatement1.Location = new System.Drawing.Point(6, 61);
            this.txtStatement1.MaximumSize = new System.Drawing.Size(197, 20);
            this.txtStatement1.MinimumSize = new System.Drawing.Size(167, 20);
            this.txtStatement1.Name = "txtStatement1";
            this.txtStatement1.Size = new System.Drawing.Size(190, 20);
            this.txtStatement1.TabIndex = 59;
            // 
            // txtStatement2
            // 
            this.txtStatement2.Location = new System.Drawing.Point(6, 82);
            this.txtStatement2.MaximumSize = new System.Drawing.Size(197, 20);
            this.txtStatement2.MinimumSize = new System.Drawing.Size(167, 20);
            this.txtStatement2.Name = "txtStatement2";
            this.txtStatement2.Size = new System.Drawing.Size(190, 20);
            this.txtStatement2.TabIndex = 60;
            // 
            // btnStatementAdd
            // 
            this.btnStatementAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnStatementAdd.BackColorUseGeneric = false;
            this.btnStatementAdd.Checked = false;
            this.btnStatementAdd.Location = new System.Drawing.Point(6, 103);
            this.btnStatementAdd.MaximumSize = new System.Drawing.Size(197, 20);
            this.btnStatementAdd.MinimumSize = new System.Drawing.Size(167, 20);
            this.btnStatementAdd.Name = "btnStatementAdd";
            this.btnStatementAdd.Size = new System.Drawing.Size(190, 20);
            this.btnStatementAdd.TabIndex = 61;
            this.btnStatementAdd.Text = "Add statement command";
            this.btnStatementAdd.Click += new System.EventHandler(this.btnStatementAdd_Click);
            // 
            // btnClearTempVar
            // 
            this.btnClearTempVar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearTempVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnClearTempVar.BackColorUseGeneric = false;
            this.btnClearTempVar.Checked = false;
            this.btnClearTempVar.Location = new System.Drawing.Point(6, 124);
            this.btnClearTempVar.Name = "btnClearTempVar";
            this.btnClearTempVar.Size = new System.Drawing.Size(190, 22);
            this.btnClearTempVar.TabIndex = 160;
            this.btnClearTempVar.Text = "Clear Vars and Ints (cmd)";
            this.btnClearTempVar.Click += new System.EventHandler(this.btnClearTempVar_Click);
            // 
            // darkGroupBox11
            // 
            this.darkGroupBox11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.darkGroupBox11.Controls.Add(this.splitContainer4);
            this.darkGroupBox11.Controls.Add(this.txtLabel);
            this.darkGroupBox11.Controls.Add(this.lbLabels);
            this.darkGroupBox11.Controls.Add(this.darkPanel2);
            this.darkGroupBox11.Location = new System.Drawing.Point(359, 3);
            this.darkGroupBox11.Name = "darkGroupBox11";
            this.darkGroupBox11.Size = new System.Drawing.Size(143, 175);
            this.darkGroupBox11.TabIndex = 163;
            this.darkGroupBox11.TabStop = false;
            this.darkGroupBox11.Text = "Labels";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Location = new System.Drawing.Point(5, 149);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btnGotoLabel);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btnAddLabel);
            this.splitContainer4.Size = new System.Drawing.Size(133, 21);
            this.splitContainer4.SplitterDistance = 64;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 168;
            // 
            // btnGotoLabel
            // 
            this.btnGotoLabel.AutoSize = true;
            this.btnGotoLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGotoLabel.BackColorUseGeneric = false;
            this.btnGotoLabel.Checked = false;
            this.btnGotoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGotoLabel.Location = new System.Drawing.Point(0, 0);
            this.btnGotoLabel.Name = "btnGotoLabel";
            this.btnGotoLabel.Size = new System.Drawing.Size(64, 21);
            this.btnGotoLabel.TabIndex = 112;
            this.btnGotoLabel.Text = "Goto";
            this.btnGotoLabel.Click += new System.EventHandler(this.btnGotoLabel_Click);
            // 
            // btnAddLabel
            // 
            this.btnAddLabel.AutoSize = true;
            this.btnAddLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAddLabel.BackColorUseGeneric = false;
            this.btnAddLabel.Checked = false;
            this.btnAddLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddLabel.Location = new System.Drawing.Point(0, 0);
            this.btnAddLabel.Name = "btnAddLabel";
            this.btnAddLabel.Size = new System.Drawing.Size(68, 21);
            this.btnAddLabel.TabIndex = 111;
            this.btnAddLabel.Text = "Add";
            this.btnAddLabel.Click += new System.EventHandler(this.btnAddLabel_Click);
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(5, 130);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(133, 20);
            this.txtLabel.TabIndex = 113;
            this.txtLabel.Text = "Label name";
            this.txtLabel.Click += new System.EventHandler(this.TextboxEnter);
            this.txtLabel.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // lbLabels
            // 
            this.lbLabels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lbLabels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLabels.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbLabels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbLabels.FormattingEnabled = true;
            this.lbLabels.HorizontalExtent = 24;
            this.lbLabels.HorizontalScrollbar = true;
            this.lbLabels.ItemHeight = 24;
            this.lbLabels.Location = new System.Drawing.Point(5, 15);
            this.lbLabels.Name = "lbLabels";
            this.lbLabels.Size = new System.Drawing.Size(133, 114);
            this.lbLabels.TabIndex = 114;
            this.lbLabels.DoubleClick += new System.EventHandler(this.lbLabels_DoubleClick);
            // 
            // darkPanel2
            // 
            this.darkPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.darkPanel2.Location = new System.Drawing.Point(5, 15);
            this.darkPanel2.Name = "darkPanel2";
            this.darkPanel2.Size = new System.Drawing.Size(133, 114);
            this.darkPanel2.TabIndex = 165;
            // 
            // darkGroupBox14
            // 
            this.darkGroupBox14.Controls.Add(this.txtPlayer);
            this.darkGroupBox14.Controls.Add(this.btnGoto);
            this.darkGroupBox14.Location = new System.Drawing.Point(5, 230);
            this.darkGroupBox14.Name = "darkGroupBox14";
            this.darkGroupBox14.Size = new System.Drawing.Size(167, 42);
            this.darkGroupBox14.TabIndex = 166;
            this.darkGroupBox14.TabStop = false;
            this.darkGroupBox14.Text = "Player";
            // 
            // txtPlayer
            // 
            this.txtPlayer.Location = new System.Drawing.Point(6, 16);
            this.txtPlayer.Name = "txtPlayer";
            this.txtPlayer.Size = new System.Drawing.Size(105, 20);
            this.txtPlayer.TabIndex = 69;
            this.txtPlayer.Text = "Player name";
            this.txtPlayer.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtPlayer.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnGoto
            // 
            this.btnGoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGoto.BackColorUseGeneric = false;
            this.btnGoto.Checked = false;
            this.btnGoto.Location = new System.Drawing.Point(112, 16);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(48, 20);
            this.btnGoto.TabIndex = 68;
            this.btnGoto.Text = "Goto";
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // darkGroupBox12
            // 
            this.darkGroupBox12.Controls.Add(this.txtSetInt);
            this.darkGroupBox12.Controls.Add(this.btnSetInt);
            this.darkGroupBox12.Controls.Add(this.numSetInt);
            this.darkGroupBox12.Controls.Add(this.btnIncreaseInt);
            this.darkGroupBox12.Controls.Add(this.btnDecreaseInt);
            this.darkGroupBox12.Location = new System.Drawing.Point(6, 162);
            this.darkGroupBox12.Name = "darkGroupBox12";
            this.darkGroupBox12.Size = new System.Drawing.Size(167, 62);
            this.darkGroupBox12.TabIndex = 164;
            this.darkGroupBox12.TabStop = false;
            this.darkGroupBox12.Text = "Temp Integer";
            // 
            // txtSetInt
            // 
            this.txtSetInt.Location = new System.Drawing.Point(5, 15);
            this.txtSetInt.Name = "txtSetInt";
            this.txtSetInt.Size = new System.Drawing.Size(99, 20);
            this.txtSetInt.TabIndex = 156;
            this.txtSetInt.Text = "Integer";
            this.txtSetInt.Click += new System.EventHandler(this.TextboxEnter);
            this.txtSetInt.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnSetInt
            // 
            this.btnSetInt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetInt.BackColorUseGeneric = false;
            this.btnSetInt.Checked = false;
            this.btnSetInt.Location = new System.Drawing.Point(5, 34);
            this.btnSetInt.Name = "btnSetInt";
            this.btnSetInt.Size = new System.Drawing.Size(99, 23);
            this.btnSetInt.TabIndex = 155;
            this.btnSetInt.Text = "Set int (cmd)";
            this.btnSetInt.Click += new System.EventHandler(this.btnSetInt_Click);
            // 
            // numSetInt
            // 
            this.numSetInt.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSetInt.Location = new System.Drawing.Point(103, 15);
            this.numSetInt.LoopValues = false;
            this.numSetInt.Name = "numSetInt";
            this.numSetInt.Size = new System.Drawing.Size(56, 20);
            this.numSetInt.TabIndex = 157;
            // 
            // btnIncreaseInt
            // 
            this.btnIncreaseInt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnIncreaseInt.BackColorUseGeneric = false;
            this.btnIncreaseInt.Checked = false;
            this.btnIncreaseInt.Location = new System.Drawing.Point(103, 34);
            this.btnIncreaseInt.Name = "btnIncreaseInt";
            this.btnIncreaseInt.Size = new System.Drawing.Size(28, 23);
            this.btnIncreaseInt.TabIndex = 158;
            this.btnIncreaseInt.Text = "++";
            this.btnIncreaseInt.Click += new System.EventHandler(this.btnIncreaseInt_Click);
            // 
            // btnDecreaseInt
            // 
            this.btnDecreaseInt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnDecreaseInt.BackColorUseGeneric = false;
            this.btnDecreaseInt.Checked = false;
            this.btnDecreaseInt.Location = new System.Drawing.Point(130, 34);
            this.btnDecreaseInt.Name = "btnDecreaseInt";
            this.btnDecreaseInt.Size = new System.Drawing.Size(29, 23);
            this.btnDecreaseInt.TabIndex = 158;
            this.btnDecreaseInt.Text = "--";
            this.btnDecreaseInt.Click += new System.EventHandler(this.btnDecreaseInt_Click);
            // 
            // darkGroupBox8
            // 
            this.darkGroupBox8.Controls.Add(this.numIndexCmd);
            this.darkGroupBox8.Controls.Add(this.btnGotoIndex);
            this.darkGroupBox8.Controls.Add(this.btnGoUpIndex);
            this.darkGroupBox8.Controls.Add(this.btnGoDownIndex);
            this.darkGroupBox8.Location = new System.Drawing.Point(5, 275);
            this.darkGroupBox8.Name = "darkGroupBox8";
            this.darkGroupBox8.Size = new System.Drawing.Size(167, 39);
            this.darkGroupBox8.TabIndex = 162;
            this.darkGroupBox8.TabStop = false;
            this.darkGroupBox8.Text = "Index";
            // 
            // numIndexCmd
            // 
            this.numIndexCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numIndexCmd.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numIndexCmd.Location = new System.Drawing.Point(6, 14);
            this.numIndexCmd.LoopValues = false;
            this.numIndexCmd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numIndexCmd.Name = "numIndexCmd";
            this.numIndexCmd.Size = new System.Drawing.Size(54, 20);
            this.numIndexCmd.TabIndex = 152;
            // 
            // btnGotoIndex
            // 
            this.btnGotoIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGotoIndex.BackColorUseGeneric = false;
            this.btnGotoIndex.Checked = false;
            this.btnGotoIndex.Location = new System.Drawing.Point(61, 14);
            this.btnGotoIndex.Name = "btnGotoIndex";
            this.btnGotoIndex.Size = new System.Drawing.Size(44, 20);
            this.btnGotoIndex.TabIndex = 153;
            this.btnGotoIndex.Text = "Goto";
            this.btnGotoIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
            // 
            // btnGoUpIndex
            // 
            this.btnGoUpIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoUpIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGoUpIndex.BackColorUseGeneric = false;
            this.btnGoUpIndex.Checked = false;
            this.btnGoUpIndex.Location = new System.Drawing.Point(104, 14);
            this.btnGoUpIndex.Name = "btnGoUpIndex";
            this.btnGoUpIndex.Size = new System.Drawing.Size(29, 20);
            this.btnGoUpIndex.TabIndex = 153;
            this.btnGoUpIndex.Text = "++";
            this.btnGoUpIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
            // 
            // btnGoDownIndex
            // 
            this.btnGoDownIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoDownIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGoDownIndex.BackColorUseGeneric = false;
            this.btnGoDownIndex.Checked = false;
            this.btnGoDownIndex.Location = new System.Drawing.Point(131, 14);
            this.btnGoDownIndex.Name = "btnGoDownIndex";
            this.btnGoDownIndex.Size = new System.Drawing.Size(29, 20);
            this.btnGoDownIndex.TabIndex = 153;
            this.btnGoDownIndex.Text = "--";
            this.btnGoDownIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
            // 
            // darkGroupBox7
            // 
            this.darkGroupBox7.Controls.Add(this.txtCustomAggromon);
            this.darkGroupBox7.Controls.Add(this.chkInMapCustom);
            this.darkGroupBox7.Controls.Add(this.txtInMap);
            this.darkGroupBox7.Controls.Add(this.btnProvokeInMapOff);
            this.darkGroupBox7.Controls.Add(this.btnProvokeInMapOn);
            this.darkGroupBox7.Controls.Add(this.txtInRoom);
            this.darkGroupBox7.Controls.Add(this.btnProvokeOff);
            this.darkGroupBox7.Controls.Add(this.btnProvokeOn);
            this.darkGroupBox7.Location = new System.Drawing.Point(178, 182);
            this.darkGroupBox7.Name = "darkGroupBox7";
            this.darkGroupBox7.Size = new System.Drawing.Size(324, 58);
            this.darkGroupBox7.TabIndex = 161;
            this.darkGroupBox7.TabStop = false;
            this.darkGroupBox7.Text = "Provoke Monster";
            // 
            // txtCustomAggromon
            // 
            this.txtCustomAggromon.Enabled = false;
            this.txtCustomAggromon.Location = new System.Drawing.Point(133, 32);
            this.txtCustomAggromon.Name = "txtCustomAggromon";
            this.txtCustomAggromon.Size = new System.Drawing.Size(185, 20);
            this.txtCustomAggromon.TabIndex = 160;
            this.txtCustomAggromon.Text = "%xt%zm%aggroMon%1%MonMapID%";
            // 
            // chkInMapCustom
            // 
            this.chkInMapCustom.AutoSize = true;
            this.chkInMapCustom.Location = new System.Drawing.Point(133, 15);
            this.chkInMapCustom.Name = "chkInMapCustom";
            this.chkInMapCustom.Size = new System.Drawing.Size(60, 17);
            this.chkInMapCustom.TabIndex = 159;
            this.chkInMapCustom.Text = "Custom";
            this.chkInMapCustom.CheckedChanged += new System.EventHandler(this.chkInMapCustom_CheckedChanged);
            // 
            // txtInMap
            // 
            this.txtInMap.AutoSize = true;
            this.txtInMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtInMap.Location = new System.Drawing.Point(72, 16);
            this.txtInMap.Name = "txtInMap";
            this.txtInMap.Size = new System.Drawing.Size(42, 13);
            this.txtInMap.TabIndex = 154;
            this.txtInMap.Text = "In map:";
            // 
            // btnProvokeInMapOff
            // 
            this.btnProvokeInMapOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnProvokeInMapOff.BackColorUseGeneric = false;
            this.btnProvokeInMapOff.Checked = false;
            this.btnProvokeInMapOff.Location = new System.Drawing.Point(104, 32);
            this.btnProvokeInMapOff.Name = "btnProvokeInMapOff";
            this.btnProvokeInMapOff.Size = new System.Drawing.Size(30, 20);
            this.btnProvokeInMapOff.TabIndex = 153;
            this.btnProvokeInMapOff.Text = "Off";
            this.btnProvokeInMapOff.Click += new System.EventHandler(this.btnProvokeInMapOff_Click);
            // 
            // btnProvokeInMapOn
            // 
            this.btnProvokeInMapOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnProvokeInMapOn.BackColorUseGeneric = false;
            this.btnProvokeInMapOn.Checked = false;
            this.btnProvokeInMapOn.Location = new System.Drawing.Point(75, 32);
            this.btnProvokeInMapOn.Name = "btnProvokeInMapOn";
            this.btnProvokeInMapOn.Size = new System.Drawing.Size(30, 20);
            this.btnProvokeInMapOn.TabIndex = 152;
            this.btnProvokeInMapOn.Text = "On";
            this.btnProvokeInMapOn.Click += new System.EventHandler(this.btnProvokeInMapOn_Click);
            // 
            // txtInRoom
            // 
            this.txtInRoom.AutoSize = true;
            this.txtInRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtInRoom.Location = new System.Drawing.Point(3, 16);
            this.txtInRoom.Name = "txtInRoom";
            this.txtInRoom.Size = new System.Drawing.Size(38, 13);
            this.txtInRoom.TabIndex = 151;
            this.txtInRoom.Text = "In cell:";
            // 
            // btnProvokeOff
            // 
            this.btnProvokeOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnProvokeOff.BackColorUseGeneric = false;
            this.btnProvokeOff.Checked = false;
            this.btnProvokeOff.Location = new System.Drawing.Point(35, 32);
            this.btnProvokeOff.Name = "btnProvokeOff";
            this.btnProvokeOff.Size = new System.Drawing.Size(30, 20);
            this.btnProvokeOff.TabIndex = 150;
            this.btnProvokeOff.Text = "Off";
            this.btnProvokeOff.Click += new System.EventHandler(this.btnProvokeOff_Click);
            // 
            // btnProvokeOn
            // 
            this.btnProvokeOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnProvokeOn.BackColorUseGeneric = false;
            this.btnProvokeOn.Checked = false;
            this.btnProvokeOn.Location = new System.Drawing.Point(6, 32);
            this.btnProvokeOn.Name = "btnProvokeOn";
            this.btnProvokeOn.Size = new System.Drawing.Size(30, 20);
            this.btnProvokeOn.TabIndex = 149;
            this.btnProvokeOn.Text = "On";
            this.btnProvokeOn.Click += new System.EventHandler(this.btnProvokeOn_Click);
            // 
            // tabMisc2
            // 
            this.tabMisc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabMisc2.Controls.Add(this.darkGroupBox13);
            this.tabMisc2.Controls.Add(this.darkGroupBox10);
            this.tabMisc2.Controls.Add(this.darkGroupBox9);
            this.tabMisc2.Controls.Add(this.chkMerge);
            this.tabMisc2.Controls.Add(this.grpPacketlist);
            this.tabMisc2.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabMisc2.Location = new System.Drawing.Point(4, 20);
            this.tabMisc2.Margin = new System.Windows.Forms.Padding(0);
            this.tabMisc2.Name = "tabMisc2";
            this.tabMisc2.Padding = new System.Windows.Forms.Padding(3);
            this.tabMisc2.Size = new System.Drawing.Size(514, 320);
            this.tabMisc2.TabIndex = 8;
            this.tabMisc2.Text = "Misc 2";
            // 
            // darkGroupBox13
            // 
            this.darkGroupBox13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.darkGroupBox13.Controls.Add(this.label3);
            this.darkGroupBox13.Controls.Add(this.chkSkip);
            this.darkGroupBox13.Controls.Add(this.btnBotDelay);
            this.darkGroupBox13.Controls.Add(this.numBotDelay);
            this.darkGroupBox13.Controls.Add(this.chkRestartDeath);
            this.darkGroupBox13.Location = new System.Drawing.Point(236, 6);
            this.darkGroupBox13.Name = "darkGroupBox13";
            this.darkGroupBox13.Size = new System.Drawing.Size(158, 119);
            this.darkGroupBox13.TabIndex = 161;
            this.darkGroupBox13.TabStop = false;
            this.darkGroupBox13.Text = "Bot Delay";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 72;
            this.label3.Text = "Overall Delay:";
            // 
            // chkSkip
            // 
            this.chkSkip.AutoSize = true;
            this.chkSkip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.chkSkip.Checked = true;
            this.chkSkip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSkip.Location = new System.Drawing.Point(6, 68);
            this.chkSkip.Name = "chkSkip";
            this.chkSkip.Size = new System.Drawing.Size(145, 17);
            this.chkSkip.TabIndex = 62;
            this.chkSkip.Text = "Skip bot delay for index/if";
            // 
            // btnBotDelay
            // 
            this.btnBotDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBotDelay.BackColorUseGeneric = false;
            this.btnBotDelay.Checked = false;
            this.btnBotDelay.Location = new System.Drawing.Point(55, 36);
            this.btnBotDelay.Name = "btnBotDelay";
            this.btnBotDelay.Size = new System.Drawing.Size(97, 20);
            this.btnBotDelay.TabIndex = 70;
            this.btnBotDelay.Text = "Set delay (cmd)";
            this.btnBotDelay.Click += new System.EventHandler(this.btnBotDelay_Click);
            // 
            // numBotDelay
            // 
            this.numBotDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numBotDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numBotDelay.Location = new System.Drawing.Point(6, 36);
            this.numBotDelay.LoopValues = false;
            this.numBotDelay.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.numBotDelay.Name = "numBotDelay";
            this.numBotDelay.Size = new System.Drawing.Size(48, 20);
            this.numBotDelay.TabIndex = 71;
            this.numBotDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // chkRestartDeath
            // 
            this.chkRestartDeath.AutoSize = true;
            this.chkRestartDeath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.chkRestartDeath.Checked = true;
            this.chkRestartDeath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRestartDeath.Location = new System.Drawing.Point(6, 92);
            this.chkRestartDeath.Name = "chkRestartDeath";
            this.chkRestartDeath.Size = new System.Drawing.Size(132, 17);
            this.chkRestartDeath.TabIndex = 116;
            this.chkRestartDeath.Text = "Restart bot upon dying";
            // 
            // darkGroupBox10
            // 
            this.darkGroupBox10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.darkGroupBox10.Controls.Add(this.txtStopBotMessage);
            this.darkGroupBox10.Controls.Add(this.btnStopBotWithMessageCmd);
            this.darkGroupBox10.Controls.Add(this.btnStop);
            this.darkGroupBox10.Controls.Add(this.btnRestart);
            this.darkGroupBox10.Location = new System.Drawing.Point(6, 187);
            this.darkGroupBox10.Name = "darkGroupBox10";
            this.darkGroupBox10.Size = new System.Drawing.Size(224, 123);
            this.darkGroupBox10.TabIndex = 117;
            this.darkGroupBox10.TabStop = false;
            this.darkGroupBox10.Text = "Bot Control";
            // 
            // txtStopBotMessage
            // 
            this.txtStopBotMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStopBotMessage.Location = new System.Drawing.Point(6, 59);
            this.txtStopBotMessage.MaxLength = 2147483647;
            this.txtStopBotMessage.Multiline = true;
            this.txtStopBotMessage.Name = "txtStopBotMessage";
            this.txtStopBotMessage.Size = new System.Drawing.Size(212, 60);
            this.txtStopBotMessage.TabIndex = 110;
            this.txtStopBotMessage.Text = "Message description (for Stop bot command above)";
            this.txtStopBotMessage.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtStopBotMessage.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnStopBotWithMessageCmd
            // 
            this.btnStopBotWithMessageCmd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStopBotWithMessageCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnStopBotWithMessageCmd.BackColorUseGeneric = false;
            this.btnStopBotWithMessageCmd.Checked = false;
            this.btnStopBotWithMessageCmd.Location = new System.Drawing.Point(6, 38);
            this.btnStopBotWithMessageCmd.Name = "btnStopBotWithMessageCmd";
            this.btnStopBotWithMessageCmd.Size = new System.Drawing.Size(212, 22);
            this.btnStopBotWithMessageCmd.TabIndex = 67;
            this.btnStopBotWithMessageCmd.Text = "Stop bot with message (cmd)";
            this.btnStopBotWithMessageCmd.Click += new System.EventHandler(this.btnStopBotWithMessageCmd_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnStop.BackColorUseGeneric = false;
            this.btnStop.Checked = false;
            this.btnStop.Location = new System.Drawing.Point(6, 15);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(104, 22);
            this.btnStop.TabIndex = 65;
            this.btnStop.Text = "Stop bot (cmd)";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnRestart.BackColorUseGeneric = false;
            this.btnRestart.Checked = false;
            this.btnRestart.Location = new System.Drawing.Point(111, 15);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(107, 22);
            this.btnRestart.TabIndex = 66;
            this.btnRestart.Text = "Restart bot (cmd)";
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // darkGroupBox9
            // 
            this.darkGroupBox9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.darkGroupBox9.Controls.Add(this.btnLoad);
            this.darkGroupBox9.Controls.Add(this.txtAuthor);
            this.darkGroupBox9.Controls.Add(this.btnSave);
            this.darkGroupBox9.Controls.Add(this.txtDescription);
            this.darkGroupBox9.Location = new System.Drawing.Point(6, 6);
            this.darkGroupBox9.Name = "darkGroupBox9";
            this.darkGroupBox9.Size = new System.Drawing.Size(224, 175);
            this.darkGroupBox9.TabIndex = 116;
            this.darkGroupBox9.TabStop = false;
            this.darkGroupBox9.Text = "Save / Load Bot";
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoad.BackColorUseGeneric = false;
            this.btnLoad.Checked = false;
            this.btnLoad.Location = new System.Drawing.Point(113, 20);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(105, 22);
            this.btnLoad.TabIndex = 163;
            this.btnLoad.Text = "Load bot";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtAuthor
            // 
            this.txtAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAuthor.Location = new System.Drawing.Point(6, 44);
            this.txtAuthor.Multiline = true;
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(212, 20);
            this.txtAuthor.TabIndex = 110;
            this.txtAuthor.Text = "Author";
            this.txtAuthor.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtAuthor.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSave.BackColorUseGeneric = false;
            this.btnSave.Checked = false;
            this.btnSave.Location = new System.Drawing.Point(6, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 22);
            this.btnSave.TabIndex = 162;
            this.btnSave.Text = "Save bot";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(6, 63);
            this.txtDescription.MaxLength = 2147483647;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(212, 106);
            this.txtDescription.TabIndex = 109;
            this.txtDescription.Text = "Description (Write in Rich Text Format)";
            this.txtDescription.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtDescription.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // chkMerge
            // 
            this.chkMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMerge.AutoSize = true;
            this.chkMerge.Location = new System.Drawing.Point(706, 56);
            this.chkMerge.Name = "chkMerge";
            this.chkMerge.Size = new System.Drawing.Size(55, 17);
            this.chkMerge.TabIndex = 115;
            this.chkMerge.Text = "Merge";
            // 
            // grpPacketlist
            // 
            this.grpPacketlist.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpPacketlist.Controls.Add(this.chkEnablePacketlistSpam);
            this.grpPacketlist.Controls.Add(this.btnPacketlistAdd);
            this.grpPacketlist.Controls.Add(this.txtPacketlistCommands);
            this.grpPacketlist.Controls.Add(this.darkLabel3);
            this.grpPacketlist.Controls.Add(this.btnPacketlistOnCmd);
            this.grpPacketlist.Controls.Add(this.btnPacketlistOffCmd);
            this.grpPacketlist.Controls.Add(this.btnPacketlistClearCmd);
            this.grpPacketlist.Controls.Add(this.btnPacketlistRemoveCmd);
            this.grpPacketlist.Controls.Add(this.btnPacketlistSetDelayCmd);
            this.grpPacketlist.Controls.Add(this.txtPlistDelay);
            this.grpPacketlist.Controls.Add(this.numPacketlistDelay);
            this.grpPacketlist.Controls.Add(this.btnPacketlistAddCmd);
            this.grpPacketlist.Controls.Add(this.txtPacketlist);
            this.grpPacketlist.Enabled = false;
            this.grpPacketlist.Location = new System.Drawing.Point(236, 135);
            this.grpPacketlist.Name = "grpPacketlist";
            this.grpPacketlist.Padding = new System.Windows.Forms.Padding(0);
            this.grpPacketlist.Size = new System.Drawing.Size(219, 175);
            this.grpPacketlist.TabIndex = 59;
            this.grpPacketlist.TabStop = false;
            this.grpPacketlist.Text = "Packetlist";
            this.grpPacketlist.Visible = false;
            // 
            // chkEnablePacketlistSpam
            // 
            this.chkEnablePacketlistSpam.AutoSize = true;
            this.chkEnablePacketlistSpam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.chkEnablePacketlistSpam.Location = new System.Drawing.Point(109, 128);
            this.chkEnablePacketlistSpam.Name = "chkEnablePacketlistSpam";
            this.chkEnablePacketlistSpam.Size = new System.Drawing.Size(107, 17);
            this.chkEnablePacketlistSpam.TabIndex = 166;
            this.chkEnablePacketlistSpam.Text = "Enable Packetlist";
            // 
            // btnPacketlistAdd
            // 
            this.btnPacketlistAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPacketlistAdd.Checked = false;
            this.btnPacketlistAdd.Location = new System.Drawing.Point(6, 19);
            this.btnPacketlistAdd.Name = "btnPacketlistAdd";
            this.btnPacketlistAdd.Size = new System.Drawing.Size(207, 23);
            this.btnPacketlistAdd.TabIndex = 165;
            this.btnPacketlistAdd.Text = "Add to Packetlist";
            this.btnPacketlistAdd.Click += new System.EventHandler(this.btnPacketlistAdd_Click);
            // 
            // txtPacketlistCommands
            // 
            this.txtPacketlistCommands.AutoSize = true;
            this.txtPacketlistCommands.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtPacketlistCommands.Location = new System.Drawing.Point(3, 68);
            this.txtPacketlistCommands.Name = "txtPacketlistCommands";
            this.txtPacketlistCommands.Size = new System.Drawing.Size(62, 13);
            this.txtPacketlistCommands.TabIndex = 164;
            this.txtPacketlistCommands.Text = "Commands:";
            // 
            // darkLabel3
            // 
            this.darkLabel3.AutoSize = true;
            this.darkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel3.Location = new System.Drawing.Point(84, 150);
            this.darkLabel3.Name = "darkLabel3";
            this.darkLabel3.Size = new System.Drawing.Size(19, 13);
            this.darkLabel3.TabIndex = 162;
            this.darkLabel3.Text = "=>";
            // 
            // btnPacketlistOnCmd
            // 
            this.btnPacketlistOnCmd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPacketlistOnCmd.Checked = false;
            this.btnPacketlistOnCmd.Location = new System.Drawing.Point(6, 106);
            this.btnPacketlistOnCmd.Name = "btnPacketlistOnCmd";
            this.btnPacketlistOnCmd.Size = new System.Drawing.Size(104, 22);
            this.btnPacketlistOnCmd.TabIndex = 60;
            this.btnPacketlistOnCmd.Text = "Toggle On (cmd)";
            this.btnPacketlistOnCmd.Click += new System.EventHandler(this.btnPacketlist_Click);
            // 
            // btnPacketlistOffCmd
            // 
            this.btnPacketlistOffCmd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPacketlistOffCmd.Checked = false;
            this.btnPacketlistOffCmd.Location = new System.Drawing.Point(109, 106);
            this.btnPacketlistOffCmd.Name = "btnPacketlistOffCmd";
            this.btnPacketlistOffCmd.Size = new System.Drawing.Size(104, 22);
            this.btnPacketlistOffCmd.TabIndex = 61;
            this.btnPacketlistOffCmd.Text = "Toggle Off (cmd)";
            this.btnPacketlistOffCmd.Click += new System.EventHandler(this.btnPacketlist_Click);
            // 
            // btnPacketlistClearCmd
            // 
            this.btnPacketlistClearCmd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPacketlistClearCmd.Checked = false;
            this.btnPacketlistClearCmd.Location = new System.Drawing.Point(144, 84);
            this.btnPacketlistClearCmd.Name = "btnPacketlistClearCmd";
            this.btnPacketlistClearCmd.Size = new System.Drawing.Size(69, 23);
            this.btnPacketlistClearCmd.TabIndex = 62;
            this.btnPacketlistClearCmd.Text = "Clear";
            this.btnPacketlistClearCmd.Click += new System.EventHandler(this.btnPacketlist_Click);
            // 
            // btnPacketlistRemoveCmd
            // 
            this.btnPacketlistRemoveCmd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPacketlistRemoveCmd.Checked = false;
            this.btnPacketlistRemoveCmd.Location = new System.Drawing.Point(75, 84);
            this.btnPacketlistRemoveCmd.Name = "btnPacketlistRemoveCmd";
            this.btnPacketlistRemoveCmd.Size = new System.Drawing.Size(70, 23);
            this.btnPacketlistRemoveCmd.TabIndex = 63;
            this.btnPacketlistRemoveCmd.Text = "Remove";
            this.btnPacketlistRemoveCmd.Click += new System.EventHandler(this.btnPacketlist_Click);
            // 
            // btnPacketlistSetDelayCmd
            // 
            this.btnPacketlistSetDelayCmd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPacketlistSetDelayCmd.Checked = false;
            this.btnPacketlistSetDelayCmd.Location = new System.Drawing.Point(109, 148);
            this.btnPacketlistSetDelayCmd.Name = "btnPacketlistSetDelayCmd";
            this.btnPacketlistSetDelayCmd.Size = new System.Drawing.Size(104, 21);
            this.btnPacketlistSetDelayCmd.TabIndex = 64;
            this.btnPacketlistSetDelayCmd.Text = "Set delay (cmd)";
            this.btnPacketlistSetDelayCmd.Click += new System.EventHandler(this.btnPacketlist_Click);
            // 
            // txtPlistDelay
            // 
            this.txtPlistDelay.AutoSize = true;
            this.txtPlistDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtPlistDelay.Location = new System.Drawing.Point(4, 132);
            this.txtPlistDelay.Name = "txtPlistDelay";
            this.txtPlistDelay.Size = new System.Drawing.Size(37, 13);
            this.txtPlistDelay.TabIndex = 71;
            this.txtPlistDelay.Text = "Delay:";
            // 
            // numPacketlistDelay
            // 
            this.numPacketlistDelay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numPacketlistDelay.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numPacketlistDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numPacketlistDelay.Location = new System.Drawing.Point(7, 148);
            this.numPacketlistDelay.LoopValues = false;
            this.numPacketlistDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numPacketlistDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numPacketlistDelay.Name = "numPacketlistDelay";
            this.numPacketlistDelay.Size = new System.Drawing.Size(71, 20);
            this.numPacketlistDelay.TabIndex = 68;
            this.numPacketlistDelay.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // btnPacketlistAddCmd
            // 
            this.btnPacketlistAddCmd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPacketlistAddCmd.Checked = false;
            this.btnPacketlistAddCmd.Location = new System.Drawing.Point(6, 84);
            this.btnPacketlistAddCmd.Name = "btnPacketlistAddCmd";
            this.btnPacketlistAddCmd.Size = new System.Drawing.Size(70, 23);
            this.btnPacketlistAddCmd.TabIndex = 65;
            this.btnPacketlistAddCmd.Text = "Add";
            this.btnPacketlistAddCmd.Click += new System.EventHandler(this.btnPacketlist_Click);
            // 
            // txtPacketlist
            // 
            this.txtPacketlist.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPacketlist.Location = new System.Drawing.Point(6, 41);
            this.txtPacketlist.Name = "txtPacketlist";
            this.txtPacketlist.Size = new System.Drawing.Size(207, 20);
            this.txtPacketlist.TabIndex = 66;
            this.txtPacketlist.Text = "%xt%zm%cmd%1%tfer%PLAYERNAME%MAP-1e99%";
            // 
            // tabOptions
            // 
            this.tabOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabOptions.Controls.Add(this.darkGroupBox19);
            this.tabOptions.Controls.Add(this.darkGroupBox6);
            this.tabOptions.Controls.Add(this.txtSoundItem);
            this.tabOptions.Controls.Add(this.btnSoundAdd);
            this.tabOptions.Controls.Add(this.btnSoundDelete);
            this.tabOptions.Controls.Add(this.btnSoundTest);
            this.tabOptions.Controls.Add(this.lstSoundItems);
            this.tabOptions.Controls.Add(this.label9);
            this.tabOptions.Controls.Add(this.grpLogin);
            this.tabOptions.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabOptions.Location = new System.Drawing.Point(4, 20);
            this.tabOptions.Margin = new System.Windows.Forms.Padding(0);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(514, 320);
            this.tabOptions.TabIndex = 5;
            this.tabOptions.Text = "Options";
            // 
            // darkGroupBox19
            // 
            this.darkGroupBox19.Controls.Add(this.chkSaveState);
            this.darkGroupBox19.Controls.Add(this.chkProvokeAllMon);
            this.darkGroupBox19.Controls.Add(this.chkProvoke);
            this.darkGroupBox19.Controls.Add(this.chkGender);
            this.darkGroupBox19.Controls.Add(this.label6);
            this.darkGroupBox19.Controls.Add(this.chkInfiniteRange);
            this.darkGroupBox19.Controls.Add(this.numOptionsTimer);
            this.darkGroupBox19.Controls.Add(this.chkMagnet);
            this.darkGroupBox19.Controls.Add(this.chkUntarget);
            this.darkGroupBox19.Controls.Add(this.chkLag);
            this.darkGroupBox19.Controls.Add(this.chkEnableSettings);
            this.darkGroupBox19.Controls.Add(this.chkHidePlayers);
            this.darkGroupBox19.Controls.Add(this.chkDisableAnims);
            this.darkGroupBox19.Controls.Add(this.chkSkipCutscenes);
            this.darkGroupBox19.Controls.Add(this.label8);
            this.darkGroupBox19.Controls.Add(this.numWalkSpeed);
            this.darkGroupBox19.Location = new System.Drawing.Point(162, 3);
            this.darkGroupBox19.Name = "darkGroupBox19";
            this.darkGroupBox19.Size = new System.Drawing.Size(145, 286);
            this.darkGroupBox19.TabIndex = 160;
            this.darkGroupBox19.TabStop = false;
            this.darkGroupBox19.Text = "Bot Options";
            // 
            // chkSaveState
            // 
            this.chkSaveState.AutoSize = true;
            this.chkSaveState.Location = new System.Drawing.Point(6, 186);
            this.chkSaveState.Name = "chkSaveState";
            this.chkSaveState.Size = new System.Drawing.Size(99, 17);
            this.chkSaveState.TabIndex = 159;
            this.chkSaveState.Text = "Auto-save state";
            this.chkSaveState.CheckedChanged += new System.EventHandler(this.chkSaveState_CheckedChanged);
            // 
            // chkProvokeAllMon
            // 
            this.chkProvokeAllMon.AutoSize = true;
            this.chkProvokeAllMon.Location = new System.Drawing.Point(6, 50);
            this.chkProvokeAllMon.Name = "chkProvokeAllMon";
            this.chkProvokeAllMon.Size = new System.Drawing.Size(118, 17);
            this.chkProvokeAllMon.TabIndex = 158;
            this.chkProvokeAllMon.Text = "Provoke all monster";
            this.chkProvokeAllMon.CheckedChanged += new System.EventHandler(this.chkProvokeAllMon_CheckedChanged);
            // 
            // chkProvoke
            // 
            this.chkProvoke.AutoSize = true;
            this.chkProvoke.Location = new System.Drawing.Point(6, 33);
            this.chkProvoke.Name = "chkProvoke";
            this.chkProvoke.Size = new System.Drawing.Size(110, 17);
            this.chkProvoke.TabIndex = 117;
            this.chkProvoke.Text = "Provoke monsters";
            this.chkProvoke.CheckedChanged += new System.EventHandler(this.chkProvoke_CheckedChanged);
            this.chkProvoke.Click += new System.EventHandler(this.chkProvoke_Clicked);
            // 
            // chkGender
            // 
            this.chkGender.AutoSize = true;
            this.chkGender.Location = new System.Drawing.Point(6, 152);
            this.chkGender.Name = "chkGender";
            this.chkGender.Size = new System.Drawing.Size(88, 17);
            this.chkGender.TabIndex = 137;
            this.chkGender.Text = "Gender swap";
            this.chkGender.CheckedChanged += new System.EventHandler(this.changeGenderAsync);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label6.Location = new System.Drawing.Point(54, 263);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 157;
            this.label6.Text = "Options Timer";
            // 
            // chkInfiniteRange
            // 
            this.chkInfiniteRange.AutoSize = true;
            this.chkInfiniteRange.Location = new System.Drawing.Point(6, 16);
            this.chkInfiniteRange.Name = "chkInfiniteRange";
            this.chkInfiniteRange.Size = new System.Drawing.Size(119, 17);
            this.chkInfiniteRange.TabIndex = 116;
            this.chkInfiniteRange.Text = "Infinite attack range";
            this.chkInfiniteRange.CheckedChanged += new System.EventHandler(this.chkInfiniteRange_CheckedChanged);
            // 
            // numOptionsTimer
            // 
            this.numOptionsTimer.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numOptionsTimer.Location = new System.Drawing.Point(6, 260);
            this.numOptionsTimer.LoopValues = false;
            this.numOptionsTimer.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numOptionsTimer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOptionsTimer.Name = "numOptionsTimer";
            this.numOptionsTimer.Size = new System.Drawing.Size(42, 20);
            this.numOptionsTimer.TabIndex = 156;
            this.numOptionsTimer.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numOptionsTimer.ValueChanged += new System.EventHandler(this.numOptionsTimer_ValueChanged);
            // 
            // chkMagnet
            // 
            this.chkMagnet.AutoSize = true;
            this.chkMagnet.Location = new System.Drawing.Point(6, 67);
            this.chkMagnet.Name = "chkMagnet";
            this.chkMagnet.Size = new System.Drawing.Size(95, 17);
            this.chkMagnet.TabIndex = 118;
            this.chkMagnet.Text = "Enemy magnet";
            this.chkMagnet.CheckedChanged += new System.EventHandler(this.chkMagnet_CheckedChanged);
            // 
            // chkUntarget
            // 
            this.chkUntarget.AutoSize = true;
            this.chkUntarget.Location = new System.Drawing.Point(6, 169);
            this.chkUntarget.Name = "chkUntarget";
            this.chkUntarget.Size = new System.Drawing.Size(85, 17);
            this.chkUntarget.TabIndex = 154;
            this.chkUntarget.Text = "Untarget self";
            this.chkUntarget.CheckedChanged += new System.EventHandler(this.chkUntarget_CheckedChanged);
            // 
            // chkLag
            // 
            this.chkLag.AutoSize = true;
            this.chkLag.Location = new System.Drawing.Point(6, 84);
            this.chkLag.Name = "chkLag";
            this.chkLag.Size = new System.Drawing.Size(67, 17);
            this.chkLag.TabIndex = 119;
            this.chkLag.Text = "Lag killer";
            this.chkLag.CheckedChanged += new System.EventHandler(this.chkLag_CheckedChanged);
            // 
            // chkEnableSettings
            // 
            this.chkEnableSettings.Location = new System.Drawing.Point(6, 208);
            this.chkEnableSettings.Name = "chkEnableSettings";
            this.chkEnableSettings.Size = new System.Drawing.Size(96, 30);
            this.chkEnableSettings.TabIndex = 132;
            this.chkEnableSettings.Text = "Enable options\r\nwithout starting";
            this.chkEnableSettings.Click += new System.EventHandler(this.chkEnableSettings_Click);
            // 
            // chkHidePlayers
            // 
            this.chkHidePlayers.AutoSize = true;
            this.chkHidePlayers.Location = new System.Drawing.Point(6, 101);
            this.chkHidePlayers.Name = "chkHidePlayers";
            this.chkHidePlayers.Size = new System.Drawing.Size(83, 17);
            this.chkHidePlayers.TabIndex = 120;
            this.chkHidePlayers.Text = "Hide players";
            this.chkHidePlayers.CheckedChanged += new System.EventHandler(this.chkHidePlayers_CheckedChanged);
            // 
            // chkDisableAnims
            // 
            this.chkDisableAnims.Location = new System.Drawing.Point(6, 135);
            this.chkDisableAnims.Name = "chkDisableAnims";
            this.chkDisableAnims.Size = new System.Drawing.Size(133, 16);
            this.chkDisableAnims.TabIndex = 131;
            this.chkDisableAnims.Text = "Disable player anims";
            this.chkDisableAnims.CheckedChanged += new System.EventHandler(this.chkDisableAnims_CheckedChanged);
            // 
            // chkSkipCutscenes
            // 
            this.chkSkipCutscenes.AutoSize = true;
            this.chkSkipCutscenes.Location = new System.Drawing.Point(6, 118);
            this.chkSkipCutscenes.Name = "chkSkipCutscenes";
            this.chkSkipCutscenes.Size = new System.Drawing.Size(98, 17);
            this.chkSkipCutscenes.TabIndex = 121;
            this.chkSkipCutscenes.Text = "Skip cutscenes";
            this.chkSkipCutscenes.CheckedChanged += new System.EventHandler(this.chkSkipCutscenes_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label8.Location = new System.Drawing.Point(54, 242);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 122;
            this.label8.Text = "Walk speed";
            // 
            // numWalkSpeed
            // 
            this.numWalkSpeed.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numWalkSpeed.Location = new System.Drawing.Point(6, 238);
            this.numWalkSpeed.LoopValues = false;
            this.numWalkSpeed.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numWalkSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkSpeed.Name = "numWalkSpeed";
            this.numWalkSpeed.Size = new System.Drawing.Size(42, 20);
            this.numWalkSpeed.TabIndex = 123;
            this.numWalkSpeed.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numWalkSpeed.ValueChanged += new System.EventHandler(this.numWalkSpeed_ValueChanged);
            // 
            // darkGroupBox6
            // 
            this.darkGroupBox6.Controls.Add(this.splitContainer5);
            this.darkGroupBox6.Controls.Add(this.lstLogText);
            this.darkGroupBox6.Controls.Add(this.txtLog);
            this.darkGroupBox6.Controls.Add(this.label5);
            this.darkGroupBox6.Location = new System.Drawing.Point(313, 3);
            this.darkGroupBox6.Name = "darkGroupBox6";
            this.darkGroupBox6.Size = new System.Drawing.Size(189, 286);
            this.darkGroupBox6.TabIndex = 159;
            this.darkGroupBox6.TabStop = false;
            this.darkGroupBox6.Text = "Logs";
            // 
            // splitContainer5
            // 
            this.splitContainer5.Location = new System.Drawing.Point(7, 107);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.btnLog);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.btnLogDebug);
            this.splitContainer5.Size = new System.Drawing.Size(176, 23);
            this.splitContainer5.SplitterDistance = 86;
            this.splitContainer5.SplitterWidth = 2;
            this.splitContainer5.TabIndex = 156;
            // 
            // btnLog
            // 
            this.btnLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLog.BackColorUseGeneric = false;
            this.btnLog.Checked = false;
            this.btnLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLog.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLog.Location = new System.Drawing.Point(0, 0);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(86, 23);
            this.btnLog.TabIndex = 148;
            this.btnLog.Text = "Log Script";
            this.btnLog.Click += new System.EventHandler(this.logScript);
            // 
            // btnLogDebug
            // 
            this.btnLogDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLogDebug.BackColorUseGeneric = false;
            this.btnLogDebug.Checked = false;
            this.btnLogDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogDebug.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLogDebug.Location = new System.Drawing.Point(0, 0);
            this.btnLogDebug.Name = "btnLogDebug";
            this.btnLogDebug.Size = new System.Drawing.Size(88, 23);
            this.btnLogDebug.TabIndex = 152;
            this.btnLogDebug.Text = "Log Debug";
            this.btnLogDebug.Click += new System.EventHandler(this.logDebug);
            // 
            // lstLogText
            // 
            this.lstLogText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLogText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstLogText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLogText.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstLogText.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstLogText.FormattingEnabled = true;
            this.lstLogText.ItemHeight = 16;
            this.lstLogText.Items.AddRange(new object[] {
            "{USERNAME}",
            "{MAP}",
            "{GOLD}",
            "{LEVEL}",
            "{CELL}",
            "{HEALTH}",
            "{TIME: 12}",
            "{TIME: 24}",
            "{CLEAR}",
            "{ITEM: item name}",
            "{ITEM MAX: item name}",
            "{REP XP: faction}",
            "{REP RANK: faction}",
            "{REP TOTAL: faction}",
            "{REP REMAINING: faction}",
            "{REP REQUIRED: faction}",
            "{REP CURRENT: faction}",
            "{INT VALUE: int}",
            "{ROOM_ID}"});
            this.lstLogText.Location = new System.Drawing.Point(7, 150);
            this.lstLogText.Name = "lstLogText";
            this.lstLogText.Size = new System.Drawing.Size(176, 130);
            this.lstLogText.TabIndex = 153;
            this.lstLogText.DoubleClick += new System.EventHandler(this.lstLogText_DoubleClick);
            this.lstLogText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLogText_KeyDown);
            // 
            // txtLog
            // 
            this.txtLog.AcceptsReturn = true;
            this.txtLog.AcceptsTab = true;
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.AutoCompleteCustomSource.AddRange(new string[] {
            "{USERNAME}",
            "{MAP}",
            "{GOLD}",
            "{LEVEL}",
            "{CELL}",
            "{HEALTH}",
            "{TIME: 12}",
            "{TIME: 24}",
            "{CLEAR}",
            "{ITEM: item name}",
            "{ITEM MAX: item name}",
            "{REP XP: faction}",
            "{REP RANK: faction}",
            "{REP TOTAL: faction}",
            "{REP REMAINING: faction}",
            "{REP REQUIRED: faction}",
            "{REP CURRENT: faction}"});
            this.txtLog.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtLog.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtLog.Location = new System.Drawing.Point(7, 16);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(176, 89);
            this.txtLog.TabIndex = 147;
            this.txtLog.Text = "Text";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label5.Location = new System.Drawing.Point(4, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 155;
            this.label5.Text = "Viable Log References";
            // 
            // txtSoundItem
            // 
            this.txtSoundItem.Location = new System.Drawing.Point(4, 245);
            this.txtSoundItem.Name = "txtSoundItem";
            this.txtSoundItem.Size = new System.Drawing.Size(139, 20);
            this.txtSoundItem.TabIndex = 130;
            this.txtSoundItem.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtSoundItem.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnSoundAdd
            // 
            this.btnSoundAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSoundAdd.BackColorUseGeneric = false;
            this.btnSoundAdd.Checked = false;
            this.btnSoundAdd.Location = new System.Drawing.Point(49, 267);
            this.btnSoundAdd.Name = "btnSoundAdd";
            this.btnSoundAdd.Size = new System.Drawing.Size(43, 22);
            this.btnSoundAdd.TabIndex = 129;
            this.btnSoundAdd.Text = "Add";
            this.btnSoundAdd.Click += new System.EventHandler(this.btnSoundAdd_Click);
            // 
            // btnSoundDelete
            // 
            this.btnSoundDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSoundDelete.BackColorUseGeneric = false;
            this.btnSoundDelete.Checked = false;
            this.btnSoundDelete.Location = new System.Drawing.Point(94, 267);
            this.btnSoundDelete.Name = "btnSoundDelete";
            this.btnSoundDelete.Size = new System.Drawing.Size(49, 22);
            this.btnSoundDelete.TabIndex = 128;
            this.btnSoundDelete.Text = "Delete";
            this.btnSoundDelete.Click += new System.EventHandler(this.btnSoundDelete_Click);
            // 
            // btnSoundTest
            // 
            this.btnSoundTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSoundTest.BackColorUseGeneric = false;
            this.btnSoundTest.Checked = false;
            this.btnSoundTest.Location = new System.Drawing.Point(4, 267);
            this.btnSoundTest.Name = "btnSoundTest";
            this.btnSoundTest.Size = new System.Drawing.Size(42, 22);
            this.btnSoundTest.TabIndex = 126;
            this.btnSoundTest.Text = "Test";
            this.btnSoundTest.Click += new System.EventHandler(this.btnSoundTest_Click);
            // 
            // lstSoundItems
            // 
            this.lstSoundItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstSoundItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSoundItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstSoundItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstSoundItems.FormattingEnabled = true;
            this.lstSoundItems.ItemHeight = 18;
            this.lstSoundItems.Location = new System.Drawing.Point(3, 185);
            this.lstSoundItems.Name = "lstSoundItems";
            this.lstSoundItems.Size = new System.Drawing.Size(140, 56);
            this.lstSoundItems.TabIndex = 125;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label9.Location = new System.Drawing.Point(1, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 26);
            this.label9.TabIndex = 124;
            this.label9.Text = "If any of the following items\r\nare dropped, play a sound";
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.chkAFK);
            this.grpLogin.Controls.Add(this.cbServers);
            this.grpLogin.Controls.Add(this.chkRelogRetry);
            this.grpLogin.Controls.Add(this.chkRelog);
            this.grpLogin.Controls.Add(this.numRelogDelay);
            this.grpLogin.Controls.Add(this.label7);
            this.grpLogin.Location = new System.Drawing.Point(4, 3);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(152, 138);
            this.grpLogin.TabIndex = 115;
            this.grpLogin.TabStop = false;
            this.grpLogin.Text = "Auto relogin";
            // 
            // chkAFK
            // 
            this.chkAFK.AutoSize = true;
            this.chkAFK.Enabled = false;
            this.chkAFK.Location = new System.Drawing.Point(5, 59);
            this.chkAFK.Name = "chkAFK";
            this.chkAFK.Size = new System.Drawing.Size(99, 17);
            this.chkAFK.TabIndex = 159;
            this.chkAFK.Text = "Relogin on AFK";
            this.chkAFK.CheckedChanged += new System.EventHandler(this.chkAFK_CheckedChanged);
            // 
            // cbServers
            // 
            this.cbServers.DisplayMember = "Name";
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Location = new System.Drawing.Point(5, 16);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(123, 21);
            this.cbServers.TabIndex = 0;
            this.cbServers.ValueMember = "Name";
            // 
            // chkRelogRetry
            // 
            this.chkRelogRetry.AutoSize = true;
            this.chkRelogRetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkRelogRetry.Location = new System.Drawing.Point(5, 78);
            this.chkRelogRetry.Name = "chkRelogRetry";
            this.chkRelogRetry.Size = new System.Drawing.Size(142, 17);
            this.chkRelogRetry.TabIndex = 88;
            this.chkRelogRetry.Text = "Relog again if in battleon";
            // 
            // chkRelog
            // 
            this.chkRelog.AutoSize = true;
            this.chkRelog.Location = new System.Drawing.Point(5, 40);
            this.chkRelog.Name = "chkRelog";
            this.chkRelog.Size = new System.Drawing.Size(81, 17);
            this.chkRelog.TabIndex = 1;
            this.chkRelog.Text = "Auto relogin";
            this.chkRelog.CheckedChanged += new System.EventHandler(this.chkRelog_CheckedChanged);
            // 
            // numRelogDelay
            // 
            this.numRelogDelay.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRelogDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numRelogDelay.Location = new System.Drawing.Point(5, 114);
            this.numRelogDelay.LoopValues = false;
            this.numRelogDelay.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numRelogDelay.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRelogDelay.Name = "numRelogDelay";
            this.numRelogDelay.Size = new System.Drawing.Size(46, 20);
            this.numRelogDelay.TabIndex = 86;
            this.numRelogDelay.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label7.Location = new System.Drawing.Point(2, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 13);
            this.label7.TabIndex = 87;
            this.label7.Text = "Delay before starting the bot";
            // 
            // tabOptions2
            // 
            this.tabOptions2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabOptions2.Controls.Add(this.chkWarningMsgFilter);
            this.tabOptions2.Controls.Add(this.grpMapSwf);
            this.tabOptions2.Controls.Add(this.darkGroupBox17);
            this.tabOptions2.Controls.Add(this.darkGroupBox16);
            this.tabOptions2.Controls.Add(this.chkToggleMute);
            this.tabOptions2.Controls.Add(this.darkGroupBox15);
            this.tabOptions2.Controls.Add(this.groupBox1);
            this.tabOptions2.Controls.Add(this.chkChangeRoomTag);
            this.tabOptions2.Controls.Add(this.grpAccessLevel);
            this.tabOptions2.Controls.Add(this.chkChangeChat);
            this.tabOptions2.Controls.Add(this.chkHideYulgarPlayers);
            this.tabOptions2.Controls.Add(this.chkAntiAfk);
            this.tabOptions2.Controls.Add(this.grpAlignment);
            this.tabOptions2.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabOptions2.Location = new System.Drawing.Point(4, 20);
            this.tabOptions2.Margin = new System.Windows.Forms.Padding(0);
            this.tabOptions2.Name = "tabOptions2";
            this.tabOptions2.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions2.Size = new System.Drawing.Size(514, 320);
            this.tabOptions2.TabIndex = 7;
            this.tabOptions2.Text = "Client";
            // 
            // chkWarningMsgFilter
            // 
            this.chkWarningMsgFilter.Location = new System.Drawing.Point(168, 284);
            this.chkWarningMsgFilter.Name = "chkWarningMsgFilter";
            this.chkWarningMsgFilter.Size = new System.Drawing.Size(186, 17);
            this.chkWarningMsgFilter.TabIndex = 154;
            this.chkWarningMsgFilter.Text = "Disable server warning messages";
            this.chkWarningMsgFilter.Visible = false;
            this.chkWarningMsgFilter.CheckedChanged += new System.EventHandler(this.chkWarningMsgFilter_Click);
            // 
            // grpMapSwf
            // 
            this.grpMapSwf.Controls.Add(this.btnReloadMap);
            this.grpMapSwf.Controls.Add(this.txtMapSwf);
            this.grpMapSwf.Controls.Add(this.btnLoadMapSwf);
            this.grpMapSwf.Controls.Add(this.btnLoadMapSwfCmd);
            this.grpMapSwf.Location = new System.Drawing.Point(182, 101);
            this.grpMapSwf.Name = "grpMapSwf";
            this.grpMapSwf.Size = new System.Drawing.Size(124, 84);
            this.grpMapSwf.TabIndex = 153;
            this.grpMapSwf.TabStop = false;
            this.grpMapSwf.Text = "Map SWF";
            // 
            // btnReloadMap
            // 
            this.btnReloadMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnReloadMap.BackColorUseGeneric = false;
            this.btnReloadMap.Checked = false;
            this.btnReloadMap.Location = new System.Drawing.Point(6, 58);
            this.btnReloadMap.Name = "btnReloadMap";
            this.btnReloadMap.Size = new System.Drawing.Size(112, 20);
            this.btnReloadMap.TabIndex = 155;
            this.btnReloadMap.Text = "Reload map";
            this.btnReloadMap.Click += new System.EventHandler(this.btnReloadMap_Click);
            // 
            // txtMapSwf
            // 
            this.txtMapSwf.Location = new System.Drawing.Point(6, 18);
            this.txtMapSwf.Name = "txtMapSwf";
            this.txtMapSwf.Size = new System.Drawing.Size(112, 20);
            this.txtMapSwf.TabIndex = 135;
            this.txtMapSwf.Text = "Map filename (.swf)";
            this.txtMapSwf.Click += new System.EventHandler(this.TextboxEnter);
            this.txtMapSwf.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnLoadMapSwf
            // 
            this.btnLoadMapSwf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoadMapSwf.BackColorUseGeneric = false;
            this.btnLoadMapSwf.Checked = false;
            this.btnLoadMapSwf.Location = new System.Drawing.Point(6, 37);
            this.btnLoadMapSwf.Name = "btnLoadMapSwf";
            this.btnLoadMapSwf.Size = new System.Drawing.Size(56, 20);
            this.btnLoadMapSwf.TabIndex = 142;
            this.btnLoadMapSwf.Text = "Load";
            this.btnLoadMapSwf.Click += new System.EventHandler(this.btnLoadMapSwf_Click);
            // 
            // btnLoadMapSwfCmd
            // 
            this.btnLoadMapSwfCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLoadMapSwfCmd.BackColorUseGeneric = false;
            this.btnLoadMapSwfCmd.Checked = false;
            this.btnLoadMapSwfCmd.Location = new System.Drawing.Point(61, 37);
            this.btnLoadMapSwfCmd.Name = "btnLoadMapSwfCmd";
            this.btnLoadMapSwfCmd.Size = new System.Drawing.Size(57, 20);
            this.btnLoadMapSwfCmd.TabIndex = 142;
            this.btnLoadMapSwfCmd.Text = "(cmd)";
            this.btnLoadMapSwfCmd.Click += new System.EventHandler(this.btnLoadMapSwfCmd_Click);
            // 
            // darkGroupBox17
            // 
            this.darkGroupBox17.Controls.Add(this.txtClassName);
            this.darkGroupBox17.Controls.Add(this.btnSetCustomClassName);
            this.darkGroupBox17.Controls.Add(this.txtUsername);
            this.darkGroupBox17.Controls.Add(this.txtGuild);
            this.darkGroupBox17.Controls.Add(this.btnchangeGuild);
            this.darkGroupBox17.Controls.Add(this.btnChangeGuildCmd);
            this.darkGroupBox17.Controls.Add(this.btnchangeName);
            this.darkGroupBox17.Controls.Add(this.btnChangeNameCmd);
            this.darkGroupBox17.Location = new System.Drawing.Point(6, 6);
            this.darkGroupBox17.Name = "darkGroupBox17";
            this.darkGroupBox17.Size = new System.Drawing.Size(172, 86);
            this.darkGroupBox17.TabIndex = 152;
            this.darkGroupBox17.TabStop = false;
            this.darkGroupBox17.Text = "Player";
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(6, 60);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(69, 20);
            this.txtClassName.TabIndex = 144;
            this.txtClassName.Text = "Class";
            this.txtClassName.Click += new System.EventHandler(this.TextboxEnter);
            this.txtClassName.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnSetCustomClassName
            // 
            this.btnSetCustomClassName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetCustomClassName.BackColorUseGeneric = false;
            this.btnSetCustomClassName.Checked = false;
            this.btnSetCustomClassName.Location = new System.Drawing.Point(81, 60);
            this.btnSetCustomClassName.Name = "btnSetCustomClassName";
            this.btnSetCustomClassName.Size = new System.Drawing.Size(84, 20);
            this.btnSetCustomClassName.TabIndex = 145;
            this.btnSetCustomClassName.Text = "Set";
            this.btnSetCustomClassName.Click += new System.EventHandler(this.btnSetCustomClassName_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(6, 18);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(69, 20);
            this.txtUsername.TabIndex = 135;
            this.txtUsername.Text = "Username";
            this.txtUsername.Click += new System.EventHandler(this.TextboxEnter);
            this.txtUsername.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtGuild
            // 
            this.txtGuild.Location = new System.Drawing.Point(6, 39);
            this.txtGuild.Name = "txtGuild";
            this.txtGuild.Size = new System.Drawing.Size(69, 20);
            this.txtGuild.TabIndex = 136;
            this.txtGuild.Text = "Guild";
            this.txtGuild.Click += new System.EventHandler(this.TextboxEnter);
            this.txtGuild.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnchangeGuild
            // 
            this.btnchangeGuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnchangeGuild.BackColorUseGeneric = false;
            this.btnchangeGuild.Checked = false;
            this.btnchangeGuild.Location = new System.Drawing.Point(81, 39);
            this.btnchangeGuild.Name = "btnchangeGuild";
            this.btnchangeGuild.Size = new System.Drawing.Size(38, 20);
            this.btnchangeGuild.TabIndex = 143;
            this.btnchangeGuild.Text = "Set";
            this.btnchangeGuild.Click += new System.EventHandler(this.btnchangeGuild_Click);
            // 
            // btnChangeGuildCmd
            // 
            this.btnChangeGuildCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnChangeGuildCmd.BackColorUseGeneric = false;
            this.btnChangeGuildCmd.Checked = false;
            this.btnChangeGuildCmd.Location = new System.Drawing.Point(120, 39);
            this.btnChangeGuildCmd.Name = "btnChangeGuildCmd";
            this.btnChangeGuildCmd.Size = new System.Drawing.Size(45, 20);
            this.btnChangeGuildCmd.TabIndex = 143;
            this.btnChangeGuildCmd.Text = "(cmd)";
            this.btnChangeGuildCmd.Click += new System.EventHandler(this.btnChangeCmd_Click);
            // 
            // btnchangeName
            // 
            this.btnchangeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnchangeName.BackColorUseGeneric = false;
            this.btnchangeName.Checked = false;
            this.btnchangeName.Location = new System.Drawing.Point(81, 18);
            this.btnchangeName.Name = "btnchangeName";
            this.btnchangeName.Size = new System.Drawing.Size(38, 20);
            this.btnchangeName.TabIndex = 142;
            this.btnchangeName.Text = "Set";
            this.btnchangeName.Click += new System.EventHandler(this.btnchangeName_Click);
            // 
            // btnChangeNameCmd
            // 
            this.btnChangeNameCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnChangeNameCmd.BackColorUseGeneric = false;
            this.btnChangeNameCmd.Checked = false;
            this.btnChangeNameCmd.Location = new System.Drawing.Point(120, 18);
            this.btnChangeNameCmd.Name = "btnChangeNameCmd";
            this.btnChangeNameCmd.Size = new System.Drawing.Size(45, 20);
            this.btnChangeNameCmd.TabIndex = 142;
            this.btnChangeNameCmd.Text = "(cmd)";
            this.btnChangeNameCmd.Click += new System.EventHandler(this.btnChangeCmd_Click);
            // 
            // darkGroupBox16
            // 
            this.darkGroupBox16.Controls.Add(this.btnAddClientGold);
            this.darkGroupBox16.Controls.Add(this.btnAddClientACs);
            this.darkGroupBox16.Location = new System.Drawing.Point(6, 139);
            this.darkGroupBox16.Name = "darkGroupBox16";
            this.darkGroupBox16.Size = new System.Drawing.Size(172, 68);
            this.darkGroupBox16.TabIndex = 151;
            this.darkGroupBox16.TabStop = false;
            this.darkGroupBox16.Text = "Gold / ACs";
            // 
            // btnAddClientGold
            // 
            this.btnAddClientGold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAddClientGold.BackColorUseGeneric = false;
            this.btnAddClientGold.Checked = false;
            this.btnAddClientGold.Location = new System.Drawing.Point(6, 14);
            this.btnAddClientGold.Name = "btnAddClientGold";
            this.btnAddClientGold.Size = new System.Drawing.Size(159, 23);
            this.btnAddClientGold.TabIndex = 145;
            this.btnAddClientGold.Text = "Add infinite gold count";
            this.btnAddClientGold.Click += new System.EventHandler(this.btnAddClientGold_Click);
            // 
            // btnAddClientACs
            // 
            this.btnAddClientACs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAddClientACs.BackColorUseGeneric = false;
            this.btnAddClientACs.Checked = false;
            this.btnAddClientACs.Location = new System.Drawing.Point(6, 39);
            this.btnAddClientACs.Name = "btnAddClientACs";
            this.btnAddClientACs.Size = new System.Drawing.Size(159, 23);
            this.btnAddClientACs.TabIndex = 146;
            this.btnAddClientACs.Text = "Add infinite ACs count";
            this.btnAddClientACs.Click += new System.EventHandler(this.btnAddClientACs_Click);
            // 
            // chkToggleMute
            // 
            this.chkToggleMute.AutoSize = true;
            this.chkToggleMute.Location = new System.Drawing.Point(6, 284);
            this.chkToggleMute.Name = "chkToggleMute";
            this.chkToggleMute.Size = new System.Drawing.Size(84, 17);
            this.chkToggleMute.TabIndex = 6;
            this.chkToggleMute.Text = "Toggle mute";
            this.chkToggleMute.CheckedChanged += new System.EventHandler(this.chkToggleMute_CheckedChanged);
            // 
            // darkGroupBox15
            // 
            this.darkGroupBox15.Controls.Add(this.cmdSetClientLevel);
            this.darkGroupBox15.Controls.Add(this.btnSetJoinLevel);
            this.darkGroupBox15.Location = new System.Drawing.Point(6, 93);
            this.darkGroupBox15.Name = "darkGroupBox15";
            this.darkGroupBox15.Size = new System.Drawing.Size(172, 46);
            this.darkGroupBox15.TabIndex = 150;
            this.darkGroupBox15.TabStop = false;
            this.darkGroupBox15.Text = "Client Level";
            // 
            // cmdSetClientLevel
            // 
            this.cmdSetClientLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cmdSetClientLevel.BackColorUseGeneric = false;
            this.cmdSetClientLevel.Checked = false;
            this.cmdSetClientLevel.Location = new System.Drawing.Point(120, 17);
            this.cmdSetClientLevel.Name = "cmdSetClientLevel";
            this.cmdSetClientLevel.Size = new System.Drawing.Size(45, 23);
            this.cmdSetClientLevel.TabIndex = 144;
            this.cmdSetClientLevel.Text = "(cmd)";
            this.cmdSetClientLevel.Click += new System.EventHandler(this.btnSetClientLevel_Click);
            // 
            // btnSetJoinLevel
            // 
            this.btnSetJoinLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetJoinLevel.BackColorUseGeneric = false;
            this.btnSetJoinLevel.Checked = false;
            this.btnSetJoinLevel.Location = new System.Drawing.Point(6, 17);
            this.btnSetJoinLevel.Name = "btnSetJoinLevel";
            this.btnSetJoinLevel.Size = new System.Drawing.Size(113, 23);
            this.btnSetJoinLevel.TabIndex = 142;
            this.btnSetJoinLevel.Text = "Set level to max";
            this.btnSetJoinLevel.Click += new System.EventHandler(this.btnSetJoinLevel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddInfoMsg);
            this.groupBox1.Controls.Add(this.btnAddWarnMsg);
            this.groupBox1.Controls.Add(this.inputMsgClient);
            this.groupBox1.Location = new System.Drawing.Point(182, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 92);
            this.groupBox1.TabIndex = 148;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Messages";
            // 
            // btnAddInfoMsg
            // 
            this.btnAddInfoMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAddInfoMsg.BackColorUseGeneric = false;
            this.btnAddInfoMsg.Checked = false;
            this.btnAddInfoMsg.Location = new System.Drawing.Point(6, 63);
            this.btnAddInfoMsg.Name = "btnAddInfoMsg";
            this.btnAddInfoMsg.Size = new System.Drawing.Size(112, 23);
            this.btnAddInfoMsg.TabIndex = 150;
            this.btnAddInfoMsg.Text = "Add info msg";
            this.btnAddInfoMsg.Click += new System.EventHandler(this.btnClientMessageEvt);
            // 
            // btnAddWarnMsg
            // 
            this.btnAddWarnMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnAddWarnMsg.BackColorUseGeneric = false;
            this.btnAddWarnMsg.Checked = false;
            this.btnAddWarnMsg.Location = new System.Drawing.Point(6, 39);
            this.btnAddWarnMsg.Name = "btnAddWarnMsg";
            this.btnAddWarnMsg.Size = new System.Drawing.Size(112, 23);
            this.btnAddWarnMsg.TabIndex = 149;
            this.btnAddWarnMsg.Text = "Add warning msg";
            this.btnAddWarnMsg.Click += new System.EventHandler(this.btnClientMessageEvt);
            // 
            // inputMsgClient
            // 
            this.inputMsgClient.Location = new System.Drawing.Point(6, 18);
            this.inputMsgClient.Name = "inputMsgClient";
            this.inputMsgClient.Size = new System.Drawing.Size(112, 20);
            this.inputMsgClient.TabIndex = 147;
            this.inputMsgClient.Text = "Hello World!";
            this.inputMsgClient.Click += new System.EventHandler(this.TextboxEnter);
            this.inputMsgClient.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // chkChangeRoomTag
            // 
            this.chkChangeRoomTag.AutoSize = true;
            this.chkChangeRoomTag.Location = new System.Drawing.Point(168, 238);
            this.chkChangeRoomTag.Name = "chkChangeRoomTag";
            this.chkChangeRoomTag.Size = new System.Drawing.Size(106, 17);
            this.chkChangeRoomTag.TabIndex = 144;
            this.chkChangeRoomTag.Text = "Anonymous room";
            this.chkChangeRoomTag.CheckedChanged += new System.EventHandler(this.chkChangeRoomTag_CheckedChanged);
            // 
            // grpAccessLevel
            // 
            this.grpAccessLevel.Controls.Add(this.btnSetMem);
            this.grpAccessLevel.Controls.Add(this.btnSetModerator);
            this.grpAccessLevel.Controls.Add(this.btnSetNonMem);
            this.grpAccessLevel.Location = new System.Drawing.Point(404, 6);
            this.grpAccessLevel.Name = "grpAccessLevel";
            this.grpAccessLevel.Size = new System.Drawing.Size(88, 96);
            this.grpAccessLevel.TabIndex = 6;
            this.grpAccessLevel.TabStop = false;
            this.grpAccessLevel.Text = "Access";
            // 
            // btnSetMem
            // 
            this.btnSetMem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetMem.BackColorUseGeneric = false;
            this.btnSetMem.Checked = false;
            this.btnSetMem.Location = new System.Drawing.Point(6, 19);
            this.btnSetMem.Name = "btnSetMem";
            this.btnSetMem.Size = new System.Drawing.Size(75, 23);
            this.btnSetMem.TabIndex = 3;
            this.btnSetMem.Text = "Member";
            this.btnSetMem.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetModerator
            // 
            this.btnSetModerator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetModerator.BackColorUseGeneric = false;
            this.btnSetModerator.Checked = false;
            this.btnSetModerator.Location = new System.Drawing.Point(6, 67);
            this.btnSetModerator.Name = "btnSetModerator";
            this.btnSetModerator.Size = new System.Drawing.Size(75, 23);
            this.btnSetModerator.TabIndex = 5;
            this.btnSetModerator.Text = "Moderator";
            this.btnSetModerator.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetNonMem
            // 
            this.btnSetNonMem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetNonMem.BackColorUseGeneric = false;
            this.btnSetNonMem.Checked = false;
            this.btnSetNonMem.Location = new System.Drawing.Point(6, 43);
            this.btnSetNonMem.Name = "btnSetNonMem";
            this.btnSetNonMem.Size = new System.Drawing.Size(75, 23);
            this.btnSetNonMem.TabIndex = 4;
            this.btnSetNonMem.Text = "Non-Mem";
            this.btnSetNonMem.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // chkChangeChat
            // 
            this.chkChangeChat.AutoSize = true;
            this.chkChangeChat.Location = new System.Drawing.Point(168, 261);
            this.chkChangeChat.Name = "chkChangeChat";
            this.chkChangeChat.Size = new System.Drawing.Size(103, 17);
            this.chkChangeChat.TabIndex = 144;
            this.chkChangeChat.Text = "Anonymous user";
            this.chkChangeChat.CheckedChanged += new System.EventHandler(this.chkChangeChat_CheckedChanged);
            // 
            // chkHideYulgarPlayers
            // 
            this.chkHideYulgarPlayers.AutoSize = true;
            this.chkHideYulgarPlayers.Location = new System.Drawing.Point(6, 238);
            this.chkHideYulgarPlayers.Name = "chkHideYulgarPlayers";
            this.chkHideYulgarPlayers.Size = new System.Drawing.Size(122, 17);
            this.chkHideYulgarPlayers.TabIndex = 144;
            this.chkHideYulgarPlayers.Text = "Hide players upstairs";
            this.chkHideYulgarPlayers.CheckedChanged += new System.EventHandler(this.chkHideYulgarPlayers_CheckedChanged);
            // 
            // chkAntiAfk
            // 
            this.chkAntiAfk.AutoSize = true;
            this.chkAntiAfk.Location = new System.Drawing.Point(6, 261);
            this.chkAntiAfk.Name = "chkAntiAfk";
            this.chkAntiAfk.Size = new System.Drawing.Size(66, 17);
            this.chkAntiAfk.TabIndex = 144;
            this.chkAntiAfk.Text = "Anti-AFK";
            this.chkAntiAfk.CheckedChanged += new System.EventHandler(this.chkAntiAfk_CheckedChanged);
            // 
            // grpAlignment
            // 
            this.grpAlignment.Controls.Add(this.btnSetChaos);
            this.grpAlignment.Controls.Add(this.btnSetUndecided);
            this.grpAlignment.Controls.Add(this.btnSetGood);
            this.grpAlignment.Controls.Add(this.btnSetEvil);
            this.grpAlignment.Location = new System.Drawing.Point(312, 6);
            this.grpAlignment.Name = "grpAlignment";
            this.grpAlignment.Size = new System.Drawing.Size(86, 120);
            this.grpAlignment.TabIndex = 1;
            this.grpAlignment.TabStop = false;
            this.grpAlignment.Text = "Alignment";
            // 
            // btnSetChaos
            // 
            this.btnSetChaos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetChaos.BackColorUseGeneric = false;
            this.btnSetChaos.Checked = false;
            this.btnSetChaos.Location = new System.Drawing.Point(5, 67);
            this.btnSetChaos.Name = "btnSetChaos";
            this.btnSetChaos.Size = new System.Drawing.Size(75, 23);
            this.btnSetChaos.TabIndex = 0;
            this.btnSetChaos.Text = "Chaos";
            this.btnSetChaos.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetUndecided
            // 
            this.btnSetUndecided.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetUndecided.BackColorUseGeneric = false;
            this.btnSetUndecided.Checked = false;
            this.btnSetUndecided.Location = new System.Drawing.Point(5, 91);
            this.btnSetUndecided.Name = "btnSetUndecided";
            this.btnSetUndecided.Size = new System.Drawing.Size(75, 23);
            this.btnSetUndecided.TabIndex = 0;
            this.btnSetUndecided.Text = "Undecided";
            this.btnSetUndecided.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetGood
            // 
            this.btnSetGood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetGood.BackColorUseGeneric = false;
            this.btnSetGood.Checked = false;
            this.btnSetGood.Location = new System.Drawing.Point(5, 19);
            this.btnSetGood.Name = "btnSetGood";
            this.btnSetGood.Size = new System.Drawing.Size(75, 23);
            this.btnSetGood.TabIndex = 0;
            this.btnSetGood.Text = "Good";
            this.btnSetGood.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetEvil
            // 
            this.btnSetEvil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSetEvil.BackColorUseGeneric = false;
            this.btnSetEvil.Checked = false;
            this.btnSetEvil.Location = new System.Drawing.Point(5, 43);
            this.btnSetEvil.Name = "btnSetEvil";
            this.btnSetEvil.Size = new System.Drawing.Size(75, 23);
            this.btnSetEvil.TabIndex = 0;
            this.btnSetEvil.Text = "Evil";
            this.btnSetEvil.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // tabBots
            // 
            this.tabBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tabBots.Controls.Add(this.btnSaveDirectory);
            this.tabBots.Controls.Add(this.darkPanel1);
            this.tabBots.Controls.Add(this.panel6);
            this.tabBots.Controls.Add(this.lblBoosts);
            this.tabBots.Controls.Add(this.lblDrops);
            this.tabBots.Controls.Add(this.lblQuests);
            this.tabBots.Controls.Add(this.lblSkills);
            this.tabBots.Controls.Add(this.lblCommands);
            this.tabBots.Controls.Add(this.lblItems);
            this.tabBots.Controls.Add(this.txtSavedAuthor);
            this.tabBots.Controls.Add(this.lblBots);
            this.tabBots.Controls.Add(this.txtSavedAdd);
            this.tabBots.Controls.Add(this.btnSavedAdd);
            this.tabBots.Controls.Add(this.txtSaved);
            this.tabBots.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabBots.Location = new System.Drawing.Point(4, 20);
            this.tabBots.Margin = new System.Windows.Forms.Padding(0);
            this.tabBots.Name = "tabBots";
            this.tabBots.Padding = new System.Windows.Forms.Padding(3);
            this.tabBots.Size = new System.Drawing.Size(514, 320);
            this.tabBots.TabIndex = 6;
            this.tabBots.Text = "Bots";
            // 
            // btnSaveDirectory
            // 
            this.btnSaveDirectory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSaveDirectory.BackColorUseGeneric = false;
            this.btnSaveDirectory.Checked = false;
            this.btnSaveDirectory.Location = new System.Drawing.Point(398, 4);
            this.btnSaveDirectory.Name = "btnSaveDirectory";
            this.btnSaveDirectory.Size = new System.Drawing.Size(90, 20);
            this.btnSaveDirectory.TabIndex = 149;
            this.btnSaveDirectory.Text = "Save directory";
            this.btnSaveDirectory.Click += new System.EventHandler(this.btnSaveDirectory_Click);
            // 
            // darkPanel1
            // 
            this.darkPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkPanel1.Controls.Add(this.treeBots);
            this.darkPanel1.Location = new System.Drawing.Point(4, 27);
            this.darkPanel1.Name = "darkPanel1";
            this.darkPanel1.Size = new System.Drawing.Size(282, 228);
            this.darkPanel1.TabIndex = 148;
            // 
            // treeBots
            // 
            this.treeBots.AllowDrop = true;
            this.treeBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.treeBots.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeBots.ForeColor = System.Drawing.Color.Gainsboro;
            this.treeBots.LineColor = System.Drawing.Color.DarkGray;
            this.treeBots.Location = new System.Drawing.Point(0, 0);
            this.treeBots.Name = "treeBots";
            this.treeBots.Size = new System.Drawing.Size(282, 228);
            this.treeBots.TabIndex = 17;
            this.treeBots.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeBots_AfterExpand);
            this.treeBots.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeBots_AfterSelect);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtSavedDesc);
            this.panel6.Location = new System.Drawing.Point(295, 90);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(193, 165);
            this.panel6.TabIndex = 147;
            // 
            // txtSavedDesc
            // 
            this.txtSavedDesc.Location = new System.Drawing.Point(0, 0);
            this.txtSavedDesc.MaxLength = 2147483647;
            this.txtSavedDesc.Multiline = true;
            this.txtSavedDesc.Name = "txtSavedDesc";
            this.txtSavedDesc.Size = new System.Drawing.Size(193, 165);
            this.txtSavedDesc.TabIndex = 20;
            this.txtSavedDesc.Text = "Description";
            // 
            // lblBoosts
            // 
            this.lblBoosts.AutoSize = true;
            this.lblBoosts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblBoosts.Location = new System.Drawing.Point(357, 275);
            this.lblBoosts.Name = "lblBoosts";
            this.lblBoosts.Size = new System.Drawing.Size(42, 13);
            this.lblBoosts.TabIndex = 25;
            this.lblBoosts.Text = "Boosts:";
            this.lblBoosts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblDrops
            // 
            this.lblDrops.AutoSize = true;
            this.lblDrops.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblDrops.Location = new System.Drawing.Point(275, 275);
            this.lblDrops.Name = "lblDrops";
            this.lblDrops.Size = new System.Drawing.Size(38, 13);
            this.lblDrops.TabIndex = 24;
            this.lblDrops.Text = "Drops:";
            this.lblDrops.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblQuests
            // 
            this.lblQuests.AutoSize = true;
            this.lblQuests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblQuests.Location = new System.Drawing.Point(187, 275);
            this.lblQuests.Name = "lblQuests";
            this.lblQuests.Size = new System.Drawing.Size(43, 13);
            this.lblQuests.TabIndex = 23;
            this.lblQuests.Text = "Quests:";
            this.lblQuests.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblSkills
            // 
            this.lblSkills.AutoSize = true;
            this.lblSkills.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblSkills.Location = new System.Drawing.Point(113, 275);
            this.lblSkills.Name = "lblSkills";
            this.lblSkills.Size = new System.Drawing.Size(34, 13);
            this.lblSkills.TabIndex = 22;
            this.lblSkills.Text = "Skills:";
            this.lblSkills.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblCommands
            // 
            this.lblCommands.AutoSize = true;
            this.lblCommands.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblCommands.Location = new System.Drawing.Point(1, 275);
            this.lblCommands.Name = "lblCommands";
            this.lblCommands.Size = new System.Drawing.Size(62, 13);
            this.lblCommands.TabIndex = 21;
            this.lblCommands.Text = "Commands:";
            this.lblCommands.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblItems.Location = new System.Drawing.Point(436, 275);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(35, 13);
            this.lblItems.TabIndex = 146;
            this.lblItems.Text = "Items:";
            this.lblItems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtSavedAuthor
            // 
            this.txtSavedAuthor.Location = new System.Drawing.Point(295, 64);
            this.txtSavedAuthor.Name = "txtSavedAuthor";
            this.txtSavedAuthor.Size = new System.Drawing.Size(193, 20);
            this.txtSavedAuthor.TabIndex = 19;
            this.txtSavedAuthor.Text = "Author";
            // 
            // lblBots
            // 
            this.lblBots.AutoSize = true;
            this.lblBots.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblBots.Location = new System.Drawing.Point(292, 50);
            this.lblBots.Name = "lblBots";
            this.lblBots.Size = new System.Drawing.Size(83, 13);
            this.lblBots.TabIndex = 18;
            this.lblBots.Text = "Number of Bots:";
            // 
            // txtSavedAdd
            // 
            this.txtSavedAdd.Location = new System.Drawing.Point(295, 27);
            this.txtSavedAdd.Name = "txtSavedAdd";
            this.txtSavedAdd.Size = new System.Drawing.Size(104, 20);
            this.txtSavedAdd.TabIndex = 16;
            // 
            // btnSavedAdd
            // 
            this.btnSavedAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSavedAdd.BackColorUseGeneric = false;
            this.btnSavedAdd.Checked = false;
            this.btnSavedAdd.Location = new System.Drawing.Point(398, 27);
            this.btnSavedAdd.Name = "btnSavedAdd";
            this.btnSavedAdd.Size = new System.Drawing.Size(90, 20);
            this.btnSavedAdd.TabIndex = 15;
            this.btnSavedAdd.Text = "Add folder";
            this.btnSavedAdd.Click += new System.EventHandler(this.btnSavedAdd_Click);
            // 
            // txtSaved
            // 
            this.txtSaved.Location = new System.Drawing.Point(4, 4);
            this.txtSaved.Name = "txtSaved";
            this.txtSaved.Size = new System.Drawing.Size(395, 20);
            this.txtSaved.TabIndex = 13;
            this.txtSaved.TextChanged += new System.EventHandler(this.txtSaved_TextChanged);
            // 
            // btnSearchCmd
            // 
            this.btnSearchCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSearchCmd.BackColorUseGeneric = false;
            this.btnSearchCmd.Checked = false;
            this.btnSearchCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSearchCmd.Location = new System.Drawing.Point(118, 0);
            this.btnSearchCmd.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearchCmd.Name = "btnSearchCmd";
            this.btnSearchCmd.Size = new System.Drawing.Size(17, 20);
            this.btnSearchCmd.TabIndex = 147;
            this.btnSearchCmd.Text = "◀";
            this.btnSearchCmd.Click += new System.EventHandler(this.btnSearchCmd_Click);
            // 
            // txtSearchCmd
            // 
            this.txtSearchCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchCmd.Location = new System.Drawing.Point(0, 0);
            this.txtSearchCmd.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearchCmd.Name = "txtSearchCmd";
            this.txtSearchCmd.Size = new System.Drawing.Size(119, 20);
            this.txtSearchCmd.TabIndex = 146;
            this.txtSearchCmd.Text = "Search";
            this.txtSearchCmd.Click += new System.EventHandler(this.TextboxEnter);
            this.txtSearchCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSearchCmd_KeyPress);
            this.txtSearchCmd.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // colorfulCommands
            // 
            this.colorfulCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorfulCommands.Checked = true;
            this.colorfulCommands.CheckState = System.Windows.Forms.CheckState.Checked;
            this.colorfulCommands.Location = new System.Drawing.Point(3, 46);
            this.colorfulCommands.Name = "colorfulCommands";
            this.colorfulCommands.Size = new System.Drawing.Size(127, 21);
            this.colorfulCommands.TabIndex = 149;
            this.colorfulCommands.Text = "Custom Commands";
            this.colorfulCommands.CheckedChanged += new System.EventHandler(this.chkColorfulCommands_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 273);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.splitContainer1.Panel1.Controls.Add(this.colorfulCommands);
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.panel13);
            this.splitContainer1.Panel2.Controls.Add(this.panel7);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(266, 75);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 149;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer6.Location = new System.Drawing.Point(3, 0);
            this.splitContainer6.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.panel2);
            this.splitContainer6.Panel1.Controls.Add(this.panel8);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.panel9);
            this.splitContainer6.Panel2.Controls.Add(this.panel10);
            this.splitContainer6.Size = new System.Drawing.Size(128, 44);
            this.splitContainer6.SplitterDistance = 59;
            this.splitContainer6.SplitterWidth = 1;
            this.splitContainer6.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.btnDown);
            this.panel2.Location = new System.Drawing.Point(0, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(60, 20);
            this.panel2.TabIndex = 175;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnDown.BackColorUseGeneric = false;
            this.btnDown.Checked = false;
            this.btnDown.Location = new System.Drawing.Point(0, 0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(59, 20);
            this.btnDown.TabIndex = 166;
            this.btnDown.Text = "▼";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel8.Controls.Add(this.btnClear);
            this.panel8.Location = new System.Drawing.Point(0, 23);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(60, 21);
            this.panel8.TabIndex = 176;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnClear.BackColorUseGeneric = false;
            this.btnClear.Checked = false;
            this.btnClear.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnClear.Location = new System.Drawing.Point(0, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(59, 21);
            this.btnClear.TabIndex = 167;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel9.Controls.Add(this.btnRemove);
            this.panel9.Location = new System.Drawing.Point(0, 23);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(64, 21);
            this.panel9.TabIndex = 177;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnRemove.BackColorUseGeneric = false;
            this.btnRemove.Checked = false;
            this.btnRemove.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnRemove.Location = new System.Drawing.Point(0, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(64, 21);
            this.btnRemove.TabIndex = 166;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel10.Controls.Add(this.btnUp);
            this.panel10.Location = new System.Drawing.Point(0, 2);
            this.panel10.Margin = new System.Windows.Forms.Padding(0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(64, 20);
            this.panel10.TabIndex = 178;
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnUp.BackColorUseGeneric = false;
            this.btnUp.Checked = false;
            this.btnUp.Location = new System.Drawing.Point(0, 0);
            this.btnUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(64, 20);
            this.btnUp.TabIndex = 165;
            this.btnUp.Text = "▲";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.btnBotStart);
            this.panel4.Controls.Add(this.btnBotPause);
            this.panel4.Controls.Add(this.btnBotStop);
            this.panel4.Controls.Add(this.btnBotResume);
            this.panel4.Location = new System.Drawing.Point(0, 23);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(135, 21);
            this.panel4.TabIndex = 174;
            // 
            // btnBotStart
            // 
            this.btnBotStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBotStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBotStart.BackColorUseGeneric = false;
            this.btnBotStart.Checked = false;
            this.btnBotStart.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBotStart.Location = new System.Drawing.Point(0, 0);
            this.btnBotStart.Margin = new System.Windows.Forms.Padding(0);
            this.btnBotStart.Name = "btnBotStart";
            this.btnBotStart.Size = new System.Drawing.Size(135, 21);
            this.btnBotStart.TabIndex = 167;
            this.btnBotStart.Text = "Start";
            this.btnBotStart.Click += new System.EventHandler(this.btnBotStart_ClickAsync);
            // 
            // btnBotPause
            // 
            this.btnBotPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBotPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBotPause.BackColorUseGeneric = false;
            this.btnBotPause.Checked = false;
            this.btnBotPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBotPause.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBotPause.Location = new System.Drawing.Point(118, 0);
            this.btnBotPause.Margin = new System.Windows.Forms.Padding(0);
            this.btnBotPause.Name = "btnBotPause";
            this.btnBotPause.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnBotPause.Size = new System.Drawing.Size(17, 21);
            this.btnBotPause.TabIndex = 169;
            this.btnBotPause.Text = "║";
            this.btnBotPause.Visible = false;
            this.btnBotPause.Click += new System.EventHandler(this.btnBotPause_Click);
            // 
            // btnBotStop
            // 
            this.btnBotStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBotStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBotStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBotStop.BackColorUseGeneric = false;
            this.btnBotStop.Checked = false;
            this.btnBotStop.Location = new System.Drawing.Point(0, 0);
            this.btnBotStop.Name = "btnBotStop";
            this.btnBotStop.Size = new System.Drawing.Size(119, 21);
            this.btnBotStop.TabIndex = 168;
            this.btnBotStop.Text = "Stop";
            this.btnBotStop.Visible = false;
            this.btnBotStop.Click += new System.EventHandler(this.btnBotStop_Click);
            // 
            // btnBotResume
            // 
            this.btnBotResume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBotResume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBotResume.BackColorUseGeneric = false;
            this.btnBotResume.Checked = false;
            this.btnBotResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnBotResume.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBotResume.Location = new System.Drawing.Point(118, 0);
            this.btnBotResume.Margin = new System.Windows.Forms.Padding(0);
            this.btnBotResume.Name = "btnBotResume";
            this.btnBotResume.Size = new System.Drawing.Size(17, 21);
            this.btnBotResume.TabIndex = 168;
            this.btnBotResume.Text = "▶";
            this.btnBotResume.Visible = false;
            this.btnBotResume.Click += new System.EventHandler(this.btnBotResume_Click);
            // 
            // panel13
            // 
            this.panel13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel13.Controls.Add(this.cbLists);
            this.panel13.Location = new System.Drawing.Point(0, 45);
            this.panel13.Margin = new System.Windows.Forms.Padding(0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(135, 21);
            this.panel13.TabIndex = 172;
            // 
            // cbLists
            // 
            this.cbLists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLists.FormattingEnabled = true;
            this.cbLists.Items.AddRange(new object[] {
            "Commands",
            "Skills",
            "Quests",
            "Drops",
            "Boosts",
            "Items"});
            this.cbLists.Location = new System.Drawing.Point(0, 0);
            this.cbLists.Margin = new System.Windows.Forms.Padding(0);
            this.cbLists.Name = "cbLists";
            this.cbLists.Size = new System.Drawing.Size(135, 21);
            this.cbLists.TabIndex = 169;
            this.cbLists.SelectedIndexChanged += new System.EventHandler(this.cbLists_SelectedIndexChanged);
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.btnSearchCmd);
            this.panel7.Controls.Add(this.txtSearchCmd);
            this.panel7.Location = new System.Drawing.Point(0, 2);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(135, 20);
            this.panel7.TabIndex = 173;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.lstCommands);
            this.panel1.Controls.Add(this.lstSkills);
            this.panel1.Controls.Add(this.lstQuests);
            this.panel1.Controls.Add(this.lstDrops);
            this.panel1.Controls.Add(this.lstPackets);
            this.panel1.Controls.Add(this.lstBoosts);
            this.panel1.Controls.Add(this.lstItems);
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.ForeColor = System.Drawing.Color.Gainsboro;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 342);
            this.panel1.TabIndex = 150;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(10);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            this.splitContainer2.Panel1MinSize = 30;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.mainTabControl);
            this.splitContainer2.Size = new System.Drawing.Size(798, 344);
            this.splitContainer2.SplitterDistance = 270;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 150;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(150, 184);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 158;
            this.checkBox1.Text = "Placeholder";
            // 
            // chkBuffup
            // 
            this.chkBuffup.Location = new System.Drawing.Point(0, 0);
            this.chkBuffup.Name = "chkBuffup";
            this.chkBuffup.Size = new System.Drawing.Size(104, 24);
            this.chkBuffup.TabIndex = 0;
            // 
            // BotManagerMenuStrip
            // 
            this.BotManagerMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.BotManagerMenuStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.BotManagerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFontsToolStripMenuItem,
            this.commandColorsToolStripMenuItem,
            this.multilineToggleToolStripMenuItem,
            this.toggleTabpagesToolStripMenuItem,
            this.clearAllCommandListsToolStripMenuItem});
            this.BotManagerMenuStrip.Name = "contextMenuStrip1";
            this.BotManagerMenuStrip.Size = new System.Drawing.Size(198, 114);
            // 
            // changeFontsToolStripMenuItem
            // 
            this.changeFontsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.changeFontsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.changeFontsToolStripMenuItem.Name = "changeFontsToolStripMenuItem";
            this.changeFontsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.changeFontsToolStripMenuItem.Text = "Change fonts";
            this.changeFontsToolStripMenuItem.Click += new System.EventHandler(this.changeFontsToolStripMenuItem_Click);
            // 
            // commandColorsToolStripMenuItem
            // 
            this.commandColorsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.commandColorsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.commandColorsToolStripMenuItem.Name = "commandColorsToolStripMenuItem";
            this.commandColorsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.commandColorsToolStripMenuItem.Text = "Command customizer";
            this.commandColorsToolStripMenuItem.Click += new System.EventHandler(this.commandColorsToolStripMenuItem_Click);
            // 
            // multilineToggleToolStripMenuItem
            // 
            this.multilineToggleToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.multilineToggleToolStripMenuItem.CheckOnClick = true;
            this.multilineToggleToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.multilineToggleToolStripMenuItem.Name = "multilineToggleToolStripMenuItem";
            this.multilineToggleToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.multilineToggleToolStripMenuItem.Text = "Multiline toggle";
            this.multilineToggleToolStripMenuItem.Click += new System.EventHandler(this.multilineToggleToolStripMenuItem_Click);
            // 
            // toggleTabpagesToolStripMenuItem
            // 
            this.toggleTabpagesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.toggleTabpagesToolStripMenuItem.CheckOnClick = true;
            this.toggleTabpagesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toggleTabpagesToolStripMenuItem.Name = "toggleTabpagesToolStripMenuItem";
            this.toggleTabpagesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.toggleTabpagesToolStripMenuItem.Text = "Tabpages toggle";
            this.toggleTabpagesToolStripMenuItem.Click += new System.EventHandler(this.toggleTabpagesToolStripMenuItem_Click);
            // 
            // clearAllCommandListsToolStripMenuItem
            // 
            this.clearAllCommandListsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.clearAllCommandListsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.clearAllCommandListsToolStripMenuItem.Name = "clearAllCommandListsToolStripMenuItem";
            this.clearAllCommandListsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.clearAllCommandListsToolStripMenuItem.Text = "Clear all command lists";
            this.clearAllCommandListsToolStripMenuItem.Click += new System.EventHandler(this.clearAllCommandLists_Click);
            // 
            // darkButton2
            // 
            this.darkButton2.Checked = false;
            this.darkButton2.Location = new System.Drawing.Point(0, 0);
            this.darkButton2.Name = "darkButton2";
            this.darkButton2.Size = new System.Drawing.Size(75, 23);
            this.darkButton2.TabIndex = 170;
            // 
            // BotManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(795, 342);
            this.ContextMenuStrip = this.BotManagerMenuStrip;
            this.Controls.Add(this.splitContainer2);
            this.FlatBorder = true;
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "BotManager";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bot Manager";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BotManager_FormClosing);
            this.Load += new System.EventHandler(this.BotManager_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSearchCmd_KeyDown);
            this.mainTabControl.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabCombat.ResumeLayout(false);
            this.tabCombat.PerformLayout();
            this.darkGroupBox30.ResumeLayout(false);
            this.darkGroupBox30.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkillCmdHPMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSkillCmd)).EndInit();
            this.darkGroupBox29.ResumeLayout(false);
            this.darkGroupBox29.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSafe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSkillD)).EndInit();
            this.darkGroupBox28.ResumeLayout(false);
            this.darkGroupBox27.ResumeLayout(false);
            this.darkGroupBox27.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRestMP)).EndInit();
            this.darkGroupBox26.ResumeLayout(false);
            this.darkGroupBox26.PerformLayout();
            this.darkGroupBox25.ResumeLayout(false);
            this.darkGroupBox25.PerformLayout();
            this.tabItem.ResumeLayout(false);
            this.tabItem.PerformLayout();
            this.darkGroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMapItem)).EndInit();
            this.darkGroupBox4.ResumeLayout(false);
            this.darkGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDropDelay)).EndInit();
            this.darkGroupBox3.ResumeLayout(false);
            this.darkGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numShopId)).EndInit();
            this.darkGroupBox2.ResumeLayout(false);
            this.tabMap.ResumeLayout(false);
            this.darkGroupBox18.ResumeLayout(false);
            this.darkGroupBox18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkY)).EndInit();
            this.darkGroupBox1.ResumeLayout(false);
            this.darkGroupBox1.PerformLayout();
            this.tabQuest.ResumeLayout(false);
            this.darkGroupBox20.ResumeLayout(false);
            this.darkGroupBox20.PerformLayout();
            this.darkGroupBox21.ResumeLayout(false);
            this.darkGroupBox21.PerformLayout();
            this.tabMisc.ResumeLayout(false);
            this.darkGroupBox24.ResumeLayout(false);
            this.darkGroupBox24.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPacketDelay)).EndInit();
            this.darkGroupBox23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSetFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeepTimes)).EndInit();
            this.darkGroupBox22.ResumeLayout(false);
            this.darkGroupBox22.PerformLayout();
            this.darkGroupBox11.ResumeLayout(false);
            this.darkGroupBox11.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.darkGroupBox14.ResumeLayout(false);
            this.darkGroupBox14.PerformLayout();
            this.darkGroupBox12.ResumeLayout(false);
            this.darkGroupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSetInt)).EndInit();
            this.darkGroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numIndexCmd)).EndInit();
            this.darkGroupBox7.ResumeLayout(false);
            this.darkGroupBox7.PerformLayout();
            this.tabMisc2.ResumeLayout(false);
            this.tabMisc2.PerformLayout();
            this.darkGroupBox13.ResumeLayout(false);
            this.darkGroupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBotDelay)).EndInit();
            this.darkGroupBox10.ResumeLayout(false);
            this.darkGroupBox10.PerformLayout();
            this.darkGroupBox9.ResumeLayout(false);
            this.darkGroupBox9.PerformLayout();
            this.grpPacketlist.ResumeLayout(false);
            this.grpPacketlist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPacketlistDelay)).EndInit();
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.darkGroupBox19.ResumeLayout(false);
            this.darkGroupBox19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOptionsTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkSpeed)).EndInit();
            this.darkGroupBox6.ResumeLayout(false);
            this.darkGroupBox6.PerformLayout();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).EndInit();
            this.tabOptions2.ResumeLayout(false);
            this.tabOptions2.PerformLayout();
            this.grpMapSwf.ResumeLayout(false);
            this.grpMapSwf.PerformLayout();
            this.darkGroupBox17.ResumeLayout(false);
            this.darkGroupBox17.PerformLayout();
            this.darkGroupBox16.ResumeLayout(false);
            this.darkGroupBox15.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpAccessLevel.ResumeLayout(false);
            this.grpAlignment.ResumeLayout(false);
            this.tabBots.ResumeLayout(false);
            this.tabBots.PerformLayout();
            this.darkPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.BotManagerMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        static BotManager()
        {
            Instance = new BotManager();
            Log = new LogForm();
        }

        private void btnBuyFast_Click(object sender, EventArgs e)
        {
            if (txtShopItem.TextLength > 0)
            {
                AddCommand(new CmdBuyFast
                {
                    ItemName = txtShopItem.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnLoadShop_Click(object sender, EventArgs e)
        {
            int id = (int)numShopId.Value;
            AddCommand(new CmdLoad2
            {
                ShopId = id.ToString()
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        public void changeGenderAsync(object sender, EventArgs e)
        {
            int num = Flash.Call<int>("UserID", new string[0]);
            string text = Flash.Call<string>("Gender", new string[0]);
            text = (!text.Contains("M")) ? "M" : "F";
            string data = $"{{\"t\":\"xt\",\"b\":{{\"r\":-1,\"o\":{{\"cmd\":\"genderSwap\",\"bitSuccess\":1,\"gender\":\"{text}\",\"intCoins\":0,\"uid\":\"{num}\",\"strHairFileName\":\"\",\"HairID\":\"\",\"strHairName\":\"\"}}}}}}";
            _ = Proxy.Instance.SendToClient(data);
        }

        private void logScript(object sender, EventArgs e)
        {
            AddCommand(new CmdLog
            {
                Text = txtLog.Text
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void logDebug(object sender, EventArgs e)
        {
            AddCommand(new CmdLog
            {
                Text = txtLog.Text,
                Debug = true
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogForm.Instance.Show();
            LogForm.Instance.BringToFront();
        }

        private void btnYulgar_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdYulgar(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnProvoke_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdToggleProvoke
            {
                Type = 2
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnchangeName_Click(object sender, EventArgs e)
        {
            CustomName = txtUsername.Text;
        }

        private void btnchangeGuild_Click(object sender, EventArgs e)
        {
            CustomGuild = txtGuild.Text;
        }

        private void btnProvokeOn_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdToggleProvoke
            {
                Type = 1
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnProvokeOff_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdToggleProvoke
            {
                Type = 0
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        public void AddItem(string Name)
        {
            if (!lstItems.Items.Contains(Name))
            {
                lstItems.Items.Add(Name);
            }
        }

        private void btnUnbanklst_Click(object sender, EventArgs e)
        {
            string text = txtWhitelist.Text;
            if (text.Length > 0)
            {
                AddItem(text);
            }
        }

        private void lstLogText_KeyDown(object sender, KeyEventArgs e)
        {
            bool flag = e.Control && e.KeyCode == Keys.C;
            if (flag)
            {
                string data = this.lstLogText.SelectedItem.ToString();
                Clipboard.SetData(DataFormats.StringFormat, data);
            }
        }

        private void numOptionsTimer_ValueChanged(object sender, EventArgs e)
        {
            OptionsManager.Timer = (int)numOptionsTimer.Value;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabBots)
            {
                Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                this.txtSaved.Text = Path.Combine(Application.StartupPath, "Bots");
                string botsDirectory;
                try
                {
                    botsDirectory = c.Get("botsDirectory");
                    if (!string.IsNullOrEmpty(botsDirectory))
                    {
                        this.txtSaved.Text = botsDirectory;
                    }
                }
                catch { }
                UpdateTree();
            }
            else if (e.TabPage == tabMisc)
            {
                GetAllCommands<CmdLabel>(lbLabels);
            }
        }

        private void txtSaved_TextChanged(object sender, EventArgs e)
        {
            UpdateTree();
        }

        private void lstLogText_DoubleClick(object sender, EventArgs e)
        {
            string data = this.txtLog.Text == "Logs" ? "" : txtLog.Text + " ";
            string data2 = this.lstLogText.SelectedItem.ToString();
            this.txtLog.Text = $"{data}{data2}";
        }

        public void MultiMode()
        {
            if (this.lstCommands.SelectionMode != SelectionMode.MultiExtended)
            {
                this.lstCommands.SelectionMode = SelectionMode.MultiExtended;
                this.lstItems.SelectionMode = SelectionMode.MultiExtended;
                this.lstSkills.SelectionMode = SelectionMode.MultiExtended;
                this.lstQuests.SelectionMode = SelectionMode.MultiExtended;
                this.lstDrops.SelectionMode = SelectionMode.MultiExtended;
                this.lstBoosts.SelectionMode = SelectionMode.MultiExtended;
            }
            else
            {
                this.lstCommands.SelectionMode = SelectionMode.One;
                this.lstItems.SelectionMode = SelectionMode.One;
                this.lstSkills.SelectionMode = SelectionMode.One;
                this.lstQuests.SelectionMode = SelectionMode.One;
                this.lstDrops.SelectionMode = SelectionMode.One;
                this.lstBoosts.SelectionMode = SelectionMode.One;
            }
        }

        private void chkUntarget_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.Untarget = chkUntarget.Checked;
        }

        private void lstCommands_DragDrop(object sender, DragEventArgs e)
        {
            Configuration config;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1) return;
            if (TryDeserialize(File.ReadAllText(files[0]), out config))
            {
                ApplyConfiguration(config);
                GetAllCommands<CmdLabel>(lbLabels);
            }
        }

        private void lstCommands_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void lstCommands_Click(object sender, EventArgs e)
        {}

        private void lstCommands_MouseEnter(object sender, EventArgs e)
        {
            if (lstCommands.Items.Count <= 0)
            {
                Color c1 = lstCommands.BackColor;
                Color c2 = Color.FromArgb(c1.A, (int)(c1.R * 0.8), (int)(c1.G * 0.8), (int)(c1.B * 0.8));
                lstCommands.BackColor = c2;
            }
        }

        private void lstCommands_MouseLeave(object sender, EventArgs e)
        {
            Color lstCommandsBackColor = Color.FromArgb(28, 32, 40);
            lstCommands.BackColor = lstCommandsBackColor;
        }

        private void btnBlank_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdBlank3 { Text = "", Alpha = 0, R = 220, G = 220, B = 220 }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkAFK_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.AFK = chkAFK.Checked;
        }

        private void chkRelog_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAFK.Enabled = chkRelog.Checked;
        }

        private void btnCurrBlank_Click(object sender, EventArgs e)
        {
            this.txtJoinCell.Text = "Blank";
            this.txtJoinPad.Text = "Spawn";
            this.txtCell.Text = "Blank";
            this.txtPad.Text = "Spawn";
        }
        private void btnSetSpawn_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdSetSpawn(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnBeep_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdBeep
            {
                Times = (int)numBeepTimes.Value
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnAddSkillCmd_Click(object sender, EventArgs e)
        {
            string monster = string.IsNullOrEmpty(txtMonsterSkillCmd.Text) ? "*" : txtMonsterSkillCmd.Text;
            if (txtMonsterSkillCmd.Text == "Monster (* = any)")
            {
                monster = "*";
            }
            string text = numSkillCmd.Text;
            AddCommand(new CmdUseSkill2
            {
                Monster = monster,
                Index = text,
                SafeHp = (int)this.numSkillCmdHPMP.Value,
                SafeMp = (int)this.numSkillCmdHPMP.Value,
                Wait = this.chkSkillCmdWait.Checked,
                Skill = text + ": " + Skill.GetSkillName(text)
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnSetHero_Click(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            switch (s.Name)
            {
                case "btnSetGood":
                    Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%1%");
                    break;
                case "btnSetEvil":
                    Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%2%");
                    break;
                case "btnSetChaos":
                    Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%3%");
                    break;
                case "btnSetUndecided":
                    Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%5%");
                    break;
                case "btnSetMem":
                    Player.ChangeAccessLevel("Member");
                    break;
                case "btnSetNonMem":
                    Player.ChangeAccessLevel("Non Member");
                    break;
                case "btnSetModerator":
                    Player.ChangeAccessLevel("Moderator");
                    break;
            }
        }

        private void chkToggleMute_CheckedChanged(object sender, EventArgs e)
        {
            Player.ToggleMute(chkToggleMute.Checked);
        }

        private void changeFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fdlg = new FontDialog();
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                this.Font = new Font(fdlg.Font.FontFamily, fdlg.Font.Size, FontStyle.Regular, GraphicsUnit.Point, 0);
                foreach (Control control in this.Controls)
                {
                    control.Font = new Font(fdlg.Font.FontFamily, fdlg.Font.Size, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                var selectedOption = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Would you like to save style and have it load on the next startup?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (selectedOption == DialogResult.Yes)
                {
                    Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                    c.Set("font", fdlg.Font.FontFamily.Name.ToString());
                    c.Set("fontSize", fdlg.Font.Size.ToString());
                    c.Save();
                }
            }
        }

        private void numDropDelay_ValueChanged(object sender, EventArgs e)
        {
            Bot.Instance.DropDelay = (int)numDropDelay.Value;
        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            string monster = string.IsNullOrEmpty(txtMonster.Text) ? "*" : txtMonster.Text;
            AddCommand(new CmdAttack
            {
                Monster = txtMonster.Text == "Monster (* = any)" ? "*" : txtMonster.Text
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkBankOnStop_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.Instance.BankOnStop = chkBankOnStop.Checked;
        }

        private void btnGotoIndex_Click(object sender, EventArgs e)
        {
            IBotCommand cmd;
            switch (((Button)sender).Text)
            {
                case "++":
                    cmd = new CmdIndex
                    {
                        Type = CmdIndex.IndexCommand.Up,
                        Index = (int)numIndexCmd.Value
                    };
                    break;
                case "--":
                    cmd = new CmdIndex
                    {
                        Type = CmdIndex.IndexCommand.Down,
                        Index = (int)numIndexCmd.Value
                    };
                    break;
                default:
                    cmd = new CmdIndex
                    {
                        Type = CmdIndex.IndexCommand.Goto,
                        Index = (int)numIndexCmd.Value
                    };
                    break;
            }
            AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
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

        public Dictionary<string, Color> CurrentColors = new Dictionary<string, Color>();

        public Color GetCurrentColor(string cmd)
        {
            if (!CurrentColors.ContainsKey(cmd))
                CurrentColors[cmd] = GetColor(cmd);
            return CurrentColors[cmd];
        }

        public Color GetColor(string name)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            return Color.FromArgb(int.Parse(c.Get(name + "Color") ?? "FFDCDCDC", System.Globalization.NumberStyles.HexNumber));
        }

        public Dictionary<string, bool> CurrentCentered = new Dictionary<string, bool>();

        private bool GetCurrentBoolCentered(string cmd)
        {
            if (!CurrentCentered.ContainsKey(cmd))
                CurrentCentered[cmd] = GetBoolCentered(cmd);
            return CurrentCentered[cmd];
        }

        private bool GetBoolCentered(string name)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            return bool.Parse(c.Get(name + "Centered") ?? "false");
        }

        private void lstCommands_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (!(e.Index > -1))
                return;
            e.DrawBackground();
            if (!colorfulCommands.Checked)
            {
                // Define the default color of the brush as black.
                Brush myBrush = Brushes.Gainsboro;
                // Draw the current item text based on the current Font 
                // and the custom brush settings.
                e.Graphics.DrawString(lstCommands.Items[e.Index].ToString(),
                    e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                // If the ListBox has focus, draw a focus rectangle around the selected item.
                e.DrawFocusRectangle();
                return;
            }

            #region Settings
            IBotCommand cmd = (IBotCommand)lstCommands.Items[e.Index];
            string[] count = cmd.GetType().ToString().Split('.');
            string scmd = count[count.Count() - 1].Replace("Cmd", "");
            //string WindowText = SystemColors.WindowText.ToArgb().ToString();
            SolidBrush color = new SolidBrush(GetCurrentColor(scmd));
            SolidBrush varColor = new SolidBrush(GetCurrentColor("Variable"));
            SolidBrush eVarColor = new SolidBrush(GetCurrentColor("ExtendedVariable"));
            SolidBrush indexcolor = new SolidBrush(GetCurrentColor("Index"));
            RectangleF region = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height);
            Font font = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Regular);
            StringFormat centered = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            #endregion

            string[] LabelLess = new string[]
            {
                "Label",
                "GotoLabel",
                "Blank",
                "Blank2",
                "StatementCommand",
                "Index",
                "SkillSet"
            };

            if (cmd is CmdBlank || cmd is CmdBlank2 || cmd is CmdBlank3)
            {
                string txt = lstCommands.Items[e.Index].ToString();
                Font cmdFont = new Font("Arial", e.Font.Size + (float)6.5, FontStyle.Bold, GraphicsUnit.Pixel);
                if (cmd is CmdBlank2 && txt.Contains("[RGB]"))
                    using (Font the_font = new Font("Times New Roman", e.Font.Size + (float)6.5, FontStyle.Bold, GraphicsUnit.Pixel))
                    {
                        var font_info = new FontInfo(e.Graphics, the_font);
                        SizeF text_size = e.Graphics.MeasureString(txt, the_font);
                        int x0 = (int)((this.ClientSize.Width - text_size.Width) / 2);
                        int y0 = (int)((this.ClientSize.Height - text_size.Height) / 2);
                        int brush_y0 = (int)(y0 + font_info.InternalLeadingPixels) + (int)font_info.InternalLeadingPixels;
                        int brush_y1 = (int)(y0 + font_info.AscentPixels) + 5;
                        using (LinearGradientBrush rainbowbrush = new LinearGradientBrush(new Point(x0, brush_y0), new Point(x0, brush_y1), Color.Red, Color.Violet))
                        {
                            Color[] colors = new Color[]
                            {
                                Color.FromArgb(255, 0, 0),
                                Color.FromArgb(255, 0, 0),
                                Color.FromArgb(255, 128, 0),
                                Color.FromArgb(255, 255, 0),
                                Color.FromArgb(0, 255, 0),
                                Color.FromArgb(0, 255, 128),
                                Color.FromArgb(0, 255, 255),
                                Color.FromArgb(0, 128, 255),
                                Color.FromArgb(0, 0, 255),
                                Color.FromArgb(0, 0, 255),
                            };
                            int num_colors = colors.Length;
                            float[] blend_positions = new float[num_colors];
                            for (int i = 0; i < num_colors; i++)
                                blend_positions[i] = i / (num_colors - 1f);
                            ColorBlend color_blend = new ColorBlend
                            {
                                Colors = colors,
                                Positions = blend_positions
                            };
                            rainbowbrush.InterpolationColors = color_blend;

                            // Draw the text.
                            txt = txt.Replace("[RGB]", "");
                            e.Graphics.DrawString(txt, the_font, rainbowbrush, e.Bounds, centered);
                        }
                    }
                else if (cmd is CmdBlank2 && txt.StartsWith("["))
                {
                    try
                    {
                        string[] rgbarray = txt.Replace("[", "").Split(']')[0].Split(',');
                        SolidBrush b2b = new SolidBrush(Color.Black);
                        if (rgbarray.Length == 3)
                            b2b = new SolidBrush(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2])));
                        else if (rgbarray.Length == 4)
                            b2b = new SolidBrush(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2]), int.Parse(rgbarray[3])));
                        txt = Regex.Replace(txt, @"\[.*?\]", "");
                        if (txt.Contains("(TROLL)"))
                            e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, b2b, e.Bounds, StringFormat.GenericDefault);
                        else
                            e.Graphics.DrawString(txt, cmdFont, b2b, e.Bounds, centered);
                    }
                    catch { }
                }
                else if (cmd is CmdBlank3)
                {
                    var jsonObj = JsonConvert.DeserializeObject<CmdBlank3>(JsonConvert.SerializeObject(cmd));
                    try
                    {
                        SolidBrush colorBrush = new SolidBrush(jsonObj.Argb());
                        e.Graphics.DrawString(txt, cmdFont, colorBrush, e.Bounds, centered);
                    }
                    catch { }
                }
                else if (txt.Contains("(TROLL)"))
                    e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
                return;
            }

            if (!LabelLess.Contains(scmd))
            {
                //Draw Index
                //Region first = DrawString(e.Graphics, $"[{e.Index.ToString()}] ", this.Font, indexcolor, region, new StringFormat(StringFormatFlags.DirectionRightToLeft));
                Region first = DrawString(e.Graphics, $"[{e.Index.ToString()}] ", font, indexcolor, region, StringFormat.GenericDefault);
                // Adjust the region we wish to print with a +3 offset.
                region = new RectangleF(region.X + first.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
            }

            if (GetBoolCentered(scmd))
            {
                e.Graphics.DrawString(lstCommands.Items[e.Index].ToString(), font, color, e.Bounds, centered);
                return;
            }

            // Draw the second string (rest of the string, in this case Command type).
            string cmdText = lstCommands.Items[e.Index].ToString();
            string[] toDraw;
            if (cmdText.Contains(':'))
            {
                toDraw = lstCommands.Items[e.Index].ToString().Split(':');
                Region second = DrawString(e.Graphics, toDraw[0], font, color, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
                region = new RectangleF(region.X + second.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
                if (toDraw[1].Contains(","))
                {
                    toDraw = toDraw[1].Split(',');
                    Region third = DrawString(e.Graphics, toDraw[0], font, varColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
                    for (int i = 1; i < toDraw.Length; i++)
                    {
                        region = new RectangleF(region.X + third.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
                        third = DrawString(e.Graphics, toDraw[i], font, eVarColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
                    }
                }
                else
                    DrawString(e.Graphics, toDraw[1], font, varColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
            }
            else
                DrawString(e.Graphics, cmdText, font, color, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
        }

        private Region DrawString(Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            format.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, s.Length) });
            format.Alignment = StringAlignment.Near;
            g.DrawString(s, font, brush, layoutRectangle, format);
            return g.MeasureCharacterRanges(s, font, layoutRectangle, format)[0];
        }

        private Region DrawRTLString(Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            var format = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };
            format.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, s.Length) });
            Region length = g.MeasureCharacterRanges(s, font, layoutRectangle, format)[0];
            layoutRectangle = new RectangleF(layoutRectangle.Width, layoutRectangle.Y, length.GetBounds(g).Width, layoutRectangle.Height);
            DrawString(g, s, font, brush, layoutRectangle, format);
            return length;
        }

        private void multilineToggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MultiMode();
        }

        private void toggleTabpagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TogglePages();
        }

        private void commandColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Root.Instance.ShowForm(CommandColorForm.Instance);
        }

        private void btnChangeCmd_Click(object sender, EventArgs e)
        {
            IBotCommand cmd;
            switch (((Button)sender).Name)
            {
                case "btnChangeGuildCmd":
                    cmd = new CmdChange
                    {
                        Guild = true,
                        Text = txtGuild.Text
                    };
                    break;
                default:
                    cmd = new CmdChange
                    {
                        Guild = false,
                        Text = txtUsername.Text
                    };
                    break;
            }
            AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkAntiAfk_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager._antiAfk = chkAntiAfk.Checked;
            OptionsManager.AntiAfk();
        }

        private void treeBots_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void chkChangeRoomTag_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.HideRoom = chkChangeRoomTag.Checked;
            if (OptionsManager.HideRoom)
            {
                OptionsManager.RoomNumber = World.RoomNumber;
                BotClientConfig c = BotClientConfig.Load(System.Windows.Forms.Application.StartupPath + "\\BotClientConfig.cfg");
                Flash.Call("ChangeAreaName", c.Get("areaName") ?? "discord.io/aqwbots");
            }
            else
            {
                Flash.Call("ChangeAreaName", $"{Player.Map}-{OptionsManager.RoomNumber}");
            }
        }

        private void chkChangeChat_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.ChangeChat = chkChangeChat.Checked;
            if (OptionsManager.ChangeChat)
            {
                Flash.Call("ChangeName", "You");
            }
            else
            {
                Flash.Call("ChangeName", Player.Username);
            }
        }

        private void btnSetJoinLevel_Click(object sender, EventArgs e)
        {
            Task.Run(() => OptionsManager.SetClientLevel());
        }

        private void btnSetClientLevel_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdSetClientLevel(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnAddClientGold_Click(object sender, EventArgs e)
        {
            BotClientConfig c = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");
            string packetClientGold = c.Get("packetClientGold");
            Proxy.Instance.SendToClient(packetClientGold);
        }
        private void btnAddClientACs_Click(object sender, EventArgs e)
        {
            BotClientConfig c = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");
            string packetClientACs = c.Get("packetClientACs");
            Proxy.Instance.SendToClient(packetClientACs);
        }

        private void btnClientPacket_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdPacket2
            {
                Packet = txtPacket.Text,
                Delay = (int)numPacketDelay.Value,
                Client = true
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkHideYulgarPlayers_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.HideYulgar = chkHideYulgarPlayers.Checked;
        }

        private void btnSetInt_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdInt
            {
                Int = txtSetInt.Text,
                Value = (int)numSetInt.Value,
                type = CmdInt.Types.Set
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnIncreaseInt_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdInt
            {
                Int = txtSetInt.Text,
                Value = (int)numSetInt.Value,
                type = CmdInt.Types.Upper
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnDecreaseInt_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdInt
            {
                Int = txtSetInt.Text,
                Value = (int)numSetInt.Value,
                type = CmdInt.Types.Lower
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void lstCommands_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnWhitelistOn_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWhitelist
            {
                State = CmdWhitelist.state.On
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWhitelistOff_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWhitelist
            {
                State = CmdWhitelist.state.Off
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWhitelistClear_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWhitelist
            {
                State = CmdWhitelist.state.Clear
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        //Search Command Function
        public int LastIndexedSearch = 0;
        public string SKeyword = "";
        public List<int> Filtered = new List<int>();

        private void btnSearchCmd_Click(object sender, EventArgs e)
        {
            string Keyword = this.txtSearchCmd.Text.ToLower();
            ListBox.ObjectCollection lists = SelectedList.Items;
            if (Keyword != SKeyword)
            {
                SKeyword = "";
                LastIndexedSearch = 0;
                Filtered.Clear();
            }
            for (int i = 0; i < lists.Count; i++)
            {
                if (Keyword == SKeyword)
                {
                    Console.WriteLine("Using Cached List.");
                    break;
                }
                if (lists[i].ToString().ToLower().Contains($@"{Keyword}"))
                {
                    Filtered.Add(i);
                }
            }
            SKeyword = Keyword;
            if (Filtered.Count > 0)
            {
                SelectedList.SelectedIndex = -1;
                SelectedList.SelectedIndex = Filtered[LastIndexedSearch];
                SelectedList.TopIndex = Filtered[LastIndexedSearch];
                LastIndexedSearch++;
                if (LastIndexedSearch >= Filtered.Count)
                {
                    LastIndexedSearch = 0;
                }
            }
        }

        private void btnClientMessageEvt(object sender, EventArgs e)
        {
            IBotCommand cmd;
            switch (((Button)sender).Name)
            {
                case "btnAddWarnMsg":
                    cmd = new CmdClientMessage
                    {
                        IsWarning = true,
                        Messages = (string)inputMsgClient.Text
                    };
                    break;
                case "btnAddInfoMsg":
                    cmd = new CmdClientMessage
                    {
                        Messages = (string)inputMsgClient.Text
                    };
                    break;
                default:
                    cmd = new CmdClientMessage
                    {
                        Messages = (string)inputMsgClient.Text
                    };
                    break;
            };

            AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnReturnCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdReturn(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnClearTempVar_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdClearTemp(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnSetFPSCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdSetFPS
            {
                FPS = (int)numSetFPS.Value
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void lbLabels_DoubleClick(object sender, EventArgs e)
        {
            object selected = lbLabels.SelectedItem;
            if (selected != null)
                txtLabel.Text = selected.ToString().Substring(1, selected.ToString().Length - 2);
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rtbInfo.Rtf = richTextBox2.Text;
                return;

                var currDir = System.AppDomain.CurrentDomain.BaseDirectory;
                var tempDoc = $@"{currDir}/tempdoc";
                var target = $@"{currDir}/temprtf";
                if (File.Exists(tempDoc))
                {
                    File.Delete(tempDoc);
                }
                using (FileStream newFile = File.Create(tempDoc))
                {
                    byte[] toWrite = new UTF8Encoding(true).GetBytes(richTextBox2.Text);
                    newFile.Write(toWrite, 0, toWrite.Length);
                }
                var tempRtf = "";
                Task.WaitAll(Task.Run(() => DocConvert.toRtf(tempDoc, target, out tempRtf)));
                rtbInfo.Rtf = File.ReadAllText(tempRtf);
            }
            catch { };
        }

        private void rtbInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void btnCancelTargetCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdCancelTarget(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnCancelAutoAttackCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdCancelAutoAttack(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnBuyItemByID_Click(object sender, EventArgs e)
        {
            if ((txtItemID.TextLength > 0) && (txtShopID.TextLength > 0) && (txtShopItemID.TextLength > 0))
            {
                AddCommand(new CmdBuyByID
                {
                    ItemID = txtItemID.Text,
                    ShopID = txtShopID.Text,
                    ShopItemID = txtShopItemID.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnBuyFastByID_Click(object sender, EventArgs e)
        {
            if ((txtItemID.TextLength > 0) && (txtShopID.TextLength > 0) && (txtShopItemID.TextLength > 0))
            {
                AddCommand(new CmdBuyFastByID
                {
                    ItemID = txtItemID.Text,
                    ShopID = txtShopID.Text,
                    ShopItemID = txtShopItemID.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnStopBotWithMessageCmd_Click(object sender, EventArgs e)
        {
            if (txtStopBotMessage.TextLength > 0)
            {
                AddCommand(new CmdStopBotWithMessage
                {
                    Message = txtStopBotMessage.Text,
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnSetCustomClassName_Click(object sender, EventArgs e)
        {
            CustomClassName = txtClassName.Text;
        }

        private void btnProvokeInMapOn_Click(object sender, EventArgs e)
        {
            string txtProvokePacket;
            if (chkInMapCustom.Checked == true || !txtCustomAggromon.Text.Contains("MonMapID"))
                txtProvokePacket = txtCustomAggromon.Text;
            else
                txtProvokePacket = "%xt%zm%aggroMon%1%1%2%3%4%5%6%7%8%9%10%11%12%13%14%15%16%17%18%19%20%21%22%23%24%25%26%27%28%29%30%" +
                    "31%32%33%34%35%36%37%38%39%40%41%42%43%44%45%46%47%48%49%50%51%52%53%54%55%56%57%58%59%60%61%62%63%64%65%66%67%68%69%70%";
            AddCommand(new CmdToggleProvokeInMap
            {
                Type = 1,
                ProvokePacket = txtProvokePacket
            }, (ModifierKeys & Keys.Control) == Keys.Control); ;
        }

        private void btnProvokeInMapOff_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdToggleProvokeInMap
            {
                Type = 0
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkInMapCustom_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomAggromon.Enabled = chkInMapCustom.Checked;
        }

        private void chkSafeHP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSafeMP.Checked)
                chkSafeMP.Checked = !chkSafeHP.Checked;
        }

        private void chkSafeMP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSafeHP.Checked)
                chkSafeHP.Checked = !chkSafeMP.Checked;
        }

        private void chkSafeGreaterThan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSafeLessThan.Checked)
                chkSafeLessThan.Checked = !chkSafeGreaterThan.Checked;
        }

        private void chkSafeLessThan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSafeGreaterThan.Checked)
                chkSafeGreaterThan.Checked = !chkSafeLessThan.Checked;
        }

        private void btnQuestlistAddCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdQuestlist
            {
                QuestID = txtQuestID.Text,
                ItemID = chkQuestlistItemID.Checked ? txtQuestItemID.Text : null,
                state = CmdQuestlist.State.Add
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnQuestlistRemoveCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdQuestlist
            {
                QuestID = txtQuestID.Text,
                ItemID = chkQuestlistItemID.Checked ? txtQuestItemID.Text : null,
                state = CmdQuestlist.State.Remove
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void txtQuestItemID_CheckedChanged(object sender, EventArgs e)
        {
            txtQuestItemID.Enabled = chkQuestlistItemID.Checked;
        }

        private void btnPacketlist_Click(object sender, EventArgs e)
        {
            IBotCommand cmd;
            string packet = txtPacketlist.Text;
            int delay = (int)numPacketlistDelay.Value;
            switch (((Button)sender).Name)
            {
                case "btnPacketlistAddCmd":
                    cmd = new CmdPacketlist
                    {
                        State = CmdPacketlist.state.Add,
                        Packet = packet
                    };
                    break;
                case "btnPacketlistRemoveCmd":
                    cmd = new CmdPacketlist
                    {
                        State = CmdPacketlist.state.Remove,
                        Packet = packet
                    };
                    break;
                case "btnPacketlistOnCmd":
                    cmd = new CmdPacketlist
                    {
                        State = CmdPacketlist.state.On
                    };
                    break;
                case "btnPacketlistOffCmd":
                    cmd = new CmdPacketlist
                    {
                        State = CmdPacketlist.state.Off
                    };
                    break;
                case "btnPacketlistClearCmd":
                    cmd = new CmdPacketlist
                    {
                        State = CmdPacketlist.state.Clear
                    };
                    break;
                case "btnPacketlistSetDelayCmd":
                    cmd = new CmdPacketlist
                    {
                        State = CmdPacketlist.state.SetDelay,
                        Delay = delay
                    };
                    break;
                default:
                    cmd = new CmdPacketlist
                    {
                    };
                    break;
            }
            AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnPacketlistAdd_Click(object sender, EventArgs e)
        {
            string text = txtPacketlist.Text;
            if (text.Length > 0)
            {
                AddPacket(text);
            }
        }

        private void btnPacketSpamOn_Click(object sender, EventArgs e)
        {
            string txtPacketSpam = txtPacket.Text;
            AddCommand(new CmdPacketlist2
            {
                Packet = txtPacketSpam,
                Delay = (int)numPacketDelay.Value,
                Type = 1
            }, (ModifierKeys & Keys.Control) == Keys.Control); ;
        }

        private void btnPacketSpamOff_Click(object sender, EventArgs e)
        {
            string txtPacketSpam = txtPacket.Text;
            AddCommand(new CmdPacketlist2
            {
                Type = 0
            }, (ModifierKeys & Keys.Control) == Keys.Control); ;
        }

        private void btnSearchCmd_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchCmd_Click(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void btnSearchCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.F)
            {
                txtSearchCmd.Focus();
                txtSearchCmd.Clear();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void btnLoadMapSwfCmd_Click(object sender, EventArgs e)
        {
            string swf = txtMapSwf.Text;
            if (txtMapSwf.Text.ToLower().Contains("oldmobius"))
                swf = "ChiralValley/town-Mobius-21Feb14.swf";
            AddCommand(new CmdLoadMapSwf
            {
                Name = swf
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private async void btnLoadMapSwf_Click(object sender, EventArgs e)
        {
            if (txtMapSwf.Text == "" || txtMapSwf.Text == "Map filename (.swf)" || !Player.IsLoggedIn)
            {
                World.GameMessage("Are you retarded?");
                return;
            }
            string swf = txtMapSwf.Text;
            if (txtMapSwf.Text.ToLower().Contains("oldmobius"))
                swf = "ChiralValley/town-Mobius-21Feb14.swf";
            btnLoadMapSwf.Enabled = false;
            Player.LoadMap(swf);
            await Task.Delay(2500);
            btnLoadMapSwf.Enabled = true;
        }

        private void btnQuestlistClearCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdQuestlist
            {
                state = CmdQuestlist.State.Clear
            }, (ModifierKeys & Keys.Control) == Keys.Control);
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

        private async void btnReloadMap_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                btnReloadMap.Enabled = false;
                World.ReloadCurrentMap();
                await Task.Delay(1000);
                World.GameMessage("The map has been reloaded!");
                await Task.Delay(1500);
                btnReloadMap.Enabled = true;
            }
        }

        public void SelectionModeToggle(bool Type)
        {
            if (Type)
            {
                this.lstCommands.SelectionMode = SelectionMode.MultiExtended;
                this.lstItems.SelectionMode = SelectionMode.MultiExtended;
                this.lstSkills.SelectionMode = SelectionMode.MultiExtended;
                this.lstQuests.SelectionMode = SelectionMode.MultiExtended;
                this.lstDrops.SelectionMode = SelectionMode.MultiExtended;
                this.lstBoosts.SelectionMode = SelectionMode.MultiExtended;
                this.lstPackets.SelectionMode = SelectionMode.MultiExtended;
            }
            else
            {
                this.lstCommands.SelectionMode = SelectionMode.One;
                this.lstItems.SelectionMode = SelectionMode.One;
                this.lstSkills.SelectionMode = SelectionMode.One;
                this.lstQuests.SelectionMode = SelectionMode.One;
                this.lstDrops.SelectionMode = SelectionMode.One;
                this.lstBoosts.SelectionMode = SelectionMode.One;
                this.lstPackets.SelectionMode = SelectionMode.One;
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

        public async void OnBankItemExecute()
        {
            if (lstItems != null && this.chkBankOnStop.Checked)
            {
                foreach (InventoryItem item in Player.Inventory.Items)
                {
                    if (!item.IsEquipped && item.IsAcItem && item.Category != "Class" && item.Name.ToLower() != "treasure potion" && lstItems.Items.Contains(item.Name))
                    {
                        Player.Bank.TransferToBank(item.Name);
                        await Task.Delay(70);
                        LogForm.Instance.AppendDebug("Transferred to Bank: " + item.Name + "\r\n");
                    }
                }
                LogForm.Instance.AppendDebug("Banked all AC Items in Items list \r\n");
            }
        }

        public async void CustomCommandToggle(bool Type)
        {
            Config c = Config.Load(Application.StartupPath + "\\BotClientConfig.cfg");
            bool customCommandToggle;
            try
            {
                customCommandToggle = bool.Parse(c.Get("customCommandToggle"));
            }
            catch { customCommandToggle = false; }
            if (customCommandToggle)
            {
                colorfulCommands.Checked = Type;
                lstCommands.Invalidate();
            }
            else
            {
                return;
            }
        }

        public void chkColorfulCommands_Click(object sender, EventArgs e)
        {
            lstCommands.Invalidate();
        }

        public void chkWarningMsgFilter_Click(object sender, EventArgs e)
        {
            OptionsManager.WarningMsgFilter = chkWarningMsgFilter.Checked;
        }

        public void btnSaveDirectory_Click(object sender, EventArgs e)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            c.Set("botsDirectory", txtSaved.Text.ToString());
            c.Save();
        }

        public void chkSaveState_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager._saveState = chkSaveState.Checked;
        }

    }
}