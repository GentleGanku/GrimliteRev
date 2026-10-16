using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
    public class HandlerInfo : IJsonMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[4]
        {
            "ct",
            "cb",
            "moveToArea",
            "uotls"
        };

        #endregion

        #region Methods

        public async void Handle(JsonMessage message)
        {
            await ParseInfo(message, message.Command);
        }

        public async Task ParseInfo(JsonMessage message, string command)
        {
            switch (command)
            {
                case "ct":
                case "cb":
                    break;
                case "moveToArea":
                    break;
                case "uotls":
                    break;
            }
        }

        #endregion
        }
}
