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
    /// 注文・名入れウィンドウを操作するクラス。
    /// </summary>
    sealed class NaireWindow : NengaAppWindowBasic
    {
        private readonly NaireApplicationConfig _config;


        public NaireWindow(INativeWindowFactory nativeWindowFactory, NaireApplicationConfig naireApplicationConfig)
            : base(nativeWindowFactory, naireApplicationConfig.Basic)
        {
            Assert.IsNull(naireApplicationConfig, nameof(naireApplicationConfig));

            _config = naireApplicationConfig;
        }


        /// <summary>
        /// 問番を入力してデータを開く。
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
                .SendEnterTo(_config.ButtonName_Open)
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
                throw new NengaBoosterException("問番のテキストボックスを取得できません。");

            var controls = WindowStates.GetFormControlStatesArray(_config.TextBoxPoint_Toiban);
            if (controls.Length == 0)
                throw new NengaBoosterException("問番のテキストボックスを取得できません。");

            return controls[0].Enabled();
        }

        /// <summary>
        /// 問番テキストボックスが空ならtrueを返す。
        /// </summary>
        public bool IsToibanTextBoxEmpty()
        {
            if (!IsRunning() || !Exists())
                throw new NengaBoosterException("問番のテキストボックスを取得できません。");

            var controls = WindowStates.GetFormControlStatesArray(_config.TextBoxPoint_Toiban);
            if (controls.Length == 0)
                throw new NengaBoosterException("問番のテキストボックスを取得できません。");

            var toiban = controls[0].GetText();

            // 何故か空の問番テキストボックスからテキストを取得すると1234567890が返ってくる
            return String.IsNullOrEmpty(toiban) || toiban == "1234567890";
        }

        /// <summary>
        /// 組版依頼をかける。
        /// </summary>
        public async Task<bool> KumihanAsync()
        {
            if (!IsRunning() || !Exists())
                return false;

            return await WindowOperator
                .Activate()
                .LeftClick(_config.RadioButtonName_KumihanIrai)
                .Wait(50)
                .SendEnterTo(_config.ButtonName_Save)
                .ThrowIfFailed()
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
