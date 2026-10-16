using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Item;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Extensions = Grimoire.Botting.Extensions;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        private void btnPacket_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdPacket2
            {
                Packet = txtPacket.Text,
                Delay = (int)numPacketDelay.Value,
                Client = false
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnDelay_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdDelay
            {
                Delay = (int)numDelay.Value
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            string playerName = txtPlayer.Text;
            if (playerName.Length > 0)
            {
                AddCommand(new CmdGotoPlayer
                {
                    PlayerName = playerName
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnBotDelay_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdBotDelay2
            {
                Delay = ((int)numBotDelay.Value).ToString()
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

        private void btnLoadCmd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select bot to load";
                openFileDialog.Filter = "Grimoire bots|*.gbot";
                openFileDialog.InitialDirectory = Config.Instance.GetValue<string>("botsDirectory") ?? Path.Combine(Application.StartupPath, "Bots");
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
                StatementCommand statementCommand = (StatementCommand)Activator.CreateInstance(cbStatement.SelectedItem.GetType());
                statementCommand.Value1 = txtStatement1.Text;
                statementCommand.Value2 = txtStatement2.Text;
                AddCommand((IBotCommand)statementCommand, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex > -1)
            {
                if (cbStatement.Items.Count > 0)
                {
                    cbStatement.Items.Clear();
                }
                cbStatement.Items.AddRange(_statementCommands.Where((StatementCommand s) => s.Tag == cbCategories.SelectedItem.ToString()).ToArray());
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
                if (lbLabels.Items.IndexOf($"[{txtLabel.Text.ToUpper()}]") > -1)
                {
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "A label with the same name has already been added to the Command list.", "Add Label", MessageBoxIcon.Error);
                    return;
                }
                AddCommand(new CmdLabel
                {
                    Name = txtLabel.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
            GetAllCommands<CmdLabel>(lbLabels);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdLogout(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnProvokeOn_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdToggleProvoke
            {
                Type = 1,
                ProvokePacket = chkInMapCustom.Checked ? txtCustomAggromon.Text : null
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnProvokeOff_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdToggleProvoke
            {
                Type = 0
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnBlank_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdBlank3 { Text = "", Alpha = 0, R = 220, G = 220, B = 220 }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnBeep_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdBeep
            {
                Times = (int)numBeepTimes.Value
            }, (ModifierKeys & Keys.Control) == Keys.Control);
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

        private void btnClientPacket_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdPacket2
            {
                Packet = txtPacket.Text,
                Delay = (int)numPacketDelay.Value,
                Client = true
            }, (ModifierKeys & Keys.Control) == Keys.Control);
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

        private void chkInMapCustom_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomAggromon.Enabled = chkInMapCustom.Checked;
        }

        private void btnPacketSpamOn_Click(object sender, EventArgs e)
        {
            string txtPacketSpam = txtPacket.Text;
            AddCommand(new CmdPacketlist2
            {
                Packet = txtPacketSpam,
                Delay = (int)numPacketDelay.Value,
                Type = 1
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnPacketSpamOff_Click(object sender, EventArgs e)
        {
            string txtPacketSpam = txtPacket.Text;
            AddCommand(new CmdPacketlist2
            {
                Packet = "",
                Delay = 0,
                Type = 0
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        #endregion
    }
}
