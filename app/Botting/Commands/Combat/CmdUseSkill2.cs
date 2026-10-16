using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdUseSkill2 : IBotCommand
    {
        #region Properties

        public string Monster
        {
            get;
            set;
        }

        public string Skill
        {
            get;
            set;
        }

        public string Index
        {
            get;
            set;
        }

        public int SafeHp
        {
            get;
            set;
        }

        public int SafeMp
        {
            get;
            set;
        }

        public bool Wait
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            var monster = instance.Value(Monster);
            if (!BotUtilities.ShouldUseSkill && (monster != "Self-targeted"))
            {
                return;
            }
            BotData.BotState = BotData.State.Combat;
            bool onSafeHp = SafeHp != 100;
            bool onSafeMp = SafeMp != 100;
            bool use = onSafeHp ? (Player.Health / (double)Player.HealthMax * 100.0 <= SafeHp) : (onSafeMp ? (Player.Mana / (double)Player.ManaMax * 100.0 <= SafeMp) : false);
            if (Wait)
            {
                await Task.Delay(Player.SkillAvailable(Index));
            }
            if ((onSafeHp || onSafeMp) && !use)
            {
                return;
            }
            if ((Index != "5") || (monster != "Self-targeted"))
            {
                Player.AttackMonster(World.IsMonsterAvailable(monster) ? monster : "*");
            }
            Player.UseSkill(Index);
        }

        public override string ToString()
        {
            return "Skill " + Skill;
        }

        #endregion
    }
}
