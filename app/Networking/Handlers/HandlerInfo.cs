using Grimoire.Utils;
using System;
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
            try
            {
                await ParseInfo(message, message.Command);
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running HandlerInfo.Handle", ex);
            }
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
