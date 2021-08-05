using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Map
{
    public class CmdTravel : IBotCommand
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
            BotData.BotState = BotData.State.Others;
            await WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
            string cmdMap = Map.Contains("-") ? Map.Split('-')[0] : Map;
            string map = Player.Map;
            if (!cmdMap.Equals(map, StringComparison.OrdinalIgnoreCase))
            {
                await WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
                if (Player.CurrentState == Player.State.InCombat)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await WaitUntil(() => Player.CurrentState != Player.State.InCombat);
                }
                Player.JoinMap(Map, Cell, Pad);
                await WaitUntil(() => Player.Map.Equals(cmdMap, StringComparison.OrdinalIgnoreCase));
                await WaitUntil(() => !World.IsMapLoading, 40);
            }
        }

        private async Task WaitUntil(Func<bool> condition, int timeout = 15)
        {
            int iterations = 0;
            while (!condition() && (iterations < timeout || timeout == -1))
            {
                await Task.Delay(1000);
                iterations++;
            }
        }
    }
}