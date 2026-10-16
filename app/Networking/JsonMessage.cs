using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking
{
    public class JsonMessage : Message
    {
        public JToken Object
        {
            get;
        }

        public JToken DataObject
        {
            get
            {
                return Object?["b"]?["o"];
            }
        }

        public JsonMessage(string raw)
        {
            try
            {
                RawContent = raw;
                Object = JObject.Parse(raw);
                Command = DataObject?["cmd"]?.Value<string>();
            }
            catch (JsonReaderException)
            {
            }
        }

        public override string ToString()
        {
            return Object.ToString(Formatting.None);
        }
    }
}