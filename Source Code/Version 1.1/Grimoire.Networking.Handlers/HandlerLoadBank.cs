using Grimoire.Botting;

namespace Grimoire.Networking.Handlers
{
    public class HandlerLoadBank : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "loadBank"
        };

        public void Handle(JsonMessage message)
        {
            //BotUtilities.BankLoadEvent.Set();
        }
    }
}