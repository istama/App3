using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.UseCases.NengaApps;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    sealed partial class MainFormController
    {
        /// <summary>
        /// 注文名入れの動作モードを設定する。
        /// </summary>
        public async Task SetNaireOpenModeToNormalAsync()
        {
            await SetNaireOpenModeAsync(NaireOpenMode.Normal);
            ChangeMainFormColorToNormalMode();
        }
        public async Task SetNaireOpenModeToSaikumiAsync()
        {
            await SetNaireOpenModeAsync(NaireOpenMode.Saikumi);
            ChangeMainFormColorToSaikumiMode();
        }
        private async Task SetNaireOpenModeAsync(NaireOpenMode mode)
        {
            _viewmodel.NaireOpenMode_Normal = ReturnCheckedIfSame(mode, NaireOpenMode.Normal);
            _viewmodel.NaireOpenMode_Saikumi = ReturnCheckedIfSame(mode, NaireOpenMode.Saikumi);
            await _repositories.UserConfigRepository.SetNaireOpenModeAsync(mode).ConfigureAwait(false);
        }

        /// <summary>
        /// 出力履歴を表示する。
        /// </summary>
        public Task ShowOutputToibanHistoryAsync()
        {
            return null;
        }

        /// <summary>
        /// 問番出力リストを外部に保存する。
        /// </summary>
        public Task StoreOutputToibanListAsync()
        {
            return null;
        }

        /// <summary>
        /// 問番出力リストを外部データからロードする。
        /// </summary>
        public Task LoadOutputToibanListAsync()
        {
            return null;
        }

        /// <summary>
        /// ユーザーアカウント情報を入力するフォームを開く。
        /// </summary>
        public async Task OpenUserAccountFormAsync(Form owner)
        {
            var userAccount = await _repositories.UserConfigRepository.GetUserAccountAsync().ConfigureAwait(false);
            _forms.UserAccountForm.ShowDialog(owner, userAccount);
            await _repositories.UserConfigRepository.SetUserAccountAsync(userAccount).ConfigureAwait(false);
        }

        /// <summary>
        /// ユーザー設定をするフォームを開く。
        /// </summary>
        public async Task OpenUserConfigForm(Form owner)
        {
            var filepath = await _repositories.OtherConfigRepository.GetUserConfigFilepathAsync().ConfigureAwait(false);
            var result = _forms.UserConfigForm.ShowDialog(owner, filepath);

            // ユーザー設定が変更されたときに、右クリックメニューのチェック状態を更新する
            if (result != DialogResult.Cancel)
            {
                await LoadNengaBoosterFormLookAsync();
                await LoadOperationModeMenuStripCheckStateAsync();
            }
        }

        /// <summary>
        /// プロパティを設定するフォームを開く。
        /// </summary>
        public void OpenNengaBoosterConfigForm(Form owner)
        {
            _forms.NengaBoosterConfigForm.Show(owner, async (sender, e) =>
            {
                var form = (Form)sender;
                // アプリケーション設定が更新されたら、右クリックメニューの表示更新する
                if (form.DialogResult != DialogResult.Cancel)
                {
                    await LoadNengaBoosterFormLookAsync();
                    await LoadOperationModeMenuStripCheckStateAsync();
                }
            });
        }
    }
}
