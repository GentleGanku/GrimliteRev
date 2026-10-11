using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdManaGreaterThan : StatementCommand, IBotCommand
    {
        public CmdManaGreaterThan()
        {
            Tag = "This player";
            Text = "Mana is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Mana <= int.Parse(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Mana is greater than: " + Value1;
        }
    }
}