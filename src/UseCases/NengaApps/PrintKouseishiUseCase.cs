using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.Interfaces;
using IsTama.NengaBooster.UseCases.Presenters;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.Utils;

namespace IsTama.NengaBooster.UseCases.NengaApps
{
    sealed class PrintKouseishiUseCase : IPrintKouseishiUseCase
    {
        private readonly IMainFormPresenter _presenter;
        private readonly LoginService _loginService;
        private readonly KouseishiRPAService _kouseishiRPAService;
        private readonly NengaAppWindowFactory _nengaAppWindowFactory;

        private readonly IApplicationConfigRepository _applicationConfigRepository;
        private readonly IBehaviorConfigRepository _behaviorConfigRepository;
        private readonly IUserConfigRepository _userConfigRepository;

        public PrintKouseishiUseCase(
            IMainFormPresenter presenter,
            LoginService loginService,
            KouseishiRPAService kouseishiRPAService,
            NengaAppWindowFactory nengaAppWindowFactory,
            IApplicationConfigRepository applicationConfigRepository,
            IBehaviorConfigRepository behaviorConfigRepository,
            IUserConfigRepository userConfigRepository)
        {
            Assert.IsNull(presenter, nameof(presenter));
            Assert.IsNull(loginService, nameof(loginService));
            Assert.IsNull(kouseishiRPAService, nameof(kouseishiRPAService));
            Assert.IsNull(nengaAppWindowFactory, nameof(nengaAppWindowFactory));
            Assert.IsNull(applicationConfigRepository, nameof(applicationConfigRepository));
            Assert.IsNull(behaviorConfigRepository, nameof(behaviorConfigRepository));
            Assert.IsNull(userConfigRepository, nameof(userConfigRepository));

            _presenter = presenter;
            _loginService = loginService;
            _kouseishiRPAService = kouseishiRPAService;
            _nengaAppWindowFactory = nengaAppWindowFactory;
            _applicationConfigRepository = applicationConfigRepository;
            _behaviorConfigRepository = behaviorConfigRepository;
            _userConfigRepository = userConfigRepository;
        }


        public async Task ExecuteAsync(Toiban toiban)
        {
            await ExecuteAsync(new List<Toiban> { toiban });
        }

        public async Task ExecuteAsync(List<Toiban> toibanList)
        {
            var applicationConfig = await _applicationConfigRepository.GetKouseishiAppilicationConfigAsync().ConfigureAwait(false);
            var kouseishiWindow = _nengaAppWindowFactory.GetOrCreateKouseishiWindow(applicationConfig);

            if (!kouseishiWindow.IsOpen(0))
            {
                if (!await _loginService.ExecuteAsync(applicationConfig.Basic, kouseishiWindow).ConfigureAwait(false))
                {
                    return;
                }
            }

            var dialogConfig = await _applicationConfigRepository.GetKouseishiDialogConfigAsync().ConfigureAwait(false);
            var dialog = _nengaAppWindowFactory.GetOrCreateKouseishiDialogWindow(dialogConfig);

            var shouldUncheck = await _userConfigRepository.ShouldUncheckToibanFromCheckedListWhenEnterToKouseishiAsync().ConfigureAwait(false);

            for (var i = 0; i < toibanList.Count; i++)
            {
                var toiban = toibanList[i];
                if (!await _kouseishiRPAService.EnterToibanAsync(kouseishiWindow, toiban, dialog).ConfigureAwait(false))
                {
                    return;
                }

                // 動作モードが「出力リストのチェックを外す」の場合
                if (shouldUncheck)
                {
                    _presenter.UncheckToibanFromCheckedListAt(toiban);
                }

                // 最後の問番の場合
                if (i >= toibanList.Count)
                    break;

                await Task.Delay(10).ConfigureAwait(false);
            }
        }
    }
}
