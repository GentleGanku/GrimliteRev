using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIntNotEqualInt : StatementCommand, IBotCommand
    {
        public CmdIntNotEqualInt()
        {
            Tag = "Misc";
            Text = "Int is not equal to Int";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Tempvalues[instance.Value(Value1)] == Configuration.Tempvalues[instance.Value(Value2)])
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"{Value1} is not equal to: {Value2} (int)";
        }
    }
}
