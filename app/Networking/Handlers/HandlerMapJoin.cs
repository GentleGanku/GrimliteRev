using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Tools;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
    public class HandlerMapJoin : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "moveToArea"
        };

        public void Handle(JsonMessage message)
        {
            OptionsManager.RoomNumber = World.RoomNumber;
            if (OptionsManager.HideRoom)
            {
                HideRoom();
            }
        }

        public async void HideRoom()
        {
            await Task.Delay(10);
            Flash.Call("ChangeAreaName", BotClientConfig.Instance.GetValue<string>("areaName") ?? "discord.io/aqwbots");
        }
    }
}