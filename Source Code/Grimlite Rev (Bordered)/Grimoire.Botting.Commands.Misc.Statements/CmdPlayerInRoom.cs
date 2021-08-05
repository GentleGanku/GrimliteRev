using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerInRoom : StatementCommand, IBotCommand
    {
        public CmdPlayerInRoom()
        {
            Tag = "Player";
            Text = "Player is in room";
        }

        public Task Execute(IBotEngine instance)
        {
            string PlayerName = "";
            if ( instance.IsVar(Value1) )
            {
                PlayerName = Configuration.Tempvariable[instance.GetVar(Value1)];
            }
            else
            {
                PlayerName = Value1;
            }

            if (World.PlayersInMap.FirstOrDefault((string p) => p.Equals(PlayerName, StringComparison.OrdinalIgnoreCase)) == null)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player is in room: " + Value1;
        }
    }
}