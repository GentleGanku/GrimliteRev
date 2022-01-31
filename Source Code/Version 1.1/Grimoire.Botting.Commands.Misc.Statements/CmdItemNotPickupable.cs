using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdItemNotPickupable : StatementCommand, IBotCommand
    {
        public CmdItemNotPickupable()
        {
            Tag = "Item";
            Text = "Has not dropped";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.DropStack.Contains((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item has not dropped: " + Value1;
        }
    }
}