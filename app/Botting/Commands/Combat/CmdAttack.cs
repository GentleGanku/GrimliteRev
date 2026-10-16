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
            Player.AttackMonster(instance.Value(Monster));
        }

        public override string ToString()
        {
            return "Attack: " + Monster;
        }
    }
}