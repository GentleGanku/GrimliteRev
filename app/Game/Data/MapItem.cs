using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class MapItem
    {
        #region Properties

        /// <summary>
        /// The ID of the map item.
        /// </summary>
        [JsonProperty("ID")]
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the map item.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The quest ID for the map item.
        /// </summary>
        [JsonProperty("qID")]
        public int QuestID
        {
            get;
            set;
        }

        /// <summary>
        /// The process message for the map item.
        /// </summary>
        [JsonProperty("itemName")]
        public string ProcessMsg
        {
            get;
            set;
        }

        /// <summary>
        /// The collect message for the map item.
        /// </summary>
        [JsonProperty("collectMsg")]
        public string CollectMsg
        {
            get;
            set;
        }

        #endregion
    }
}
