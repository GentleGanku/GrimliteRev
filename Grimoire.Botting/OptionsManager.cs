using Grimoire.Game;
using Grimoire.UI;
using Grimoire.Networking;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Grimoire.Botting
{
    public static class OptionsManager
    {
        private static bool _isRunning;

        private static bool _disableAnimations;

        private static bool _hidePlayers;

        private static bool _infRange;

        private static bool _hideYulgar;

        private static bool _hideRoom;

        private static bool _afk;

        private static bool _afk2;

        private static bool _infMana;

        public static bool InfMana
        {
            get => _infMana;
            set
            {
                _infMana = value;
            }
        }

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

        public static bool Buff
        {
            get;
            set;
        }

        public static bool ProvokeMonsters
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
            get;
            set;
        }

        public static bool SkipCutscenes
        {
            get;
            set;
        }
        
        public static bool DisableAnimations
        {
            get => _disableAnimations;
            set
            {
                _disableAnimations = value;
                if (value)
                    Proxy.Instance.RegisterHandler(HandlerDisableAnimations);
                else
                    Proxy.Instance.UnregisterHandler(HandlerDisableAnimations);
            }
        }

        public static bool HidePlayers
        {
            get => _hidePlayers;
            set
            {
                _hidePlayers = value;
                if (value)
                {
                    Proxy.Instance.RegisterHandler(HandlerHidePlayers);
                    DestroyPlayers();
                }
                else
                {
                    Proxy.Instance.UnregisterHandler(HandlerHidePlayers);
                }
            }
        }
        
        public static bool InfiniteRange
        {
            get => _infRange;
            set
            {
                _infRange = value;
                if (value)
                {
                    SetInfiniteRange();
                }
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

        public static bool Packet
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
            get => _afk;
            set
            {
                _afk = value;
                if (value)
                    Proxy.Instance.RegisterHandler(HandlerAFK1);
                else
                    Proxy.Instance.UnregisterHandler(HandlerAFK1);
            }
        }

        public static bool AFK2
        {
            get => _afk2;
            set
            {
                _afk2 = value;
                if (value)
                    Proxy.Instance.RegisterHandler(HandlerAFK2);
                else
                    Proxy.Instance.UnregisterHandler(HandlerAFK2);

            }
        }

        public static bool HideRoom
        {
            get => _hideRoom;
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

        public static int SetLevelOnJoin
        {
            get;
            set;
        }

        public static bool HideYulgar
        {
            get => _hideYulgar;
            set
            {
                _hideYulgar = value;
                if (value)
                {
                    Proxy.Instance.RegisterHandler(HandlerYulgar);
                    if((Player.Map.ToLower() ?? "") == "yulgar" && (Player.Cell.ToLower() ?? "") == "upstairs")
                        DestroyPlayers();
                }
                else
                    Proxy.Instance.UnregisterHandler(HandlerYulgar);
            }
        }

        private static readonly string[] empty = new string[0];

        public static event Action<bool> StateChanged;

        private static void SetInfiniteRange() => Flash.Call("SetInfiniteRange", empty);

        private static void SetProvokeMonsters() => Flash.Call("SetProvokeMonsters", empty);

        private static void SetEnemyMagnet() => Flash.Call("SetEnemyMagnet", empty);

        private static void SetLagKiller() => Flash.Call("SetLagKiller", LagKiller ? bool.TrueString : bool.FalseString);

        public static void DestroyPlayers() => Flash.Call("DestroyPlayers", empty);

        private static void SetSkipCutscenes() => Flash.Call("SetSkipCutscenes", empty);

        public static void SetWalkSpeed() => Flash.Call("SetWalkSpeed", WalkSpeed.ToString());

        public static void Start()
        {
            if (!IsRunning)
            {
                ApplySettings();
            }
        }

        public static void Stop()
        {
            IsRunning = false;
        }

        public static async Task SetClientLevel()
        {
            BotClientConfig c = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");
            string packetClientLevel = c.Get("packetClientLevel");
            await Proxy.Instance.SendToClient(packetClientLevel);
        }

        private static async Task ApplySettings()
        {
            IsRunning = true;
            while (IsRunning && Player.IsLoggedIn)
            {
                bool flagprovoke = ProvokeMonsters && Player.IsAlive && BotData.BotState != BotData.State.Move && BotData.BotState != BotData.State.Rest && BotData.BotState != BotData.State.Transaction;
                if (flagprovoke)
                {
                    if (BotData.BotState == BotData.State.Quest)
                    {
                        await Task.Delay(1500);
                        SetProvokeMonsters();
                        BotData.BotState = BotData.State.Combat;
                    }
                    SetProvokeMonsters();
                }
                if (EnemyMagnet && Player.IsAlive)
                    SetEnemyMagnet();
                if (Untarget)
                    Player.CancelTargetSelf();
                if (Buff)
                    Player.SetBuff();
                if (SkipCutscenes)
                    SetSkipCutscenes();
                SetWalkSpeed();
                SetLagKiller();
                await Task.Delay(millisecondsDelay: Timer);
            }
        }
        
        private static IJsonMessageHandler HandlerDisableAnimations
        {
            get;
        }

        private static IXtMessageHandler HandlerHidePlayers
        {
            get;
        }

        private static IXtMessageHandler HandlerYulgar
        {
            get;
        }

        private static IJsonMessageHandler HandlerHideRoom
        {
            get;
        }

        private static IJsonMessageHandler HandlerRange
        {
            get;
        }
        
        private static IXtMessageHandler HandlerAFK1
        {
            get;
        }
        
        private static IXtMessageHandler HandlerAFK2
        {
            get;
        }

        static OptionsManager()
        {
            HandlerDisableAnimations = new HandlerAnimations();
            HandlerHidePlayers = new HandlerPlayers();
            HandlerRange = new HandlerSkills();
            HandlerYulgar = new HandlerXtCellJoin();
            HandlerHideRoom = new HandlerMapJoin();
            HandlerAFK1 = new HandlerAFK();
            HandlerAFK2 = new HandlerAFK2();
            WalkSpeed = 8;
        }
    }
}