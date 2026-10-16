namespace Grimoire.Tools.Plugins
{
    public interface IGrimoirePlugin
    {
        #region Properties

        string Author
        {
            get;
        }

        string Description
        {
            get;
        }

        #endregion

        #region Methods

        void Load();

        void Unload();

        #endregion
    }
}