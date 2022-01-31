using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using Grimoire.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Game
{
    public static class World
    {
        public static void RefreshDictionary() => _players = JsonConvert.DeserializeObject<Dictionary<string, PlayerInfo>>(Flash.Call("Players", new object[0]));

        public static Dictionary<string, PlayerInfo> _players;
        /// <summary>
        /// Gets a list of all players in the current map.
        /// </summary>
        public static List<PlayerInfo> Players => _players.Values.ToList();

        public static List<ShopInfo> LoadedShops;

        public static DropStack DropStack;

        private static readonly Dictionary<LockActions, string> LockedActions;

        public static List<Monster> AvailableMonsters => Flash.Call<List<Monster>>("GetMonstersInCell", new string[0]);

        public static bool IsMapLoading => !Flash.Call<bool>("MapLoadComplete", new string[0]);

        public static List<string> PlayersInMap => Flash.Call<List<string>>("PlayersInMap", new string[0]);

        public static List<InventoryItem> ItemTree => Flash.Call<List<InventoryItem>>("GetItemTree", new string[0]);

        public static string[] Cells => Flash.Call<string[]>("GetCells", new string[0]);

        public static int RoomId => Flash.Call<int>("RoomId", new string[0]);

        public static int RoomNumber => Flash.Call<int>("RoomNumber", new string[0]);

        public static event Action<InventoryItem> ItemDropped;

        public static event Action<ShopInfo> ShopLoaded;

        public static void OnItemDropped(InventoryItem drop)
        {
            Action<InventoryItem> itemDropped = ItemDropped;
            string dropQty = (Player.Bank.SavedItems.FirstOrDefault((InventoryItem it) => it.Name.Equals(drop.Name, StringComparison.OrdinalIgnoreCase)) != null) ? "Banked" : $"{(Player.Inventory.Items.Find((InventoryItem x) => x.Name == drop.Name) ?? new InventoryItem()).Quantity}";
            LogForm.Instance.AppendDrops($"[Item Drop] ({dropQty}) {drop.Name} x {drop.Quantity} at {DateTime.Now:hh:mm:ss tt}.\r\n");
            itemDropped(drop);
        }

        public static void OnShopLoaded(ShopInfo shopInfo)
        {
            ShopLoaded?.Invoke(shopInfo);
            LoadedShops.Add(shopInfo);
        }

        public static bool IsActionAvailable(LockActions action) => Flash.Call<bool>("IsActionAvailable", LockedActions[action]);

        public static void SetSpawnPoint() => Flash.Call("SetSpawnPoint", new string[0]);

        public static bool IsMonsterAvailable(string name) => Flash.Call<bool>("IsMonsterAvailable", new string[1] { name });

        public static int MonsterHealth(string name) => Flash.Call<int>("MonsterHealth", new string[1] { name });

        public static void ReloadCurrentMap() => Flash.Call("reloadCurrentMap", new string[0]);

        public static void GameMessage(string msg) => Flash.Call("GameMessage", msg);

        public static string ServerTime() => Flash.Call<string>("GetServerTime", new string[0]);

        public static string ServerName() => Flash.Call<string>("GetServerName", new string[0]);

        public static void SendClientPacket(string packet, string type = "str")
        {
            Flash.Call("sendClientPacket", packet, type);
        }

        public static void SendPacket(string packet, string type = "String")
        {
            Flash.CallGameFunction("sfc.send" + type, packet);
        }

        [ObjectBinding("world.uoTree")]
        private static Dictionary<string, PlayerInfo> _ps;
        /// <summary>
        /// Gets a list of all players in the current map.
        /// </summary>
        public static List<PlayerInfo> Ps => _ps.Values.ToList();
        /// <summary>
        /// Gets a list of all players in the current cell.
        /// </summary>
        public static List<PlayerInfo> CellPsInMyCell => Ps.FindAll(p => p.Cell == Player.Cell);
        /// <summary>
        /// Gets a list of all players in the corresponding cell.
        /// </summary>
        public static List<PlayerInfo> CellPs(string cell) => Ps.FindAll(p => p.Cell == cell);

        [ObjectBinding("world.monsters", Select = "objData")]
        public static List<Monster> Monsters { get; }

        public static List<Monster> MonstersInCell(string cell) => Monsters.FindAll(m => m.Cell == cell);

        public static List<Monster> VisibleMonster(string cell) => Monsters.FindAll(m => (m.Cell == cell) && m.Alive);

        public static bool IsGameLoaded() => Flash.Call<bool>("InGame", new string[0]);

        [ObjectBinding("cDropsUI.inner_menu.isMenuOpen()")]
        public static bool VisibleDropUI;

        public static bool VisibleConnScreen => Flash.Call<bool>("InConnStage", new string[0]);

        [ObjectBinding("world.uiLock")]
        public static bool uiLock;

        static World()
        {
            LoadedShops = new List<ShopInfo>();
            DropStack = new DropStack();
            LockedActions = new Dictionary<LockActions, string>(14)
            {
                {
                    LockActions.LoadShop,
                    "loadShop"
                },
                {
                    LockActions.LoadEnhShop,
                    "loadEnhShop"
                },
                {
                    LockActions.LoadHairShop,
                    "loadHairShop"
                },
                {
                    LockActions.EquipItem,
                    "equipItem"
                },
                {
                    LockActions.UnequipItem,
                    "unequipItem"
                },
                {
                    LockActions.BuyItem,
                    "buyItem"
                },
                {
                    LockActions.SellItem,
                    "sellItem"
                },
                {
                    LockActions.GetMapItem,
                    "getMapItem"
                },
                {
                    LockActions.TryQuestComplete,
                    "tryQuestComplete"
                },
                {
                    LockActions.AcceptQuest,
                    "acceptQuest"
                },
                {
                    LockActions.DoIA,
                    "doIA"
                },
                {
                    LockActions.Rest,
                    "rest"
                },
                {
                    LockActions.Who,
                    "who"
                },
                {
                    LockActions.Transfer,
                    "tfer"
                }
            };
        }
    }
}