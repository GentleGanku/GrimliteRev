using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdLevelLessThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdLevelLessThan()
        {
            Tag = "This player";
            Text = "Level is less than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.Level >= int.Parse(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Level is less than: " + Value1;
        }

        #endregion
    }
}