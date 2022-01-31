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
            if (Player.Mana <= int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
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