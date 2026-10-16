using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotInBank : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdNotInBank()
        {
            Tag = "Item";
            Text = "Is not in bank";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.Bank.ContainsItem(instance.Value(Value1), instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item is not in bank: " + Value1 + ", " + Value2;
        }

        #endregion
    }
}