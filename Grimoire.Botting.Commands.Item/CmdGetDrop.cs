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
            var item = instance.IsVar(ItemName) ? Configuration.Tempvariable[instance.GetVar(ItemName)] : ItemName;
            BotData.BotState = BotData.State.Others;
            await World.DropStack.GetDrop(item);
            await instance.WaitUntil(() => !World.DropStack.Contains(item), null, 5, 500);
        }

        public override string ToString()
        {
            return "Get drop: " + ItemName;
        }
    }
}