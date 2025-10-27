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
    /// インフォメーション詳細ウィンドウを操作するクラス。
    /// </summary>
    sealed class InformationDetailWindow : NengaAppWindowBasic
    {
        private readonly InformationDetailApplicationConfig _config;


        public InformationDetailWindow(INativeWindowFactory nativeWindowFactory, InformationDetailApplicationConfig config)
            : base(nativeWindowFactory, config.Basic)
        {
            Assert.IsNull(config, nameof(config));

            _config = config;
        }


        /// <summary>
        /// 校正ページを開く。
        /// </summary>
        public async Task<Boolean> OpenKouseiPageAsync(int waitTimeForWindowToOpen_ms)
        {
            if (!IsRunning() || !Exists())
                return false;

            if (!await ActivateAsync(waitTimeForWindowToOpen_ms).ConfigureAwait(false))
                return false;

            if (!IsOpen(waitTimeForWindowToOpen_ms))
                return false;

            return await WindowOperator
                .Activate()
                .LeftClick(_config.ButtonName_KouseiPage)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 組版ページを開く。
        /// </summary>
        public async Task<Boolean> OpenKumihanPageAsync(int waitTimeForWindowToOpen_ms)
        {
            if (!IsRunning() || !Exists())
                return false;

            if (!await ActivateAsync(waitTimeForWindowToOpen_ms).ConfigureAwait(false))
                return false;

            if (!IsOpen(waitTimeForWindowToOpen_ms))
                return false;

            return await WindowOperator
                .Activate()
                .LeftClick(_config.ButtonName_KumihanPage)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// ウィンドウを閉じる。
        /// </summary>
        public async Task<Boolean> CloseAsync()
        {
            if (!IsRunning() || !Exists())
                return true;

            if (!IsOpen(0))
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
