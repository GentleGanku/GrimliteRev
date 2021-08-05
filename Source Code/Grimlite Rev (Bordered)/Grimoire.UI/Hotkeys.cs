using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Map;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class Hotkeys : DarkForm
    {
        public static readonly Action[] Actions = new Action[18]
        {
            delegate
            {
                Root.Instance.ShowForm(BotManager.Instance);
            },
            delegate
            {
                Root.Instance.ShowForm(Instance);
            },
            delegate
            {
                Root.Instance.ShowForm(Loaders.Instance);
            },
            delegate
            {
                Root.Instance.ShowForm(PacketLogger.Instance);
            },
            delegate
            {
                Root.Instance.ShowForm(PacketSpammer.Instance);
            },
            delegate
            {
                Root.Instance.ShowForm(Travel.Instance);
            },
            delegate
            {
                Root.Instance.ShowForm(Root.Instance);
            },
            delegate
            {
                Player.Bank.Show();
            },
            delegate
            {
                Root.Instance.ShowForm(CosmeticForm.Instance);
            },
            delegate
            {
                Root.Instance.ShowForm(LogForm.Instance);
            },
            delegate
            {
                Root.Instance.ShowForm(Notepad.Instance);
            },
            delegate
            {
                Shop.LoadHairShop(1);
            },
            delegate
            {
                Shop.LoadArmorCustomizer();
            },
            delegate
            {
                ExecuteTravel(new List<IBotCommand>
                {
                    CreateJoinCommand("yulgar-100000", "Room", "Center")
                });
            },
            async delegate
            {
                string map = Player.Map;
                string mapnumber = World.RoomNumber.ToString();
                string cell = Player.Cell;
                string pad = Player.Pad;
                Player.Logout();
                await AutoRelogin.Login(new Server() { Name = "Safiria" }, 3000, cts: new System.Threading.CancellationTokenSource(), ensureSuccess:true);
                ExecuteTravel(new List<IBotCommand>
                {
                    CreateJoinCommand($"{map}-{mapnumber}", cell, pad)
                });
            },
            delegate
            {
                BotToggleAsync();
            },
            delegate
            {
                if (OptionsManager.IsRunning)
                    OptionsManager.Start();
                else
                    OptionsManager.Stop();
            },
            delegate
            {
                Console.WriteLine(Player.IsMember);
            }
        };
        
        private static async void BotToggleAsync()
        {
            if (Player.IsAlive && Player.IsLoggedIn && BotManager.Instance.lstCommands.Items.Count > 0 && BotManager.Instance.ActiveBotEngine.IsRunning)
            {
                BotManager.Instance.MultiMode();
                BotManager.Instance.ActiveBotEngine.IsRunningChanged += BotManager.Instance.OnIsRunningChanged;
                BotManager.Instance.ActiveBotEngine.IndexChanged += BotManager.Instance.OnIndexChanged;
                BotManager.Instance.ActiveBotEngine.ConfigurationChanged += BotManager.Instance.OnConfigurationChanged;
                BotManager.Instance.ActiveBotEngine.Start(BotManager.Instance.GenerateConfiguration());
                BotManager.Instance.BotStateChanged(IsRunning: true);
            }
            else
            {
                if (Configuration.Instance.Items != null && Configuration.Instance.BankOnStop)
                {
                    foreach (InventoryItem item in Player.Inventory.Items)
                    {
                        if (!item.IsEquipped && item.IsAcItem && item.Category != "Class" && item.Name.ToLower() != "treasure potion" && Configuration.Instance.Items.Contains(item.Name))
                        {
                            Player.Bank.TransferToBank(item.Name);
                            await Task.Delay(70);
                            LogForm.Instance.AppendDebug("Transferred to Bank: " + item.Name + "\r\n");
                        }
                    }
                    LogForm.Instance.AppendDebug("Banked all AC Items in Items list \r\n");
                }
                BotManager.Instance.ActiveBotEngine.Stop();
                BotManager.Instance.MultiMode();
                await Task.Delay(2000);
                BotManager.Instance.BotStateChanged(IsRunning: false);
            }
        }

        public static readonly List<Hotkey> InstalledHotkeys = new List<Hotkey>();

        private int _processId;

        private IContainer components;

        private DarkListBox lstKeys;

        private DarkComboBox cbKeys;

        private DarkComboBox cbActions;

        private DarkButton btnAdd;

        private DarkButton btnRemove;

        private TableLayoutPanel tableLayoutPanel1;

        private DarkButton btnSave;

        public static Hotkeys Instance
        {
            get;
        } = new Hotkeys();

        private string configPath => Path.Combine(Application.StartupPath, "hotkeys.json");

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        private Hotkeys()
        {
            InitializeComponent();
        }

        private void Hotkeys_Load(object sender, EventArgs e)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string font = c.Get("font");
            float? fontSize = float.Parse(c.Get("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            lstKeys.DisplayMember = "Text";
            cbActions.SelectedIndex = 0;
            cbKeys.SelectedIndex = 0;
            if (font != null && fontSize != null)
                this.Font = new Font(font, (float)fontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        private static CmdTravel CreateJoinCommand(string map, string cell = "Enter", string pad = "Spawn")
        {
            return new CmdTravel
            {
                Map = map,
                Cell = cell,
                Pad = pad
            };
        }

        private static async void ExecuteTravel(List<IBotCommand> cmds)
        {
            foreach (IBotCommand cmd in cmds)
            {
                await cmd.Execute(null);
                await Task.Delay(1000);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Hotkey hotkey = (Hotkey)lstKeys.SelectedItem;
            if (hotkey != null)
            {
                hotkey.Uninstall();
                InstalledHotkeys.Remove(hotkey);
                lstKeys.Items.Remove(hotkey);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int selectedIndex = cbActions.SelectedIndex;
            if (selectedIndex > -1 && cbKeys.SelectedIndex > -1)
            {
                Keys keys = (Keys)Enum.Parse(typeof(Keys), cbKeys.SelectedItem.ToString());
                if (!KeyboardHook.Instance.TargetedKeys.Contains(keys))
                {
                    Hotkey hotkey = new Hotkey
                    {
                        ActionIndex = selectedIndex,
                        Key = keys,
                        Text = $"{keys}: {cbActions.Items[selectedIndex]}"
                    };
                    hotkey.Install();
                    InstalledHotkeys.Add(hotkey);
                    lstKeys.Items.Add(hotkey);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(InstalledHotkeys));
        }

        public void LoadHotkeys()
        {
            if (File.Exists(configPath))
            {
                Hotkey[] array = JsonConvert.DeserializeObject<Hotkey[]>(File.ReadAllText(configPath));
                if (array != null)
                {
                    InstalledHotkeys.AddRange(array);
                    foreach (Hotkey installedHotkey in InstalledHotkeys)
                    {
                        lstKeys.Items.Add(installedHotkey);
                        installedHotkey.Install();
                    }
                }
            }
            KeyboardHook.Instance.KeyDown += OnKeyDown;
            _processId = Process.GetCurrentProcess().Id;
        }

        public void OnKeyDown(Keys key)
        {
            Hotkey hotkey = InstalledHotkeys.First((Hotkey h) => h.Key == key);
            if (ApplicationContainsFocus() || (string)cbActions.Items[hotkey.ActionIndex] == "Minimize to tray")
            {
                Actions[hotkey.ActionIndex]();
            }
        }

        public bool ApplicationContainsFocus()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            if (foregroundWindow == IntPtr.Zero)
            {
                return false;
            }
            GetWindowThreadProcessId(foregroundWindow, out int processId);
            return processId == _processId;
        }

        private void Hotkeys_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hotkeys));
            this.lstKeys = new DarkUI.Controls.DarkListBox(this.components);
            this.cbKeys = new DarkUI.Controls.DarkComboBox();
            this.cbActions = new DarkUI.Controls.DarkComboBox();
            this.btnAdd = new DarkUI.Controls.DarkButton();
            this.btnRemove = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstKeys
            // 
            this.lstKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstKeys.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lstKeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstKeys.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstKeys.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstKeys.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lstKeys.FormattingEnabled = true;
            this.lstKeys.HorizontalScrollbar = true;
            this.lstKeys.ItemHeight = 18;
            this.lstKeys.Location = new System.Drawing.Point(12, 68);
            this.lstKeys.Name = "lstKeys";
            this.lstKeys.Size = new System.Drawing.Size(230, 74);
            this.lstKeys.TabIndex = 28;
            // 
            // cbKeys
            // 
            this.cbKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbKeys.FormattingEnabled = true;
            this.cbKeys.Items.AddRange(new object[] {
            "Left",
            "Up",
            "Right",
            "Down",
            "D0",
            "D1",
            "D2",
            "D3",
            "D4",
            "D5",
            "D6",
            "D7",
            "D8",
            "D9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12"});
            this.cbKeys.Location = new System.Drawing.Point(3, 3);
            this.cbKeys.Name = "cbKeys";
            this.cbKeys.Size = new System.Drawing.Size(109, 21);
            this.cbKeys.TabIndex = 29;
            // 
            // cbActions
            // 
            this.cbActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbActions.FormattingEnabled = true;
            this.cbActions.Items.AddRange(new object[] {
            "Show Bot",
            "Show Hotkeys",
            "Show Loaders",
            "Show Packet logger",
            "Show Packet spammer",
            "Show Fast travels",
            "Minimize to tray",
            "Show bank",
            "Show Cosmetics form",
            "Show Logs",
            "Show Notepad",
            "Load Hair shop",
            "Load Armor Customizer",
            "Yulgar Suite 42",
            "Relog",
            "Start/Stop Bot",
            "Toggle Options,",
            "Execute Debug"});
            this.cbActions.Location = new System.Drawing.Point(118, 3);
            this.cbActions.Name = "cbActions";
            this.cbActions.Size = new System.Drawing.Size(109, 21);
            this.cbActions.TabIndex = 30;
            // 
            // btnAdd
            // 
            this.btnAdd.Checked = false;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(3, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(109, 21);
            this.btnAdd.TabIndex = 31;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Checked = false;
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemove.Location = new System.Drawing.Point(118, 30);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(109, 21);
            this.btnRemove.TabIndex = 32;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Checked = false;
            this.btnSave.Location = new System.Drawing.Point(12, 154);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(230, 23);
            this.btnSave.TabIndex = 33;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cbKeys, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbActions, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRemove, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(230, 54);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // Hotkeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(255, 186);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lstKeys);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Hotkeys";
            this.Text = "Hotkeys";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Hotkeys_FormClosing);
            this.Load += new System.EventHandler(this.Hotkeys_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}