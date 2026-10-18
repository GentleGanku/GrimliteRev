using Grimoire.Utils;
using Newtonsoft.Json;
using System;

namespace Grimoire.Tools
{
    /// <summary>
    /// Reads item names: trims them and straightens curly apostrophes and quotes, so they match what the user enters.
    /// </summary>
    public class TrimConverter : JsonConverter
    {
        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ((string)reader.Value)?.Trim().UnifyQuotes();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}