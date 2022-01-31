using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsNotInCell : StatementCommand, IBotCommand
    {
        public CmdPlayerIsNotInCell()
        {
            Tag = "Player";
            Text = "Player is not in cell";
        }

        public Task Execute(IBotEngine instance)
        {
            string cell = instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2;
            if (cell == "Blank")
            {
                cell = "Wait";
            }
            if (World.CellPs(cell).FirstOrDefault((PlayerInfo p) => p.Name.Equals(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1, StringComparison.OrdinalIgnoreCase)) != null)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player is not in {Value2}: " + Value1;
        }
    }
}