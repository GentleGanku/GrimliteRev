using Grimoire.Game;
using Grimoire.Botting;
using Grimoire.UI;
using System.Threading.Tasks;

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

        public async void Handle(XtMessage message)
        {
            switch (message.Arguments[5])
            {
                case "true":
                    if (BotManager.Instance.ActiveBotEngine.IsRunning && BotManager.Instance.ActiveBotEngine.Configuration.AutoRelogin && BotManager.Instance.ActiveBotEngine.Configuration.AFK)
                    {
                        Player.Logout();
                    }
                    //if (OptionsManager._antiAfk)
                    //{
                    //    await Task.Delay(750);
                    //    Proxy.Instance.SendToServer("%xt%zm%afk%1%false%");
                    //}
                    break;
            }
        }
    }
}