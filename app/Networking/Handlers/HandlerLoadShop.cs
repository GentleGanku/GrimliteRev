using Grimoire.Game;
using Grimoire.Game.Data;
using Newtonsoft.Json.Linq;

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
            JToken jToken = message.DataObject["shopinfo"];
            if (jToken != null)
            {
                World.OnShopLoaded(jToken.ToObject<ShopInfo>());
            }
        }

        #endregion
    }

}