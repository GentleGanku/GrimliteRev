using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Grimoire.Tools.Plugins
{
    public class GrimoirePlugin
    {
        public static List<GrimoirePlugin> LoadedPlugins = new List<GrimoirePlugin>();

        private readonly string _pluginPath;

        private IGrimoirePlugin _plugin;

        public string Name
        {
            get;
        }

        public string Author => _plugin?.Author ?? string.Empty;

        public string Description => _plugin?.Description ?? string.Empty;

        public string LastError
        {
            get;
            private set;
        }

        public GrimoirePlugin(string dllFilePath)
        {
            _pluginPath = dllFilePath;
            Name = Path.GetFileName(dllFilePath);
        }

        private Type[] TryGetTypes(Assembly asm)
        {
            try
            {
                return asm.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where((Type t) => t != null).ToArray();
            }
        }

        public bool Load()
        {
            try
            {
                if (!File.Exists(_pluginPath))
                {
                    LastError = "Could not find file: " + _pluginPath;
                    return false;
                }
                Assembly asm = Assembly.LoadFile(_pluginPath);
                Type[] source;
                if ((source = TryGetTypes(asm)) == null)
                {
                    LastError = "Unable to retrieve any types in the assembly.";
                    return false;
                }
                Type type;
                if ((type = source.FirstOrDefault((Type t) => t.IsDefined(typeof(GrimoirePluginEntry), inherit: true))) == null)
                {
                    LastError = "Could not find a class marked with the GrimoirePluginEntry attribute.";
                    return false;
                }
                _plugin = (IGrimoirePlugin)Activator.CreateInstance(type);
                _plugin.Load();
                LoadedPlugins.Add(this);
                return true;
            }
            catch (Exception ex)
            {
                LastError = "Failure! This is either not a Grimoire plugin, or its code does not conform to the Grimoire standard.\n" + ex.Message + "\n" + ex.StackTrace;
                return false;
            }
        }

        public bool Unload()
        {
            try
            {
                _plugin.Unload();
                LoadedPlugins.Remove(this);
                return true;
            }
            catch (Exception ex)
            {
                LastError = "Failed to unload plugin!\n" + ex.Message + "\n" + ex.StackTrace;
                return false;
            }
        }
    }
}