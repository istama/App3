using System;
using System.Runtime.InteropServices;

namespace IsTama.Utils
{
	/// <summary>
	/// ウィンドウのスレッドに関するWindowsAPIを集めたモジュールクラス。
	/// </summary>
	public static class NativeWindowThread
	{
        /// <summary>
        /// 指定したウィンドウハンドルのスレッドＩＤを取得する。
        /// 引数ProcessIDに参照を渡せばそこに取得したＩＤが格納される。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(
            HandleRef handle,
            IntPtr ProcessId
        );
        
        /// <summary>
        /// 現在のスレッドのプロセスＩＤを取得する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        /// <summary>
        /// スレッドの入出力機構を別のスレッドにアタッチする。
        /// <参考>
        ///  http://eternalwindows.jp/windevelop/message/message06.html
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool AttachThreadInput(
            uint idAttach,
            uint idAttachTo,
            bool fAttach
        );

        /// <summary>
        /// 自身のスレッドをフォアグラウンドのウィンドウのスレッドにアタッチする。
        /// 引数には自身のウィンドウハンドルと、スレッドをアタッチした後に行いたい処理のコールバックを渡す。
        /// </summary>
        public static bool AttachMyWindowThreadToForeground<TArg>(HandleRef my_window_handle, Func<TArg, bool> callback, TArg arg)
        {
            // フォアグラウンドウィンドウのハンドルを取得
            var fore_hwnd = NativeWindowHandleSearcher.GetForegroundWindow();
            // このウィンドウが既に最前面に表示されている場合
            if (fore_hwnd == my_window_handle.Handle)
                return callback(arg);

            var fore_handle = new HandleRef(null, fore_hwnd);

            // 入力処理機構をアタッチすることでフォーカスを移動させる
            // <参考>
            //  http://eternalwindows.jp/windevelop/message/message06.html

            // フォアグラウンドのスレッドIDを取得
            uint fore_thread = GetWindowThreadProcessId(fore_handle, IntPtr.Zero);
            // 自分のスレッドIDを収得
            uint this_thread = GetCurrentThreadId();

            try
            {
                if (fore_thread != this_thread)
                    AttachThreadInput(this_thread, fore_thread, true);

                return callback(arg);
            }
            finally
            {
                if (fore_thread != this_thread)
                    AttachThreadInput(this_thread, fore_thread, false);
            }
        }
    }
}
