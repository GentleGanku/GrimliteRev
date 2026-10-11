using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers
{
    public class HandlerAFK : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "afk"
        };

        public void Handle(XtMessage message)
        {
            if (message.Arguments[5] == "true" && (BotManager.Instance.ActiveBotEngine.IsRunning))
                Player.Logout();
        }
    }
}