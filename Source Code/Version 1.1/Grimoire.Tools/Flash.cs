using AxShockwaveFlashObjects;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.UI;
using Grimoire.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Grimoire.Tools
{
    public delegate void FlashCallHandler(AxShockwaveFlash flash, string function, params object[] args);

    public delegate void FlashErrorHandler(AxShockwaveFlash flash, Exception e, string function, params object[] args);

    public class Flash
    {
        private static Flash _instance;
        public static Flash Instance => _instance ?? (_instance = new Flash());

        public static AxShockwaveFlash flash;

        public static event FlashCallHandler FlashCall;

        public static event FlashErrorHandler FlashError;

        public static event Action<int> SwfLoadProgress;

        public static void ProcessFlashCall(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
        {
            XElement xElement = XElement.Parse(e.request);
            string text = xElement.Attribute("name")?.Value;
            string text2 = xElement.Element("arguments")?.Value;
            if (text == null)
            {
                return;
            }
            if (!(text == "progress"))
            {
                if (text == "modifyServers")
                {
                    Root.Instance.Client.SetReturnValue("<string>" + ModifyServerList(text2.Trim()) + "</string>");
                }
            }
            else
            {
                SwfLoadProgress?.Invoke(int.Parse(text2));
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

        public string CallGameFunction(string path, params object[] args)
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

        private static string ModifyServerList(string xml)
        {
            if (!xml.StartsWith("<login") || !xml.EndsWith("</login>"))
            {
                return xml;
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            XmlElement xmlElement = xmlDocument["login"];
            Server[] array = new Server[xmlElement.ChildNodes.Count];
            for (int i = 0; i < xmlElement.ChildNodes.Count; i++)
            {
                XmlElement xmlElement2 = (XmlElement)xmlElement.ChildNodes[i];
                XmlAttribute xmlAttribute = xmlElement2.Attributes["sIP"];
                if (xmlAttribute == null)
                {
                    return xml;
                }
                XmlAttribute xmlAttribute2 = xmlElement2.Attributes["iPort"];
                xmlElement2.Attributes.Append(xmlDocument.CreateAttribute("RealAddress")).Value = xmlAttribute.Value;
                xmlElement2.Attributes.Append(xmlDocument.CreateAttribute("RealPort")).Value = xmlAttribute2.Value;
                xmlElement2.Attributes["iPort"].Value = Proxy.Instance.ListenerPort.ToString();
                xmlAttribute.Value = "127.0.0.1";
                array[i] = new Server
                {
                    IsChatRestricted = xmlElement2.Attributes["iChat"].Value == "0",
                    PlayerCount = int.Parse(xmlElement2.Attributes["iCount"].Value),
                    IsMemberOnly = xmlElement2.Attributes["bUpg"].Value == "1",
                    IsOnline = xmlElement2.Attributes["bOnline"].Value == "1",
                    Name = xmlElement2.Attributes["sName"].Value,
                    Port = int.Parse(xmlElement2.Attributes["iPort"].Value)
                };
            }
            BotManager.Instance.OnServersLoaded(array);
            return xmlDocument.OuterXml;
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