
using System;
using System.Runtime.InteropServices;

namespace IsTama.Utils
{
    /// <summary>
    /// ウィンドウを操作するWindowAPIを集めたモジュールクラス。
    /// <参考> https://www.tokovalue.jp/function/mouse_event.htm
    /// </summary>
    public static class NativeWindowController
    {
        /* ウィンドウのZオーダー（SetWindowPos()関数で使用）*/
        /// Z オーダーの先頭
        public static readonly HandleRef HANDLE_TOP = new HandleRef(null, new IntPtr(0));
        /// Z オーダーの最後
        public static readonly HandleRef HANDLE_BOTTOM = new HandleRef(null, new IntPtr(1));
        /// 常に一番手前に表示される最前面ウィンドウにする
        public static readonly HandleRef HANDLE_TOPMOST = new HandleRef(null, new IntPtr(-1));
        /// 最前面ウィンドウ以外のすべてのウィンドウの前
        public static readonly HandleRef HANDLE_NOTOPMOST = new HandleRef(null, new IntPtr(-2));

        /* ウィンドウの表示状態（SetWindowPos()関数で使用）*/
        /// サイズを変更しない
        public static readonly uint SWP_NOSIZE = 1;
        /// 位置を変更しない
        public static readonly uint SWP_NOMOVE = 2;

        //    public static readonly uint SWP_NOZORDER       = 4;      //Zオーダーを変更しない
        //    public static readonly uint SWP_NOREDRAW       = 8;      //変更に伴う再描画をしない
        //    public static readonly uint SWP_NOACTIVATE     = 0x10;      //ウィンドウをアクティブにしない
        //    public static readonly uint SWP_FRAMECHANGED   = 0x20;      //SetWindowLong関数を使用後に変更を適用
        public static readonly uint SWP_SHOWWINDOW = 0x40;
        //ウィンドウを表示する
        //    public static readonly uint SWP_HIDEWINDOW     = 0x80;      //ウィンドウを隠す
        //    public static readonly uint SWP_NOCOPYBITS     = 0x100;  //クライアント領域の内容全体を破棄
        //    public static readonly uint SWP_NOOWNERZORDER  = 0x200;  //オーナーウィンドウの Z オーダーを変更しない
        //    public static readonly uint SWP_NOSENDCHANGING = 0x400;  //WM_WINDOWPOSCHANGINGメッセージを送らない
        //    public static readonly uint SWP_DEFERERASE     = 0x2000; //WM_SYNCPAuintメッセージを送らない

        // ウィンドウの表示状態（ShowWindow()関数で使用）
        //    public static readonly int SW_HIDE            = 0;  // 非表示にし、他のウィンドウをアクティブにする
        //    public static readonly int SW_SHOWNORMAL      = 1;  // ウィンドウサイズを元に戻してアクティブにする 初めて表示するウィンドウに指定する値
        //    public static readonly int SW_SHOWMINIMIZED   = 2;  // アクティブにして最大化する
        //    public static readonly int SW_SHOWMAXIMIZED   = 3;  // アクティブにして最小化する
        //    public static readonly int SW_SHOWNOACTIVATE  = 4;  // サイズを元に戻すがアクティブにはしない
        //    public static readonly int SW_SHOW            = 5;  // 現在のサイズでアクティブにする
        //    public static readonly int SW_MINIMIZE        = 6;  // 最小化し、次のウィンドウをアクティブにする
        //    public static readonly int SW_SHOWMINNOACTIVE = 7;  // 最小化して表示するが、アクティブにはしない
        //    public static readonly int SW_SHOWNA          = 8;  // 現在のサイズで表示するが、アクティブにはしない
        public static readonly int SW_RESTORE = 9;
        // サイズを元に戻してアクティブにする 最小化されていたウィンドウに指定する値
        //    public static readonly int SW_SHOWDEFAULT     = 10; // CreateProcessのパラメータとおりに表示
        //    public static readonly int SW_FORCEMINIMIZE   = 11; // ウィンドウを所有するスレッドがハングしていても、ウィンドウを最小化

        public static readonly int WM_SETFOCUS = 0x0007; // フォーカスを取得する



        /// <summary>
        /// 指定したウィンドウをアクティブにする。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(
            IntPtr hWnd
        );

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// ウィンドウの表示状態を設定する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ShowWindow(
            //IntPtr hWnd,
            HandleRef hwnd,
            int nCmdShow
        );

        /// <summary>
        /// 指定したウィンドウをアクティブにする。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool BringWindowToTop(
            IntPtr hWnd
        );

        /// <summary>
        /// 指定したウィンドウをアクティブにする。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetWindowPos(
            HandleRef hWnd,             // ウィンドウのハンドル
            HandleRef hWndInsertAfter,  // 配置順序のハンドル
            int X,                      // 横方向の位置
            int Y,                      // 縦方向の位置
            int cx,                     // 幅
            int cy,                     // 高さ
            uint uFlags                 // ウィンドウ位置のオプション
        );

        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //public static extern IntPtr SetActiveWindow(
        //    IntPtr hWnd   // ウィンドウのハンドル
        //);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(
            HandleRef hWnd   // ウィンドウのハンドル
        );

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        /// <summary>
        /// 指定したウィンドウにフォーカスを当てる。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr SetFocus(
            HandleRef handle
        );

        /// <summary>
        /// フォーカスの当たっているウィンドウハンドルを取得する。
        /// 別アプリケーションのウィンドウハンドルを取得するにはGetForgroundWindow()と組み合わせて呼び出す必要がある。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int MoveWindow(
            IntPtr hwnd,
            int x,
            int y,
            int nWidth,
            int nHeight,
            int bRepaint
        );

        /// <summary>
        /// ウィンドウを破棄する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool DestroyWindow(
            HandleRef hwnd  // ウィンドウハンドル
        );
    }
}
