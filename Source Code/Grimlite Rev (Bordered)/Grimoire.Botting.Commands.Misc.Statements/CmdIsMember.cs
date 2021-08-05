using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIsMember : StatementCommand, IBotCommand
    {
        public CmdIsMember()
        {
            Tag = "This player";
            Text = "Is Member";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.IsMember)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Is Member";
        }
    }
}