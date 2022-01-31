using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdChange : IBotCommand
    {
        public bool Guild
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            if (Guild)
                BotManager.Instance.CustomGuild = Text;
            else
                BotManager.Instance.CustomName = Text;
        }

        public override string ToString()
        {
            if (Guild)
                return $"Guild: {Text}";
            return $"Name: {Text}";
        }
    }
}