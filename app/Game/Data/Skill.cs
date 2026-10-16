using Grimoire.Tools;

namespace Grimoire.Game.Data
{
    public class Skill
    {
        #region Enums

        public enum SkillType
        {
            Normal,
            Safe,
            Label
        }

        #endregion

        #region Properties

        public string Text
        {
            get;
            set;
        }

        public SkillType Type
        {
            get;
            set;
        }

        public string Index
        {
            get;
            set;
        }

        public int SafePercentage
        {
            get;
            set;
        }

        public bool SafeHp
        {
            get;
            set;
        }

        public bool SafeMp
        {
            get;
            set;
        }

        public bool ComparisonType
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public static string GetSkillName(string index)
        {
            return Flash.Call<string>("GetSkillName", new string[1]
            {
                index
            });
        }

        public void UseSafeSkill()
        {
            double Percentage = SafeHp ? (Player.Health / (double)Player.HealthMax * 100.0) : (SafeMp ? (Player.Mana / (double)Player.ManaMax * 100.0) : SafePercentage);
            bool Use = ComparisonType ? (Percentage >= SafePercentage) : (Percentage <= SafePercentage);
            if (Use)
            {
                Player.UseSkill(Index);
            }
        }

        #endregion
    }
}