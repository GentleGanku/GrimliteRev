namespace Grimoire.Networking
{
    public interface IXtMessageHandler
    {
        string[] HandledCommands
        {
            get;
        }

        void Handle(XtMessage message);
    }
}