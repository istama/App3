using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    interface IKeyboard
    {
        void PressKey(InputKey keys);
        void ReleaseKey(InputKey keys);

        void PressModifierKeys(ModifierKeys keys);
        void ReleaseModifierKeys(ModifierKeys keys);

        Boolean IsPressingKey(InputKey key);
        Boolean IsPressingModifierKey(ModifierKeys key);
    }
}
