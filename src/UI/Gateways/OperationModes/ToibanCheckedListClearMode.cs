using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// 出力問番リストを全削除したときの動作モード
    /// </summary>
    enum ToibanCheckedListClearMode
    {
        // チェック付きの問番のみ削除する
        CheckedOnly = 0,
        // チェックされてない問番のみ削除する
        UncheckedOnly = 1,
        // 全削除する
        All = 2,
    }
}
