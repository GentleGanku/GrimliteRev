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
using System.Windows.Forms;

namespace Grimoire.Botting
{
    public class Bot : IBotEngine
    {
        public bool IsVar(string value)
        {
            return Regex.IsMatch(value, @"\[([^\)]*)\]");
        }

        public string GetVar(string value)
        {
            return Regex.Replace(value, @"[\[\]']+", "");
        }

        public static Bot Instance = new Bot();

        private int _index;

        private Configuration _config;
        
        private bool _isRunning;

        private CancellationTokenSource _ctsBot;

        private Stopwatch _questDelayCounter;

        private Stopwatch _boostDelayCounter;

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

        public void Start(Configuration config)
        {
            IsRunning = true;
            Configuration = config;
            Index = 0;
            BotData.BotState = BotData.State.Others;
            _ctsBot = new CancellationTokenSource();
            _questDelayCounter = new Stopwatch();
            _boostDelayCounter = new Stopwatch();
            World.ItemDropped += OnItemDropped;
            Player.Quests.QuestsLoaded += OnQuestsLoaded;
            Player.Quests.QuestCompleted += OnQuestCompleted;
            _questDelayCounter.Start();
            this.LoadAllQuests();
            this.LoadBankItems();
            CheckBoosts();
            _boostDelayCounter.Start();
            OptionsManager.Start();
            Task.Factory.StartNew(Activate, _ctsBot.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            BotData.BotMap = null;
            BotData.BotCell = null;
            BotData.BotPad = null;
            BotData.BotSkill = null;
            BotData.BotState = BotData.State.Others;
            BotData.SkillSet.Clear();
            for (int i = 0; i < Configuration.Skills.Count; i++)
            {
                if (Configuration.Skills[i].Type == Skill.SkillType.Label)
                {
                    BotData.SkillSet.Add(Configuration.Skills[i].Text.ToUpper(), i);
                }
            }
            if (config.Items.Count > 0)
            {
                Player.Bank.LoadItems();
                foreach (string item in config.Items)
                {
                    if (!Player.Inventory.ContainsItem(item, "*") && Player.Bank.ContainsItem(item))
                    {
                        Player.Bank.TransferFromBank(item);
                        Task.Delay(70);
                        LogForm.Instance.AppendDebug("Transferred from Bank: " + item + "\r\n");
                    }
                    else if (Player.Inventory.ContainsItem(item, "*"))
                    {
                        LogForm.Instance.AppendDebug("Item Already exists in Inventory: " + item + "\r\n");
                    }
                }
            }
            List<InventoryItem> inventory = Player.Inventory.Items;
            int num = (from i in Enumerable.Range(0, config.Items.Count)
                       where inventory.Find((InventoryItem x) => x.Name.ToLower() == config.Items[i].ToLower()) == null
                       select i).Count();
            if (config.Items != null && num > Player.Inventory.AvailableSlots)
            {
                int num2 = config.Items.Count - num - Player.Inventory.AvailableSlots;
                MessageBox.Show(string.Concat
                    (
                        "You don't have enough available inventory slots to use this bot, please bank some items, you need ",
                        config.Items.Count, " Free Inventory spots in total (you need ", num2, " more),"
                    ), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }

        public void Stop()
        {
            _ctsBot?.Cancel(throwOnFirstException: false);
            World.ItemDropped -= OnItemDropped;
            Player.Quests.QuestsLoaded -= OnQuestsLoaded;
            Player.Quests.QuestCompleted -= OnQuestCompleted;
            _questDelayCounter.Stop();
            _boostDelayCounter.Stop();
            OptionsManager.Stop();
            IsRunning = false;
            BotData.BotState = BotData.State.Others;
        }

        private async Task Activate()
        {
            while (true)
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
                    Index = 0;
                    this.LoadAllQuests();
                    this.LoadBankItems();
                    OptionsManager.Start();
                }
                if (!_ctsBot.IsCancellationRequested)
                {
                    if (Player.IsLoggedIn && !Player.IsAlive)
                    {
                        World.SetSpawnPoint();
                        await this.WaitUntil(() => Player.IsAlive, () => IsRunning && Player.IsLoggedIn, -1);
                        Index = (!Configuration.RestartUponDeath) ? (Index - 1) : 0;
                    }
                    if (!_ctsBot.IsCancellationRequested)
                    {
                        if (Configuration.RestIfHp)
                        {
                            await RestHealth();
                        }
                        if (!_ctsBot.IsCancellationRequested)
                        {
                            if (Configuration.RestIfMp)
                            {
                                await RestMana();
                            }
                            if (!_ctsBot.IsCancellationRequested)
                            {
                                this.IndexChanged?.Invoke(Index);
                                IBotCommand cmd = Configuration.Commands[Index];
                                await cmd.Execute(this);
                                if (!_ctsBot.IsCancellationRequested)
                                {
                                    if (Configuration.BotDelay > 0 && (!Configuration.SkipDelayIndexIf || (Configuration.SkipDelayIndexIf && cmd.RequiresDelay())))
                                    {
                                        await Task.Delay(_config.BotDelay);
                                    }
                                    if (!_ctsBot.IsCancellationRequested)
                                    {
                                        if (Configuration.Quests.Count > 0)
                                        {
                                            await CheckQuests();
                                        }
                                        if (!_ctsBot.IsCancellationRequested)
                                        {
                                            if (Configuration.Boosts.Count > 0)
                                            {
                                                CheckBoosts();
                                            }
                                            if (!_ctsBot.IsCancellationRequested)
                                            {
                                                Index++;
                                                continue;
                                            }
                                            return;
                                        }
                                        return;
                                    }
                                    return;
                                }
                                return;
                            }
                            return;
                        }
                        return;
                    }
                    return;
                }
                return;
            }
            Stop();
        }

        private async Task RestHealth()
        {
            if (Player.Health / (double)Player.HealthMax <= Configuration.RestHp / 100.0)
            {
                BotData.State TempState = BotData.BotState;
                BotData.BotState = BotData.State.Rest;
                if (Configuration.ExitCombatBeforeRest)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(500);
                }
                Player.Rest();
                await this.WaitUntil(() => Player.Health >= Player.HealthMax);
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
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(500);
                }
                Player.Rest();
                await this.WaitUntil(() => Player.Mana >= Player.ManaMax);
                BotData.BotState = TempState;
            }
        }

