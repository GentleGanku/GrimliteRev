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
            LogForm.Instance.AppendDebug(string.Format("[{1}:HH:mm:ss] Quest \"{0}\" has been completed.\r\n", comp.ToString(), DateTime.Now));
        }
    }
}