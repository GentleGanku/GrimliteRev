using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace Grimoire.Networking.Handlers
{
    public class HandlerXtJoin : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "server"
        };

        public void Handle(XtMessage message)
        {
            if (!message.RawContent.Contains("You joined "))
                return;
            if (BotManager.Instance.CustomName != null)
                BotManager.Instance.CustomName = BotManager.Instance.CustomName;
            if (BotManager.Instance.CustomGuild != null)
                BotManager.Instance.CustomGuild = BotManager.Instance.CustomGuild;
            if (OptionsManager.HideRoom)
            {
                Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                message.Arguments[4] =  c.Get("JoinMessage") ?? "You joined a place but... where?";
            }
            LogForm.Instance.AppendChat(string.Format("[{0:hh:mm:ss}] {1} \r\n", DateTime.Now, message.Arguments[4]));
        }
    }

    public class HandlerXtCellJoin : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "moveToCell"
        };

        public void Handle(XtMessage message)
        {
            if (Player.Map.ToLower() == "yulgar" && Player.Cell.ToLower() == "upstairs" && OptionsManager.HideYulgar)
                OptionsManager.DestroyPlayers();
        }
    }
}