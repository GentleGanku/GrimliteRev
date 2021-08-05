using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Game
{
    public class PlayerInfo
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        [JsonProperty("uoName")]
        public string Name { get; set; }

        /// <summary>
        /// The player's current HP.
        /// </summary>
        [JsonProperty("intHP")]
        public int HP { get; set; }

        /// <summary>
        /// The player's maximum HP.
        /// </summary>
        [JsonProperty("intHPMax")]
        public int MaxHP { get; set; }

        /// <summary>
        /// The player's current MP.
        /// </summary>
        [JsonProperty("intMP")]
        public int MP { get; set; }

        /// <summary>
        /// Whether the player is AFK.
        /// </summary>
        [JsonProperty("afk")]
        public bool AFK { get; set; }

        /// <summary>
        /// Whether the player is Member.
        /// </summary>
        [JsonProperty("bUpg")]
        public bool IsMember { get; set; }

        /// <summary>
        /// The entity ID of the player.
        /// </summary>
        [JsonProperty("entID")]
        public int EntID { get; set; }

        /// <summary>
        /// The player's level.
        /// </summary>
        [JsonProperty("intLevel")]
        public int Level { get; set; }

        /// <summary>
        /// The cell the player is currently in.
        /// </summary>
        [JsonProperty("strFrame")]
        public string Cell { get; set; }

        /// <summary>
        /// The pad the player is currently in.
        /// </summary>
        [JsonProperty("strPad")]
        public string Pad { get; set; }

        /// <summary>
        /// The player's X coordinate.
        /// </summary>
        [JsonProperty("tx")]
        public float X { get; set; }

        /// <summary>
        /// The player's Y coordinate.
        /// </summary>
        [JsonProperty("ty")]
        public float Y { get; set; }

        /// <summary>
        /// The player's state.
        /// </summary>
        [JsonProperty("intState")]
        public int State { get; set; }

        public override string ToString()
        {
            return $"{EntID}: {Name}";
        }
    }
}