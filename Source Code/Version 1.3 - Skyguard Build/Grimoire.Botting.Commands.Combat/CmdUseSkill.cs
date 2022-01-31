using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdUseSkill : IBotCommand
    {
        public string Skill { get; set; }

        public string Index { get; set; }

        public int SafeHp { get; set; }

        public int SafeMp { get; set; }

        public bool Wait { get; set; }

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
    }

    public class CmdUseSkill2 : IBotCommand
    {
        public string Monster { get; set; }

        public string Skill { get; set; }

        public string Index { get; set; }

        public int SafeHp { get; set; }

        public int SafeMp { get; set; }

        public bool Wait { get; set; }

        public async Task Execute(IBotEngine instance)
        {
            string MonsterName = instance.IsVar(Monster) ? Configuration.Tempvariable[instance.GetVar(Monster)] : Monster;
            if (!BotUtilities.ShouldUseSkill && (MonsterName != "Self-targeted"))
            {
                return;
            }
            bool safeHpCheck = (SafeHp == 100) ? false : true;
            bool safeMpCheck = (SafeMp == 100) ? false : true;
            BotData.BotState = BotData.State.Combat;
            if (this.Wait)
            {
                await Task.Delay(Player.SkillAvailable(this.Index));
            }
            if (!safeHpCheck && !safeMpCheck)
            {
                if ((this.Index != "5") || (MonsterName != "Self-targeted"))
                {
                    if (World.IsMonsterAvailable(MonsterName))
                    {
                        Player.AttackMonster(MonsterName);
                    }
                    else
                    {
                        Player.AttackMonster("*");
                    }
                }
                Player.UseSkill(this.Index);
            }
            else if (safeHpCheck)
            {
                if (Player.Health / (double)Player.HealthMax * 100.0 <= SafeHp)
                {
                    if ((this.Index != "5") || (MonsterName != "Self-targeted"))
                    {
                        if (World.IsMonsterAvailable(MonsterName))
                        {
                            Player.AttackMonster(MonsterName);
                        }
                        else
                        {
                            Player.AttackMonster("*");
                        }
                    }
                    Player.UseSkill(this.Index);
                }
            }
            else if (safeMpCheck)
            {
                if (Player.Mana / (double)Player.ManaMax * 100.0 <= SafeMp)
                {
                    if ((this.Index != "5") || (MonsterName != "Self-targeted"))
                    {
                        if (World.IsMonsterAvailable(MonsterName))
                        {
                            Player.AttackMonster(MonsterName);
                        }
                        else
                        {
                            Player.AttackMonster("*");
                        }
                    }
                    Player.UseSkill(this.Index);
                }
            }
        }

        public override string ToString()
        {
            return "Skill " + this.Skill;
        }
    }
}