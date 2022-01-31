using Grimoire.Tools;

namespace Grimoire.Game.Data
{
    public class Skill
    {
        public enum SkillType
        {
            Normal,
            Safe,
            Label
        }

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


        public static string GetSkillName(string index)
        {
            return Flash.Call<string>("GetSkillName", new string[1]
            {
                index
            });
        }
    }
}