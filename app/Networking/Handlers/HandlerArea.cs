using Grimoire.Game.Data;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;
using Grimoire.Utils;
using Newtonsoft.Json;
using System;

namespace Grimoire.Networking.Handlers
{
    public class HandlerArea : IJsonMessageHandler
    {
        #region Fields

        private static System.Threading.CancellationTokenSource _cts = new System.Threading.CancellationTokenSource();

        #endregion

        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "moveToArea"
        };

        #endregion

        #region Methods

        public async void Handle(JsonMessage message)
        {
            try
            {
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.ActiveCutscene == 0 && !World.mapLoadInProgress && Player.IsLoggedIn, () => !_cts.IsCancellationRequested, -1, 500, 500);
                Map map = JsonConvert.DeserializeObject<Map>(message.DataObject.ToString());
                World.CurrentMap = map;
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running HandlerArea.Handle", ex);
            }
        }

        #endregion
    }
}