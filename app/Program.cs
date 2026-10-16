using DarkUI.Forms;
using Grimoire.UI;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace Grimoire
{
    internal static class Program
    {
        #region Fields

        public static string version = "1.4.0";
        public static string releaseDate = "14 Oct 2026";

        #endregion

        #region Methods

        public static async Task CheckForLatestBuild()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            bool isOutdated = false;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)");

                    var result = await client.GetStringAsync("https://github.com/GentleGanku/GrimliteRev/releases/latest");
                    isOutdated = !result.Contains($"v{version}");
                }
            }
            catch
            {
                return;
            }

            if (!isOutdated)
            {
                return;
            }
            
            DialogResult dialogResult = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "A new update for Grimlite Rev has been released! Would you like to download the latest version?", $"New update is available!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("https://github.com/GentleGanku/GrimliteRev/releases/latest");
            }
        }

        public static void CheckForFlashPlayer()
        {
            string flashVer = string.Empty;
            RegistryKey subKey = Registry.ClassesRoot.OpenSubKey(@"ShockwaveFlash.ShockwaveFlash\CurVer");
            
            if (subKey != null)
            {
                flashVer = subKey.GetValue(null).ToString();
            }
            else
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "You have to install the Adobe Flash Player plugin (for ActiveX) in order for the game screen to load.", $"Failed loading the game!", MessageBoxIcon.Error);
            }
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);
            Application.Run(new Root());
        }

        #endregion
    }
}