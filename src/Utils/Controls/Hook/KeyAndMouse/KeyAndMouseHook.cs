using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.Utils
{
    class KeyAndMouseHook
    {
        public event EventHandler<KeyAndMouseHookEventArgs> KeyOrMouseActive;

        private readonly KeyHook   _keyHook;
        private readonly MouseHook _mouseHook;

        private InputKey     _lastInputKey;
        private ModifierKeys _lastModifierKeys;
        private MouseAction  _lastMouseAction;
        private MouseButtons _lastMouseButtons;

        private bool _inititalized = false;
        private bool _keyHooked    = false;
        private bool _mouseHooked  = false;
        

        public KeyAndMouseHook(KeyHook keyHook, MouseHook mouseHook)
        {
            Assert.IsNull(keyHook,   nameof(keyHook));
            Assert.IsNull(mouseHook, nameof(mouseHook));

            _keyHook   = keyHook;
            _mouseHook = mouseHook;

            _lastInputKey     = InputKey.NONE;
            _lastModifierKeys = ModifierKeys.NONE;
            _lastMouseAction  = MouseAction.None;
            _lastMouseButtons = MouseButtons.None;
        }


        /// <summary>
        /// キーフックを開始する。
        /// </summary>
        public void StartKeyHook()
        {
            if (_keyHooked)
                return;

            _keyHook.Hook();
            _keyHook.InputKeyDown    += InputKeyDown;
            _keyHook.ModifierKeyDown += ModifierKeyDown;
            _keyHook.ModifierKeyUp   += ModifierKeyUp;
            _keyHooked = true;
        }

        /// <summary>
        /// キーフックを解除する。
        /// </summary>
        public void QuitKeyHook()
        {
            _keyHook.Unhook();
            _keyHook.InputKeyDown    -= InputKeyDown;
            _keyHook.ModifierKeyDown -= ModifierKeyDown;
            _keyHook.ModifierKeyUp   -= ModifierKeyUp;
            _keyHooked = false;
        }

        /// <summary>
        /// キー入力されたときに呼び出されるイベントハンドラ。
        /// </summary>
        protected virtual void InputKeyDown(object sender, KeyHookEventArgs e)
        {
            _lastInputKey     = e.KeyState.InputKey;
            _lastModifierKeys = e.KeyState.ModifierKeys;

            var state = new KeyAndMouseState(_lastInputKey, _lastModifierKeys, _lastMouseButtons);
            var args  = new KeyAndMouseHookEventArgs(state);
            OnActive(args);

            // 元のイベントにHandledされたことを伝播する
            if (args.Handled)
                e.Handled = true;
        }

        /// <summary>
        /// 修飾キーがキー入力されたときに呼び出されるイベントハンドラ。
        /// </summary>
        private void ModifierKeyDown(object sender, KeyHookEventArgs e)
        {
            _lastInputKey     = e.KeyState.InputKey;
            _lastModifierKeys = e.KeyState.ModifierKeys;
        }

        /// <summary>
        /// 修飾キーがキー離されたときに呼び出されるイベントハンドラ。
        /// </summary>
        private void ModifierKeyUp(object sender, KeyHookEventArgs e)
        {
            _lastInputKey     = e.KeyState.InputKey;
            _lastModifierKeys = e.KeyState.ModifierKeys;
        }


        /// <summary>
        /// このクラスのインスタンスを呼び出したら最初に呼び出す必要があるメソッド。
        /// 引数のwindowHandkeには、マウスフックした際にPostMessage()が呼び出されるので、
        /// そのメッセージの送り先となるウィンドウハンドルを渡す。
        /// </summary>
        public void InitializeMouseHook(HandleRef windowHandle)
        {
            if (!_inititalized)
            {
                _mouseHook.SetWindowHandle(windowHandle);

                _inititalized = true;
            }
        }

        /// <summary>
        /// マウスフックを開始する。
        /// </summary>
        public void StartMouseHook()
        {
            if (!_inititalized)
                throw new InvalidOperationException("Initialize()が呼び出されていません。");

            if (_mouseHooked)
                return;

            _mouseHook.Hook();
            _mouseHook.MouseActive += MouseActive;
            _mouseHooked = true;
        }

        /// <summary>
        /// マウスフックを解除する。
        /// </summary>
        public void QuitMouseHook()
        {
            _mouseHook.UnHook();
            _mouseHook.MouseActive -= MouseActive;
            _mouseHooked = false;
        }

        /// <summary>
        /// マウス操作が行われたときに呼び出されるイベントハンドラ。
        /// </summary>
        protected virtual void MouseActive(object sender, MouseHookEventArgs e)
        {
            _lastMouseAction  = e.MouseState.Action;
            _lastMouseButtons = e.MouseState.Buttons;

            var state = new KeyAndMouseState(_lastModifierKeys, _lastMouseAction, _lastMouseButtons);
            var args  = new KeyAndMouseHookEventArgs(state);

            OnActive(args);

            // 元のイベントにHandledされたことを伝播する
            if (args.Handled)
                e.Handled = true;
        }

        /// <summary>
        /// Actionイベントを発生させる。
        /// </summary>
        protected virtual void OnActive(KeyAndMouseHookEventArgs e)
        {
            Volatile.Read(ref KeyOrMouseActive)?.Invoke(this, e);
        }

        /// <summary>
        /// 左クリックのマウスフックの動作方法をセットする。
        /// </summary>
        public void SetMessagingModeOnLClick(MouseHookMessagingMode messagingMode)
        {
            _mouseHook.MessagingModeOnLClick = messagingMode;
        }

        /// <summary>
        /// 中クリックのマウスフックの動作方法をセットする。
        /// </summary>
        public void SetMessagingModeOnMClick(MouseHookMessagingMode messagingMode)
        {
            _mouseHook.MessagingModeOnMClick = messagingMode;
        }

        /// <summary>
        /// 右クリックのマウスフックの動作方法をセットする。
        /// </summary>
        public void SetMessagingModeOnRClick(MouseHookMessagingMode messagingMode)
        {
            _mouseHook.MessagingModeOnRClick = messagingMode;
        }

        /// <summary>
        /// ホイールのマウスフックの動作方法をセットする。
        /// </summary>
        public void SetMessagingModeOnWheel(MouseHookMessagingMode messagingMode)
        {
            _mouseHook.MessagingModeOnWheel = messagingMode;
        }

        /// <summary>
        /// ウィンドウメッセージを受け取りそれに応じたActionイベントを発生させる。
        /// </summary>
        public void WndProc(ref Message msg)
        {
            _mouseHook.WndProc(ref msg);
        }
    }
}
