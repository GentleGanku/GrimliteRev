using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdEquipSet : IBotCommand
    {
        #region Fields

        private const int MaxAttempts = 5;

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Include,
            TypeNameHandling = TypeNameHandling.All,
            SerializationBinder = BotFileBinder.Instance
        };

        #endregion

        #region Properties

        public string ItemName
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            string file = FindSetFile(ItemName);
            if (file == null)
            {
                LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Equip Set: the set file {ItemName} doesn't exist.\r\n");
                return;
            }
            SetItem set = JsonConvert.DeserializeObject<SetItem>(File.ReadAllText(file), _serializerSettings);
            foreach (Grimoire.UI.Item setItem in set?.Set.OfType<Grimoire.UI.Item>() ?? Enumerable.Empty<Grimoire.UI.Item>())
            {
                InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(setItem.Name, StringComparison.OrdinalIgnoreCase) && i.IsEquippable);
                if (item == null)
                {
                    LogForm.Instance.AppendBot($"[{DateTime.Now:hh:mm:ss}] Equip Set: {setItem.Name} isn't in the inventory.\r\n");
                    continue;
                }
                for (int attempt = 0; attempt < MaxAttempts && instance.IsRunning && !IsEquipped(item.Id); attempt++)
                {
                    BotData.BotState = BotData.State.Transaction;
                    while (instance.IsRunning && Player.CurrentState == Player.State.InCombat)
                    {
                        Player.MoveToCell(Player.Cell, Player.Pad);
                        await Task.Delay(1000);
                    }
                    await instance.WaitUntil(() => World.IsActionAvailable(LockActions.EquipItem));
                    Player.Equip(item.Id);
                    await instance.WaitUntil(() => IsEquipped(item.Id), null, 4, 500);
                }
            }
        }

        // ItemName has been written as an absolute path, a path under the program folder, or a name in Sets\ with or
        // without the .gset extension; accept all of them.
        private static string FindSetFile(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            string relative = name.Trim().TrimStart('\\', '/');
            string[] candidates =
            {
                Path.IsPathRooted(name) ? name : null,
                Path.Combine(Application.StartupPath, relative),
                Path.Combine(Application.StartupPath, "Sets", relative),
                Path.Combine(Application.StartupPath, "Sets", relative + ".gset")
            };
            return candidates.FirstOrDefault(c => c != null && File.Exists(c));
        }

        public bool IsEquipped(int ItemID)
        {
            return Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.IsEquipped && it.Id == ItemID) != null;
        }

        public override string ToString()
        {
            return "Equip Set: " + ItemName;
        }

        #endregion
    }
}