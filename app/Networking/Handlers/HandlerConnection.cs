using Grimoire.Game;

namespace Grimoire.Networking.Handlers
{
    public class HandlerConnection : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[2]
        {
            "loginResponse",
            "logoutWarning"
        };

        //%xt%loginResponse%-1%true%22333%Koringa%%2022-22-22T08:49:13%sNews=154,sMap=news/Map-UI_r37.swf,sBook=news/spiderbook3.swf,sAssets=Assets_20211229.swf,gMenu=dynamic-gameMenu-17Jan22.swf,sVersion=R0015%3.0065%
        //%xt%logoutWarning%-1%%30%
        //{"t":"xt","b":{"r":-1,"o":{"cmd":"balance","intGold":11122233,"intExp":122,"intCoins":665,"iUpgDays":-333}}}
        public void Handle(XtMessage message)
        {
            switch (message.Command)
            {
                case "loginResponse":
                    Player.IsLoggedIn = true;
                    break;
                case "logoutWarning":
                    Player.IsLoggedIn = false;
                    break;
            }
        }
    }
}