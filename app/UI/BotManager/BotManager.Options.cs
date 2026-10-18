using Grimoire.Botting;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Tools;
using Grimoire.Utils;
using System;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        public void changeGenderAsync(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn)
            {
                return;
            }
            string gender = Flash.Call<string>("Gender", new string[0]);
            gender = (!gender.Contains("M")) ? "M" : "F";
            string data = $"{{\"t\":\"xt\",\"b\":{{\"r\":-1,\"o\":{{\"cmd\":\"genderSwap\",\"bitSuccess\":1,\"gender\":\"{gender}\",\"intCoins\":0,\"uid\":\"{Flash.Call<int>("UserID", new string[0])}\",\"strHairFileName\":\"\",\"HairID\":\"\",\"strHairName\":\"\"}}}}}}";
            _ = Proxy.Instance.SendToClient(data);
        }

        private void btnSoundAdd_Click(object sender, EventArgs e)
        {
            if (txtSoundItem.TextLength > 0)
            {
                lstSoundItems.Items.Add(txtSoundItem.Text.UnifyQuotes());
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
            OptionsManager.BotOptions.InfiniteRange = chkInfiniteRange.Checked;
            OptionsManager.Refresh();
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
            OptionsManager.BotOptions.ProvokeMonsters = chkProvoke.Checked;
            OptionsManager.Refresh();
        }

        private void chkMagnet_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.BotOptions.EnemyMagnet = chkMagnet.Checked;
            OptionsManager.Refresh();
        }

        private void chkLag_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.BotOptions.LagKiller = chkLag.Checked;
            OptionsManager.Refresh();
        }

        private void chkSkipCutscenes_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.BotOptions.SkipCutscenes = chkSkipCutscenes.Checked;
            OptionsManager.Refresh();
        }

        private void numWalkSpeed_ValueChanged(object sender, EventArgs e)
        {
            OptionsManager.BotOptions.WalkSpeed = (int)numWalkSpeed.Value;
            OptionsManager.Refresh();
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

        private void lstLogText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string data = this.lstLogText.SelectedItem.ToString();
                Clipboard.SetData(DataFormats.StringFormat, data);
            }
        }

        private void numOptionsTimer_ValueChanged(object sender, EventArgs e)
        {
            OptionsManager.Timer = (int)numOptionsTimer.Value;
        }

        private void lstLogText_DoubleClick(object sender, EventArgs e)
        {
            this.txtLog.Text = $"{(this.txtLog.Text == "Logs" ? "" : txtLog.Text + " ")}{this.lstLogText.SelectedItem.ToString()}";
        }

        private void chkAFK_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.AFK = chkAFK.Checked;
        }

        private void chkRelog_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAFK.Enabled = chkRelog.Checked;
        }

        #endregion
    }
}
