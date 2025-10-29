using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    /// <summary>
    /// 校正紙出力ウィンドウのRPAを行うサービスクラス。
    /// </summary>
    sealed class KouseishiRPAService
    {

        public KouseishiRPAService()
        {
        }

        /// <summary>
        /// 問番を入力する。
        /// </summary>
        public async Task<bool> EnterToibanAsync(KouseishiWindow kouseishiWindow, Toiban toiban, DialogWindow dialogWindow)
        {
            if (!kouseishiWindow.IsRunning())
                kouseishiWindow.ThrowNengaBoosterExceptionBecauseApplicationNotRun();

            // ウィンドウが開かれていないなら入力しない
            if (!kouseishiWindow.IsOpen(0))
            {
                return false;
            }

            // ダイアログが表示されてないるなら問番は入力しない
            if (dialogWindow.IsOpen(0))
            {
                await kouseishiWindow.ActivateAsync(0);
                await dialogWindow.ActivateAsync(0);
                return false;
            }

            // 問番のテキストボックスが無効なら入力しない
            if (!kouseishiWindow.IsToibanTextBoxEnabled())
            {
                await kouseishiWindow.ActivateAsync(0);
                return false;
            }

            // 問番を入力してデータを開く
            if (!await kouseishiWindow.EnterToibanAsync(toiban).ConfigureAwait(false))
            {
                return false;
            }

            // テキストボックスが空になるまでループ
            var count = 0;
            while (true)
            {
                // 工程違いなどからダイアログが表示されたら
                if (dialogWindow.IsOpen(0))
                {
                    return false;
                }

                // 問番を入力するテキストボックスが空になったら
                if (kouseishiWindow.IsToibanTextBoxEmpty() && kouseishiWindow.IsToibanTextBoxEnabled())
                    return true;

                // 一定時間内にテキストボックスが空にならなず、ダイアログも表示されないなら
                if (count >= 700)
                    return false;

                count += 1;

                await Task.Delay(10).ConfigureAwait(false);
            }
        }
        

        /// <summary>
        /// ウィンドウを閉じる。
        /// </summary>
        public async Task<bool> CloseAsync(KouseishiWindow kouseishiWindow, DialogWindow dialogWindow)
        {
            if (!kouseishiWindow.IsRunning() || !kouseishiWindow.IsOpen(0))
                return true;

            if (!dialogWindow.IsOpen(0))
                return false;

            return await kouseishiWindow.CloseAsync().ConfigureAwait(false);
        }
    }
}
