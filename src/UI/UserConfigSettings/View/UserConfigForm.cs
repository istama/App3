using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.UI.UserConfigSettings.Presentations;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.UserConfigSettings.View
{
    partial class UserConfigForm : Form
    {
        /// <summary>
        /// ユーザー設定のファイルパス。
        /// </summary>
        private readonly string _userConfigFilepath;
        private readonly UserConfigFormViewModel _viewmodel;
        private readonly UserConfigFormPresenter _presenter;

        private readonly ViewModelBinder _viewmodelBinder;


        public UserConfigForm(string userConfigFilepath, UserConfigFormViewModel viewmodel, UserConfigFormPresenter presenter)
        {
            Assert.IsNullOrWhiteSpace(userConfigFilepath, nameof(userConfigFilepath));
            Assert.IsNot(userConfigFilepath.IsValidAsPath(), $"パスの書式が不正です。 {userConfigFilepath}");
            Assert.IsNull(viewmodel, nameof(viewmodel));
            Assert.IsNull(presenter, nameof(presenter));

            _userConfigFilepath = userConfigFilepath;
            _viewmodel = viewmodel;
            _presenter = presenter;

            _viewmodelBinder = new ViewModelBinder(_viewmodel);

            InitializeComponent();
        }


        private async void UserConfigForm_Load(object sender, EventArgs e)
        {
            _viewmodelBinder.BindText(TxtBoxUserName, nameof(_viewmodel.UserName));
            _viewmodelBinder.BindText(TxtBoxPassword, nameof(_viewmodel.Password));
            _viewmodelBinder.BindText(TxtBoxKeyReplaceSettingsFilePath, nameof(_viewmodel.KeyReplaceSettingsFilePath));
            
            _viewmodelBinder.BindChecked(RadioButtonThatMakeTextSelectedByClick, nameof(_viewmodel.SelectToibanByClickChecked));
            _viewmodelBinder.BindChecked(RadioButtonThatMakeTextSelectedByWClick, nameof(_viewmodel.SelectToibanByWClickChecked));
            
            _viewmodelBinder.BindChecked(RadioButtonThatOpenHensyuMenuWindow, nameof(_viewmodel.OpenHensyuMenuOnlyChecked));
            _viewmodelBinder.BindChecked(RadioButtonThatOpenHensyuTegumiWindow, nameof(_viewmodel.OpenHensyuTegumiWindowChecked));
            
            _viewmodelBinder.BindChecked(CheckBoxThatAddToOutputToibanList, nameof(_viewmodel.ShouldAddToibanToCheckedListWhenInformationOpenChecked));
            _viewmodelBinder.BindChecked(RadioButtonThatNotOpenInformationDetailWindow, nameof(_viewmodel.OpenInformationSearchFormOnlyChecked));
            _viewmodelBinder.BindChecked(RadioButtonThatOpenInformationDetailWindow, nameof(_viewmodel.OpenInformationDetailWindowChecked));
            _viewmodelBinder.BindChecked(RadioButtonThatOpenKouseiPage, nameof(_viewmodel.OpenInformationKouseiPageChecked));
            _viewmodelBinder.BindChecked(RadioButtonThatOpenKumihanPage, nameof(_viewmodel.OpenInformationKumihanPageChecked));
            _viewmodelBinder.BindChecked(CheckBoxThatUncheckToibanCheckBox, nameof(_viewmodel.ShouldUncheckToibanFromCheckedListChecked));

            _viewmodelBinder.BindChecked(RadioButtonThatRemoveToibanAll, nameof(_viewmodel.RemoveToibanAllChecked));
            _viewmodelBinder.BindChecked(RadioButtonThatRemoveToibanChecked, nameof(_viewmodel.RemoveToibanCheckedChecked));
            _viewmodelBinder.BindChecked(RadioButtonThatRemoveToibanUnchecked, nameof(_viewmodel.RemoveToibanUncheckedChecked));

            try
            {
                await _presenter.LoadAsync(_userConfigFilepath).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Show(ex.ToString());
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                await _presenter.SaveAsync(_userConfigFilepath).ConfigureAwait(false);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Show(ex.ToString());
            }
        }

        private async void BtnCancel_Click(object sender, EventArgs e)
        {
            if (await _presenter.Updated(_userConfigFilepath).ConfigureAwait(false))
            {
                var result = MessageBox.Show("値が変更されましたが保存せずに終了してよろしいですか？", "プロパティ", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                    return;
            }
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
