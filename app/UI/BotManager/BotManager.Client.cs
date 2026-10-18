using Grimoire.Botting;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Tools;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        public void chkExitCombatUponStop_Click(object sender, EventArgs e)
        {
            BotClientConfig.Instance.SetValue("exitCombatUponStop", chkExitCombatUponStop.Checked.ToString());
        }

        private void btnchangeName_Click(object sender, EventArgs e)
        {
            CustomName = txtUsername.Text;
        }

        private void btnchangeGuild_Click(object sender, EventArgs e)
        {
            CustomGuild = txtGuild.Text;
        }

        private void btnSetHero_Click(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            switch (s.Name)
            {
                case "btnSetGood":
                    _ = Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%1%");
                    break;
                case "btnSetEvil":
                    _ = Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%2%");
                    break;
                case "btnSetChaos":
                    _ = Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%3%");
                    break;
                case "btnSetUndecided":
                    _ = Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%5%");
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
            Player.ToggleMute();
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
            BotClientConfig.Instance.SetValue("antiAFK", chkAntiAfk.Checked.ToString());
            OptionsManager._antiAfk = chkAntiAfk.Checked;
            OptionsManager.AntiAfk();
        }

        private void chkChangeRoomTag_CheckedChanged(object sender, EventArgs e)
        {
            BotClientConfig.Instance.SetValue("anonymousRoom", chkChangeRoomTag.Checked.ToString());
            OptionsManager.HideRoom = chkChangeRoomTag.Checked;
            if (OptionsManager.HideRoom)
            {
                OptionsManager.RoomNumber = World.RoomNumber;
                Flash.Call("ChangeAreaName", BotClientConfig.Instance.GetValue<string>("areaName") ?? "discord.io/aqwbots");
            }
            else
            {
                Flash.Call("ChangeAreaName", $"{Player.Map}-{OptionsManager.RoomNumber}");
            }
        }

        private void chkChangeChat_CheckedChanged(object sender, EventArgs e)
        {
            BotClientConfig.Instance.SetValue("anonymousUser", chkChangeChat.Checked.ToString());
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
            OptionsManager.SetClientLevel();
        }

        private void btnSetClientLevel_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdSetClientLevel(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnAddClientGold_Click(object sender, EventArgs e)
        {
            _ = Proxy.Instance.SendToClient("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"id\":1,\"cmd\":\"addGoldExp\",\"intGold\":99999999,\"intExp\":0,\"typ\":\"m\"}}}");
        }

        private void btnAddClientACs_Click(object sender, EventArgs e)
        {
            _ = Proxy.Instance.SendToClient("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"sellItem\",\"intAmount\":999999,\"CharItemID\":1,\"bCoins\":1}}}");
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
                        IsWarning = false,
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

        private void btnSetCustomClassName_Click(object sender, EventArgs e)
        {
            CustomClassName = txtClassName.Text;
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

        #endregion
    }
}
