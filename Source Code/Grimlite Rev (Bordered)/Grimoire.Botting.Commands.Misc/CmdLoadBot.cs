using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdLoadBot : IBotCommand
    {
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

        public async Task Execute(IBotEngine instance)
        {
            string name = instance.IsVar(BotFileName) ? Configuration.Tempvariable[instance.GetVar(BotFileName)] : BotFileName;
            string path = instance.IsVar(BotFilePath) ? Configuration.Tempvariable[instance.GetVar(BotFilePath)] : BotFilePath;
            if (File.Exists(path))
            {
                try
                {
                    string value;
                    using (TextReader reader = new StreamReader(path))
                    {
                        value = await reader.ReadToEndAsync();
                    }
                    JsonSerializerSettings serializerSettings = new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Include,
                        //NullValueHandling = NullValueHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.All
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
                        instance.LoadBankItems();
                        instance.LoadAllQuests();
                        instance.CurrentConfiguration++;
                    }
                }
                catch (Exception e) { MessageBox.Show(e.ToString()); }
            }
        }

        public override string ToString()
        {
            return "Load bot: " + BotFileName;
        }
    }
}