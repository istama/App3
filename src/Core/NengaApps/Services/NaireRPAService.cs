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
    /// 注文名入れウィンドウのRPAを行うサービスクラス。
    /// </summary>
    sealed class NaireRPAService
    {

        public NaireRPAService()
        {
        }


        /// <summary>
        /// 問番を入力する。
        /// </summary>
        public async Task<bool> EnterToibanAsync(NaireWindow naireWindow, Toiban toiban, DialogWindow dialogWindow, NaireBehaviorConfig config)
        {
            if (!naireWindow.IsRunning())
                naireWindow.ThrowNengaBoosterExceptionBecauseApplicationNotRun();

            // ウィンドウが開かれていないなら入力しない
            if (!naireWindow.IsOpen(0))
            {
                return false;
            }

            // ダイアログが表示されてないるなら問番は入力しない
            if (dialogWindow.IsOpen(0))
            {
                await naireWindow.ActivateAsync(0);
                await dialogWindow.ActivateAsync(0);
                return false;
            }

            // 問番のテキストボックスが無効なら入力しない
            if (!naireWindow.IsToibanTextBoxEnabled())
            {
                await naireWindow.ActivateAsync(0);
                return false;
            }

            // 問番を入力してデータを開く
            if (!await naireWindow.EnterToibanAsync(toiban).ConfigureAwait(false))
            {
                return false;
            }

            // 指定時間以内にダイアログが開かれた場合
            if (dialogWindow.IsOpen(config.WaitTime_DialogOpen))
            {
                // ダイアログにエラーメッセージが含まれる場合
                if (dialogWindow.IsErrorDialog())
                {
                    return false;
                }

                // ダイアログのメッセージに確認不要な工程名が含まれる場合、ダイアログを閉じる
                if (dialogWindow.Contains(config.Texts_Dialog_WorkProcessNames))
                {
                    await dialogWindow.OkAsync().ConfigureAwait(false);
                    await Task.Delay(50).ConfigureAwait(false);
                }
            }

            // 行程違いのダイアログが表示されてもtrueを返す理由:
            // 確認のためにダイアログを表示したところで動作を止めるが、そのまま処理する可能性が高いので、
            // 出力リストに問番を追加するためにtrueを返す
            return true;
        }

        /// <summary>
        /// 組版依頼をかける。
        /// 引数にはダイアログが開かれるか確認するまでの待機時間を渡す。
        /// </summary>
        public async Task<bool> ExecuteKumihanIraiAsync(NaireWindow naireWindow, DialogWindow dialogWindow, NaireBehaviorConfig config)
        {
            if (!naireWindow.IsRunning())
                naireWindow.ThrowNengaBoosterExceptionBecauseApplicationNotRun();

            // ウィンドウが開かれていないなら実行しない
            if (!naireWindow.IsOpen(0))
                return false;

            // ダイアログが表示されてないるなら実行しない
            if (dialogWindow.IsOpen(0))
                return false;

            // 組版依頼をかける
            if (!await naireWindow.KumihanAsync().ConfigureAwait(false))
                return false;

            // 組版依頼をかけた後、ダイアログが表示されたならfalseを返す
            return !dialogWindow.IsOpen(config.WaitTime_SaikumiAlertDialogOpen);
        }

        /// <summary>
        /// ウィンドウを閉じる。
        /// </summary>
        public async Task<bool> CloseAsync(NaireWindow naireWindow, DialogWindow dialogWindow)
        {
            if (!naireWindow.IsRunning() || !naireWindow.IsOpen(0))
                return true;

            if (!dialogWindow.IsOpen(0))
                return false;

            return await naireWindow.CloseAsync().ConfigureAwait(false);
        }
    }
}
