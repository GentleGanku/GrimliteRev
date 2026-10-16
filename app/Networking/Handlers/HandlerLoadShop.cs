using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Utils;
using Newtonsoft.Json.Linq;
using System;

namespace Grimoire.Networking.Handlers
{
    public class HandlerLoadShop : IJsonMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "loadShop"
        };

        #endregion

        #region Methods

        public async void Handle(JsonMessage message)
        {
            try
            {
                JToken jToken = message.DataObject["shopinfo"];
                if (jToken != null)
                {
                    World.OnShopLoaded(jToken.ToObject<ShopInfo>());
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running HandlerLoadShop.Handle", ex);
            }
        }

        #endregion
    }

}