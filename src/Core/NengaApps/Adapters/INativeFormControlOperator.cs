using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.NengaApps
{
    interface INativeFormControlOperator
    {
        bool LeftClick();
        bool RightClick();

        bool Focus();

        bool SetText(string text);
        bool SendEnter();
    }
}
