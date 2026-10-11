using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdLevelIs : StatementCommand, IBotCommand
    {
        public CmdLevelIs()
        {
            Tag = "This player";
            Text = "Level is";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Level != int.Parse(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Level is equal to: " + Value1;
        }
    }
}