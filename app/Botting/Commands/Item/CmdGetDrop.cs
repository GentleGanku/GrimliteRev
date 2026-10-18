using Grimoire.Game;
using Grimoire.Utils;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdGetDrop : IBotCommand
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

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            await World.DropStack.GetDrop(instance.Value(ItemName));
        }

        public override string ToString()
        {
            return "Get drop: " + ItemName;
        }

        #endregion
    }
}