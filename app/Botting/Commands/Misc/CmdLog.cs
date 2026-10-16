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
        #region Properties

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

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            string message = Text;
            message = message.Replace("{USERNAME}", Player.Username);
            message = message.Replace("{MAP}", Player.Map).Replace("{ROOM_ID}", World.RoomId.ToString()).Replace("{ROOM_NUM}", World.RoomNumber.ToString());
            message = message.Replace("{GOLD}", Player.Gold.ToString());
            message = message.Replace("{LEVEL}", Player.Level.ToString());
            message = message.Replace("{CELL}", Player.Cell).Replace("{PAD}", Player.Pad);
            message = message.Replace("{HEALTH}", Player.Health.ToString()).Replace("{MANA}", Player.Mana.ToString());
            message = message.Replace("{TIME}", $"{DateTime.Now:HH:mm:ss}");
            message = message.Replace("{TIME: 12}", $"{DateTime.Now:hh:mm:ss tt}");
            message = message.Replace("{TIME: 24}", $"{DateTime.Now:HH:mm:ss}");

            message = new Regex(
                "{ITEM:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(message, (Match m) =>
                $"{(Player.Inventory.Items.Find((InventoryItem x) => x.Name == m.Groups[1].Value) ?? new InventoryItem()).Quantity}");

            message = new Regex(
                "{ITEM MAX:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(message, (Match m) =>
            $"{(Player.Inventory.Items.Find((InventoryItem x) => x.Name == m.Groups[1].Value) ?? new InventoryItem()).MaxStack}");

            message = new Regex(
                "{REP XP:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(message, (Match m) =>
          $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).Rep}/" +
          $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).RequiredRep}");

            message = new Regex(
                "{REP CURRENT:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(message, (Match m) =>
               $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).Rep}");

            message = new Regex(
                "{REP REMAINING:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(message, (Match m) =>
                 $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).RemainingRep}");

            message = new Regex(
                "{REP REQUIRED:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(message, (Match m) =>
                $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).RequiredRep}");

            message = new Regex(
                "{REP RANK:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(message, (Match m) =>
            $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).Rank}");

            message = new Regex(
                "{REP TOTAL:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(message, (Match m) =>
             $"{(Player.Factions.Find((Faction x) => x.Name == m.Groups[1].Value) ?? new Faction()).TotalRep}");

            message = new Regex(
                "{INT VALUE:\\s*(.*?)}", RegexOptions.IgnoreCase).Replace(message, (Match m) =>
             $"{Configuration.Tempvalues[m.Groups[1].Value]}");

            message = message + "\r\n";
            if (Debug)
                if (Clear)
                    LogForm.Instance.txtLogDebug.Clear();
                else
                    LogForm.Instance.AppendDebug(message);
            else if (Clear)
                LogForm.Instance.txtLogScript.Clear();
            else
                LogForm.Instance.AppendScript(message);
        }

        public override string ToString()
        {
            string typetxt = Debug ? "Debug" : "Script";
            Clear = Text.Contains("{CLEAR}");
            return Clear ? $"Clear {typetxt}" : $"Log {typetxt}: {Text}";
        }

        #endregion
    }
}