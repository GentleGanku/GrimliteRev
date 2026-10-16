using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayersLessThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayersLessThan()
        {
            Tag = "Player";
            Text = "Count is less than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (World.PlayersInMap.Count >= int.Parse(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player count is less than: " + Value1;
        }

        #endregion
    }
}