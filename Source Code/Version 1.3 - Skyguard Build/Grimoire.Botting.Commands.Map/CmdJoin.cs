using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Tools;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Map
{
    public class CmdJoin : IBotCommand
    {
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

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Move;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
            string cmdMap = Map.Contains("-") ? Map.Split('-')[0] : Map;
            string text = Map.Substring(cmdMap.Length);
            bool checkVar = instance.IsVar(text.Replace("-", ""));
            if (text.Contains("Packet"))
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
                if (text.Contains("Glitch"))
                {
                    int Max = 9999;
                    int Min = 9990;
                    if (text.Contains(":"))
                    {
                        Max = Convert.ToInt16(text.Split(':')[1]);
                        Min = Convert.ToInt16(text.Split(':')[2]);
                    }
                    while (!cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase) && Max >= Min)
                    {
                        if (!instance.IsRunning || !Player.IsAlive || !Player.IsLoggedIn)
                        {
                            return;
                        }
                        await TryJoin(instance, cmdMap, "-" + Max);
                        Max--;
                    }
                    if (!cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase) || (cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase) && World.PlayersInMap.Count < 2))
                    {
                        await TryJoin(instance, cmdMap);
                    }
                }
                else if (checkVar)
                {
                    text = "-" + Configuration.Tempvariable[instance.GetVar(text.Replace("-", ""))];
                    await TryJoin(instance, cmdMap, text);
                }
                else
                    await TryJoin(instance, cmdMap, text);
            }
            if (cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
            {
                if (!Player.Cell.Equals(Cell, StringComparison.OrdinalIgnoreCase) && !text.Contains("Packet"))
                {
                    Player.MoveToCell(Cell, Pad);
                    await Task.Delay(500);
                }
                World.SetSpawnPoint();
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
    }

    public class CmdJoin2 : IBotCommand
    {
        public string Map
        {
            get;
            set;
        }

        public string Room
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

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Move;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
            var map = instance.IsVar(Map) ? Configuration.Tempvariable[instance.GetVar(Map)] : Map;
            var room = instance.IsVar(Room) ? Configuration.Tempvariable[instance.GetVar(Room)] : Room;
            var cell = instance.IsVar(Cell) ? Configuration.Tempvariable[instance.GetVar(Cell)] : Cell;
            var pad = instance.IsVar(Pad) ? Configuration.Tempvariable[instance.GetVar(Pad)] : Pad;

            if (!map.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
            {
                await TryJoin(instance, map, room);
                if (map.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
                {
                    if (cell == "Blank")
                    {
                        cell = "Wait";
                    }
                    if (!Player.Cell.Equals(cell, StringComparison.OrdinalIgnoreCase))
                    {
                        Player.MoveToCell(cell, pad);
                        await Task.Delay(500);
                    }
                    World.SetSpawnPoint();
                    BotData.BotMap = map;
                    BotData.BotCell = cell;
                    BotData.BotPad = pad;
                }
            }
        }

        public async Task TryJoin(IBotEngine instance, string MapName, string RoomProp = "1e99")
        {
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
            var cell = instance.IsVar(Cell) ? Configuration.Tempvariable[instance.GetVar(Cell)] : Cell;
            if (cell == "Blank")
            {
                cell = "Wait";
            }
            var pad = instance.IsVar(Pad) ? Configuration.Tempvariable[instance.GetVar(Pad)] : Pad;
            if (Player.CurrentState == Player.State.InCombat)
            {
                Player.MoveToCell(Player.Cell, Player.Pad);
                await Task.Delay(2000);
            }
            RoomProp = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(RoomProp, (Match m) => new Random().Next(1001, 99999).ToString());
            Player.JoinMap($"{MapName}-{RoomProp}", cell, pad);
            await instance.WaitUntil(() => Player.Map.Equals(MapName, StringComparison.OrdinalIgnoreCase), null, 5);
            await instance.WaitUntil(() => !World.IsMapLoading, null, 10);
            if ((AutoRelogin.IsClientLoading("MapLoadingStuck")) || (AutoRelogin.IsClientLoading("MapLoadingError")))
            {
                World.ReloadCurrentMap();
                World.GameMessage("The map has been reloaded!");
            }
        }

        public override string ToString()
        {
            return $"Join: {Map}, {Room}, {Cell}, {Pad}";
        }
    }
}