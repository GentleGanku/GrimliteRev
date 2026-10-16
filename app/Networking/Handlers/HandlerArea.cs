using Grimoire.Game.Data;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;
using Grimoire.Utils;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace Grimoire.Networking.Handlers
{
    public class HandlerArea : IJsonMessageHandler
    {
        #region Fields

        private static CancellationTokenSource _cts = new CancellationTokenSource();

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
                // Only the newest map matters: a newer moveToArea cancels this wait, and a wait never outlives a minute.
                var cts = new CancellationTokenSource();
                Interlocked.Exchange(ref _cts, cts).Cancel();
                await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.ActiveCutscene == 0 && !World.mapLoadInProgress && Player.IsLoggedIn, () => !cts.IsCancellationRequested, 120, 500, 500);
                if (cts.IsCancellationRequested)
                {
                    return;
                }
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