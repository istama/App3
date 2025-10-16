using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// マウスフックしたときのウィンドウメッセージの送信方法。
    /// </summary>
    enum MouseHookMessagingMode
    {
        // マウスフックは使用しない
        None,
        // マウスが動作したらウィンドウメッセージをポストする
        Post,
        // マウスが動作したらウィンドウメッセージを送る
        Send,
    }
}
