using Grimoire.Game.Data;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;
using Newtonsoft.Json;

namespace Grimoire.Networking.Handlers
{
    public class HandlerArea : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "moveToArea"
        };

        private static System.Threading.CancellationTokenSource _cts = new System.Threading.CancellationTokenSource();

        public async void Handle(JsonMessage message)
        {
            await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.ActiveCutscene == 0 && !World.mapLoadInProgress && Player.IsLoggedIn, () => !_cts.IsCancellationRequested, -1, 500, 500);
            Map map = JsonConvert.DeserializeObject<Map>(message.DataObject.ToString());
            World.CurrentMap = map;
        }
    }
}