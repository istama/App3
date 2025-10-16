using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace IsTama.Utils
{
    static class NativeWindowMouseInput
    {
        // マウスの操作方法（SendInput()関数で使用）
        // マウスイベント
        public static readonly int INPUT_MOUSE = 0;
        public static readonly int MOUSEEVENTF_MOVE = 0x1;    // マウス移動
        public static readonly int MOUSEEVENTF_ABSOLUTE = 0x8000; // 絶対座標指定
        public static readonly int MOUSEEVENTF_LEFTDOWN = 0x2;    // 左　ボタン押す
        public static readonly int MOUSEEVENTF_LEFTUP = 0x4;    // 左　ボタン離す
        public static readonly int MOUSEEVENTF_RIGHTDOWN = 0x8;    // 右　ボタン押す
        public static readonly int MOUSEEVENTF_RIGHTUP = 0x10;   // 右　ボタン離す
        public static readonly int MOUSEEVENTF_MIDDLEDOWN = 0x20;   // 中央ボタン押す
        public static readonly int MOUSEEVENTF_MIDDLEUP = 0x40;   // 中央ボタン離す

        // ホイール回転
        public static readonly int MOUSEEVENTF_WHEEL = 0x800;
        // ホイール値
        public static readonly int WHEEL_DELTA = 120;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(
            HandleRef handle,   // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            int msg,            // 送信するメッセージ
            long wparam,        // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            long lparam         // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int PostMessage(
            HandleRef handle,   // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            int msg,            // 送信するメッセージ
            long wparam,        // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            long lparam         // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );

        /// <summary>
        /// マウス操作を送信する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(
            long dwFlags,
            long dx,
            long dy,
            long cButtons,
            long dwExtraInfo
        );

        /// <summary>
        /// マウス操作を送信する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendInput(
            int nInputs,
            INPUT[] pInputs,
            int cvsize
        );

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct INPUT
        {
            public int type;
            public MOUSEPOINT m;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct MOUSEPOINT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }
    }


}
