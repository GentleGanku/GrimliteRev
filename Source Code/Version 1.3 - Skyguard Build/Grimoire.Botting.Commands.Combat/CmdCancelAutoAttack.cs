using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdCancelAutoAttack : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
            if (Player.HasTarget)
            {
                Player.CancelAutoAttack();
            }
        }

        public override string ToString()
        {
            return "Cancel auto attack";
        }
    }
}