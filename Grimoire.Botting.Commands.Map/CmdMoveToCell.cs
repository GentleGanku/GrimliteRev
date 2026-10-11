using Grimoire.Game;
using System;
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

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Move;
            var cell = instance.IsVar(Cell) ? Configuration.Tempvariable[instance.GetVar(Cell)] : Cell;
            var pad = instance.IsVar(Pad) ? Configuration.Tempvariable[instance.GetVar(Pad)] : Pad;
            if (cell == "Blank")
            {
                cell = "Wait";
            }
            if (!Player.Cell.Equals(cell, StringComparison.OrdinalIgnoreCase))
            {
                Player.MoveToCell(cell, pad);
                await Task.Delay(500);
            }
            World.SetSpawnPoint();
            BotData.BotCell = cell;
            BotData.BotPad = pad;
        }

        public override string ToString()
        {
            return "Move to Cell: " + Cell + ", " + Pad;
        }
    }
}