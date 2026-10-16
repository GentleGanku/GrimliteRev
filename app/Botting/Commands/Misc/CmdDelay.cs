using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdDelay : IBotCommand
    {
        #region Properties

        public int Delay
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            await Task.Delay(Delay);
        }

        public override string ToString()
        {
            return "Delay: " + Delay;
        }

        #endregion
    }
}