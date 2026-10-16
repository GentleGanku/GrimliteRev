using Grimoire.Botting;
using Grimoire.Game.Data;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grimoire.Game
{
    public class Avatar
    {
        #region Enums

        public enum State
        {
            Dead,
            Idle,
            InCombat
        }

        public enum Access
        {
            Player = 0,
            Staff = 30,
            Tester = 40,
            Developer = 50,
            Moderator = 60,
            LeadDeveloper = 100
        }

        #endregion

        #region Properties

        /// <summary>
        /// The name of the player, in lowercase.
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
        /// The player's gender (value is either M or F).
        /// </summary>
        [JsonProperty("strGender")]
        public string Gender
        {
            get;
            set;
        }

        /// <summary>
        /// The class that the player is using.
        /// </summary>
        [JsonProperty("strClassName")]
        public string Class
        {
            get;
            set;
        }

        /// <summary>
        /// The rank of the player's class.
        /// </summary>
        public int ClassRank
        {
            get
            {
                return CalculatePoints(CP);
            }
        }

        /// <summary>
        /// The rank of the player's class (in Class Points).
        /// </summary>
        [JsonProperty("iCP")]
        public int CP
        {
            get;
            set;
        }

        /// <summary>
        /// The player's equipped items.
        /// </summary>
        [JsonProperty("eqp")]
        public Dictionary<string, CosmeticEquipment> EquippedItems
        {
            get;
            set;
        }

        /// <summary>
        /// The player's current Health Point(s).
        /// </summary>
        [JsonProperty("intHP")]
        public int HP
        {
            get;
            set;
        }

        /// <summary>
        /// The player's maximum Health Point(s).
        /// </summary>
        [JsonProperty("intHPMax")]
        public int MaxHP
        {
            get;
            set;
        }

        /// <summary>
        /// The player's current Mana Point(s).
        /// </summary>
        [JsonProperty("intMP")]
        public int MP
        {
            get;
            set;
        }

        /// <summary>
        /// The player's maximum Mana Point(s).
        /// </summary>
        [JsonProperty("intMPMax")]
        public int MaxMP
        {
            get;
            set;
        }

        /// <summary>
        /// The player's current Stamina Point(s).
        /// </summary>
        [JsonProperty("intSP")]
        public int SP
        {
            get;
            set;
        }

        /// <summary>
        /// The player's maximum Stamina Point(s).
        /// </summary>
        [JsonProperty("intSPMax")]
        public int MaxSP
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
        /// The remaining member days for the player.
        /// </summary>
        /// 
        [JsonProperty("iUpgDays")]
        public int UpgDays
        {
            get;
            set;
        }

        /// <summary>
        /// The user ID of the player.
        /// </summary>
        [JsonProperty("uid")]
        public int UserID
        {
            get;
            set;
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
        /// The entity type of the player.
        /// </summary>
        [JsonProperty("entType")]
        public string EntType
        {
            get;
            set;
        }

        /// <summary>
        /// The type of the player's access.
        /// </summary>
        public Access AccessType
        {
            get
            {
                return (Access)AccessLevel;
            }
        }

        /// <summary>
        /// The player's access level.
        /// </summary>
        [JsonProperty("intAccessLevel")]
        public int AccessLevel
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
        /// The player's current position.
        /// </summary>
        public float[] Position
        {
            get
            {
                return new float[] { X, Y };
            }
        }

        /// <summary>
        /// The player's state.
        /// </summary>
        [JsonProperty("intState")]
        public int CurrentState
        {
            get;
            set;
        }

        /// <summary>
        /// The extended avatar datas of the player.
        /// </summary>
        public ExtendedAvatar Extended
        {
            get;
            set;
        }

        /// <summary>
        /// The list of auras the player has.
        /// </summary>
        public List<Aura> Auras
        {
            get;
            set;
        }

        /// <summary>
        /// The list of targets the player has.
        /// </summary>
        public List<Monster> Targets
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public int CalculatePoints(int value)
        {
            if (value >= 302500)
                return 10;
            if (value >= 202500)
                return 9;
            if (value >= 129600)
                return 8;
            if (value >= 78400)
                return 7;
            if (value >= 44100)
                return 6;
            if (value >= 22500)
                return 5;
            if (value >= 10000)
                return 4;
            if (value >= 3600)
                return 3;
            if (value >= 900)
                return 2;
            if (value < 900)
                return 1;
            return 0;
        }

        public override string ToString()
        {
            return $"{EntID}: {Name}";
        }

        #endregion
    }
}