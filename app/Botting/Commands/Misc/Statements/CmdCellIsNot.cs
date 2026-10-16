using Grimoire.UI;
using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdCellIsNot : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdCellIsNot()
        {
            Tag = "Map";
            Text = "Cell is not";
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
            if (Player.Cell.Equals(_cell, StringComparison.OrdinalIgnoreCase))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Cell is not: " + Value1;
        }

        #endregion
    }
}