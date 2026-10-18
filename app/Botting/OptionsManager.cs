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
        private static readonly ManipulationOptions Off = new ManipulationOptions();
        private static bool _isRunning;
        private static bool _botActive;
        private static bool _lagKiller;
        private static bool _infRange;
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

        /// <summary>
        /// The options set in the main menu's Settings > Manipulation Options. They apply whenever you're in game.
        /// </summary>
        public static ManipulationOptions MenuOptions
        {
            get;
        } = new ManipulationOptions();

        /// <summary>
        /// The options set in Bot Manager. They apply only while a bot runs.
        /// </summary>
        public static ManipulationOptions BotOptions
        {
            get;
        } = new ManipulationOptions();

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

        /// <summary>
        /// Whether the options loop has something to do. A running bot always keeps it going, because bot commands
        /// such as packet spam switch options on during the run.
        /// </summary>
        private static bool NeedsLoop
        {
            get
            {
                return _botActive || PacketSpam || EnemyMagnet || SkipCutscenes || ProvokeMonsters || WalkSpeed != 8;
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

        public static void SetSkipCutscenes() => Flash.Call("SetSkipCutscenes", (BotData.BotCell == null) ? "Enter" : BotData.BotCell, (BotData.BotPad == null) ? "Spawn" : BotData.BotPad);

        public static void SetWalkSpeed() => Flash.Call("SetWalkSpeed", WalkSpeed);

        public static void DisableDeathAds() => Flash.Call("DisableDeathAd", empty);

        /// <summary>
        /// Applies the bot's options for as long as the bot runs.
        /// </summary>
        public static void Start()
        {
            _botActive = true;
            Refresh();
        }

        /// <summary>
        /// Takes the bot's options back off when the bot stops, leaving the menu's. Packet spam is set by a bot
        /// command, so it ends with the bot. When provoke turns off, the player moves to a cell of their own so the
        /// provoked monsters stop following.
        /// </summary>
        public static void Stop()
        {
            bool provoked = ProvokeMonsters;
            _botActive = false;
            PacketSpam = false;
            Refresh();
            if (provoked && !ProvokeMonsters)
            {
                BotUtilities.MoveToSelfCell();
            }
        }

        /// <summary>
        /// Sets the options the client uses from the two sets: an option is on when the menu has it on, or, while a bot
        /// runs, when Bot Manager has it on. Walk speed takes the faster of the two. Only a value that changes is sent
        /// to the game, and the options loop starts when something needs it.
        /// </summary>
        public static void Refresh()
        {
            ManipulationOptions bot = _botActive ? BotOptions : Off;
            bool infiniteRange = MenuOptions.InfiniteRange || bot.InfiniteRange;
            if (infiniteRange != InfiniteRange)
            {
                InfiniteRange = infiniteRange;
            }
            bool lagKiller = MenuOptions.LagKiller || bot.LagKiller;
            if (lagKiller != LagKiller)
            {
                LagKiller = lagKiller;
            }
            ProvokeMonsters = MenuOptions.ProvokeMonsters || bot.ProvokeMonsters;
            EnemyMagnet = MenuOptions.EnemyMagnet || bot.EnemyMagnet;
            SkipCutscenes = MenuOptions.SkipCutscenes || bot.SkipCutscenes;
            int walkSpeed = Math.Max(MenuOptions.WalkSpeed, bot.WalkSpeed);
            if (walkSpeed != WalkSpeed)
            {
                WalkSpeed = walkSpeed;
                if (Player.IsLoggedIn)
                {
                    SetWalkSpeed();
                }
            }
            if (NeedsLoop && !IsRunning && Player.IsLoggedIn)
            {
                ApplySettings().Forget("applying the client options");
            }
        }

        /// <summary>
        /// Sends the options that are on to a freshly loaded world, which starts with all of them off, and starts the
        /// options loop when something needs it. Called after every login.
        /// </summary>
        public static void ApplyOnLogin()
        {
            if (InfiniteRange)
            {
                SetInfiniteRange(true);
            }
            if (LagKiller)
            {
                SetLagKiller();
            }
            if (NeedsLoop && !IsRunning)
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
        /// Runs the options loop while something needs it. Each call starts a new loop; an older one sees a newer
        /// generation and exits, so a quick Stop and Start (as auto-relogin does) doesn't leave two loops sending
        /// everything twice. The newest loop clears <see cref="IsRunning"/> when it ends, for example on logout, so
        /// turning an option on afterwards starts a new one.
        /// </summary>
        public static async Task ApplySettings()
        {
            int generation = Interlocked.Increment(ref _loopGeneration);
            IsRunning = true;
            while (IsRunning && generation == _loopGeneration && NeedsLoop && Player.IsLoggedIn)
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