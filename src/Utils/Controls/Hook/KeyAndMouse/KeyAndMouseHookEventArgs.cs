using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    class KeyAndMouseHookEventArgs : EventArgs
    {
        public KeyAndMouseState KeyAndMouseState { get; }
        public bool  Handled          { get; set; } = false;


        public KeyAndMouseHookEventArgs(KeyAndMouseState keyAndMouseState)
        {
            Assert.IsNull(keyAndMouseState, nameof(keyAndMouseState));

            KeyAndMouseState = keyAndMouseState;
        }

    }
}
