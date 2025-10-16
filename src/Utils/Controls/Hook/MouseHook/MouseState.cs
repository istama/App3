using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    readonly struct MouseState : IEquatable<MouseState>
    {
        public static readonly MouseState Empty = new MouseState(MouseButtons.None, MouseAction.None);

        public MouseButtons Buttons { get; }
        public MouseAction  Action  { get; }


        public MouseState(MouseButtons buttons, MouseAction action)
        {
            Buttons = buttons;
            Action  = action;
        }


        public bool IsEmpty()
            => this == Empty;

        public static bool operator ==(MouseState a, MouseState b)
            => a.Equals(b);
        public static bool operator !=(MouseState a, MouseState b)
            => !(a == b);

        public override bool Equals(object obj)
            => obj != null && obj is KeyAndMouseState other && Equals(other);
        public bool Equals(MouseState other)
            => Buttons == other.Buttons && Action == other.Action;

        public override int GetHashCode()
             => Buttons.GetHashCode() ^ (Action.GetHashCode() >> 1);

        public override string ToString()
            => $"{Buttons.ToString()} {Action.ToString()}";
    }
}
