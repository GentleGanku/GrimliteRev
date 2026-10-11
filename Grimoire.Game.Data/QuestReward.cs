using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class QuestReward
    {
        [JsonProperty("iCP")]
        public int ClassPoints
        {
            get;
            set;
        }

        [JsonProperty("intGold")]
        public int Gold
        {
            get;
            set;
        }

        [JsonProperty("intExp")]
        public int Experience
        {
            get;
            set;
        }

        [JsonProperty("typ")]
        public string Type
        {
            get;
            set;
        }
    }
}