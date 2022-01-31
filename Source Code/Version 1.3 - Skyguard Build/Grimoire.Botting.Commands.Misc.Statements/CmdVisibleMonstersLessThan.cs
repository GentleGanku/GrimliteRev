using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdVisibleMonstersLessThan : StatementCommand, IBotCommand
    {
        public CmdVisibleMonstersLessThan()
        {
            Tag = "Monster";
            Text = "Visible count is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.VisibleMonster(Player.Cell).Count >= int.Parse((instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Visible monster count is less than: " + Value1;
        }
    }
}