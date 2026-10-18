using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdEquip : IBotCommand
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

        public bool Safe
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(instance.Value(ItemName), StringComparison.OrdinalIgnoreCase) && i.IsEquippable);
            if ((item == null) || item.IsEquipped)
            {
                return;
            }
            BotData.BotState = BotData.State.Transaction;
            if (Safe)
            {
                await BotUtilities.MoveOutOfCombat();
            }
            await item.Equip();
        }

        public override string ToString()
        {
            return (Safe ? "Safe " : null) + "Equip: " + ItemName;
        }

        #endregion
    }
}