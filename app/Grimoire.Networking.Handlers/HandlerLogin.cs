using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Threading;

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

        private static CancellationTokenSource _cts = new CancellationTokenSource();

        private List<string> eventModifiers = new List<string>
        {
            "ModifyBtnSend",
            "OnShopEvents",
            "OnInventoryEvents",
            "OnHouseInventoryEvents",
            "OnPortraitTargetEvents",
            "OnMenuBtnItemEvents"
        };

        public async void Handle(XtMessage message)
        {
            if (message.Command == "loginResponse")
            {
                LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Logged in to {World.ServerName()} server.\r\n");
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => AutoRelogin.GameLabel, () => !_cts.IsCancellationRequested, 10, 1000);
                Player.Quests.LoadedQuests.Clear();
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !World.uiLock, () => !_cts.IsCancellationRequested, -1, 500);
                Player.Bank.Load();
                World.SendClientPacket("%xt%server%-1%Type \"/help\" without the quotation marks to bring information about Chat Commands into the chat.%");
                InstallSettings();
                OptionsManager.AntiAfk();
                Player.EnableChat();
                OptionsManager.DisableDeathAds();
                BotUtilities.ShouldUseSkill = true;
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.Bank.IsBankLoaded, () => !_cts.IsCancellationRequested, 20, 500);
            }
            Player.Bank.SavedItems = Flash.Call<List<InventoryItem>>("GetBankItems", new string[0]);
        }

        public void InstallSettings()
        {
            foreach (string modifier in eventModifiers)
            {
                Flash.Call(modifier, new string[0]);
            }
            if (Root.Instance.rtbPing.Visible)
            {
                Flash.Call("StartLatencyTimer", new string[0]);
            }
            if (Travel.Instance.chkCustomChatTrigger.Checked)
            {
                Flash.Call("LoadTravelTriggers", BotClientConfig.Instance.GetValue<string>("customTravels"));
            }
        }
    }
}