using Grimoire.Game;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayersInCellGreaterThan : StatementCommand, IBotCommand
    {
        public CmdPlayersInCellGreaterThan()
        {
            Tag = "Player";
            Text = "Player count in cell is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            string cell = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            if (cell == "Blank")
            {
                cell = "Wait";
            }
            if (World.CellPs(cell).Count() < int.Parse(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player count in {Value1} is greater than: {Value2}";
        }
    }
}