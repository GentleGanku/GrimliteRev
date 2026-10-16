using AxShockwaveFlashObjects;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Grimoire.Tools
{
    public partial class Flash
    {
        #region Methods

        public static void ProcessFlashCall(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
        {
            XElement xElement = XElement.Parse(e.request);
            string callName = xElement.Attribute("name")?.Value;
            string callArgs = xElement.Element("arguments")?.Value;
            if (callName == null)
            {
                return;
            }
            if (!(callName == "progress"))
            {
                if (callName == "modifyServers")
                {
                    Root.Instance.Client.SetReturnValue("<string>" + ModifyServerList(callArgs.Trim()) + "</string>");
                }
                // -1 keeps the game's own backgrounds (Camouflage), 0 is the first custom background.
                if (callName == "getBackgroundIndex")
                {
                    Root.Instance.Client.SetReturnValue(Root.Instance.camouflageToolStripMenuItem.Checked ? "<number>-1</number>" : "<number>0</number>");
                }
                Proxy.Instance.LogPackets(callName, callArgs);
            }
            else
            {
                SwfLoadProgress?.Invoke(int.Parse(callArgs));
            }
        }

        public static void CallHandler(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
        {
            XElement el = XElement.Parse(e.request);
            string name = el.Attribute("name").Value;
            object[] args = el.Elements().Select(x => FromFlashXml(x)).ToArray();
            FlashCall?.Invoke(flash, name, args);
        }

        private static string ModifyServerList(string response)
        {
            if (response.StartsWith("{\"login\"") && response.EndsWith("}"))
            {
                return ServersFromJson(response);
            }
            return response;
        }

        private static string ServersFromJson(String json)
        {
            JObject packet = (JObject)JObject.Parse(json);
            JObject login = (JObject)packet["login"];
            JArray servers = (JArray)packet["servers"];
            Server[] parsedServers = new Server[servers.Count];

            login["iAge"] = 99;

            for (int i = 0; i < servers.Count; i++)
            {
                JObject server = (JObject)servers[i];
                parsedServers[i] = new Server
                {
                    IsChatRestricted = server.GetValue("iChat")?.ToString() == "0",
                    PlayerCount = int.Parse(server.GetValue("iCount")?.ToString()),
                    MaxCount = int.Parse(server.GetValue("iMax")?.ToString()),
                    IsMemberOnly = server.GetValue("bUpg")?.ToString() == "1",
                    IsOnline = server.GetValue("bOnline")?.ToString() == "1",
                    Name = server.GetValue("sName")?.ToString(),
                    Language = server.GetValue("sLang")?.ToString(),
                    Port = int.Parse(server.GetValue("iPort")?.ToString()),
                    Ip = server.GetValue("sIP")?.ToString()
                };
            }
            BotManager.Instance.OnServersLoaded(parsedServers);
            ParseAvatarDatas(login);
            return packet.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ParseAvatarDatas(JObject loginResponse)
        {
            Player.Username = loginResponse["unm"].ToString();
            Player.CharUserID = int.Parse(loginResponse["userid"].ToString());
        }

        #endregion
    }
}
