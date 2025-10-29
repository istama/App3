using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;

namespace IsTama.NengaBooster.UseCases.NengaApps
{
    sealed class ActiveNengaBoosterFormService
    {
        private readonly string _nengaBoosterWindowTitlePattern;
        private readonly INativeWindowFactory _nativeWindowFactory;


        public ActiveNengaBoosterFormService(string nengaBoosterWindowTitlePattern, INativeWindowFactory nativeWindowFactory)
        {
            _nengaBoosterWindowTitlePattern = nengaBoosterWindowTitlePattern;
            _nativeWindowFactory = nativeWindowFactory;
        }


        public async Task ExecuteAsync()
        {
            var nengaBooster = _nativeWindowFactory.GetOrCreateWindowOperator(_nengaBoosterWindowTitlePattern);

            // NengaBoosterをアクティブにする
            await nengaBooster
                .Activate()
                .Focus(new Point(22, 63))
                .ThrowIfFailed()
                .DoAsync()
                .ConfigureAwait(false);
        }
    }
}
