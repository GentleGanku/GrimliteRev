using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIsNotMaxStack : StatementCommand, IBotCommand
    {
        public CmdIsNotMaxStack()
        {
            Tag = "Item";
            Text = "Is not max in inventory";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.Inventory.ContainsMaxItem((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Is not maxed out: {Value1}";
        }
    }
}
