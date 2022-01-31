using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIsNotMember : StatementCommand, IBotCommand
    {
        public CmdIsNotMember()
        {
            Tag = "This player";
            Text = "Is Not Member";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.IsMember)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Is Not Member";
        }
    }
}