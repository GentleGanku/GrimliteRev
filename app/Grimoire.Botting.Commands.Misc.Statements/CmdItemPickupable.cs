using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdItemPickupable : StatementCommand, IBotCommand
    {
        public CmdItemPickupable()
        {
            Tag = "Item";
            Text = "Has dropped";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!World.DropStack.Contains(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item has dropped: " + Value1;
        }
    }
}