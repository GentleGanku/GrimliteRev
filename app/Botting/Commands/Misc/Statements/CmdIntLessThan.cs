using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIntLessThan : StatementCommand, IBotCommand
    {
        public CmdIntLessThan()
        {
            Tag = "Misc";
            Text = "Int is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Tempvalues[instance.Value(Value1)] >= int.Parse(instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"{Value1} is less than: {Value2} (value)";
        }
    }
}