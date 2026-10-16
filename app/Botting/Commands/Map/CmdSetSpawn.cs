using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Map
{
    public class CmdSetSpawn : IBotCommand
    {
        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            Player.SetSpawnPoint();
        }

        public override string ToString()
        {
            return "Set Spawnpoint";
        }

        #endregion
    }
}