using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    sealed class KeyHookPrimitiveEventArgs
    {
        public InputKey     InputKey     { get; }
        public ModifierKeys ModifierKeys { get; }
        public bool         Handled      { get; set; } = false;

        public KeyHookPrimitiveEventArgs(InputKey key, ModifierKeys modifierKeys)
        {
            InputKey = key;
            ModifierKeys = modifierKeys;
        }
    }
}
