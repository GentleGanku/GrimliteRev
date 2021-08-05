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
            bool checkVarCell = instance.IsVar(Cell);
            bool checkVarPad = instance.IsVar(Pad);

            if (!Player.Cell.Equals(Cell, StringComparison.OrdinalIgnoreCase))
            {
                if (checkVarCell && checkVarPad)
                    Player.MoveToCell(Configuration.Tempvariable[instance.GetVar(Cell)], Configuration.Tempvariable[instance.GetVar(Pad)]);
                else if (checkVarCell)
                    Player.MoveToCell(Configuration.Tempvariable[instance.GetVar(Cell)], Pad);
                else if (checkVarPad)
                    Player.MoveToCell(Cell, Configuration.Tempvariable[instance.GetVar(Pad)]);

                else
                    Player.MoveToCell(Cell, Pad);
                await Task.Delay(500);
            }
            BotData.BotCell = Cell;
            BotData.BotPad = Pad;
        }

        public override string ToString()
        {
            return "Move to cell: " + Cell + ", " + Pad;
        }
    }
}