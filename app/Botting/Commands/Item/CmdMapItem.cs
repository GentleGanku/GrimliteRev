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

    public class CmdMapItem2 : IBotCommand
    {
        #region Properties

        public string ItemId
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.GetMapItem), null, 10, 500);
            Player.GetMapItem(instance.Value(ItemId));
            await Task.Delay(1500);
        }

        public override string ToString()
        {
            return "Get map item: " + ItemId;
        }

        #endregion
    }
}