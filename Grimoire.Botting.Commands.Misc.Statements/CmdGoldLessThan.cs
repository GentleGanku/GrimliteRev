using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdGoldLessThan : StatementCommand, IBotCommand
    {
        public CmdGoldLessThan()
        {
            Tag = "This player";
            Text = "Gold is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Gold >= int.Parse(instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Gold is less than: " + Value1;
        }
    }
}