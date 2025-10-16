using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// ModifierKeysと仮想キーコードの相互変換を行うメソッドを集めた静的クラス。
    /// </summary>
    static class ModifierKeysConverters
    {
        private static readonly Dictionary<ModifierKeys, byte> _ModifierKeyAndKeycodeMap;
        private static readonly Dictionary<byte, ModifierKeys> _KeycodeAndModifierKeyMap;

        static ModifierKeysConverters()
        {
            _ModifierKeyAndKeycodeMap = new Dictionary<ModifierKeys, byte>
            {
                // この制御キーの仮想キーコードはキーボードの左右のキーを区別しない値
                { ModifierKeys.VSHIFT, NativeWindowKeyInput.VK_SHIFT   },
                { ModifierKeys.VCTRL,  NativeWindowKeyInput.VK_CONTROL },
                { ModifierKeys.VALT,   NativeWindowKeyInput.VK_MENU    },

                { ModifierKeys.LSHIFT, NativeWindowKeyInput.VK_LSHIFT   },
                { ModifierKeys.LCTRL,  NativeWindowKeyInput.VK_LCONTROL },
                { ModifierKeys.LALT,   NativeWindowKeyInput.VK_LMENU    },
                { ModifierKeys.LWIN,   NativeWindowKeyInput.VK_LWIN     },

                { ModifierKeys.RSHIFT, NativeWindowKeyInput.VK_RSHIFT   },
                { ModifierKeys.RCTRL,  NativeWindowKeyInput.VK_RCONTROL },
                { ModifierKeys.RALT,   NativeWindowKeyInput.VK_RMENU    },
                { ModifierKeys.RWIN,   NativeWindowKeyInput.VK_RWIN     },
            };

            _KeycodeAndModifierKeyMap = new Dictionary<byte, ModifierKeys>();

            // 列挙値と仮想キーコードを反対にしてマップに登録する
            foreach (var key in _ModifierKeyAndKeycodeMap.Keys)
            {
                var code = _ModifierKeyAndKeycodeMap[key];
                if (_KeycodeAndModifierKeyMap.ContainsKey(code))
                {
                    var k = _KeycodeAndModifierKeyMap[code];
                    throw new InvalidOperationException($"{code}: 仮想キーコードの値が {k.ToString()} と重複しています。");
                }
                _KeycodeAndModifierKeyMap.Add(code, key);
            }
        }


        /// <summary>
        /// 左右のキーを吸収した値を返す。
        /// </summary>
        public static ModifierKeys ToVirtualKeys(this ModifierKeys keys)
        {
            if (keys.HasAlt())
                keys |= ModifierKeys.VALT;
            if (keys.HasCtrl())
                keys |= ModifierKeys.VCTRL;
            if (keys.HasShift())
                keys |= ModifierKeys.VSHIFT;
            if (keys.HasWin())
                keys |= ModifierKeys.LWIN | ModifierKeys.RWIN;

            return keys;
        }

        /// <summary>
        /// ModifierKeys列挙型の要素が示すキーを、win32api に渡すことのできる
        /// 仮想キーコードに変換する。
        /// keyが複数のフラグが立っている場合は例外を投げる。
        /// </summary>
        public static byte ToVirtualKeyCode(this ModifierKeys key)
        {
            if (_ModifierKeyAndKeycodeMap.ContainsKey(key))
                return _ModifierKeyAndKeycodeMap[key];

            throw new ArgumentException($"{key.ToString()} はキーマップに割り当てられてないキーです。");
        }

        /// <summary>
        /// Win32APIに渡す仮想キーコードを対応するModifierKeys列挙体の値に変換する。
        /// </summary>
        public static bool TryConvertToModifierKeyFrom(byte vk_code, out ModifierKeys key)
        {
            key = ModifierKeys.NONE;

            if (_KeycodeAndModifierKeyMap.ContainsKey(vk_code))
            {
                key = _KeycodeAndModifierKeyMap[vk_code];
                return true;
            }

            return false;
        }

        /// <summary>
        /// Win32APIに渡す仮想キーコードを対応するModifierKeys列挙体の値に変換する。
        /// </summary>
        public static ModifierKeys ToModifierKeyFrom(byte vk_code)
        {
            if (_KeycodeAndModifierKeyMap.ContainsKey(vk_code))
                return _KeycodeAndModifierKeyMap[vk_code];

            return ModifierKeys.NONE;
        }

        /// <summary>
        /// InputKeys列挙型の要素が示すキーを、System.Windows.Forms.SendKeys.SendWait() に渡すことのできる
        /// キーを表す文字列に変換する。
        /// なおWinキーには対応しない。
        /// </summary>
        public static string ToSendKeyText(ModifierKeys key)
        {
            var builder = new StringBuilder(4);

            if (key.HasShift())
                builder.Append("+");
            if (key.HasCtrl())
                builder.Append("^");
            if (key.HasAlt())
                builder.Append("%");

            return builder.ToString();
        }
    }
}
