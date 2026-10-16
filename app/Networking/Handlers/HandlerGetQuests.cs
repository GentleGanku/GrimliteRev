using Grimoire.Game;
using Grimoire.Game.Data;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Networking.Handlers
{
    public class HandlerGetQuests : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "getQuests"
        };

        public void Handle(JsonMessage message)
        {
            Dictionary<int, Quest> quests = message.DataObject?["quests"]?.ToObject<Dictionary<int, Quest>>();
            if (quests != null && quests.Count > 0)
            {
                Player.Quests.OnQuestsLoaded(quests.Select((KeyValuePair<int, Quest> q) => q.Value).ToList());
                Player.Quests.LoadedQuests.AddRange(quests.Select((KeyValuePair<int, Quest> q) => q.Value).ToList());
            }
        }
    }
}