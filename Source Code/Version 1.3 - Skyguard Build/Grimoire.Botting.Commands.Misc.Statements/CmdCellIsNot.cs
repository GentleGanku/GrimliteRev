using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdCellIsNot : StatementCommand, IBotCommand
    {
        public CmdCellIsNot()
        {
            Tag = "Map";
            Text = "Cell is not";
        }

        public Task Execute(IBotEngine instance)
        {
            string cell = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            if (cell == "Blank")
            {
                cell = "Wait";
            }
            if (cell.Equals(Player.Cell, StringComparison.OrdinalIgnoreCase))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Cell is not: " + Value1;
        }
    }
}