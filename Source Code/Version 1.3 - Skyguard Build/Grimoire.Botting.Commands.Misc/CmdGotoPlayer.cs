using Grimoire.Game;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdGotoPlayer : RegularExpression, IBotCommand
    {
        public string PlayerName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            string player = instance.IsVar(PlayerName) ? Configuration.Tempvariable[instance.GetVar(PlayerName)] : PlayerName;
            List<string> playersInMap = World.PlayersInMap;
            Player.GoToPlayer(player);
            if (playersInMap.Any((string p) => p.Equals(player, StringComparison.OrdinalIgnoreCase)))
            {
                await Task.Delay(500);
            }
            await instance.WaitUntil(() => !World.IsMapLoading, null, 10);
            if ((AutoRelogin.IsClientLoading("MapLoadingStuck")) || (AutoRelogin.IsClientLoading("MapLoadingError")))
            {
                World.ReloadCurrentMap();
                World.GameMessage("The map has been reloaded!");
            }
            World.SetSpawnPoint();
            BotData.BotMap = Player.Map;
            BotData.BotCell = Player.Cell;
            BotData.BotPad = Player.Pad;
        }

        public override string ToString()
        {
            return "Goto player: " + PlayerName;
        }
    }
}