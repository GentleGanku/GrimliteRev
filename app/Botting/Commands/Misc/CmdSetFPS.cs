using Grimoire.Tools;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdSetFPS : IBotCommand
    {
        #region Properties

        public int FPS
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            Flash.Call("SetFPS", FPS);
        }

        public override string ToString()
        {
            return $"Set FPS: {FPS}";
        }

        #endregion
    }
}