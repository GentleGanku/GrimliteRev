using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdCancelAutoAttack : IBotCommand
    {
        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            Player.CancelAutoAttack();
        }

        public override string ToString()
        {
            return "Cancel auto attack";
        }

        #endregion
    }
}