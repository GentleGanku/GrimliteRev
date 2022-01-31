using System.Xml;

namespace Grimoire.Networking
{
    public class XmlMessage : Message
    {
        public XmlDocument Body
        {
            get;
        }

        public XmlMessage(string raw)
        {
            try
            {
                RawContent = raw;
                Body = new XmlDocument();
                Body.LoadXml(raw);
                Command = raw.Contains("cross-domain-policy") ? "policy" : Body.DocumentElement?["body"]?.Attributes["action"]?.Value;
            }
            catch (XmlException)
            {
            }
        }

        public override string ToString()
        {
            return Body.OuterXml;
        }
    }
}