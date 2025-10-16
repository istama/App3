
using System;
using System.Drawing;
using System.Windows.Forms;
using IsTama.NengaBooster.Error;
using IsTama.NengaBooster.UI.NengaBoosterConfigSettings.Presentations;

namespace IsTama.NengaBooster.UI.NengaBoosterConfigSettings.View
{
    /// <summary>
    /// プロパティ設定のフォーム。
    /// </summary>
    partial class NengaBoosterConfigForm : Form
    {
        private readonly NengaBoosterConfigFormViewModel _viewmodel;
        private readonly NengaBoosterConfigFormPresenter _presenter;


        public NengaBoosterConfigForm(NengaBoosterConfigFormViewModel viewmodel, NengaBoosterConfigFormPresenter presenter)
        {

            InitializeComponent();

            _viewmodel = viewmodel;
            _presenter = presenter;

            
            PropertyGridBehavior.SelectedObject      = _viewmodel.BehaviorSettings;
            PropertyGridApplication.SelectedObject   = _viewmodel.ApplicationSettings;

            PropertyGridBehavior.PropertyValueChanged      += PropertyGrid_PropertyValueChanged;
            PropertyGridApplication.PropertyValueChanged   += PropertyGrid_PropertyValueChanged;

            btnUpdate.Enabled = false;
            DialogResult = DialogResult.Cancel;
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            btnUpdate.Enabled = true;
        }

        private async void NengaBoosterConfigForm_Load(object sender, EventArgs e)
        {
            try
            {
                var (success, errorMessage) = await _presenter.TryLoadAsync().ConfigureAwait(false);
                if (!success)
                {
                    ErrorMessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex) {
                ErrorMessageBox.Show(ex.ToString());
            }
        }

        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            try {
                var (success, errorMessage) = await _presenter.TrySaveAsync().ConfigureAwait(false);
                if (success)
                {
                    btnUpdate.Enabled = false;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    ErrorMessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex) {
                ErrorMessageBox.Show(ex.ToString());
            }
        }

        private async void BtnCancel_Click(object sender, EventArgs e)
        {
            if (await _presenter.AreViewModelValuesUpdatedAsync().ConfigureAwait(false))
            {
                var result = MessageBox.Show("値が変更されましたが保存せずに終了してよろしいですか？", "プロパティ", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            Close();
        }
    }
}
