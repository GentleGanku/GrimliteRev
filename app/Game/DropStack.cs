using Grimoire.Botting;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using Grimoire.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Game
{
    public class DropStack : IReadOnlyList<InventoryItem>, IReadOnlyCollection<InventoryItem>, IEnumerable<InventoryItem>, IEnumerable
    {
        #region Fields

        private readonly List<InventoryItem> _drops = new List<InventoryItem>();
        private readonly List<KeyValuePair<int, Stopwatch>> _cooldown = new List<KeyValuePair<int, Stopwatch>>();

        #endregion

        #region Constructors

        public DropStack()
        {
            World.ItemDropped += OnItemDropped;
        }

        #endregion

        #region Properties

        public List<string> MarkedDrops
        {
            get;
            set;
        } = new List<string>();

        public bool InProcess
        {
            get;
            set;
        } = false;

        public int Count
        {
            get
            {
                lock (_drops)
                {
                    return _drops.Count;
                }
            }
        }

        #endregion

        #region Indexers

        public InventoryItem this[int index]
        {
            get
            {
                lock (_drops)
                {
                    return index >= 0 && index < _drops.Count ? _drops[index] : null;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Enumerates a copy of the drops: packet handlers add drops on the UI thread while the bot reads them on a
        /// worker thread.
        /// </summary>
        public IEnumerator<InventoryItem> GetEnumerator()
        {
            lock (_drops)
            {
                return _drops.ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public async Task<bool> GetDrop(string itemName)
        {
            Bot.ForgetDropInInventory(itemName);
            InventoryItem inventoryItem;
            lock (_drops)
            {
                inventoryItem = _drops.FirstOrDefault((InventoryItem d) => d.Name.Equals(itemName.UnifyQuotes(), StringComparison.OrdinalIgnoreCase));
            }
            bool success = inventoryItem != null;
            if (success)
            {
                success = await GetDrop(inventoryItem.Id);
            }
            return success;
        }

        public async Task<bool> RemoveAll(int itemId)
        {
            lock (_drops)
            {
                return _drops.RemoveAll((InventoryItem d) => d.Id == itemId) > 0;
            }
        }

        public async Task<bool> GetDrop(int itemId)
        {
            if (Contains(itemId) && Player.CanAcceptDrop(itemId))
            {
                InventoryItem drop;
                lock (_drops)
                {
                    drop = _drops.FirstOrDefault((InventoryItem d) => d.Id == itemId);
                }
                InProcess = true;
                Player.AcceptDrop(itemId);
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !World.DropStack.Contains(drop.Name), null, 6, 500);
                InProcess = false;
                return true;
            }
            return false;
        }

        public bool Contains(int itemId)
        {
            lock (_drops)
            {
                return _drops.Any((InventoryItem d) => d.Id == itemId);
            }
        }

        public bool Contains(string itemName)
        {
            bool shown;
            lock (_drops)
            {
                shown = _drops.Any((InventoryItem d) => d.Name.Equals(itemName.UnifyQuotes(), StringComparison.OrdinalIgnoreCase));
            }
            return shown || Bot.Instance.HasDropInInventory(itemName);
        }

        /// <summary>
        /// Rejects the drops that aren't on the drop list until the bot stops or pauses (<paramref name="token"/>). The
        /// setting is read on every pass, so a bot loaded with Load bot can switch rejection on or off.
        /// </summary>
        public async Task RejectDrops(CancellationToken token)
        {
            while (BotManager.Instance.ActiveBotEngine.IsRunning && !token.IsCancellationRequested)
            {
                Configuration configuration = BotManager.Instance.ActiveBotEngine.Configuration;
                if (configuration.EnableRejection)
                {
                    string wlArray;
                    if (configuration.Drops.Count > 0)
                    {
                        string[] whitelisted = configuration.Drops.ToArray();
                        wlArray = string.Join(",", whitelisted).ToLower();
                    }
                    else
                        wlArray = "";
                    Flash.Call("RejectDrop", wlArray);
                }
                await Task.Delay(100);
            }
        }

        private void OnItemDropped(InventoryItem item)
        {
            lock (_drops)
            {
                if (_drops.All((InventoryItem d) => d.Id != item.Id))
                {
                    _drops.Add(item);
                }
            }
        }

        #endregion
    }
}