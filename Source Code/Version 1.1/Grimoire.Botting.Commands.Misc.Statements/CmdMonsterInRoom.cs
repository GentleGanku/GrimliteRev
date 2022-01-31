using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMonsterInRoom : StatementCommand, IBotCommand
    {
        public CmdMonsterInRoom()
        {
            Tag = "Monster";
            Text = "Is in room";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!World.IsMonsterAvailable((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Monster is in room: " + Value1;
        }
    }
}