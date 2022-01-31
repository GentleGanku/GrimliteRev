using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdCancelTarget : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
            if (Player.HasTarget)
            {
                Player.CancelTarget();
            }
        }

        public override string ToString()
        {
            return "Cancel target";
        }
    }
}