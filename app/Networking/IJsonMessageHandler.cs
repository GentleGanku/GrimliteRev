namespace Grimoire.Networking
{
    public interface IJsonMessageHandler
    {
        #region Properties

        string[] HandledCommands
        {
            get;
        }

        #endregion

        #region Methods

        void Handle(JsonMessage message);

        #endregion
    }
}