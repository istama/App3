using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// キーの状態を保持する可変クラス。
    /// </summary>
    sealed class KeyStateBuffer
    {
        private readonly Dictionary<ModifierKeys, bool> _modifierKeysStateMap;

        private int _lastPressedKeyCode = -1;

        public KeyStateBuffer()
        {
            _modifierKeysStateMap = new Dictionary<ModifierKeys, bool>
            {
                { ModifierKeys.LSHIFT, false },
                { ModifierKeys.LCTRL,  false },
                { ModifierKeys.LALT,   false },
                { ModifierKeys.LWIN,   false },

                { ModifierKeys.RSHIFT, false },
                { ModifierKeys.RCTRL,  false },
                { ModifierKeys.RALT,   false },
                { ModifierKeys.RWIN,   false },
            };
        }

        /// <summary>
        /// 引数のキーコードが入力された状態に更新する。
        /// テキストキーの状態が前回から変化したらtrueを返す
        /// </summary>
        public bool UpdateByInputKeyDown(InputKey inputKey)
        {
            var key_code = inputKey.ToVirtualKeyCode();
            // 押されたキーを保存
            return Interlocked.Exchange(ref _lastPressedKeyCode, key_code) != _lastPressedKeyCode;
        }

        /// <summary>
        /// 引数のキーコードが離された状態に更新する。
        /// テキストキーの状態が前回から変化した場合は引数のキーの種類を返す。
        /// </summary>
        public bool UpdateByInputKeyUp()
        {
            return Interlocked.Exchange(ref _lastPressedKeyCode, -1) != -1;
        }

        /// <summary>
        /// 修飾キーの状態を更新する。
        /// 複数のフラグが立っている場合は更新せずfalseを返す。
        /// 前回の状態から更新されたならtrueを返す。
        /// </summary>
        public bool UpdateByModifierKeysDown(ModifierKeys modifierKeys)
        {
            if (!_modifierKeysStateMap.ContainsKey(modifierKeys))
                return false;

            if (_modifierKeysStateMap[modifierKeys] == true)
                return false;

            _modifierKeysStateMap[modifierKeys] = true;
            return true;
        }

        /// <summary>
        /// 修飾キーの状態を更新する。
        /// 複数のフラグが立っている場合は更新せずfalseを返す。
        /// 前回の状態から更新されたならtrueを返す。
        /// </summary>
        public bool UpdateByModifierKeysUp(ModifierKeys modifierKeys)
        {
            if (!_modifierKeysStateMap.ContainsKey(modifierKeys))
                return false;

            if (_modifierKeysStateMap[modifierKeys] == false)
                return false;

            _modifierKeysStateMap[modifierKeys] = false;
            return true;
        }

        public KeyState ToKeyState()
        {
            var input_key = InputKeyConverters.ToInputKeyFrom((byte)_lastPressedKeyCode);
            var modifier_keys = ModifierKeys.NONE;

            if (_modifierKeysStateMap[ModifierKeys.LSHIFT])
                modifier_keys |= ModifierKeys.LSHIFT;
            if (_modifierKeysStateMap[ModifierKeys.LCTRL])
                modifier_keys |= ModifierKeys.LCTRL;
            if (_modifierKeysStateMap[ModifierKeys.LALT])
                modifier_keys |= ModifierKeys.LALT;
            if (_modifierKeysStateMap[ModifierKeys.LWIN])
                modifier_keys |= ModifierKeys.LWIN;

            if (_modifierKeysStateMap[ModifierKeys.RSHIFT])
                modifier_keys |= ModifierKeys.RSHIFT;
            if (_modifierKeysStateMap[ModifierKeys.RCTRL])
                modifier_keys |= ModifierKeys.RCTRL;
            if (_modifierKeysStateMap[ModifierKeys.RALT])
                modifier_keys |= ModifierKeys.RALT;
            if (_modifierKeysStateMap[ModifierKeys.RWIN])
                modifier_keys |= ModifierKeys.RWIN;

            return new KeyState(input_key, modifier_keys);
        }

        public override string ToString()
        {
            var key = InputKeyConverters.ToInputKeyFrom((byte)_lastPressedKeyCode);

            var lshift = _modifierKeysStateMap[ModifierKeys.LSHIFT];
            var lctrl  = _modifierKeysStateMap[ModifierKeys.LCTRL];
            var lalt   = _modifierKeysStateMap[ModifierKeys.LALT];
            var lwin   = _modifierKeysStateMap[ModifierKeys.LWIN];

            var rshift = _modifierKeysStateMap[ModifierKeys.RSHIFT];
            var rctrl  = _modifierKeysStateMap[ModifierKeys.RCTRL];
            var ralt   = _modifierKeysStateMap[ModifierKeys.RALT];
            var rwin   = _modifierKeysStateMap[ModifierKeys.RWIN];

            return string.Format("key   : {9}{0} lshift: {1}{0} lctrl : {2}{0} lalt  : {3}{0} lwin: {4}{0} rshift : {5}{0} rctrl  : {6}{0} ralt : {7}{0} rwin : {8}{0}",
                Environment.NewLine,
                lshift.ToString(), lctrl.ToString(), lalt.ToString(), lwin.ToString(),
                rshift.ToString(), rctrl.ToString(), ralt.ToString(), rwin.ToString(),
                key);
        }
    }
}
