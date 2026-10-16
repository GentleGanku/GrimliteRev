using Grimoire.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grimoire.Game.Data
{
    public class Quest
    {
        [JsonProperty("sFaction")]
        public string Faction
        {
            get;
            set;
        }

        [JsonProperty("iSlot")]
        public int Slot
        { 
            get; 
            set; 
        }

        [JsonProperty("iValue")]
        public int Value 
        { 
            get; 
            set; 
        }

        [JsonProperty("iClass")]
        public int ClassPointsReward
        {
            get;
            set;
        }

        [JsonProperty("oRewards")]
        [JsonConverter(typeof(QuestRewardConverter))]
        public List<ItemBase> oRewards
        {
            get;
            set;
        }

        [JsonProperty("sDesc")]
        public string Description
        {
            get;
            set;
        }

        [JsonProperty("sEndText")]
        public string EndText
        {
            get;
            set;
        }

        [JsonProperty("iReqRep")]
        public int RequiredReputation
        {
            get;
            set;
        }

        [JsonProperty("iRep")]
        public int ReputationReward
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

        [JsonProperty("turnin")]
        public List<InventoryItem> RequiredItems
        {
            get;
            set;
        }

        [JsonProperty("iGold")]
        public int GoldReward
        {
            get;
            set;
        }

        [JsonProperty("iReqCP")]
        public int RequiredClassPoints
        {
            get;
            set;
        }

        [JsonProperty("QuestID")]
        public int Id
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bOnce")]
        public bool IsNotRepeatable
        {
            get;
            set;
        }

        [JsonProperty("iExp")]
        public int ExperienceReward
        {
            get;
            set;
        }

        [JsonProperty("reward")]
        public List<InventoryItem> Rewards
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
        [JsonProperty("bUpg")]
        public bool IsMemberOnly
        {
            get;
            set;
        }

        [JsonProperty("FactionID")]
        public int FactionId
        {
            get;
            set;
        }

        public string ItemId
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public bool IsInProgress
        {
            get
            {
                return Flash.Call<bool>("IsInProgress", new string[1]
                {
                    Id.ToString()
                });
            }
        }

        public bool CanComplete
        {
            get
            {
                return Flash.Call<bool>("CanComplete", new string[1] { Id.ToString() });
            }
        }

        public void Accept()
        {
            Flash.Call("Accept", Id.ToString());
        }

        public void Complete()
        {
            if (!string.IsNullOrEmpty(ItemId))
            {
                Flash.Call("Complete", Id.ToString(), ItemId);
            }
            else
            {
                Flash.Call("Complete", Id.ToString());
            }
        }

        #region ShouldSerialize
        public bool ShouldSerializeFaction() => false;

        public bool ShouldSerializeClassPointsReward() => false;

        public bool ShouldSerializeDescription() => false;

        public bool ShouldSerializeRequiredReputation() => false;

        public bool ShouldSerializeReputationReward() => false;

        public bool ShouldSerializeLevel() => false;

        public bool ShouldSerializeGoldReward
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeRequiredClassPoints
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeIsNotRepeatable
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeExperienceReward
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeRewards
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeName
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeIsMemberOnly
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeFactionId
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeIsInProgress
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeCanComplete
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}