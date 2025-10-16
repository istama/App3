using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.RPA
{
    sealed class NativeFormControlOperator : INativeFormControlOperator
    {
        private readonly FormControl _control;
        private readonly IKeyboard _keyboard;

        public NativeFormControlOperator(FormControl control, IKeyboard keyboard)
        {
            _control = control;
            _keyboard = keyboard;
        }

        public bool Focus()
        {
            return _control.Focus();
        }

        public bool LeftClick()
        {
            _control.LeftClick();
            return true;
        }

        public bool RightClick()
        {
            _control.RightClick();
            return true;
        }

        public bool SendEnter()
        {
            if (!Focus())
                return false;

            _keyboard.PressKey(InputKey.ENTER);
            return true;
        }

        public bool SetText(string text)
        {
            _control.SetText(text);
            return true;
        }
    }
}
