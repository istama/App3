using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// IMEモードを表す列挙値。
    /// </summary>
    [Flags]
    enum ImeModes
    {
        Default      = 0,
        AlphaNumeric = 1,      // 英数字入力モード
        Native       = 1 << 2, // ネイティブモード（日本語など）。
        Katakana     = 1 << 3, // カタカナモード。
        Fullshape    = 1 << 4, // 全角モード。
        Roman        = 1 << 5, // ローマ字入力モード。
        Noconversion = 1 << 6, // 変換を行わないモード。
    }

    static class ImeModesConverters
    {
        private static readonly Dictionary<ImeModes, uint> _MODE_AND_VALUE_MAP;
        private static readonly Dictionary<uint, ImeModes> _VALUE_AND_MODE_MAP;

        static ImeModesConverters()
        {
            _MODE_AND_VALUE_MAP = new Dictionary<ImeModes, uint>
            {
                { ImeModes.AlphaNumeric, NativeWindowIme.IME_CMODE_ALPHANUMERIC },
                { ImeModes.Native,       NativeWindowIme.IME_CMODE_NATIVE },
                { ImeModes.Katakana,     NativeWindowIme.IME_CMODE_KATAKANA },
                { ImeModes.Fullshape,    NativeWindowIme.IME_CMODE_FULLSHAPE},
                { ImeModes.Roman,        NativeWindowIme.IME_CMODE_ROMAN },
                { ImeModes.Noconversion, NativeWindowIme.IME_CMODE_NOCONVERSION },
            };

            _VALUE_AND_MODE_MAP = new Dictionary<uint, ImeModes>();
            foreach (var pair in _MODE_AND_VALUE_MAP)
            {
                _VALUE_AND_MODE_MAP.Add(pair.Value, pair.Key);
            }
        }


        /// <summary>
        /// ImeModesをWin32ApiのIMEの設定に渡す数値に変換する。
        /// </summary>
        public static uint ToImeCode(this ImeModes modes)
        {
            // 設定がない場合は既定値を返す
            if (modes == ImeModes.Default)
                return NativeWindowIme.IME_CMODE_ALPHANUMERIC;

            uint ime_code = 0;
            foreach (var v in Enum.GetValues(typeof(ImeModes)))
            {
                var value = (ImeModes)v;
                // フラグから１つのフラグを取得する
                var one_mode = (modes & value);
                if (one_mode != 0 && _MODE_AND_VALUE_MAP.ContainsKey(one_mode))
                {
                    ime_code |= _MODE_AND_VALUE_MAP[one_mode];
                }
            }
            return ime_code;
        }

        /// <summary>
        /// Win32Apiに渡すIMEの設定値をImeModesに変換する。
        /// </summary>
        public static ImeModes ToImeModesFrom(uint imeCode)
        {
            if (imeCode == 0)
                return ImeModes.AlphaNumeric;

            ImeModes ime_modes = 0;
            foreach (var v in _VALUE_AND_MODE_MAP.Keys)
            {
                if (v == 0)
                    continue;

                // フラグから１つのフラグを取得する
                var one_code = imeCode & v;
                if (one_code != 0 && _VALUE_AND_MODE_MAP.ContainsKey(one_code))
                {
                    ime_modes |= _VALUE_AND_MODE_MAP[one_code];
                }
            }
            return ime_modes;
        }
    }
}
