using Grimoire.UI;
using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdCellIsNot : StatementCommand, IBotCommand
    {
        public CmdCellIsNot()
        {
            Tag = "Map";
            Text = "Cell is not";
        }

        private string _cell => (BotManager.Instance.ActiveBotEngine.Value(Value1) != "Blank" && BotManager.Instance.ActiveBotEngine.Value(Value1) != "Wait") ? BotManager.Instance.ActiveBotEngine.Value(Value1) : "Enter";

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
    }
}