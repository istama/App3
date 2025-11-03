using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    sealed partial class MainFormController
    {
        private readonly MainFormViewModel _viewmodel;

        private readonly RepositoriesThatMainFormControllerDependsOn _repositories;
        private readonly UseCasesThatMainFormControllerDependsOn _usecases;
        private readonly FormsThatMainFormControllerDependsOn _forms;

        private readonly KeyReplacerExecutor _keyReplacerExecutor;
        private readonly ModifierKeysStateNotification _modifierKeysStateNotification;


        public MainFormController(
            MainFormViewModel viewmodel,
            RepositoriesThatMainFormControllerDependsOn repositories,
            UseCasesThatMainFormControllerDependsOn usecases, 
            FormsThatMainFormControllerDependsOn forms,
            KeyReplacerExecutor keyReplacerExecutor,
            ModifierKeysStateNotification modifierKeysStateNotification)
        {
            Assert.IsNull(viewmodel, nameof(viewmodel));
            Assert.IsNull(repositories, nameof(repositories));
            Assert.IsNull(usecases, nameof(usecases));
            Assert.IsNull(forms, nameof(forms));
            Assert.IsNull(keyReplacerExecutor, nameof(keyReplacerExecutor));
            Assert.IsNull(modifierKeysStateNotification, nameof(modifierKeysStateNotification));

            _viewmodel = viewmodel;
            _repositories = repositories;
            _usecases = usecases;
            _forms = forms;
            _keyReplacerExecutor = keyReplacerExecutor;
            _modifierKeysStateNotification = modifierKeysStateNotification;
        }


        /// <summary>
        /// ユーザ設定を読み込んでMainFormの表示に反映させる。
        /// </summary>
        public async Task LoadUserConfigAsync(string userConfigFilepath)
        {
            // 直接ユーザー設定ファイルを指定している場合
            if (!string.IsNullOrWhiteSpace(userConfigFilepath))
            {
                await _repositories.OtherConfigRepository.SetUserConfigFilepathAsync(userConfigFilepath);
            }

            // 問番テキストボックスにユーザー名を表示する
            var userAccount = await _repositories.UserConfigRepository.GetUserAccountAsync();
            _viewmodel.Toiban = userAccount.UserName;

            // フォームの見た目をロードする。
            await LoadNengaBoosterFormLookAsync();
            // 右クリックメニューのチェック状態をロードする
            await LoadOperationModeMenuStripCheckStateAsync();
            
        }

        /// <summary>
        /// 問番を注文名入れに入力する。
        /// </summary>
        public async Task EnterToibanToNaireAsync()
        {
            var toiban = _viewmodel.Toiban;
            await _usecases.OpenNaire.ExecuteAsync(toiban).ConfigureAwait(false);
        }

        /// <summary>
        /// 問番を編集に入力する。
        /// </summary>
        public async Task EnterToibanToHensyuAsync()
        {
            var toiban = _viewmodel.Toiban;
            await _usecases.OpenHensyu.ExecuteAsync(toiban).ConfigureAwait(false);
        }

        /// <summary>
        /// 問番をインフォメーションに入力する。
        /// </summary>
        public async Task EnterToibanToInformationAsync()
        {
            var toiban = _viewmodel.Toiban;
            await _usecases.OpenInformation.ExecuteAsync(toiban).ConfigureAwait(false);
        }

        /// <summary>
        /// 問番を出力リストに追加する。
        /// </summary>
        public void AddToibanToCheckedList()
        {
            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            var toiban = _viewmodel.Toiban;
            UpdateToibanCheckedList(helper.AppendIfNothing(toiban), toiban);
        }


        /// <summary>
        /// SSSを起動する。
        /// </summary>
        public async Task StartScreenSaverStopperAsync()
        {
            await _usecases.StartScreenSaverStopper.ExecuteAsync();
            _viewmodel.StartScreenSaverStopperButtonEnabled = false;
            _viewmodel.StopScreenSaverStopperButtonEnabled = true;
        }

        /// <summary>
        /// SSSを停止する。
        /// </summary>
        public void StopScreenSaverStopper()
        {
            _usecases.StopScreenSaverStopper.Execute();
            _viewmodel.StartScreenSaverStopperButtonEnabled = true;
            _viewmodel.StopScreenSaverStopperButtonEnabled = false;
        }

        /// <summary>
        /// KeyReplacerを起動する。
        /// </summary>
        public async Task ExecuteKeyReplacerAsync()
        {
            var keyReplacerSettingFilepath = await _repositories.UserConfigRepository.GetKeyReplacerSettingsFilepathAsync().ConfigureAwait(false);
            await _keyReplacerExecutor.ExecuteKeyReplacerAsync(keyReplacerSettingFilepath).ConfigureAwait(false);
        }

        /// <summary>
        /// KeyReplacerを停止する。
        /// </summary>
        public void KillKeyReplacer()
        {
            _keyReplacerExecutor.KillKeyReplacer();
        }
        
    }
}

