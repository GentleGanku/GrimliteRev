using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Grimoire.Tools
{
    public class KeyboardHook : IDisposable
    {
        public delegate int CallbackDelegate(int code, int wParam, int lParam);

        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 256;

        private const int WM_SYSKEYDOWN = 260;

        private readonly CallbackDelegate hookCallback;

        public readonly List<Keys> TargetedKeys;

        private readonly int _hookId;

        public static KeyboardHook Instance
        {
            get;
        } = new KeyboardHook();

        public event Action<Keys> KeyDown;

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, CallbackDelegate lpfn, int hInstance, int threadId);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, int lParam);

        private KeyboardHook()
        {
            hookCallback = HookProc;
            _hookId = SetWindowsHookEx(13, hookCallback, 0, 0);
            TargetedKeys = new List<Keys>();
        }

        private int HookProc(int code, int wParam, int lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(_hookId, code, wParam, lParam);
            }
            if (wParam == 256 || wParam == 260)
            {
                Keys keys = (Keys)Marshal.ReadInt32((IntPtr)lParam);
                if (TargetedKeys.Contains(keys))
                {
                    this.KeyDown?.Invoke(keys);
                }
            }
            return CallNextHookEx(_hookId, code, wParam, lParam);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookId);
        }
    }
}