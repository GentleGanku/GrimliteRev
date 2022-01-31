using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting
{
    public class OptionsManager
    {
        private static bool _isRunning;

        private static bool _disableAnimation;

        private static bool _hidePlayers;

        private static bool _infRange;

        private static bool _hideYulgar;

        private static bool _hideRoom;

        private static bool _afk;

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

        public static bool ProvokeAllMonster
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
        } = "%xt%zm%aggroMon%1%MonMapID%";

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
            get => _disableAnimation;
            set
            {
                _disableAnimation = value;
                RunDisableAnimations(value);
            }
        }

        public static bool HidePlayers
        {
            get => _hidePlayers;
            set
            {
                _hidePlayers = value;
                if (Player.Inventory.Items.Count > 0)
                    DestroyPlayers(value);
                else
                    DestroyPlayers(false);
            }
        }

        public static bool InfiniteRange
        {
            get => _infRange;
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

        public static bool WarningMsgFilter
        {
            get;
            set;
        }

        public static bool _saveState
        {
            get;
            set;
        } = false;

        public static bool _antiAfk
        {
            get;
            set;
        } = true;

        public static bool HideYulgar
        {
            get => _hideYulgar;
            set
            {
                _hideYulgar = value;
                if (value)
                {
                    Proxy.Instance.RegisterHandler(HandlerYulgar);
                    if ((Player.Map.ToLower() ?? "") == "yulgar" && (Player.Cell.ToLower() ?? "") == "upstairs")
                        DestroyPlayers(true);
                }
                else
                    Proxy.Instance.UnregisterHandler(HandlerYulgar);
            }
        }

        private static readonly string[] empty = new string[0];

        public static event Action<bool> StateChanged;

        public static void SetInfiniteRange(bool Toggle) => Flash.Call("SetInfiniteRange", Toggle);

        public static void SetProvokeMonsters() => Flash.Call("SetProvokeMonsters", empty);

        public static void SetEnemyMagnet() => Flash.Call("SetEnemyMagnet", empty);

        public static void SetLagKiller() => Flash.Call("SetLagKiller", LagKiller ? false : true);

        public static void DestroyPlayers(bool Enabled) => Flash.Call("DestroyPlayers", Enabled);

        public static void SetSkipCutscenes() => Flash.Call("SetSkipCutscenes", (BotData.BotCell == null) ? "Enter" : BotData.BotCell, (BotData.BotPad == null) ? "Spawn" : BotData.BotPad);

        public static void SetWalkSpeed() => Flash.Call("SetWalkSpeed", WalkSpeed);

        public static void RunDisableAnimations(bool Enabled) => Flash.Call("DisableAnimations", Enabled);

        public static void Start()
        {
            ApplySettings();
            if (BotManager.Instance.chkLag.Checked)
            {
                LagKiller = true;
                SetLagKiller();
            }
            if (BotManager.Instance.chkProvoke.Checked)
            {
                ProvokeMonsters = true;
            }
            if (BotManager.Instance.chkProvokeAllMon.Checked)
            {
                ProvokeAllMonster = true;
            }
            if (BotManager.Instance.chkSaveState.Checked)
            {
                _saveState = true;
                SaveState();
            }
        }

        public static void Stop()
        {
            IsRunning = false;
            if (BotManager.Instance.chkLag.Checked)
            {
                LagKiller = false;
                SetLagKiller();
            }
            if (BotManager.Instance.chkProvoke.Checked)
            {
                ProvokeMonsters = false;
            }
            if (BotManager.Instance.chkProvokeAllMon.Checked)
            {
                ProvokeAllMonster = false;
            }
            if (BotManager.Instance.chkProvoke.Checked || BotManager.Instance.chkProvokeAllMon.Checked || Configuration.Instance.ProvokeMonsters || Configuration.Instance.ProvokeAllMonster || ProvokeMonsters || ProvokeAllMonster)
            {
                BotUtilities.MoveToSelfCell();
            }
            if (BotManager.Instance.chkSaveState.Checked)
            {
                _saveState = false;
            }
        }

        public static async Task SetClientLevel()
        {
            BotClientConfig c = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");
            string packetClientLevel = c.Get("packetClientLevel");
            await Proxy.Instance.SendToClient(packetClientLevel);
        }

        public static async Task SetProvokeAllMonster()
        {
            if (!ProvokePacket.Contains("MonMapID"))
                await Proxy.Instance.SendToServer($"{ProvokePacket}");
            else
                await Proxy.Instance.SendToServer("%xt%zm%aggroMon%1%1%2%3%4%5%6%7%8%9%10%11%12%13%14%15%16%17%18%19%20%21%22%23%24%25%26%27%28%29%30%31%32%33%34%35%36%37%38%39%40%41%42%43%44%45%46%47%48%49%50%51%52%53%54%55%56%57%58%59%60%61%62%63%64%65%66%67%68%69%70%");
        }

        public static async Task SetPacketSpam()
        {
            await Proxy.Instance.SendToServer($"{SpamPacket}");
            await Task.Delay(PacketDelay);
        }

        public static async void SaveState()
        {
            while (IsRunning && _saveState)
            {
                await Proxy.Instance.SendToServer($"%xt%zm%whisper%1%Save State: Ensures any gold, experience, reputation, and class points gained are saved. Triggers after every 5 minutes passed.%{Player.Username}%");
                LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss tt}] Save state has been triggered.");
                await Task.Delay(300000);
            }
        }

        public static async void AntiAfk()
        {
            while (_antiAfk)
            {
                Player.cancelAfk();
                await Task.Delay(60000);
            }
        }

        private static async Task ApplySettings()
        {
            IsRunning = true;
            while (IsRunning && Player.IsLoggedIn)
            {
                bool flagprovoke = ProvokeMonsters && Player.IsAlive && BotData.BotState != BotData.State.Move && BotData.BotState != BotData.State.Rest && BotData.BotState != BotData.State.Transaction;
                bool flagprovokeAll = ProvokeAllMonster && Player.IsAlive && BotData.BotState != BotData.State.Move && BotData.BotState != BotData.State.Rest && BotData.BotState != BotData.State.Transaction;
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
                if (flagprovokeAll)
                {
                    if (BotData.BotState == BotData.State.Quest)
                    {
                        await Task.Delay(1500);
                        SetProvokeAllMonster();
                        BotData.BotState = BotData.State.Combat;
                    }
                    SetProvokeAllMonster();
                }
                if (PacketSpam)
                    SetPacketSpam();
                SetLagKiller();
                if (EnemyMagnet && Player.IsAlive)
                    SetEnemyMagnet();
                if (Untarget)
                    Player.CancelTargetSelf();
                if (SkipCutscenes)
                    SetSkipCutscenes();
                SetInfiniteRange(InfiniteRange);
                DestroyPlayers(HidePlayers);
                SetWalkSpeed();
                await Task.Delay(millisecondsDelay: Timer);
            }
        }

        private static IXtMessageHandler HandlerYulgar
        {
            get;
        }

        private static IJsonMessageHandler HandlerHideRoom
        {
            get;
        }

        private static IXtMessageHandler HandlerAFK1
        {
            get;
        }

        static OptionsManager()
        {
            HandlerYulgar = new HandlerXtCellJoin();
            HandlerHideRoom = new HandlerMapJoin();
            HandlerAFK1 = new HandlerAFK();
            WalkSpeed = 8;
        }
    }
}