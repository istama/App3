using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UseCases.MainForm
{
    /// <summary>
    /// 問番を出力したときに動作モード。
    /// </summary>
    enum PostToibanCheckedListOutputMode
    {
        // 何もしない
        None = 0,
        // チェックを外す
        Uncheck = 1
    }
}
