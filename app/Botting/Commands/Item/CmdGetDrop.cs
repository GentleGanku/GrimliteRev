using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdGetDrop : IBotCommand
    {
        public string ItemName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            await World.DropStack.GetDrop(instance.Value(ItemName));
        }

        public override string ToString()
        {
            return "Get drop: " + ItemName;
        }
    }
}