using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    /// <summary>
    /// インフォメーションのRPAを行うサービスクラス。
    /// </summary>
    sealed class InformationRPAService
    {

        public InformationRPAService()
        {
        }

        /// <summary>
        /// 検索結果を表示する
        /// </summary>
        public async Task<bool> EnterToibanAsync(InformationWindow infoWindow, Toiban toiban, InformationDetailWindow detailWindow, DialogWindow dialog)
        {
            if (!infoWindow.IsRunning())
                infoWindow.ThrowNengaBoosterExceptionBecauseApplicationNotRun();

            if (infoWindow.IsOpen(0))
            {
                return false;
            }

            // ダイアログが表示されてないるなら問番は入力しない
            if (dialog.IsOpen(0))
            {
                await infoWindow.ActivateAsync(0);
                await dialog.ActivateAsync(0);
                return false;
            }

            // 詳細ウィンドウが開かれているなら閉じる
            if (detailWindow.IsOpen(0))
            {
                await detailWindow.CloseAsync().ConfigureAwait(false);
            }

            // 問番を入力して開始ボタンを押す
            if (!await infoWindow.EnterToibanAsync(toiban).ConfigureAwait(false))
            {
                return false;
            }

            // 件数0件のダイアログが表示されないならtrueを返す
            return !dialog.IsOpen(50);
        }

        /// <summary>
        /// 詳細ウィンドウを開く。
        /// </summary>
        public async Task<bool> OpenDetailWindowAsync(InformationWindow infoWindow, InformationDetailWindow detailWindow)
        {
            if (detailWindow.IsOpen(1000))
                return true;

            if (!await infoWindow.OpenInformationDetailWindowAsync().ConfigureAwait(false))
                return false;

            return true;
        }

        /// <summary>
        /// 詳細ウィンドウの校正ページを開く。
        /// </summary>
        public async Task<bool> OpenKouseiPageAsync(InformationWindow infoWindow, InformationDetailWindow detailWindow)
        {
            if (!await OpenDetailWindowAsync(infoWindow, detailWindow).ConfigureAwait(false))
                return false;

            return await detailWindow.OpenKouseiPageAsync(1000).ConfigureAwait(false);
        }

        /// <summary>
        /// 詳細ウィンドウの組版ページを開く。
        /// </summary>
        public async Task<bool> OpenKumihanPageAsync(InformationWindow infoWindow, InformationDetailWindow detailWindow)
        {
            if (!await OpenDetailWindowAsync(infoWindow, detailWindow).ConfigureAwait(false))
                return false;

            return await detailWindow.OpenKumihanPageAsync(1000).ConfigureAwait(false);
        }

        /// <summary>
        /// ウィンドウをすべて閉じる。
        /// </summary>
        public async Task<bool> CloseAsync(InformationWindow infoWindow, InformationDetailWindow detailWindow)
        {
            if (!infoWindow.IsRunning())
                return true;

            await detailWindow.CloseAsync().ConfigureAwait(false);
            
            return await infoWindow.CloseAsync().ConfigureAwait(false);
        }
    }
}
