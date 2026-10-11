using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System;
using Newtonsoft.Json.Linq;

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
            if (message.DataObject["bSuccess"].Value<int>() == 1)
            {
                if (Bot.Instance.questFailureCounter.IsRunning)
                {
                    Bot.Instance.questFailureCounter.Stop();
                }
                var comp = message.DataObject.ToObject<CompletedQuest>();
                Player.Quests.OnQuestCompleted(comp);
                LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Quest completed: \"{message.DataObject["sName"].Value<string>()}\" - ID {message.DataObject["QuestID"].Value<int>()}.\r\n");
            }
            else if (BotManager.Instance.chkReloginOnQuestFailure.Checked && BotManager.Instance.ActiveBotEngine.IsRunning)
            {
                if (!Bot.Instance.questFailureCounter.IsRunning)
                {
                    Bot.Instance.questFailureCounter.Start();
                }
                if (Bot.Instance.questFailureCounter.ElapsedMilliseconds > 15000)
                {
                    Bot.Instance.questFailureCounter.Stop();
                    return;
                }
                Bot.Instance.questCompletionFailure++;
                Bot.Instance.ReloginOnQuestFailure();
            }
        }

    }
}