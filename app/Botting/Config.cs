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
                Contents = Parse(File.ReadLines(path))
            };
        }

        /// <summary>
        /// Reads <c>key=value</c> lines. Only the first <c>=</c> separates, so values may contain <c>=</c> (Base64, URLs);
        /// lines without a key are skipped, and a repeated key keeps its last value.
        /// </summary>
        public static Dictionary<string, string> Parse(IEnumerable<string> lines)
        {
            var contents = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                int separator = line.IndexOf('=');
                if (separator > 0)
                {
                    contents[line.Substring(0, separator)] = line.Substring(separator + 1);
                }
            }
            return contents;
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