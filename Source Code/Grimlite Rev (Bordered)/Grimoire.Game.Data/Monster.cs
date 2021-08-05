using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class Monster
    {
        private string _name;

        [JsonProperty("sRace")]
        public string Race
        {
            get;
            set;
        }

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

        [JsonProperty("MonID")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("iLvl")]
        public int Level
        {
            get;
            set;
        }

        [JsonProperty("intState")]
        public int State
        {
            get;
            set;
        }

        [JsonProperty("intHP")]
        public int Health
        {
            get;
            set;
        }

        [JsonProperty("intHPMax")]
        public int MaxHealth
        {
            get;
            set;
        }
    }
}