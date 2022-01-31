using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Item;
using Grimoire.Botting.Commands.Map;
using Grimoire.Game;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class Travel : DarkForm
    {
        private IContainer components;
        private DarkButton btnDage;
        private DarkButton btnEscherion;
        private DarkButton btnNulgath;
        private DarkButton btnSwindle;
        private DarkButton btnTaro;
        private DarkButton btnTwins;
        private DarkButton btnTercess;
        private DarkGroupBox grpTravel;
        private DarkNumericUpDown numPriv;
        private DarkCheckBox chkPriv;
        private DarkButton btnPolish;
        private DarkButton btnLae;
        private DarkButton btnCarnage;
        private DarkButton AweTravel;
        private DarkGroupBox aweGroup;
        private DarkRadioButton aweWizard;
        private DarkRadioButton aweLucky;
        private DarkPanel panel1;
        private DarkRadioButton aweThief;
        private DarkRadioButton aweHybrid;
        private DarkRadioButton aweHealer;
        private TableLayoutPanel tableLayoutPanel1;
        private DarkRadioButton aweFigther;
        private DarkGroupBox grpCustomTravel;
        private DarkTextBox txtCustomMap;
        private DarkTextBox txtCustomCell;
        private DarkTextBox txtCustomPad;
        private DarkTextBox txtCustomTravelName;
        public DarkComboBox cbCustomTravels;
        private DarkGroupBox grpCustomTravel2;
        private DarkButton btnGo;
        private Label lblCustomTravelTip;
        private DarkRadioButton rBtnRemove;
        private DarkRadioButton rBtnAdd;
        private DarkRadioButton rBtnShow;
        private DarkGroupBox grpCustomTravel3;
        private Label lblCustomHotkeyTip2;
        private Label lblCustomHotkeyTip1;
        private Label lblCustomHotkeyTip4;
        private Label lblCustomHotkeyTip3;
        private Label lblCustomChatTriggerTip1;
        private DarkGroupBox darkGroupBox1;
        private DarkButton btnRaxgore;
        private DarkButton btnDummy;
        private DarkButton btnFrostSpirit;
        private DarkButton btnUltraBosses;
        public DarkCheckBox chkCustomHotkeys;
        public DarkCheckBox chkCustomChatTrigger;
        private DarkButton btnBinky;

        public static Travel Instance
        {
            get;
        }

        private Travel()
        {
            InitializeComponent();
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

        private CmdTravel CreateJoinCommand(string map, string cell = "Enter", string pad = "Spawn", bool custom = false)
        {
            return new CmdTravel
            {
                Map = !custom ? ((chkPriv.Checked ? (map + $"-{numPriv.Value}") : map)) : map,
                Cell = cell,
                Pad = pad
            };
        }

        private CmdLoadTravel CreateShopCommand(int shopid)
        {
            return new CmdLoadTravel
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
                await cmd.Execute(null);
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
            this.btnDage = new DarkUI.Controls.DarkButton();
            this.btnEscherion = new DarkUI.Controls.DarkButton();
            this.btnBinky = new DarkUI.Controls.DarkButton();
            this.btnNulgath = new DarkUI.Controls.DarkButton();
            this.btnSwindle = new DarkUI.Controls.DarkButton();
            this.btnTaro = new DarkUI.Controls.DarkButton();
            this.btnTwins = new DarkUI.Controls.DarkButton();
            this.btnTercess = new DarkUI.Controls.DarkButton();
            this.grpTravel = new DarkUI.Controls.DarkGroupBox();
            this.btnUltraBosses = new DarkUI.Controls.DarkButton();
            this.btnRaxgore = new DarkUI.Controls.DarkButton();
            this.btnDummy = new DarkUI.Controls.DarkButton();
            this.btnFrostSpirit = new DarkUI.Controls.DarkButton();
            this.numPriv = new DarkUI.Controls.DarkNumericUpDown();
            this.btnPolish = new DarkUI.Controls.DarkButton();
            this.btnLae = new DarkUI.Controls.DarkButton();
            this.btnCarnage = new DarkUI.Controls.DarkButton();
            this.chkPriv = new DarkUI.Controls.DarkCheckBox();
            this.AweTravel = new DarkUI.Controls.DarkButton();
            this.aweGroup = new DarkUI.Controls.DarkGroupBox();
            this.panel1 = new DarkUI.Controls.DarkPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.aweLucky = new DarkUI.Controls.DarkRadioButton();
            this.aweHybrid = new DarkUI.Controls.DarkRadioButton();
            this.aweHealer = new DarkUI.Controls.DarkRadioButton();
            this.aweFigther = new DarkUI.Controls.DarkRadioButton();
            this.aweThief = new DarkUI.Controls.DarkRadioButton();
            this.aweWizard = new DarkUI.Controls.DarkRadioButton();
            this.grpCustomTravel = new DarkUI.Controls.DarkGroupBox();
            this.lblCustomTravelTip = new System.Windows.Forms.Label();
            this.cbCustomTravels = new DarkUI.Controls.DarkComboBox();
            this.txtCustomTravelName = new DarkUI.Controls.DarkTextBox();
            this.txtCustomPad = new DarkUI.Controls.DarkTextBox();
            this.txtCustomCell = new DarkUI.Controls.DarkTextBox();
            this.txtCustomMap = new DarkUI.Controls.DarkTextBox();
            this.grpCustomTravel2 = new DarkUI.Controls.DarkGroupBox();
            this.rBtnShow = new DarkUI.Controls.DarkRadioButton();
            this.rBtnRemove = new DarkUI.Controls.DarkRadioButton();
            this.rBtnAdd = new DarkUI.Controls.DarkRadioButton();
            this.btnGo = new DarkUI.Controls.DarkButton();
            this.grpCustomTravel3 = new DarkUI.Controls.DarkGroupBox();
            this.chkCustomHotkeys = new DarkUI.Controls.DarkCheckBox();
            this.lblCustomHotkeyTip1 = new System.Windows.Forms.Label();
            this.lblCustomHotkeyTip4 = new System.Windows.Forms.Label();
            this.lblCustomHotkeyTip3 = new System.Windows.Forms.Label();
            this.lblCustomHotkeyTip2 = new System.Windows.Forms.Label();
            this.lblCustomChatTriggerTip1 = new System.Windows.Forms.Label();
            this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            this.chkCustomChatTrigger = new DarkUI.Controls.DarkCheckBox();
            this.grpTravel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriv)).BeginInit();
            this.aweGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpCustomTravel.SuspendLayout();
            this.grpCustomTravel2.SuspendLayout();
            this.grpCustomTravel3.SuspendLayout();
            this.darkGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDage
            // 
            this.btnDage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnDage.BackColorUseGeneric = false;
            this.btnDage.Checked = false;
            this.btnDage.Location = new System.Drawing.Point(6, 287);
            this.btnDage.Name = "btnDage";
            this.btnDage.Size = new System.Drawing.Size(145, 23);
            this.btnDage.TabIndex = 12;
            this.btnDage.Text = "Dage (underworld)";
            this.btnDage.Click += new System.EventHandler(this.btnDage_Click);
            // 
            // btnEscherion
            // 
            this.btnEscherion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnEscherion.BackColorUseGeneric = false;
            this.btnEscherion.Checked = false;
            this.btnEscherion.Location = new System.Drawing.Point(6, 314);
            this.btnEscherion.Name = "btnEscherion";
            this.btnEscherion.Size = new System.Drawing.Size(145, 23);
            this.btnEscherion.TabIndex = 13;
            this.btnEscherion.Text = "Escherion (escherion)";
            this.btnEscherion.Click += new System.EventHandler(this.btnEscherion_Click);
            // 
            // btnBinky
            // 
            this.btnBinky.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnBinky.BackColorUseGeneric = false;
            this.btnBinky.Checked = false;
            this.btnBinky.Location = new System.Drawing.Point(6, 260);
            this.btnBinky.Name = "btnBinky";
            this.btnBinky.Size = new System.Drawing.Size(145, 23);
            this.btnBinky.TabIndex = 11;
            this.btnBinky.Text = "Binky (doomvault)";
            this.btnBinky.Click += new System.EventHandler(this.btnBinky_Click);
            // 
            // btnNulgath
            // 
            this.btnNulgath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnNulgath.BackColorUseGeneric = false;
            this.btnNulgath.Checked = false;
            this.btnNulgath.Location = new System.Drawing.Point(6, 125);
            this.btnNulgath.Name = "btnNulgath";
            this.btnNulgath.Size = new System.Drawing.Size(145, 23);
            this.btnNulgath.TabIndex = 3;
            this.btnNulgath.Text = "Nulgath / Skew (tercess)";
            this.btnNulgath.Click += new System.EventHandler(this.btnNulgath_Click);
            // 
            // btnSwindle
            // 
            this.btnSwindle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSwindle.BackColorUseGeneric = false;
            this.btnSwindle.Checked = false;
            this.btnSwindle.Location = new System.Drawing.Point(6, 152);
            this.btnSwindle.Name = "btnSwindle";
            this.btnSwindle.Size = new System.Drawing.Size(145, 23);
            this.btnSwindle.TabIndex = 4;
            this.btnSwindle.Text = "Swindle (tercess)";
            this.btnSwindle.Click += new System.EventHandler(this.btnSwindle_Click);
            // 
            // btnTaro
            // 
            this.btnTaro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnTaro.BackColorUseGeneric = false;
            this.btnTaro.Checked = false;
            this.btnTaro.Location = new System.Drawing.Point(6, 98);
            this.btnTaro.Name = "btnTaro";
            this.btnTaro.Size = new System.Drawing.Size(145, 23);
            this.btnTaro.TabIndex = 5;
            this.btnTaro.Text = "VHL / Taro / Zee (tercess)";
            this.btnTaro.Click += new System.EventHandler(this.btnTaro_Click);
            // 
            // btnTwins
            // 
            this.btnTwins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnTwins.BackColorUseGeneric = false;
            this.btnTwins.Checked = false;
            this.btnTwins.Location = new System.Drawing.Point(6, 71);
            this.btnTwins.Name = "btnTwins";
            this.btnTwins.Size = new System.Drawing.Size(145, 23);
            this.btnTwins.TabIndex = 6;
            this.btnTwins.Text = "Twins (tercess)";
            this.btnTwins.Click += new System.EventHandler(this.btnTwins_Click);
            // 
            // btnTercess
            // 
            this.btnTercess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnTercess.BackColorUseGeneric = false;
            this.btnTercess.Checked = false;
            this.btnTercess.Location = new System.Drawing.Point(6, 44);
            this.btnTercess.Name = "btnTercess";
            this.btnTercess.Size = new System.Drawing.Size(145, 23);
            this.btnTercess.TabIndex = 7;
            this.btnTercess.Text = "Oblivion (tercess)";
            this.btnTercess.Click += new System.EventHandler(this.btnTercess_Click);
            // 
            // grpTravel
            // 
            this.grpTravel.Controls.Add(this.btnUltraBosses);
            this.grpTravel.Controls.Add(this.btnRaxgore);
            this.grpTravel.Controls.Add(this.btnDummy);
            this.grpTravel.Controls.Add(this.btnFrostSpirit);
            this.grpTravel.Controls.Add(this.numPriv);
            this.grpTravel.Controls.Add(this.btnPolish);
            this.grpTravel.Controls.Add(this.btnLae);
            this.grpTravel.Controls.Add(this.btnCarnage);
            this.grpTravel.Controls.Add(this.btnDage);
            this.grpTravel.Controls.Add(this.btnEscherion);
            this.grpTravel.Controls.Add(this.btnBinky);
            this.grpTravel.Controls.Add(this.btnNulgath);
            this.grpTravel.Controls.Add(this.btnSwindle);
            this.grpTravel.Controls.Add(this.btnTaro);
            this.grpTravel.Controls.Add(this.btnTwins);
            this.grpTravel.Controls.Add(this.btnTercess);
            this.grpTravel.Controls.Add(this.chkPriv);
            this.grpTravel.Location = new System.Drawing.Point(3, 5);
            this.grpTravel.Name = "grpTravel";
            this.grpTravel.Size = new System.Drawing.Size(157, 450);
            this.grpTravel.TabIndex = 8;
            this.grpTravel.TabStop = false;
            this.grpTravel.Text = "Fast Travel";
            // 
            // btnUltraBosses
            // 
            this.btnUltraBosses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnUltraBosses.BackColorUseGeneric = false;
            this.btnUltraBosses.Checked = false;
            this.btnUltraBosses.Location = new System.Drawing.Point(6, 422);
            this.btnUltraBosses.Name = "btnUltraBosses";
            this.btnUltraBosses.Size = new System.Drawing.Size(145, 23);
            this.btnUltraBosses.TabIndex = 17;
            this.btnUltraBosses.Text = "Ultra Bosses (timeinn)";
            this.btnUltraBosses.Click += new System.EventHandler(this.btnUltraBosses_Click);
            // 
            // btnRaxgore
            // 
            this.btnRaxgore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnRaxgore.BackColorUseGeneric = false;
            this.btnRaxgore.Checked = false;
            this.btnRaxgore.Location = new System.Drawing.Point(6, 395);
            this.btnRaxgore.Name = "btnRaxgore";
            this.btnRaxgore.Size = new System.Drawing.Size(145, 23);
            this.btnRaxgore.TabIndex = 16;
            this.btnRaxgore.Text = "Raxgore (doomvaultb)";
            this.btnRaxgore.Click += new System.EventHandler(this.btnRaxgore_Click);
            // 
            // btnDummy
            // 
            this.btnDummy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnDummy.BackColorUseGeneric = false;
            this.btnDummy.Checked = false;
            this.btnDummy.Location = new System.Drawing.Point(6, 368);
            this.btnDummy.Name = "btnDummy";
            this.btnDummy.Size = new System.Drawing.Size(145, 23);
            this.btnDummy.TabIndex = 15;
            this.btnDummy.Text = "Dummy (classhall)";
            this.btnDummy.Click += new System.EventHandler(this.btnDummy_Click);
            // 
            // btnFrostSpirit
            // 
            this.btnFrostSpirit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnFrostSpirit.BackColorUseGeneric = false;
            this.btnFrostSpirit.Checked = false;
            this.btnFrostSpirit.Location = new System.Drawing.Point(6, 341);
            this.btnFrostSpirit.Name = "btnFrostSpirit";
            this.btnFrostSpirit.Size = new System.Drawing.Size(145, 23);
            this.btnFrostSpirit.TabIndex = 14;
            this.btnFrostSpirit.Text = "Frost Spirit (ISA)";
            this.btnFrostSpirit.Click += new System.EventHandler(this.btnFrostSpirit_Click);
            // 
            // numPriv
            // 
            this.numPriv.Enabled = false;
            this.numPriv.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numPriv.Location = new System.Drawing.Point(64, 18);
            this.numPriv.LoopValues = false;
            this.numPriv.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPriv.Name = "numPriv";
            this.numPriv.Size = new System.Drawing.Size(87, 20);
            this.numPriv.TabIndex = 1;
            this.numPriv.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // btnPolish
            // 
            this.btnPolish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnPolish.BackColorUseGeneric = false;
            this.btnPolish.Checked = false;
            this.btnPolish.Location = new System.Drawing.Point(6, 179);
            this.btnPolish.Name = "btnPolish";
            this.btnPolish.Size = new System.Drawing.Size(145, 23);
            this.btnPolish.TabIndex = 8;
            this.btnPolish.Text = "Polish (tercess)";
            this.btnPolish.Click += new System.EventHandler(this.btnPolish_Click);
            // 
            // btnLae
            // 
            this.btnLae.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLae.BackColorUseGeneric = false;
            this.btnLae.Checked = false;
            this.btnLae.Location = new System.Drawing.Point(6, 233);
            this.btnLae.Name = "btnLae";
            this.btnLae.Size = new System.Drawing.Size(145, 23);
            this.btnLae.TabIndex = 10;
            this.btnLae.Text = "Lae (tercess)";
            this.btnLae.Click += new System.EventHandler(this.btnLae_Click);
            // 
            // btnCarnage
            // 
            this.btnCarnage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCarnage.BackColorUseGeneric = false;
            this.btnCarnage.Checked = false;
            this.btnCarnage.Location = new System.Drawing.Point(6, 206);
            this.btnCarnage.Name = "btnCarnage";
            this.btnCarnage.Size = new System.Drawing.Size(145, 23);
            this.btnCarnage.TabIndex = 9;
            this.btnCarnage.Text = "Carnage / Ninja (tercess)";
            this.btnCarnage.Click += new System.EventHandler(this.btnCarnage_Click);
            // 
            // chkPriv
            // 
            this.chkPriv.AutoSize = true;
            this.chkPriv.Location = new System.Drawing.Point(6, 19);
            this.chkPriv.Name = "chkPriv";
            this.chkPriv.Size = new System.Drawing.Size(58, 17);
            this.chkPriv.TabIndex = 2;
            this.chkPriv.Text = "Private";
            this.chkPriv.CheckedChanged += new System.EventHandler(this.chkPriv_CheckedChanged);
            // 
            // AweTravel
            // 
            this.AweTravel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.AweTravel.BackColorUseGeneric = false;
            this.AweTravel.Checked = false;
            this.AweTravel.Location = new System.Drawing.Point(6, 91);
            this.AweTravel.Name = "AweTravel";
            this.AweTravel.Size = new System.Drawing.Size(177, 23);
            this.AweTravel.TabIndex = 8;
            this.AweTravel.Text = "Awe Shop (museum)";
            this.AweTravel.Click += new System.EventHandler(this.AweTravel_Click);
            // 
            // aweGroup
            // 
            this.aweGroup.Controls.Add(this.panel1);
            this.aweGroup.Controls.Add(this.AweTravel);
            this.aweGroup.Location = new System.Drawing.Point(166, 336);
            this.aweGroup.Name = "aweGroup";
            this.aweGroup.Size = new System.Drawing.Size(189, 119);
            this.aweGroup.TabIndex = 9;
            this.aweGroup.TabStop = false;
            this.aweGroup.Text = "Awe Enhancement Shop";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 70);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.54099F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.45901F));
            this.tableLayoutPanel1.Controls.Add(this.aweLucky, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.aweHybrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.aweHealer, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.aweFigther, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.aweThief, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.aweWizard, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(177, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // aweLucky
            // 
            this.aweLucky.AutoSize = true;
            this.aweLucky.Checked = true;
            this.aweLucky.Location = new System.Drawing.Point(3, 3);
            this.aweLucky.Name = "aweLucky";
            this.aweLucky.Size = new System.Drawing.Size(54, 17);
            this.aweLucky.TabIndex = 9;
            this.aweLucky.TabStop = true;
            this.aweLucky.Text = "Lucky";
            // 
            // aweHybrid
            // 
            this.aweHybrid.AutoSize = true;
            this.aweHybrid.Location = new System.Drawing.Point(3, 26);
            this.aweHybrid.Name = "aweHybrid";
            this.aweHybrid.Size = new System.Drawing.Size(55, 17);
            this.aweHybrid.TabIndex = 14;
            this.aweHybrid.Text = "Hybrid";
            // 
            // aweHealer
            // 
            this.aweHealer.AutoSize = true;
            this.aweHealer.Location = new System.Drawing.Point(87, 26);
            this.aweHealer.Name = "aweHealer";
            this.aweHealer.Size = new System.Drawing.Size(56, 17);
            this.aweHealer.TabIndex = 15;
            this.aweHealer.Text = "Healer";
            // 
            // aweFigther
            // 
            this.aweFigther.AutoSize = true;
            this.aweFigther.Location = new System.Drawing.Point(3, 49);
            this.aweFigther.Name = "aweFigther";
            this.aweFigther.Size = new System.Drawing.Size(57, 17);
            this.aweFigther.TabIndex = 11;
            this.aweFigther.Text = "Fighter";
            // 
            // aweThief
            // 
            this.aweThief.AutoSize = true;
            this.aweThief.Location = new System.Drawing.Point(87, 3);
            this.aweThief.Name = "aweThief";
            this.aweThief.Size = new System.Drawing.Size(49, 17);
            this.aweThief.TabIndex = 13;
            this.aweThief.Text = "Thief";
            // 
            // aweWizard
            // 
            this.aweWizard.AutoSize = true;
            this.aweWizard.Location = new System.Drawing.Point(87, 49);
            this.aweWizard.Name = "aweWizard";
            this.aweWizard.Size = new System.Drawing.Size(58, 17);
            this.aweWizard.TabIndex = 10;
            this.aweWizard.Text = "Wizard";
            // 
            // grpCustomTravel
            // 
            this.grpCustomTravel.Controls.Add(this.lblCustomTravelTip);
            this.grpCustomTravel.Controls.Add(this.cbCustomTravels);
            this.grpCustomTravel.Location = new System.Drawing.Point(166, 5);
            this.grpCustomTravel.Name = "grpCustomTravel";
            this.grpCustomTravel.Size = new System.Drawing.Size(189, 76);
            this.grpCustomTravel.TabIndex = 10;
            this.grpCustomTravel.TabStop = false;
            this.grpCustomTravel.Text = "Custom Travel";
            // 
            // lblCustomTravelTip
            // 
            this.lblCustomTravelTip.Location = new System.Drawing.Point(3, 17);
            this.lblCustomTravelTip.Name = "lblCustomTravelTip";
            this.lblCustomTravelTip.Size = new System.Drawing.Size(183, 30);
            this.lblCustomTravelTip.TabIndex = 20;
            this.lblCustomTravelTip.Text = "Press the ▶ button to join a map based on the selected travel target.";
            // 
            // cbCustomTravels
            // 
            this.cbCustomTravels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCustomTravels.FormattingEnabled = true;
            this.cbCustomTravels.Location = new System.Drawing.Point(6, 50);
            this.cbCustomTravels.MaxDropDownItems = 50;
            this.cbCustomTravels.Name = "cbCustomTravels";
            this.cbCustomTravels.Size = new System.Drawing.Size(177, 21);
            this.cbCustomTravels.TabIndex = 19;
            this.cbCustomTravels.SelectedIndexChanged += new System.EventHandler(this.cbCustomTravels_SelectedIndexChanged);
            this.cbCustomTravels.Click += new System.EventHandler(this.cbCustomTravels_Click);
            // 
            // txtCustomTravelName
            // 
            this.txtCustomTravelName.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtCustomTravelName.Location = new System.Drawing.Point(6, 28);
            this.txtCustomTravelName.Name = "txtCustomTravelName";
            this.txtCustomTravelName.Size = new System.Drawing.Size(162, 20);
            this.txtCustomTravelName.TabIndex = 0;
            this.txtCustomTravelName.Text = "Travel name";
            // 
            // txtCustomPad
            // 
            this.txtCustomPad.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtCustomPad.Location = new System.Drawing.Point(96, 74);
            this.txtCustomPad.Name = "txtCustomPad";
            this.txtCustomPad.Size = new System.Drawing.Size(87, 20);
            this.txtCustomPad.TabIndex = 3;
            this.txtCustomPad.Text = "Pad";
            // 
            // txtCustomCell
            // 
            this.txtCustomCell.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtCustomCell.Location = new System.Drawing.Point(6, 74);
            this.txtCustomCell.Name = "txtCustomCell";
            this.txtCustomCell.Size = new System.Drawing.Size(87, 20);
            this.txtCustomCell.TabIndex = 2;
            this.txtCustomCell.Text = "Cell";
            // 
            // txtCustomMap
            // 
            this.txtCustomMap.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtCustomMap.Location = new System.Drawing.Point(6, 51);
            this.txtCustomMap.Name = "txtCustomMap";
            this.txtCustomMap.Size = new System.Drawing.Size(177, 20);
            this.txtCustomMap.TabIndex = 1;
            this.txtCustomMap.Text = "Map name with room number";
            // 
            // grpCustomTravel2
            // 
            this.grpCustomTravel2.Controls.Add(this.rBtnShow);
            this.grpCustomTravel2.Controls.Add(this.rBtnRemove);
            this.grpCustomTravel2.Controls.Add(this.rBtnAdd);
            this.grpCustomTravel2.Controls.Add(this.btnGo);
            this.grpCustomTravel2.Controls.Add(this.txtCustomTravelName);
            this.grpCustomTravel2.Controls.Add(this.txtCustomPad);
            this.grpCustomTravel2.Controls.Add(this.txtCustomCell);
            this.grpCustomTravel2.Controls.Add(this.txtCustomMap);
            this.grpCustomTravel2.Location = new System.Drawing.Point(166, 80);
            this.grpCustomTravel2.Name = "grpCustomTravel2";
            this.grpCustomTravel2.Size = new System.Drawing.Size(189, 100);
            this.grpCustomTravel2.TabIndex = 20;
            this.grpCustomTravel2.TabStop = false;
            // 
            // rBtnShow
            // 
            this.rBtnShow.AutoSize = true;
            this.rBtnShow.Location = new System.Drawing.Point(6, 5);
            this.rBtnShow.Name = "rBtnShow";
            this.rBtnShow.Size = new System.Drawing.Size(52, 17);
            this.rBtnShow.TabIndex = 12;
            this.rBtnShow.Text = "Show";
            this.rBtnShow.CheckedChanged += new System.EventHandler(this.rBtnShow_CheckedChanged);
            // 
            // rBtnRemove
            // 
            this.rBtnRemove.AutoSize = true;
            this.rBtnRemove.Location = new System.Drawing.Point(114, 5);
            this.rBtnRemove.Name = "rBtnRemove";
            this.rBtnRemove.Size = new System.Drawing.Size(65, 17);
            this.rBtnRemove.TabIndex = 11;
            this.rBtnRemove.Text = "Remove";
            this.rBtnRemove.CheckedChanged += new System.EventHandler(this.rBtnRemove_CheckedChanged);
            // 
            // rBtnAdd
            // 
            this.rBtnAdd.AutoSize = true;
            this.rBtnAdd.Location = new System.Drawing.Point(64, 5);
            this.rBtnAdd.Name = "rBtnAdd";
            this.rBtnAdd.Size = new System.Drawing.Size(44, 17);
            this.rBtnAdd.TabIndex = 10;
            this.rBtnAdd.Text = "Add";
            this.rBtnAdd.CheckedChanged += new System.EventHandler(this.rBtnAdd_CheckedChanged);
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnGo.BackColorUseGeneric = false;
            this.btnGo.Checked = false;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnGo.Location = new System.Drawing.Point(167, 28);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(16, 20);
            this.btnGo.TabIndex = 8;
            this.btnGo.Text = "▶";
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // grpCustomTravel3
            // 
            this.grpCustomTravel3.Controls.Add(this.chkCustomHotkeys);
            this.grpCustomTravel3.Controls.Add(this.lblCustomHotkeyTip1);
            this.grpCustomTravel3.Controls.Add(this.lblCustomHotkeyTip4);
            this.grpCustomTravel3.Controls.Add(this.lblCustomHotkeyTip3);
            this.grpCustomTravel3.Controls.Add(this.lblCustomHotkeyTip2);
            this.grpCustomTravel3.Location = new System.Drawing.Point(166, 179);
            this.grpCustomTravel3.Name = "grpCustomTravel3";
            this.grpCustomTravel3.Size = new System.Drawing.Size(189, 94);
            this.grpCustomTravel3.TabIndex = 21;
            this.grpCustomTravel3.TabStop = false;
            // 
            // chkCustomHotkeys
            // 
            this.chkCustomHotkeys.AutoSize = true;
            this.chkCustomHotkeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomHotkeys.Location = new System.Drawing.Point(6, 4);
            this.chkCustomHotkeys.Name = "chkCustomHotkeys";
            this.chkCustomHotkeys.Size = new System.Drawing.Size(71, 17);
            this.chkCustomHotkeys.TabIndex = 7;
            this.chkCustomHotkeys.Text = "Hotkeys";
            this.chkCustomHotkeys.CheckedChanged += new System.EventHandler(this.chkCustomHotkeys_CheckedChanged);
            // 
            // lblCustomHotkeyTip1
            // 
            this.lblCustomHotkeyTip1.AutoSize = true;
            this.lblCustomHotkeyTip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCustomHotkeyTip1.Location = new System.Drawing.Point(3, 24);
            this.lblCustomHotkeyTip1.Name = "lblCustomHotkeyTip1";
            this.lblCustomHotkeyTip1.Size = new System.Drawing.Size(172, 13);
            this.lblCustomHotkeyTip1.TabIndex = 4;
            this.lblCustomHotkeyTip1.Text = "SHIFT + ▶: Immediate travel target";
            // 
            // lblCustomHotkeyTip4
            // 
            this.lblCustomHotkeyTip4.AutoSize = true;
            this.lblCustomHotkeyTip4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCustomHotkeyTip4.Location = new System.Drawing.Point(3, 75);
            this.lblCustomHotkeyTip4.Name = "lblCustomHotkeyTip4";
            this.lblCustomHotkeyTip4.Size = new System.Drawing.Size(127, 13);
            this.lblCustomHotkeyTip4.TabIndex = 3;
            this.lblCustomHotkeyTip4.Text = "SHIFT + ◀: Cancel travel";
            // 
            // lblCustomHotkeyTip3
            // 
            this.lblCustomHotkeyTip3.AutoSize = true;
            this.lblCustomHotkeyTip3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCustomHotkeyTip3.Location = new System.Drawing.Point(3, 59);
            this.lblCustomHotkeyTip3.Name = "lblCustomHotkeyTip3";
            this.lblCustomHotkeyTip3.Size = new System.Drawing.Size(167, 13);
            this.lblCustomHotkeyTip3.TabIndex = 2;
            this.lblCustomHotkeyTip3.Text = "SHIFT + ▼: Switch to lower target";
            // 
            // lblCustomHotkeyTip2
            // 
            this.lblCustomHotkeyTip2.AutoSize = true;
            this.lblCustomHotkeyTip2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCustomHotkeyTip2.Location = new System.Drawing.Point(3, 40);
            this.lblCustomHotkeyTip2.Name = "lblCustomHotkeyTip2";
            this.lblCustomHotkeyTip2.Size = new System.Drawing.Size(169, 13);
            this.lblCustomHotkeyTip2.TabIndex = 1;
            this.lblCustomHotkeyTip2.Text = "SHIFT + ▲: Switch to upper target";
            // 
            // lblCustomChatTriggerTip1
            // 
            this.lblCustomChatTriggerTip1.AutoSize = true;
            this.lblCustomChatTriggerTip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCustomChatTriggerTip1.Location = new System.Drawing.Point(3, 24);
            this.lblCustomChatTriggerTip1.Name = "lblCustomChatTriggerTip1";
            this.lblCustomChatTriggerTip1.Size = new System.Drawing.Size(171, 26);
            this.lblCustomChatTriggerTip1.TabIndex = 5;
            this.lblCustomChatTriggerTip1.Text = ".ct[index]: Immediate join on a \r\n                 travel target (ex. \".ct1\").";
            // 
            // darkGroupBox1
            // 
            this.darkGroupBox1.Controls.Add(this.chkCustomChatTrigger);
            this.darkGroupBox1.Controls.Add(this.lblCustomChatTriggerTip1);
            this.darkGroupBox1.Location = new System.Drawing.Point(166, 272);
            this.darkGroupBox1.Name = "darkGroupBox1";
            this.darkGroupBox1.Size = new System.Drawing.Size(189, 58);
            this.darkGroupBox1.TabIndex = 22;
            this.darkGroupBox1.TabStop = false;
            // 
            // chkCustomChatTrigger
            // 
            this.chkCustomChatTrigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomChatTrigger.Location = new System.Drawing.Point(6, 4);
            this.chkCustomChatTrigger.Name = "chkCustomChatTrigger";
            this.chkCustomChatTrigger.Size = new System.Drawing.Size(177, 17);
            this.chkCustomChatTrigger.TabIndex = 6;
            this.chkCustomChatTrigger.Text = "Chat Trigger (Type In-game)";
            this.chkCustomChatTrigger.CheckedChanged += new System.EventHandler(this.chkCustomChatTrigger_CheckedChanged);
            // 
            // Travel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(359, 459);
            this.Controls.Add(this.darkGroupBox1);
            this.Controls.Add(this.grpCustomTravel3);
            this.Controls.Add(this.grpCustomTravel2);
            this.Controls.Add(this.grpCustomTravel);
            this.Controls.Add(this.aweGroup);
            this.Controls.Add(this.grpTravel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Travel";
            this.Text = "Fast Travels";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Travel_FormClosing);
            this.grpTravel.ResumeLayout(false);
            this.grpTravel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriv)).EndInit();
            this.aweGroup.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpCustomTravel.ResumeLayout(false);
            this.grpCustomTravel2.ResumeLayout(false);
            this.grpCustomTravel2.PerformLayout();
            this.grpCustomTravel3.ResumeLayout(false);
            this.grpCustomTravel3.PerformLayout();
            this.darkGroupBox1.ResumeLayout(false);
            this.darkGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        static Travel()
        {
            Instance = new Travel();
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
                    CreateJoinCommand("museum"),
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
                BotClientConfig c = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");
                string loadedTravels;
                try
                {
                    loadedTravels = c.Get("customTravels");

                }
                catch { loadedTravels = ""; }
                string[] tempTravels = string.IsNullOrEmpty(loadedTravels) ? new string[0] : loadedTravels.Split('`');
                string newTravels = loadedTravels + (tempTravels.Length > 1 ? "|" : "") + $"{txtCustomTravelName.Text}`{txtCustomMap.Text}`{txtCustomCell.Text}`{txtCustomPad.Text}";
                c.Set("customTravels", newTravels);
                c.Save();
                scanCustomTravel();
                cbCustomTravels.SelectedIndex = 0;
            }
            else if (rBtnRemove.Checked)
            {
                BotClientConfig c = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");
                int targetedIndex = travelNames.IndexOf(txtCustomTravelName.Text);
                if (targetedIndex > -1)
                {
                    travels.RemoveAt(targetedIndex);
                    string newTravels = string.Join("|", travels.ToArray());
                    c.Set("customTravels", newTravels);
                    c.Save();
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

        public List<string> travels { get; set; } = new List<string>();
        private List<string> travelNames { get; set; } = new List<string>();

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

        private void chkCustomHotkeys_CheckedChanged(object sender, EventArgs e)
        {
            BotClientConfig c = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");
            c.Set("customTravelHotkeys", chkCustomHotkeys.Checked.ToString());
            c.Save();
        }

        private void chkCustomChatTrigger_CheckedChanged(object sender, EventArgs e)
        {
            BotClientConfig c = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");
            c.Set("customTravelTrigger", chkCustomChatTrigger.Checked.ToString());
            c.Save();
            if (chkCustomChatTrigger.Checked)
            {
                Flash.Call("LoadTravelTriggers", c.Get("customTravels"));
            }
        }

        public void scanCustomTravel()
        {
            if (travels.Count > 0)
                travels.Clear();
            if (cbCustomTravels.Items.Count > 0)
                cbCustomTravels.Items.Clear();
            if (travelNames.Count > 0)
                travelNames.Clear();
            BotClientConfig c = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");
            string tempTravels;
            try
            {
                tempTravels = c.Get("customTravels");
            }
            catch { tempTravels = ""; }
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

    }
}