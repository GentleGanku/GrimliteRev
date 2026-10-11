using DarkUI.Forms;
using Grimoire.UI;
using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace Grimoire
{
    internal static class Program
    {
        public static string versionDate = "14072022";

        public static string flashVersion;

        public static bool privateBuild { get; set; } = false;

        public static bool access { get; set; }

        [STAThread]
        private static void Main()
        {
            access = Access();
            flashVersion = FlashVersion();
            if (privateBuild && !access)
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Access denied.", "Grimlite Rev v1.3", MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);
            Application.Run(new Root());
        }

        private static bool Access()
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            try
            {
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)");
                var result = client.GetStringAsync(privateBuild ? "https://gitfront.io/r/GentleGanku/75b44be5e2626c1af385e2911c99e94367a4b7df/GrimliteRev-Internal/" : "https://github.com/GentleGanku/GrimliteRev/releases/latest").Result;
                if (privateBuild)
                {
                    string title = Regex.Match(result, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                    return !title.Contains("GrimliteRev-Internal") ? false : true;
                }
                else
                {
                    return !result.Contains($"({versionDate})") ? false : true;
                }
            }
            catch
            {
                return privateBuild ? false : true;
            }
        }

        public static string FlashVersion()
        {
            var subKey = Registry.ClassesRoot.OpenSubKey(@"ShockwaveFlash.ShockwaveFlash\CurVer");
            if (subKey != null)
            {
                var value = subKey.GetValue(null) as String;
                return value;
            }
            return string.Empty;
        }

        public static async void CheckVersion()
        {
            if (!privateBuild && !access)
            {
                await Task.Delay(250);
                DialogResult result = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Outdated Build issue has been detected. Would you like to download the latest version?", $"Grimlite Rev v1.3 ({versionDate})", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    Process.Start("https://github.com/GentleGanku/GrimliteRev/releases/latest");
                }
            }
        }

        public static bool CheckFlashVersion()
        {
            if (flashVersion == string.Empty)
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "You may have to install the Adobe Flash Player plugin (for ActiveX) in order for the bot client to load the game.", $"Flash Player", MessageBoxIcon.Error);
                return true;
            }
            return false;
        }
    }
}