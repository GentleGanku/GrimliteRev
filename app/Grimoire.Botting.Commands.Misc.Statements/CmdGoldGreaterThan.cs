using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdGoldGreaterThan : StatementCommand, IBotCommand
    {
        public CmdGoldGreaterThan()
        {
            Tag = "This player";
            Text = "Gold is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Gold <= int.Parse(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Gold is greater than: " + Value1;
        }
    }
}