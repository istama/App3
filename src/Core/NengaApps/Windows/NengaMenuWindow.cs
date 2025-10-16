using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.Error;

namespace IsTama.NengaBooster.Core.NengaApps
{
    /// <summary>
    /// 年賀メニューのウィンドウを操作するクラス。
    /// </summary>
    sealed class NengaMenuWindow : NengaAppWindowBasic
    {
        //private readonly NengaMenuConfig _config;

        public NengaMenuWindow(INativeWindowFactory nativeWindowFactory, NengaMenuConfig nengaMenuConfig)
            : base(nativeWindowFactory, nengaMenuConfig.Basic)
        {
            //Assert.IsNull(nengaMenuConfig, nameof(nengaMenuConfig));
            //_config = nengaMenuConfig;
        }


        /// <summary>
        /// 年賀アプリを起動する。
        /// </summary>
        public async Task<Boolean> ExecuteNengaAppAsync(String applicationNameOnNengaMenu)
        {
            if (!base.IsRunning())
                throw new NengaBoosterException("年賀メニューが起動していません。");

            return await WindowOperator
                .Activate()
                .LeftClick(applicationNameOnNengaMenu)
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }
    }
}
