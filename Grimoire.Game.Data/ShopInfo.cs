using Grimoire.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grimoire.Game.Data
{
    public class ShopInfo
    {
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bUpgrd")]
        public bool IsMemberOnly
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

        [JsonProperty("ShopID")]
        public int Id
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bStaff")]
        public bool IsStaffOnly
        {
            get;
            set;
        }

        [JsonProperty("sName")]
        public string Name
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }
    }
}