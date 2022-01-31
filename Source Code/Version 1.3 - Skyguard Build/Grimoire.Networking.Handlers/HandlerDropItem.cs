using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
    public class HandlerDropItem : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[3]
        {
            "dropItem",
            "getDrop",
            "addItems"
        };

        public async void Handle(JsonMessage message)
        {
            if (message.Command == "dropItem")
            {
                JToken jToken = message.DataObject?["items"];
                if (jToken == null)
                {
                    return;
                }
                InventoryItem item = jToken.ToObject<Dictionary<int, InventoryItem>>().First().Value;
                World.OnItemDropped(item);
            }
            else if (message.Command == "getDrop")
            {
                int id = message.DataObject["ItemID"].Value<int>();
                if (message.DataObject["bSuccess"].Value<int>() != 1)
                {
                    return;
                }
                if (!BotManager.Instance.ActiveBotEngine.IsRunning)
                {
                    World.DropStack.RemoveAll(id);
                }
                else
                {
                    await Task.Delay(10);
                    string dropName = (message.DataObject["bBank"].Value<int>() != 0) ? $"{Player.Bank.SavedItems.FirstOrDefault((InventoryItem it) => it.Id.Equals(id)).Name}" : $"{(Player.Inventory.Items.Find((InventoryItem x) => x.Id.Equals(id)) ?? new InventoryItem()).Name}";
                    int dropQty = message.DataObject["iQty"].Value<int>();
                    LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] (Bot) Item accepted: {dropName} x {dropQty}.\r\n");
                }
            }
            else if (message.Command == "addItems")
            {
                int itemID = int.Parse(message.RawContent.Split(new string[] { "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"addItems\",\"items\":{\"" }, StringSplitOptions.None)[1].Split('"')[0].Trim());
                string dropQty = message.RawContent.Split(new string[] { "\"iQty\":" }, StringSplitOptions.None)[1].Split('}')[0].Trim();
                string dropQtyNow = message.RawContent.Split(new string[] { "\"iQtyNow\":" }, StringSplitOptions.None)[1].Split(',')[0].Trim();
                string dropName = $"{Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Id.Equals(itemID)).Name}";
                if (BotManager.Instance.ActiveBotEngine.IsRunning)
                {
                    LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] (Bot) Item accepted: {dropName} x {dropQty}.\r\n");
                }
                if (!Bot.Instance.DropsInInventory.Contains(dropName.ToLower()))
                    Bot.Instance.DropsInInventory.Add(dropName.ToLower());
                LogForm.Instance.AppendDrops($"[Item Drop] ({dropQtyNow}) {dropName} x {dropQty} at {DateTime.Now:hh:mm:ss tt}.\r\n");
            }
        }
    }
}