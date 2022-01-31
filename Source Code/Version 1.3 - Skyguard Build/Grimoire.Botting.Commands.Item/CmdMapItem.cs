using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdMapItem : IBotCommand
    {
        public int ItemId
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.GetMapItem));
            Player.GetMapItem(ItemId);
            await Task.Delay(500);
        }

        public override string ToString()
        {
            return $"Get map item: {ItemId}";
        }
    }

    public class CmdMapItem2 : IBotCommand
    {
        public string ItemId
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.GetMapItem));
            Player.GetMapItem(int.Parse(instance.IsVar(ItemId) ? Configuration.Tempvariable[instance.GetVar(ItemId)] : ItemId));
            await Task.Delay(500);
        }

        public override string ToString()
        {
            return $"Get map item: {ItemId}";
        }
    }
}