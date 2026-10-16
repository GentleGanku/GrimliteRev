using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdVisibleMonstersGreaterThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdVisibleMonstersGreaterThan()
        {
            Tag = "Monster";
            Text = "Visible count is greater than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            try
            {
                if (World.VisibleMonster(Player.Cell).Count <= int.Parse(instance.Value(Value1)))
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
            return "Visible monster count is greater than: " + Value1;
        }

        #endregion
    }
}