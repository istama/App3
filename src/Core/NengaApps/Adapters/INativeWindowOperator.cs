using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.NengaApps
{
    /// <summary>
    /// ウィンドウを操作するインタフェース。
    /// </summary>
    interface INativeWindowOperator
    {
        INativeWindowOperator Activate();

        INativeWindowOperator LeftClick(string controlName);
        INativeWindowOperator LeftClick(Point point);
        INativeWindowOperator RightClick(string controlName);
        INativeWindowOperator RightClick(Point point);

        INativeWindowOperator Focus(string controlName);
        INativeWindowOperator Focus(Point controlPoint);

        INativeWindowOperator SetText(Point controlPoint, string text);
        INativeWindowOperator SendEnterTo(string controlName);
        INativeWindowOperator SendEnterTo(Point controlPoint);

        INativeWindowOperator Wait(int waittime_ms);

        INativeWindowOperator ThrowIfFailed();
        bool Do();
        Task<bool> DoAsync();
    }
}
