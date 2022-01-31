using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;
using System;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
    public class HandlerChat : IXtMessageHandler
    {

        public string[] HandledCommands
        {
            get;
        } = new string[5]
        {
            "chatm",
            "warning",
            "whisper",
            "message",
            "server"
        };

        public void Handle(XtMessage message)
        {
            string type = message.Arguments[2];
            string tolog = message.Arguments[4];
            message.Arguments[5] = (message.Arguments[5] == Player.Username && OptionsManager.ChangeChat) ? "You" : message.Arguments[5];
            switch (type)
            {
                case "chatm":
                    tolog = (message.Arguments[5] + message.Arguments[4]).Replace("zone~", ": ");
                    break;
                case "whisper":
                    tolog = message.Arguments[6] == Player.Username ? "From " + message.Arguments[5] : "To " + message.Arguments[6];
                    tolog = $"{tolog}: {message.Arguments[4]}";
                    break;
                case "warning":
                    break;
                case "server":
                    break;
            }
            LogForm.Instance.AppendChat(string.Format("[{0:hh:mm:ss}] {1}\r\n", DateTime.Now, tolog));
        }

    }
}
