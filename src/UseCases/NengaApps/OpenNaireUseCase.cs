
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.Interfaces;
using IsTama.NengaBooster.UseCases.Presenters;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.Utils;

namespace IsTama.NengaBooster.UseCases.NengaApps
{
    sealed class OpenNaireUseCase : IOpenNaireUseCase
    {
        private readonly IMainFormPresenter _presenter;

        private readonly LoginService _loginService;
        private readonly NaireRPAService _naireRPAService;
        private readonly NengaAppWindowFactory _nengaAppWindowFactory;

        private readonly IApplicationConfigRepository _applicationConfigRepository;
        private readonly IBehaviorConfigRepository _behaviorConfigRepository;
        private readonly IUserConfigRepository _userConfigRepository;


        public OpenNaireUseCase(
            IMainFormPresenter presenter,
            LoginService loginService,
            NaireRPAService naireRPAService,
            NengaAppWindowFactory nengaAppWindowFactory,
            IApplicationConfigRepository applicationConfigRepository,
            IBehaviorConfigRepository behaviorConfigRepository,
            IUserConfigRepository userConfigRepository)
        {
            Assert.IsNull(presenter, nameof(presenter));
            Assert.IsNull(loginService, nameof(loginService));
            Assert.IsNull(naireRPAService, nameof(naireRPAService));
            Assert.IsNull(nengaAppWindowFactory, nameof(nengaAppWindowFactory));
            Assert.IsNull(applicationConfigRepository, nameof(applicationConfigRepository));
            Assert.IsNull(behaviorConfigRepository, nameof(behaviorConfigRepository));
            Assert.IsNull(userConfigRepository, nameof(userConfigRepository));

            _presenter = presenter;
            _loginService = loginService;
            _naireRPAService = naireRPAService;
            _nengaAppWindowFactory = nengaAppWindowFactory;
            _applicationConfigRepository = applicationConfigRepository;
            _behaviorConfigRepository = behaviorConfigRepository;
            _userConfigRepository = userConfigRepository;
        }


        public async Task ExecuteAsync(Toiban toiban)
        {
            // 注文名入れアプリとの連携設定を取得
            var applicationConfig = await _applicationConfigRepository.GetNaireAppilicationConfigAsync().ConfigureAwait(false);
            // 注文名入れウィンドウを取得
            var window = _nengaAppWindowFactory.GetOrCreateNaireWindow(applicationConfig);

            // ログインして注文名入れアプリを起動する
            if (!await _loginService.ExecuteAsync(applicationConfig.Basic, window).ConfigureAwait(false))
                return;

            // 注文名入れアプリのダイアログを取得
            var dialogConfig = await _applicationConfigRepository.GetNaireDialogConfigAsync().ConfigureAwait(false);
            var dialog = _nengaAppWindowFactory.GetOrCreateNaireDialogWindow(dialogConfig);

            // 注文名入れの動作設定を取得
            var behaviorConfig = await _behaviorConfigRepository.GetNaireBehaviorConfigAsync().ConfigureAwait(false);
            // 注文名入れに問番を入力
            if (!await _naireRPAService.EnterToibanAsync(window, toiban, dialog, behaviorConfig).ConfigureAwait(false))
                return;

            // 再組版モードなら組版依頼を行う
            var openMode = await _userConfigRepository.GetNaireOpenModeAsync().ConfigureAwait(false);
            if (openMode == NaireOpenMode.Saikumi)
            {
                if (await _naireRPAService.ExecuteKumihanIraiAsync(window, dialog, behaviorConfig).ConfigureAwait(false))
                {
                    // TODO NengaBoosterをアクティブにする
                }
            }

            // 問番を出力リストに追加する
            _presenter.AddToibanToCheckedList(toiban);
        }


    }
}