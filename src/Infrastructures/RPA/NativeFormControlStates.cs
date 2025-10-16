using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.RPA
{
    sealed class NativeFormControlStates : INativeFormControlStates
    {
        private readonly FormControl _control;


        public NativeFormControlStates(FormControl control)
        {
            Assert.IsNull(control, nameof(control));

            _control = control;
        }

        public bool Enabled()
        {
            return _control.Enabled();
        }

        public Point GetPoint()
        {
            return _control.GetRelativePoint();
        }

        public Size GetSize()
        {
            return _control.GetSize();
        }

        public string GetText()
        {
            return _control.GetText();
        }

        public bool IsVisible()
        {
            return _control.IsVisible();
        }
    }
}
