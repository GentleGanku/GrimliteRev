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

        public async void Handle(JsonMessage message)
        {
            OptionsManager.RoomNumber = World.RoomNumber;
            if (OptionsManager.HideRoom)
            {
                await Task.Delay(10);
                BotClientConfig c = BotClientConfig.Load(System.Windows.Forms.Application.StartupPath + "\\BotClientConfig.cfg");
                Flash.Call("ChangeAreaName", c.Get("areaName") ?? "discord.io/aqwbots");
            }
        }
    }
}