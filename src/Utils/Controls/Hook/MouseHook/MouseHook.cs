using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    /// <summary>
    /// マウスフックを行うクラス。
    /// マウス操作をフックしたときの通知方法をプロパティ設定できる。
    /// PostWindowMessageが標準的な方法だが、マウス操作が必ずOSに返されるため、マウスを操作したときの既定の動作を止めることができない。
    /// SendWindowMessageやRaiseEventでは、EventArgs.Handledにtrueを渡せば、マウスをOSに渡さないようにできる。
    /// しかし、それによってマウス操作自体が不安定になる場合もある。
    /// 参考
    ///  https://learn.microsoft.com/ja-jp/windows/win32/winmsg/about-hooks#wh_mouse_ll
    ///  https://learn.microsoft.com/ja-jp/windows/win32/winmsg/lowlevelkeyboardproc
    ///  https://learn.microsoft.com/ja-jp/windows/win32/winmsg/using-hooks
    ///  https://learn.microsoft.com/ja-jp/windows/win32/winmsg/mouseproc
    /// </summary>
    class MouseHook : MouseHookBase
    {
        public event EventHandler<MouseHookEventArgs> MouseActive;


        private HandleRef _windowHandle;

        public MouseHookMessagingMode MessagingModeOnLClick { get; set; } = MouseHookMessagingMode.Post;
        public MouseHookMessagingMode MessagingModeOnMClick { get; set; } = MouseHookMessagingMode.Post;
        public MouseHookMessagingMode MessagingModeOnRClick { get; set; } = MouseHookMessagingMode.Post;
        public MouseHookMessagingMode MessagingModeOnWheel  { get; set; } = MouseHookMessagingMode.Post;

        private readonly MouseWindowMessageProcedures _procedures;


        public MouseHook(MouseWindowMessageProcedures procedures) : this(new HandleRef(null, IntPtr.Zero), procedures)
        {
        }
        public MouseHook(HandleRef windowHandle, MouseWindowMessageProcedures procedures)
        {
            Assert.IsNull(procedures, nameof(procedures));

            _windowHandle = windowHandle;
            _procedures   = procedures;

            _procedures.MouseActive += ProceduresMouseActive;
        }


        /// <summary>
        /// ウィンドウメッセージの送信先のウィンドウのハンドルをセットする。
        /// </summary>
        public void SetWindowHandle(HandleRef windowHandle)
        {
            _windowHandle = windowHandle;
        }

        /// <summary>
        /// キー入力されると呼び出される。
        /// 参照：https://learn.microsoft.com/ja-jp/windows/win32/inputdev/wm-lbuttondblclk
        /// このプロシージャが呼び出された以降の処理を一定時間以内に完了しないと、
        /// マウスフックは強制的にアンフックされる。
        /// </summary>
        protected override IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // 入力情報をカスタムのマウスウィンドウメッセージに変換する
            if (TryConvertToCustomMouseWindowMessage(nCode, wParam, lParam, out uint customMouseWindowMessage))
            {
                var messaging_mode = GetMouseHookMessagingMode(customMouseWindowMessage);

                if (messaging_mode == MouseHookMessagingMode.None)
                {
                    return base.HookProcedure(nCode, wParam, lParam);
                }

                if (messaging_mode == MouseHookMessagingMode.Post)
                {
                    return PostWindowMessage(nCode, wParam, lParam, customMouseWindowMessage);
                }

                if (messaging_mode == MouseHookMessagingMode.Send)
                {
                    return SendWindowMessage(nCode, wParam, lParam, customMouseWindowMessage);
                }
            }

            return base.HookProcedure(nCode, wParam, lParam);
        }

        /// <summary>
        /// カスタムウィンドウメッセージに変換する。
        /// </summary>
        private Boolean TryConvertToCustomMouseWindowMessage(int nCode, IntPtr wParam, IntPtr lParam, out uint customMouseWindowMessage)
        {
            customMouseWindowMessage = default;

            if (nCode < 0)
                return false;

            // ウィンドウメッセージを取得
            var wmsg = wParam.ToInt32();

            // ホイールの回転方向を判定する
            if (TryConvertToMouseWheelWindowMessageFrom(wmsg, lParam, out customMouseWindowMessage))
            {
                return true;
            }

            if (MouseWindowMessage.TryConvertToCustomMouseWindowMessageFrom(wmsg, out customMouseWindowMessage))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// マウスホイールの回転方向を示すウィンドウメッセージを取得する。
        /// </summary>
        private bool TryConvertToMouseWheelWindowMessageFrom(int wmsg, IntPtr lParam, out uint customMouseWindowMessage)
        {
            if (wmsg != NativeWindowMouseHook.WM_MOUSEWHEEL)
            {
                customMouseWindowMessage = default;
                return false;
            }

            // ホイールの回転方向を取得する
            // なおこの処理はPostMessage()で飛ばした先で行っても上手くいかない
            var structure = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
            var delta = (int)structure.mouseData >> 16;

            customMouseWindowMessage = delta > 0
                ? MouseWindowMessage.WHEEL_UP
                : MouseWindowMessage.WHEEL_DOWN;

            return true;
        }

        /// <summary>
        /// 引数のウィンドウメッセージが使うべきマウスフックの動作モードを返す。
        /// </summary>
        private MouseHookMessagingMode GetMouseHookMessagingMode(uint customMouseWindowMessage)
        {
            if (MouseWindowMessage.IsWindowMessageMove(customMouseWindowMessage))
            {
                return MouseHookMessagingMode.None;
            }

            if (MouseWindowMessage.IsWindowMessageLButton(customMouseWindowMessage))
            {
                return MessagingModeOnLClick;
            }

            if (MouseWindowMessage.IsWindowMessageMButton(customMouseWindowMessage))
            {
                return MessagingModeOnMClick;
            }

            if (MouseWindowMessage.IsWindowMessageRButton(customMouseWindowMessage))
            {
                return MessagingModeOnRClick;
            }

            if (MouseWindowMessage.IsWindowMessageWheel(customMouseWindowMessage))
            {
                return MessagingModeOnWheel;
            }

            return MouseHookMessagingMode.None;
        }

        /// <summary>
        /// カスタムのマウスウィンドウメッセージをメッセージループにポストする。
        /// </summary>
        private IntPtr PostWindowMessage(int nCode, IntPtr wParam, IntPtr lParam, uint customMouseWindowMessage)
        { 
            //NativeWindowSubWindows.PostMessage();
            // 受け取ったマウス情報はPostMessage()に渡す
            // PostMessage()の第一引数にはメッセージを送信するウィンドウのハンドルを渡す。
            // ウィンドウハンドルはFindWindow()などで取得したものでよい。
            // マウスフックはそこからメッセージループで処理しないといけない（？）
            //
            // WH_MOUSE_LLはすべてのマウスイベントをフックする
            // WH_MOUSEは特定のウィンドウのマウスイベントをフックする
            // https://qiita.com/yasunari_matsuo/items/a1d294f09ac21e9508b6

            // C#でウィンドウズメッセージを受け取るコード
            // https://www.ipentec.com/document/csharp-visual-component-get-windows-message
            // public class MyForm : Form
            // {
            //     protected override void WndProc(ref Message m)
            //     {
            //         if (m.Msg == WM_MY_MESSAGE)
            //         {
            //             // メッセージを受信したときの処理
            //         }
            //         base.WndProc(ref m);
            //     }
            // 
            //     private const int WM_MY_MESSAGE = 0x8000; // カスタムメッセージのID
            // }

            if (_windowHandle.Handle == IntPtr.Zero)
                return base.HookProcedure(nCode, wParam, lParam);

            // このプロシージャが呼び出されたら即座にPostMessage()を呼び出すべき。
            // このクラスのインスタンスでマウス操作に対応したイベントハンドラを呼び出すべきではない。
            // なぜなら、一定時間内にこのプロシージャの処理を完了させないと、マウスフックが強制的に解除されるから。
            // PostMessage()を呼び出せば、命令はキューに渡されるのでこのプロシージャは即座に処理を完了でき、
            // 後続のマウス操作にも影響を与えずにすむ。
            // （ただし、メッセージループでの処理が高負荷だと動作に影響を与える）

            UIThreadMethodInvoker.Invoke(() =>
               NativeWindowSubWindows.PostMessage(_windowHandle.Handle, customMouseWindowMessage, wParam, lParam));

            return base.HookProcedure(nCode, wParam, lParam);
        }

        /// <summary>
        /// カスタムのマウスウィンドウメッセージをメッセージループに送る。
        /// </summary>
        private IntPtr SendWindowMessage(int nCode, IntPtr wParam, IntPtr lParam, uint customMouseWindowMessage)
        {
            if (_windowHandle.Handle == IntPtr.Zero)
                return base.HookProcedure(nCode, wParam, lParam);

            var result = UIThreadMethodInvoker.Invoke(() =>
               NativeWindowSubWindows.SendMessage(_windowHandle.Handle, customMouseWindowMessage, wParam, lParam));

            if (result != IntPtr.Zero)
            {
                return new IntPtr(-1);
            }

            return base.HookProcedure(nCode, wParam, lParam);
        }

        /// <summary>
        /// ウィンドウメッセージを受け取りそれに応じたActionイベントを発生させる。
        /// </summary>
        public void WndProc(ref Message msg)
        {
            _procedures.WndProc(ref msg);
        }

        protected virtual void ProceduresMouseActive(object sender, MouseHookEventArgs e)
        {
            OnMouseActive(e);
        }

        private void OnMouseActive(MouseHookEventArgs e)
        {
            Volatile.Read(ref this.MouseActive)?.Invoke(this, e);
        }
    }
}
