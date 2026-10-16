using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsNotInMyCell : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayerIsNotInMyCell()
        {
            Tag = "Player";
            Text = "Player is not in my cell";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            try
            {
                if (World.AvatarsInMyCell.FirstOrDefault((Avatar p) => p.Username.Equals(instance.Value(Value1), StringComparison.OrdinalIgnoreCase)) != null)
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
            return "Player is not in my cell: " + Value1;
        }

        #endregion
    }
}