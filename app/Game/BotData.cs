using System.Collections.Generic;

namespace Grimoire.Game
{
    public static class BotData
    {
        #region Enums

        public enum State
        {
            Others,
            Transaction,
            Move,
            Combat,
            Rest,
            Quest
        }

        #endregion

        #region Properties

        public static State BotState
        {
            get;
            set;
        } = State.Others;

        public static string BotMap
        {
            get;
            set;
        } = null;

        public static string BotCell
        {
            get;
            set;
        } = null;

        public static string BotPad
        {
            get;
            set;
        } = null;

        public static string BotSkill
        {
            get;
            set;
        } = null;

        public static Dictionary<string, int> SkillSet
        {
            get;
            set;
        } = new Dictionary<string, int>();

        public static List<int> BotQuests
        {
            get;
            set;
        } = new List<int>();

        #endregion
    }
}