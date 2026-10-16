using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.Botting
{
    public class BotClientConfig
    {
        public string file
        {
            get;
            set;
        }

        public Dictionary<string, string> Contents
        {
            get;
            set;
        } = new Dictionary<string, string>();

        public BotClientConfig() { }

        public static BotClientConfig Instance
        {
            get;
            set;
        } = BotClientConfig.Load(Application.StartupPath + "\\BotClientConfig.cfg");

        public string Get(string key)
        {
            return Contents.TryGetValue(key, out string s) ? s : null;
        }

        public void Set(string key, string value)
        {
            Contents[key] = value;
        }

        public void Save()
        {
            File.WriteAllLines(file, Contents.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        public static BotClientConfig Load(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            return new BotClientConfig()
            {
                file = path,
                Contents = File.ReadLines(path).Select(l => l.Split('=')).ToDictionary(a => a[0], a => a[1])
            };
        }

        public T GetValue<T>(string key)
        {
            object value;
            try
            {
                value = BotClientConfig.Instance.Get(key);
            }
            catch
            {
                value = null;
            }
            if (typeof(T) == typeof(int))
            {
                value = (value == null) ? -1 : int.Parse(value.ToString());
            }
            else if (typeof(T) == typeof(bool))
            {
                value = (value == null) ? false : bool.Parse(value.ToString());
            }
            else if (typeof(T) == typeof(string))
            {
                value = (value == null) ? null : value.ToString();
            }
            return (T)value;
        }

        public void SetValue(string key, string value)
        {
            BotClientConfig.Instance.Set(key, value);
            BotClientConfig.Instance.Save();
        }
    }
}
