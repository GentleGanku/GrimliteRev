using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Grimoire.Tools
{
    public class KeyboardHook : IDisposable
    {
        public delegate IntPtr CallbackDelegate(int code, IntPtr wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 256;

        private const int WM_SYSKEYDOWN = 260;

        private readonly CallbackDelegate hookCallback;

        public readonly List<Keys> TargetedKeys;

        private readonly IntPtr _hookId;

        public static KeyboardHook Instance
        {
            get;
        } = new KeyboardHook();

        public event Action<Keys> KeyDown;

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr SetWindowsHookEx(int idHook, CallbackDelegate lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        private KeyboardHook()
        {
            hookCallback = HookProc;
            _hookId = SetWindowsHookEx(13, hookCallback, IntPtr.Zero, 0);
            TargetedKeys = new List<Keys>();
        }

        private IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(_hookId, code, wParam, lParam);
            }
            if (wParam.ToInt32() == 256 || wParam.ToInt32() == 260)
            {
                Keys keys = (Keys)Marshal.ReadInt32(lParam);
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