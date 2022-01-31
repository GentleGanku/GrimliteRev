using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Tools;

namespace Grimoire.Networking.Handlers
{
    public class HandlerPlayers : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[2]
        {
            "retrieveUserData",
            "retrieveUserDatas"
        };

        public void Handle(XtMessage message)
        {
            if (OptionsManager.HidePlayers && Player.Inventory.Items.Count > 0)
            {
                message.Send = message.RawContent.Contains(Player.UserID.ToString()) ? true : false;
                OptionsManager.DestroyPlayers();
            }
        }
    }
}