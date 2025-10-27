
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
    sealed class OpenInformationUseCase : IOpenInformationUseCase
    {
        private readonly IMainFormPresenter _presenter;

        private readonly LoginService _loginService;
        private readonly InformationRPAService _infoRPAService;
        private readonly NengaAppWindowFactory _nengaAppWindowFactory;

        private readonly IApplicationConfigRepository _applicationConfigRepository;
        private readonly IBehaviorConfigRepository _behaviorConfigRepository;
        private readonly IUserConfigRepository _userConfigRepository;


        public OpenInformationUseCase(
            IMainFormPresenter presenter,
            LoginService loginService,
            InformationRPAService infoRPAService,
            NengaAppWindowFactory nengaAppWindowFactory,
            IApplicationConfigRepository applicationConfigRepository,
            IBehaviorConfigRepository behaviorConfigRepository,
            IUserConfigRepository userConfigRepository)
        {
            Assert.IsNull(presenter, nameof(presenter));
            Assert.IsNull(loginService, nameof(loginService));
            Assert.IsNull(infoRPAService, nameof(infoRPAService));
            Assert.IsNull(nengaAppWindowFactory, nameof(nengaAppWindowFactory));
            Assert.IsNull(applicationConfigRepository, nameof(applicationConfigRepository));
            Assert.IsNull(behaviorConfigRepository, nameof(behaviorConfigRepository));
            Assert.IsNull(userConfigRepository, nameof(userConfigRepository));

            _presenter = presenter;
            _loginService = loginService;
            _infoRPAService = infoRPAService;
            _nengaAppWindowFactory = nengaAppWindowFactory;
            _applicationConfigRepository = applicationConfigRepository;
            _behaviorConfigRepository = behaviorConfigRepository;
            _userConfigRepository = userConfigRepository;
        }


        public async Task ExecuteAsync(Toiban toiban)
        {
            var detailAppConfig = await _applicationConfigRepository.GetInformationDetailAppilicationConfigAsync().ConfigureAwait(false);
            var detailWindow = _nengaAppWindowFactory.GetOrCreateInformationDetailWindow(detailAppConfig);

            // 詳細ウィンドウがすでに開かれているなら閉じる
            if (detailWindow.IsOpen(0))
            {
                await detailWindow.CloseAsync().ConfigureAwait(false);
            }

            var infoAppConfig = await _applicationConfigRepository.GetInformationAppilicationConfigAsync().ConfigureAwait(false);
            var infoWindow = _nengaAppWindowFactory.GetOrCreateInformationWindow(infoAppConfig);

            // 検索ウィンドウが開かれていないならログインする
            if (!infoWindow.IsOpen(0))
            {
                if (!await _loginService.ExecuteAsync(infoAppConfig.Basic, infoWindow).ConfigureAwait(false))
                    return;
            }

            var dialogConfig = await _applicationConfigRepository.GetInformationDialogConfigAsync().ConfigureAwait(false);
            var dialog = _nengaAppWindowFactory.GetOrCreateInformationDialogWindow(dialogConfig);

            // インフォメーション検索に問番を入力する
            if (!await _infoRPAService.EnterToibanAsync(infoWindow, toiban, detailWindow, dialog).ConfigureAwait(false))
            {
                return;
            }

            // 動作モードが詳細ウィンドウを開く設定か
            var openMode = await _userConfigRepository.GetInformationOpenModeAsync().ConfigureAwait(false);
            if (openMode == InformationOpenMode.DetailWindow)
            {
                await _infoRPAService.OpenDetailWindowAsync(infoWindow, detailWindow).ConfigureAwait(false);
            }
            else if (openMode == InformationOpenMode.KouseiPage)
            {
                await _infoRPAService.OpenKouseiPageAsync(infoWindow, detailWindow).ConfigureAwait(false);
            }
            else if (openMode == InformationOpenMode.KumihanPage)
            {
                await _infoRPAService.OpenKumihanPageAsync(infoWindow, detailWindow).ConfigureAwait(false);
            }

            // 問番を出力リストに追加する設定か
            var shouldAddToibanToCheckedList = await _userConfigRepository.ShouldAddToibanToCheckedListWhenInformationSearchAsync().ConfigureAwait(false);
            if (shouldAddToibanToCheckedList)
            {
                _presenter.AddToibanToCheckedList(toiban);
            }
        }
    }
}