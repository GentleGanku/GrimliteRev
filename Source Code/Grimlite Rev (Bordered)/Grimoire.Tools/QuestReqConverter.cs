using Grimoire.Game.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Tools
{
    public class QuestReqConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<ItemBase>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Dictionary<int, ItemBase>>(reader).ToList();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }
    }
}
