using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    class KeyHookEventArgs : EventArgs
    {
        public KeyState KeyState { get; }
        public bool  Handled  { get; set; } = false;

        public KeyHookEventArgs(KeyState keyState)
        {
            this.KeyState = keyState;
        }

        public KeyHookEventArgs(KeyEventArgs args)
        {
            var input_key = InputKeyConverters.ToInputKeyFrom((byte)args.KeyValue);

            var modifier_keys = ModifierKeys.NONE;
            if (args.Shift)
                modifier_keys |= ModifierKeys.LSHIFT | ModifierKeys.RSHIFT;
            if (args.Control)
                modifier_keys |= ModifierKeys.LCTRL | ModifierKeys.RCTRL;
            if (args.Alt)
                modifier_keys |= ModifierKeys.LALT | ModifierKeys.RALT;

            KeyState = new KeyState(input_key, modifier_keys);
        }
    }
}
