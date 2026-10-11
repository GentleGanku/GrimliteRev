using Grimoire.Game.Data;
using Grimoire.Botting;
using Grimoire.Tools;
using Grimoire.Game;
using Grimoire.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers
{
    public class HandlerArea : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "moveToArea"
        };

        private static System.Threading.CancellationTokenSource _cts = new System.Threading.CancellationTokenSource();

        public async void Handle(JsonMessage message)
        {
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.ActiveCutscene == 0 && !World.mapLoadInProgress && Player.IsLoggedIn, () => !_cts.IsCancellationRequested, -1, 500, 500);
            Map map = JsonConvert.DeserializeObject<Map>(message.DataObject.ToString());
            World.CurrentMap = map;
            //if (!World.Maps.Any(m => m.FilePath.Equals(map.FilePath)))
            //{
            //    map.Monsters = World.Monsters;
            //    //World.ScanMap();
            //    //await BotManager.Instance.ActiveBotEngine.WaitUntil(() => !World.BusyClient(), () => !_cts.IsCancellationRequested, -1, 500);
            //    //map.Cells = World.GetMapCells;
            //    World.Cells?.ToList().ForEach(c => map.Cells.Add(new Cell
            //    {
            //        Name = c,
            //        Pads = new List<string>(),
            //        MapItems = new List<MapItem>()
            //    }));
            //    World.Maps.Add(map);
            //}
        }
    }

    //public class HandlerUserData : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[2]
    //    {
    //        "initUserData",
    //        "initUserDatas"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "initUserData":
    //                Map.Players.Where(a => a.Username.Equals((string)message.DataObject["data"]["strUsername"]))
    //                .Select(a => {   
    //                    a.UserID = (int)message.DataObject["uid"];
    //                    a.CP = (int)message.DataObject["data"]["iCP"];
    //                    a.Level = (int)message.DataObject["data"]["intLevel"];
    //                    a.AccessLevel = (int)message.DataObject["data"]["intAccessLevel"];
    //                    a.UpgDays = (int)message.DataObject["data"]["iUpgDays"];
    //                    a.Gender = (string)message.DataObject["data"]["strGender"];
    //                    a.Class = (string)message.DataObject["data"]["strClassName"];
    //                    a.EquippedItems = message.DataObject["eqp"]?.ToObject<Dictionary<string, CosmeticEquipment>>();
    //                    return a;
    //                }).ToList();
    //                //Avatar avatar = Map.Players.Find(a => a.Username.Equals((string)message.DataObject["data"]["strUsername"]));
    //                //LogForm.Instance.AppendDebug(avatar.UserID + " " + avatar.CP + " " + avatar.Level + " " + avatar.AccessLevel + " " + avatar.UpgDays + " " + avatar.Gender + " " + avatar.Class + "\r\n");
    //                break;
    //            case "initUserDatas":
    //                foreach (JToken player in message.DataObject["a"])
    //                {
    //                    Map.Players.Where(a => a.Username.Equals((string)player["data"]["strUsername"]))
    //                    .Select(a => {
    //                        a.UserID = (int)player["uid"];
    //                        a.CP = (int)player["data"]["iCP"];
    //                        a.Level = (int)player["data"]["intLevel"];
    //                        a.AccessLevel = (int)player["data"]["intAccessLevel"];
    //                        a.UpgDays = (int)player["data"]["iUpgDays"];
    //                        a.Gender = (string)player["data"]["strGender"];
    //                        a.Class = (string)player["data"]["strClassName"];
    //                        a.EquippedItems = player["eqp"]?.ToObject<Dictionary<string, CosmeticEquipment>>();
    //                        return a;
    //                    }).ToList();
    //                }
    //                break;
    //        }
    //    }
    //}

    //public class HandlerBounty : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[1]
    //    {
    //        "addGoldExp"
    //    };

    //    //{"t":"xt","b":{"r":-1,"o":{"FactionID":46,"cmd":"addGoldExp","intGold":700,"intExp":0,"typ":"q","bonusGold":350,"iRep":300}}}
    //    public void Handle(JsonMessage message)
    //    {
    //        Map.Players.Where(a => a.Username.Equals(Player.Username))
    //        .Select(a => {
    //            a.Extended.Gold = a.Extended.Gold + (int)message.DataObject?["intGold"] + (int)message.DataObject?["bonusGold"];
    //            a.CP = a.CP + (int)message.DataObject?["iCP"];
    //            a.Extended.Factions.Find(f => f.ID == (int)message.DataObject?["FactionID"]).TotalRep = a.Extended.Factions.Find(f => f.ID == (int)message.DataObject?["FactionID"]).TotalRep + (int)message.DataObject?["iRep"];
    //            return a;
    //        }).ToList();
    //        //Map.Players.Find(a => a.Username.Equals(Player.Username)).Extended.Gold = Map.Players.Find(a => a.Username.Equals(Player.Username)).Extended.Gold + (int)message.DataObject?["intGold"] + (int)message.DataObject?["bonusGold"];
    //        //if (message.DataObject["iCP"] != null)
    //        //{
    //        //    Map.Players.Find(a => a.Username.Equals(Player.Username)).CP = Map.Players.Find(a => a.Username.Equals(Player.Username)).CP + (int)message.DataObject?["iCP"];
    //        //}
    //        ////if (message.DataObject["FactionID"])
    //    }
    //}

    //public class HandlerLevel : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[1]
    //    {
    //        "levelUp"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        Player.Self.Level = (int)message.DataObject["intLevel"];
    //    }
    //}

    //public class HandlerInventory : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[1]
    //    {
    //        "loadInventoryBig"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        Map.Players.Where(a => a.Username.Equals(Player.Username))
    //        .Select(a => {
    //            a.Extended.UsedBankSlots = (int)message.DataObject["bankCount"];
    //            a.Extended.Inventory = JsonConvert.DeserializeObject<List<InventoryItem>>(message.DataObject["items"].ToString());
    //            a.Extended.Factions = JsonConvert.DeserializeObject<List<Faction>>(message.DataObject["factions"].ToString());
    //            a.Extended.HouseInventory = JsonConvert.DeserializeObject<List<InventoryItem>>(message.DataObject["hitems"].ToString());
    //            return a;
    //        }).ToList();
    //    }
    //}

    //public class HandlerSlot : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[3]
    //    {
    //        "buyBagSlots",
    //        "buyBankSlots",
    //        "buyHouseSlots"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        if ((int)message.DataObject["bitSuccess"] != 1)
    //        {
    //            return;
    //        }
    //        Player.Self.Extended.AdventureCoin = Player.Self.Extended.AdventureCoin - ((int)message.DataObject["iSlots"] * 200);
    //        switch (message.Command)
    //        {
    //            case "buyBagSlots":
    //                Player.Self.Extended.InventorySlots = Player.Self.Extended.InventorySlots + (int)message.DataObject["iSlots"];
    //                break;
    //            case "buyBankSlots":
    //                Player.Self.Extended.BankSlots = Player.Self.Extended.BankSlots + (int)message.DataObject["iSlots"];
    //                break;
    //            case "buyHouseSlots":
    //                Player.Self.Extended.HouseSlots = Player.Self.Extended.HouseSlots + (int)message.DataObject["iSlots"];
    //                break;
    //        }
    //    }
    //}

    //public class HandlerLoad : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[3]
    //    {
    //        "loadBank",
    //        "loadShop",
    //        "loadEnhShop"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "loadBank":
    //                if ((int)message.DataObject["bitSuccess"] != 1)
    //                {
    //                    return;
    //                }
    //                Player.Self.Extended.Bank = JsonConvert.DeserializeObject<List<InventoryItem>>(message.DataObject["items"].ToString());
    //                break;
    //            case "loadShop":
    //            case "loadEnhShop":
    //                ShopInfo shop = message.DataObject["shopinfo"].ToObject<ShopInfo>();
    //                World.CurrentShop = shop;
    //                if (World.Shops.Find(s => s.Id == shop.Id) == null)
    //                {
    //                    World.Shops.Add(shop);
    //                }
    //                break;
    //        }
    //    }
    //}

    //public class HandlerTransfer : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[3]
    //    {
    //        "bankFromInv",
    //        "bankToInv",
    //        "bankSwapInv"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "bankFromInv":
    //                if ((int)message.DataObject["bSuccess"] != 1)
    //                {
    //                    return;
    //                }
    //                Map.Players.Where(a => a.Username.Equals(Player.Username))
    //                .Select(a => {
    //                    a.Extended.Bank.Add(Player.Self.Extended.Inventory.Find(i => i.Id == (int)message.DataObject["ItemID"]));
    //                    a.Extended.Inventory.Remove(Player.Self.Extended.Inventory.Find(i => i.Id == (int)message.DataObject["ItemID"]));
    //                    a.Extended.UsedBankSlots++;
    //                    a.Extended.UsedInventorySlots--;
    //                    return a;
    //                }).ToList();
    //                break;
    //            case "bankToInv":
    //                Map.Players.Where(a => a.Username.Equals(Player.Username))
    //                .Select(a => {
    //                    a.Extended.Inventory.Add(Player.Self.Extended.Bank.Find(i => i.Id == (int)message.DataObject["ItemID"]));
    //                    a.Extended.Bank.Remove(Player.Self.Extended.Inventory.Find(i => i.Id == (int)message.DataObject["ItemID"]));
    //                    a.Extended.UsedBankSlots--;
    //                    a.Extended.UsedInventorySlots++;
    //                    return a;
    //                }).ToList();
    //                break;
    //            case "bankSwapInv":
    //                Map.Players.Where(a => a.Username.Equals(Player.Username))
    //                .Select(a => {
    //                    a.Extended.Inventory.Add(Player.Self.Extended.Bank.Find(i => i.Id == (int)message.DataObject["bankItemID"]));
    //                    a.Extended.Bank.Add(Player.Self.Extended.Inventory.Find(i => i.Id == (int)message.DataObject["invItemID"]));
    //                    a.Extended.Inventory.Remove(Player.Self.Extended.Inventory.Find(i => i.Id == (int)message.DataObject["invItemID"]));
    //                    a.Extended.Bank.Remove(Player.Self.Extended.Bank.Find(i => i.Id == (int)message.DataObject["bankItemID"]));
    //                    return a;
    //                }).ToList();
    //                break;
    //        }
    //    }
    //}

    //public class HandlerEnhance : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[2]
    //    {
    //        "enhanceItemShop",
    //        "enhanceItemLocal"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        if (message.DataObject["iCost"] != null)
    //        {
    //            Player.Self.Extended.Gold = Player.Self.Extended.Gold - (int)message.DataObject["iCost"];
    //        }
    //        Player.Self.Extended.Inventory = Flash.Call<List<InventoryItem>>("GetInventoryItems", new string[0]);
    //    }
    //}

    //public class HandlerItem : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[3]
    //    {
    //        "buyItem",
    //        "sellItem",
    //        "removeItem"
    //    };

    //    public async void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "buyItem":
    //                await Task.Delay(10);
    //                if ((int)message.DataObject["bitSuccess"] != 1)
    //                {
    //                    return;
    //                }
    //                InventoryItem item = Player.shopBuyItem;
    //                if (item.IsAcItem)
    //                {
    //                    Player.Self.Extended.AdventureCoin = Player.Self.Extended.AdventureCoin - item.Cost;
    //                }
    //                else
    //                {
    //                    Player.Self.Extended.Gold = Player.Self.Extended.Gold - item.Cost;
    //                }
    //                if (Player.Self.Extended.Inventory.Find(i => i.Id == item.Id) == null)
    //                {
    //                    item.CharItemId = (int)message.DataObject["CharItemID"];
    //                    item.Quantity = 1;
    //                    if (item.HouseItem)
    //                    {
    //                        Player.Self.Extended.HouseInventory.Add(item);
    //                        Player.Self.Extended.UsedHouseSlots++;
    //                    }
    //                    else
    //                    {
    //                        Player.Self.Extended.Inventory.Add(item);
    //                        Player.Self.Extended.InventorySlots++;
    //                    }
    //                }
    //                else
    //                {
    //                    if (item.HouseItem)
    //                    {
    //                        Player.Self.Extended.HouseInventory.Find(i => i.Id == item.Id).Quantity = Player.Self.Extended.HouseInventory.Find(i => i.Id == item.Id).Quantity + item.Quantity;
    //                    }
    //                    else
    //                    {
    //                        Player.Self.Extended.Inventory.Find(i => i.Id == item.Id).Quantity = Player.Self.Extended.Inventory.Find(i => i.Id == item.Id).Quantity + item.Quantity;
    //                    }
    //                }
    //                break;
    //            case "sellItem":
    //            case "removeItem":
    //                int charItemID = (int)message.DataObject["CharItemID"];
    //                int qtyNow = (int)message.DataObject["iQtyNow"];
    //                if (message.Command == "sellItem")
    //                {
    //                    if ((int)message.DataObject["bCoins"] == 1)
    //                    {
    //                        Player.Self.Extended.AdventureCoin = Player.Self.Extended.AdventureCoin + (int)message.DataObject["intAmount"];
    //                    }
    //                    else if (Player.Self.Extended.Gold < 100000000)
    //                    {
    //                        Player.Self.Extended.Gold = Player.Self.Extended.Gold + (int)message.DataObject["intAmount"];
    //                    }
    //                }
    //                if (Player.Self.Extended.Inventory.Find(i => i.CharItemId == charItemID) != null)
    //                {
    //                    if (qtyNow == 0)
    //                    {
    //                        Player.Self.Extended.Inventory.Remove(Player.Self.Extended.Inventory.Find(i => i.CharItemId == charItemID));
    //                    }
    //                    else
    //                    {
    //                        Player.Self.Extended.Inventory.Find(i => i.CharItemId == charItemID).Quantity = qtyNow;
    //                    }
    //                }
    //                else
    //                {
    //                    if (qtyNow == 0)
    //                    {
    //                        Player.Self.Extended.HouseInventory.Remove(Player.Self.Extended.HouseInventory.Find(i => i.CharItemId == charItemID));
    //                    }
    //                    else
    //                    {
    //                        Player.Self.Extended.HouseInventory.Find(i => i.CharItemId == charItemID).Quantity = qtyNow;
    //                    }
    //                }
    //                break;
    //        }
    //    }
    //}

    //public class HandlerEquip : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[2]
    //    {
    //        "equipItem",
    //        "unequipItem"
    //    };

    //    public async void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "equipItem":
    //                Player.Avatar((int)message.DataObject["uid"]).EquippedItems[message.DataObject["strES"].ToString()] = JsonConvert.DeserializeObject<CosmeticEquipment>(message.DataObject.ToString());
    //                if (Player.Self.UserID == (int)message.DataObject["uid"])
    //                {
    //                    if (Player.Self.Extended.Inventory.Find(i => i.EquipmentSlot == message.DataObject["strES"].ToString() && i.IsEquipped) != null)
    //                    {
    //                        Player.Self.Extended.Inventory.Find(i => i.EquipmentSlot == message.DataObject["strES"].ToString() && i.IsEquipped).IsEquipped = false;
    //                    }
    //                    Player.Self.Extended.Inventory.Find(i => i.Id == (int)message.DataObject["ItemID"]).IsEquipped = true;
    //                }
    //                break;
    //            case "unequipItem":
    //                Player.Avatar((int)message.DataObject["uid"]).EquippedItems[message.DataObject["strES"].ToString()] = new CosmeticEquipment();
    //                if (Player.Self.UserID == (int)message.DataObject["uid"])
    //                {
    //                    Player.Self.Extended.Inventory.Find(i => i.Id == (int)message.DataObject["ItemID"]).IsEquipped = false;
    //                }
    //                break;
    //        }
    //    }
    //}

    //public class HandlerClass : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[1]
    //    {
    //        "updateClass",
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        Map.Players.Where(a => a.Username.Equals(Player.Username))
    //        .Select(a => {
    //            a.UserID = (int)message.DataObject["uid"];
    //            a.CP = (int)message.DataObject["iCP"];
    //            a.Class = (string)message.DataObject["sClassName"];
    //            return a;
    //        }).ToList();
    //    }
    //}

    //public class HandlerDrop : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[5]
    //    {
    //        "getDrop",
    //        "addItems",
    //        "forceAddItem",
    //        "Wheel",
    //        "powerGem"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "getDrop":
    //                if ((int)message.DataObject["bSuccess"] != 1)
    //                {
    //                    return;
    //                }
    //                if ((int)message.DataObject["bBank"] != 1)
    //                {
    //                    if (Player.Self.Extended.Inventory.Find(i => i.Id == (int)message.DataObject["ItemID"]) != null)
    //                    {
    //                        Player.Self.Extended.Inventory.Find(i => i.Id == (int)message.DataObject["ItemID"]).Quantity = (int)message.DataObject["iQtyNow"];
    //                    }
    //                    else
    //                    {
    //                        Player.Self.Extended.Inventory.Add(World.Drops.Find(i => i.Id == (int)message.DataObject["ItemID"]));
    //                    }
    //                }
    //                else
    //                {
    //                    Player.Self.Extended.Bank.Find(i => i.Id == (int)message.DataObject["ItemID"]).Quantity = (int)message.DataObject["iQtyNow"];
    //                }
    //                break;
    //            case "addItems":
    //                List<InventoryItem> items = message.DataObject?["items"]?.ToObject<Dictionary<int, InventoryItem>>().Select((KeyValuePair<int, InventoryItem> i) => i.Value).ToList();
    //                foreach (InventoryItem item in items)
    //                {
    //                    var playerInv = (item.TempItem != null) ? (item.TempItem ? Player.Self.Extended.TemporaryInventory : Player.Self.Extended.Inventory) : ((Player.Self.Extended.TemporaryInventory.Find(i => i.Id == item.Id) != null) ? Player.Self.Extended.TemporaryInventory : Player.Self.Extended.Inventory);
    //                    if (playerInv.Find(i => i.Id == item.Id) == null)
    //                    {
    //                        playerInv.Add(item);
    //                    }
    //                    else
    //                    {
    //                        playerInv.Find(i => i.Id == item.Id).Quantity = playerInv.Find(i => i.Id == item.Id).Quantity + item.Quantity;
    //                    }
    //                }
    //                break;
    //            case "forceAddItem":
    //                List<InventoryItem> forceItems = message.DataObject?["items"]?.ToObject<Dictionary<int, InventoryItem>>().Select((KeyValuePair<int, InventoryItem> i) => i.Value).ToList();
    //                foreach (InventoryItem forceItem in forceItems)
    //                {
    //                    if (Player.Self.Extended.Inventory.Find(i => i.Id == forceItem.Id) == null)
    //                    {
    //                        Player.Self.Extended.Inventory.Add(forceItem);
    //                    }
    //                    else
    //                    {
    //                        Player.Self.Extended.Inventory.Find(i => i.Id == forceItem.Id).Quantity = Player.Self.Extended.Inventory.Find(i => i.Id == forceItem.Id).Quantity + forceItem.Quantity;
    //                    }
    //                }
    //                break;
    //            case "Wheel":
    //            case "powerGem":
    //                Player.Self.Extended.Inventory = Flash.Call<List<InventoryItem>>("GetInventoryItems", new string[0]);
    //                break;
    //        }
    //    }
    //}

    //public class HandlerQuest : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[4]
    //    {
    //        "turnIn",
    //        "getQuests",
    //        "getQuests2",
    //        "ccqr",
    //        //"updateQuest"
    //    };

    //    //{"t":"xt","b":{"r":-1,"o":{"cmd":"turnIn","sItems":"34157:6,34158:6"}}}
    //    //{"t":"xt","b":{"r":-1,"o":{"cmd":"ccqr","rewardObj":{"FactionID":46,"iCP":0,"intGold":350,"intExp":350,"typ":"q","iRep":300},"bSuccess":1,"QuestID":4900,"sName":"Kick Some Can"}}}
    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "turnIn":
    //                string[] reqItems = message.DataObject["sItems"].ToString().Split(',');
    //                foreach (string item in reqItems)
    //                {
    //                    if (Player.Self.Extended.Inventory.Find(i => i.Id == int.Parse(item.Split(':')[0])) != null)
    //                    {
    //                        Player.Self.Extended.Inventory.Find(i => i.Id == int.Parse(item.Split(':')[0])).Quantity = Player.Self.Extended.Inventory.Find(i => i.Id == int.Parse(item.Split(':')[0])).Quantity - int.Parse(item.Split(':')[1]);
    //                        if (Player.Self.Extended.Inventory.Find(i => i.Id == int.Parse(item.Split(':')[0])).Quantity < 1)
    //                        {
    //                            Player.Self.Extended.Inventory.Remove(Player.Self.Extended.Inventory.Find(i => i.Id == int.Parse(item.Split(':')[0])));
    //                        }
    //                    }
    //                    else
    //                    {
    //                        Player.Self.Extended.HouseInventory.Find(i => i.Id == int.Parse(item.Split(':')[0])).Quantity = Player.Self.Extended.HouseInventory.Find(i => i.Id == int.Parse(item.Split(':')[0])).Quantity - int.Parse(item.Split(':')[1]);
    //                        if (Player.Self.Extended.HouseInventory.Find(i => i.Id == int.Parse(item.Split(':')[0])).Quantity < 1)
    //                        {
    //                            Player.Self.Extended.HouseInventory.Remove(Player.Self.Extended.HouseInventory.Find(i => i.Id == int.Parse(item.Split(':')[0])));
    //                        }
    //                    }
    //                }
    //                break;
    //            case "getQuests":
    //            case "getQuests2":
    //                World.Quests.AddRange(message.DataObject?["quests"]?.ToObject<Dictionary<int, Quest>>().Select((KeyValuePair<int, Quest> q) => q.Value).ToList());
    //                break;
    //            case "ccqr":
    //                if ((int)message.DataObject["bSuccess"] != 1)
    //                {
    //                    return;
    //                }
    //                Map.Players.Where(a => a.Username.Equals(Player.Username))
    //                .Select(a => {
    //                    a.Extended.Factions.Find(f => f.ID == (int)message.DataObject["rewardObj"]["FactionID"]).TotalRep = a.Extended.Factions.Find(f => f.ID == (int)message.DataObject["FactionID"]).TotalRep + (int)message.DataObject["rewardObj"]["iRep"];
    //                    a.CP = a.CP + (int)message.DataObject["rewardObj"]["iCP"];
    //                    a.Extended.Gold = a.Extended.Gold + (int)message.DataObject["rewardObj"]["intGold"];
    //                    return a;
    //                }).ToList();
    //                break;
    //            case "updateQuest":
    //                //{"t":"xt","b":{"r":-1,"o":{"cmd":"updateQuest","iValue":10,"iIndex":159}}}
    //                break;
    //        }
    //    }
    //}

    //public class HandlerTls : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[2]
    //    {
    //        "uotls",
    //        "mtls",
    //        //"playerDeath"
    //    };

    //    //{"t":"xt","b":{"r":-1,"o":{"cmd":"uotls","o":{"strFrame":"Enter","intMP":100,"intState":1,"strPad":"Spawn","intSP":100,"intHP":4185},"unm":"Koringa"}}}
    //    //{"t":"xt","b":{"r":-1,"o":{"id":1,"cmd":"mtls","o":{"intAct":1,"intState":1}}}}
    //    //{"t":"xt","b":{"r":-1,"o":{"userID":69,"monPad":1,"cmd":"playerDeath","entType":"m","did":69}}}
    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "uotls":
    //                if (Map.Players.Find(a => a.Username.Equals(message.DataObject["unm"].ToString(), StringComparison.OrdinalIgnoreCase)) == null)
    //                {
    //                    Map.Players.Add(JsonConvert.DeserializeObject<Avatar>(message.DataObject["o"].ToString()));
    //                }
    //                Map.Players.Where(a => a.Username.Equals(message.DataObject["unm"].ToString(), StringComparison.OrdinalIgnoreCase))
    //                .Select(a => {
    //                    a.CurrentState = (int)message.DataObject["o"]["intState"];
    //                    return a;
    //                }).ToList();
    //                break;
    //            case "mtls":
    //                World.CurrentMap.Monsters.Where(mon => mon.MonMapID == (int)message.DataObject["id"])
    //                .Select(mon => {
    //                    mon.Health = (int)message.DataObject["o"]["intHP"];
    //                    mon.Mana = (int)message.DataObject["o"]["intMP"];
    //                    mon.CurrentState = (int)Monster.State.Idle;
    //                    return mon;
    //                }).ToList();
    //                break;
    //            //case "playerDeath":
    //            //    break;
    //        }
    //    }
    //}

    //public class HandlerAuras : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[3]
    //    {
    //        "clearAuras",
    //        "cb",
    //        "ct"
    //    };

    //    //{"t":"xt","b":{"r":-1,"o":{"cmd":"cb","p":{"Koringa":{"intState":2}},"m":{"3":{"intState":2},"2":{"intState":2},"1":{"intState":2},"6":{"intState":2},"5":{"intState":2},"4":{"intState":2}}}}}
    //    //{"t":"xt","b":{"r":-1,"o":{"cmd":"ct","anims":[{"strFrame":"Enter","cInf":"m:1","fx":"m","animStr":"Attack1,Attack2","tInf":"p:37177"},{"strFrame":"Enter","cInf":"m:2","fx":"m","animStr":"Attack1,Attack2","tInf":"p:37177"}],"p":{"Koringa":{"intHP":2137}}}}}
    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "clearAuras":
    //                Player.Self.Auras.Clear();
    //                break;
    //            case "cb":
    //                Dictionary<string, Avatar> avatars = message.DataObject?["p"]?.ToObject<Dictionary<string, Avatar>>();
    //                if (avatars != null)
    //                {
    //                    foreach (Avatar avatar in avatars.Select((KeyValuePair<string, Avatar> a) => a.Value).ToList())
    //                    {
    //                        Player.Avatar(avatar.Username).CurrentState = avatar.CurrentState;
    //                    }
    //                }
    //                Dictionary<int, Monster> monsters = message.DataObject?["m"]?.ToObject<Dictionary<int, Monster>>();
    //                if (monsters != null)
    //                {
    //                    foreach (Monster monster in monsters.Select((KeyValuePair<int, Monster> m) => m.Value).ToList())
    //                    {
    //                        World.CurrentMap.Monsters.Find(m => m.MonMapID == monster.MonMapID).CurrentState = monster.CurrentState;
    //                    }
    //                }
    //                break;
    //            case "ct":
    //                break;
    //        }
    //    }
    //}

    //public class HandlerAttack : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[2]
    //    {
    //        "sar",
    //        "sars"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
                
    //        }
    //    }
    //}

    //public class HandlerSkill : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[2]
    //    {
    //        "sAct",
    //        "seia"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {

    //        }
    //    }
    //}

    //public class HandlerFaction : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[2]
    //    {
    //        "loadFactions",
    //        "addFaction"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "loadFactions":
    //                Player.Self.Extended.Factions = JsonConvert.DeserializeObject<List<Faction>>(message.DataObject["factions"].ToString());
    //                break;
    //            case "addFaction":
    //                Player.Self.Extended.Factions.Add(JsonConvert.DeserializeObject<Faction>(message.DataObject["faction"].ToString()));
    //                break;
    //        }
    //    }
    //}

    //public class HandlerBoost : IJsonMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[4]
    //    {
    //        "xpboost",
    //        "gboost",
    //        "repboost",
    //        "cpboost"
    //    };

    //    public void Handle(JsonMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "xpboost":
    //                Player.Self.Extended.BoostXP = (message.DataObject["op"].ToString() == "+") ? true : false;
    //                break;
    //            case "gboost":
    //                Player.Self.Extended.BoostGold = (message.DataObject["op"].ToString() == "+") ? true : false;
    //                break;
    //            case "repboost":
    //                Player.Self.Extended.BoostRep = (message.DataObject["op"].ToString() == "+") ? true : false;
    //                break;
    //            case "cpboost":
    //                Player.Self.Extended.BoostCP = (message.DataObject["op"].ToString() == "+") ? true : false;
    //                break;
    //        }
    //    }
    //}

}