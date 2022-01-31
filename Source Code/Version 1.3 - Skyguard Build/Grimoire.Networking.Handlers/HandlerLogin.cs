using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Grimoire.Networking.Handlers
{
    public class HandlerLogin : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[3]
        {
            "loginResponse",
            "bankToInv",
            "bankFromInv"
        };

        private static System.Threading.CancellationTokenSource _cts = new System.Threading.CancellationTokenSource();

        public async void Handle(XtMessage message)
        {
            if (message.Command == "loginResponse")
            {
                LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Logged in to {World.ServerName()} server.\r\n");
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => AutoRelogin.GameLabel, () => !_cts.IsCancellationRequested, 10, 1000);
                if (Travel.Instance.chkCustomChatTrigger.Checked)
                {
                    Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                    Flash.Call("ModifyBtnSend", new string[0]);
                    string customTravels;
                    try
                    {
                        customTravels = c.Get("customTravels");
                    }
                    catch { customTravels = ""; }
                    Flash.Call("LoadTravelTriggers", customTravels);
                }
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !World.uiLock, () => !_cts.IsCancellationRequested, 10, 1000);
                Player.Bank.Load();
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.Bank.IsBankLoaded, () => !_cts.IsCancellationRequested, 10, 1000);
            }
            Player.Bank.SavedItems = Flash.Call<List<InventoryItem>>("GetBankItems", new string[0]);
        }
    }
}