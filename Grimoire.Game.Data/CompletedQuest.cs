using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class CompletedQuest
    {
        [JsonProperty("QuestID")]
        public int Id
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

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bSuccess")]
        public bool Success
        {
            get;
            set;
        }

        [JsonProperty("rewardObj")]
        public QuestReward Reward
        {
            get;
            set;
        }
    }
}