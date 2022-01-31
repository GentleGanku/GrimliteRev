using System;
using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire.Game;

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
            bool CheckVarMonster = instance.IsVar(Monster);

            if (instance.IsRunning && Player.IsAlive && Player.IsLoggedIn)
            {
                if (CheckVarMonster)
                    Player.AttackMonster(instance.IsVar(Monster) ? Configuration.Tempvariable[instance.GetVar(Monster)] : Monster);
                else
                    Player.AttackMonster(Monster);
            }
        }
        
        public override string ToString()
        {
            return "Attack: " + this.Monster;
        }
    }
}