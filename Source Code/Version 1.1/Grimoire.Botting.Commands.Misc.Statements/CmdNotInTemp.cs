using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotInTemp : StatementCommand, IBotCommand
    {
        public CmdNotInTemp()
        {
            Tag = "Item";
            Text = "Is not in temp";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.TempInventory.ContainsItem((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), (instance.IsVar(Value2)  ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item is not in temp inventory: " + Value1 + ", " + Value2;
        }
    }
}