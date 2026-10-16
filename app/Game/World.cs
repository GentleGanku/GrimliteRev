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

        public static List<Map> Maps = new List<Map>();

        public static Map CurrentMap = new Map();

        public static List<ShopInfo> Shops = new List<ShopInfo>();

        public static ShopInfo CurrentShop = new ShopInfo();

        public static List<InventoryItem> Drops = new List<InventoryItem>();

        public static Server CurrentServer = new Server();

        public static List<Quest> Quests = new List<Quest>();

        public static void RefreshDictionary() => _players = JsonConvert.DeserializeObject<Dictionary<string, PlayerInfo>>(Flash.Call("Players", new object[0]));

        public static Dictionary<string, PlayerInfo> _players;
        /// <summary>
        /// Gets a list of all players in the current map.
        /// </summary>
        public static List<PlayerInfo> Players => _players.Values.ToList();

        public static List<ShopInfo> LoadedShops;

        public static DropStack DropStack;

        private static readonly Dictionary<LockActions, string> LockedActions;

        public static List<Monster> VisibleMonsters => Flash.Call<List<Monster>>("GetVisibleMonstersInCell", new string[0]);

        public static List<Monster> AvailableMonsters => Flash.Call<List<Monster>>("GetMonstersInCell", new string[0]);

        public static bool IsMapLoading => !Flash.Call<bool>("MapLoadComplete", new string[0]);

        public static List<string> PlayersInMap => Flash.Call<List<string>>("PlayersInMap", new string[0]);

        public static List<InventoryItem> ItemTree => Flash.Call<List<InventoryItem>>("GetItemTree", new string[0]);

        public static string[] Cells => Flash.Call<string[]>("GetCells", new string[0]);

        public static List<string> Pads => Flash.Call<List<string>>("GetPads", new string[0]);

        public static int RoomId => Map.ID;

        public static int RoomNumber => int.Parse(Map.Instance);

        public static event Action<InventoryItem> ItemDropped;

        public static event Action<ShopInfo> ShopLoaded;

        public static void OnItemDropped(InventoryItem drop)
        {
            Action<InventoryItem> itemDropped = ItemDropped;
            itemDropped(drop);
            if (Drops.Find(d => d.Id == drop.Id) == null)
            {
                Drops.Add(drop);
            }
            InventoryItem item = Player.Bank.SavedItems.FirstOrDefault((InventoryItem it) => it.Name.Equals(drop.Name, StringComparison.OrdinalIgnoreCase));
            int dropQty = (item != null) ? item.Quantity : (Player.Inventory.Items.Find((InventoryItem x) => x.Name.Equals(drop.Name)) ?? new InventoryItem()).Quantity;
            LogForm.Instance.AppendDrops($"[Item Drop] ({dropQty}) {drop.Name} x {drop.Quantity} at {DateTime.Now:hh:mm:ss tt}.\r\n");
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

        public static List<Avatar> AvatarsInMap => Flash.Call<List<Avatar>>("AvatarsInMap", new string[0]);

        public static List<Avatar> AvatarsInMyCell => AvatarsInMap.FindAll(a => a.Cell == Player.Cell);

        public static List<Avatar> AvatarsInCell(string cell)
        {
            string targetedCell = (cell != "Wait" && cell != "Blank") ? cell : "Enter";
            return AvatarsInMap.FindAll(a => a.Cell == targetedCell);
        }

        public static List<Monster> MonstersInMap => Flash.Call<List<Monster>>("MonstersInMap", new string[0]);

        public static List<Monster> Monsters { get => GameObject.Select<List<Monster>>("world.monsters", "objData"); set => GameObject.Set("world.monsters", value); }

        public static List<Monster> MonstersInCell(string cell) => Monsters.FindAll(mon => mon.Cell == cell);

        public static List<Monster> VisibleMonster(string cell) => Monsters.FindAll(m => (m.Cell == cell) && m.Alive);

        public static bool IsGameLoaded() => Flash.Call<bool>("InGame", new string[0]);

        public static bool VisibleDropUI { get => GameObject.Get<bool>("cDropsUI.inner_menu.isMenuOpen()"); set => GameObject.Set("cDropsUI.inner_menu.isMenuOpen()", value); }

        public static bool VisibleConnScreen => Flash.Call<bool>("InConnStage", new string[0]);

        public static bool uiLock { get => GameObject.Get<bool>("world.uiLock"); set => GameObject.Set("world.uiLock", value); }

        public static bool VisibleWorld { get => GameObject.Get<bool>("world.visible"); set => GameObject.Set("world.visible", value); }

        public static int ActiveCutscene { get => GameObject.Get<int>("mcExtSWF.numChildren"); set => GameObject.Set("mcExtSWF.numChildren", value); }

        public static void GamePopup(string text, bool red = false) => Flash.Call("GamePopup", text, red);

        public static List<Aura> PlayerAuras { get; set; } = new List<Aura>();

        public static List<Aura> EnemyAuras { get; set; } = new List<Aura>();

        public static event Action<Aura> auraCountdown;

        public static bool FPSBox { get => GameObject.Get<bool>("ui.mcFPS.visible"); set => GameObject.Set("ui.mcFPS.visible", value); }

        public static List<Cell> GetMapCells => Flash.Call<List<Cell>>("GetMapCells", new string[0]);

        public static bool mapLoadInProgress { get => GameObject.Get<bool>("world.mapLoadInProgress"); set => GameObject.Set("world.mapLoadInProgress", value); }

        public static Cell CellInfo(string cell) => Flash.Call<Cell>("GetCellInfo", cell);

        public static List<Cell> AllCellInfo => Flash.Call<List<Cell>>("GetAllPads", true);

        public static void ScanMap() => Flash.Call("GetAllPads", false);

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