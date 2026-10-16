using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdManaLessThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdManaLessThan()
        {
            Tag = "This player";
            Text = "Mana is less than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.Mana >= int.Parse(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Mana is less than: " + Value1;
        }

        #endregion
    }
}