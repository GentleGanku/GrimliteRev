using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.UI;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdEquipSet : IBotCommand
    {
        public string ItemName
        {
            get;
            set;
        }

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Include,
            //NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        };

        public async Task Execute(IBotEngine instance)
        {

            dynamic jsonObj = JsonConvert.DeserializeObject<SetItem>(Application.StartupPath + ItemName, _serializerSettings);

            foreach (var obj in jsonObj.Set)
            {
                InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(obj.Name, StringComparison.OrdinalIgnoreCase) && i.IsEquippable);
                while (instance.IsRunning && !IsEquipped(item.Id))
                {
                    BotData.BotState = BotData.State.Transaction;
                    while (instance.IsRunning && Player.CurrentState == Player.State.InCombat)
                    {
                        Player.MoveToCell(Player.Cell, Player.Pad);
                        await Task.Delay(1000);
                    }
                    await instance.WaitUntil(() => World.IsActionAvailable(LockActions.EquipItem));
                    Player.Equip(item.Name);
                }
            }
        }

        public bool IsEquipped(int ItemID)
        {
            return Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.IsEquipped && it.Id == ItemID) != null;
        }

        public override string ToString()
        {
            return "Equip Set: " + ItemName;
        }
    }
}