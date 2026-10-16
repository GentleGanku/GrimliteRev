using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdAvailableMonstersLessThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdAvailableMonstersLessThan()
        {
            Tag = "Monster";
            Text = "Available count is less than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            try
            {
                if (World.MonstersInCell(Player.Cell).Count >= int.Parse(instance.Value(Value1)))
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
            return "Available monster count is less than: " + Value1;
        }

        #endregion
    }
}