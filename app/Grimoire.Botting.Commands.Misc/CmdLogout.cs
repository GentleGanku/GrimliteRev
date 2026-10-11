using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdLogout : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
            Player.Logout();
            await instance.WaitUntil(() => !Player.IsLoggedIn, null, 10, 500);
        }

        public override string ToString()
        {
            return "Logout";
        }
    }
}