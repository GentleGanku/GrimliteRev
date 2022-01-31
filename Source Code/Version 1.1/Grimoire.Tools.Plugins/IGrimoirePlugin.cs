namespace Grimoire.Tools.Plugins
{
    public interface IGrimoirePlugin
    {
        string Author
        {
            get;
        }

        string Description
        {
            get;
        }

        void Load();

        void Unload();
    }
}