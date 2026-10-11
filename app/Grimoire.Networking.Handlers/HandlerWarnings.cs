using Grimoire.UI;
using System;

namespace Grimoire.Networking.Handlers
{
    public class HandlerWarningsXt : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "logoutWarning"
        };

        public void Handle(XtMessage message)
        {
            LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Logged out from the game due to a warning trigger.\r\n");
        }
    }
}