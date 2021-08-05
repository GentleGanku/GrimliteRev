using System;
using System.Collections.Generic;

namespace Grimoire.Utils
{
    public class TimeLimiter
    {
        private Dictionary<string, int> _last = new Dictionary<string, int>();

        public bool LimitedRun(string name, int delay, Action action)
        {
            bool run = !_last.TryGetValue(name, out int time) || Environment.TickCount - time >= delay;
            if (run)
            {
                action();
                _last[name] = Environment.TickCount;
            }
            return run;
        }
    }
}