using Grimoire.Botting;
using Grimoire.Botting.Commands.Map;
using Grimoire.Botting.Commands.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using DarkUI.Forms;
using DarkUI.Controls;

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
        private RadioButton aweWizard;
        private RadioButton aweLucky;
        private DarkPanel panel1;
        private RadioButton aweThief;
        private RadioButton aweHybrid;
        private RadioButton aweHealer;
        private TableLayoutPanel tableLayoutPanel1;
        private RadioButton aweFigther;
        private DarkGroupBox grpChatTrigger;
        private DarkLabel label5;
        private RichTextBox rtbChatTrigger;
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
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim")
            });
        }

        private void btnTwins_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "Twins", "Left")
            });
        }

        private void btnTaro_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "Taro", "Left")
            });
        }

        private void btnSwindle_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "Swindle", "Left")
            });
        }

        private void btnNulgath_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "Boss2", "Right")
            });
        }

        private void btnEscherion_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("escherion", "Boss", "Left")
            });
        }

        private void btnDage_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("underworld", "s1", "Left")
            });
        }

        private CmdTravel CreateJoinCommand(string map, string cell = "Enter", string pad = "Spawn")
        {
            return new CmdTravel
            {
                Map = chkPriv.Checked ? (map + $"-{numPriv.Value}") : map,
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
            aweGroup.Enabled  = false;
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
                    aweGroup.Enabled  = true;
                });
            }
            else
            {
                grpTravel.Enabled = true;
                aweGroup.Enabled  = true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Travel));
            this.btnDage = new DarkUI.Controls.DarkButton();
            this.btnEscherion = new DarkUI.Controls.DarkButton();
            this.btnBinky = new DarkUI.Controls.DarkButton();
            this.btnNulgath = new DarkUI.Controls.DarkButton();
            this.btnSwindle = new DarkUI.Controls.DarkButton();
            this.btnTaro = new DarkUI.Controls.DarkButton();
            this.btnTwins = new DarkUI.Controls.DarkButton();
            this.btnTercess = new DarkUI.Controls.DarkButton();
            this.grpTravel = new DarkUI.Controls.DarkGroupBox();
            this.numPriv = new DarkUI.Controls.DarkNumericUpDown();
            this.btnPolish = new DarkUI.Controls.DarkButton();
            this.btnLae = new DarkUI.Controls.DarkButton();
            this.btnCarnage = new DarkUI.Controls.DarkButton();
            this.chkPriv = new DarkUI.Controls.DarkCheckBox();
            this.AweTravel = new DarkUI.Controls.DarkButton();
            this.aweGroup = new DarkUI.Controls.DarkGroupBox();
            this.panel1 = new DarkUI.Controls.DarkPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.aweLucky = new System.Windows.Forms.RadioButton();
            this.aweHybrid = new System.Windows.Forms.RadioButton();
            this.aweHealer = new System.Windows.Forms.RadioButton();
            this.aweFigther = new System.Windows.Forms.RadioButton();
            this.aweThief = new System.Windows.Forms.RadioButton();
            this.aweWizard = new System.Windows.Forms.RadioButton();
            this.grpChatTrigger = new DarkUI.Controls.DarkGroupBox();
            this.label5 = new DarkUI.Controls.DarkLabel();
            this.rtbChatTrigger = new System.Windows.Forms.RichTextBox();
            this.grpTravel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriv)).BeginInit();
            this.aweGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpChatTrigger.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDage
            // 
            this.btnDage.Checked = false;
            this.btnDage.Location = new System.Drawing.Point(6, 305);
            this.btnDage.Name = "btnDage";
            this.btnDage.Size = new System.Drawing.Size(145, 23);
            this.btnDage.TabIndex = 0;
            this.btnDage.Text = "Dage (underworld)";
            this.btnDage.Click += new System.EventHandler(this.btnDage_Click);
            // 
            // btnEscherion
            // 
            this.btnEscherion.Checked = false;
            this.btnEscherion.Location = new System.Drawing.Point(6, 334);
            this.btnEscherion.Name = "btnEscherion";
            this.btnEscherion.Size = new System.Drawing.Size(145, 23);
            this.btnEscherion.TabIndex = 1;
            this.btnEscherion.Text = "Escherion";
            this.btnEscherion.Click += new System.EventHandler(this.btnEscherion_Click);
            // 
            // btnBinky
            // 
            this.btnBinky.Checked = false;
            this.btnBinky.Location = new System.Drawing.Point(6, 276);
            this.btnBinky.Name = "btnBinky";
            this.btnBinky.Size = new System.Drawing.Size(145, 23);
            this.btnBinky.TabIndex = 2;
            this.btnBinky.Text = "Binky (doomvault)";
            this.btnBinky.Click += new System.EventHandler(this.btnBinky_Click);
            // 
            // btnNulgath
            // 
            this.btnNulgath.Checked = false;
            this.btnNulgath.Location = new System.Drawing.Point(6, 131);
            this.btnNulgath.Name = "btnNulgath";
            this.btnNulgath.Size = new System.Drawing.Size(145, 23);
            this.btnNulgath.TabIndex = 3;
            this.btnNulgath.Text = "Nulgath / Skew (tercess)";
            this.btnNulgath.Click += new System.EventHandler(this.btnNulgath_Click);
            // 
            // btnSwindle
            // 
            this.btnSwindle.Checked = false;
            this.btnSwindle.Location = new System.Drawing.Point(6, 160);
            this.btnSwindle.Name = "btnSwindle";
            this.btnSwindle.Size = new System.Drawing.Size(145, 23);
            this.btnSwindle.TabIndex = 4;
            this.btnSwindle.Text = "Swindle (tercess)";
            this.btnSwindle.Click += new System.EventHandler(this.btnSwindle_Click);
            // 
            // btnTaro
            // 
            this.btnTaro.Checked = false;
            this.btnTaro.Location = new System.Drawing.Point(6, 102);
            this.btnTaro.Name = "btnTaro";
            this.btnTaro.Size = new System.Drawing.Size(145, 23);
            this.btnTaro.TabIndex = 5;
            this.btnTaro.Text = "VHL / Taro / Zee (tercess)";
            this.btnTaro.Click += new System.EventHandler(this.btnTaro_Click);
            // 
            // btnTwins
            // 
            this.btnTwins.Checked = false;
            this.btnTwins.Location = new System.Drawing.Point(6, 73);
            this.btnTwins.Name = "btnTwins";
            this.btnTwins.Size = new System.Drawing.Size(145, 23);
            this.btnTwins.TabIndex = 6;
            this.btnTwins.Text = "Twins (tercess)";
            this.btnTwins.Click += new System.EventHandler(this.btnTwins_Click);
            // 
            // btnTercess
            // 
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
            this.grpTravel.Size = new System.Drawing.Size(157, 378);
            this.grpTravel.TabIndex = 8;
            this.grpTravel.TabStop = false;
            this.grpTravel.Text = "Fast travels";
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
            this.btnPolish.Checked = false;
            this.btnPolish.Location = new System.Drawing.Point(6, 189);
            this.btnPolish.Name = "btnPolish";
            this.btnPolish.Size = new System.Drawing.Size(145, 23);
            this.btnPolish.TabIndex = 0;
            this.btnPolish.Text = "Polish (tercess)";
            this.btnPolish.Click += new System.EventHandler(this.btnPolish_Click);
            // 
            // btnLae
            // 
            this.btnLae.Checked = false;
            this.btnLae.Location = new System.Drawing.Point(6, 247);
            this.btnLae.Name = "btnLae";
            this.btnLae.Size = new System.Drawing.Size(145, 23);
            this.btnLae.TabIndex = 0;
            this.btnLae.Text = "Lae (tercess)";
            this.btnLae.Click += new System.EventHandler(this.btnLae_Click);
            // 
            // btnCarnage
            // 
            this.btnCarnage.Checked = false;
            this.btnCarnage.Location = new System.Drawing.Point(6, 218);
            this.btnCarnage.Name = "btnCarnage";
            this.btnCarnage.Size = new System.Drawing.Size(145, 23);
            this.btnCarnage.TabIndex = 0;
            this.btnCarnage.Text = "Carnage / Ninja (tercess)";
            this.btnCarnage.Click += new System.EventHandler(this.btnCarnage_Click);
            // 
            // chkPriv
            // 
            this.chkPriv.AutoSize = true;
            this.chkPriv.Location = new System.Drawing.Point(6, 19);
            this.chkPriv.Name = "chkPriv";
            this.chkPriv.Size = new System.Drawing.Size(58, 17);
            this.chkPriv.TabIndex = 0;
            this.chkPriv.Text = "Private";
            this.chkPriv.CheckedChanged += new System.EventHandler(this.chkPriv_CheckedChanged);
            // 
            // AweTravel
            // 
            this.AweTravel.Checked = false;
            this.AweTravel.Location = new System.Drawing.Point(3, 88);
            this.AweTravel.Name = "AweTravel";
            this.AweTravel.Size = new System.Drawing.Size(183, 23);
            this.AweTravel.TabIndex = 8;
            this.AweTravel.Text = "Awe Shop (museum)";
            this.AweTravel.Click += new System.EventHandler(this.AweTravel_Click);
            // 
            // aweGroup
            // 
            this.aweGroup.Controls.Add(this.panel1);
            this.aweGroup.Controls.Add(this.AweTravel);
            this.aweGroup.Location = new System.Drawing.Point(166, 5);
            this.aweGroup.Name = "aweGroup";
            this.aweGroup.Size = new System.Drawing.Size(189, 115);
            this.aweGroup.TabIndex = 9;
            this.aweGroup.TabStop = false;
            this.aweGroup.Text = "Awe Enchantment Shop";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 70);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.40506F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.59494F));
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(183, 70);
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
            this.aweLucky.UseVisualStyleBackColor = true;
            // 
            // aweHybrid
            // 
            this.aweHybrid.AutoSize = true;
            this.aweHybrid.Location = new System.Drawing.Point(3, 26);
            this.aweHybrid.Name = "aweHybrid";
            this.aweHybrid.Size = new System.Drawing.Size(55, 17);
            this.aweHybrid.TabIndex = 14;
            this.aweHybrid.Text = "Hybrid";
            this.aweHybrid.UseVisualStyleBackColor = true;
            // 
            // aweHealer
            // 
            this.aweHealer.AutoSize = true;
            this.aweHealer.Location = new System.Drawing.Point(80, 26);
            this.aweHealer.Name = "aweHealer";
            this.aweHealer.Size = new System.Drawing.Size(56, 17);
            this.aweHealer.TabIndex = 15;
            this.aweHealer.Text = "Healer";
            this.aweHealer.UseVisualStyleBackColor = true;
            // 
            // aweFigther
            // 
            this.aweFigther.AutoSize = true;
            this.aweFigther.Location = new System.Drawing.Point(3, 49);
            this.aweFigther.Name = "aweFigther";
            this.aweFigther.Size = new System.Drawing.Size(57, 17);
            this.aweFigther.TabIndex = 11;
            this.aweFigther.Text = "Fighter";
            this.aweFigther.UseVisualStyleBackColor = true;
            // 
            // aweThief
            // 
            this.aweThief.AutoSize = true;
            this.aweThief.Location = new System.Drawing.Point(80, 3);
            this.aweThief.Name = "aweThief";
            this.aweThief.Size = new System.Drawing.Size(49, 17);
            this.aweThief.TabIndex = 13;
            this.aweThief.Text = "Thief";
            this.aweThief.UseVisualStyleBackColor = true;
            // 
            // aweWizard
            // 
            this.aweWizard.AutoSize = true;
            this.aweWizard.Location = new System.Drawing.Point(80, 49);
            this.aweWizard.Name = "aweWizard";
            this.aweWizard.Size = new System.Drawing.Size(58, 17);
            this.aweWizard.TabIndex = 10;
            this.aweWizard.Text = "Wizard";
            this.aweWizard.UseVisualStyleBackColor = true;
            // 
            // grpChatTrigger
            // 
            this.grpChatTrigger.Controls.Add(this.rtbChatTrigger);
            this.grpChatTrigger.Controls.Add(this.label5);
            this.grpChatTrigger.Location = new System.Drawing.Point(166, 126);
            this.grpChatTrigger.Name = "grpChatTrigger";
            this.grpChatTrigger.Size = new System.Drawing.Size(189, 257);
            this.grpChatTrigger.TabIndex = 160;
            this.grpChatTrigger.TabStop = false;
            this.grpChatTrigger.Text = "Chat Trigger";
            this.grpChatTrigger.Enter += new System.EventHandler(this.grpChatTrigger_Enter);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label5.Location = new System.Drawing.Point(3, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 26);
            this.label5.TabIndex = 155;
            this.label5.Text = "Type a chat trigger (.) in-game for fast\r\ntravel (private room):";
            // 
            // rtbChatTrigger
            // 
            this.rtbChatTrigger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.rtbChatTrigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbChatTrigger.DetectUrls = false;
            this.rtbChatTrigger.ForeColor = System.Drawing.Color.Gainsboro;
            this.rtbChatTrigger.Location = new System.Drawing.Point(3, 48);
            this.rtbChatTrigger.Name = "rtbChatTrigger";
            this.rtbChatTrigger.ReadOnly = true;
            this.rtbChatTrigger.Size = new System.Drawing.Size(183, 207);
            this.rtbChatTrigger.TabIndex = 156;
            this.rtbChatTrigger.Text = resources.GetString("rtbChatTrigger.Text");
            this.rtbChatTrigger.WordWrap = false;
            this.rtbChatTrigger.TextChanged += new System.EventHandler(this.rtbChatTrigger_TextChanged);
            // 
            // Travel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(359, 387);
            this.Controls.Add(this.grpChatTrigger);
            this.Controls.Add(this.aweGroup);
            this.Controls.Add(this.grpTravel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Travel";
            this.Text = "Fast travels";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Travel_FormClosing);
            this.grpTravel.ResumeLayout(false);
            this.grpTravel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriv)).EndInit();
            this.aweGroup.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpChatTrigger.ResumeLayout(false);
            this.grpChatTrigger.PerformLayout();
            this.ResumeLayout(false);

        }

        static Travel()
        {
            Instance = new Travel();
        }

        private void btnBinky_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("doomvault", "r5", "Left")
            });
        }

        private void btnCarnage_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "m4", "Top")
            });
        }

        private void btnLae_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "m5", "Top")
            });
        }

        private void btnPolish_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "m12", "Top")
            });
        }

        private void AweTravel_Click(object sender, EventArgs e)
        {
            // Sorry Satan :*
            bool IsLucky   = aweLucky.Checked;
            bool IsWizard  = aweWizard.Checked;
            bool IsHybrid  = aweHybrid.Checked;
            bool IsThief   = aweThief.Checked;
            bool IsFighter = aweFigther.Checked;
            bool IsHealer  = aweHealer.Checked;

            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("museum"),
                
                // Staircase to hell.
                // Sorry Satan :3
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

        private void grpChatTrigger_Enter(object sender, EventArgs e)
        {

        }

        private void rtbChatTrigger_TextChanged(object sender, EventArgs e)
        {

        }
    }
}