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
        #region Fields

        public static List<Map> Maps = new List<Map>();
        public static Map CurrentMap = new Map();
        public static List<ShopInfo> Shops = new List<ShopInfo>();
        public static ShopInfo CurrentShop = new ShopInfo();
        public static List<InventoryItem> Drops = new List<InventoryItem>();
        public static Server CurrentServer = new Server();
        public static List<Quest> Quests = new List<Quest>();
        public static Dictionary<string, PlayerInfo> _players;
        public static List<ShopInfo> LoadedShops;
        public static DropStack DropStack;
        private static readonly Dictionary<LockActions, string> LockedActions;

        #endregion

        #region Constructors

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

        #endregion

        #region Events

        public static event Action<InventoryItem> ItemDropped;
        public static event Action<ShopInfo> ShopLoaded;
        public static event Action<Aura> auraCountdown;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all players in the current map.
        /// </summary>
        public static List<PlayerInfo> Players
        {
            get
            {
                return _players.Values.ToList();
            }
        }

        public static List<Monster> VisibleMonsters
        {
            get
            {
                return Flash.Call<List<Monster>>("GetVisibleMonstersInCell", new string[0]);
            }
        }

        public static List<Monster> AvailableMonsters
        {
            get
            {
                return Flash.Call<List<Monster>>("GetMonstersInCell", new string[0]);
            }
        }

        public static bool IsMapLoading
        {
            get
            {
                return !Flash.Call<bool>("MapLoadComplete", new string[0]);
            }
        }

        public static List<string> PlayersInMap
        {
            get
            {
                return Flash.Call<List<string>>("PlayersInMap", new string[0]);
            }
        }

        public static List<InventoryItem> ItemTree
        {
            get
            {
                return Flash.Call<List<InventoryItem>>("GetItemTree", new string[0]);
            }
        }

        public static string[] Cells
        {
            get
            {
                return Flash.Call<string[]>("GetCells", new string[0]);
            }
        }

        public static List<string> Pads
        {
            get
            {
                return Flash.Call<List<string>>("GetPads", new string[0]);
            }
        }

        public static int RoomId
        {
            get
            {
                return Map.ID;
            }
        }

        public static int RoomNumber
        {
            get
            {
                return int.Parse(Map.Instance);
            }
        }

        public static List<Avatar> AvatarsInMap
        {
            get
            {
                return Flash.Call<List<Avatar>>("AvatarsInMap", new string[0]);
            }
        }

        public static List<Avatar> AvatarsInMyCell
        {
            get
            {
                return AvatarsInMap.FindAll(a => a.Cell == Player.Cell);
            }
        }

        public static List<Monster> MonstersInMap
        {
            get
            {
                return Flash.Call<List<Monster>>("MonstersInMap", new string[0]);
            }
        }

        public static List<Monster> Monsters
        {
            get
            {
                return GameObject.Select<List<Monster>>("world.monsters", "objData");
            }
            set
            {
                GameObject.Set("world.monsters", value);
            }
        }

        public static bool VisibleDropUI
        {
            get
            {
                return GameObject.Get<bool>("cDropsUI.inner_menu.isMenuOpen()");
            }
            set
            {
                GameObject.Set("cDropsUI.inner_menu.isMenuOpen()", value);
            }
        }

        public static bool VisibleConnScreen
        {
            get
            {
                return Flash.Call<bool>("InConnStage", new string[0]);
            }
        }

        public static bool uiLock
        {
            get
            {
                return GameObject.Get<bool>("world.uiLock");
            }
            set
            {
                GameObject.Set("world.uiLock", value);
            }
        }

        public static bool VisibleWorld
        {
            get
            {
                return GameObject.Get<bool>("world.visible");
            }
            set
            {
                GameObject.Set("world.visible", value);
            }
        }

        public static int ActiveCutscene
        {
            get
            {
                return GameObject.Get<int>("mcExtSWF.numChildren");
            }
            set
            {
                GameObject.Set("mcExtSWF.numChildren", value);
            }
        }

        public static List<Aura> PlayerAuras
        {
            get;
            set;
        } = new List<Aura>();

        public static List<Aura> EnemyAuras
        {
            get;
            set;
        } = new List<Aura>();

        public static bool FPSBox
        {
            get
            {
                return GameObject.Get<bool>("ui.mcFPS.visible");
            }
            set
            {
                GameObject.Set("ui.mcFPS.visible", value);
            }
        }

        public static List<Cell> GetMapCells
        {
            get
            {
                return Flash.Call<List<Cell>>("GetMapCells", new string[0]);
            }
        }

        public static bool mapLoadInProgress
        {
            get
            {
                return GameObject.Get<bool>("world.mapLoadInProgress");
            }
            set
            {
                GameObject.Set("world.mapLoadInProgress", value);
            }
        }

        public static List<Cell> AllCellInfo
        {
            get
            {
                return Flash.Call<List<Cell>>("GetAllPads", true);
            }
        }

        #endregion

        #region Methods

        public static void RefreshDictionary() => _players = JsonConvert.DeserializeObject<Dictionary<string, PlayerInfo>>(Flash.Call("Players", new object[0]));

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

        public static List<Avatar> AvatarsInCell(string cell)
        {
            string targetedCell = (cell != "Wait" && cell != "Blank") ? cell : "Enter";
            return AvatarsInMap.FindAll(a => a.Cell == targetedCell);
        }

        public static List<Monster> MonstersInCell(string cell) => Monsters.FindAll(mon => mon.Cell == cell);

        public static List<Monster> VisibleMonster(string cell) => Monsters.FindAll(m => (m.Cell == cell) && m.Alive);

        public static bool IsGameLoaded() => Flash.Call<bool>("InGame", new string[0]);

        public static void GamePopup(string text, bool red = false) => Flash.Call("GamePopup", text, red);

        public static Cell CellInfo(string cell) => Flash.Call<Cell>("GetCellInfo", cell);

        public static void ScanMap() => Flash.Call("GetAllPads", false);

        #endregion
    }
}