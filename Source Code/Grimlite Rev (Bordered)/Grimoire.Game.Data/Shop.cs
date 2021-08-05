using Grimoire.Tools;
using Newtonsoft.Json;
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

        public static bool IsShopLoaded => Flash.Call<bool>("IsShopLoaded", new string[0]);

        public static void BuyItem(string name)
        {
            Flash.Call("BuyItem", name);
        }

        public static void ResetShopInfo()
        {
            Flash.Call("ResetShopInfo", new string[0]);
        }

        public static void Load(int id)
        {
            Flash.Call("LoadShop", id.ToString());
        }

        public static void SellItem(string name)
        {
            Flash.Call("SellItem", name);
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