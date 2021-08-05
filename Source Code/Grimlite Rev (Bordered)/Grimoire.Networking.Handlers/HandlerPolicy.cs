using System.Xml;

namespace Grimoire.Networking.Handlers
{
    public class HandlerPolicy : IXmlMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "policy"
        };

        public void Handle(XmlMessage message)
        {
            XmlElement xmlElement = message.Body?["cross-domain-policy"]?["allow-access-from"];
            if (xmlElement != null)
            {
                xmlElement.Attributes["to-ports"].Value = Proxy.Instance.ListenerPort.ToString();
            }
        }
    }
}