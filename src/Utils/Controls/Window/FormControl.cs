using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    sealed class FormControl : WindowBase
    {
        /// <summary>
        /// フォーカスが当たっているコントロールを取得する。
        /// wrapperはプラットフォーム呼び出し中にGCに回収されるのを防ぐ役割をもつオブジェクト。
        /// ファイナライズされたくないオブジェクトを渡せばよい。（thisでよい）
        /// </summary>
        public static FormControl GetFocusedControl(object wrapper = null)
        {
            FormControl control = null;
            NativeWindowThread.AttachMyWindowThreadToForeground(
                new HandleRef(null, IntPtr.Zero),
                (_) =>
                {
                    var hwnd = NativeWindowController.GetFocus();
                    control = new FormControl(new HandleRef(wrapper, hwnd));
                    return true;
                },
                string.Empty
                );

            return control;
        }

        private readonly HandleRef _handle;

        public FormControl(HandleRef handle) : base(handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// このコントロールのwindows内で扱われるクラス名を取得する。
        /// </summary>
        public string GetNativeClassName()
        {
            // Shift_JISのバイト列をUTF16に変換する方法
            //var bytes = new byte[512];
            //NativeWindowInformation.GetClassName(_handle, bytes, 512);
            //return Encoding.UTF8.GetString(bytes);

            var builder = new StringBuilder(512);
            NativeWindowInformation.GetClassNameW(_handle, builder, 512);
            return builder.ToString();
        }

        /// <summary>
        /// このコントロールの種類をC#のWindows Formsのクラス名に対応した名前で取得する。
        /// </summary>
        public string GetClassName()
        {
            var class_name = GetNativeClassName();

            // Windowsフォームの種類？バージョン？によって返してくるクラス名が異なる。
            // ButtonとRadioButton, CheckBoxが同じクラス名という問題がある
            if (class_name.Contains("BUTTON") || class_name.Contains("CommandButton"))
                return nameof(System.Windows.Forms.Button);
            if (class_name.Contains("CheckBox"))
                return nameof(System.Windows.Forms.CheckBox);
            if (class_name.Contains("EDIT") || class_name.Contains("TextBox"))
                return nameof(System.Windows.Forms.TextBox);
            if (class_name.Contains("COMBOBOX") || class_name.Contains("ComboBox"))
                return nameof(System.Windows.Forms.ComboBox);
            if (class_name.Contains("STATIC"))
                return nameof(System.Windows.Forms.Label);
            if (class_name.Contains(".Window."))
                return nameof(System.Windows.Forms.TabControl);
            if (class_name.Contains("TabControl"))
                return nameof(System.Windows.Forms.TabControl);

            return string.Empty;
        }

        /// <summary>
        /// 現在のウィンドウにフォーカスを当てる。
        /// </summary>
        public bool Focus()
        {
            // 親ウィンドウを取得してアクティブにする
            var parent = new WindowController(GetRootWindowHandle());
            if (!parent.Activate())
                return false;

            NativeWindowThread.AttachMyWindowThreadToForeground(
                _handle,
                (handle) =>
                {
                    NativeWindowController.SetFocus(handle);
                    return true;
                },
                _handle
                );

            return true;
        }

        /// <summary>
        /// 現在のウィンドウにフォーカスが当たっているならtrueを返す。
        /// </summary>
        public bool IsFocused()
        {
            var focused = FormControl.GetFocusedControl();

            return _handle.Handle == focused._handle.Handle;
        }

        /// <summary>
        /// このコントロールが表示されているならtrueを返す。
        /// </summary>
        public bool IsVisible()
        {
            return NativeWindowInformation.IsWindowVisible(_handle);
        }

        /// <summary>
        /// タブ移動可能なウィンドウならtrueを返す。
        /// </summary>
        public bool IsTabStop()
        {
            return HasWindowStyleOf(NativeWindowInformation.WS_TABATOP);
        }

        /// <summary>
        /// 読み込み専用なウィンドウならtrueを返す。
        /// </summary>
        public bool IsReadOnly()
        {
            return HasWindowStyleOf(NativeWindowInformation.ES_READONLY);
        }

        private bool HasWindowStyleOf(int style)
        {
            return (NativeWindowInformation.GetWindowLong(_handle, NativeWindowInformation.GWL_STYLE) & style) == style;
        }

        /// <summary>
        /// ウィンドウのTextプロパティに値をセットする。
        /// </summary>
        public void SetText(string text)
        {
            //NativeWindowInfomation.SendMessageA(_handle, NativeWindowInfomation.WM_SETTEXT, 0, text);
            NativeWindowInformation.SendMessage(_handle, NativeWindowInformation.WM_SETTEXT, 0, text);
        }

        /// <summary>
        /// 選択中の文字列をクリップボードにコピーする。
        /// </summary>
        public void Copy()
            => NativeWindowKeyInput.Copy(_handle);

        /// <summary>
        /// クリップボードの文字列をペーストする。
        /// </summary>
        public void Paste()
            => NativeWindowKeyInput.Paste(_handle);

        /// <summary>
        /// 文字列が選択されている開始位置と終了位置を取得する。
        /// </summary>
        public (int, int) GetSelectedRange()
            => NativeWindowInformation.GetSelectedRange(_handle);

        /// <summary>
        /// 選択されている範囲の文字列を取得する。
        /// </summary>
        public string GetSelectedText()
        {
            var (start, end) = GetSelectedRange();
            return base.GetText().SubStringByCharacterWidth(start, end - start);
        }

        /// <summary>
        /// このコントロールの親ウィンドウに対してのインデックスを取得する。
        /// </summary>
        public int GetIndex()
        {
            var parent_handle = GetParentWindowHandle();
            var parent_window = new WindowController(parent_handle);
            var index = 0;
            foreach (var control in parent_window.FindControls())
            {
                if (control == this)
                    return index;

                index += 1;
            }

            return -1;
        }

        /// <summary>
        /// コントロールの中心を左クリックする。
        /// </summary>
        public void LeftClick()
        {
            var size = GetSize();
            LeftClick(new Point(size.Width / 2, size.Height / 2));
        }

        /// <summary>
        /// コントロールの中心を右クリックする。
        /// </summary>
        public void RightClick()
        {
            var size = GetSize();
            RightClick(new Point(size.Width / 2, size.Height / 2));
        }

        /// <summary>
        /// コントロールの中心にマウスカーソルを移動する。
        /// </summary>
        public void MoveMouse()
        {
            var size = GetSize();
            MoveMouse(new Point(size.Width / 2, size.Height / 2));
        }

        ///// <summary>
        ///// Enterキーをウィンドウに送る。
        ///// </summary>
        //public bool SendEnterKey_old()
        //{
        //    if (!Focus())
        //        return false;
        //
        //    var key_down_lparam = CreateLParamToSendKeys(false, false, false);
        //    NativeWindowKeyInput.PostMessage(_handle, NativeWindowKeyInput.WM_KEYDOWN, NativeWindowKeyInput.VK_RETURN, key_down_lparam);
        //    var key_up_lparam = CreateLParamToSendKeys(false, true, true);
        //    NativeWindowKeyInput.PostMessage(_handle, NativeWindowKeyInput.WM_KEYUP, NativeWindowKeyInput.VK_RETURN, key_up_lparam);
        //
        //    return true;
        //}

        /// <summary>
        /// キー入力メッセージを送信するときに必要なLParamを生成する。
        /// </summary>
        /// <param name="alt_key_pressed">altキーが押されている状態で新たにキーメッセージを送信するならtrue</param>
        /// <param name="previous_key_pressed">前回のキーメッセージがキーを押下するメッセージならtrue</param>
        /// <param name="is_key_up_msg">このキーメッセージがキーを離すメッセージならtrue</param>
        private int CreateLParamToSendKeys(bool alt_key_pressed, bool previous_key_pressed, bool is_key_up_msg)
        {
            /*
             * キーボード入力の概要
             * https://docs.microsoft.com/ja-jp/windows/win32/inputdev/about-keyboard-input#keystroke-message-flags
             */

            var lparam = 0;
            // キーの繰り返し入力回数。（0-15ビット）
            // キーを処理するより早くキーを入力した場合（例えばキーを長押しした場合）、
            // 新たなキー入力情報がキューに投函されるのではなく、この値をインクリメントする。
            // キー入力を受け取った側はこの回数を見て繰り返し処理を行う。
            // WM_KEYUPメッセージを送る場合は、キーを離す操作を繰り返し行うことはできないので常に１が設定される。
            lparam |= 1 << 0;
            // スキャンコード。（16-23ビット）
            // ハードウェアに依存するキーの値。
            // 通常、アプリケーションはこの値を無視し、デバイスに依存しない仮想キーコードを使用してキー入力を解釈する。
            lparam |= 0 << 16;
            // 拡張キーコード（24ビット)
            // 拡張キーが送信された場合1をセットする。
            // 拡張キーとは主に、右側のCtrl, Shift, Altや、Insert, Delete, Home, End, PageUp, PageDown, 方向キー, テンキーのNumLock, /, Enter, PrintScreen, Break
            // などが該当する。
            // 拡張キーを指定した場合は、スキャンコードにプレフィクスバイト 0xE0 が付く。
            lparam |= 0 << 24;
            // コンテキストコード（29ビット）
            // キーストロークメッセージが生成されたときにALTキーが押されている場合は1をセット、押されてない場合は0をセットする。
            lparam |= (alt_key_pressed ? 1 : 0) << 29;
            // 前のキー状態フラグ（30ビット）
            // 前のキーストロークメッセージがキーをダウンした状態なら1をセットする。アップした状態なら0をセットする。
            // WM_KEYUPメッセージを送る場合は常に1をセットする。
            lparam |= (previous_key_pressed ? 1 : 0) << 30;
            // 遷移状態フラグ（31ビット）
            // WM_KEYDOWNメッセージを送る場合は常に0、WM_KEYUPメッセージを送る場合は常に1をセットする。
            lparam |= ((is_key_up_msg) ? 1 : 0) << 31;

            return lparam;
        }

        
    }
}
