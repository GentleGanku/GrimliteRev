namespace Grimoire.Networking
{
    public interface IJsonMessageHandler
    {
        string[] HandledCommands
        {
            get;
        }

        void Handle(JsonMessage message);
    }
}