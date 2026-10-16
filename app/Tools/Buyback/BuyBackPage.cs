using HtmlAgilityPack;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Grimoire.Tools.Buyback
{
    public class BuyBackPage
    {
        private readonly HtmlDocument _doc;

        public const string EventTarget = "GridBuyBack";

        public const string Confirm = "YES%2c+GET+NOW+FOR+FREE";

        public string EventArgument
        {
            get
            {
                try
                {
                    string value = _doc.DocumentNode.SelectNodes("//input[@type]").First().Attributes["onclick"].Value;
                    return HttpUtility.UrlEncode(new Regex("BuyNow(\\$)\\d{1,2}", RegexOptions.IgnoreCase).Matches(value)[0].Value);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public string ViewState
        {
            get
            {
                try
                {
                    return HttpUtility.UrlEncode(_doc.DocumentNode.SelectSingleNode("//input[@id='__VIEWSTATE']").Attributes["value"].Value);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public string ViewStateGenerator
        {
            get
            {
                try
                {
                    return HttpUtility.UrlEncode(_doc.DocumentNode.SelectSingleNode("//input[@id='__VIEWSTATEGENERATOR']").Attributes["value"].Value);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public string EventValidation
        {
            get
            {
                try
                {
                    return HttpUtility.UrlEncode(_doc.DocumentNode.SelectSingleNode("//input[@id='__EVENTVALIDATION']").Attributes["value"].Value);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public BuyBackPage(string html)
        {
            _doc = new HtmlDocument();
            _doc.LoadHtml(html);
        }
    }
}