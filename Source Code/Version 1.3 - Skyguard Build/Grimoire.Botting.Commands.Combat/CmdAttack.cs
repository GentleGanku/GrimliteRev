using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdAttack : IBotCommand
    {
        public string Monster
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            if (instance.IsRunning && Player.IsAlive && Player.IsLoggedIn)
            {
                Player.AttackMonster(instance.IsVar(Monster) ? Configuration.Tempvariable[instance.GetVar(Monster)] : Monster);
            }
        }

        public override string ToString()
        {
            return "Attack: " + this.Monster;
        }
    }
}