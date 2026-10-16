using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grimoire.Game.Data
{
    public class Cell
    {
        #region Properties

        /// <summary>
        /// The name of the cell.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The list of pads in the cell.
        /// </summary>
        [JsonProperty("pads")]
        public List<string> Pads
        {
            get;
            set;
        }

        /// <summary>
        /// The list of map items in the cell.
        /// </summary>
        [JsonProperty("mapItems")]
        public List<MapItem> MapItems
        {
            get;
            set;
        }

        #endregion
    }
}
