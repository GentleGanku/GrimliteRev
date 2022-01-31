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
            var Value1 = ItemName;
            BotData.BotState = BotData.State.Others;
            await Task.Delay(500);
            await World.DropStack.GetDrop(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1);
            await instance.WaitUntil(() => !World.DropStack.Contains(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1));
        }

        public override string ToString()
        {
            return "Get drop: " + ItemName;
        }
    }
}