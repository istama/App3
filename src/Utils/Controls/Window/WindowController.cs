using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    sealed class WindowController : WindowBase
    {
        public static bool IsActivated(string window_title_pattern)
        {
            var windows = FindAll(window_title_pattern, 0, w => w.IsActivated());
            return windows.Count > 0;
        }

        public static bool TryGetActiveWindow(int waittime_ms, out WindowController window)
        {
            window = default;

            var windows = FindAll(".*", waittime_ms, w => w.IsActivated());
            if (windows.Count == 0)
                return false;

            window = windows[0];
            return true;
        }

        public static List<WindowController> FindAll(string window_title_pattern, int waittime_ms)
            => FindAll(window_title_pattern, waittime_ms, null);

        public static List<WindowController> FindAll(string window_title_pattern, int waittime_ms, Func<WindowController, bool> condition)
        {
            Assert.IsNull(window_title_pattern, nameof(window_title_pattern));

            List<WindowController> window_list;

            if (condition == null)
                condition = (w) => true;

            var timer = new Stopwatch();
            timer.Start();

            do
            {
                window_list = WindowBase
                    .GetWindowHandlesByPatternMatch(window_title_pattern)
                    .Select(handle => new WindowController(handle))
                    .Where(window =>
                    {
                        var size = window.GetSize();
                        if (size.Height <= 0 || size.Width <= 0 || !window.IsOpen())
                            return false;

                        return condition(window);
                    })
                    .ToList();
            } while (window_list.Count == 0 && timer.ElapsedMilliseconds < waittime_ms);

            timer.Stop();

            return window_list;
        }

        private readonly HandleRef _handle;

        public WindowController(HandleRef handle) : base(handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// ウィンドウをトップに持ってくる。
        /// ただし、最小化されているウィンドウは最小化されたまま。
        /// 成功するとtrueを返す。
        /// </summary>
        public bool ToTopPos()
        {
            // SetWindowPosの解説
            //  https://msdn.microsoft.com/ja-jp/library/cc411206.aspx?f=255&MSPPError=-2147217396
            // SetWindowPos()関数はウィンドウを最前面に持ってくるが、
            // 最小化されているウィンドウを表示状態にしたりはしない。
            // 表示するウィンドウのサイズや位置を指定できるのが利点
            return NativeWindowController.SetWindowPos(
                this._handle,
                NativeWindowController.HANDLE_TOP,
                0, 0, 0, 0,
                NativeWindowController.SWP_SHOWWINDOW | NativeWindowController.SWP_NOMOVE | NativeWindowController.SWP_NOSIZE
            );
        }

        /// <summary>
        /// ウィンドウをアクティブにする。
        /// </summary>
        public bool Activate()
        {
            // ウィンドウが最小化されている場合は元に戻す
            if (IsMinimum())
                Unminimized();

            return NativeWindowThread.AttachMyWindowThreadToForeground(_handle,
                (handle) =>
                {
                    // このウィンドウを最前面に移動する
                    if (!ToTopPos())
                        return false;

                    // このウィンドウにフォーカスを当てる
                    NativeWindowController.SetFocus(handle);
                    return true;
                },
                _handle);
        }

        /// <summary>
        /// ウィンドウがアクティブ状態（フォーカスが当たっている状態）ならtrueを返す。
        /// </summary>
        public bool IsActivated()
            => _handle.Handle == NativeWindowController.GetForegroundWindow();

        /// <summary>
        /// ウィンドウが開かれている（最小化状態も含む）ならtrueを返す。
        /// ただし、コントロールがShowDialog()で開かれていてフォーカスを当てることができないウィンドウはfalseを返す。
        /// </summary>
        public bool IsOpen()
            => NativeWindowInformation.IsWindowVisible(_handle);

        /// <summary>
        /// ウィンドウを閉じる。
        /// </summary>
        public bool Close()
        {
            return NativeWindowThread.AttachMyWindowThreadToForeground(
                _handle,
                (handle) => NativeWindowController.DestroyWindow(handle),
                _handle);
        }

        /// <summary>
        /// ウィンドウが最小化されている場合はtrueを返す。
        /// </summary>
        public bool IsMinimum()
            => NativeWindowInformation.IsIconic(_handle);

        /// <summary>
        /// ウィンドウの最小化を解除する。
        /// </summary>
        public void Unminimized()
        {
            if (IsMinimum())
                NativeWindowController.ShowWindow(_handle, NativeWindowController.SW_RESTORE);
        }

        /// <summary>
        /// ウィンドウがビジー状態ならtrueを返す。
        /// </summary>
        public bool IsBusy()
        {
            var pid = NativeWindowThread.GetWindowThreadProcessId(_handle, IntPtr.Zero);
            var process = Process.GetProcessById((int) pid);
            return !process.Responding;
        }

        /// <summary>
        /// コントロールをすべて取得する。
        /// </summary>
        /// <returns></returns>
        public List<FormControl> FindControls(Func<FormControl, bool> condition = null)
        {
            return GetChildWindowHandles()
                .Select(h => new FormControl(h))
                .Where(c => condition == null || condition(c))
                .ToList();
        }

        /// <summary>
        /// 指定したインデックスにあるコントロールを取得する。
        /// </summary>
        public FormControl GetControl(int control_index)
        {
            if (control_index < 0)
                throw new IndexOutOfRangeException($"コントロールのインデックスの指定が範囲外です。 {control_index}");

            var control_handles = GetChildWindowHandles();

            if (control_index >= control_handles.Count)
                throw new IndexOutOfRangeException($"コントロールのインデックスの指定が範囲外です。 {control_index}");

            return new FormControl(control_handles[control_index]);
        }

        /// <summary>
        /// 指定したパターンに一致するテキストを持つコントロールを取得する。
        /// </summary>
        public List<FormControl> GetFormControls(string control_text_pattern, Func<FormControl, bool> condition = null)
        {
            var pattern = new Regex(control_text_pattern ?? string.Empty);
            return GetChildWindowHandles()
                .Select(h => new FormControl(h))
                .Where(w => pattern.IsMatch(w.GetText()))
                .Where(c => condition == null || condition(c))
                .ToList();
        }

        /// <summary>
        /// 指定した座標のコントロールを取得する。
        /// </summary>
        public List<FormControl> GetFormControls(Point p, Func<FormControl, bool> condition = null)
        {
            return GetChildWindowHandles()
                .Select(h => new FormControl(h))
                .Where(c => c.GetRelativePoint().Equals(p))
                .Where(c => condition == null || condition(c))
                .ToList();
        }

        /// <summary>
        /// 指定した座標とサイズのコントロールを取得する。
        /// </summary>
        public List<FormControl> GetControls(Point p, Size s, Func<FormControl, bool> condition = null)
        {
            return GetChildWindowHandles()
                .Select(h => new FormControl(h))
                .Where(c => c.GetRelativePoint().Equals(p) && c.GetSize().Equals(s))
                .Where(c => condition == null || condition(c))
                .ToList();
        }

        /// <summary>
        /// 指定した座標とサイズのコントロールを取得する。
        /// </summary>
        public List<FormControl> GetControls(Rectangle r, Func<FormControl, bool> condition = null)
        {
            return GetChildWindowHandles()
                .Select(h => new FormControl(h))
                .Where(c => c.GetRelativePoint().Equals(new Point(r.X, r.Y)) && c.GetSize().Equals(new Size(r.Width, r.Height)))
                .Where(c => condition == null || condition(c))
                .ToList();
        }

        /// <summary>
        /// IMEモードを設定する。
        /// </summary>
        public void SetImeMode(ImeModes imeModes)
        {

            // 入力コンテキストを取得する
            var himc = NativeWindowIme.ImmGetContext(_handle.Handle);

            //if (IsOpened())
            //    System.Windows.Forms.MessageBox.Show("opened");
            //if (IsActivated())
            //    System.Windows.Forms.MessageBox.Show("activated");
            //
            //System.Windows.Forms.MessageBox.Show(NativeWindowIme.ImmGetContext(_handle.Handle).ToString());

            if (himc != IntPtr.Zero /*&& NativeWindowIme.ImmGetOpenStatus(himc)*/)
            {
                System.Windows.Forms.MessageBox.Show("set ime modes");
                // IMEの設定を変更する
                var ime_code = imeModes.ToImeCode();
                NativeWindowIme.ImmSetConversionStatus(himc, ime_code, 0);
                // 入力コンテキストを解放する
                NativeWindowIme.ImmReleaseContext(_handle, himc);
            }
        }

        /// <summary>
        /// IMEモードを設定する。
        /// </summary>
        public ImeModes GetImeModes()
        {
            // 入力コンテキストを取得する
            var himc = NativeWindowIme.ImmGetContext(_handle.Handle);

            if (himc != IntPtr.Zero && NativeWindowIme.ImmGetOpenStatus(himc))
            {
                // IMEの設定を変更する
                NativeWindowIme.ImmGetConversionStatus(himc, out var ime_code, out _);
                // 入力コンテキストを解放する
                NativeWindowIme.ImmReleaseContext(_handle, himc);

                return ImeModesConverters.ToImeModesFrom(ime_code);
            }

            // 既定値を返す
            return ImeModes.AlphaNumeric;
        }

        /// <summary>
        /// IMEが有効ならtrueを返す。
        /// </summary>
        private bool ImeEnabled()
            => NativeWindowIme.GetSystemMetrics(NativeWindowIme.SM_IMMENABLED) != 0;

        public bool Equals(WindowController other)
            => base.Equals(other);
    }
}
