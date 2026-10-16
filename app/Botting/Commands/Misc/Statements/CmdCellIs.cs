using Grimoire.UI;
using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdCellIs : StatementCommand, IBotCommand
    {
        public CmdCellIs()
        {
            Tag = "Map";
            Text = "Cell is";
        }

        private string _cell => (BotManager.Instance.ActiveBotEngine.Value(Value1) != "Blank" && BotManager.Instance.ActiveBotEngine.Value(Value1) != "Wait") ? BotManager.Instance.ActiveBotEngine.Value(Value1) : "Enter";

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
    }
}