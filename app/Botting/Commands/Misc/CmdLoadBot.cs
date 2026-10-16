using Grimoire.UI;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using DarkUI.Forms;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdLoadBot : IBotCommand
    {
        #region Properties

        public string BotFileName
        {
            get;
            set;
        }

        public string BotFilePath
        {
            get;
            set;
        }

        private string _path
        {
            get
            {
                return BotManager.Instance.ActiveBotEngine.Value(BotFilePath);
            }
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            if (File.Exists(_path))
            {
                try
                {
                    string value;
                    using (TextReader reader = new StreamReader(_path))
                    {
                        value = await reader.ReadToEndAsync();
                    }
                    JsonSerializerSettings serializerSettings = new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Include,
                        TypeNameHandling = TypeNameHandling.All,
                        SerializationBinder = BotFileBinder.Instance
                    };
                    Configuration newConfiguration = JsonConvert.DeserializeObject<Configuration>(value, serializerSettings);
                    int i = instance.CurrentConfiguration;
                    if (newConfiguration != null && newConfiguration.Commands.Count > 0)
                    {
                        if (!Bot.Configurations.ContainsKey(i))
                            Bot.Configurations.Add(i, instance.Configuration);
                        else Bot.Configurations[i] = instance.Configuration;
                        if (!Bot.OldIndex.ContainsKey(i))
                            Bot.OldIndex.Add(i, instance.Index);
                        else Bot.OldIndex[i] = instance.Index;
                        instance.Configuration = newConfiguration;
                        instance.Index = -1;
                        instance.CurrentConfiguration++;
                        BotManager.Instance.LastIndexedSearch = 0;
                        await instance.LoadBotQuests();
                    }
                }
                catch (Exception e) 
                {
                    var specifiedError = e.Message;
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen, Icon = global::Properties.Resources.GrimoireIcon }, $"Failed to load the bot. You cannot load any bot that has commands not available in this client.\r\n\r\nError Message:\r\n{specifiedError}", "Load Bot", MessageBoxIcon.Error);
                }
            }
        }

        public override string ToString()
        {
            return "Load bot: " + BotFileName;
        }

        #endregion
    }
}