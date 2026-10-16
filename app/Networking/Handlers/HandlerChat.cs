using Grimoire.Game;
using Grimoire.UI;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
    public class HandlerChat : IXtMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[3]
        {
            "chatm",
            "whisper",
            "server"
        };

        #endregion

        #region Methods

        public static async Task ResetAFKTimer()
        {
            while (Player.IsAfk)
            {
                Player.AFKPostpone();
                await Task.Delay(60000);
            }
        }

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
            playerMsg = ReplaceOwnName(playerMsg, Player.Username?.ToLower());
            if (!string.IsNullOrEmpty(playerMsg))
            {
                LogForm.Instance.AppendChat($"[{DateTime.Now:hh:mm:ss}] {playerMsg}\r\n");
            }
        }

        /// <summary>Replaces <paramref name="username"/> with "You" where it stands as a whole name, not inside other words.</summary>
        public static string ReplaceOwnName(string message, string username)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(username))
            {
                return message;
            }
            return Regex.Replace(message, $@"(?<![\w]){Regex.Escape(username)}(?![\w])", "You");
        }

        #endregion
    }
}
