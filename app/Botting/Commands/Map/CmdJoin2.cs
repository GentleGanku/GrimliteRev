using Grimoire.UI;
using Grimoire.Game;
using Grimoire.Tools;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Map
{
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
