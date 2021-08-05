using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdLevelGreaterThan : StatementCommand, IBotCommand
    {
        public CmdLevelGreaterThan()
        {
            Tag = "This player";
            Text = "Level is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Level <= int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Level is greater than: " + Value1;
        }
    }
}