using DarkUI.Forms;
using Grimoire.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
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

        private static readonly string _repositoryParent = FindRepositoryParent();
        // A burst of errors (one per packet, one per repaint) would otherwise stack popups faster than they can be closed.
        private static int _popupOpen;

        #endregion

        #region Methods

        /// <summary>
        /// Shortens the source paths in a stack trace to start at the repository folder, so
        /// <c>C:\Projects\Freestyle\GrimliteRev\app\Game\World.cs</c> reads <c>GrimliteRev\app\Game\World.cs</c>.
        /// </summary>
        public static string TrimPaths(string text)
        {
            return string.IsNullOrEmpty(text) || string.IsNullOrEmpty(_repositoryParent) ? text : text.Replace(_repositoryParent, string.Empty);
        }

        /// <summary>
        /// Writes the error to the Log window's Debug tab, or to the debugger output while the Log window doesn't exist
        /// yet (startup) or anymore (shutdown).
        /// </summary>
        public static void Log(string context, Exception ex)
        {
            string text = $"[{DateTime.Now:hh:mm:ss}] Error while {context}: {Describe(ex)}\r\n{TrimPaths(ex?.StackTrace)}\r\n";
            try
            {
                LogForm.Instance.AppendDebug(text);
            }
            catch (Exception logFailure)
            {
                Debug.WriteLine(text + TrimPaths(logFailure.ToString()));
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

        /// <summary>
        /// Runs <paramref name="action"/> on the UI thread: posted through the main form from any other thread, so the
        /// caller doesn't wait. A control's own <c>InvokeRequired</c> isn't enough for forms that haven't been shown:
        /// with no handle it says false on every thread, and the control then gets its window on the calling thread.
        /// </summary>
        public static void OnUiThread(Action action)
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

        /// <summary>
        /// The folder that holds the repository on the machine that built the app, worked out from this file's build
        /// path (<c>&lt;parent&gt;\&lt;repository&gt;\app\Utils\ErrorReporter.cs</c>). Stack traces use the same build paths.
        /// </summary>
        private static string FindRepositoryParent([CallerFilePath] string thisFile = "")
        {
            string repository = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(thisFile)));
            string parent = string.IsNullOrEmpty(repository) ? null : Path.GetDirectoryName(repository);
            return string.IsNullOrEmpty(parent) ? null : parent.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
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

        #endregion
    }
}
