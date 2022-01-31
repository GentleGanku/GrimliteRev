using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Tools;
using Grimoire.UI;

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
            if (OptionsManager.ChangeChat)
                Flash.Call("ChangeName", "You");
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
                OptionsManager.DestroyPlayers(OptionsManager.HideYulgar);
        }
    }
}