using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdLogout : IBotCommand
    {
        public Task Execute(IBotEngine instance)
        {
            Player.Logout();
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Logout";
        }
    }
}