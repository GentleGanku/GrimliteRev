using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerInRoom : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayerInRoom()
        {
            Tag = "Player";
            Text = "Player is in room";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (World.PlayersInMap.FirstOrDefault((string p) => p.Equals(instance.Value(Value1), StringComparison.OrdinalIgnoreCase)) == null)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player is in room: " + Value1;
        }

        #endregion
    }
}