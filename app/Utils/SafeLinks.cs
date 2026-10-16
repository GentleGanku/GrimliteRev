using Grimoire.UI;
using System;
using System.Diagnostics;

namespace Grimoire.Utils
{
    /// <summary>
    /// Opens links only when they are http or https addresses. Links come from bot descriptions and from the game
    /// client, so a <c>file:</c> URL, UNC path or program name could otherwise start a program (CR-02).
    /// </summary>
    public static class SafeLinks
    {
        #region Methods

        public static bool IsAllowed(string url, out Uri uri)
        {
            uri = null;
            return !string.IsNullOrWhiteSpace(url)
                && Uri.TryCreate(url.Trim(), UriKind.Absolute, out uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

        public static bool Open(string url)
        {
            if (!IsAllowed(url, out Uri uri))
            {
                LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Didn't open a link that isn't a web address: {url}\r\n");
                return false;
            }
            Process.Start(uri.AbsoluteUri);
            return true;
        }

        #endregion
    }
}
