using Grimoire.Game;
using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Map
{
    public class CmdMoveToCell : IBotCommand
    {
        public string Cell
        {
            get;
            set;
        }

        public string Pad
        {
            get;
            set;
        }

        private string _cell
        {
            get
            {
                return BotManager.Instance.ActiveBotEngine.Value(Cell);
            }
        }

        private string _pad
        {
            get
            {
                return BotManager.Instance.ActiveBotEngine.Value(Pad);
            }
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Move;
            Player.MoveToCell(_cell, _pad);
            BotData.BotCell = _cell;
            BotData.BotPad = _pad;
        }

        public override string ToString()
        {
            return "Move to Cell: " + Cell + ", " + Pad;
        }
    }
}