using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.Botting
{
    public class Config
    {
        #region Constructors

        public Config() { }

        #endregion

        #region Properties

        public static Config Instance
        {
            get;
            set;
        } = Config.Load(Application.StartupPath + "\\config.cfg");

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

        #endregion

        #region Methods

        public static Config Load(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            return new Config()
            {
                file = path,
                Contents = File.ReadLines(path).Select(l => l.Split('=')).ToDictionary(a => a[0], a => a[1])
            };
        }

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

        public T GetValue<T>(string key)
        {
            object value;
            try
            {
                value = Config.Instance.Get(key);
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
            Config.Instance.Set(key, value);
            Config.Instance.Save();
        }

        #endregion
    }
}