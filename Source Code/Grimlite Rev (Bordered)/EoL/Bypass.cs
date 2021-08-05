using System;
using System.Runtime.InteropServices;
using EasyHook;

namespace EoL
{
    public class Bypass
    {
        private static LocalHook _hook;

        public struct _SYSTEMTIME
        {
            public ushort wYear;
            
            public ushort wMonth;
            
            public ushort wDayOfWeek;
            
            public ushort wDay;
            
            public ushort wHour;
            
            public ushort wMinute;
            
            public ushort wSecond;
            
            public ushort wMilliseconds;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate void GetSystemTimeDelegate(IntPtr lpSystemTime);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern void GetSystemTime(IntPtr lpSystemTime);

        private unsafe static void GetSystemTimeHooked(IntPtr lpSystemTime)
        {
            GetSystemTime(lpSystemTime);
            _SYSTEMTIME* ptr = (_SYSTEMTIME*)((void*)lpSystemTime);
            ptr->wYear = 2020;
        }

        public static bool IsHooked
        {
            get
            {
                return Bypass._hook != null;
            }
        }

        public static void Hook()
        {
            _hook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "GetSystemTime"), new GetSystemTimeDelegate(GetSystemTimeHooked), null);
            _hook.ThreadACL.SetInclusiveACL(new int[1]);
            
        }

        public static void Unhook()
        {
            _hook.Dispose();
        }
    }
}