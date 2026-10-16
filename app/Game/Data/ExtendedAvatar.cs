using Grimoire.Game.Data;
using Grimoire.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grimoire.Game
{
    public class ExtendedAvatar
    {
        /// <summary>
        /// The player's universal User ID.
        /// </summary>
        [JsonProperty("UserID")]
        public int UserID { get; set; }

        /// <summary>
        /// The player's character ID.
        /// </summary>
        [JsonProperty("CharID")]
        public int CharID { get; set; }

        /// <summary>
        /// The amount of Gold the player has.
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// The amount of AdventureCoin the player has.
        /// </summary>
        [JsonProperty("intCoins")]
        public int AdventureCoin { get; set; }

        /// <summary>
        /// The state of the player's reputation booster.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("iBoostRep")]
        public bool BoostRep { get; set; }

        /// <summary>
        /// The state of the player's gold booster.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("iBoostG")]
        public bool BoostGold { get; set; }

        /// <summary>
        /// The state of the player's experience booster.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("iBoostXP")]
        public bool BoostXP { get; set; }

        /// <summary>
        /// The state of the player's class points booster.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("iBoostCP")]
        public bool BoostCP { get; set; }

        /// <summary>
        /// The list of factions the player has.
        /// </summary>
        public List<Faction> Factions { get; set; }

        /// <summary>
        /// The player's whole inventory.
        /// </summary>
        public List<InventoryItem> Inventory { get; set; }

        /// <summary>
        /// The player's whole temporary inventory.
        /// </summary>
        public List<InventoryItem> TemporaryInventory { get; set; }

        /// <summary>
        /// The player's whole house inventory.
        /// </summary>
        public List<InventoryItem> HouseInventory { get; set; }

        /// <summary>
        /// The player's whole bank.
        /// </summary>
        public List<InventoryItem> Bank { get; set; }

        /// <summary>
        /// The player's total inventory slots.
        /// </summary>
        [JsonProperty("iBagSlots")]
        public int InventorySlots { get; set; }

        /// <summary>
        /// The player's used inventory slots.
        /// </summary>
        public int UsedInventorySlots { get; set; }

        /// <summary>
        /// The player's total bank slots.
        /// </summary>
        [JsonProperty("iBankSlots")]
        public int BankSlots { get; set; }

        /// <summary>
        /// The player's used bank slots.
        /// </summary>
        [JsonProperty("bankCount")]
        public int UsedBankSlots { get; set; }

        /// <summary>
        /// The player's total house slots.
        /// </summary>
        [JsonProperty("iHouseSlots")]
        public int HouseSlots { get; set; }

        /// <summary>
        /// The player's total house slots.
        /// </summary>
        public int UsedHouseSlots { get; set; }

        /// <summary>
        /// The player's activation flag.
        /// </summary>
        [JsonProperty("intActivationFlag")]
        public int ActivationFlag { get; set; }

    }
}