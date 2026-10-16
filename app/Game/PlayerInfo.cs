using Newtonsoft.Json;

namespace Grimoire.Game
{
    public class PlayerInfo
    {
        #region Enums

        public enum State
        {
            Dead,
            Idle,
            InCombat
        }

        #endregion

        #region Properties

        /// <summary>
        /// The name of the player, in lowercase version.
        /// </summary>
        [JsonProperty("uoName")]
        public string Name 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The name of the player.
        /// </summary>
        [JsonProperty("strUsername")]
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// The class name of the player.
        /// </summary>
        [JsonProperty("strClassName")]
        public string Class
        {
            get;
            set;
        }

        /// <summary>
        /// The player's current HP.
        /// </summary>
        [JsonProperty("intHP")]
        public int HP
        {
            get;
            set;
        }

        /// <summary>
        /// The player's maximum HP.
        /// </summary>
        [JsonProperty("intHPMax")]
        public int MaxHP
        {
            get;
            set;
        }

        /// <summary>
        /// The player's current MP.
        /// </summary>
        [JsonProperty("intMP")]
        public int MP
        {
            get;
            set;
        }

        /// <summary>
        /// Whether the player is AFK.
        /// </summary>
        [JsonProperty("afk")]
        public bool AFK
        {
            get;
            set;
        }

        /// <summary>
        /// Whether the player is Member.
        /// </summary>
        /// 
        public bool Member
        {
            get
            {
                if (UpgDays >= 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// The entity ID of the player.
        /// </summary>
        [JsonProperty("entID")]
        public int EntID
        {
            get;
            set;
        }

        /// <summary>
        /// The player's level.
        /// </summary>
        [JsonProperty("intLevel")]
        public int Level
        {
            get;
            set;
        }

        /// <summary>
        /// The cell the player is currently in.
        /// </summary>
        [JsonProperty("strFrame")]
        public string Cell
        {
            get;
            set;
        }

        /// <summary>
        /// The pad the player is currently in.
        /// </summary>
        [JsonProperty("strPad")]
        public string Pad
        {
            get;
            set;
        }

        /// <summary>
        /// The player's X coordinate.
        /// </summary>
        [JsonProperty("tx")]
        public float X
        {
            get;
            set;
        }

        /// <summary>
        /// The player's Y coordinate.
        /// </summary>
        [JsonProperty("ty")]
        public float Y
        {
            get;
            set;
        }

        /// <summary>
        /// The player's state.
        /// </summary>
        [JsonProperty("intState")]
        public State CurrentState
        {
            get;
            set;
        }

        /// <summary>
        /// The remaining member days for the player.
        /// </summary>
        /// 
        [JsonProperty("iUpgDays")]
        private int UpgDays
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{EntID}: {Name}";
        }

        #endregion
    }
}