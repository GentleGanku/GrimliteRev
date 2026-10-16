using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Grimoire.Tools.Buyback
{
    public class AutoBuyBack : IDisposable
    {
        private const string UrlBuyBack = "inventory.aspx?tab=buyback";

        private readonly HttpClient _client;

        protected internal string Username
        {
            get
            {
                return Flash.Call<string>("Username", new string[0]);
            }
        }

        protected internal string Password
        {
            get
            {
                return Flash.Call<string>("Password", new string[0]);
            }
        }

        public AutoBuyBack()
        {
            _client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                CookieContainer = new CookieContainer()
            })
            {
                BaseAddress = new Uri("https://account.aq.com")
            };
        }

        public async Task Perform(string item, int pageCap)
        {
            string lastRequestedPage;
            string[] array;
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(lastRequestedPage = await SendPost(string.Empty, "uuu=" + Username + "&pps=" + Password + "&submit=")) && (array = await GetItemHtml(lastRequestedPage, item, pageCap)).Length >= 2)
            {
                BuyBackPage buyBackPage = new BuyBackPage(array[0]);
                BuyBackPage buyBackPage2 = new BuyBackPage(array[1]);
                string postData = "__EVENTTARGET=GridBuyBack&__EVENTARGUMENT=" + buyBackPage.EventArgument + "&__VIEWSTATE=" + buyBackPage2.ViewState + "&__VIEWSTATEGENERATOR=" + buyBackPage2.ViewStateGenerator + "&__VIEWSTATEENCRYPTED=&__EVENTVALIDATION=" + buyBackPage2.EventValidation;
                string html;
                if (!string.IsNullOrEmpty(html = await SendPost("inventory.aspx?tab=buyback", postData)))
                {
                    BuyBackPage buyBackPage3 = new BuyBackPage(html);
                    string postData2 = "__VIEWSTATE=" + buyBackPage3.ViewState + "&__VIEWSTATEGENERATOR=" + buyBackPage3.ViewStateGenerator + "&__VIEWSTATEENCRYPTED=&__EVENTVALIDATION=" + buyBackPage3.EventValidation + "&btnConfirmYes=YES%2c+GET+NOW+FOR+FREE";
                    await SendPost("inventory.aspx?tab=buyback", postData2);
                }
            }
        }

        private async Task<string[]> GetItemHtml(string lastRequestedPage, string item, int cap)
        {
            string[] ret = new string[2];
            for (int i = 1; i <= cap; i++)
            {
                BuyBackPage buyBackPage = new BuyBackPage(lastRequestedPage);
                string postData = string.Format("__EVENTTARGET={0}&__EVENTARGUMENT=Page%24{1}&", "GridBuyBack", i) + "__VIEWSTATE=" + buyBackPage.ViewState + "&__VIEWSTATEGENERATOR=" + buyBackPage.ViewStateGenerator + "&__VIEWSTATEENCRYPTED=&__EVENTVALIDATION=" + buyBackPage.EventValidation;
                string page = await SendPost("inventory.aspx?tab=buyback", postData);
                lastRequestedPage = page;
                string[] lines = page.Split('\n');
                foreach (string line in lines)
                {
                    if (line.IndexOf(item, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        ret[0] = line;
                        ret[1] = page;
                        return ret;
                    }
                }
            }
            return ret;
        }

        private async Task<string> SendPost(string url, string postData)
        {
            try
            {
                return HttpUtility.HtmlDecode(await _client.PostAsync(url, new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded")).Result.Content.ReadAsStringAsync());
            }
            catch
            {
                return string.Empty;
            }
        }

        private async Task<string> SendGet(string url)
        {
            try
            {
                return HttpUtility.HtmlDecode(await _client.GetStringAsync(url));
            }
            catch
            {
                return string.Empty;
            }
        }

        public void Dispose()
        {
            SendGet("logout.aspx").Wait();
            _client.Dispose();
        }
    }
}