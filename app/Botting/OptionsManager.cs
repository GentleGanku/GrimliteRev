using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using Grimoire.UI;
using Grimoire.Utils;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public class OptionsManager
    {
        #region Fields

        private static readonly string[] empty = new string[0];
        private static bool _isRunning;
        private static bool _lagKiller;
        private static bool _infRange;
        private static bool _hideYulgar;
        private static bool _hideRoom;
        private static bool _afk;
        private static int _loopGeneration;
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

        public static void SetEnemyMagnet() => Flash.Call("SetEnemyMagnet", empty);

        public static void SetLagKiller() => Flash.Call("SetLagKiller", !LagKiller);

        public static void DestroyPlayers(bool Enabled) => Flash.Call("DestroyPlayers", Enabled);

        public static void SetSkipCutscenes() => Flash.Call("SetSkipCutscenes", (BotData.BotCell == null) ? "Enter" : BotData.BotCell, (BotData.BotPad == null) ? "Spawn" : BotData.BotPad);

        public static void SetWalkSpeed() => Flash.Call("SetWalkSpeed", WalkSpeed);

        public static void DisableDeathAds() => Flash.Call("DisableDeathAd", empty);

        public static void Start()
        {
            ApplySettings().Forget("applying the client options");
            if (BotManager.Instance.chkLag.Checked)
            {
                LagKiller = true;
            }
            if (BotManager.Instance.chkProvoke.Checked)
            {
                ProvokeMonsters = true;
            }
        }

        /// <summary>
        /// Stops the options loop when a bot stops or "Enable options without starting" is unchecked. Packet spam is set
        /// by a bot command, so it ends with the bot. Cell provoke keeps running while its option is checked, so the
        /// loop starts again for it.
        /// </summary>
        public static void Stop()
        {
            IsRunning = false;
            PacketSpam = false;
            if (BotManager.Instance.chkLag.Checked)
            {
                LagKiller = false;
            }
            if (ProvokeMonsters)
            {
                ApplySettings().Forget("applying the client options");
            }
        }

        public static void SetClientLevel()
        {
            _ = Proxy.Instance.SendToClient("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"levelUp\",\"intExpToLevel\":\"0\",\"intLevel\":" + Player.levelCap + "}}}");
        }

        /// <summary>
        /// Sends the packet-spam packet. It is called on every options tick (250 ms) and sends only once PacketDelay
        /// has passed since the last packet.
        /// </summary>
        public static async void SetPacketSpam()
        {
            try
            {
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

        /// <summary>
        /// Runs the options loop. Each call starts a new loop; an older one sees a newer generation and exits, so a
        /// quick Stop and Start (as auto-relogin does) doesn't leave two loops sending everything twice. The newest loop
        /// clears <see cref="IsRunning"/> when it ends, for example on logout, so turning an option on afterwards starts
        /// a new one.
        /// </summary>
        public static async Task ApplySettings()
        {
            int generation = Interlocked.Increment(ref _loopGeneration);
            IsRunning = true;
            while (IsRunning && generation == _loopGeneration && Player.IsLoggedIn)
            {
                bool alive = (PacketSpam || EnemyMagnet) && Player.IsAlive;
                if (PacketSpam && alive)
                    SetPacketSpam();
                if (EnemyMagnet && alive)
                    SetEnemyMagnet();
                if (SkipCutscenes && World.ActiveCutscene > 0)
                    SetSkipCutscenes();
                if (Player.walkSpeed != WalkSpeed)
                    SetWalkSpeed();
                Provoke();
                await Task.Delay(millisecondsDelay: Timer);
            }
            if (generation == _loopGeneration)
            {
                IsRunning = false;
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