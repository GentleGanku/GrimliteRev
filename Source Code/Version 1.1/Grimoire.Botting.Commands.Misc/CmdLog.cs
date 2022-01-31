using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdLog : IBotCommand
    {
        public string Text
        {
            get;
            set;
        }

        public bool Debug
        {
            get;
            set;
        } = false;

        private bool Clear
        {
            get;
            set;
        } = false;

        public async Task Execute(IBotEngine instance)
        {
            string text = Text;
            text = text.Replace("{USERNAME}", Player.Username);
            text = text.Replace("{MAP}", Player.Map).Replace("{ROOM_ID}", World.RoomId.ToString()).Replace("{ROOM_NUM}", World.RoomNumber.ToString());
            text = text.Replace("{GOLD}", Player.Gold.ToString());
            text = text.Replace("{LEVEL}", Player.Level.ToString());
            text = text.Replace("{CELL}", Player.Cell).Replace("{PAD}", Player.Pad);
            text = text.Replace("{HEALTH}", Player.Health.ToString()).Replace("{MANA}", Player.Mana.ToString());
            text = text.Replace("{TIME}", $"{DateTime.Now:HH:mm:ss}");
            text = text.Replace("{TIME: 12}", $"{DateTime.Now:hh:mm:ss tt}");
            text = text.Replace("{TIME: 24}", $"{DateTime.Now:HH:mm:ss}");

            text = new Regex(
                "{ITEM:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(text, (Match m) =>
                $"{(Player.Inventory.Items.Find((InventoryItem x) => x.Name == m.Groups[1].Value) ?? new InventoryItem()).Quantity}");

            text = new Regex(
                "{ITEM MAX:\\s*(.*?)}",     RegexOptions.IgnoreCase).Replace(text, (Match m) =>
                $"{(Player.Inventory.Items.Find((InventoryItem x) => x.Name == m.Groups[1].Value) ?? new InventoryItem()).MaxStack}");

            text = new Regex(
                "{REP XP:\\s*(.*?)}",       RegexOptions.IgnoreCase).Replace(text, (Match m) => 
                $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).Rep}/" +
                $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).RequiredRep}");

            text = new Regex(
                "{REP CURRENT:\\s*(.*?)}",  RegexOptions.IgnoreCase).Replace(text, (Match m) =>
                $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).Rep}");

            text = new Regex(
                "{REP REMAINING:\\s*(.*?)}",RegexOptions.IgnoreCase).Replace(text, (Match m) =>
                $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).RemainingRep}");

            text = new Regex(
                "{REP REQUIRED:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(text, (Match m) =>
                $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).RequiredRep}");

            text = new Regex(
                "{REP RANK:\\s*(.*?)}",     RegexOptions.IgnoreCase).Replace(text, (Match m) => 
                $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).Rank}");

            text = new Regex(
                "{REP TOTAL:\\s*(.*?)}",    RegexOptions.IgnoreCase).Replace(text, (Match m) => 
                $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).TotalRep}");

            text = new Regex(
                "{INT VALUE:\\s*(.*?)}",    RegexOptions.IgnoreCase).Replace(text, (Match m) =>
                $"{Configuration.Tempvalues[m.Groups[1].Value]}");

            text = text + "\r\n";
            if (Debug)
                if (Clear)
                    LogForm.Instance.txtLogDebug.Clear();
                else
                    LogForm.Instance.AppendDebug(text);
            else if (Clear)
                LogForm.Instance.txtLogScript.Clear();
            else
                LogForm.Instance.AppendScript(text);
        }

        public override string ToString()
        {
            string typetxt = Debug ? "Debug" : "Script";
            Clear = Text.Contains("{CLEAR}") ? true : false;
            return Clear ? $"Clear {typetxt}" : $"Log {typetxt}: {Text}";
        }
    }
}