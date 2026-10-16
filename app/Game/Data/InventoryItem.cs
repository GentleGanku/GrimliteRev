using Grimoire.UI;
using Grimoire.Tools;
using Grimoire.Botting;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Game.Data
{
    public class InventoryItem
    {
        #region Fields

        public static readonly string[] EquippableCategories = new string[21]
        {
            "Sword",
            "Axe",
            "Dagger",
            "Gauntlet",
            "Gun",
            "HandGun",
            "Bow",
            "Mace",
            "Polearm",
            "Rifle",
            "Staff",
            "Wand",
            "Whip",
            "Class",
            "Armor",
            "Helm",
            "Cape",
            "Pet",
            "Necklace",
            "Misc",
            "Item"
        };

        public static readonly string[] Weapons = new string[13]
        {
            "Sword",
            "Axe",
            "Dagger",
            "Gauntlet",
            "Gun",
            "HandGun",
            "Bow",
            "Mace",
            "Polearm",
            "Rifle",
            "Staff",
            "Wand",
            "Whip",
        };

        public static readonly string[] EquippableNonWeapon = new string[8]
        {
            "Class",
            "Armor",
            "Helm",
            "Cape",
            "Pet",
            "Necklace",
            "Misc",
            "Item"
        };

        private string _name;

        #endregion

        #region Properties

        [JsonProperty("iQty")]
        public int Quantity
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

        [JsonProperty("sLink")]
        public string Link
        {
            get;
            set;
        }

        [JsonProperty("sFile")]
        public string File
        {
            get;
            set;
        }

        [JsonProperty("strES")]
        public string EquipmentSlot
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
        [JsonProperty("bBank")]
        public bool IsBankItem
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bHouse")]
        public bool IsHouseItem
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

        [JsonProperty("sType")]
        public string Category
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

        [JsonProperty("sName")]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = World.ItemTree.FirstOrDefault((InventoryItem i) => i.Id == Id)?.Name;
                }
                return _name;
            }
            set
            {
                _name = value?.Trim();
            }
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

        public bool IsEquippable
        {
            get
            {
                return EquippableCategories.Contains(Category);
            }
        }

        public bool IsWeapon
        {
            get
            {
                return Weapons.Contains(Category);
            }
        }

        public bool IsEquippableNonItem
        {
            get
            {
                return EquippableNonWeapon.Contains(Category);
            }
        }

        public bool ShouldSerializeDescription
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeMaxStack
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeLevel
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeIsAcItem
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeLink
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeFile
        {
            get
            {
                return false;
            }
        }

        public bool ShouldSerializeIsEquipped
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

        #endregion

        #region Methods

        public override string ToString()
        {
            return Name;
        }

        public async Task Equip()
        {
            if (IsEquipped || !IsEquippable)
            {
                return;
            }
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.IsActionAvailable(LockActions.EquipItem), null, 10, 500);
            if (Category == "Item")
            {
                if (Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Category == "Item" && i.IsEquipped) != null)
                {
                    Player.UnequipPotion(Name);
                }
                Player.EquipPotion(Id, Description, File, Name);
            }
            else
            {
                Player.Equip(Id);
            }
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => IsEquipped, null, 10, 500);
        }

        public bool ShouldSerializeIsTemporary() => false;

        public bool ShouldSerializeCost() => false;

        public bool ShouldSerializeCategory() => false;

        public bool ShouldSerializeShopItemId() => false;

        public bool ShouldSerializeDropChance() => false;

        #endregion
    }
}
