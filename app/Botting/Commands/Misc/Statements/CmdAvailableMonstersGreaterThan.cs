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
            try
            {
                if (World.MonstersInCell(Player.Cell).Count <= int.Parse(instance.Value(Value1)))
                {
                    instance.Index++;
                }
            }
            catch
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