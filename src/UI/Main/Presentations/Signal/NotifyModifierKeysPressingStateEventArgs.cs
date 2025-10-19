using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    class NotifyModifierKeysPressingStateEventArgs : EventArgs
    {
        public ModifierKeysPressingState ModifierKeysState { get; set; }

        public NotifyModifierKeysPressingStateEventArgs()
        {
        }
    }
}
