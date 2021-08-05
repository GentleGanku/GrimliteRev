using System;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers
{
    public class HandlerLogin : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "loginResponse"
        };

        public void Handle(XtMessage message)
        {
            //foreach (string n in Configuration.BlockedPlayers)
            //{
            //    if (Player.Username.ToLower() == n)
            //    {
            //        Environment.Exit(0);
            //    }
            //}
            LogForm.Instance.AppendDebug($"Relogin to server: {Configuration.Instance.Server.Name} at {DateTime.Now:hh:mm:ss tt} \r\n");
        }
    }
}