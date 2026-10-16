using Grimoire.UI;
using Grimoire.Botting;
using Grimoire.Utils;
using Newtonsoft.Json.Linq;
using System;

namespace Grimoire.Networking.Handlers
{
    public class HandlerBuyItem : IJsonMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "buyItem"
        };

        #endregion

        #region Methods

        public async void Handle(JsonMessage message)
        {
            try
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
            catch (Exception ex)
            {
                ErrorReporter.Log("running HandlerBuyItem.Handle", ex);
            }
        }

        #endregion
    }
}
