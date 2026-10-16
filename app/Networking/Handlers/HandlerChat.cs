using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;
using System;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
    public class HandlerChat : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[3]
        {
            "chatm",
            "whisper",
            "server"
        };

        public void Handle(XtMessage message)
        {
            string playerMsg = "";
            switch (message.Arguments[2])
            {
                case "chatm":
                    playerMsg = (message.Arguments[5] + message.Arguments[4]).Replace("zone~", ": ");
                    break;
                case "whisper":
                    playerMsg = (message.Arguments[6] == Player.Username.ToLower()) ? "From " + message.Arguments[5] : "To " + message.Arguments[6];
                    playerMsg = $"{playerMsg}: {message.Arguments[4]}";
                    break;
                case "server":
                    playerMsg = message.Arguments[4];
                    break;
            }
            playerMsg = playerMsg.Replace(Player.Username.ToLower(), "You");
            if (!string.IsNullOrEmpty(playerMsg))
            {
                LogForm.Instance.AppendChat($"[{DateTime.Now:hh:mm:ss}] {playerMsg}\r\n");
            }
        }

        public static async Task ResetAFKTimer()
        {
            while (Player.IsAfk)
            {
                Player.AFKPostpone();
                await Task.Delay(60000);
            }
        }

    }
}
