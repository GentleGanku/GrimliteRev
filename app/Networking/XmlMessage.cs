using System.Xml;

namespace Grimoire.Networking
{
    public class XmlMessage : Message
    {
        #region Constructors

        /// <summary>
        /// Parses <paramref name="raw"/>. For text that isn't XML, Command stays null, so no XML handler matches the
        /// message.
        /// </summary>
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

        #endregion

        #region Properties

        public XmlDocument Body
        {
            get;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Body.OuterXml;
        }

        #endregion
    }
}