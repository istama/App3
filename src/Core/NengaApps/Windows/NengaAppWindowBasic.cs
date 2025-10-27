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
    /// ウィンドウの基本的な操作をまとめたクラス。
    /// 個別のウィンドウクラスはこのクラスを継承して定義する。
    /// </summary>
    class NengaAppWindowBasic
    {
        private readonly INativeWindowFactory _nativeWindowFactory;
        private readonly ApplicationBasicConfig _config;


        public NengaAppWindowBasic(INativeWindowFactory nativeWindowFactory, ApplicationBasicConfig basicConfig)
        {
            Assert.IsNull(nativeWindowFactory, nameof(nativeWindowFactory));
            Assert.IsNull(basicConfig, nameof(basicConfig));

            _nativeWindowFactory = nativeWindowFactory;
            _config = basicConfig;
        }


        private protected INativeWindowStates WindowStates
            => _nativeWindowFactory.GetOrCreateWindowStates(_config.WindowTitlePattern, _config.WindowWidth);

        private protected INativeWindowOperator WindowOperator
            => _nativeWindowFactory.GetOrCreateWindowOperator(_config.WindowTitlePattern, _config.WindowWidth);


        /// <summary>
        /// アプリケーションが起動していないという例外を投げる。
        /// </summary>
        public void ThrowNengaBoosterExceptionBecauseApplicationNotRun()
        {
            throw new NengaBoosterException($"{_config.ApplicationName_OnNengaMenu} が起動していません。");
        }

        /// <summary>
        /// このウィンドウのアプリケーション名を返す。
        /// </summary>
        /// <returns></returns>
        public string GetNengaApplicationNameOnNengaMenu()
        {
            return _config.ApplicationName_OnNengaMenu;
        }

        /// <summary>
        /// この年賀アプリケーションのプロセスが起動しているならtrueを返す。
        /// </summary>
        public bool IsRunning()
        {
            var processes = Process.GetProcessesByName(_config.ProcessName);
            return processes.Length > 0;
        }

        /// <summary>
        /// この年賀アプリケーションのウィンドウが存在するならtrueを返す。
        /// </summary>
        public bool Exists()
        {
            return Exists(0);
        }

        public bool Exists(int waittime_ms)
        {
            return WindowStates.Exists(waittime_ms);
        }

        /// <summary>
        /// この年賀アプリケーションのウィンドウが開いているならtrueを返す。
        /// </summary>
        public bool IsOpen(Int32 waittime_ms)
        {
            return WindowStates.IsOpen(waittime_ms);
        }

        /// <summary>
        /// この年賀アプリケーションのウィンドウがアクティブ状態ならtrueを返す。
        /// </summary>
        public bool IsActivated(Int32 waittime_ms)
        {
            return WindowStates.IsActivated(waittime_ms);
        }

        /// <summary>
        /// この年賀アプリケーションのウィンドウをアクティブ状態にする。
        /// </summary>
        public async Task<bool> ActivateAsync(Int32 waittime_ms)
        {
            if (!IsRunning())
            {
                //System.Windows.Forms.MessageBox.Show("window is not running");
                return false;
            }

            if (!Exists(waittime_ms))
            {
                //System.Windows.Forms.MessageBox.Show("window not exists");
                return false;
            }

            if (!IsOpen(waittime_ms))
            {
                //System.Windows.Forms.MessageBox.Show("window is not open");
                return false;
            }

            return await WindowOperator
                .Activate()
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }
    }
}
