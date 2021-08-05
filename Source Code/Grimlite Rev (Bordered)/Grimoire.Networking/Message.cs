namespace Grimoire.Networking
{
    public class Message
    {
        public string RawContent
        {
            get;
            set;
        }

        public string Command
        {
            get;
            set;
        }

        public bool Send
        {
            get;
            set;
        } = true;
    }
}