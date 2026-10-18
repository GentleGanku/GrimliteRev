using DarkUI.Forms;
using Grimoire.UI;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Grimoire.Utils
{
    /// <summary>
    /// One place for errors that used to be swallowed or crash the client: they go to the Log window (Debug tab),
    /// and the ones the user has to know about also show a popup.
    /// </summary>
    public static class ErrorReporter
    {
        #region Fields

        // A burst of errors (one per packet, one per repaint) would otherwise stack popups faster than they can be closed.
        private static int _popupOpen;

        #endregion

        #region Methods

        /// <summary>
        /// Writes the error to the Log window's Debug tab, or to the debugger output while the Log window doesn't exist
        /// yet (startup) or anymore (shutdown).
        /// </summary>
        public static void Log(string context, Exception ex)
        {
            string text = $"[{DateTime.Now:hh:mm:ss}] Error while {context}: {Describe(ex)}\r\n{ex?.StackTrace}\r\n";
            try
            {
                LogForm.Instance.AppendDebug(text);
            }
            catch (Exception logFailure)
            {
                Debug.WriteLine(text + logFailure);
            }
        }

        public static void Report(string context, Exception ex)
        {
            Log(context, ex);
            Show("Unexpected error", $"Something went wrong while {context}:\r\n\r\n{Describe(ex)}\r\n\r\nThe details are in the Log window (Debug tab).");
        }

        /// <summary>
        /// Logs a bot error, stops the bot the same way the Stop button does, and tells the user where it stopped.
        /// </summary>
        /// <param name="where">Completes "The bot stopped ...", e.g. "at index 4 (Kill: Frogzard)".</param>
        public static void BotFailed(string where, Exception ex)
        {
            Log($"running the bot {where}", ex);
            OnUiThread(() =>
            {
                if (BotManager.Instance.ActiveBotEngine.IsRunning)
                {
                    BotManager.Instance.btnBotStop_Click(null, EventArgs.Empty);
                }
            });
            Show("Bot stopped", $"The bot stopped {where} because of an error:\r\n\r\n{Describe(ex)}\r\n\r\nThe details are in the Log window (Debug tab).");
        }

        private static string Describe(Exception ex)
        {
            return ex == null ? "unknown error" : $"{ex.GetType().Name}: {ex.Message}";
        }

        private static void Show(string title, string message)
        {
            if (Interlocked.Exchange(ref _popupOpen, 1) == 1)
            {
                return;
            }
            OnUiThread(() =>
            {
                try
                {
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, message, title, MessageBoxIcon.Error);
                }
                finally
                {
                    Interlocked.Exchange(ref _popupOpen, 0);
                }
            });
        }

        private static void OnUiThread(Action action)
        {
            Form owner = Root.Instance;
            if (owner != null && owner.IsHandleCreated && owner.InvokeRequired)
            {
                owner.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        #endregion
    }
}
