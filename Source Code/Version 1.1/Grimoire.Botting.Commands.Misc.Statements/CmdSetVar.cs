using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdSetVar : StatementCommand, IBotCommand
    {
        public CmdSetVar()
        {
            Tag = "Misc";
            Text = "Set Temporary Variable";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Configuration.Tempvariable.ContainsKey(Value1))
                Configuration.Tempvariable.Add(Value1, Value2);
            else
                Configuration.Tempvariable[Value1] = Value2;

            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Variable {Value1}: {Value2}";
        }
    }
}