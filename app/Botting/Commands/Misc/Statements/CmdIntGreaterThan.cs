using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIntGreaterThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdIntGreaterThan()
        {
            Tag = "Misc";
            Text = "Int is greater than or equal to";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Tempvalues[instance.Value(Value1)] < int.Parse(instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"{Value1} is greater than or equal to: {Value2} (value)";
        }

        #endregion
    }
}