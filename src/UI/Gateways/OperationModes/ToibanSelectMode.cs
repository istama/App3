using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// 問番テキストボックスの動作モード。
    /// </summary>
    enum ToibanSelectMode
    {
        // テキストボックスがクリックされたときに問番を選択状態にする
        ByClick = 0,
        // テキストボックスがWクリックされたときに問番を選択状態にする
        ByWClick = 1
    }
}
