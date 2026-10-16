using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdKillFor : IBotCommand
    {
        #region Properties

        public string Monster
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public ItemType ItemType
        {
            get;
            set;
        }

        public string Quantity
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            CmdKill kill = new CmdKill
            {
                Monster = instance.Value(Monster)
            };
            while (instance.IsRunning && notInInventory(instance.Value(ItemName), instance.Value(Quantity)))
            {
                await kill.Execute(instance);
            }
            Player.CancelTarget();
        }

        public override string ToString()
        {
            return "Kill for " + ((ItemType == ItemType.Items) ? "items" : "temporary items") + ": " + Monster;
        }

        private bool notInInventory(string item, string quantity)
        {
            if (ItemType == ItemType.Items)
            {
                return !Player.Inventory.ContainsItem(item, quantity);
            }
            else
            {
                return !Player.TempInventory.ContainsItem(item, quantity);
            }
        }

        #endregion
    }
}