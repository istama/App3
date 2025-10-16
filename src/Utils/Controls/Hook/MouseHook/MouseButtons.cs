using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    [Flags]
    enum MouseButtons
    {
        None        = 0,
        LButtonDown = 1,
        RButtonDown = 1 << 1,
        MButtonDown = 1 << 2
    }
}
