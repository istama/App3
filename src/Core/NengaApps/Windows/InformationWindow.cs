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
    /// インフォメーション検索ウィンドウを操作するクラス。
    /// </summary>
    sealed class InformationWindow : NengaAppWindowBasic
    {
        private readonly InformationApplicationConfig _config;


        public InformationWindow(INativeWindowFactory nativeWindowFactory, InformationApplicationConfig config)
            : base(nativeWindowFactory, config.Basic)
        {
            Assert.IsNull(config, nameof(config));

            _config = config;
        }


        /// <summary>
        /// アプリケーションに問番を送る。
        /// </summary>
        public async Task<bool> EnterToibanAsync(Toiban toiban)
        {
            if (!Exists() || !IsOpen(0))
                return false;

            // 問番を入力して開始ボタンを押す
            return await WindowOperator
                .Activate()
                .Focus(_config.TextBoxPoint_Toiban)
                .SetText(_config.TextBoxPoint_Toiban, toiban.Text)
                .Wait(50)
                .SendEnterTo(_config.ButtonName_Open)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// インフォメーション詳細ウィンドウを開く。
        /// </summary>
        public async Task<Boolean> OpenInformationDetailWindowAsync()
        {
            if (!Exists() || !IsOpen(0))
                return false;

            return await WindowOperator
                .Activate()
                .SendEnterTo(_config.ButtonName_Detail)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// ウィンドウを閉じる。
        /// </summary>
        public async Task<Boolean> CloseAsync()
        {
            if (!IsRunning())
                return true;

            if (!Exists() || !IsOpen(0))
                return true;

            return await WindowOperator
                .Activate()
                .LeftClick(_config.ButtonName_Close)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }
    }
}
