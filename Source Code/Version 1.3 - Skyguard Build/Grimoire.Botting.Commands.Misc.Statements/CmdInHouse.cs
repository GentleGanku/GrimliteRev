using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdInHouse : StatementCommand, IBotCommand
    {
        public CmdInHouse()
        {
            Tag = "Item";
            Text = "Is in house";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.House.ContainsItem((instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), (instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Is in house: " + Value1 + ", " + Value2;
        }
    }
}