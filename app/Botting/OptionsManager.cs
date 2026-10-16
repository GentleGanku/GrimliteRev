using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using Grimoire.UI;
using Grimoire.Utils;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public class OptionsManager
    {
        #region Fields

        private static readonly string[] empty = new string[0];
        private static bool _isRunning;
        private static bool _disableAnimation;
        private static bool _lagKiller;
        private static bool _hidePlayers;
        private static bool _infRange;
        private static bool _hideYulgar;
        private static bool _hideRoom;
        private static bool _afk;
        private static readonly Stopwatch _packetSpamTimer = new Stopwatch();

        #endregion

        #region Constructors

        static OptionsManager()
        {
            HandlerHideRoom = new HandlerMapJoin();
            WalkSpeed = 8;
        }

        #endregion

        #region Events

        public static event Action<bool> StateChanged;

        #endregion

        #region Properties

        public static bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            private set
            {
                _isRunning = value;
                StateChanged?.Invoke(value);
            }
        }

        public static bool ProvokeMonsters
        {
            get;
            set;
        }

        public static bool PacketSpam
        {
            get;
            set;
        }

        public static string ProvokePacket
        {
            get;
            set;
        }

        public static string SpamPacket
        {
            get;
            set;
        }

        public static bool EnemyMagnet
        {
            get;
            set;
        }

        public static bool LagKiller
        {
            get
            {
                return _lagKiller;
            }
            set
            {
                _lagKiller = value;
                SetLagKiller();
            }
        }

        public static bool SkipCutscenes
        {
            get;
            set;
        }

        public static bool DisableAnimations
        {
            get
            {
                return _disableAnimation;
            }
            set
            {
                _disableAnimation = value;
                RunDisableAnimations(value);
            }
        }

        public static bool HidePlayers
        {
            get
            {
                return _hidePlayers;
            }
            set
            {
                _hidePlayers = value;
                DestroyPlayers(value);
            }
        }

        public static bool InfiniteRange
        {
            get
            {
                return _infRange;
            }
            set
            {
                _infRange = value;
                SetInfiniteRange(value);
            }
        }

        public static int WalkSpeed
        {
            get;
            set;
        }

        public static int Timer
        {
            get;
            set;
        } = 250;

        public static int PacketDelay
        {
            get;
            set;
        }

        public static int RoomNumber
        {
            get;
            set;
        }

        public static bool Untarget
        {
            get;
            set;
        }

        public static bool AFK
        {
            get
            {
                return _afk;
            }
            set
            {
                _afk = value;
                if (value)
                    Proxy.Instance.RegisterHandler(HandlerAFK1);
                else
                    Proxy.Instance.UnregisterHandler(HandlerAFK1);
            }
        }

        public static bool HideRoom
        {
            get
            {
                return _hideRoom;
            }
            set
            {
                _hideRoom = value;
                if (value)
                    Proxy.Instance.RegisterHandler(HandlerHideRoom);
                else
                    Proxy.Instance.UnregisterHandler(HandlerHideRoom);
            }
        }

        public static bool ChangeChat
        {
            get;
            set;
        }

        public static bool WarningMsgFilter
        {
            get;
            set;
        }

        public static bool _antiAfk
        {
            get;
            set;
        } = false;

        public static bool HideYulgar
        {
            get
            {
                return _hideYulgar;
            }
            set
            {
                _hideYulgar = value;
                if (value)
                {
                    if (Player.IsLoggedIn && Player.Map.ToLower() == "yulgar")
                        DestroyPlayers(true);
                }
            }
        }

        private static IJsonMessageHandler HandlerHideRoom
        {
            get;
        }

        private static IXtMessageHandler HandlerAFK1
        {
            get;
        } = new HandlerAFK();

        #endregion

        #region Methods

        public static void SetInfiniteRange(bool Toggle) => Flash.Call("SetInfiniteRange", Toggle);

        public static void SetProvokeMonsters() => Flash.Call("SetProvokeMonsters", empty);

        public static void SetEnemyMagnet() => Flash.Call("SetEnemyMagnet", empty);

        public static void SetLagKiller() => Flash.Call("SetLagKiller", !LagKiller);

        public static void DestroyPlayers(bool Enabled) => Flash.Call("DestroyPlayers", Enabled);

        public static void SetSkipCutscenes() => Flash.Call("SetSkipCutscenes", (BotData.BotCell == null) ? "Enter" : BotData.BotCell, (BotData.BotPad == null) ? "Spawn" : BotData.BotPad);

        public static void SetWalkSpeed() => Flash.Call("SetWalkSpeed", WalkSpeed);

        public static void DisableDeathAds() => Flash.Call("DisableDeathAd", empty);

        public static void RunDisableAnimations(bool Enabled) => Flash.Call("DisableAnimations", Enabled);

        public static void Start()
        {
            ApplySettings();
            if (BotManager.Instance.chkLag.Checked)
            {
                LagKiller = true;
            }
            if (BotManager.Instance.chkProvoke.Checked)
            {
                ProvokeMonsters = true;
            }
        }

        public static void Stop()
        {
            IsRunning = false;
            if (BotManager.Instance.chkLag.Checked)
            {
                LagKiller = false;
            }
            if (BotManager.Instance.chkProvoke.Checked)
            {
                ProvokeMonsters = false;
            }
            if (BotManager.Instance.chkProvoke.Checked || BotManager.Instance.ActiveBotEngine.Configuration?.ProvokeMonsters == true || ProvokeMonsters)
            {
                BotUtilities.MoveToSelfCell();
            }
        }

        public static void SetClientLevel()
        {
            Proxy.Instance.SendToClient("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"levelUp\",\"intExpToLevel\":\"0\",\"intLevel\":" + Player.levelCap + "}}}");
        }

        public static async void SetPacketSpam()
        {
            try
            {
                // Called on every options tick (250 ms); send only once PacketDelay has passed since the last packet.
                if (_packetSpamTimer.IsRunning && _packetSpamTimer.ElapsedMilliseconds < PacketDelay)
                {
                    return;
                }
                _packetSpamTimer.Restart();
                await Proxy.Instance.SendToServer($"{SpamPacket}");
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running OptionsManager.SetPacketSpam", ex);
            }
        }

        public static async void AntiAfk()
        {
            try
            {
                if (!_antiAfk)
                {
                    return;
                }
                while (Player.IsLoggedIn && _antiAfk)
                {
                    Player.cancelAfk();
                    await Task.Delay(60000);
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running OptionsManager.AntiAfk", ex);
            }
        }

        public static async Task ApplySettings()
        {
            IsRunning = true;
            while (IsRunning && Player.IsLoggedIn)
            {
                if (PacketSpam && Player.IsAlive)
                    SetPacketSpam();
                if (EnemyMagnet && Player.IsAlive)
                    SetEnemyMagnet();
                if (Untarget && Player.targetOnSelf)
                    Player.CancelTargetSelf();
                if (SkipCutscenes && World.ActiveCutscene > 0)
                    SetSkipCutscenes();
                if (Player.walkSpeed != WalkSpeed)
                    SetWalkSpeed();
                Provoke();
                await Task.Delay(millisecondsDelay: Timer);
            }
        }

        private static void Provoke()
        {
            if (ProvokeMonsters)
            {
                World.SendPacket(!string.IsNullOrEmpty(ProvokePacket) && !ProvokePacket.Contains("MonMapID") ? $"{ProvokePacket}" : $"%xt%zm%aggroMon%{World.RoomId}%{string.Join("%", World.MonstersInCell(Player.Cell)?.ConvertAll<int>(m => m.MonMapID))}%");
            }
        }

        #endregion
    }
}