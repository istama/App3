using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.NengaApps
{
    /// <summary>
    /// ウィンドウの情報を取得するインタフェース。
    /// </summary>
    interface INativeWindowStates
    {
        bool Exists();
        bool IsOpen(int waittime_ms);
        bool IsActivated(int waittime_ms);

        string GetTitle();
        Point GetPoint();
        Size GetSize();

        INativeFormControlStates   GetFormControlStates(Int32 index);
        INativeFormControlStates[] GetFormControlStatesArray(String controlTitlePattern);
        INativeFormControlStates[] GetFormControlStatesArray(Point point);
        INativeFormControlStates[] GetFormControlStatesArray(Point point, Size size);
    }
}
