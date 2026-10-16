using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdReturn : IBotCommand
    {
        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            try
            {
                int i = --instance.CurrentConfiguration;
                Configuration oldConfig = Bot.Configurations[i];
                int oldIndex = Bot.OldIndex[i];
                if (oldConfig != null && oldConfig.Commands.Count > 0 && oldIndex > -1)
                {
                    instance.Configuration = oldConfig;
                    instance.Index = oldIndex;
                    await instance.LoadBotQuests();
                }
            }
            catch {}
        }

        public override string ToString()
        {
            return "Return";
        }

        #endregion
    }
}