        private void CheckBoosts()
        {
            if (_boostDelayCounter.ElapsedMilliseconds >= 10000)
            {
                foreach (InventoryItem boost in Configuration.Boosts)
                {
                    if (!Player.HasActiveBoost(boost.Name))
                    {
                        Player.UseBoost(boost.Id);
                    }
                }
                _boostDelayCounter.Restart();
            }
        }

        private async Task CheckQuests()
        {
            if (!World.IsActionAvailable(LockActions.TryQuestComplete) || _questDelayCounter.ElapsedMilliseconds < 3000)
            {
                return;
            }
            Quest quest = Configuration.Quests.FirstOrDefault((Quest q) => q.CanComplete);
            if (quest == null)
            {
                return;
            }
            BotData.State TempState = BotData.BotState;
            BotData.BotState = BotData.State.Quest;
            string pCell = Player.Cell;
            string pPad = Player.Pad;
            if (_config.ExitCombatBeforeQuest)
            {
                while (Player.CurrentState == Player.State.InCombat)
                {
                    Player.MoveToCell("Blank", "Left");
                    await Task.Delay(2200);
                }
            }
            quest.Complete();
            if (_config.ExitCombatBeforeQuest && Player.Cell != pCell)
            {
                Player.MoveToCell(pCell, pPad);
            }
            BotData.BotState = TempState;
            _questDelayCounter.Restart();
        }

        public int DropDelay { get; set; } = 1000;
        
        private void OnItemDropped(InventoryItem drop)
        {
            NotifyDrop(drop);
            bool flag = Configuration.Drops.Any((string d) => d.Equals(drop.Name, StringComparison.OrdinalIgnoreCase));
            if (Configuration.EnablePickupAll)
            {
                Task.Delay(DropDelay);
                World.DropStack.GetDrop(drop.Id);
            }
            else if (Configuration.EnablePickup && flag)
            {
                Task.Delay(DropDelay);
                World.DropStack.GetDrop(drop.Id);
            }

            if (Configuration.EnablePickupAcTagged)
            {
                Task.Delay(DropDelay);
                if (drop.IsAcItem)
                {
                    World.DropStack.GetDrop(drop.Id);
                }
            }

            //else if (Configuration.EnableRejectAll)
            //{
            //    World.DropStack.RemoveAll(drop.Id);
            //}
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

        private void OnQuestsLoaded(List<Quest> quests)
        {
            List<Quest> qs = quests.Where((Quest q) => Configuration.Quests.Any((Quest qq) => qq.Id == q.Id)).ToList();
            int count = qs.Count;
            if (qs.Count <= 0)
            {
                return;
            }
            if (count == 1)
            {
                qs[0].Accept();
                return;
            }
            for (int i = 0; i < count; i++)
            {
                int ii = i;
                Task.Run(async delegate
                {
                    await Task.Delay(1000 * ii);
                    qs[ii].Accept();
                });
            }
        }

        private void OnQuestCompleted(CompletedQuest quest)
        {
            Configuration.Quests.FirstOrDefault((Quest q) => q.Id == quest.Id)?.Accept();
        }
    }
}