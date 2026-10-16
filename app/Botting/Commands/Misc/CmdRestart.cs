using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdRestart : IBotCommand
    {
        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            instance.Index = 0;
            return;
        }

        public override string ToString()
        {
            return "Restart bot";
        }

        #endregion
    }
}