using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking
{
    public class JsonMessage : Message
    {
        #region Constructors

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

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        public override string ToString()
        {
            return Object.ToString(Formatting.None);
        }

        #endregion
    }
}