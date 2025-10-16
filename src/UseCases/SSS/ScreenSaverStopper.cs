using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IsTama.Utils;

namespace IsTama.NengaBooster.UseCases.SSS
{
    /// <summary>
    /// スクリーンセーバーが起動しないように一定時間おきにマウスを動かす。
    /// </summary>
    sealed class ScreenSaverStopper
    {
        private Task _screenSaverStopperTask;
        private CancellationTokenSource _cts;


        /// <summary>
        /// SSSを起動する。
        /// </summary>
        public async Task OnAsync(ScreenSaverStopperPeriods screenSaverStopperPeriods)
        {
            // ScreenSaverStopperが起動中なら停止する
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                Off();
            }

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            // 新しいScreenSaverStopperを起動する。
            _screenSaverStopperTask = Task.Factory.StartNew(async (arg) => {
                var periods = (ScreenSaverStopperPeriods)arg;

                while (true)
                {
                    if (token.IsCancellationRequested)
                        break;

                    await Task.Delay(1000 * 59).ConfigureAwait(false);

                    var inTime = periods.IsWithinTimeRage(DateTime.Now);
                    if (inTime)
                        NativeWindowMouseInput.mouse_event(1, 0, 0, 0, 0);
                }
            }, screenSaverStopperPeriods, _cts.Token).ContinueWith(task => task.Dispose());

            await _screenSaverStopperTask.ConfigureAwait(false);
        }

        /// <summary>
        /// SSSを停止する。
        /// </summary>
        public void Off()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();

                _screenSaverStopperTask.Wait();
                _screenSaverStopperTask = null;

                _cts.Dispose();
                _cts = null;
            }
        }
    }
}
