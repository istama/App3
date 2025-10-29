using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    /// <summary>
    /// 年賀アプリにログインするサービス。
    /// </summary>
    sealed class LoginRPAService
    {
        private readonly IUserAccountForm _userAccountForm;


        public LoginRPAService(IUserAccountForm userAccountForm)
        {
            Assert.IsNull(userAccountForm, nameof(userAccountForm));

            _userAccountForm = userAccountForm;
        }


        public async Task<Boolean> LoginAsync(NengaMenuWindow nengaMenuWindow, LoginFormWindow loginWindow, NengaAppWindowBasic nengaAppWindow, UserAccount user)
        {
            // 年賀メニューが起動していない
            if (!nengaMenuWindow.IsRunning())
                throw new NengaBoosterException("年賀メニューが起動していません。");

            // 年賀アプリが起動していない
            if (!nengaAppWindow.IsRunning())
            {
                // 年賀アプリを起動する
                if (!await nengaMenuWindow.ExecuteNengaAppAsync(nengaAppWindow.GetNengaApplicationNameOnNengaMenu()).ConfigureAwait(false))
                {
                    return false;
                }

                // ログインウィンドウが開くまで待機する
                await loginWindow.ActivateAsync(10000).ConfigureAwait(false);
                if (!loginWindow.IsOpen(3000))
                {
                    return false;
                }
            }

            // 年賀アプリが開かれてない
            if (!nengaAppWindow.IsOpen(250))
            {
                // ログインウィンドウも開かれてない場合は何もできないので終了する
                if (!loginWindow.IsOpen(0))
                {
                    return false;
                }

                // ユーザー情報が登録されていない
                if (!user.IsFilled())
                {
                    // ユーザ情報を登録するフォームを表示する
                    _userAccountForm.ShowDialog(null, user);
                }

                // ログインウィンドウをアクティブにする
                if (!await loginWindow.ActivateAsync(2000).ConfigureAwait(false))
                {
                    return false;
                }

                // ユーザー名とパスワードを入力する
                if (!await loginWindow.EnterUserNameAndPasswordAsync(user).ConfigureAwait(false))
                {
                    return false;
                }

                // TODO アカウントが間違っていた場合の処理を入れる

                // 年賀アプリが開くまで待機する
                await nengaAppWindow.ActivateAsync(10000).ConfigureAwait(false);
                if (!nengaAppWindow.IsOpen(1000))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
