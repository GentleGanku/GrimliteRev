using Grimoire.Game;
using Grimoire.UI;
using Grimoire.Utils;
using System;

namespace Grimoire.Networking.Handlers
{
    public class HandlerAFK : IXtMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "afk"
        };

        #endregion

        #region Methods

        public async void Handle(XtMessage message)
        {
            try
            {
                switch (message.Arguments[5])
                {
                    case "true":
                        if (BotManager.Instance.ActiveBotEngine.IsRunning && BotManager.Instance.ActiveBotEngine.Configuration.AutoRelogin && BotManager.Instance.ActiveBotEngine.Configuration.AFK)
                        {
                            Player.Logout();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running HandlerAFK.Handle", ex);
            }
        }

        #endregion
    }
}