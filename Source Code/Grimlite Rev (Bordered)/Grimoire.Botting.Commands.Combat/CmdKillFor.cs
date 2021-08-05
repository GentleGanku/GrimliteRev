using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdKillFor : IBotCommand
    {
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

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Combat;
            CmdKill kill = new CmdKill
            {
                Monster = Monster
            };
            if (ItemType == ItemType.Items)
            {
                while (instance.IsRunning && Player.IsLoggedIn && Player.IsAlive && !Player.Inventory.ContainsItem(ItemName, Quantity))
                {
                    await kill.Execute(instance);
                    await Task.Delay(1000);
                }
            }
            else
            {
                while (instance.IsRunning && Player.IsLoggedIn && Player.IsAlive && !Player.TempInventory.ContainsItem(ItemName, Quantity))
                {
                    await kill.Execute(instance);
                    await Task.Delay(1000);
                }
            }
        }

        public override string ToString()
        {
            return "Kill for " + ((ItemType == ItemType.Items) ? "items" : "tempitems") + ": " + Monster;
        }
    }
}