using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdAvailableMonstersGreaterThan : StatementCommand, IBotCommand
    {
        public CmdAvailableMonstersGreaterThan()
        {
            Tag = "Monster";
            Text = "Available count is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.MonstersInCell(Player.Cell).Count <= int.Parse(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Available monster count is greater than: " + Value1;
        }
    }
}