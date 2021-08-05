using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class TempItem
    {
        private string _name;

        [JsonProperty("sName")]
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

        [JsonProperty("sDesc")]
        public string Description
        {
            get;
            set;
        }

        [JsonProperty("ItemID")]
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

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bCoins")]
        public bool IsAcItem
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

        [JsonProperty("iQty")]
        public int Quantity
        {
            get;
            set;
        }

        [JsonProperty("sType")]
        public string Type
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

        [JsonProperty("iCost")]
        public int Cost
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

        public bool ShouldSerializeDescription()
        {
            return false;
        }

        public bool ShouldSerializeLevel()
        {
            return false;
        }

        public bool ShouldSerializeIsAcItem()
        {
            return false;
        }

        public bool ShouldSerializeLink()
        {
            return false;
        }

        public bool ShouldSerializeType()
        {
            return false;
        }

        public bool ShouldSerializeIsMemberOnly()
        {
            return false;
        }

        public bool ShouldSerializeCost()
        {
            return false;
        }

        public bool ShouldSerializeMaxStack()
        {
            return false;
        }
    }
}