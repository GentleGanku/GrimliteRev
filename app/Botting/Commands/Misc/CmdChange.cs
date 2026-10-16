using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdChange : IBotCommand
    {
        #region Properties

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

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            if (Guild)
                BotManager.Instance.CustomGuild = instance.Value(Text);
            else
                BotManager.Instance.CustomName = instance.Value(Text);
        }

        public override string ToString()
        {
            if (Guild)
                return $"Guild: {Text}";
            return $"Name: {Text}";
        }

        #endregion
    }
}