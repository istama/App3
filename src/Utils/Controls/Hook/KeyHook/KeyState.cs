using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    readonly struct KeyState : IEquatable<KeyState>
    {
        public static readonly KeyState Empty = new KeyState(InputKey.NONE, ModifierKeys.NONE);


        public InputKey     InputKey     { get; }
        public ModifierKeys ModifierKeys { get; }


        public KeyState(InputKey inputKey) : this(inputKey, ModifierKeys.NONE) { }
        public KeyState(InputKey inputKey, ModifierKeys modifierKeys)
        {
            InputKey     = inputKey;
            ModifierKeys = modifierKeys;
        }


        public Boolean IsEmpty()
            => this == default || this == Empty;

        /// <summary>
        /// 修飾キーの左右の違いを吸収した値を返す。
        /// LCtrlとRCtrlを同じ値として判定させたい場合などに使用する。
        /// 修飾キーを送信するときにLRの違いを吸収するための機能ではない。
        /// </summary>
        public KeyState ToVirtualKeys()
            => new KeyState(InputKey, ModifierKeys.ToVirtualKeys());

        public static Boolean operator ==(KeyState a, KeyState b)
            => a.Equals(b);
        public static bool operator !=(KeyState a, KeyState b)
            => !(a == b);

        public override Boolean Equals(Object obj)
            => obj != null && obj is KeyState other && Equals(other);
        public Boolean Equals(KeyState other)
            => InputKey == other.InputKey && ModifierKeys == other.ModifierKeys;

        public override Int32 GetHashCode()
             => InputKey.GetHashCode() ^ (ModifierKeys.GetHashCode() >> 1);

        public override string ToString()
            => JoinTexts(ModifierKeys.ToStringForUser(), InputKey.ToString());
            
        public string ToExactString()
            => JoinTexts(ModifierKeys.ToExactString(), InputKey.ToString());

        private String JoinTexts(params String[] texts)
            => String.Join("", texts.Where(t => !String.IsNullOrWhiteSpace(t)));
    }
}
