using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class QuestInfo
    {
        #region Fields

        public itemsS sItems;
        public itemsR rItems;

        #endregion
    }

    public class itemsS
    {
        #region Properties

        [JsonProperty("sIcon")]
        public string Icon
        {
            get;
            set;
        }

        [JsonProperty("ItemID")]
        public int ItemId
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

        [JsonProperty("sLink")]
        public string Link
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bTemp")]
        public bool Temp
        {
            get;
            set;
        }

        [JsonProperty("sElmt")]
        public string sElmt
        {
            get;
            set;
        }

        [JsonProperty("bStaff")]
        public bool Staff
        {
            get;
            set;
        }

        [JsonProperty("iRng")]
        public int iRng
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bCoins")]
        public bool Coins
        {
            get;
            set;
        }

        [JsonProperty("iDPS")]
        public int iDPS
        {
            get;
            set;
        }

        [JsonProperty("sES")]
        public string sES
        {
            get;
            set;
        }

        [JsonProperty("bPTR")]
        public int bPTR
        {
            get;
            set;
        }

        [JsonProperty("iQSIndex")]
        public int iQSIndex
        {
            get;
            set;
        }

        [JsonProperty("sType")]
        public string Category
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

        [JsonProperty("iStk")]
        public int MaxStack
        {
            get;
            set;
        }

        [JsonProperty("iCost")]
        public int Cost
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bUpg")]
        public bool Upgrade
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bHouse")]
        public bool HouseItem
        {
            get;
            set;
        }

        [JsonProperty("iRty")]
        public int iRty
        {
            get;
            set;
        }

        [JsonProperty("bQuest")]
        public int Quest
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

        [JsonProperty("iQSValue")]
        public int iQSValue
        {
            get;
            set;
        }

        [JsonProperty("sReqQuests")]
        public int RequiredQuest
        {
            get;
            set;
        }

        #endregion
    }

    public class itemsR
    {
        #region Properties

        [JsonProperty("sIcon")]
        public string Icon
        {
            get;
            set;
        }

        [JsonProperty("ItemID")]
        public int ItemId
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

        [JsonProperty("sLink")]
        public string Link
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bTemp")]
        public bool Temp
        {
            get;
            set;
        }

        [JsonProperty("sElmt")]
        public string sElmt
        {
            get;
            set;
        }

        [JsonProperty("bStaff")]
        public bool Staff
        {
            get;
            set;
        }

        [JsonProperty("iRng")]
        public int iRng
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bCoins")]
        public bool Coins
        {
            get;
            set;
        }

        [JsonProperty("iDPS")]
        public int iDPS
        {
            get;
            set;
        }

        [JsonProperty("sES")]
        public string sES
        {
            get;
            set;
        }

        [JsonProperty("bPTR")]
        public int bPTR
        {
            get;
            set;
        }

        [JsonProperty("iQSIndex")]
        public int iQSIndex
        {
            get;
            set;
        }

        [JsonProperty("sType")]
        public string Category
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

        [JsonProperty("iStk")]
        public int MaxStack
        {
            get;
            set;
        }

        [JsonProperty("iCost")]
        public int Cost
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bUpg")]
        public bool Upgrade
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bHouse")]
        public bool HouseItem
        {
            get;
            set;
        }

        [JsonProperty("iRty")]
        public int iRty
        {
            get;
            set;
        }

        [JsonProperty("bQuest")]
        public int Quest
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

        [JsonProperty("iQSValue")]
        public int iQSValue
        {
            get;
            set;
        }

        [JsonProperty("sReqQuests")]
        public int RequiredQuest
        {
            get;
            set;
        }

        #endregion
    }
}