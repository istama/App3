
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static IsTama.Utils.NativeWindowKeyHook;

namespace IsTama.Utils
{
    /// <summary>
    /// キーフッククラス。
    /// </summary>
    class KeyHookPrimitive : KeyHookBase
    {
        public event EventHandler<KeyHookPrimitiveEventArgs> InputKeyDown;
        public event EventHandler<KeyHookPrimitiveEventArgs> InputKeyUp;

        public event EventHandler<KeyHookPrimitiveEventArgs> ModifierKeyDown;
        public event EventHandler<KeyHookPrimitiveEventArgs> ModifierKeyUp;

        public KeyHookPrimitive() : base()
        {
        }

        /// <summary>
        /// キーが押下されたイベントを通知する。
        /// argsを受け取ったハンドラがargsのHandledプロパティにtrueをセットすると、
        /// 次のウィンドウにキー入力を渡さないようにできる。
        /// </summary>
        protected virtual void OnInputKeyDown(KeyHookPrimitiveEventArgs e)
            => Volatile.Read(ref this.InputKeyDown)?.Invoke(this, e);
        protected virtual void OnInputKeyUp(KeyHookPrimitiveEventArgs e)
            => Volatile.Read(ref this.InputKeyUp)?.Invoke(this, e);

        protected virtual void OnModifierKeyDown(KeyHookPrimitiveEventArgs e)
            => Volatile.Read(ref this.ModifierKeyDown)?.Invoke(this, e);
        protected virtual void OnModifierKeyUp(KeyHookPrimitiveEventArgs e)
            => Volatile.Read(ref this.ModifierKeyUp)?.Invoke(this, e);

        /// <summary>
        /// キー入力されると呼び出される。
        /// </summary>
        protected override IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
                return base.HookProcedure(nCode, wParam, lParam);

            // OSから渡された入力キーのポインタをInputKeyかModifierKeysに変換する
            if (!TryConvertToKeyFrom(lParam, out var input_key, out var modifier_keys))
                return base.HookProcedure(nCode, wParam, lParam);

            var e = new KeyHookPrimitiveEventArgs(input_key, modifier_keys);

            if (IsWindowMessageKeyDown(wParam))
            {
                if (e.InputKey != InputKey.NONE)
                    OnInputKeyDown(e);
                else if (e.ModifierKeys != ModifierKeys.NONE)
                    OnModifierKeyDown(e);

                goto End;
            }

            if (IsWindowMessageKeyUp(wParam))
            {
                if (e.InputKey != InputKey.NONE)
                    OnInputKeyUp(e);
                else if (e.ModifierKeys != ModifierKeys.NONE)
                    OnModifierKeyUp(e);

                goto End;
            }

            End:
            
            // Handledがtrueにされたら次のアプリケーションにキー入力を渡さないようにする
            // ただし、キーイベントに時間がかかりすぎると（約300ミリ秒以上かかると）タイムアウトとなり、
            // ここでの処理に関係なく次のアプリケーションにキー入力が渡され、
            // さらにこのHookProcedure()がWindowsによってキーフックの設定から強制的に外される。
            if (e.Handled)
                return new IntPtr(-1);
            else
                return base.HookProcedure(nCode, wParam, lParam);
        }

        private bool TryConvertToKeyFrom(IntPtr lParam, out InputKey inputKey, out ModifierKeys modifierKeys)
        {
            var kb = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
            var key_code = (byte)(int)kb.vkCode;

            var converted_to_input_key = InputKeyConverters.TryConvertToInputKeyFrom(key_code, out inputKey);
            var converted_to_modifier_key = ModifierKeysConverters.TryConvertToModifierKeyFrom(key_code, out modifierKeys);

            return converted_to_input_key | converted_to_modifier_key;
        }

        private bool IsWindowMessageKeyDown(IntPtr wParam)
            => wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN;

        private bool IsWindowMessageKeyUp(IntPtr wParam)
            => wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP;
    }
}
