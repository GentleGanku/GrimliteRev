using Grimoire.UI;
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

    public class CmdJoin2 : IBotCommand
    {
        #region Properties

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

        private string _map
        {
            get
            {
                return BotManager.Instance.ActiveBotEngine.Value(Map);
            }
        }

        private string _room
        {
            get
            {
                return (BotManager.Instance.ActiveBotEngine.Value(Room) != "1e99") ? BotManager.Instance.ActiveBotEngine.Value(Room) : new Random().Next(1001, 99999).ToString();
            }
        }

        private string _cell
        {
            get
            {
                return BotManager.Instance.ActiveBotEngine.Value(Cell);
            }
        }

        private string _pad
        {
            get
            {
                return BotManager.Instance.ActiveBotEngine.Value(Pad);
            }
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Move;
            await JoinMap(instance);
            await JoinCell(instance);
            BotData.BotMap = _map;
            BotData.BotCell = _cell;
            BotData.BotPad = _pad;
        }

        public async Task JoinMap(IBotEngine instance)
        {
            if (!Player.Map.Equals(_map, StringComparison.OrdinalIgnoreCase))
            {
                await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer), null, 10, 500);
                await BotUtilities.MoveOutOfCombat();
                Player.JoinMap($"{_map}-{_room}", _cell, _pad);
                await instance.WaitUntil(() => Player.Map.Equals(_map, StringComparison.OrdinalIgnoreCase), null, 10, 500);
                await instance.WaitUntil(() => !World.IsMapLoading, null, 10, 500);
                if (AutoRelogin.IsClientLoading("MapLoadingStuck") || AutoRelogin.IsClientLoading("MapLoadingError"))
                {
                    World.ReloadCurrentMap();
                }
            }
        }

        public async Task JoinCell(IBotEngine instance)
        {
            if (Player.Map.Equals(_map, StringComparison.OrdinalIgnoreCase) && !Player.Cell.Equals(_cell, StringComparison.OrdinalIgnoreCase))
            {
                Player.MoveToCell(_cell, _pad);
            }
        }

        public override string ToString()
        {
            return $"Join: {Map}, {Room}, {Cell}, {Pad}";
        }

        #endregion
    }
}