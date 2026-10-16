using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdCancelTarget : IBotCommand
    {
        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            Player.CancelTarget();
        }

        public override string ToString()
        {
            return "Cancel target";
        }

        #endregion
    }
}