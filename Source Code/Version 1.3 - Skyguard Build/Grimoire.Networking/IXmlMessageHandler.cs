namespace Grimoire.Networking
{
    public interface IXmlMessageHandler
    {
        string[] HandledCommands
        {
            get;
        }

        void Handle(XmlMessage message);
    }
}