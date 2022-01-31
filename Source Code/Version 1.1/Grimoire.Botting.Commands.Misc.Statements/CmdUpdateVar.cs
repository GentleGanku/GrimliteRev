using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdUpdateVar : StatementCommand, IBotCommand
    {
        public CmdUpdateVar()
        {
            Tag = "Misc";
            Text = "Update Temporary Variable";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Tempvariable.ContainsKey(Value1))
                Configuration.Tempvariable[Value1] = Value2;
            else
                instance.Index++;

            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Update Variable {Value1}: {Value2}";
        }
    }
}