using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdAvailableMonstersLessThan : StatementCommand, IBotCommand
    {
        public CmdAvailableMonstersLessThan()
        {
            Tag = "Monster";
            Text = "Available count is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.MonstersInCell(Player.Cell).Count >= int.Parse(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Available monster count is less than: " + Value1;
        }
    }
}