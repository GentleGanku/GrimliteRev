using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIntIsNot : StatementCommand, IBotCommand
    {
        public CmdIntIsNot()
        {
            Tag = "Misc";
            Text = "Int is not";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Tempvalues[instance.Value(Value1)] == int.Parse(instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"{Value1} is not equal to: {Value2} (value)";
        }
    }
}