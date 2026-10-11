namespace Grimoire.Networking
{
    public class XtMessage : Message
    {
        public string[] Arguments
        {
            get;
            set;
        }

        public XtMessage(string raw)
        {
            if (raw != null)
            {
                RawContent = raw;
                if ((Arguments = raw.Split('%')).Length >= 4)
                {
                    Command = Arguments[2] != "zm" ? Arguments[2] : (Arguments[3] == "cmd" ? Arguments[5] : Arguments[3]);
                }
            }
        }

        public override string ToString()
        {
            return string.Join("%", Arguments);
        }
    }
}