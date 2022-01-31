using Grimoire.Game;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayersInMyCellGreaterThan : StatementCommand, IBotCommand
    {
        public CmdPlayersInMyCellGreaterThan()
        {
            Tag = "Player";
            Text = "Player count in my cell is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.CellPsInMyCell.Count() < int.Parse(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player count in my cell is greater than: {Value1}";
        }
    }
}