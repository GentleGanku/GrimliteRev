using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayersInMyCellGreaterThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayersInMyCellGreaterThan()
        {
            Tag = "Player";
            Text = "Player count in my cell is greater than";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            try
            {
                if (World.AvatarsInMyCell.Count <= int.Parse(instance.Value(Value1)))
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
            return $"Player count in my cell is greater than: {Value1}";
        }

        #endregion
    }
}