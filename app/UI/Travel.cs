using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Item;
using Grimoire.Botting.Commands.Map;
using Grimoire.Game;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class Travel : DarkForm
    {
        #region Constructors

        static Travel()
        {
            Instance = new Travel();
        }

        private Travel()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public static Travel Instance
        {
            get;
        }

        public List<string> travels
        {
            get;
            set;
        } = new List<string>();

        private List<string> travelNames
        {
            get;
            set;
        } = new List<string>();

        #endregion

        #region Methods

        public void executeCustomTravel()
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                string[] targetedTravel = travels[cbCustomTravels.SelectedIndex].Split('`');
                ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand(targetedTravel[1], targetedTravel[2], targetedTravel[3], true)
                    });
            }
        }

        public void scanCustomTravel()
        {
            if (travels.Count > 0)
            {
                travels.Clear();
            }
            if (cbCustomTravels.Items.Count > 0)
            {
                cbCustomTravels.Items.Clear();
            }
            if (travelNames.Count > 0)
            {
                travelNames.Clear();
            }
            string tempTravels = BotClientConfig.Instance.GetValue<string>("customTravels");
            if (!string.IsNullOrEmpty(tempTravels))
            {
                travels.AddRange(tempTravels.Split('|'));
                foreach (string travel in travels)
                {
                    travelNames.Add(travel.Split('`')[0]);
                }
                cbCustomTravels.Items.AddRange(travelNames.ToArray());
            }
        }

        private void Travel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void chkPriv_CheckedChanged(object sender, EventArgs e)
        {
            numPriv.Enabled = chkPriv.Checked;
        }

        private void btnTercess_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "tercessuinotlim")
                {
                    Player.MoveToCell("m22", "Left");
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("tercessuinotlim")
                    });
                }
                else
                    Player.MoveToCell("Enter", "Spawn");
            }
        }

        private void btnTwins_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "tercessuinotlim")
                {
                    Player.MoveToCell("m22", "Left");
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("tercessuinotlim", "Twins", "Left")
                    });
                }
                else
                    Player.MoveToCell("Twins", "Left");
            }
        }

        private void btnTaro_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "tercessuinotlim")
                {
                    Player.MoveToCell("m22", "Left");
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("tercessuinotlim", "Taro", "Left")
                    });
                }
                else
                    Player.MoveToCell("Taro", "Left");
            }
        }

        private void btnSwindle_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "tercessuinotlim")
                {
                    Player.MoveToCell("m22", "Left");
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("tercessuinotlim", "Swindle", "Left")
                    });
                }
                else
                    Player.MoveToCell("Swindle", "Left");
            }
        }

        private void btnNulgath_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "tercessuinotlim")
                {
                    Player.MoveToCell("m22", "Left");
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("tercessuinotlim", "Boss2", "Right")
                    });
                }
                else
                    Player.MoveToCell("Boss2", "Right");
            }
        }

        private void btnEscherion_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "escherion")
                {
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("escherion", "Boss", "Left")
                    });
                }
                else
                    Player.MoveToCell("Boss", "Left");
            }
        }

        private void btnDage_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "underworld")
                {
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("underworld", "s1", "Left")
                    });
                }
                else
                    Player.MoveToCell("s1", "Left");
            }
        }

        private CmdJoin2 CreateJoinCommand(string map, string cell = "Enter", string pad = "Spawn", bool custom = false)
        {
            return new CmdJoin2
            {
                Map = map.Contains("-") ? map.Split('-')[0] : map,
                Room = !custom ? (chkPriv.Checked ? $"{numPriv.Value}" : "1") : (map.Contains("-") ? map.Split('-')[1] : "1"),
                Cell = cell,
                Pad = pad
            };
        }

        private CmdLoad CreateShopCommand(int shopid)
        {
            return new CmdLoad
            {
                ShopId = shopid
            };
        }

        private async void ExecuteTravel(List<IBotCommand> cmds)
        {
            grpTravel.Enabled = false;
            aweGroup.Enabled = false;
            foreach (IBotCommand cmd in cmds)
            {
                await cmd.Execute(BotManager.Instance.ActiveBotEngine);
                await Task.Delay(1000);
            }
            if (InvokeRequired)
            {
                Invoke((Action)delegate
                {
                    grpTravel.Enabled = true;
                    aweGroup.Enabled = true;
                });
            }
            else
            {
                grpTravel.Enabled = true;
                aweGroup.Enabled = true;
            }
        }

        private void btnBinky_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "doomvault")
                {
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("doomvault", "r5", "Left")
                    });
                }
                else
                    Player.MoveToCell("r5", "Left");
            }
        }

        private void btnCarnage_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "tercessuinotlim")
                {
                    Player.MoveToCell("m22", "Left");
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("tercessuinotlim", "m4", "Top")
                    });
                }
                else
                    Player.MoveToCell("m4", "Top");
            }
        }

        private void btnLae_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "tercessuinotlim")
                {
                    Player.MoveToCell("m22", "Left");
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("tercessuinotlim", "m5", "Top")
                    });
                }
                else
                    Player.MoveToCell("m5", "Top");
            }
        }

        private void btnPolish_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "tercessuinotlim")
                {
                    Player.MoveToCell("m22", "Left");
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("tercessuinotlim", "m12", "Top")
                    });
                }
                else
                    Player.MoveToCell("m12", "Top");
            }
        }

        private void AweTravel_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                bool IsLucky = aweLucky.Checked;
                bool IsWizard = aweWizard.Checked;
                bool IsHybrid = aweHybrid.Checked;
                bool IsThief = aweThief.Checked;
                bool IsFighter = aweFigther.Checked;
                bool IsHealer = aweHealer.Checked;

                ExecuteTravel(new List<IBotCommand>
                {
                    CreateShopCommand(
                        IsHybrid ? 633 :
                        IsFighter ? 635 :
                        IsWizard ? 636 :
                        IsThief ? 637 :
                        IsHealer ? 638 :
                        IsLucky ? 639 : 633
                    )
                });
            }
        }

        private void btnFrostSpirit_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "icestormarena")
                {
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("icestormarena", "r3c", "Top")
                    });
                }
                else
                    Player.MoveToCell("r3c", "Top");
            }
        }

        private void btnDummy_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "classhall")
                {
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("classhall", "r4", "Right")
                    });
                }
                else
                    Player.MoveToCell("r4", "Right");
            }
        }

        private void btnRaxgore_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "doomvaultb")
                {
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("doomvaultb", "r26", "Left")
                    });
                }
                else
                    Player.MoveToCell("r26", "Left");
            }
        }

        private void btnUltraBosses_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn)
            {
                if (Player.Map != "timeinn")
                {
                    ExecuteTravel(new List<IBotCommand>
                    {
                        CreateJoinCommand("timeinn", "r3", "Bottom")
                    });
                }
                else
                    Player.MoveToCell("r3", "Bottom");
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (rBtnShow.Checked)
            {
                if (cbCustomTravels.SelectedIndex > -1)
                {
                    executeCustomTravel();
                }
                else
                {
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                        "Failed to execute a Custom Travel. Please make sure that you have a pre-existing list.", "Custom Travels", MessageBoxIcon.Error);
                }
            }
            else if (rBtnAdd.Checked)
            {
                string loadedTravels = BotClientConfig.Instance.GetValue<string>("customTravels");
                string[] tempTravels = string.IsNullOrEmpty(loadedTravels) ? new string[0] : loadedTravels.Split('`');
                string newTravels = loadedTravels + (tempTravels.Length > 1 ? "|" : "") + $"{txtCustomTravelName.Text}`{txtCustomMap.Text}`{txtCustomCell.Text}`{txtCustomPad.Text}";
                BotClientConfig.Instance.SetValue("customTravels", newTravels);
                scanCustomTravel();
                cbCustomTravels.SelectedIndex = 0;
            }
            else if (rBtnRemove.Checked)
            {
                int targetedIndex = travelNames.IndexOf(txtCustomTravelName.Text);
                if (targetedIndex > -1)
                {
                    travels.RemoveAt(targetedIndex);
                    string newTravels = string.Join("|", travels.ToArray());
                    BotClientConfig.Instance.SetValue("customTravels", newTravels);
                    scanCustomTravel();
                    cbCustomTravels.SelectedIndex = -1;
                }
                else
                {
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                        "Failed to remove the targeted Custom Travel. Please make sure that you have typed the travel name correctly.", "Load Custom Travels", MessageBoxIcon.Error);
                }
            }
        }

        private void cbCustomTravels_Click(object sender, EventArgs e)
        {
            try
            {
                scanCustomTravel();
            }
            catch
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                    "Failed to load the Custom Travel list. Please make sure that you have a pre-existing list.", "Load Custom Travels", MessageBoxIcon.Error);
            }
        }

        private void cbCustomTravels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCustomTravels.SelectedIndex > -1)
            {
                rBtnShow.Checked = true;
                if (rBtnAdd.Checked)
                    rBtnAdd.Checked = false;
                if (rBtnRemove.Checked)
                    rBtnRemove.Checked = false;
                txtCustomTravelName.ReadOnly = true;
                txtCustomMap.ReadOnly = true;
                txtCustomMap.Enabled = true;
                txtCustomCell.ReadOnly = true;
                txtCustomCell.Enabled = true;
                txtCustomPad.ReadOnly = true;
                txtCustomPad.Enabled = true;
                string[] targetedTravel = travels[cbCustomTravels.SelectedIndex].Split('`');
                txtCustomTravelName.Text = targetedTravel[0];
                txtCustomMap.Text = targetedTravel[1];
                txtCustomCell.Text = targetedTravel[2];
                txtCustomPad.Text = targetedTravel[3];
            }
        }

        private void rBtnShow_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCustomTravels.SelectedIndex > -1)
            {
                if (rBtnShow.Checked)
                {
                    if (rBtnAdd.Checked)
                        rBtnAdd.Checked = false;
                    if (rBtnRemove.Checked)
                        rBtnRemove.Checked = false;
                    txtCustomTravelName.ReadOnly = true;
                    txtCustomMap.ReadOnly = true;
                    txtCustomMap.Enabled = true;
                    txtCustomCell.ReadOnly = true;
                    txtCustomCell.Enabled = true;
                    txtCustomPad.ReadOnly = true;
                    txtCustomPad.Enabled = true;
                    string[] targetedTravel = travels[cbCustomTravels.SelectedIndex].Split('`');
                    txtCustomTravelName.Text = targetedTravel[0];
                    txtCustomMap.Text = targetedTravel[1];
                    txtCustomCell.Text = targetedTravel[2];
                    txtCustomPad.Text = targetedTravel[3];
                }
            }
            else if (rBtnShow.Checked && !rBtnAdd.Checked && !rBtnRemove.Checked)
            {
                rBtnShow.Checked = false;
                if (txtCustomMap.Enabled)
                    rBtnAdd.Checked = true;
                else
                    rBtnRemove.Checked = true;
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                    "Failed to show the Custom Travel. Please make sure that you have selected a target.", "Show Custom Travel", MessageBoxIcon.Error);
            }
        }

        private void rBtnAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtnAdd.Checked)
            {
                if (rBtnShow.Checked)
                    rBtnShow.Checked = false;
                if (rBtnRemove.Checked)
                    rBtnRemove.Checked = false;
                txtCustomTravelName.ReadOnly = false;
                txtCustomMap.ReadOnly = false;
                txtCustomMap.Enabled = true;
                txtCustomCell.ReadOnly = false;
                txtCustomCell.Enabled = true;
                txtCustomPad.ReadOnly = false;
                txtCustomPad.Enabled = true;
            }
        }

        private void rBtnRemove_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtnRemove.Checked)
            {
                if (rBtnShow.Checked)
                    rBtnShow.Checked = false;
                if (rBtnAdd.Checked)
                    rBtnAdd.Checked = false;
                txtCustomTravelName.ReadOnly = false;
                txtCustomMap.ReadOnly = false;
                txtCustomMap.Enabled = false;
                txtCustomCell.ReadOnly = false;
                txtCustomCell.Enabled = false;
                txtCustomPad.ReadOnly = false;
                txtCustomPad.Enabled = false;
            }
        }

        private void chkCustomHotkeys_CheckedChanged(object sender, EventArgs e)
        {
            BotClientConfig.Instance.SetValue("customTravelHotkeys", chkCustomHotkeys.Checked.ToString());
        }

        private void chkCustomChatTrigger_CheckedChanged(object sender, EventArgs e)
        {
            BotClientConfig.Instance.SetValue("customTravelTrigger", chkCustomChatTrigger.Checked.ToString());
            if (chkCustomChatTrigger.Checked)
            {
                Flash.Call("LoadTravelTriggers", BotClientConfig.Instance.GetValue<string>("customTravels"));
            }
        }

        #endregion
    }
}