using Newtonsoft.Json;
using System;

namespace Grimoire.Tools
{
    public class BoolConverter : JsonConverter
    {
        #region Methods

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? 1 : 0);
        }

        /// <summary>
        /// Reads a game flag. The game sends flags as 1/0, "1"/"0" or true/false depending on the packet (dropItem sends
        /// bCoins as a JSON boolean), so all three count. A missing value is false.
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Boolean:
                    return (bool)reader.Value;
                case JsonToken.Integer:
                    return Convert.ToInt64(reader.Value) != 0;
                case JsonToken.String:
                    string text = ((string)reader.Value).Trim();
                    return text == "1" || text.Equals("true", StringComparison.OrdinalIgnoreCase);
                default:
                    return false;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }

        #endregion
    }
}