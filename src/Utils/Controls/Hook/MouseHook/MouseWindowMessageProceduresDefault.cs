using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    /// <summary>
    /// マウスフックからのマウスウィンドウメッセージを受け取ると、イベントを発生させるクラス。
    /// 基底クラスのWndProc(ref Message m)にウィンドウメッセージを渡すと、
    /// MouseActiveイベントが発生する。
    /// イベントのパラメータにはマウスの状態が格納されている。
    /// </summary>
    class MouseWindowMessageProceduresDefault: MouseWindowMessageProcedures
    {
        /// <summary>
        /// マウスの状態を保存するバッファクラス。
        /// </summary>
        private readonly MouseButtonsBuffer _mouseButtonsBuffer;


        public MouseWindowMessageProceduresDefault() : this(new MouseButtonsBuffer())
        {
        }

        public MouseWindowMessageProceduresDefault(MouseButtonsBuffer mouseStateController)
        {
            Assert.IsNull(mouseStateController, nameof(mouseStateController));

            _mouseButtonsBuffer = mouseStateController;
        }


        protected override void LButtonDown(ref Message m)
        {
            _mouseButtonsBuffer.LButtonDown();
        }
        
        protected override void LButtonUp(ref Message m)
        {
            _mouseButtonsBuffer.LButtonUp();

            var args = CreateMouseActive(MouseAction.LClick);
            OnMouseActive(ref m, args);
        }

        protected override void MButtonDown(ref Message m)
        {
            _mouseButtonsBuffer.MButtonDown();
        }

        protected override void MButtonUp(ref Message m)
        {
            _mouseButtonsBuffer.MButtonUp();

            var args = CreateMouseActive(MouseAction.MClick);
            OnMouseActive(ref m, args);
        }

        protected override void RButtonDown(ref Message m)
        {
            _mouseButtonsBuffer.RButtonDown();
        }
        
        protected override void RButtonUp(ref Message m)
        {
            _mouseButtonsBuffer.RButtonUp();

            var args = CreateMouseActive(MouseAction.RClick);
            OnMouseActive(ref m, args);
        }
        
        protected override void WheelDown(ref Message m)
        {
            var args = CreateMouseActive(MouseAction.WheelDown);
            OnMouseActive(ref m, args);
        }
        
        protected override void WheelUp(ref Message m)
        {
            var args = CreateMouseActive(MouseAction.WheelUp);
            OnMouseActive(ref m, args);
        }
        
        private MouseHookEventArgs CreateMouseActive(MouseAction action)
        {
            var buttons = _mouseButtonsBuffer.ToMouseButtons();
            var mouse_state = new MouseState(buttons, action);
        
            return new MouseHookEventArgs(mouse_state);
        }

        private void OnMouseActive(ref Message m, MouseHookEventArgs e)
        {
            base.OnMouseActive(e);

            // イベントの結果を返す
            m.Result = e.Handled ? new IntPtr(1) : IntPtr.Zero;
        }

    }
}
