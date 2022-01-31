using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers
{
    public class HandlerAnimations : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "ct"
        };

        public void Handle(JsonMessage message)
        {
            if (message.DataObject["anims"] != null)
            {
                message.DataObject["anims"] = new JArray();
            }
            if (message.DataObject["a"] != null)
            {
                message.DataObject["a"] = new JArray();
            }
        }
    }
}