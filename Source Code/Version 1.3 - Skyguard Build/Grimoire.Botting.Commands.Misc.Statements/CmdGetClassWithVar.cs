using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdGetClassWithVar : StatementCommand, IBotCommand
    {
        public CmdGetClassWithVar()
        {
            Tag = "This player";
            Text = "Get equipped class with Variable";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Configuration.Tempvariable.ContainsKey(Value1))
                Configuration.Tempvariable.Add(Value1, Player.Class);
            else
                Configuration.Tempvariable[Value1] = Player.Class;

            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Get equipped class with Variable: {Value1}";
        }
    }
}