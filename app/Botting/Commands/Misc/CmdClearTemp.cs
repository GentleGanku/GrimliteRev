using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdClearTemp : IBotCommand
    {
        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            Configuration.Tempvalues.Clear();
            Configuration.Tempvariable.Clear();
        }

        public override string ToString()
        {
            return "Clear Variables and Integers";
        }

        #endregion
    }
}