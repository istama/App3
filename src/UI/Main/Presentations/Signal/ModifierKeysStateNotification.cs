using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    /// <summary>
    /// 修飾キーの状態をバックグラウンドタスクで調べて通知するクラス。
    /// </summary>
    sealed class ModifierKeysStateNotification  //: IModifierKeysStateCheckService
    {
        public delegate void NotifyModifierKeysPressingStateEventDelegate(object sender, NotifyModifierKeysPressingStateEventArgs e);

        public event NotifyModifierKeysPressingStateEventDelegate NotifyPressingState;

        private readonly IKeyboard _keyboard;

        private bool _running = false;
        private Task _notificationTask;


        public ModifierKeysStateNotification(IKeyboard keyboard)
        {
            Assert.IsNull(keyboard, nameof(keyboard));

            _keyboard = keyboard;
        }


        /// <summary>
        /// 修飾キーの状態を通知するタスクが起動しているならtrueを返す。
        /// </summary>
        public bool IsRunning() => _running == true;

        /// <summary>
        /// 修飾キーの状態を通知するタスクを起動する。
        /// </summary>
        public void StartTaskToNotifyModifierKeysState()
        {
            if (_notificationTask != null)
                return;

            _running = true;

            _notificationTask = Task.Factory.StartNew(async () =>
            {
                while (_running)
                {
                    var pressingState = CreateModifierKeysPressingState();
                    OnNotifyModifierKeyStateEvent(pressingState);

                    await Task.Delay(100).ConfigureAwait(false);
                }

                _notificationTask = null;
            });
        }

        private ModifierKeysPressingState CreateModifierKeysPressingState()
        {
            return new ModifierKeysPressingState
            {
                IsLCtrlPressing = _keyboard.IsPressingModifierKey(ModifierKeys.LCTRL),
                IsLShiftPressing = _keyboard.IsPressingModifierKey(ModifierKeys.LSHIFT),
                IsLAltPressing = _keyboard.IsPressingModifierKey(ModifierKeys.LALT),

                IsRCtrlPressing = _keyboard.IsPressingModifierKey(ModifierKeys.RCTRL),
                IsRShiftPressing = _keyboard.IsPressingModifierKey(ModifierKeys.RSHIFT),
                IsRAltPressing = _keyboard.IsPressingModifierKey(ModifierKeys.RALT),
            };
        }

        private void OnNotifyModifierKeyStateEvent(ModifierKeysPressingState keysPressingState)
        {
            var args = new NotifyModifierKeysPressingStateEventArgs
            {
                ModifierKeysState = keysPressingState
            };

            Volatile.Read(ref this.NotifyPressingState)?.Invoke(this, args);
        }

        /// <summary>
        /// 修飾キーの状態を通知するタスクを停止する。
        /// </summary>
        public void StopTaskToNotifyModifierKeyState()
        {
            _running = false;
            _notificationTask = null;
        }

    }
}
