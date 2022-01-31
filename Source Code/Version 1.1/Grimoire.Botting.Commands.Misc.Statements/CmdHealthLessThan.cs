using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdHealthLessThan : StatementCommand, IBotCommand
    {
        public CmdHealthLessThan()
        {
            Tag = "This player";
            Text = "Health is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Health >= int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Health is less than: " + Value1;
        }
    }
}