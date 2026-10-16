using Grimoire.UI;
using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsInCell : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayerIsInCell()
        {
            Tag = "Player";
            Text = "Player is in cell";
        }

        #endregion

        #region Properties

        private string _cell
        {
            get
            {
                return BotManager.Instance.ActiveBotEngine.Value(Value2);
            }
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            try
            {
                if (World.AvatarsInCell(_cell).FirstOrDefault((Avatar p) => p.Name.Equals(instance.Value(Value1), StringComparison.OrdinalIgnoreCase)) == null)
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
            return $"Player is in {Value2}: " + Value1;
        }

        #endregion
    }
}