namespace Grimoire.Botting
{
    /// <summary>
    /// One set of client options: the main menu's Settings > Manipulation Options, or a bot's options in Bot Manager.
    /// <see cref="OptionsManager.Refresh"/> combines the two into what the client uses.
    /// </summary>
    public class ManipulationOptions
    {
        #region Properties

        public bool InfiniteRange
        {
            get;
            set;
        }

        public bool ProvokeMonsters
        {
            get;
            set;
        }

        public bool EnemyMagnet
        {
            get;
            set;
        }

        public bool LagKiller
        {
            get;
            set;
        }

        public bool SkipCutscenes
        {
            get;
            set;
        }

        public int WalkSpeed
        {
            get;
            set;
        } = 8;

        #endregion
    }
}
