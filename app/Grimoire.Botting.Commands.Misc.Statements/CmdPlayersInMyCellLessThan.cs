using Grimoire.Game;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayersInMyCellLessThan : StatementCommand, IBotCommand
    {
        public CmdPlayersInMyCellLessThan()
        {
            Tag = "Player";
            Text = "Player count in my cell is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            try
            {
                if (World.AvatarsInMyCell.Count >= int.Parse(instance.Value(Value1)))
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
            return $"Player count in my cell is less than: {Value1}";
        }
    }
}