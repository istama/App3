using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    class MouseHookBase
    {
        private NativeWindowMouseHook.LowLevelMouseProcDelegate _mouseHookProc;
        private IntPtr _hookId = IntPtr.Zero;

        public MouseHookBase()
        {
        }

        public void Hook()
        {
            if (_hookId == IntPtr.Zero)
            {
                _mouseHookProc = HookProcedure;
                using (var current_process = Process.GetCurrentProcess())
                {
                    using (ProcessModule module = current_process.MainModule)
                    {
                        _hookId = NativeWindowMouseHook.SetWindowsHookEx(
                            NativeWindowMouseHook.WH_MOUSE_LL,
                            _mouseHookProc,
                            NativeWindowMouseHook.GetModuleHandle(module.ModuleName),
                            0);
                    }
                }
            }
        }

        /// <summary>
        /// システムに登録したキーフックを解除する。
        /// </summary>
        public void UnHook()
        {
            NativeWindowMouseHook.UnhookWindowsHookEx(_hookId);
            _hookId = IntPtr.Zero;
        }

        /// <summary>
        /// キー入力されたときに呼び出されるメソッド。
        /// このメソッドをオーバーライドすることで自前のメソッドが呼び出されるようになる。
        /// </summary>
        protected virtual IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return NativeWindowMouseHook.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        
    }
}
