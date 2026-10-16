using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting.Commands.Map;
using Grimoire.Game;
using Grimoire.Tools;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        private void btnJoin_Click(object sender, EventArgs e)
        {
            string mapName = string.IsNullOrEmpty(txtJoin.Text) ? "-" : txtJoin.Text;
            string[] split = mapName.Split('-');
            if (!mapName.Contains('-'))
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                    "Failed to add the Join Map command. You have to insert the map name AND the room number (ex. \"battleon-1e99\") in order to add the command.", "Join Map Command", MessageBoxIcon.Error);
                return;
            }
            if (mapName.Length > 0)
            {
                AddCommand(new CmdJoin2
                {
                    Map = string.IsNullOrEmpty(split[0]) ? "battleon" : split[0],
                    Room = string.IsNullOrEmpty(split[1]) ? "1e99" : split[1],
                    Cell = string.IsNullOrEmpty(txtJoinCell.Text) ? "Enter" : txtJoinCell.Text,
                    Pad = string.IsNullOrEmpty(txtJoinPad.Text) ? "Spawn" : txtJoinPad.Text
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
            if (Player.IsLoggedIn)
            {
                txtCell.Text = Player.Cell;
                txtPad.Text = Player.Pad;
            }
        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdMoveToCell
            {
                Cell = string.IsNullOrEmpty(txtCell.Text) ? "Enter" : txtCell.Text,
                Pad = string.IsNullOrEmpty(txtPad.Text) ? "Spawn" : txtPad.Text
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWalk_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWalk
            {
                X = numWalkX.Value.ToString(),
                Y = numWalkY.Value.ToString()
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
            AddCommand(new CmdWalk
            {
                Type = "Random",
                X = numWalkX.Value.ToString(),
                Y = numWalkY.Value.ToString()
            }, (ModifierKeys & Keys.Control) == Keys.Control);
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

        #endregion
    }
}
