using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    sealed class MouseHookEventArgs : EventArgs
    {
        public MouseState MouseState { get; }
        public bool       Handled    { get; set; } = false;


        public MouseHookEventArgs(MouseButtons buttons, MouseAction action) : this(new MouseState(buttons, action))
        {
        }

        public MouseHookEventArgs(MouseState mouseState)
        {
            Assert.IsNull(mouseState, nameof(mouseState));

            MouseState = mouseState;
        }
    }
}
