using Grimoire.UI;
using System.Threading.Tasks;
using Grimoire;
using Grimoire.Botting;
using Newtonsoft.Json.Linq;
using System.Windows;
using Grimoire.Game;

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
            if (OptionsManager.HideRoom)
                message.DataObject["areaName"] = "discord.io/aqwbots";
            //JToken jToken = message.DataObject["uoBranch"];
            //if (OptionsManager.SetLevelOnJoin != null && jToken.Type != JTokenType.Null)
            //{
            //    int i = 0;
            //    foreach(JToken j in jToken)
            //    {
            //        MessageBox.Show(j.ToString());
            //        //if (j["uoName"].ToString() == Player.Username.ToLower())
            //        //    j["intLevel"] = OptionsManager.SetLevelOnJoin;
            //    }
            //}
            //MessageBox.Show(BotManager.Instance.CustomName + " 1 \r\n" + message.RawContent.Split(':')[6].Split('-')[0].Replace("\"", "").ToLower());
        }
    }
}