using System;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

namespace IsTama.Utils
{
    /// <summary>
    /// ウィンドウの見た目の情報を取得するWindowsAPIを集めたモジュールクラス。
    /// 
    /// API別Win32サンプル集: http://nienie.com/~masapico/api_index.html
    /// </summary>
    public static class NativeWindowInformation
	{
        /*
         * ウィンドウハンドルについて。
         * 基本的にはウィンドウハンドルの値が変更されるタイミングは、ウィンドウが生成、破棄されたとき。
         * Windows APIを直接呼び出すときは、ウィンドウハンドルの値は作成から削除まで変化しない。
         * ただし、.NET Frameworkなど上位ライブラリではウィンドウスタイルの変更によって、
         * ウィンドウが内部的に作り直される可能性はあり、その時にウィンドウハンドルが変わることもあり得る。
         * ウィンドウハンドルは必要な時にその都度取得しなおすのが安全ではある。
         */

        /// <summary>
        /// SendMessageA()関数などに渡すウィンドウメッセージ。
        /// コントロールのテキストを取得する。
        /// wparam: テキストを格納するバッファのサイズを指定します。このサイズには終端ヌル文字も含まれます。
        /// lparam; テキストを格納するバッファのアドレスを指定します。（結果を格納するオブジェクト）
        /// 戻り値: コピーされたテキストのサイズが返ります。このサイズには終端ヌル文字は含まれません。
        /// http://chokuto.ifdef.jp/urawaza/message/WM_GETTEXT.html
        /// </summary>
        public static int WM_GETTEXT = 0x000D;
        public static int WM_SETTEXT = 0x000C;

        // 現在選択されている範囲を取得する
        public static int EM_GETSEL = 0x00B0;

        // ステータスウィンドウのパーツの数と、それぞれの右端の座標を取得する
        public static int SB_GETPARTS = 0x0406;
        // ステータスバーに表示されるテキストを取得する
        public static int SB_GETTEXT  = 0x0402;
        // ステータスバーに表示されるテキストの長さを取得する
        public static int SB_GETTEXTLENGTH = 0x0403;
        // ステータスバーに表示されるテキストを取得する
        public static int SB_GETTEXTW = 0x040D;
        // ステータスバーに表示されるテキストの長さを取得する
        public static int SB_GETTEXTLENGTHW = 0x040C;

        // リストビューのコントロールのアイテムやサブアイテムを取得する
        public static int LVM_GETITEM = 0x1005;
        // リストビューに含まれるアイテムの数を取得する
        public static int LVM_GETITEMCOUNT = 0x1004;

        // タブの数を取得する
        public static int TCM_GETITEMCOUNT = 0x1304;
        // タブの情報を取得する
        public static int TCM_GETITEM = 0x1305;
        // タブを選択する
        public static int TCM_SETCURSEL = 0x130C;
        // 選択されているタブのインデックスを取得する
        public static int TCM_GETCURSEL = 0x130B;

        // GetWindowLong()からウィンドウスタイルを取得する
        public static int GWL_STYLE = -16;
        
        /* ウィンドウスタイルに関するフラグ */
        // 無効なコントロールな場合に立つビット
        public static int WS_DISABLED = 0x08000000;
        // タブ移動可能なコントロールな場合に立つビット
        public static int WS_TABATOP = 0x00010000;
        // 読み込み専用なコントロールの場合に立つビット
        public static int ES_READONLY = 0x0800;

