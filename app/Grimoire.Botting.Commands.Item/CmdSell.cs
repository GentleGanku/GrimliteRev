using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdSell : IBotCommand
    {
        public string ItemName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            await Shop.SellItem(instance.Value(ItemName));
        }

        public override string ToString()
        {
            return "Sell: " + ItemName;
        }
    }
}