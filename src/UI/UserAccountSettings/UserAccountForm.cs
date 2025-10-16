
using System;
using System.Drawing;
using System.Windows.Forms;
using IsTama.NengaBooster.UI.UserAccountSettings.Presentations;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.UserAccountSettings.View
{
    /// <summary>
    /// ユーザーアカウント情報を入力するフォーム。
    /// </summary>
    partial class UserAccountForm : Form
    {
        private readonly UserAccountFormPresenter _presenter;

        public UserAccountForm(UserAccountFormPresenter presenter)
        {
            Assert.IsNull(presenter, nameof(presenter));

            InitializeComponent();
            InitializeStartPosition();

            _presenter = presenter;

            this.Shown += UserInfoForm_Shown;
        }

        private void InitializeStartPosition()
        {
            StartPosition = FormStartPosition.Manual;
            var screen_size = Screen.GetBounds(this);
            DesktopLocation = new Point(
                screen_size.Width  / 2 - Width  / 2,
                screen_size.Height / 2 - Height / 2);
        }

        void LoginFormLoad(object sender, EventArgs e)
        {
            this.TxtBoxUserName.Text = _presenter.UserName;
            this.TxtBoxPassword.Text = _presenter.Password;
        }

        private void UserInfoForm_Shown(object sender, EventArgs e)
        {
            // このフォームが表示されたときアクティブになるように
            this.Activate();
        }

        void BtnOkClick(object sender, EventArgs e)
        {
            this.Ok();
        }
    
        void BtnCancelClick(object sender, EventArgs e)
        {
            this.Cancel();
        }
    
        /// <summary>
        /// フォームＩＤのテキストボックスが入力されているときに呼び出される値のチェック。
        /// </summary>
        void TxtBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Ok();
        }
    
        private void Ok()
        {
            _presenter.UserName = this.TxtBoxUserName.Text;
            _presenter.Password = this.TxtBoxPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    
        private void Cancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    
    }
}
