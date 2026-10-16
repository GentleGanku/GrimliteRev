using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdUseSkill : IBotCommand
    {
        #region Properties

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
            BotData.BotState = BotData.State.Combat;

            if (this.Wait)
            {
                await Task.Delay(Player.SkillAvailable(this.Index));
            }
            if (Player.Health / (double)Player.HealthMax * 100.0 <= SafeHp)
            {
                if (Player.Mana / (double)Player.ManaMax * 100.0 <= SafeMp)
                {
                    if (this.Index != "5")
                    {
                        Player.AttackMonster("*");
                    }
                    Player.UseSkill(this.Index);
                }
            }
        }

        public override string ToString()
        {
            return "Skill " + this.Skill;
        }

        #endregion
    }
}