using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Tools
{
    public static class AutoRelogin
    {
        public static bool IsTemporarilyKicked => Flash.Call<bool>("IsTemporarilyKicked", new string[0]);

        public static bool AreServersLoaded => Flash.Call<bool>("AreServersLoaded", new string[0]);

        public static bool IsClientLoading(string Type) => Flash.Call<bool>("IsClientLoading", Type);

        public static bool LoginLabel => Flash.Call<bool>("LoginLabel", new string[0]);

        public static bool ServerLabel => Flash.Call<bool>("ServerLabel", new string[0]);

        public static string ServerIP => Flash.Call<string>("ServerIP", new string[0]);

        public static string ServerAddress => Flash.Call<string>("RealAddress", new string[0]);

        public static bool GameLabel => Flash.Call<bool>("GameLabel", new string[0]);

        public static string ServerName => Flash.Call<string>("GetServerName", new string[0]);

        public static void Login()
        {
            Flash.Call("Login", new string[0]);
        }

        public static void LoginExecute() => Flash.Call("OnLoginExecute", new string[0]);

        public static bool ResetServers()
        {
            return Flash.Call<bool>("ResetServers", new string[0]);
        }

        public static void Connect(Server server)
        {
            Flash.Call("Connect", server.Name);
        }

        public static async Task Login(Server server, int relogDelay, CancellationTokenSource cts, bool ensureSuccess)
        {
            OptionsCheck(false);
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !IsTemporarilyKicked, null, 65);
            ResetServers();
            await Task.Delay(1000);
            Login();
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !IsClientLoading("Account"), () => !cts.IsCancellationRequested, 10);
            if (cts.IsCancellationRequested)
            {
                await OnLogoutExecute();
                await OnLogin();
            }
            Connect(server);
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.IsLoggedIn, () => !cts.IsCancellationRequested, 10);
            if (cts.IsCancellationRequested)
            {
                await OnLogoutExecute();
                await OnLogin();
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !IsClientLoading("Account"), null, 5);
                if (!ServerLabel)
                {
                    return;
                }
                await OnConnect(server);
            }
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !World.IsMapLoading, () => !cts.IsCancellationRequested, 10);
            if ((AutoRelogin.IsClientLoading("MapLoadingStuck")) || (AutoRelogin.IsClientLoading("MapLoadingError")))
            {
                World.ReloadCurrentMap();
                World.GameMessage("The map has been reloaded!");
            }
            if (!cts.IsCancellationRequested)
            {
                await Task.Delay(relogDelay);
                if (ensureSuccess)
                {
                    Task.Run(() => EnsureLoginSuccess(cts));
                }
                OptionsCheck(true);
            }
        }

        private static async Task EnsureLoginSuccess(CancellationTokenSource cts)
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(1000);
                if (cts.IsCancellationRequested)
                {
                    return;
                }
                string map = Player.Map;
                if (!string.IsNullOrEmpty(map) && !map.Equals("name", StringComparison.OrdinalIgnoreCase) && !map.Equals("battleon", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }
            if (Player.Map.Equals("battleon", StringComparison.OrdinalIgnoreCase))
            {
                Player.Logout();
            }
        }

        public static Server[] ServerList;
        public static void Servers(Server[] servers)
        {
            ServerList = servers;
        }

        public static async Task ForceLogin(Server server, CancellationTokenSource cts)
        {
            OptionsCheck(false);
            Connect(server);
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.IsMapLoading, () => !cts.IsCancellationRequested, 10, 500);
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !World.IsMapLoading, () => !cts.IsCancellationRequested, 10, 500);
            if (IsClientLoading("MapLoadingStuck") || IsClientLoading("MapLoadingError"))
            {
                World.ReloadCurrentMap();
                World.GameMessage("The map has been reloaded!");
            }
            OptionsCheck(true);
        }

        public static async Task OnLogoutExecute()
        {
            Player.Logout();
        }

        public static async Task OnLoginExecute()
        {
            LoginExecute();
        }

        public static async Task OnLogin()
        {
            Login();
        }

        public static async Task OnConnect(Server server)
        {
            Connect(server);
        }

        public static void OptionsCheck(bool check)
        {
            if (BotManager.Instance.chkLag.Checked)
            {
                OptionsManager.LagKiller = check;
            }
            if (BotManager.Instance.chkDisableAnims.Checked)
            {
                OptionsManager.DisableAnimations = check;
            }
            if (BotManager.Instance.chkHidePlayers.Checked)
            {
                OptionsManager.HidePlayers = check;
            }
        }

    }
}