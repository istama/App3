using System;
using System.Runtime.InteropServices;

namespace IsTama.Utils
{
	/// <summary>
	/// ウィンドウのハンドルを探すWindowsAPIを集めたモジュールクラス。
	/// </summary>
	public static class NativeWindowHandleSearcher
	{
        /// <summary>
        /// 指定したウィンドウの親ウィンドウを取得する。
        /// 指定されたウィンドウがオーナーウィンドウを持つトップレベルウィンドウならオーナーウィンドウを返す。
        /// 指定されたウィンドウがオーナーウィンドウを持たないトップレベルウィンドウか、関数が失敗した場合は0(NULL)を返す。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetParent(
            HandleRef hwnd
        );

        /// <summary>
        /// フォアグラウンドのウィンドウのウィンドウハンドルを取得する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        
        /// <summary>
        /// 指定された名前のウィンドウのハンドルを取得する。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr FindWindow(
            string lpClassName,
            string lpWindowName
        );
        
        /// <summary>
        /// 指定したウィンドウの子ウィンドウのハンドルを取得する。
        ///  参考 https://msdn.microsoft.com/ja-jp/library/cc410853.aspx
        /// 子ウィンドウとはダイアログボックスのような子ウィンドウではなく、
        /// ウィンドウ内のTextBoxなどのコントロールのことを指す。
        /// なので孫ウィンドウとはコントロールの中にあるコントロールのことをいう。
        ///  参考 http://hp.vector.co.jp/authors/VA029438/level4/about/WIN_DOW.html
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(
            HandleRef hwndParent,      // 検索する子ウィンドウの親ウィンドウのハンドル トップウィンドウを検索するならNULLを渡す
            HandleRef hwndChildAfter,  // hwndParentの子ウィンドウのハンドル Ｚオーダーでこのウィンドウの次のウィンドウから検索が始まる NULLでもよい
            string lpszclass,
            string lpszwindow
        );

        /// <summary>
        /// 画面上の親を持たないウィンドウをすべて列挙する。
        /// ウィンドウが見つかるたびに、コールバック関数が呼び出される。
        /// コールバック関数の引数には見つかったウィンドウのハンドルと、この関数に渡したパラメータが渡される。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool EnumWindows(
            DelegateEnumWindowsCallback lpEnumFunc, // コールバック関数
            IntPtr lParam                           // コールバック関数の引数に渡す値
        );
        
        /// <summary>
        /// 指定したウィンドウの子ウィンドウをすべて列挙する。
        /// 子だけでなく、孫やまたその孫のウィンドウまで列挙される。
        /// ウィンドウが見つかるたびに、コールバック関数が呼び出される。
        /// コールバック関数の引数には見つかったウィンドウのハンドルと、この関数に渡したパラメータが渡される。
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool EnumChildWindows(
            HandleRef handle,
            DelegateEnumChildWindowsCallback lpEnumFunc,
            IntPtr lParam
            
        );
        
    }
    
    public delegate bool DelegateEnumWindowsCallback(IntPtr hwnd, IntPtr lParam);
    public delegate bool DelegateEnumChildWindowsCallback(IntPtr hwnd, IntPtr lParam);
}
