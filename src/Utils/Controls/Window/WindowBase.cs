using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    /// <summary>
    /// ウィンドウを表すクラスの土台となるクラス。
    /// </summary>
    class WindowBase
    {
        /// <summary>
        /// 指定されたパターンにマッチするウィンドウタイトルのハンドルをすべて取得する。
        /// </summary>
        protected static IEnumerable<HandleRef> GetWindowHandlesByPatternMatch(string window_title_pattern)
        {
            var window_title_regex = new Regex(window_title_pattern, RegexOptions.Compiled);

            var handle = new HandleRef(0, IntPtr.Zero);
            var window_title = new StringBuilder(512);
            while (true)
            {
                // トップウィンドウを１つ取得 取得したhwndは次のトップウィンドウを取得するのに使われる
                var int_ptr = NativeWindowHandleSearcher.FindWindowEx(default, handle, null, null);
                // 取得できるウィンドウの終端に達した
                if (int_ptr == IntPtr.Zero)
                    break;

                handle = new HandleRef(null, int_ptr);
                // ウィンドウタイトルをwindow_titleに格納する
                NativeWindowInformation.GetWindowText(handle, window_title, 512);
                if (window_title_regex.IsMatch(window_title.ToString()))
                    yield return handle;

                window_title.Clear();
            }
        }

        private readonly HandleRef _handle;

        public WindowBase(HandleRef handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// ウィンドウが存在するならtrueを返す。
        /// ただし、１度破棄されて再度作成されたウィンドウは、このメソッドからは認識できない。
        /// </summary>
        public bool Exists()
            => NativeWindowInformation.IsWindow(_handle);

        /// <summary>
        /// ウィンドウが入力可能ならtrueを返す。
        /// </summary>
        public bool Enabled()
            => NativeWindowInformation.IsWindowEnabled(_handle);

        /// <summary>
        /// ウィンドウの文字列を返す。
        /// ウィンドウがトップウィンドウならウィンドウタイトルを返す。
        /// </summary>
        public string GetText()
        {
            var window_text = new StringBuilder(256);
            NativeWindowInformation.SendMessageW(_handle, NativeWindowInformation.WM_GETTEXT, window_text.Capacity, window_text);
            if (string.IsNullOrWhiteSpace(window_text.ToString()))
                NativeWindowInformation.GetWindowText(_handle, window_text, 256);

            return window_text.ToString();
        }

        /// <summary>
        /// 親ウィンドウのテキストを返す。
        /// </summary>
        private string GetParentWindowText()
        {
            var parent = new WindowBase(GetParentWindowHandle());
            return parent.GetText();
        }

        /// <summary>
        /// ルートのウィンドウのテキストを返す。
        /// </summary>
        private string GetRootWindowText()
        {
            var parent = new WindowBase(GetRootWindowHandle());
            return parent.GetText();
        }

        /// <summary>
        /// 自身のすべての子孫のウィンドウのテキストを取得する。
        /// </summary>
        protected List<HandleRef> GetChildWindowHandles()
        {
            var control_handles = new List<HandleRef>();

            bool f(IntPtr hwnd, IntPtr lParam)
            {
                control_handles.Add(new HandleRef(this, hwnd));
                return true;
            };
            var d = new DelegateEnumChildWindowsCallback(f);
            NativeWindowHandleSearcher.EnumChildWindows(_handle, d, IntPtr.Zero);

            return control_handles;
        }

        /// <summary>
        /// トップレベルの親ウィンドウを取得する。
        /// </summary>
        protected HandleRef GetParentWindowHandle()
        {
            var hwnd = NativeWindowHandleSearcher.GetParent(_handle);
            if (hwnd == IntPtr.Zero)
                return _handle;

            return new HandleRef(this, hwnd);
        }

        /// <summary>
        /// トップレベルの親ウィンドウを取得する。
        /// </summary>
        protected HandleRef GetRootWindowHandle()
        {
            var parent = _handle;
            do
            {
                var hwnd = NativeWindowHandleSearcher.GetParent(parent);
                if (hwnd == IntPtr.Zero)
                    break;
                parent = new HandleRef(this, hwnd);
            } while (true);

            return parent;
        }

        /// <summary>
        /// 指定したパターンがこのウィンドウのタイトルとマッチすればtrueを返す。
        /// </summary>
        public bool IsTextMatching(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Compiled);
            return regex.IsMatch(GetText());
        }

        /// <summary>
        /// 指定したパターンが親ウィンドウのテキストに含まれていればtrueを返す。
        /// </summary>
        public bool IsTextMatchingInParents(string pattern)
        {
            var regex = new Regex(pattern);
            var parent = _handle;
            do
            {
                var hwnd = NativeWindowHandleSearcher.GetParent(parent);
                if (hwnd == IntPtr.Zero)
                    return false;

                parent = new HandleRef(this, hwnd);

                var parent_window = new WindowBase(parent);
                if (regex.IsMatch(parent_window.GetText()))
                    break;

            } while (true);

            return true;
        }

        /// <summary>
        /// 指定したウィンドウタイトルのウィンドウがこのインスタンスのウィンドウが保持するウィンドウの１つならtrueを返す。
        /// </summary>
        public bool HasWindow(string window_title_pattern)
        {
            var has = false;
            UInt32 my_thread_id = NativeWindowThread.GetWindowThreadProcessId(_handle, IntPtr.Zero);
            foreach (var handle in GetWindowHandlesByPatternMatch(window_title_pattern))
            {
                uint thread_id = NativeWindowThread.GetWindowThreadProcessId(handle, IntPtr.Zero);
                if (thread_id == my_thread_id)
                {
                    has = true;
                    break;
                }
            }
            return has;
        }

        /// <summary>
        /// ウィンドウの上下左右の座標を取得する。
        /// </summary>
        public Rectangle GetRectangleByScreenPoint()
        {
            var rect = new RECT();
            NativeWindowInformation.GetWindowRect(_handle, out rect);

            return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
        }

        /// <summary>
        /// ウィンドウの左上のスクリーン座標を取得する。
        /// </summary>
        public Point GetScreenPoint()
        {
            var rect = new RECT();
            NativeWindowInformation.GetWindowRect(_handle, out rect);

            return new Point(rect.left, rect.top);
        }

        /// <summary>
        /// ウィンドウのサイズを取得する。
        /// </summary>
        public Size GetSize()
        {
            var rect = GetRectangleByScreenPoint();
            return new Size
            {
                Width = rect.Width,
                Height = rect.Height
            };
        }

        /// <summary>
        /// 親であるフレームウィンドウの左上とした、ウィンドウの座標とサイズをまとめた情報を取得する。
        /// </summary>
        public Rectangle GetRectangleByRelativePoint()
        {
            return new Rectangle(GetRelativePoint(), GetSize());
        }

        /// <summary>
        /// 親であるフレームウィンドウの左上を基点としたこのウィンドウの左上の相対座標を取得する。
        /// このウィンドウがフレームウィンドウである場合は0,0を返す。
        /// </summary>
        public Point GetRelativePoint()
        {
            var parent_handle = GetRootWindowHandle();
            var parent_window = new WindowBase(parent_handle);
            var parent_point  = parent_window.GetScreenPoint();
            var my_point      = GetScreenPoint();

            return new Point(my_point.X - parent_point.X, my_point.Y - parent_point.Y);
        }

        /// <summary>
        /// 指定した座標の色を返す。
        /// 備考：うまく色を取得できない。ButtonやLabelの色を取得するには、ButtonやLabelのハンドルをGetPixel()に渡す必要があるかも。
        /// </summary>
        public Color _GetPixelRGB(Point p)
        {
            var size = GetSize();
            if (p.X < 0 || p.X > size.Width || p.Y < 0 || p.Y > size.Height)
                throw new ArgumentException($"指定した座標はウィンドウの範囲外です。 / {p.ToString()}");

            var hdc = NativeWindowInformation.GetDC(_handle);
            if (hdc == null)
                throw new InvalidOperationException("デバイスコンテキストのハンドルの取得に失敗しました。");

            try
            {
                var color_value = NativeWindowInformation.GetPixel(hdc, p.X, p.Y);
                // 座標がクリップ領域の範囲外だった場合
                if (color_value == 0xFFFFFFFF)
                    return Color.Empty;

                var red = (byte)(color_value & 0xFF);
                var green = (byte)((color_value >> 8) & 0xFF);
                var blue = (byte)((color_value >> 16) & 0xFF);

                return Color.FromArgb(red, green, blue);
            }
            finally
            {
                NativeWindowInformation.ReleaseDC(_handle, hdc);
            }
        }

        private Bitmap _window_bitmap_buffer = null;

        public Color GetPixelRGB(Point p, bool recapture)
        {
            var size = GetSize();
            if (p.X < 0 || p.X > size.Width || p.Y < 0 || p.Y > size.Height)
                throw new ArgumentException($"指定した座標はウィンドウの範囲外です。 / {p.ToString()}");

            if (!recapture && _window_bitmap_buffer != null && _window_bitmap_buffer.Size == size)
            {
                return _window_bitmap_buffer.GetPixel(p.X, p.Y);
            }

            _window_bitmap_buffer = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(_window_bitmap_buffer))
            {
                // 画面をキャプチャしてBitmapに保存する
                var screen_point = GetScreenPoint();
                g.CopyFromScreen(screen_point, new Point(0, 0), size);

                return _window_bitmap_buffer.GetPixel(p.X, p.Y);
            }

            // ver1.0 動作が遅い？
            //using (var bitmap = new Bitmap(1, 1))
            //{
            //    using (var graphics = Graphics.FromImage(bitmap))
            //    {
            //        var screen_point = GetScreenPoint();
            //        var target_point = new Point(screen_point.X + p.X, screen_point.Y + p.Y);
            //        graphics.CopyFromScreen(target_point, new Point(0, 0), new Size(1, 1));
            //
            //        return bitmap.GetPixel(0, 0);
            //    }
            //}
        }

        /// <summary>
        /// ウィンドウをキャプチャしたビットマップを返す。
        /// </summary>
        public Bitmap CaptureWindowBitmap()
        {
            var size = GetSize();
            var bitmap = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                // 画面をキャプチャしてBitmapに保存する
                var screen_point = GetScreenPoint();
                g.CopyFromScreen(screen_point, new Point(0, 0), size);
                return bitmap;
            }
        }

        /// <summary>
        /// 指定した座標にマウスカーソルを移動する。
        /// 座標はウィンドウの相対座標で指定する。
        /// </summary>
        public void MoveMouse(Point point)
            => SendMouseInput(GetMouseMoveData(point));
        /// <summary>
        /// 左マウスを押す。
        /// </summary>
        public void PressLeftMouse(Point point)
            => SendMouseInput(GetMouseMoveData(point), GetLeftMousePressData());
        /// <summary>
        /// 左マウスを離す。
        /// </summary>
        public void ReleaseLeftMouse(Point point)
            => SendMouseInput(GetMouseMoveData(point), GetLeftMouseReleaseData());
        /// <summary>
        /// 指定した座標を左クリックする。
        /// 座標はウィンドウの相対座標で指定する。
        /// </summary>
        public void LeftClick(Point point)
            => SendMouseInput(GetMouseMoveData(point), GetLeftMousePressData(), GetLeftMouseReleaseData());
        /// <summary>
        /// 右マウスを押す。
        /// </summary>
        public void PressRightMouse(Point point)
            => SendMouseInput(GetMouseMoveData(point), GetRightMousePressData());
        /// <summary>
        /// 右マウスを離す。
        /// </summary>
        public void ReleaseRightMouse(Point point)
            => SendMouseInput(GetMouseMoveData(point), GetRightMouseReleaseData());
        /// <summary>
        /// 指定した座標を右クリックする。
        /// 座標はウィンドウの相対座標で指定する。
        /// </summary>
        public void RightClick(Point point)
            => SendMouseInput(GetMouseMoveData(point), GetRightMousePressData(), GetRightMouseReleaseData());
        /// <summary>
        /// 中マウスを押す。
        /// </summary>
        public void PressMiddleMouse(Point point)
            => SendMouseInput(GetMouseMoveData(point), GetMiddleMousePressData());
        /// <summary>
        /// 中マウスを離す。
        /// </summary>
        public void ReleaseMiddleMouse(Point point)
            => SendMouseInput(GetMouseMoveData(point), GetMiddleMouseReleaseData());
        /// <summary>
        /// 指定した座標を中クリックする。
        /// 座標はウィンドウの相対座標で指定する。
        /// </summary>
        public void MiddleClick(Point point)
            => SendMouseInput(GetMouseMoveData(point), GetMiddleMousePressData(), GetMiddleMouseReleaseData());
        /// <summary>
        /// 指定した座標から座標まで左ドラッグする。
        /// 座標はウィンドウの相対座標で指定する。
        /// </summary>
        public void LeftDrag(Point start_point, Point end_point, int interval_milliseconds)
        {
            SendMouseInput(GetMouseMoveData(start_point), GetLeftMousePressData());
            Thread.Sleep(interval_milliseconds);
            SendMouseInput(GetMouseMoveData(end_point), GetLeftMouseReleaseData());
        }
        /// <summary>
        /// 指定した座標から座標まで右ドラッグする。
        /// 座標はウィンドウの相対座標で指定する。
        /// </summary>
        public void RightDrag(Point relative_start_point, Point relative_end_point, int interval_milliseconds)
        {
            SendMouseInput(GetMouseMoveData(relative_start_point), GetRightMousePressData());
            Thread.Sleep(interval_milliseconds);
            SendMouseInput(GetMouseMoveData(relative_end_point), GetRightMouseReleaseData());
        }
        /// <summary>
        /// 指定した座標から座標まで中ドラッグする。
        /// 座標はウィンドウの相対座標で指定する。
        /// </summary>
        public void MiddleDrag(Point start_point, Point end_point, int interval_milliseconds)
        {
            SendMouseInput(GetMouseMoveData(start_point), GetMiddleMousePressData());
            Thread.Sleep(interval_milliseconds);
            SendMouseInput(GetMouseMoveData(end_point), GetMiddleMouseReleaseData());
        }
        /// <summary>
        /// 指定した座標でマウスを奥へホイールする。
        /// 座標はウィンドウの相対座標で指定する。
        /// </summary>
        public void UpScrollMouseWheel(Point point, int scroll_count)
            => SendMouseInput(GetMouseMoveData(point), GetMouseWheelData(scroll_count));
        /// <summary>
        /// 指定した座標でマウスを手前へホイールする。
        /// 座標はウィンドウの相対座標で指定する。
        /// </summary>
        public void DownScrollMouseWheel(Point point, int scroll_count)
            => SendMouseInput(GetMouseMoveData(point), GetMouseWheelData(scroll_count * -1));

        /// <summary>
        /// Windowsにマウスイベントを送信する。
        /// </summary>
        private void SendMouseInput(params NativeWindowMouseInput.INPUT[] mouse_input_data_array)
        {
            Assert.IsNull(mouse_input_data_array, nameof(mouse_input_data_array));
            Assert.IsNot(mouse_input_data_array.Length > 0, $"{nameof(mouse_input_data_array)} の長さが0です。");

            NativeWindowMouseInput.SendInput(mouse_input_data_array.Length, mouse_input_data_array, Marshal.SizeOf(mouse_input_data_array[0]));
        }

        /// <summary>
        /// 指定座標にマウスカーソルを動かすためのマウスイベントデータを取得する。
        /// </summary>
        private NativeWindowMouseInput.INPUT GetMouseMoveData(Point relative_point)
        {
            // このウィンドウの絶対座標を取得する
            var window_point = this.GetScreenPoint();

            // 相対座標座標を絶対座標に変換
            int x = window_point.X + relative_point.X;
            int y = window_point.Y + relative_point.Y;
            Point p = this.ConvertToAbsolutePointFrom(new Point(x, y));

            var input = NewMouseData(NativeWindowMouseInput.MOUSEEVENTF_MOVE | NativeWindowMouseInput.MOUSEEVENTF_ABSOLUTE);
            // 座標を設定
            input.m.dx = p.X;
            input.m.dy = p.Y;

            return input;
        }

        private NativeWindowMouseInput.INPUT GetLeftMousePressData()
            => NewMouseData(NativeWindowMouseInput.MOUSEEVENTF_LEFTDOWN);
        private NativeWindowMouseInput.INPUT GetLeftMouseReleaseData()
            => NewMouseData(NativeWindowMouseInput.MOUSEEVENTF_LEFTUP);
        private NativeWindowMouseInput.INPUT GetRightMousePressData()
            => NewMouseData(NativeWindowMouseInput.MOUSEEVENTF_RIGHTDOWN);
        private NativeWindowMouseInput.INPUT GetRightMouseReleaseData()
            => NewMouseData(NativeWindowMouseInput.MOUSEEVENTF_RIGHTUP);
        private NativeWindowMouseInput.INPUT GetMiddleMousePressData()
            => NewMouseData(NativeWindowMouseInput.MOUSEEVENTF_MIDDLEDOWN);
        private NativeWindowMouseInput.INPUT GetMiddleMouseReleaseData()
            => NewMouseData(NativeWindowMouseInput.MOUSEEVENTF_MIDDLEUP);

        /// <summary>
        /// マウスをホイールするためのデータを生成する。
        /// 正の値はホイールが前方（ユーザーから離れた方向）へ、負の値はホイールが後方（ユーザーの方向）へ回転したことを示す。
        /// </summary>
        private NativeWindowMouseInput.INPUT GetMouseWheelData(int amount)
        {
            var input = NewMouseData(NativeWindowMouseInput.MOUSEEVENTF_WHEEL);
            input.m.mouseData = amount;

            return input;
        }

        /// <summary>
        /// 空のマウスデータを作成する。
        /// </summary>
        private NativeWindowMouseInput.INPUT NewMouseData(int event_flags)
        {
            var input = new NativeWindowMouseInput.INPUT();
            // マウスイベントを設定
            input.type = NativeWindowMouseInput.INPUT_MOUSE;
            // マウスイベントの種類
            input.m.dwFlags = event_flags;
            // 座標を設定
            input.m.dx = 0;
            input.m.dy = 0;
            // その他データ
            input.m.mouseData = 0;
            input.m.dwExtraInfo = IntPtr.Zero;
            // デフォルトの設定
            input.m.time = 0;

            return input;
        }

        /// <summary>
        /// 相対座標を絶対座標に変換する。
        /// </summary>
        private Point ConvertToAbsolutePointFrom(Point relative_point)
        {
            /*
             * <参考: 巷にあふれているSendInput(Windows API)の座標変換のソースコードに物申す>
             * https://qiita.com/kob58im/items/23df9e22778b33986d1c
             */
            int dx = Screen.PrimaryScreen.Bounds.Width;
            int dy = Screen.PrimaryScreen.Bounds.Height;
            //int x = (relative_point.X * 65536 + dx - 1) / dx;
            //int y = (relative_point.Y * 65536 + dy - 1) / dy;
            int x = (relative_point.X * 65536) / dx;
            int y = (relative_point.Y * 65536) / dy;
            //int x = relative_point.X * (65535 / dx);
            //int y = relative_point.Y * (65535 / dy);

            return new Point(x, y);
        }

        /// <summary>
        /// 絶対座標を座標に変換する。
        /// </summary>
        private Point ConvertToRelativePointForm(Point absolute_point)
        {
            int dx = Screen.PrimaryScreen.Bounds.Width;
            int dy = Screen.PrimaryScreen.Bounds.Height;
            int x = (absolute_point.X * dx) / 65535;
            int y = (absolute_point.Y * dy) / 65535;
            
            return new Point(x, y);
        }

        //public MouseHook GetMouseHook()
        //    => new MouseHook(_handle);

        /// <summary>
        /// 現在のウィンドウの中の指定した座標のイメージをキャプチャして返す。
        /// </summary>
        public Bitmap GetBitmap(Point start, Size size)
        {
            // ウィンドウの絶対座標を取得する
            var window_point = this.GetScreenPoint();

            // 相対座標座標を絶対座標に変換
            int sx = window_point.X + start.X;
            int sy = window_point.Y + start.Y;
            var absolute_start = new Point(sx, sy);

            //var r   = new Rectangle(absolute_start.X, absolute_start.Y, size.Width, size.Height);
            var bmp = new Bitmap(size.Width, size.Height);
            //System.Windows.Forms.MessageBox.Show(r.Size.ToString() + " " + r.Width.ToString() + " " + r.Height.ToString());

            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(absolute_start, new Point(0, 0), size/*r.Size*/);
            }

            return bmp;
        }

        public static bool operator ==(WindowBase a, WindowBase b)
        {
            // aとbが同じインスタンスならtrueを返す。
            // ここで==メソッドを使ってしまうとoverrideしたこの==メソッドで無限ループしてしまう。
            if (object.ReferenceEquals(a, b))
                return true;

            return a.Equals(b);
        }
        public static bool operator !=(WindowBase a, WindowBase b)
            => !(a == b);

        public override bool Equals(object obj)
            => obj is WindowBase other && Equals(other);
        public bool Equals(WindowBase other)
            => other != null && _handle.Handle.Equals(other._handle.Handle);

        public override int GetHashCode()
            => _handle.GetHashCode();
    }
}
