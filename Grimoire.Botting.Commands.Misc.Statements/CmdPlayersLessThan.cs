using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayersLessThan : StatementCommand, IBotCommand
    {
        public CmdPlayersLessThan()
        {
            Tag = "Player";
            Text = "Count is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.PlayersInMap.Count >= int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player count is less than: " + Value1;
        }
    }
}