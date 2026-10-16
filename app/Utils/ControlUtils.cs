using System;
using System.Windows.Forms;

namespace Grimoire.Utils
{
    public static class ControlUtils
    {
        #region Methods

        /// <summary>
        /// Appends <paramref name="text"/> and, once the box holds more than <paramref name="maxLength"/> characters, drops
        /// the oldest lines down to half of that. Logs then stay fast and small over long sessions; the newest lines remain.
        /// </summary>
        public static void AppendCapped(this TextBoxBase box, string text, int maxLength = 500000)
        {
            box.AppendText(text);
            if (box.TextLength <= maxLength)
            {
                return;
            }
            string all = box.Text;
            int cut = all.IndexOf('\n', all.Length - maxLength / 2);
            box.Text = cut < 0 ? string.Empty : all.Substring(cut + 1);
            box.SelectionStart = box.TextLength;
            box.ScrollToCaret();
        }

        public static bool CheckedInvoke(this Control c, Action a)
        {
            bool req = c.InvokeRequired;
            (req ? () => c.Invoke(a) : a)();
            return req;
        }

        #endregion
    }
}