namespace Grimoire.Networking
{
    public interface IXmlMessageHandler
    {
        #region Properties

        string[] HandledCommands
        {
            get;
        }

        #endregion

        #region Methods

        void Handle(XmlMessage message);

        #endregion
    }
}