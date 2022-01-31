using Grimoire.Game;
using Grimoire.Tools.Buyback;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuyBack : IBotCommand
    {
        public string ItemName
        {
            get;
            set;
        }

        public int PageNumberCap
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            if (!Player.Inventory.ContainsItem(ItemName, "*"))
            {
                try
                {
                    await Task.Run(async delegate
                    {
                        using (AutoBuyBack abb = new AutoBuyBack())
                        {
                            await abb.Perform(ItemName, PageNumberCap);
                        }
                    });
                    Player.Logout();
                }
                catch
                {
                }
            }
        }

        public override string ToString()
        {
            return "Buy back: " + ItemName;
        }
    }
}