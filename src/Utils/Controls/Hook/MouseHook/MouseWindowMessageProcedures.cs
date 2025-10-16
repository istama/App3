using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    /// <summary>
    /// マウスフックからのマウスウィンドウメッセージに対する処理を呼び出すクラス。
    /// このクラスは継承されることを想定しており、各ウィンドウメッセージに対応するメソッドをオーバーライドする。
    /// </summary>
    class MouseWindowMessageProcedures
    {
        /// <summary>
        /// マウスが動作したときに発生するイベント。
        /// </summary>
        public event EventHandler<MouseHookEventArgs> MouseActive;


        protected MouseWindowMessageProcedures()
        {
        }


        public void WndProc(ref Message m)
        {
            if (m == null)
                return;

            WndProcCore(ref m);
        }

        protected virtual void WndProcCore(ref Message m)
        {
            if (m.Msg == MouseWindowMessage.MOVE)
            {
                Move(ref m);
            }
        
            if (m.Msg == MouseWindowMessage.LBUTTON_DOWN)
            {
                LButtonDown(ref m);
            }
        
            if (m.Msg == MouseWindowMessage.LBUTTON_UP)
            {
                LButtonUp(ref m);
            }
        
            if (m.Msg == MouseWindowMessage.MBUTTON_DOWN)
            {
                MButtonDown(ref m);
            }
        
            if (m.Msg == MouseWindowMessage.MBUTTON_UP)
            {
                MButtonUp(ref m);
            }
        
            if (m.Msg == MouseWindowMessage.RBUTTON_DOWN)
            {
                RButtonDown(ref m);
            }
        
            if (m.Msg == MouseWindowMessage.RBUTTON_UP)
            {
                RButtonUp(ref m);
            }
        
            if (m.Msg == MouseWindowMessage.WHEEL_DOWN)
            {
                WheelDown(ref m);
            }
        
            if (m.Msg == MouseWindowMessage.WHEEL_UP)
            {
                WheelUp(ref m);
            }
        }
        
        protected virtual void Move(ref Message m)         {  }

        protected virtual void LButtonDown(ref Message m)  {  }
        protected virtual void LButtonUp(ref Message m)    {  } 
        protected virtual void MButtonDown(ref Message m)  {  } 
        protected virtual void MButtonUp(ref Message m)    {  } 
        protected virtual void RButtonDown(ref Message m)  {  } 
        protected virtual void RButtonUp(ref Message m)    {  } 
        
        protected virtual void WheelDown(ref Message m)    {  }
        protected virtual void WheelUp(ref Message m)      {  }


        protected virtual void OnMouseActive(MouseHookEventArgs e)
            => Volatile.Read(ref this.MouseActive)?.Invoke(this, e);
    }
}
