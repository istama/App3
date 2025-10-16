using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// マウスの動作を表す列挙体。
    /// </summary>
    enum MouseAction
    {
        None = 0,

        Move,

        LClick,
        MClick,
        RClick,

        LDoubleClick,
        MDoubleClick,
        RDoubleClick,

        WheelUp,
        WheelDown,
    }
}
