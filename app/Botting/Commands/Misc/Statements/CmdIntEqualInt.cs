using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIntEqualInt : StatementCommand, IBotCommand
    {
        public CmdIntEqualInt()
        {
            Tag = "Misc";
            Text = "Int is equal to Int";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Tempvalues[instance.Value(Value1)] != Configuration.Tempvalues[instance.Value(Value2)])
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"{Value1} is equal to: {Value2} (int)";
        }
    }
}
