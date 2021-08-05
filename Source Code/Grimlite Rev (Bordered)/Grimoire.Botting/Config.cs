using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public class Config
    {
        public string file { get; set; }

        public Dictionary<string, string> Contents { get; set; } = new Dictionary<string, string>();

        public Config()
        {

        }

        public static Config Instance = new Config();

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

        public static Config Load(string path)
        {
            if(!File.Exists(path))
            {
                File.Create(path);
                Task.Delay(100);
            }
            return new Config()
            {
                file = path,
                Contents = File.ReadLines(path).Select(l => l.Split('=')).ToDictionary(a => a[0], a => a[1])
            };
        }
    }
}