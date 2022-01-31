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
            LogForm.Instance.AppendDebug(string.Format("[{0:hh:mm:ss}] {1} \r\n", DateTime.Now, message.Arguments[5]));
        }
    }
}