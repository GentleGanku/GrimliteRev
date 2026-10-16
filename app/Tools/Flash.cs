using AxShockwaveFlashObjects;
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
using System.Xml.Linq;

namespace Grimoire.Tools
{

    public partial class Flash
    {
        #region Fields

        public static AxShockwaveFlash flash;
        private static Flash _instance;

        #endregion

        #region Events

        public static event FlashCallHandler FlashCall;
        public static event FlashErrorHandler FlashError;
        public static event Action<int> SwfLoadProgress;

        #endregion

        #region Properties

        public static Flash Instance
        {
            get
            {
                return _instance ?? (_instance = new Flash());
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calls a function on the live game by dotted path. With arguments it uses the typed family
        /// (<see cref="Call(string, Type, object[])"/>); without, the string family, so the result is JSON-decoded.
        /// </summary>
        public static string CallGameFunction(string path, params object[] args)
        {
            return args.Length > 0 ? Call("callGameFunction", new object[] { path }.Concat(args).ToArray()) : Call<string>("callGameFunction0", path);
        }

        /// <summary>Typed family, see <see cref="Call(string, Type, object[])"/>.</summary>
        public static string Call(string function, params object[] args)
        {
            return Call<string>(function, args);
        }

        /// <summary>Typed family, see <see cref="Call(string, Type, object[])"/>.</summary>
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

        /// <summary>
        /// Typed family. Each argument is encoded by <see cref="ToFlashXml"/>, so numbers, booleans, arrays and objects
        /// keep their types and strings are XML-escaped. The call goes through <see cref="flash"/>, and the first node of
        /// the reply is converted with <see cref="Convert.ChangeType(object, Type)"/> as is (XML entities are not decoded).
        /// On failure it raises <see cref="FlashError"/> and returns the default value.
        /// </summary>
        /// <remarks>
        /// C# picks the string family (<see cref="Call{T}(string, string[])"/>) instead whenever every argument is a
        /// string, including <c>new string[0]</c>. Pass <c>new object[0]</c> or a non-string argument to stay here.
        /// </remarks>
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

        /// <summary>
        /// String family. Each argument is sent inside <c>&lt;string&gt;</c> without escaping, so callers escape XML
        /// themselves. The call goes through <c>Root.Instance.Client</c>, and the reply is HTML-decoded and then
        /// deserialized as JSON, so a string result must arrive JSON-quoted. On failure it returns the default value
        /// without raising <see cref="FlashError"/>.
        /// </summary>
        public static T Call<T>(string function, params string[] args)
        {
            return TryDeserialize<T>(GetResponse(BuildRequest(function, args)));
        }

        /// <summary>Typed family, see <see cref="Call(string, Type, object[])"/>.</summary>
        public static string CallString(string function, params object[] args)
        {
            return Call<string>(function, args);
        }

        /// <summary>String family, see <see cref="Call{T}(string, string[])"/>; the result is discarded.</summary>
        public static void Call(string function, params string[] args)
        {
            Call<string>(function, args);
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

        public void SetGameObject(string path, object value)
        {
            Call("setGameObject", path, value);
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

        #endregion
    }
}