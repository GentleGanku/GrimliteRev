using Grimoire.UI;
using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayersInCellGreaterThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayersInCellGreaterThan()
        {
            Tag = "Player";
            Text = "Player count in cell is greater than";
        }

        #endregion

        #region Properties

        private string _cell
        {
            get
            {
                return BotManager.Instance.ActiveBotEngine.Value(Value1);
            }
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            try
            {
                if (World.AvatarsInCell(_cell).Count <= int.Parse(instance.Value(Value2)))
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
            return $"Player count in {Value1} is greater than: {Value2}";
        }

        #endregion
    }
}