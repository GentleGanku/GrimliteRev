using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Utils
{
    public static class TaskExtensions
    {
        #region Methods

        /// <summary>
        /// Lets <paramref name="task"/> run in the background without waiting for it. A failure is logged right away
        /// instead of surfacing later (or never) as an unobserved task exception.
        /// </summary>
        public static void Forget(this Task task, string context)
        {
            task.ContinueWith(t => ErrorReporter.Log(context, t.Exception?.GetBaseException()), CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
        }

        #endregion
    }
}
