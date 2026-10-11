using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotInInventory : StatementCommand, IBotCommand
    {
        public CmdNotInInventory()
        {
            Tag = "Item";
            Text = "Is not in inventory";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Inventory.ContainsItem((instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), (instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item is not in inventory: " + Value1 + ", " + Value2;
        }
    }
}