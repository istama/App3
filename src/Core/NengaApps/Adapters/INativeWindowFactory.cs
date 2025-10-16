using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.NengaApps
{
    interface INativeWindowFactory
    {
        INativeWindowStates GetOrCreateWindowStates(string windowTitlePattern);
        INativeWindowStates GetOrCreateWindowStates(string windowTitlePattern, int width);

        INativeWindowOperator GetOrCreateWindowOperator(string windowTitlePattern);
        INativeWindowOperator GetOrCreateWindowOperator(string windowTitlePattern, int width);
    }
}
