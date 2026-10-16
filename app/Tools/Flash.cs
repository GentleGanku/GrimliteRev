using AxShockwaveFlashObjects;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.UI;
using Grimoire.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace Grimoire.Tools
{
    public delegate void FlashCallHandler(AxShockwaveFlash flash, string function, params object[] args);

    public delegate void FlashErrorHandler(AxShockwaveFlash flash, Exception e, string function, params object[] args);

    public class Flash
    {
        private static Flash _instance;
        public static Flash Instance
        {
            get
            {
                return _instance ?? (_instance = new Flash());
            }
        }

        public static AxShockwaveFlash flash;

        public static event FlashCallHandler FlashCall;

        public static event FlashErrorHandler FlashError;

        public static event Action<int> SwfLoadProgress;

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

        public string GetGameObject(string path)
        {
            return Call<string>("getGameObject", path);
        }

        public string GetGameObjectStatic(string path)
        {
            return Call<string>("getGameObjectS", path);
        }

        public T GetGameObject<T>(string path, T def = default)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(GetGameObject(path));
            }
            catch
            {
                return def;
            }
        }

        public static string CallGameFunction(string path, params object[] args)
        {
            return args.Length > 0 ? Call("callGameFunction", new object[] { path }.Concat(args).ToArray()) : Call<string>("callGameFunction0", path);
        }

        public void SetGameObject(string path, object value)
        {
            Call("setGameObject", path, value);
        }

        public static string Call(string function, params object[] args)
        {
            return Call<string>(function, args);
        }

        public static T Call<T>(string function, params object[] args)
        {
            try
            {
                return (T)Call(function, typeof(T), args);
            }
            catch
            {
                return default;
            }
        }

        public static object Call(string function, Type type, params object[] args)
        {
            try
            {
                StringBuilder req = new StringBuilder().Append($"<invoke name=\"{function}\" returntype=\"xml\">");
                if (args.Length > 0)
                {
                    req.Append("<arguments>");
                    args.ForEach(o => req.Append(ToFlashXml(o)));
                    req.Append("</arguments>");
                }
                req.Append("</invoke>");
                string result = flash.CallFunction(req.ToString());
                XElement el = XElement.Parse(result);
                return el == null || el.FirstNode == null ? default : Convert.ChangeType(el.FirstNode.ToString(), type);
            }
            catch (Exception e)
            {
                FlashError?.Invoke(flash, e, function, args);
                return default;
            }
        }

        public static T Call<T>(string function, params string[] args)
        {
            return TryDeserialize<T>(GetResponse(BuildRequest(function, args)));
        }

        public static string CallString(string function, params object[] args)
        {
            return Call<string>(function, args);
        }

        public static void Call(string function, params string[] args)
        {
            Call<string>(function, args);
        }

        private static string BuildRequest(string method, params string[] args)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<invoke name=\"" + method + "\" returntype=\"xml\">");
            if (args != null && args.Length != 0)
            {
                stringBuilder.Append("<arguments>");
                foreach (string str in args)
                {
                    stringBuilder.Append("<string>" + str + "</string>");
                }
                stringBuilder.Append("</arguments>");
            }
            stringBuilder.Append("</invoke>");
            return stringBuilder.ToString();
        }

        private static string GetResponse(string request)
        {
            try
            {
                return HttpUtility.HtmlDecode(XElement.Parse(Root.Instance.Client.CallFunction(request)).FirstNode?.ToString() ?? string.Empty);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static T TryDeserialize<T>(string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch
            {
                return default;
            }
        }

        public static string ToFlashXml(object o)
        {
            switch (o)
            {
                case null:
                    return "<null/>";

                case bool _:
                    return $"<{o.ToString().ToLower()}/>";

                case double _:
                case float _:
                case long _:
                case int _:
                    return $"<number>{o}</number>";

                case ExpandoObject _:
                    StringBuilder sb = new StringBuilder().Append("<object>");
                    foreach (KeyValuePair<string, object> kvp in o as IDictionary<string, object>)
                        sb.Append($"<property id=\"{kvp.Key}\">{ToFlashXml(kvp.Value)}</property>");
                    return sb.Append("</object>").ToString();

                default:
                    if (o is Array)
                    {
                        StringBuilder _sb = new StringBuilder().Append("<array>");
                        int k = 0;
                        foreach (object el in o as Array)
                            _sb.Append($"<property id=\"{k++}\">{ToFlashXml(el)}</property>");
                        return _sb.Append("</array>").ToString();
                    }
                    return $"<string>{SecurityElement.Escape(o.ToString())}</string>";
            }
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

        public static object FromFlashXml(XElement el)
        {
            switch (el.Name.ToString())
            {
                case "number":
                    return int.TryParse(el.Value, out int i) ? i : float.TryParse(el.Value, out float f) ? f : 0;
                case "true":
                    return true;
                case "false":
                    return false;
                case "null":
                    return null;
                case "array":
                    return el.Elements().Select(e => FromFlashXml(e)).ToArray();
                case "object":
                    dynamic d = new ExpandoObject();
                    el.Elements().ForEach(e => d[e.Attribute("id").Value] = FromFlashXml(e.Elements().First()));
                    return d;
                default:
                    return el.Value;
            }
        }

        public static void CallHandler(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
        {
            XElement el = XElement.Parse(e.request);
            string name = el.Attribute("name").Value;
            object[] args = el.Elements().Select(x => FromFlashXml(x)).ToArray();
            FlashCall?.Invoke(flash, name, args);
        }
    }
}