using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Tools
{
    public static class AutoRelogin
    {
        #region Properties

        public static bool IsTemporarilyKicked
        {
            get
            {
                return Flash.Call<bool>("IsTemporarilyKicked", new string[0]);
            }
        }

        public static bool AreServersLoaded
        {
            get
            {
                return Flash.Call<bool>("AreServersLoaded", new string[0]);
            }
        }

        public static bool LoginLabel
        {
            get
            {
                return Flash.Call<bool>("LoginLabel", new string[0]);
            }
        }

        public static bool ServerLabel
        {
            get
            {
                return Flash.Call<bool>("ServerLabel", new string[0]);
            }
        }

        public static string ServerIP
        {
            get
            {
                return Flash.Call<string>("ServerIP", new string[0]);
            }
        }

        public static bool GameLabel
        {
            get
            {
                return Flash.Call<bool>("GameLabel", new string[0]);
            }
        }

        #endregion

        #region Methods

        public static bool IsClientLoading(string Type) => Flash.Call<bool>("IsClientLoading", Type);

        public static void Login()
        {
            Flash.Call("Login", new string[0]);
        }

        public static void LoginExecute() => Flash.Call("OnLoginExecute", new string[0]);

        public static bool ResetServers()
        {
            return Flash.Call<bool> ("ResetServers", new string[0]);
        }

        public static void Connect(Server server)
        {
            Server targeted = Server.List.Find(s => s.Name.Equals(server.Name));
            if (targeted.PlayerCount > (targeted.MaxCount - 10))
            {
                targeted = Server.List.FindAll(s => !s.Name.Equals(targeted.Name) && s.PlayerCount <= (s.MaxCount - 10)).OrderByDescending(s => s.PlayerCount).ToList()[0];
            }
            Flash.Call("Connect", targeted.Name);
        }

        public static async Task Login(Server server, int relogDelay, CancellationTokenSource cts, bool ensureSuccess)
        {
            OptionsCheck(false);
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !IsTemporarilyKicked, null, 65);
            ResetServers();
            await Task.Delay(1000);
            Login();
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !IsClientLoading("Account"), () => !cts.IsCancellationRequested, 10);
            Connect(server);
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.IsLoggedIn, () => !cts.IsCancellationRequested, 10);
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !World.IsMapLoading, () => !cts.IsCancellationRequested, 10);
            if ((AutoRelogin.IsClientLoading("MapLoadingStuck")) || (AutoRelogin.IsClientLoading("MapLoadingError")))
            {
                World.ReloadCurrentMap();
                World.GameMessage("The map has been reloaded!");
            }
            await Task.Delay(relogDelay);
            if (ensureSuccess)
            {
                Task.Run(() => EnsureLoginSuccess(cts));
            }
            OptionsCheck(true);
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

        private static async Task EnsureLoginSuccess(CancellationTokenSource cts)
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(1000);
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

        #endregion
    }
}