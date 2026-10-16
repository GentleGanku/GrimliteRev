using Grimoire.UI;
using Grimoire.Game;
using Grimoire.Botting;
using Grimoire.Game.Data;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers
{
    public class HandlerLoadShop : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "loadShop"
        };

        public async void Handle(JsonMessage message)
        {
            JToken jToken = message.DataObject["shopinfo"];
            if (jToken != null)
            {
                World.OnShopLoaded(jToken.ToObject<ShopInfo>());
            }
        }
    }

    public class HandlerBuyItem : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "buyItem"
        };

        public async void Handle(JsonMessage message)
        {
            if (message.DataObject["bitSuccess"].Value<int>() == 1)
            {
                if (Bot.Instance.buyFailureCounter.IsRunning)
                {
                    Bot.Instance.buyFailureCounter.Stop();
                }
            }
            else if (BotManager.Instance.chkRelogUponFailBuy.Checked && BotManager.Instance.ActiveBotEngine.IsRunning)
            {
                if (!Bot.Instance.buyFailureCounter.IsRunning)
                {
                    Bot.Instance.buyFailureCounter.Start();
                }
                if (Bot.Instance.buyFailureCounter.ElapsedMilliseconds > 15000)
                {
                    Bot.Instance.buyFailureCounter.Stop();
                    return;
                }
                Bot.Instance.itemBuyFailure++;
                Bot.Instance.ReloginOnItemBuyFailure();
            }
        }
    }

}