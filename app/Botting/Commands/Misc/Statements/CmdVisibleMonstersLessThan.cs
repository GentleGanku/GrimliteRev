using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdVisibleMonstersLessThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdVisibleMonstersLessThan()
        {
            Tag = "Monster";
            Text = "Visible count is less than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            try
            {
                if (World.VisibleMonster(Player.Cell).Count >= int.Parse(instance.Value(Value1)))
                {
                    instance.Index++;
                }
            }
            catch
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Visible monster count is less than: " + Value1;
        }

        #endregion
    }
}