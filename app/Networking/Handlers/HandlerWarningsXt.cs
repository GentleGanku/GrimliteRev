using Grimoire.UI;
using System;

namespace Grimoire.Networking.Handlers
{
    public class HandlerWarningsXt : IXtMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "logoutWarning"
        };

        #endregion

        #region Methods

        public void Handle(XtMessage message)
        {
            LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Logged out from the game due to a warning trigger.\r\n");
        }

        #endregion
    }
}