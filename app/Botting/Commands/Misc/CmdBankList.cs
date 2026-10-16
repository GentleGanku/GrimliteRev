using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdBankList : IBotCommand
    {
        #region Methods

        public async Task Execute(IBotEngine instance)
        {
        }

        public override string ToString()
        {
            return "Bank Item list";
        }

        #endregion
    }
}