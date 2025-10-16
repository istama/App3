using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    static class Keyboard
    {
        /// <summary>
        /// IMEを英数字入力にする。
        /// </summary>
        public static void SetImeOff()
        {
            // IMEモードをoffにする仮想キーをプレスするとIMEが英数字モードになる
            // ※この方法だと、IMEの状態を元に戻す処理はできない
            // IMEの状態を元に戻すためには、事前にIMEの状態を調べて保持しておく必要があるが、
            // そのための機能である、IMEのWin32APIやCOMを用いたIME操作（TSF）はなぜか上手く動作しない
            PressNormalKey(NativeWindowKeyInput.VK_IME_OFF);
            ReleaseNormalKey(NativeWindowKeyInput.VK_IME_OFF);
        }

        /// <summary>
        /// キーを押した状態にする。
        /// </summary>
        public static void PressInputKey(InputKey key)
        {
            // SHIFTとの組み合わせる可能性があるので拡張キーでプレスする
            if (key.IsEditKey() || key.IsCursorKey())
                PressExtendedKey(key);
            else
                PressNormalKey(key);
        }

        /// <summary>
        /// キーを離した状態にする。
        /// </summary>
        public static void ReleaseInputKey(InputKey key)
        {
            if (key.IsEditKey() || key.IsCursorKey())
                ReleaseExtendedKey(key);
            else
                ReleaseNormalKey(key);
        }

        /// <summary>
        /// フラグが立っている修飾キーをプレス状態にする。
        /// このメソッドは修飾キーの左右を区別しない値が送信される。
        /// このメソッドは物理キーの状態とは関係なく、プログラム的に修飾キーをプレスしたいときに使用する。
        /// ここでプレスしたキーは、物理キーを離してもリリース状態にならないことがある。
        /// </summary>
        public static void PressVirtualModifierKeys(ModifierKeys key_state)
        {
            /* Shift + カーソルキー のキー送信で文字列を選択状態にするにはExtendedKeyフラグが必要 */
            if (key_state.HasShift()) PressExtendedKey(ModifierKeys.VSHIFT);
            if (key_state.HasCtrl()) PressExtendedKey(ModifierKeys.VCTRL);
            if (key_state.HasAlt()) PressExtendedKey(ModifierKeys.VALT);
        }

        /// <summary>
        /// フラグが立っている修飾キーをリリース状態にする。
        /// このメソッドは修飾キーの左右を区別しない値が送信される。
        /// このメソッドは物理キーの状態とは関係なく、PressModifierKeys()でプレスしたキーをリリースするときに使用する。
        /// 物理キーが押されているときにこのメソッドでリリースしてもリリース状態にならないことがある。
        /// 物理キーとは関係なくプログラム的にキー状態を制御したいときに使用するメソッド。
        /// </summary>
        public static void ReleaseVirtualModifierKeys(ModifierKeys key_state)
        {
            /* ExtendedKeyフラグを付けてプレスしたキーをリリースするには同様にExtendedKeyフラグが必要 */
            if (key_state.HasShift()) ReleaseExtendedKey(ModifierKeys.VSHIFT);
            if (key_state.HasCtrl()) ReleaseExtendedKey(ModifierKeys.VCTRL);
            if (key_state.HasAlt()) ReleaseExtendedKey(ModifierKeys.VALT);
        }

        /// <summary>
        /// フラグが立っている修飾キーをプレス状態にする。
        /// このメソッドは修飾キーの左右を区別したキーを送信する。
        /// このメソッドは物理的なキーが押されているときと同じ状態にしたいときに使用する。
        /// このメソッドでプレスしたキーは、物理キーを離すとリリース状態になる。
        /// </summary>
        public static void PressPhysicalModifierKeys(ModifierKeys key_state)
        {
            /* 左の修飾キーはExtendedKeyフラグを付けてプレスすると、物理キーを離してもリリース状態にならない */
            if (key_state.HasLShift()) PressNormalKey(ModifierKeys.LSHIFT);
            if (key_state.HasLCtrl()) PressNormalKey(ModifierKeys.LCTRL);
            if (key_state.HasLAlt()) PressNormalKey(ModifierKeys.LALT);
            if (key_state.HasLWin()) PressNormalKey(ModifierKeys.LWIN);

            /* 一方、右の修飾キーはExtendedKeyフラグを付けてプレスしないと、物理キーを離したときリリース状態にならない */
            if (key_state.HasRShift()) PressExtendedKey(ModifierKeys.RSHIFT);
            if (key_state.HasRCtrl()) PressExtendedKey(ModifierKeys.RCTRL);
            if (key_state.HasRAlt()) PressExtendedKey(ModifierKeys.RALT);
            if (key_state.HasRWin()) PressExtendedKey(ModifierKeys.RWIN);
        }

        /// <summary>
        /// フラグが立っている修飾キーをリリース状態にする。
        /// このメソッドは修飾キーの左右を区別したキーを送信する。
        /// このメソッドは物理的なキーが押されている状態をリリースしたいときに使用する。
        /// このメソッドでリリースしたキーは、物理キーを押していてもリリース状態になる。
        /// </summary>
        public static void ReleasePhysicalModifierKeys(ModifierKeys key_state)
        {
            /* ExtendedKeyフラグを付けずにプレスしたキーをリリースするときは、同様にExtendedKeyフラグは不要 */
            if (key_state.HasLShift()) ReleaseNormalKey(ModifierKeys.LSHIFT);
            if (key_state.HasLCtrl()) ReleaseNormalKey(ModifierKeys.LCTRL);
            if (key_state.HasLAlt()) ReleaseNormalKey(ModifierKeys.LALT);
            if (key_state.HasLWin()) ReleaseNormalKey(ModifierKeys.LWIN);

            if (key_state.HasRShift()) ReleaseExtendedKey(ModifierKeys.RSHIFT);
            if (key_state.HasRCtrl()) ReleaseExtendedKey(ModifierKeys.RCTRL);
            if (key_state.HasRAlt()) ReleaseExtendedKey(ModifierKeys.RALT);
            if (key_state.HasRWin()) ReleaseExtendedKey(ModifierKeys.RWIN);
        }

        /// <summary>
        /// 現在、指定したキーが押されているならtrueを返す。
        /// ただし、プログラムからリリース命令を出したなら、物理的に押した状態であっても、
        /// キーが押されているとは判定されない。
        /// </summary>
        public static bool IsPressingKey(InputKey key)
        {
            var code = key.ToVirtualKeyCode();
            var result = NativeWindowKeyInput.GetAsyncKeyState(code);
            return (result & 0x8000) != 0;
        }

        public static bool IsPressingModifierKey(ModifierKeys key)
        {
            var code = key.ToVirtualKeyCode();
            var result = NativeWindowKeyInput.GetAsyncKeyState(code);
            return (result & 0x8000) != 0;
        }

        private static void PressNormalKey(InputKey key)
        {
            PressNormalKey(key.ToVirtualKeyCode());
        }

        private static void PressNormalKey(ModifierKeys key)
        {
            PressNormalKey(key.ToVirtualKeyCode());
        }

        private static void ReleaseNormalKey(InputKey key)
        {
            ReleaseNormalKey(key.ToVirtualKeyCode());
        }

        private static void ReleaseNormalKey(ModifierKeys key)
        {
            ReleaseNormalKey(key.ToVirtualKeyCode());
        }

        private static void PressExtendedKey(InputKey key)
        {
            PressExtendedKey(key.ToVirtualKeyCode());
        }

        private static void PressExtendedKey(ModifierKeys key)
        {
            PressExtendedKey(key.ToVirtualKeyCode());
        }

        private static void ReleaseExtendedKey(InputKey key)
        {
            ReleaseExtendedKey(key.ToVirtualKeyCode());
        }

        private static void ReleaseExtendedKey(ModifierKeys key)
        {
            ReleaseExtendedKey(key.ToVirtualKeyCode());
        }

        private static void PressNormalKey(byte vk_code)
        {
            // 仮想キーコードを物理キーのIDに変換する
            var scan_code = NativeWindowKeyInput.MapVirtualKey(vk_code, 0);
            var flags = (uint)0;
            NativeWindowKeyInput.keybd_event(vk_code, scan_code, flags, UIntPtr.Zero);
        }

        private static void ReleaseNormalKey(byte vk_code)
        {
            var scan_code = NativeWindowKeyInput.MapVirtualKey(vk_code, 0);
            var flags = (uint)NativeWindowKeyInput.KEYEVENTF_KEYUP;
            NativeWindowKeyInput.keybd_event(vk_code, scan_code, flags, UIntPtr.Zero);
        }

        private static void PressExtendedKey(byte vk_code)
        {
            var scan_code = NativeWindowKeyInput.MapVirtualKey(vk_code, 0);
            // EXTENDEDKEYフラグが必要な状況は今のところ以下の通り
            // ・Shift + カーソルキー で文字列を選択状態にする場合
            // ・右の物理的な修飾キーのプレス/リリースの状態を制御するとき
            var flags = (uint)NativeWindowKeyInput.KEYEVENTF_EXTENDEDKEY;
            NativeWindowKeyInput.keybd_event(vk_code, scan_code, flags, UIntPtr.Zero);
        }

        private static void ReleaseExtendedKey(byte vk_code)
        {
            var scan_code = NativeWindowKeyInput.MapVirtualKey(vk_code, 0);
            var flags = (uint)NativeWindowKeyInput.KEYEVENTF_KEYUP | (uint)NativeWindowKeyInput.KEYEVENTF_EXTENDEDKEY;
            NativeWindowKeyInput.keybd_event(vk_code, scan_code, flags, UIntPtr.Zero);
        }
    }
}
