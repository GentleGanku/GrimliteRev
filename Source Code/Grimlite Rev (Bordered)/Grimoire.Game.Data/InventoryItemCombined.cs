using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class InventoryItemCombined
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