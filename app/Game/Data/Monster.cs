using System.Collections.Generic;
using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class Monster
    {
        private string _name;

        /// <summary>
        /// The race of the monster.
        /// </summary>
        [JsonProperty("sRace")]
        public string Race { get; set; }

        /// <summary>
        /// The name of the monster.
        /// </summary>
        [JsonProperty("strMonName")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value?.Trim();
            }
        }

        /// <summary>
        /// The ID of the monster.
        /// </summary>
        [JsonProperty("MonID")]
        public int Id { get; set; }

        /// <summary>
        /// The monster's level.
        /// </summary>
        [JsonProperty("iLvl")]
        public int Level { get; set; }

        /// <summary>
        /// The monster's state.
        /// </summary>
        [JsonProperty("intState")]
        public int CurrentState { get; set; }

        /// <summary>
        /// The monster's current health.
        /// </summary>
        [JsonProperty("intHP")]
        public int Health { get; set; }

        /// <summary>
        /// The monster's maximum health.
        /// </summary>
        [JsonProperty("intHPMax")]
        public int MaxHealth { get; set; }

        /// <summary>
        /// The monster's current mana.
        /// </summary>
        [JsonProperty("intMP")]
        public int Mana { get; set; }

        /// <summary>
        /// The monster's maximum mana.
        /// </summary>
        [JsonProperty("intMPMax")]
        public int MaxMana { get; set; }

        /// <summary>
        /// The cell the monster is in.
        /// </summary>
        [JsonProperty("strFrame")]
        public string Cell { get; set; }

        /// <summary>
        /// The map ID of the monster.
        /// </summary>
        [JsonProperty("MonMapID")]
        public int MonMapID { get; set; }

        /// <summary>
        /// Whether the monster is alive.
        /// </summary>
        public bool Alive => Health > 0;

        /// <summary>
        /// The list of auras the monster has.
        /// </summary>
        public List<Aura> Auras { get; set; }

        public enum State
        {
            Dead,
            Idle,
            InCombat
        }

    }
}