        /*
         SendMessageについて。
         引数に指定したハンドルのウィンドウにメッセージを送る関数。
         指定されたウィンドウプロシージャがメッセージを処理するまで処理は戻らない。
         なのでメッセージの送信先のウィンドウがフリーズしていると、自身もフリーズしてしまう。

         一方、PostMessageは指定したウィンドウのメッセージキューにメッセージを投函する。
         PostMessage関数はすぐに処理を戻す。
         メッセージを受け取った側のウィンドウは、メッセージをキューから取り出すために
         GetMessage()やPeekMessage()を呼び出さなければならない。

        http://hp.vector.co.jp/authors/VA000092/win32/win32trivia.html

         SendMessageAはANSI、SendMessageWはUNICODEのバージョン。
         SendMessageはUNICODEプリプロセッサ定数の定義に基づいてどちらかのバージョンを自動的に選択するエイリアス。
         大文字の "W" で終わるすべての関数名は、Unicodeパラメーター、つまりワイド文字のパラメーターを取る。

         エンコードに依存しないエイリアスの使用と、エンコードに依存しないコードの使用を混在させると
         コンパイルまたは実行時エラーにつながる不一致が発生する可能性がある。

         Windows SDKには、汎用バージョン、Windowsコードページバージョン、ユニコードバージョンの関数プロトタイプが用意されている。
         例えば、汎用バージョンであるSetWindowText()エイリアスは次のように定義されている。
          
          #ifdef UNICODE
          #define SetWindowText SetWindowTextW // ユニコードバージョン
          #else
          #define SetWindowText SetWindowTextA // Windowsコードページバージョン
          #endif
         
         https://learn.microsoft.com/en-us/windows/win32/intl/conventions-for-function-prototypes

          PostMessageについて

         */ 

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(
            HandleRef handle,  // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            int msg,     // 送信するメッセージ
            long wparam,  // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            long lparam   // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(
            HandleRef handle,  // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            int msg,     // 送信するメッセージ
            long wparam,  // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            StringBuilder lparam   // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(
            HandleRef handle,  // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            int msg,     // 送信するメッセージ
            long wparam,  // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            [MarshalAs(UnmanagedType.LPWStr)] string lparam   // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(
            HandleRef handle,  // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            int msg,     // 送信するメッセージ
            long wparam,  // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            out LVITEM lparam   // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessageW(
            HandleRef handle,  // メッセージを受け取るウィンドウのハンドル。(この関数によるメッセージの送り先となるウィンドウ)
            int msg,     // 送信するメッセージ
            long wparam,  // 追加のメッセージ固有情報（ウィンドウプロシージャのwParamパラメータ）
            [MarshalAs(UnmanagedType.LPWStr)] string lparam   // 追加のメッセージ固有情報（ウィンドウプロシージャのlParamパラメータ）
        );

        /// <summary>
        /// 指定したウィンドウに関する情報を取得する。
        /// 以下の値をnIndexに渡すと、渡した値に関する情報を取得できる。
        /// 例: GWL_STYLE（ウィンドウスタイルに関する情報）
        /// https://learn.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getwindowlongptra
        /// 
        /// ウィンドウスタイルに関する情報を取得すると、そのウィンドウがどのような性質を持っているかをビットフラグで返してくれる。
        /// 例: WS_DISABLED（ウィンドウが無効）, WS_TABSTOP（タブ移動できる）など
        /// 参考: ウィンドウスタイルの値
        /// https://learn.microsoft.com/en-us/windows/win32/winmsg/window-styles
        /// https://learn.microsoft.com/ja-jp/windows/win32/controls/edit-control-styles
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowLong(
            HandleRef hWnd,
            int nIndex
        );

        /// <summary>
        /// 指定したウィンドウのクラス名を取得する。
        /// ※文字列がShift_JISの文字コードで取得されてしまう。C#の内部文字コードはUTF16なのでそのまま扱うと文字化けする。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetClassName(
            HandleRef hwnd,
            [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpString,  // 取得するクラス名
            int nMaxCount
        );

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetClassName(
            HandleRef hwnd,
            byte[] lpString,  // 取得するクラス名
            int nMaxCount
        );

        /// <summary>
        /// 指定したウィンドウのクラス名を取得する。
        /// Wがついたwin32api関数はワイド文字（UTF16）で文字列を返す。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetClassNameW(
            HandleRef hwnd,
            [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpString,  // 取得するクラス名
            int nMaxCount
        );

