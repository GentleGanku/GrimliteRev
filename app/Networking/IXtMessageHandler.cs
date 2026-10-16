namespace Grimoire.Networking
{
    public interface IXtMessageHandler
    {
        #region Properties

        string[] HandledCommands
        {
            get;
        }

        #endregion

        #region Methods

        void Handle(XtMessage message);

        #endregion
    }
}