using Grimoire.Game;
using Grimoire.Networking;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Map
{
    public class CmdJoin : IBotCommand
    {
        #region Properties

        public string Map
        {
            get;
            set;
        }

        public string Cell
        {
            get;
            set;
        }

        public string Pad
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Move;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
            string cmdMap = Map.Contains("-") ? Map.Split('-')[0] : Map;
            string roomSuffix = Map.Substring(cmdMap.Length);
            bool checkVar = instance.IsVar(roomSuffix.Replace("-", ""));
            if (roomSuffix.Contains("Packet"))
            {
                await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
                if (!instance.IsRunning || !Player.IsAlive || !Player.IsLoggedIn)
                {
                    return;
                }
                string username = Player.Username;
                await Proxy.Instance.SendToServer($"%xt%zm%cmd%1%tfer%{username}%{cmdMap}-100000");
                await instance.WaitUntil(() => !World.IsMapLoading, null, 40);
                await Task.Delay(1000);
            }
            if (!cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
            {
                if (checkVar)
                {
                    roomSuffix = "-" + Configuration.Tempvariable[instance.GetVar(roomSuffix.Replace("-", ""))];
                    await TryJoin(instance, cmdMap, roomSuffix);
                }
                else
                    await TryJoin(instance, cmdMap, roomSuffix);
            }
            if (cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
            {
                if (!Player.Cell.Equals(Cell, StringComparison.OrdinalIgnoreCase) && !roomSuffix.Contains("Packet"))
                {
                    Player.MoveToCell(Cell, Pad);
                    await Task.Delay(500);
                }
                BotData.BotMap = cmdMap;
                BotData.BotCell = Cell;
                BotData.BotPad = Pad;
            }
        }

        public async Task TryJoin(IBotEngine instance, string MapName, string RoomProp = "")
        {
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
            if (Player.CurrentState == Player.State.InCombat)
            {
                Player.MoveToCell(Player.Cell, Player.Pad);
                await Task.Delay(1250);
            }
            RoomProp = new Regex("-{1,}", RegexOptions.IgnoreCase).Replace(RoomProp, (Match m) => "-");
            RoomProp = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(RoomProp, (Match m) => new Random().Next(1001, 99999).ToString());
            Player.JoinMap(MapName + RoomProp, Cell, Pad);
            await instance.WaitUntil(() => Player.Map.Equals(MapName, StringComparison.OrdinalIgnoreCase), null, 5);
            await instance.WaitUntil(() => !World.IsMapLoading, null, 40);
        }

        public override string ToString()
        {
            return $"Join: {Map}, {Cell}, {Pad}";
        }

        #endregion
    }
}