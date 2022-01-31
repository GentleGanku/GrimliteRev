using Grimoire.Game;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayersInCellLessThan : StatementCommand, IBotCommand
    {
        public CmdPlayersInCellLessThan()
        {
            Tag = "Player";
            Text = "Player count in cell is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            string cell = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            if (cell == "Blank")
            {
                cell = "Wait";
            }
            if (World.CellPs(cell).Count() > int.Parse(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player count in {Value1} is less than: {Value2}";
        }
    }
}