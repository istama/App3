
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
            // 編集アプリとの連携設定を取得
            var applicationConfig = await _applicationConfigRepository.GetHensyuAppilicationConfigAsync().ConfigureAwait(false);
            // 編集ウィンドウを取得
            var hensyuWindow = _nengaAppWindowFactory.GetOrCreateHensyuWindow(applicationConfig);

            // 編集ウィンドウが開かれていないなら
            if (!hensyuWindow.IsOpen(0))
            {
                // ログインして編集アプリを起動する
                if (!await _loginService.ExecuteAsync(applicationConfig.Basic, hensyuWindow).ConfigureAwait(false))
                    return;
            }

            // ダイアログウィンドウを取得
            var dialogConfig = await _applicationConfigRepository.GetHensyuDialogConfigAsync().ConfigureAwait(false);
            var dialog = _nengaAppWindowFactory.GetOrCreateHensyuDialogWindow(dialogConfig);

            // 編集アプリの動作設定を取得
            var behaviorConfig = await _behaviorConfigRepository.GetHensyuBehaviorConfigAsync().ConfigureAwait(false);

            // 編集アプリに問番を入力する
            if (!await _hensyuRPAService.EnterToibanAsync(hensyuWindow, toiban, dialog, behaviorConfig).ConfigureAwait(false))
                return;

            // 動作モードが「手組編集画面まで開く」の場合
            var openMode = await _userConfigRepository.GetHensyuOpenModeAsync().ConfigureAwait(false);
            if (openMode == HensyuOpenMode.TegumiWindow)
            {
                // 手組編集画面を開く
                await _hensyuRPAService.OpenTegumiWindowAsync(hensyuWindow, dialog).ConfigureAwait(false);
            }

            // 問番を出力リストに追加する
            _presenter.AddToibanToCheckedList(toiban);
        }
    }
}