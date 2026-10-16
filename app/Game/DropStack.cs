using Grimoire.Botting;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                return _drops.Count;
            }
        }

        #endregion

        #region Indexers

        public InventoryItem this[int index]
        {
            get
            {
                if (index >= _drops.Count)
                {
                    return null;
                }
                return _drops[index];
            }
        }

        #endregion

        #region Methods

        public IEnumerator<InventoryItem> GetEnumerator()
        {
            return _drops.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public async Task<bool> GetDrop(string itemName)
        {
            InventoryItem inventoryItem = _drops.FirstOrDefault((InventoryItem d) => d.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            bool success = inventoryItem != null;
            if (success)
            {
                success = await GetDrop(inventoryItem.Id);
            }
            return success;
        }

        public async Task<bool> RemoveAll(int itemId)
        {
            if (Contains(itemId))
            {
                _drops.RemoveAll((InventoryItem d) => d.Id == itemId);
                return true;
            }
            return false;
        }

        public async Task<bool> GetDrop(int itemId)
        {
            if (Contains(itemId) && Player.CanAcceptDrop(itemId))
            {
                InventoryItem drop = _drops.FirstOrDefault((InventoryItem d) => d.Id == itemId);
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
            return _drops.FirstOrDefault((InventoryItem d) => d.Id == itemId) != null;
        }

        public bool Contains(string itemName)
        {
            return ((_drops.FirstOrDefault((InventoryItem d) => d.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)) != null) || (Bot.Instance.HasDropInInventory(itemName)));
        }

        public async Task RejectDrops()
        {
            while (BotManager.Instance.ActiveBotEngine.IsRunning && BotManager.Instance.ActiveBotEngine.Configuration.EnableRejection)
            {
                Configuration configuration = BotManager.Instance.ActiveBotEngine.Configuration;
                string wlArray;
                if (configuration.Drops.Count > 0)
                {
                    string[] whitelisted = configuration.Drops.ToArray();
                    wlArray = string.Join(",", whitelisted).ToLower();
                }
                else
                    wlArray = "";
                Flash.Call("RejectDrop", wlArray);
                await Task.Delay(100);
            }
        }

        private void OnItemDropped(InventoryItem item)
        {
            if (_drops.All((InventoryItem d) => d.Id != item.Id))
            {
                _drops.Add(item);
            }
        }

        #endregion
    }
}