using Grimoire.Game;
using Grimoire.Tools.Buyback;
using Grimoire.Utils;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuyBack : IBotCommand
    {
        #region Fields

        private string _itemName;

        #endregion

        #region Properties

        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                _itemName = value.UnifyQuotes();
            }
        }

        public int PageNumberCap
        {
            get;
            set;
        }

        #endregion

        #region Methods

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

        #endregion
    }
}