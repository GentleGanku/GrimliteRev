using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public class Bot : IBotEngine
    {
        public bool IsVar(string value)
        {
            return Regex.IsMatch(value, @"\[(?!Class Item)([^\)]*)\]");
        }

        public string GetVar(string value)
        {
            return Regex.Replace(value, @"[\[\]']+", "");
        }

        public string Value(string var)
        {
            return IsVar(var) ? Configuration.Tempvariable[GetVar(var)] : var;
        }

        public static Bot Instance = new Bot();

        private IBotCommand cmd;

        private int _index;

        private Configuration _config;

        private bool _isRunning;

        public CancellationTokenSource _ctsBot;

        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = (value < Configuration.Commands.Count) ? value : 0;
            }
        }

        public Configuration Configuration
        {
            get
            {
                return _config;
            }
            set
            {
                if (value != _config)
                {
                    _config = value;
                    this.ConfigurationChanged?.Invoke(_config);
                }
            }
        }

        public static Dictionary<int, Configuration> Configurations = new Dictionary<int, Configuration>();

        public static Dictionary<int, int> OldIndex = new Dictionary<int, int>();

        public static Dictionary<string, string> ProtectedMonsters = new Dictionary<string, string>
        {
                { "Escherion"     , "Staff of Inversion"},
                { "Vath"          , "Stalagbite"},
                //{ "Ultra Engineer", "Attack Drone,Defense Drone"},
        };

        public int CurrentConfiguration { get; set; } = 0;

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                this.IsRunningChanged?.Invoke(_isRunning);
            }
        }

        public event Action<bool> IsRunningChanged;

        public event Action<int> IndexChanged;

        public event Action<Configuration> ConfigurationChanged;

        public int selectedIndex { get; set; }

        public void Start(Configuration config)
        {
            LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Starting the bot.\r\n");
            BotData.BotMap = null;
            BotData.BotCell = null;
            BotData.BotPad = null;
            BotData.BotSkill = null;
            Flash.Call("advancedOpt", OptionsManager.Untarget ? true : false);
            OnExecute(config);
            Index = 0;
            LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] The bot has been started.\r\n");
        }

        public void Stop()
        {
            LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Stopping the bot.\r\n");
            Flash.Call("advancedOpt", OptionsManager.Untarget ? true : false);
            Flash.Call("dropUIOpt", true);
            OnCancel();
            IsRunning = false;
            LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] The bot has been stopped.\r\n");
        }

        public void Resume(Configuration config)
        {
            LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Resuming the bot.\r\n");
            selectedIndex = BotManager.Instance.lstCommands.SelectedIndex;
            OnExecute(config);
            Index = selectedIndex;
            LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] The bot has been resumed.\r\n");
        }

        public void Pause()
        {
            LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Pausing the bot.\r\n");
            OnCancel();
            LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] The bot has been paused.\r\n");
        }

        private async void OnExecute(Configuration config)
        {
            IsRunning = true;
            Player.CancelTarget();
            BotUtilities.ActiveBot = true;
            BotUtilities.HasLoadedQuests = false;
            Configuration = config;
            _ctsBot = new CancellationTokenSource();
            //_questDelayCounter = new Stopwatch();
            //_boostDelayCounter = new Stopwatch();
            World.ItemDropped += OnItemDropped;
            Player.Quests.QuestsLoaded += OnQuestsLoaded;
            Player.Quests.QuestCompleted += OnQuestCompleted;
            //_questDelayCounter.Start();
            //_boostDelayCounter.Start();
            BotData.BotState = BotData.State.Others;
            BotData.SkillSet.Clear();
            BotUtilities.AddConfigSkills(config);
            if (config.Items.Count > 0 && BotManager.Instance.chkUnbankOnStart.Checked)
            { 
                foreach (string item in config.Items)
                {
                    if (!Player.Inventory.ContainsItem(item, "*") && Player.Bank.ContainsItem(item))
                    {
                        Player.Bank.TransferFromBank(item);
                        Task.Delay(70);
                        LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Items List - Item transferred to Inventory: {item}.\r\n");
                    }
                    else if (Player.Inventory.ContainsItem(item, "*"))
                    {
                        LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Items List - Item already exists in Inventory: {item}.\r\n");
                    }
                }
            }
            await this.LoadBotQuests();
            CheckQuests();
            World.DropStack.RejectDrops();
            //World.DropStack.AcceptMarkedDrops();
            OptionsManager.Start();
            Task.Factory.StartNew(Activate, _ctsBot.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private async void OnCancel()
        {
            _ctsBot?.Cancel(throwOnFirstException: false);
            Player.CancelTarget();
            if (BotManager.Instance.chkExitCombatUponStop.Checked)
            {
                BotUtilities.MoveOutOfCombat();
            }
            BotUtilities.ActiveBot = false;
            BotUtilities.HasLoadedQuests = false;
            World.ItemDropped -= OnItemDropped;
            Player.Quests.QuestsLoaded -= OnQuestsLoaded;
            Player.Quests.QuestCompleted -= OnQuestCompleted;
            //_questDelayCounter.Stop();
            //_boostDelayCounter.Stop();
            OptionsManager.Stop();
            BotData.BotState = BotData.State.Others;
            if (BotManager.Instance.lstItems.Items.Count > 0 && BotManager.Instance.chkBankOnStop.Checked)
            {
                foreach (InventoryItem item in Player.Inventory.Items)
                {
                    if (!item.IsEquipped && item.Category != "Class" && item.Name != "Treasure Potion" && BotManager.Instance.lstItems.Items.Contains(item.Name))
                    {
                        Player.Bank.TransferToBank(item.Name);
                        await Task.Delay(70);
                        LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Items List - Item transferred to Bank: {item.Name}.\r\n");
                    }
                }
            }
        }

        private async Task Activate()
        {
            while (IsRunning)
            {
                if (_ctsBot.IsCancellationRequested)
                {
                    return;
                }
                if (!Player.IsLoggedIn)
                {
                    if (!Configuration.AutoRelogin)
                    {
                        break;
                    }
                    OptionsManager.Stop();
                    await AutoRelogin.Login(Configuration.Server, Configuration.RelogDelay, _ctsBot, Configuration.RelogRetryUponFailure);
                    BotUtilities.HasLoadedQuests = false;
                    Index = 0;
                    OptionsManager.Start();
                    await this.LoadBotQuests();
                }
                if (!_ctsBot.IsCancellationRequested)
                {
                    if (Player.IsLoggedIn && !Player.IsAlive)
                    {
                        World.SetSpawnPoint();
                        await this.WaitUntil(() => Player.IsAlive, () => IsRunning && Player.IsLoggedIn, -1);
                        await Task.Delay(1000);
                        BotUtilities.ShouldUseSkill = true;
                        Index = (!Configuration.RestartUponDeath) ? (Index - 1) : 0;
                    }
                    if (Configuration.Boosts.Count > 0)
                    {
                        CheckBoosts();
                    }
                    if (Configuration.RestIfHp)
                    {
                        await RestHealth();
                    }
                    if (Configuration.RestIfMp)
                    {
                        await RestMana();
                    }
                    IndexChanged?.Invoke(Index);
                    cmd = Configuration.Commands[Index];
                    if (cmd.ShouldLog())
                        LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] (Index {Index}) {cmd.LogSpecificCmd()}{cmd.ToString()}.\r\n");
                    await cmd.Execute(this);
                    if (Configuration.BotDelay > 0 && (!Configuration.SkipDelayIndexIf || (Configuration.SkipDelayIndexIf && cmd.RequiresDelay())))
                    {
                        await Task.Delay(_config.BotDelay);
                    }
                    Index++;
                    continue;
                }
            }
        }

        private async Task RestHealth()
        {
            if (Player.Health / (double)Player.HealthMax <= Configuration.RestHp / 100.0)
            {
                BotData.State TempState = BotData.BotState;
                BotData.BotState = BotData.State.Rest;
                if (Configuration.ExitCombatBeforeRest)
                {
                    await BotUtilities.MoveOutOfCombat();
                }
                Player.Rest();
                await this.WaitUntil(() => Player.Health == Player.HealthMax, null, 20, 500);
                BotData.BotState = TempState;
            }
        }

        private async Task RestMana()
        {
            if (Player.Mana / (double)Player.ManaMax <= Configuration.RestMp / 100.0)
            {
                BotData.State TempState = BotData.BotState;
                BotData.BotState = BotData.State.Rest;
                if (Configuration.ExitCombatBeforeRest)
                {
                    await BotUtilities.MoveOutOfCombat();
                }
                Player.Rest();
                await this.WaitUntil(() => Player.Mana >= Player.ManaMax, null, 20, 500);
                BotData.BotState = TempState;
            }
        }

        private async void CheckBoosts()
        {
            List<InventoryItem> boosts = Configuration.Boosts.FindAll((InventoryItem b) => !Player.HasActiveBoost(b.Name));
            if (boosts.Count == 0)
            {
                return;
            }
            foreach (InventoryItem boost in boosts)
            {
                Player.UseBoost(boost.Id);
                await this.WaitUntil(() => Player.HasActiveBoost(boost.Name), null, 6, 500);
            }
        }

        private async Task CheckQuests() 
        {
            List<Quest> quests = new List<Quest>();
            while (IsRunning)
            {
                await Task.Delay(250);
                quests = Configuration.Quests?.Where((Quest q) => q.CanComplete)?.ToList();
                if (quests.Count == 0)
                {
                    continue;
                }
                for (int i = 0; i < quests.Count; i++)
                {
                    await this.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete), () => IsRunning, 10, 500);
                    quests[i].Complete();
                    await this.WaitUntil(() => !Player.Quests.IsInProgress(quests[i].Id), () => IsRunning, 10, 500);
                }
            }
        }

        public int DropDelay { get; set; } = 0;

        public async void OnItemDropped(InventoryItem drop)
        {
            NotifyDrop(drop);
            if (Configuration.EnablePickupAll)
            {
                await Task.Delay(DropDelay);
                World.DropStack.GetDrop(drop.Id);
            }
            else if (Configuration.EnablePickup && Configuration.Drops.Any((string d) => d.Equals(drop.Name, StringComparison.OrdinalIgnoreCase)))
            {
                await Task.Delay(DropDelay);
                World.DropStack.GetDrop(drop.Id);
            }
            else if (Configuration.EnablePickupAcTagged)
            {
                if (drop.IsAcItem)
                {
                    await Task.Delay(DropDelay);
                    World.DropStack.GetDrop(drop.Id);
                }
            }
        }

        private void NotifyDrop(InventoryItem drop)
        {
            if (Configuration.NotifyUponDrop.Count > 0 && Configuration.NotifyUponDrop.Any((string d) => d.Equals(drop.Name, StringComparison.OrdinalIgnoreCase)))
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Beep();
                }
            }
        }

        public async void OnQuestsLoaded(List<Quest> quests)
        {
            await Task.Delay(10);
            List<Quest> configQuests = quests.Where(q => Configuration.Quests.Any((Quest cq) => cq.Id == q.Id)).ToList();
            if (configQuests.Count == 0)
            {
                return;
            }
            for (int i = 0; i < configQuests.Count; i++)
            {
                await this.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), () => IsRunning, 10, 500);
                configQuests[i].Accept();
            }
        }

        public async void OnQuestCompleted(CompletedQuest quest)
        {
            await Task.Delay(10);
            Quest configQuest = Configuration.Quests.Find(q => q.Id == quest.Id);
            if (configQuest == null)
            {
                return;
            }
            await this.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), () => IsRunning, 10, 500);
            configQuest.Accept();
        }

        public static List<string> DropsInInventory { get; set; } = new List<string>();

        public bool HasDropInInventory(string item)
        {
            if (DropsInInventory.Contains(item))
            {
                DropsInInventory.Remove(item);
                return true;
            }
            return false;
        }

        public Stopwatch questFailureCounter = new Stopwatch();

        public int questCompletionFailure = 0;

        public async void ReloginOnQuestFailure()
        {
            if (questFailureCounter.ElapsedMilliseconds <= 15000 && questCompletionFailure >= 3)
            {
                questFailureCounter.Stop();
                questCompletionFailure = 0;
                Server server = Server.List.Find((Server s) => s.Name.Equals(World.ServerName(), StringComparison.OrdinalIgnoreCase));
                Player.Logout();
                if (!BotManager.Instance.chkRelog.Checked)
                {
                    Root.Instance.login_cts = new CancellationTokenSource();
                    await BotManager.Instance.ActiveBotEngine.WaitUntil(() => AutoRelogin.LoginLabel, () => !Root.Instance.login_cts.IsCancellationRequested, 5, 1500);
                    AutoRelogin.LoginExecute();
                    await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !AutoRelogin.IsClientLoading("Account"), () => !Root.Instance.login_cts.IsCancellationRequested, 10, 500);
                    AutoRelogin.ForceLogin(server, Root.Instance.login_cts);
                }
            }
        }

        public Stopwatch buyFailureCounter = new Stopwatch();

        public int itemBuyFailure = 0;

        public async void ReloginOnItemBuyFailure()
        {
            if (buyFailureCounter.ElapsedMilliseconds <= 15000 && itemBuyFailure >= 3)
            {
                buyFailureCounter.Stop();
                itemBuyFailure = 0;
                Server server = Server.List.Find((Server s) => s.Name.Equals(World.ServerName(), StringComparison.OrdinalIgnoreCase));
                Player.Logout();
                if (!BotManager.Instance.chkRelog.Checked)
                {
                    Root.Instance.login_cts = new CancellationTokenSource();
                    await BotManager.Instance.ActiveBotEngine.WaitUntil(() => AutoRelogin.LoginLabel, () => !Root.Instance.login_cts.IsCancellationRequested, 5, 1500);
                    AutoRelogin.LoginExecute();
                    await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !AutoRelogin.IsClientLoading("Account"), () => !Root.Instance.login_cts.IsCancellationRequested, 10, 500);
                    AutoRelogin.ForceLogin(server, Root.Instance.login_cts);
                }
            }
        }

    }
}