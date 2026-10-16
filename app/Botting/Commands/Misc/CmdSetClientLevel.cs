using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdSetClientLevel : IBotCommand
    {
        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            OptionsManager.SetClientLevel();
        }

        public override string ToString()
        {
            return "Set client level to max";
        }

        #endregion
    }
}