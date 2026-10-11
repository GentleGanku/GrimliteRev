using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System;

namespace Grimoire.Networking.Handlers
{
    public class HandlerQuestComplete : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "ccqr"
        };

        public void Handle(JsonMessage message)
        {
            var comp = message.DataObject.ToObject<CompletedQuest>();
            Player.Quests.OnQuestCompleted(comp);
            LogForm.Instance.AppendDebug(string.Format("Quest: {0} Completed at {1}:HH:mm:ss} \r\n", comp.ToString(), DateTime.Now));
        }
    }
}