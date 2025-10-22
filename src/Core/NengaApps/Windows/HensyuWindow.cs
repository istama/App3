using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    /// <summary>
    /// 編集ウィンドウを操作するクラス。
    /// </summary>
    sealed class HensyuWindow : NengaAppWindowBasic
    {
        private readonly HensyuApplicationConfig _config;


        public HensyuWindow(INativeWindowFactory nativeWindowFactory, HensyuApplicationConfig config)
            : base(nativeWindowFactory, config.Basic)
        {
            Assert.IsNull(config, nameof(config));

            _config = config;
        }


        /// <summary>
        /// 問番を入力する。
        /// </summary>
        public async Task<bool> EnterToibanAsync(Toiban toiban)
        {
            if (!IsRunning() || !Exists())
                return false;

            // 問番を入力して開く
            return await WindowOperator
                .Activate()
                .Focus(_config.TextBoxPoint_Toiban)
                .SetText(_config.TextBoxPoint_Toiban, toiban)
                .Wait(50)
                .SendEnterTo(_config.ButtonName_Open)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 手組ウィンドウを開く。
        /// </summary>
        public async Task<Boolean> OpenTegumiWindowAsync()
        {
            if (!IsRunning() || !Exists())
                return false;

            return await WindowOperator
                .Activate()
                .SendEnterTo(_config.ButtonName_Tegumi)
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
            return String.IsNullOrEmpty(toiban);
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
