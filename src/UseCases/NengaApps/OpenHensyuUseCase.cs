
using System;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.Presenters;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.NengaBooster.UseCases.Interfaces;
using IsTama.Utils;

namespace IsTama.NengaBooster.UseCases.NengaApps
{
    sealed class OpenHensyuUseCase : IOpenHensyuUseCase
    {
        private readonly IMainFormPresenter _presenter;

        private readonly LoginService _loginService;
        private readonly HensyuRPAService _hensyuRPAService;
        private readonly NengaAppWindowFactory _nengaAppWindowFactory;

        private readonly IApplicationConfigRepository _applicationConfigRepository;
        private readonly IBehaviorConfigRepository _behaviorConfigRepository;
        private readonly IUserConfigRepository _userConfigRepository;


        public OpenHensyuUseCase(
            IMainFormPresenter presenter,
            LoginService loginService,
            HensyuRPAService hensyuRPAService,
            NengaAppWindowFactory nengaAppWindowFactory,
            IApplicationConfigRepository applicationConfigRepository,
            IBehaviorConfigRepository behaviorConfigRepository,
            IUserConfigRepository userConfigRepository)
        {
            Assert.IsNull(presenter, nameof(presenter));
            Assert.IsNull(loginService, nameof(loginService));
            Assert.IsNull(hensyuRPAService, nameof(hensyuRPAService));
            Assert.IsNull(nengaAppWindowFactory, nameof(nengaAppWindowFactory));
            Assert.IsNull(applicationConfigRepository, nameof(applicationConfigRepository));
            Assert.IsNull(behaviorConfigRepository, nameof(behaviorConfigRepository));
            Assert.IsNull(userConfigRepository, nameof(userConfigRepository));

            _presenter = presenter;
            _loginService = loginService;
            _hensyuRPAService = hensyuRPAService;
            _nengaAppWindowFactory = nengaAppWindowFactory;
            _applicationConfigRepository = applicationConfigRepository;
            _behaviorConfigRepository = behaviorConfigRepository;
            _userConfigRepository = userConfigRepository;
        }


        public async Task ExecuteAsync(Toiban toiban)
        {
            var applicationConfig = await _applicationConfigRepository.GetHensyuAppilicationConfigAsync().ConfigureAwait(false);
            var window = _nengaAppWindowFactory.GetOrCreateHensyuWindow(applicationConfig);

            if (!await _loginService.ExecuteAsync(applicationConfig.Basic, window).ConfigureAwait(false))
                return;

            var dialogConfig = await _applicationConfigRepository.GetHensyuDialogConfigAsync().ConfigureAwait(false);
            var dialog = _nengaAppWindowFactory.GetOrCreateHensyuDialogWindow(dialogConfig);

            var behaviorConfig = await _behaviorConfigRepository.GetHensyuBehaviorConfigAsync().ConfigureAwait(false);

            if (!await _hensyuRPAService.EnterToibanAsync(window, toiban, dialog, behaviorConfig).ConfigureAwait(false))
                return;

            var openMode = await _userConfigRepository.GetHensyuOpenModeAsync().ConfigureAwait(false);
            if (openMode == HensyuOpenMode.TegumiWindow)
            {
                await _hensyuRPAService.OpenTegumiWindowAsync(window).ConfigureAwait(false);
            }

            _presenter.AddToibanToCheckedList(toiban);
        }
    }
}