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
    /// ログインウィンドウを操作するクラス。
    /// </summary>
    sealed class LoginFormWindow : NengaAppWindowBasic
    {
        private readonly LoginFormConfig _config;


        public LoginFormWindow(INativeWindowFactory nativeWindowFactory, LoginFormConfig loginFormConfig)
            : base(nativeWindowFactory, loginFormConfig.Basic)
        {
            Assert.IsNull(loginFormConfig, nameof(loginFormConfig));

            _config = loginFormConfig;
        }


        /// <summary>
        /// アカウント情報を入力する。
        /// </summary>
        public async Task<bool> EnterUserNameAndPasswordAsync(UserAccount user)
        {
            if (!base.IsOpen(0))
                return true;

            return await WindowOperator
                .Activate()
                .Wait(100)
                .Focus(_config.TextBoxPoint_UserName)
                .SetText(_config.TextBoxPoint_UserName, user.UserName)
                .SetText(_config.TextBoxPoint_Password, user.Password)
                .SendEnterTo(_config.ButtonName_Ok)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }

    }
}
