
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace IsTama.Utils
{
	/// <summary>
	/// キーフックのベースとなるクラス。
	/// </summary>
	class KeyHookBase
	{
		private NativeWindowKeyHook.KeyboardProc _keyHookProc;
        private IntPtr _hookId = IntPtr.Zero;
        
		protected KeyHookBase()
		{
		}

        public void Hook()
        {
            if (_hookId == IntPtr.Zero)
            {
                _keyHookProc = HookProcedure;
                using (var current_process = Process.GetCurrentProcess())
                {
                    using (ProcessModule module = current_process.MainModule)
                    {
                        _hookId = NativeWindowKeyHook.SetWindowsHookEx(
                            NativeWindowKeyHook.WH_KEYBOARD_LL, 
                            _keyHookProc,
                            NativeWindowKeyHook.GetModuleHandle(module.ModuleName),
                            0);
                    }
                }
            }
        }

        /// <summary>
        /// システムに登録したキーフックを解除する。
        /// </summary>
        public void Unhook()
        {
            NativeWindowKeyHook.UnhookWindowsHookEx(_hookId);
            _hookId = IntPtr.Zero;
        }

        /// <summary>
        /// キー入力されたときに呼び出されるメソッド。
        /// このメソッドをオーバーライドすることで自前のメソッドが呼び出されるようになる。
        /// </summary>
        protected virtual IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return NativeWindowKeyHook.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
	}
}
