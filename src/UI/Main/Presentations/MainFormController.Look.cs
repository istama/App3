using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UseCases.NengaApps;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    partial class MainFormController
    {
        private readonly Color _normalFormColor = SystemColors.Control;
        private readonly Color _normalButtonColor = SystemColors.ControlLight;

        private readonly Color _saikumiFormColor = Color.FromArgb(106, 139, 154);
        private readonly Color _saikumiButtonColor = Color.FromArgb(194, 210, 224);

        private readonly Color _normalSignalColor = Color.Teal;
        private readonly Color _saikumiSignalColor = Color.FromArgb(255, 230, 200);

        /// <summary>
        /// NengaBoosterのフォームの見た目を更新する。
        /// </summary>
        public async Task LoadNengaBoosterFormLookAsync()
        {
            var repos = _repositories.UserConfigRepository;

            // 出力リストの文字サイズを変更する。
            var size = await repos.GetToibanCheckedListCharSize();
            _viewmodel.ToibanCheckedListCharSize = size;
        }

        /// <summary>
        /// フォームの色を通常モードの色にする。
        /// </summary>
        public void ChangeMainFormColorToNormalMode()
        {
            _viewmodel.FormColor = _normalFormColor;
            _viewmodel.LabelSignalTextColor = _normalFormColor;
            _viewmodel.ButtonColor = _normalButtonColor;
        }

        /// <summary>
        /// フォームの色を再組版モードの色にする。
        /// </summary>
        public void ChangeMainFormColorToSaikumiMode()
        {
            _viewmodel.FormColor = _saikumiFormColor;
            _viewmodel.LabelSignalTextColor = _saikumiFormColor;
            _viewmodel.ButtonColor = _saikumiButtonColor;
        }

        /// <summary>
        /// 修飾キーの状態を通知するタスクを開始する。
        /// </summary>
        public void StartTaskToNotifyModifierKeysState()
        {
            if (!_modifierKeysStateNotification.IsRunning())
            {
                UIThreadMethodInvoker.Initialize();

                _modifierKeysStateNotification.NotifyPressingState += ModifierKeysStateNotified;
                _modifierKeysStateNotification.StartTaskToNotifyModifierKeysState();
            }
        }

        /// <summary>
        /// 修飾キーの状態を通知するタスクを停止する。
        /// </summary>
        public void StopTaskToNotifyModifierKeysState()
        {
            _modifierKeysStateNotification.NotifyPressingState -= ModifierKeysStateNotified;
            _modifierKeysStateNotification.StopTaskToNotifyModifierKeyState();
            UIThreadMethodInvoker.Dispose();
        }

        /// <summary>
        /// 修飾キーの状態が通知されたときに呼び出されるコールバックメソッド。
        /// </summary>
        private async void ModifierKeysStateNotified(object sender, NotifyModifierKeysPressingStateEventArgs e)
        {
            var mode = await _repositories.UserConfigRepository.GetNaireOpenModeAsync().ConfigureAwait(false);

            var formColor = mode == NaireOpenMode.Normal
                ? _normalFormColor
                : _saikumiFormColor;

            var signalColor = mode == NaireOpenMode.Normal
                ? _normalSignalColor
                : _saikumiSignalColor;

            UIThreadMethodInvoker.Invoke((states) =>
            {
                // シグナルの色を変更する
                _viewmodel.LabelLeftCtrlSignalColor  = states.IsLCtrlPressing  ? signalColor : formColor;
                _viewmodel.LabelLeftShiftSignalColor = states.IsLShiftPressing ? signalColor : formColor;
                _viewmodel.LabelLeftAltSignalColor   = states.IsLAltPressing   ? signalColor : formColor;

                _viewmodel.LabelRightCtrlSignalColor  = states.IsRCtrlPressing  ? signalColor : formColor;
                _viewmodel.LabelRightShiftSignalColor = states.IsRShiftPressing ? signalColor : formColor;
                _viewmodel.LabelRightAltSignalColor   = states.IsRAltPressing   ? signalColor : formColor;
            }, e.ModifierKeysState);
        }
    }
}
