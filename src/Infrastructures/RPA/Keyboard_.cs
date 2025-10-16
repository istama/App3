using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.RPA
{
    sealed class Keyboard_ : IKeyboard
    {
        public void PressKey(InputKey key)
        {
            Keyboard.PressInputKey(key);
        }

        public void ReleaseKey(InputKey key)
        {
            Keyboard.ReleaseInputKey(key);
        }

        public void PressModifierKeys(ModifierKeys keys)
        {
            // VIRTUALフラグが立っているなら左右の違いを吸収した仮想的なキーを送信する
            if (keys.HasFlag(ModifierKeys.VIRTUAL))
                Keyboard.PressVirtualModifierKeys(keys);
            else
                Keyboard.PressPhysicalModifierKeys(keys);
        }

        public void ReleaseModifierKeys(ModifierKeys keys)
        {
            // VIRTUALフラグが立っているなら左右の違いを吸収した仮想的なキーを送信する
            if (keys.HasFlag(ModifierKeys.VIRTUAL))
                Keyboard.ReleaseVirtualModifierKeys(keys);
            else
                Keyboard.ReleasePhysicalModifierKeys(keys);
        }

        public Boolean IsPressingKey(InputKey key)
        {
            return Keyboard.IsPressingKey(key);
        }

        public Boolean IsPressingModifierKey(ModifierKeys key)
        {
            return Keyboard.IsPressingModifierKey(key);
        }
    }
}
