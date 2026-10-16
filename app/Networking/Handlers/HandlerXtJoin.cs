using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Tools;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers
{
    public class HandlerXtJoin : IXtMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "server"
        };

        #endregion

        #region Methods

        public void Handle(XtMessage message)
        {
            if (!message.RawContent.Contains("You joined "))
                return;
            if (OptionsManager.ChangeChat)
                Flash.Call("ChangeName", "You");
            if (BotManager.Instance.CustomName != null)
                BotManager.Instance.CustomName = BotManager.Instance.CustomName;
            if (BotManager.Instance.CustomGuild != null)
                BotManager.Instance.CustomGuild = BotManager.Instance.CustomGuild;
        }

        #endregion
    }

    public class HandlerXtCellJoin : IXtMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "moveToCell"
        };

        #endregion

        #region Methods

        public void Handle(XtMessage message)
        {
            World.SetSpawnPoint();
            if (OptionsManager.HideYulgar && Player.Map.ToLower() == "yulgar")
                OptionsManager.DestroyPlayers(OptionsManager.HideYulgar);
            if (OptionsManager.HidePlayers)
                OptionsManager.DestroyPlayers(true);
        }

        #endregion
    }
}