        /// <summary>
        /// 指定したウィンドウの文字情報を引数lpStringに格納し、その文字数を返す。
        /// 指定したハンドルがトップウィンドウならウィンドウタイトルを返す。
        /// 指定したハンドルがラベルやテキストボックスなどの子ウィンドウならセットされている文字列を返す。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetWindowText(
            HandleRef hwnd,
            [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpString,  // 取得するウィンドウタイトル
            int nMaxCount     // 取得するタイトルの最大文字数
        );
        
        /// <summary>
        /// 指定したウィンドウのタイトルの文字数を取得する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength(
            HandleRef hWnd
        );

        /// <summary>
        /// 指定したハンドルのウィンドウが存在するならtrueを返す。
        /// ただし、ハンドルは異なるウィンドウで再利用されることがある。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(
            HandleRef hwnd
        );

        /// <summary>
        /// ウィンドウがキーやマウスで入力可能ならtrueを返す。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(
            HandleRef hwnd
        );

        /// <summary>
        /// 指定したウィンドウハンドルが親子関係か調べる。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool IsChild(
            HandleRef hParent,   // 親ウインドウのハンドル
            HandleRef hWnd       // 子ウインドウのハンドル
		);
        
        /// <summary>
        /// ウィンドウが最小化されている場合、0以外の値を返す。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool IsIconic(
            HandleRef hwnd
        );
        
        /// <summary>
        /// ウィンドウが最大化されている場合、0以外の値を返す。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool IsZoomed(
            HandleRef hwnd
        );
        
        /// <summary>
        /// ウィンドウの座標をスクリーン座標で取得する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool GetWindowRect(
            HandleRef handle,       // ウィンドウのハンドル
            out RECT lpRect         // ウィンドウの座標値
        );
        
        /// <summary>
        /// ウインドウの表示状態を調べます。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool IsWindowVisible(
            HandleRef handle
        );

        /// <summary>
        /// 文字列が選択されている開始位置と終了位置を取得する。
        /// </summary>
        public static (int, int) GetSelectedRange(HandleRef handle)
        {
            var result = SendMessage(handle, EM_GETSEL, 0, 0);

            var start = (int)(result & 0x0000FFFF);
            var end = (int)((result & 0xFFFF0000) >> 16);

            return (start, end);
        }

        /// <summary>
        /// 指定されたウィンドウのクライアント領域のDC（デバイスコンテキスト）へのハンドルを取得する。
        /// 失敗した場合はnullを返す。
        /// デバイスコンテキストは描画関係の処理を行うのに必要。
        /// 参考：https://learn.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getdc
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetDC(
            HandleRef handle    // ウィンドウハンドル
        );

        /// <summary>
        /// デバイスコンテキストを解放する。GetDCを呼び出したのと同じスレッドから呼び出す必要がある。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int ReleaseDC(
            HandleRef handle,   // ウィンドウハンドル
            IntPtr hdc          // デバイスコンテキストのハンドル
        );

        /// <summary>
        /// 指定したデバイスの座標の色を取得する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern uint GetPixel(
            IntPtr hdc,         // デバイスコンテキストのハンドル
            int x,
            int y
        );
    }
    
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
        
        public override string ToString()
        {
            return string.Format("left: {0} top: {1} right: {2} bottom: {3}", left, top, right, bottom);
        }
    }

    public struct LVITEM
    {
        public UInt32  mask;          // 有効メンバを示すフラグ
        public int   iItem;         // アイテムのインデックス
        public int   iSubItem;      // サブアイテムインデックス
        public UInt32  state;         // アイテムの状態・イメージ
        public UInt32  stateMask;     // state のフラグ
        public IntPtr  pszText;       // アイテムの文字列
        public int   cchTextMax;    // pszTextのサイズ
        public int   iImage;        // イメージのインデックス
        public IntPtr lParam;        // アイテムの持つ32ビット値
//#if (_WIN32_IE >= 0x0300)
        public int   iIndent;       // インデント
//#endif
//#if (_WIN32_IE >= 0x560)
        public int   iGroupId;      // 
        public UInt32  cColumns;      // tile view columns
        public IntPtr puColumns;     // 
//#endif
    }
}
