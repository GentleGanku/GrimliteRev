using Grimoire.UI;
using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdCellIs : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdCellIs()
        {
            Tag = "Map";
            Text = "Cell is";
        }

        #endregion

        #region Properties

        private string _cell
        {
            get
            {
                return (BotManager.Instance.ActiveBotEngine.Value(Value1) != "Blank" && BotManager.Instance.ActiveBotEngine.Value(Value1) != "Wait") ? BotManager.Instance.ActiveBotEngine.Value(Value1) : "Enter";
            }
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!Player.Cell.Equals(_cell, StringComparison.OrdinalIgnoreCase))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Cell is: " + Value1;
        }

        #endregion
    }
}