using Grimoire.Game;
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
            string TargetName = "";

            if ( IsVar(PlayerName) )
            {
                TargetName = Configuration.Tempvariable[GetVar(PlayerName)];
                Console.WriteLine("Using Variable Goto");
            }
            else
            {
                TargetName = PlayerName;
            }

            List<string> playersInMap = World.PlayersInMap;
            Player.GoToPlayer(TargetName);
            if (playersInMap.Any((string p) => p.Equals(TargetName, StringComparison.OrdinalIgnoreCase)))
            {
                await Task.Delay(500);
            }
            else
            {
                await instance.WaitUntil(() => World.PlayersInMap.Any((string p) => p.Equals(TargetName, StringComparison.OrdinalIgnoreCase)) && !World.IsMapLoading, null, 40);
            }
        }

        public override string ToString()
        {
            return "Goto player: " + PlayerName;
        }
    }
}