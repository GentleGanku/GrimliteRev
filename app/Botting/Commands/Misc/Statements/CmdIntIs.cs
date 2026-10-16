using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIntIs : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdIntIs()
        {
            Tag = "Misc";
            Text = "Int is";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Tempvalues[instance.Value(Value1)] != int.Parse(instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"{Value1} is equal to: {Value2} (value)";
        }

        #endregion
    }
}