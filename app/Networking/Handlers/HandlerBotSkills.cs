using Grimoire.Game;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers
{
    public class HandlerBotSkills : IJsonMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "ct"
        };

        #endregion

        #region Methods

        public async void Handle(JsonMessage message)
        {
            if (message.RawContent.IndexOf("\"msg\":", StringComparison.OrdinalIgnoreCase) > -1 && message.RawContent.IndexOf("counter attack", StringComparison.OrdinalIgnoreCase) > -1 && message.RawContent.Contains($"\"strFrame\":\"{Player.Cell}\""))
            {
                Proxy.DisableBotSkill();
            }
            else if (message.RawContent.IndexOf("\"nam\":\"Counter Attack\"", StringComparison.OrdinalIgnoreCase) != -1)
            {
                JArray auraEvents = (JArray)message.DataObject?["a"];
                if (auraEvents != null)
                {
                    if (auraEvents.Any(a => ((a["cmd"].ToString() == "aura-") || (a["cmd"].ToString() == "aura--")) && a["aura"]["nam"].ToString().Equals("Counter Attack", StringComparison.OrdinalIgnoreCase)))
                    {
                        Proxy.EnableBotSkill();
                    }
                }
            }
        }

        #endregion
    }
}
