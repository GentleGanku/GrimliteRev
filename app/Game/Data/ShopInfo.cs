using Grimoire.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grimoire.Game.Data
{
    public class ShopInfo
    {

        /// <summary>
        /// The exclusivity state of the shop (either for Member or not).
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bUpgrd")]
        public bool Member { get; set; }

        /// <summary>
        /// The list of items the shop has.
        /// </summary>
        [JsonProperty("items")]
        public List<InventoryItem> Items { get; set; }

        /// <summary>
        /// The shop's field.
        /// </summary>
        [JsonProperty("sField")]
        public string Field { get; set; }

        /// <summary>
        /// The ID of the shop.
        /// </summary>
        [JsonProperty("ShopID")]
        public int Id { get; set; }

        /// <summary>
        /// The exclusivity state of the shop (either for Staff or not).
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bStaff")]
        public bool Staff { get; set; }

        /// <summary>
        /// The type of the shop (either as House Shop or not).
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bHouse")]
        public bool House { get; set; }

        /// <summary>
        /// The name of the shop.
        /// </summary>
        [JsonProperty("sName")]
        public string Name { get; set; }

        /// <summary>
        /// The location(s) of the shop (if more than 1 location, the substrings are divided by the comma (",").
        /// </summary>
        [JsonProperty("Location")]
        public string Location { get; set; }

        /// <summary>
        /// The index of the shop.
        /// </summary>
        [JsonProperty("iIndex")]
        public int Index { get; set; }
    }
}