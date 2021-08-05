using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class BankItem
    {
        public static readonly string[] EquippableCategories = new string[14]
        {
            "Sword",
            "Axe",
            "Dagger",
            "Gun",
            "Bow",
            "Mace",
            "Polearm",
            "Staff",
            "Wand",
            "Class",
            "Armor",
            "Helm",
            "Cape",
            "Item"
        };

        public static readonly string[] Weapons = new string[9]
        {
            "Sword",
            "Axe",
            "Dagger",
            "Gun",
            "Bow",
            "Mace",
            "Polearm",
            "Staff",
            "Wand",
        };

        public static readonly string[] EquippableNonWeapon = new string[5]
        {
            "Class",
            "Armor",
            "Helm",
            "Cape",
            "Pet"
        };

        public static readonly Dictionary<int, string> Enhancement = new Dictionary<int, string> {
            {0, "Unenhanced"},
            {6, "Mage"},
            {9, "Lucky"}
        };

        //Obv: the level of the enhancement, 100 is max as of now
        [JsonProperty("EnhLvl")]
        public int EnhLevel
        {
            get;
            set;
        }

        //Don't know how to use this for anything yet but its here
        [JsonConverter(typeof(IntStringConverter))]
        [JsonProperty("EnhID")]
        public int EnhID
        {
            get;
            set;
        }

        public string EnhancementType => Enhancement[EnhID];

        [JsonProperty("EnhPatternID")]
        public int EnhPatternId
        {
            get;
            set;
        }

        // WIP: complete this list (check for EnhPatternID, e.g. 6 is Mage)
        [JsonProperty("CharItemID")]
        public float CharItemID
        {
            get;
            set;
        }

        [JsonProperty("EnhPatternID")]
        public string EnhPatternID
        {
            get;
            set;
        }

        [JsonProperty("EnhRng")]
        public string EnhRng
        {
            get;
            set;
        }

        [JsonProperty("EnhRty")]
        public string EnhRty
        {
            get;
            set;
        }

        [JsonProperty("ItemID")]
        public string ItemID
        {
            get;
            set;
        }


        [JsonProperty("bBank")]
        public string Bank
        {
            get;
            set;
        }
        
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bEquip")]
        public bool Equipped
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bStaff")]
        public bool Staff
        {
            get;
            set;
        }

        [JsonProperty("iDPS")]
        public string DPS
        {
            get;
            set;
        }

        [JsonProperty("EnhDPS")]
        public int EnhDPS
        {
            get;
            set;
        }

        [JsonProperty("iEnh")]
        public int iEnh
        {
            get;
            set;
        }

        [JsonProperty("iRng")]
        public string Rng
        {
            get;
            set;
        }

        [JsonProperty("iRty")]
        public int Rty
        {
            get;
            set;
        }

        [JsonProperty("sES")]
        public string ES
        {
            get;
            set;
        }

        [JsonProperty("sIcon")]
        public string Icon
        {
            get;
            set;
        }

        [JsonProperty("sElmt")]
        public string Element
        {
            get;
            set;
        }

        [JsonProperty("sMeta")]
        public string Meta
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

        public int CharItemId
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bEquip")]
        public bool IsEquipped
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

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bTemp")]
        public bool IsTemporary
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

        [JsonProperty("ItemID")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("ShopItemID")]
        public int ShopItemId
        {
            get;
            set;
        }

        [JsonProperty("iRate")]
        public string DropChance
        {
            get;
            set;
        }

        #region bools
        public bool IsEquippable => EquippableCategories.Contains(Category);

        public bool IsWeapon => Weapons.Contains(Category);

        public bool IsEquippableNonItem => EquippableNonWeapon.Contains(Category);

        public bool ShouldSerializeDescription => false;

        public bool ShouldSerializeMaxStack => false;

        public bool ShouldSerializeLevel => false;

        public bool ShouldSerializeIsAcItem => false;

        public bool ShouldSerializeLink => false;

        public bool ShouldSerializeFile => false;

        public bool ShouldSerializeIsEquipped => false;

        public bool ShouldSerializeIsMemberOnly => false;

        public bool ShouldSerializeIsTemporary() => false;

        public bool ShouldSerializeCost() => false;

        public bool ShouldSerializeCategory() => false;

        public bool ShouldSerializeShopItemId() => false;

        public bool ShouldSerializeDropChance() => false;
        #endregion

        /// <summary>
        /// The ID of the item.
        /// </summary>
        [JsonProperty("ItemID")]
        public virtual int ID { get; set; }
        /// <summary>
        /// The name of the item.
        /// </summary>
        [JsonProperty("sName")]
        [JsonConverter(typeof(TrimConverter))]
        public virtual string Name { get; set; }
        /// <summary>
        /// The description of the item.
        /// </summary>
        [JsonProperty("sDesc")]
        public virtual string Description { get; set; }
        /// <summary>
        /// The quantity of the item in this stack.
        /// </summary>
        [JsonProperty("iQty")]
        public virtual int Quantity { get; set; }
        /// <summary>
        /// The maximum stack size this item can exit in.
        /// </summary>
        [JsonProperty("iStk")]
        public virtual int MaxStack { get; set; }
        /// <summary>
        /// Indicates if the item is a member/upgrade only item.
        /// </summary>
        [JsonProperty("bUpg")]
        [JsonConverter(typeof(StringBoolConverter))]
        public virtual bool Upgrade { get; set; }
        /// <summary>
        /// Indicates if the item is an AC item.
        /// </summary>
        [JsonProperty("bCoins")]
        [JsonConverter(typeof(StringBoolConverter))]
        public virtual bool Coins { get; set; }
        /// <summary>
        /// The category of the item.
        /// </summary>
        [JsonProperty("sType")]
        public virtual string Category { get; set; }
        /// <summary>
        /// Indicates if the item is a temporary item.
        /// </summary>
        [JsonProperty("bTemp")]
        [JsonConverter(typeof(StringBoolConverter))]
        public virtual bool Temp { get; set; }
        /// <summary>
        /// Gets the item's sFile
        /// </summary>
        [JsonProperty("sFile")]
        public virtual string File { get; set; }
        /// <summary>
        /// Gets the item's sFile
        /// </summary>
        [JsonProperty("sLink")]
        public virtual string Link { get; set; }
    }
}