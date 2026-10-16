using System;
using System.Windows.Forms;

namespace Grimoire.Utils
{
    public static class ControlUtils
    {
        public static bool CheckedInvoke(this Control c, Action a)
        {
            bool req = c.InvokeRequired;
            (req ? () => c.Invoke(a) : a)();
            return req;
        }
    }
}