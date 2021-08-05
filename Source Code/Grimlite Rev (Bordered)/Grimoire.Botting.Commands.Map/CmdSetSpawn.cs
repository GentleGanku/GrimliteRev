using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Map
{
    public class CmdSetSpawn : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
            Player.SetSpawnPoint();
        }

        public override string ToString()
        {
            return "Set Spawnpoint";
        }
    }
}