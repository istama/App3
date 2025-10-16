using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    public static class NativeWindowSubWindows
    {
        /*
         * 参考：チェックボックスの状態の取得について 
         * https://qiita.com/Shujis1964/questions/8e49c48977e0a61556ac
         * SendMessageでチェックボックスの状態を取得するためには、
         * そのチェックボックスがWin32APIのウィンドウクラスである必要があるらしい。
         * .NETのチェックボックスはこれに該当しないので、SendMessageでは状態を取得できない。
         */

        public static readonly int BM_GETCHECK = 0x00F0;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            HandleRef handle, // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            uint      msg,    // 送信するメッセージ
            IntPtr    wparam, // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            IntPtr    lparam  // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr handle,    // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            uint   msg,       // 送信するメッセージ
            IntPtr wparam,    // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            IntPtr lparam     // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr PostMessage(
            HandleRef handle, // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            uint      msg,    // 送信するメッセージ
            IntPtr    wparam, // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            IntPtr    lparam  // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr PostMessage(
            IntPtr handle,    // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            uint   msg,       // 送信するメッセージ
            IntPtr wparam,    // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            IntPtr lparam     // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );

        [DllImport("oleacc.dll", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static extern object AccessibleObjectFromWindow(IntPtr hwnd, uint dwId, ref Guid riid);


    }
}
