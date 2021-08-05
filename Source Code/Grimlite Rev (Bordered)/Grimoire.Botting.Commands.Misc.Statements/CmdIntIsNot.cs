using Grimoire.Game;
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
            if (Configuration.Tempvalues[(instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)] == int.Parse((instance.IsVar(Value2)  ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"{Value1} != {Value2}";
        }
    }
}