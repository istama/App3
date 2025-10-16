using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    readonly struct KeyAndMouseState : IEquatable<KeyAndMouseState>
    {
        public static readonly KeyAndMouseState Empty = new KeyAndMouseState(InputKey.NONE, ModifierKeys.NONE, MouseAction.None, MouseButtons.None);

        public InputKey     InputKey     { get; }
        public ModifierKeys ModifierKeys { get; }

        public MouseAction  MouseAction  { get; }
        public MouseButtons MouseButtons { get; }


        public KeyAndMouseState(InputKey inputKey, ModifierKeys modifierKeys) : this(inputKey, modifierKeys, MouseButtons.None)
        { }
        public KeyAndMouseState(InputKey inputKey, ModifierKeys modifierKeys, MouseButtons mouseButtons)
            : this(inputKey, modifierKeys, MouseAction.None, mouseButtons)
        { }

        public KeyAndMouseState(MouseAction mouseAction, MouseButtons mouseButtons) : this(ModifierKeys.NONE, mouseAction, mouseButtons)
        { }
        public KeyAndMouseState(ModifierKeys modifierKeys, MouseAction mouseAction, MouseButtons mouseButtons)
            : this(InputKey.NONE, modifierKeys, mouseAction, mouseButtons)
        { }

        private KeyAndMouseState(InputKey inputKey, ModifierKeys modifierKeys, MouseAction mouseAction, MouseButtons mouseButtons)
        {
            InputKey     = inputKey;
            ModifierKeys = modifierKeys;
            MouseAction  = mouseAction;
            MouseButtons = mouseButtons;
        }


        public bool IsEmpty()
            => this == default || this == Empty;

        public KeyAndMouseState ToVirtualKeys()
            => new KeyAndMouseState(InputKey, ModifierKeys.ToVirtualKeys(), MouseAction, MouseButtons);

        public static bool operator ==(KeyAndMouseState a, KeyAndMouseState b)
            => a.Equals(b);
        public static bool operator !=(KeyAndMouseState a, KeyAndMouseState b)
            => !(a == b);

        public override bool Equals(object obj)
            => obj != null && obj is KeyAndMouseState other && Equals(other);
        public bool Equals(KeyAndMouseState other)
            => InputKey == other.InputKey && ModifierKeys == other.ModifierKeys && MouseAction == other.MouseAction && MouseButtons == other.MouseButtons;

        public override int GetHashCode()
            => InputKey.GetHashCode() ^ ModifierKeys.GetHashCode() ^ MouseAction.GetHashCode() ^ MouseButtons.GetHashCode();

        public string ToStateText()
        {
            var builder = new StringBuilder(64);

            if (InputKey != InputKey.NONE)
                builder.Append($"{InputKey.ToString()} ");

            if (ModifierKeys != ModifierKeys.NONE)
                builder.Append($"{ModifierKeys.ToStringForUser()} ");

            if (MouseAction != MouseAction.None)
                builder.Append($"{MouseAction.ToString()} ");

            if (MouseButtons != MouseButtons.None)
                builder.Append($"{MouseButtons.ToString()} ");

            return builder.ToString().TrimEnd();
        }

        public override string ToString()
            => ToStateText();
    }
}
