using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.UI.Gateways;
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
        /// ���[�U�ݒ��ǂݍ����MainForm�̕\���ɔ��f������B
        /// </summary>
        public async Task LoadUserConfigAsync(string userConfigFilepath)
        {
            // ���ڃ��[�U�[�ݒ�t�@�C�����w�肵�Ă���ꍇ
            if (!string.IsNullOrWhiteSpace(userConfigFilepath))
            {
                await _repositories.OtherConfigRepository.SetUserConfigFilepathAsync(userConfigFilepath);
            }

            // ��ԃe�L�X�g�{�b�N�X�Ƀ��[�U�[����\������
            var userAccount = await _repositories.UserConfigRepository.GetUserAccountAsync();
            _viewmodel.Toiban = userAccount.UserName;

            // �t�H�[���̌����ڂ����[�h����B
            await LoadNengaBoosterFormLookAsync();
            // �E�N���b�N���j���[�̃`�F�b�N��Ԃ����[�h����
            await LoadOperationModeMenuStripCheckStateAsync();
            
        }

        /// <summary>
        /// ��Ԃ𒍕�������ɓ��͂���B
        /// </summary>
        public async Task EnterToibanToNaireAsync()
        {
            var toiban = _viewmodel.Toiban;
            await _usecases.OpenNaire.ExecuteAsync(toiban).ConfigureAwait(false);
        }

        /// <summary>
        /// ��Ԃ�ҏW�ɓ��͂���B
        /// </summary>
        public async Task EnterToibanToHensyuAsync()
        {
            var toiban = _viewmodel.Toiban;
            await _usecases.OpenHensyu.ExecuteAsync(toiban).ConfigureAwait(false);
        }

        /// <summary>
        /// ��Ԃ��C���t�H���[�V�����ɓ��͂���B
        /// </summary>
        public async Task EnterToibanToInformationAsync()
        {
            var toiban = _viewmodel.Toiban;
            await _usecases.OpenInformation.ExecuteAsync(toiban).ConfigureAwait(false);
        }

        /// <summary>
        /// ��Ԃ��o�̓��X�g�ɒǉ�����B
        /// </summary>
        public void AddToibanToCheckedList()
        {
            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            UpdateToibanCheckedList(helper.AppendIfNothing(_viewmodel.Toiban));
        }


        /// <summary>
        /// SSS���N������B
        /// </summary>
        public async Task StartScreenSaverStopperAsync()
        {
            await _usecases.StartScreenSaverStopper.ExecuteAsync();
            _viewmodel.StartScreenSaverStopperButtonEnabled = false;
            _viewmodel.StopScreenSaverStopperButtonEnabled = true;
        }

        /// <summary>
        /// SSS���~����B
        /// </summary>
        public void StopScreenSaverStopper()
        {
            _usecases.StopScreenSaverStopper.Execute();
            _viewmodel.StartScreenSaverStopperButtonEnabled = true;
            _viewmodel.StopScreenSaverStopperButtonEnabled = false;
        }

        /// <summary>
        /// KeyReplacer���N������B
        /// </summary>
        public async Task ExecuteKeyReplacerAsync()
        {
            var keyReplacerSettingFilepath = await _repositories.UserConfigRepository.GetKeyReplacerSettingsFilepathAsync().ConfigureAwait(false);
            await _keyReplacerExecutor.ExecuteKeyReplacerAsync(keyReplacerSettingFilepath).ConfigureAwait(false);
        }

        /// <summary>
        /// KeyReplacer���~����B
        /// </summary>
        public void KillKeyReplacer()
        {
            _keyReplacerExecutor.KillKeyReplacer();
        }
        
    }
}

