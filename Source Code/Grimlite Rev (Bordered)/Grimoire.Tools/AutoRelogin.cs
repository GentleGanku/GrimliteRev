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

        public static void Login()
        {
            Flash.Call("Login", new string[0]);
        }

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
            bool killLag = OptionsManager.LagKiller;
            bool disableAnims = OptionsManager.DisableAnimations;
            bool hidePlayers = OptionsManager.HidePlayers;
            if (killLag)
            {
                OptionsManager.LagKiller = false;
            }
            if (disableAnims)
            {
                OptionsManager.DisableAnimations = false;
            }
            if (hidePlayers)
            {
                OptionsManager.HidePlayers = false;
            }
            if (IsTemporarilyKicked)
            {
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !IsTemporarilyKicked, () => !cts.IsCancellationRequested, 65);
            }
            if (cts.IsCancellationRequested)
            {
                return;
            }
            ResetServers();
            Login();
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => AreServersLoaded, () => !cts.IsCancellationRequested, 30);
            if (cts.IsCancellationRequested)
            {
                return;
            }
            Connect(server);
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !World.IsMapLoading, () => !cts.IsCancellationRequested, 40);
            if (!cts.IsCancellationRequested)
            {
                await Task.Delay(relogDelay);
                if (ensureSuccess)
                {
                    Task.Run(() => EnsureLoginSuccess(cts));
                }
                if (killLag)
                {
                    OptionsManager.LagKiller = true;
                }
                if (disableAnims)
                {
                    OptionsManager.DisableAnimations = true;
                }
                if (hidePlayers)
                {
                    OptionsManager.HidePlayers = true;
                }
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
    }
}