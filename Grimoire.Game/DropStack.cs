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
        private readonly List<InventoryItem> _drops = new List<InventoryItem>();

        private readonly List<KeyValuePair<int, Stopwatch>> _cooldown = new List<KeyValuePair<int, Stopwatch>>();

        public int Count => _drops.Count;

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

        public DropStack()
        {
            World.ItemDropped += OnItemDropped;
        }

        public IEnumerator<InventoryItem> GetEnumerator()
        {
            return _drops.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void OnItemDropped(InventoryItem item)
        {
            if (_drops.All((InventoryItem d) => d.Id != item.Id))
            {
                _drops.Add(item);
            }
        }

        public async Task<bool> GetDrop(InventoryItem item)
        {
            return await GetDrop(item.Id);
        }

        public async Task<bool> GetDrop(string itemName)
        {
            InventoryItem inventoryItem = _drops.FirstOrDefault((InventoryItem d) => d.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            bool flag = inventoryItem != null;
            if (flag)
            {
                flag = await GetDrop(inventoryItem.Id, inventoryItem.Name);
            }
            return flag;
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


        public async Task<bool> GetDrop(int itemId, string item = null)
        {
            if (Contains(itemId))
            {
                Player.AcceptDrop(itemId);
                InventoryItem drop = _drops.FirstOrDefault((InventoryItem d) => d.Id == itemId);
                _drops.RemoveAll((InventoryItem d) => d.Id == itemId);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _drops.Clear();
            _cooldown.Clear();
        }

        private bool IsCoolingDown(int itemId)
        {
            KeyValuePair<int, Stopwatch> keyValuePair = _cooldown.FirstOrDefault((KeyValuePair<int, Stopwatch> i) => i.Key == itemId);
            if (keyValuePair.Key != 0)
            {
                return (int)keyValuePair.Value.ElapsedMilliseconds < 3000;
            }
            return false;
        }

        public bool Contains(InventoryItem item)
        {
            return Contains(item.Id);
        }

        public bool Contains(int itemId)
        {
            InventoryItem drop = _drops.FirstOrDefault((InventoryItem d) => d.Id == itemId);
            return drop != null;
        }

        public bool Contains(string itemName)
        {
            return _drops.FirstOrDefault((InventoryItem d) => d.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)) != null || Bot.Instance.HasDropInInventory(itemName);
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

    }
}