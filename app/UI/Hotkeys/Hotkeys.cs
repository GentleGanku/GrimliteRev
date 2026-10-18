using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Map;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class Hotkeys : DarkForm
    {
        #region Fields

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
                if (Root.Instance.ContainsFocus)
                {
                    if (Player.IsLoggedIn)
                    {
                        Player.Bank.Show();
                    }
                }
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
                if (Root.Instance.ContainsFocus)
                {
                    if (Player.IsLoggedIn)
                    {
                        Shop.LoadHairShop(1);
                    }
                }
            },
            delegate
            {
                if (Root.Instance.ContainsFocus)
                {
                    if (Player.IsLoggedIn)
                    {
                        Shop.LoadArmorCustomizer();
                    }
                }
            },
            delegate
            {
                if (Root.Instance.ContainsFocus)
                {
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("yulgar", "1e99", "Room", "Center")
                    });
                }
            },
            async delegate
            {
                if (Root.Instance.ContainsFocus)
                {
                    if (Player.IsLoggedIn)
                    {
                        string map = Player.Map;
                        string mapnumber = World.RoomNumber.ToString();
                        string cell = Player.Cell;
                        string pad = Player.Pad;
                        Player.Logout();
                        await BotManager.Instance.ActiveBotEngine.WaitUntil(() => AutoRelogin.LoginLabel, null, 5, 1500);
                        AutoRelogin.LoginExecute();
                        await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !AutoRelogin.IsClientLoading("Account"), null, 10, 500);
                        await AutoRelogin.Login((Server)BotManager.Instance.cbServers.SelectedItem, 7000, cts: new System.Threading.CancellationTokenSource(), ensureSuccess:true);
                        ExecuteTravel(new List<IBotCommand>
                        {
                            CreateJoinCommand($"{map}-{mapnumber}", cell, pad)
                        });
                    }
                }
            },
            delegate
            {
                BotToggleAsync();
            },
            delegate
            {
                if (Root.Instance.ContainsFocus)
                {
                    Root.Instance.QuickLogin().Forget("logging in with Quick Login");
                }
            },
            delegate
            {
                Root.Instance.rtbPing.Visible = !Root.Instance.rtbPing.Visible;
            },
            delegate
            {
                Root.Instance.Root_MenuChanged();
            }
        };

        public static readonly List<Hotkey> InstalledHotkeys = new List<Hotkey>();
        private static readonly Dictionary<string, string> RenamedActions = new Dictionary<string, string>
        {
            { "Show Hotkeys", "Show Keyboard Shortcuts" },
            { "Show Cosmetics Form", "Show Cosmetics Panel" },
            { "Immediate Login", "Quick Login" }
        };
        private int _processId;
        private static System.Threading.CancellationTokenSource login_cts;

        #endregion

        #region Constructors

        private Hotkeys()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public static Hotkeys Instance
        {
            get;
        } = new Hotkeys();

        private string configPath
        {
            get
            {
                return Path.Combine(Application.StartupPath, "hotkeys.json");
            }
        }

        #endregion

        #region Methods

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

        public void LoadHotkeys()
        {
            if (File.Exists(configPath))
            {
                Hotkey[] array = JsonConvert.DeserializeObject<Hotkey[]>(File.ReadAllText(configPath));
                if (array != null)
                {
                    InstalledHotkeys.AddRange(array.Where(ResolveAction));
                    foreach (Hotkey installedHotkey in InstalledHotkeys)
                    {
                        lstKeys.Items.Add(installedHotkey.Text);
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

        private static async void BotToggleAsync()
        {
            try
            {
                if (Player.IsAlive && Player.IsLoggedIn && BotManager.Instance.lstCommands.Items.Count > 0 && (!BotManager.Instance.ActiveBotEngine.IsRunning))
                {
                    if (!BotManager.Instance.IsHandleCreated)
                    {
                        Instance.ShowForm(BotManager.Instance);
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
                    Root.Instance.BotStateChanged(IsRunning: true);
                    BotManager.Instance.btnBotPause.Enabled = true;
                    BotManager.Instance.btnBotStop.Enabled = true;
                }
                else if (BotManager.Instance.ActiveBotEngine.IsRunning)
                {
                    BotManager.Instance.btnBotStart.Enabled = false;
                    BotManager.Instance.ActiveBotEngine.Stop();
                    BotManager.Instance.CustomCommandToggle(true);
                    BotManager.Instance.SelectionModeToggle(true);
                    BotManager.Instance.BotStateChanged(IsRunning: false);
                    await Task.Delay(2000);
                    Root.Instance.BotStateChanged(IsRunning: false);
                    BotManager.Instance.UpdateStartEnabled();
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running Hotkeys.BotToggleAsync", ex);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        private static CmdJoin2 CreateJoinCommand(string map, string room = "1e99", string cell = "Enter", string pad = "Spawn")
        {
            return new CmdJoin2
            {
                Map = map,
                Room = room,
                Cell = cell,
                Pad = pad
            };
        }

        private static async void ExecuteTravel(List<IBotCommand> cmds)
        {
            try
            {
                foreach (IBotCommand cmd in cmds)
                {
                    await cmd.Execute(BotManager.Instance.ActiveBotEngine);
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running Hotkeys.ExecuteTravel", ex);
            }
        }

        private void Hotkeys_Load(object sender, EventArgs e)
        {
            string font = Config.Instance.GetValue<string>("font");
            float? fontSize = float.Parse(Config.Instance.GetValue<string>("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            lstKeys.DisplayMember = "Text";
            cbActions.SelectedIndex = 0;
            cbKeys.SelectedIndex = 0;
            if (font != null && fontSize != null)
            {
                this.Font = new Font(font, (float)fontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        /// <summary>
        /// Points a saved shortcut at its action by the label in its text ("F1: Show Bot Manager"), so it keeps its
        /// action when actions are renamed (see <see cref="RenamedActions"/>) or move in the list. A shortcut whose
        /// action no longer exists is dropped.
        /// </summary>
        private bool ResolveAction(Hotkey hotkey)
        {
            int separator = hotkey.Text?.IndexOf(": ") ?? -1;
            if (separator < 0)
            {
                return hotkey.ActionIndex >= 0 && hotkey.ActionIndex < cbActions.Items.Count;
            }
            string label = hotkey.Text.Substring(separator + 2);
            if (RenamedActions.TryGetValue(label, out string renamed))
            {
                label = renamed;
            }
            int index = cbActions.Items.IndexOf(label);
            if (index < 0)
            {
                return false;
            }
            hotkey.ActionIndex = index;
            hotkey.Text = $"{hotkey.Key}: {label}";
            return true;
        }

        private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = lstKeys.SelectedIndex > -1;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstKeys.SelectedIndex < 0)
            {
                return;
            }
            string hotkeyText = lstKeys.SelectedItem.ToString();
            int hotkeyIndex = lstKeys.Items.IndexOf($"{hotkeyText}");
            if (lstKeys.SelectedIndex > -1)
            {
                KeyboardHook.Instance.TargetedKeys.RemoveAt(hotkeyIndex);
                InstalledHotkeys.RemoveAt(hotkeyIndex);
                lstKeys.Items.RemoveAt(hotkeyIndex);
            }
            btnRemove.Enabled = lstKeys.SelectedIndex > -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int selectedIndex = cbActions.SelectedIndex;
            if (selectedIndex > -1 && cbKeys.SelectedIndex > -1)
            {
                Keys keys = (Keys)Enum.Parse(typeof(Keys), cbKeys.SelectedItem.ToString());
                if (!KeyboardHook.Instance.TargetedKeys.Contains(keys))
                {
                    if ((string)cbActions.Items[selectedIndex] == "Quick Login")
                    {
                        string input = "";
                        DialogResult result = InputBox.InputDialog(ref input,
                            "Please type the name of the server that you wish to use for the Quick Login.",
                            "Quick Login", 250, 100, 40, 40);
                        if (result == DialogResult.OK)
                        {
                            int index = Root.Instance.toolStripComboBoxLoginServer.Items.Cast<object>().ToList().FindIndex(s => string.Equals((s as Server)?.Name, input.Trim(), StringComparison.OrdinalIgnoreCase));
                            BotClientConfig.Instance.SetValue("serverIndex", ((index > -1) ? index.ToString() : "0"));
                        }
                        else
                        {
                            return;
                        }
                    }
                    Hotkey hotkey = new Hotkey
                    {
                        ActionIndex = selectedIndex,
                        Key = keys,
                        Text = $"{keys}: {cbActions.Items[selectedIndex]}"
                    };
                    hotkey.Install();
                    InstalledHotkeys.Add(hotkey);
                    lstKeys.Items.Add(hotkey.Text);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(InstalledHotkeys));
        }

        private void Hotkeys_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        #endregion
    }
}