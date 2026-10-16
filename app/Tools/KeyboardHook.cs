using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Grimoire.Tools
{
    public class KeyboardHook : IDisposable
    {
        #region Fields

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 256;
        private const int WM_SYSKEYDOWN = 260;
        public readonly List<Keys> TargetedKeys;
        private readonly CallbackDelegate hookCallback;
        private readonly IntPtr _hookId;

        #endregion

        #region Constructors

        private KeyboardHook()
        {
            hookCallback = HookProc;
            _hookId = SetWindowsHookEx(13, hookCallback, IntPtr.Zero, 0);
            TargetedKeys = new List<Keys>();
        }

        #endregion

        #region Delegates

        public delegate IntPtr CallbackDelegate(int code, IntPtr wParam, IntPtr lParam);

        #endregion

        #region Events

        public event Action<Keys> KeyDown;

        #endregion

        #region Properties

        public static KeyboardHook Instance
        {
            get;
        } = new KeyboardHook();

        #endregion

        #region Methods

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookId);
        }

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr SetWindowsHookEx(int idHook, CallbackDelegate lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

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

        #endregion
    }
}