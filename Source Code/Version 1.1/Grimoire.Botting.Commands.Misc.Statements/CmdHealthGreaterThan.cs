using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdHealthGreaterThan : StatementCommand, IBotCommand
    {
        public CmdHealthGreaterThan()
        {
            Tag = "This player";
            Text = "Health is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Health <= int.Parse(instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Health is greater than: " + Value1;
        }
    }
}