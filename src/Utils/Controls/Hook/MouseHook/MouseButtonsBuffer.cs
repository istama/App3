using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// マウスの状態を操作するクラス。
    /// </summary>
    sealed class MouseButtonsBuffer
    {
        private int _mouseButtons = 0;


        public MouseButtonsBuffer()
        {
        }


        public void LButtonDown() => ButtonDown(MouseButtons.LButtonDown);
        public void LButtonUp()   => ButtonUp(MouseButtons.LButtonDown);

        public void MButtonDown() => ButtonDown(MouseButtons.MButtonDown);
        public void MButtonUp()   => ButtonUp(MouseButtons.MButtonDown);

        public void RButtonDown() => ButtonDown(MouseButtons.RButtonDown);
        public void RButtonUp()   => ButtonUp(MouseButtons.RButtonDown);


        private void ButtonDown(MouseButtons button)
        {
            var new_buttons = _mouseButtons | (int)button;
            Interlocked.Exchange(ref _mouseButtons, new_buttons);
        }

        private void ButtonUp(MouseButtons button)
        {
            var new_buttons = _mouseButtons & ~(int)button;
            Interlocked.Exchange(ref _mouseButtons, new_buttons);
        }


        /// <summary>
        /// 現在のマウスのボタンの状態を取得する。
        /// </summary>
        public MouseButtons ToMouseButtons()
        {
            return (MouseButtons)_mouseButtons;
        }
    }
}
