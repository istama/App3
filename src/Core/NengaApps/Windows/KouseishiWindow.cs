using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    /// <summary>
    /// 校正紙出力ウィンドウを操作するクラス。
    /// </summary>
    sealed class KouseishiWindow : NengaAppWindowBasic
    {
        private readonly KouseishiApplicationConfig _config;


        public KouseishiWindow(INativeWindowFactory nativeWindowFactory, KouseishiApplicationConfig kouseishiApplicationConfig)
            : base(nativeWindowFactory, kouseishiApplicationConfig.Basic)
        {
            Assert.IsNull(kouseishiApplicationConfig, nameof(kouseishiApplicationConfig));

            _config = kouseishiApplicationConfig;
        }


        /// <summary>
        /// 問番を入力する。
        /// </summary>
        public async Task<bool> EnterToibanAsync(Toiban toiban)
        {
            if (!IsRunning() || !Exists())
                return false;

            return await WindowOperator
                .Activate()
                .Focus(_config.TextBoxPoint_Toiban)
                .SetText(_config.TextBoxPoint_Toiban, toiban.Text)
                .Wait(50)
                .SendEnterTo(_config.TextBoxPoint_Toiban)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 問番のテキストボックスが有効ならtrueを返す。
        /// </summary>
        public bool IsToibanTextBoxEnabled()
        {
            if (!IsRunning() || !Exists())
                return false;

            var controls = WindowStates.GetFormControlStatesArray(_config.TextBoxPoint_Toiban);
            if (controls.Length == 0)
                throw new NengaBoosterException("問番のテキストボックスを取得できません。");

            return controls.First().Enabled();
        }

        /// <summary>
        /// 問番テキストボックスが空ならtrueを返す。
        /// </summary>
        public bool IsToibanTextBoxEmpty()
        {
            if (!IsRunning() || !Exists())
                return false;

            var controls = WindowStates.GetFormControlStatesArray(_config.TextBoxPoint_Toiban);
            if (controls.Length == 0)
                throw new NengaBoosterException("問番のテキストボックスを取得できません。");

            var toiban = controls.First().GetText();

            // 何故か空の問番テキストボックスからテキストを取得すると1234567890が返ってくる
            return String.IsNullOrEmpty(toiban) || toiban == "1234567890";
        }

        public async Task WaitAsync(int waittime_ms)
        {
            await WindowOperator
                .Wait(waittime_ms)
                .DoAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// ウィンドウを閉じる。
        /// </summary>
        public async Task<bool> CloseAsync()
        {
            if (!IsRunning() || !Exists())
                return true;

            return await WindowOperator
                .Activate()
                .SendEnterTo(_config.ButtonName_Close)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }
    }
}
