using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdMapItem : IBotCommand
    {
        #region Properties

        public int ItemId
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.GetMapItem));
            Player.GetMapItem(ItemId);
        }

        public override string ToString()
        {
            return $"Get map item: {ItemId}";
        }

        #endregion
    }
}