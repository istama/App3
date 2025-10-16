using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    sealed class HensyuRPAService
    {

        public HensyuRPAService()
        {
        }


        /// <summary>
        /// 問番を入力する。
        /// </summary>
        public async Task<bool> EnterToibanAsync(HensyuWindow hensyuWindow, Toiban toiban, DialogWindow dialogWindow, HensyuBehaviorConfig config)
        {
            if (!hensyuWindow.IsRunning())
                hensyuWindow.ThrowNengaBoosterExceptionBecauseApplicationNotRun();

            // 編集ウィンドウが開いてないなら
            if (!hensyuWindow.IsOpen(0))
            {
                return false;
            }

            // ダイアログが表示されてないるなら問番は入力しない
            if (dialogWindow.IsOpen(0))
            {
                await hensyuWindow.ActivateAsync(0);
                await dialogWindow.ActivateAsync(0);
                return false;
            }

            // 問番のテキストボックスが無効なら入力しない
            if (!hensyuWindow.IsToibanTextBoxEnabled())
            {
                await hensyuWindow.ActivateAsync(0);
                return false;
            }

            // 問番を入力して開く
            if (!await hensyuWindow.EnterToibanAsync(toiban).ConfigureAwait(false))
            {
                return false;
            }

            // 指定時間以内にダイアログが開かれたら
            if (dialogWindow.IsOpen(config.WaitTime_DialogOpen))
            {
                // ダイアログにエラーメッセージが含まれる場合
                if (dialogWindow.IsErrorDialog())
                    return false;

                // ダイアログに確認不要な工程が含まれている場合、ダイアログを閉じる
                if (dialogWindow.Contains(config.Texts_Dialog_WorkProcessNames))
                    await dialogWindow.OkAsync().ConfigureAwait(false);
            }

            return true;
        }

        /// <summary>
        /// 手組を開く。
        /// </summary>
        public async Task<bool> OpenTegumiWindowAsync(HensyuWindow hensyuWindow)
        {
            if (!hensyuWindow.IsRunning())
                hensyuWindow.ThrowNengaBoosterExceptionBecauseApplicationNotRun();

            // ウィンドウが開かれていないなら実行しない
            if (!hensyuWindow.IsOpen(0))
                return false;

            // ダイアログが表示されてないるなら実行しない
            if (hensyuWindow.IsOpen(0))
                return false;

            // 手組を開く
            return await hensyuWindow.OpenTegumiWindowAsync();
        }

        /// <summary>
        /// ウィンドウを閉じる。
        /// </summary>
        public async Task<bool> CloseAsync(HensyuWindow hensyuWindow, DialogWindow dialogWindow)
        {
            if (!hensyuWindow.IsRunning() || !hensyuWindow.IsOpen(0))
                return true;

            if (!dialogWindow.IsOpen(0))
                return false;

            return await hensyuWindow.CloseAsync().ConfigureAwait(false);
        }
    }
}
