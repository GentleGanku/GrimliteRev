using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Grimoire.Botting
{
    public class CosmeticEquipment
    {
        private static Dictionary<EquipType, string> _cosMap = new Dictionary<EquipType, string>()
        {
            { EquipType.Helm, "he" },
            { EquipType.Cape, "ba" },
            { EquipType.Armor, "co" },
            { EquipType.Class, "ar" },
            { EquipType.Pet, "pe" },
            { EquipType.Weapon, "Weapon" }
        };

        private static Dictionary<string, EquipType> _backMap = _cosMap.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public EquipType Slot { get; set; }

        [JsonProperty("ItemID")]
        public int ID { get; set; }

        [JsonProperty("sLink")]
        public string Link { get; set; }

        [JsonProperty("sMeta")]
        public string Meta { get; set; }

        [JsonProperty("sFile")]
        public string SWFFile { get; set; }

        [JsonProperty("sType")]
        public string Type { get; set; }

        public void Equip()
        {
            string slot = _cosMap[Slot];
            dynamic equip = new ExpandoObject();
            equip.sFile = SWFFile;
            equip.sLink = Link;
            equip.sType = Type;
            equip.sMeta = Meta;
            if (ID != 0)
                equip.ItemID = ID;
            Tools.Flash.Call("SetEquip", new object[2] { slot , equip });
            //Tools.Flash.Instance.CallGameFunction("world.myAvatar.loadMovieAtES", slot, SWFFile, Link);
        }

        public override string ToString()
        {
            return $"{Slot}: {SWFFile};{Link}";
        }
        
        public static List<CosmeticEquipment> Get(int id)
        {
            Dictionary<string, CosmeticEquipment> items = JsonConvert.DeserializeObject<Dictionary<string, CosmeticEquipment>>(Tools.Flash.Call("GetEquip", id)) ?? new Dictionary<string, CosmeticEquipment>();
            return items.Select(kvp => (kvp.Value.Slot = _backMap.TryGetValue(kvp.Key, out EquipType slot) ? slot : EquipType.None) != EquipType.None ? kvp.Value : null).Where(x => x != null).ToList();
        }
    }
}