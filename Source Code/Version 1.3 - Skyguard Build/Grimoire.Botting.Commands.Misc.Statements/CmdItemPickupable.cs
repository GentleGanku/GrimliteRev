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
            var item = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            if (!World.DropStack.Contains(item))
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