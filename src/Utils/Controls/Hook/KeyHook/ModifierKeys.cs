using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    [Flags]
    enum ModifierKeys
    {
        NONE    = 0,

        // 修飾キーのLRの違いを吸収してキー送信する場合に使用する。
        VIRTUAL = 1,

        LALT    = 1 << 3,
        LCTRL   = 1 << 4,
        LSHIFT  = 1 << 5,

        RALT    = 1 << 6,
        RCTRL   = 1 << 7,
        RSHIFT  = 1 << 8,

        LWIN    = 1 << 9,
        RWIN    = 1 << 10,

        VALT = VIRTUAL | LALT | RALT,
        VCTRL = VIRTUAL | LCTRL | RCTRL,
        VSHIFT = VIRTUAL | LSHIFT | RSHIFT,

        ALL = LALT | LCTRL | LSHIFT | RALT | RCTRL | RSHIFT | LWIN | RWIN
    }

    static class ModifierKeysExtentions
    {
        public static string ToStringForUser(this ModifierKeys keys)
        {
            var builder = new StringBuilder(64);

            builder.Append(keys.HasAlt()   ? "Alt "   : string.Empty);
            builder.Append(keys.HasCtrl()  ? "Ctrl "  : string.Empty);
            builder.Append(keys.HasShift() ? "Shift " : string.Empty);
            builder.Append(keys.HasWin()   ? "Win "   : string.Empty);

            return builder.ToString();
        }

        public static string ToExactString(this ModifierKeys keys)
        {
            var builder = new StringBuilder(64);

            builder.Append(keys.HasVAlt()   ? "VAlt "   : string.Empty);
            builder.Append(keys.HasVCtrl()  ? "VCtrl "  : string.Empty);
            builder.Append(keys.HasVShift() ? "VShift " : string.Empty);

            builder.Append(keys.HasLAlt()   ? "LAlt "   : string.Empty);
            builder.Append(keys.HasLCtrl()  ? "LCtrl "  : string.Empty);
            builder.Append(keys.HasLShift() ? "LShift " : string.Empty);
            builder.Append(keys.HasLWin()   ? "LWin "   : string.Empty);

            builder.Append(keys.HasRAlt()   ? "RAlt "   : string.Empty);
            builder.Append(keys.HasRCtrl()  ? "RCtrl "  : string.Empty);
            builder.Append(keys.HasRShift() ? "RShift " : string.Empty);
            builder.Append(keys.HasRWin()   ? "RWin "   : string.Empty);

            return builder.ToString();
        }

        /// <summary>
        /// 引数のフラグをすべて持っているならtrueを返す。
        /// </summary>
        public static bool HasAll(this ModifierKeys keys, ModifierKeys flag) => (keys & flag) == flag;

        public static bool HasVAlt(this ModifierKeys keys)   => keys.HasVirtual() && keys.HasAlt();
        public static bool HasVCtrl(this ModifierKeys keys)  => keys.HasVirtual() && keys.HasCtrl();
        public static bool HasVShift(this ModifierKeys keys) => keys.HasVirtual() && keys.HasShift();

        public static bool HasAlt(this ModifierKeys keys)    => keys.HasLAlt()   | keys.HasRAlt();
        public static bool HasCtrl(this ModifierKeys keys)   => keys.HasLCtrl()  | keys.HasRCtrl();
        public static bool HasShift(this ModifierKeys keys)  => keys.HasLShift() | keys.HasRShift();
        public static bool HasWin(this ModifierKeys keys)    => keys.HasLWin()   | keys.HasRWin();

        public static bool HasLAlt(this ModifierKeys keys)   => keys.HasAll(ModifierKeys.LALT);
        public static bool HasLCtrl(this ModifierKeys keys)  => keys.HasAll(ModifierKeys.LCTRL);
        public static bool HasLShift(this ModifierKeys keys) => keys.HasAll(ModifierKeys.LSHIFT);
        public static bool HasLWin(this ModifierKeys keys)   => keys.HasAll(ModifierKeys.LWIN);

        public static bool HasRAlt(this ModifierKeys keys)   => keys.HasAll(ModifierKeys.RALT);
        public static bool HasRCtrl(this ModifierKeys keys)  => keys.HasAll(ModifierKeys.RCTRL);
        public static bool HasRShift(this ModifierKeys keys) => keys.HasAll(ModifierKeys.RSHIFT);
        public static bool HasRWin(this ModifierKeys keys)   => keys.HasAll(ModifierKeys.RWIN);

        public static bool HasVirtual(this ModifierKeys keys) => keys.HasAll(ModifierKeys.VIRTUAL);
    }
}
