using Grimoire.UI;
using Grimoire.Botting;
using Grimoire.Tools;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Grimoire.Game.Data
{
    public class Shop
    {
        public static Shop Instance = new Shop();

        [JsonProperty("sName")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("ShopID")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("items")]
        public List<InventoryItem> Items
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public static bool IsShopLoaded
        {
            get
            {
                return Flash.Call<bool>("IsShopLoaded", new string[0]);
            }
        }

        public static bool CanBuyItem(string name) => Flash.Call<bool>("CanBuyItem", name);

        public static bool CanBuyItemById(int ItemID, int ShopItemID) => Flash.Call<bool>("CanBuyItemById", ItemID.ToString(), ShopItemID.ToString());

        public async static Task BuyItem(string name, bool manual = false)
        {
            if (!CanBuyItem(name))
            {
                return;
            }
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem), null, 10, 500);
            await BotUtilities.MoveOutOfCombat();
            InventoryItem i = Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            int iQtyBefore = (i != null) ? i.Quantity : 0;
            Flash.Call("BuyItem", name, manual);
            if (i != null)
            {
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => i.Quantity > iQtyBefore, null, 6, 500);
            }
            else
            {
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.Inventory.ContainsItem(name), null, 6, 500);
            }
        }

        public async static Task BuyItemById(int ItemID, int ShopID, int ShopItemID, bool manual = false)
        {
            if (manual && !CanBuyItemById(ItemID, ShopItemID))
            {
                return;
            }
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem), null, 10, 500);
            await BotUtilities.MoveOutOfCombat();
            InventoryItem i = Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Id == ItemID);
            int iQtyBefore = (i != null) ? i.Quantity : 0;
            Flash.Call("BuyItemById", ItemID, ShopID, ShopItemID, manual);
            if (i != null)
            {
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => i.Quantity > iQtyBefore, null, 6, 500);
            }
            else
            {
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Id == ItemID) != null, null, 6, 500);
            }
        }

        public static void ResetShopInfo()
        {
            Flash.Call("ResetShopInfo", new string[0]);
        }

        public async static Task Load(int id)
        {
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.IsActionAvailable(LockActions.LoadShop), null, 10, 500);
            await BotUtilities.MoveOutOfCombat();
            ResetShopInfo();
            Flash.Call("LoadShop", id.ToString());
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => IsShopLoaded, null, 10, 500);
        }

        public async static Task SellItem(string name)
        {
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.IsActionAvailable(LockActions.SellItem), null, 10, 500);
            await BotUtilities.MoveOutOfCombat();
            InventoryItem i = Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (i != null)
            {
                bool single = i.Quantity == 1;
                Flash.Call("SellItem", name);
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => single ? !Player.Inventory.ContainsItem(i.Name) : Player.Inventory.ContainsItem(i.Name, (i.Quantity - 1).ToString()), null, 10, 500);
            }
        }

        public static void LoadHairShop(string id)
        {
            Flash.Call("LoadHairShop", id);
        }

        public static void LoadHairShop(int id)
        {
            Flash.Call("LoadHairShop", id.ToString());
        }

        public static void LoadArmorCustomizer()
        {
            Flash.Call("LoadArmorCustomizer", new string[0]);
        }
    }
}