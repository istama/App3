using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// フォーカス移動の動作を指定するための列挙値。
    /// </summary>
    enum RectangleTransitionAction
    {
        // 縦移動のとき列を優先して移動する。 
        PreferColumns,  
        // 縦移動のとき行を優先して移動する。
        PreferRows,
        // 縦移動のとき行の一番左へ移動する。
        PreferLeft
    }
}
