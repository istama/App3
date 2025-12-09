
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IWarningPresenter _warningPresenter;

        private readonly LoginService _loginService;
        private readonly NaireRPAService _naireRPAService;
        private readonly ActiveNengaBoosterFormService _activeNengaBoosterFormService;
        private readonly NengaAppWindowFactory _nengaAppWindowFactory;

        private readonly IApplicationConfigRepository _applicationConfigRepository;
        private readonly IBehaviorConfigRepository _behaviorConfigRepository;
        private readonly IUserConfigRepository _userConfigRepository;


        public OpenNaireUseCase(
            IMainFormPresenter presenter,
            IWarningPresenter warningPresenter,
            LoginService loginService,
            NaireRPAService naireRPAService,
            ActiveNengaBoosterFormService activeNengaBoosterFormService,
            NengaAppWindowFactory nengaAppWindowFactory,
            IApplicationConfigRepository applicationConfigRepository,
            IBehaviorConfigRepository behaviorConfigRepository,
            IUserConfigRepository userConfigRepository)
        {
            Assert.IsNull(presenter, nameof(presenter));
            Assert.IsNull(warningPresenter, nameof(warningPresenter));
            Assert.IsNull(loginService, nameof(loginService));
            Assert.IsNull(naireRPAService, nameof(naireRPAService));
            Assert.IsNull(activeNengaBoosterFormService, nameof(activeNengaBoosterFormService));
            Assert.IsNull(nengaAppWindowFactory, nameof(nengaAppWindowFactory));
            Assert.IsNull(applicationConfigRepository, nameof(applicationConfigRepository));
            Assert.IsNull(behaviorConfigRepository, nameof(behaviorConfigRepository));
            Assert.IsNull(userConfigRepository, nameof(userConfigRepository));

            _presenter = presenter;
            _warningPresenter = warningPresenter;
            _loginService = loginService;
            _naireRPAService = naireRPAService;
            _activeNengaBoosterFormService = activeNengaBoosterFormService;
            _nengaAppWindowFactory = nengaAppWindowFactory;
            _applicationConfigRepository = applicationConfigRepository;
            _behaviorConfigRepository = behaviorConfigRepository;
            _userConfigRepository = userConfigRepository;
        }


        public async Task ExecuteAsync(Toiban toiban, IEnumerable<Toiban> outputToibanList)
        {
            // 注文名入れアプリとの連携設定を取得
            var applicationConfig = await _applicationConfigRepository.GetNaireAppilicationConfigAsync().ConfigureAwait(false);
            // 注文名入れウィンドウを取得
            var naireWindow = _nengaAppWindowFactory.GetOrCreateNaireWindow(applicationConfig);

            // 注文名入れウィンドウが開いていないなら
            if (!naireWindow.IsOpen(0))
            {
                // ログインして注文名入れアプリを起動する
                if (!await _loginService.ExecuteAsync(applicationConfig.Basic, naireWindow).ConfigureAwait(false))
                    return;
            }

            // 注文名入れアプリのダイアログを取得
            var dialogConfig = await _applicationConfigRepository.GetNaireDialogConfigAsync().ConfigureAwait(false);
            var dialog = _nengaAppWindowFactory.GetOrCreateNaireDialogWindow(dialogConfig);

            // 注文名入れの動作設定を取得
            var behaviorConfig = await _behaviorConfigRepository.GetNaireBehaviorConfigAsync().ConfigureAwait(false);
            // 注文名入れに問番を入力
            if (!await _naireRPAService.EnterToibanAsync(naireWindow, toiban, dialog, behaviorConfig).ConfigureAwait(false))
            {
                return;
            }

            // 工程が先に進んでいる問番で、かつその問番が出力リストに含まれているなら、間違いの可能性が高いため、大きく警告を出す
            if (dialog.IsOpen(0) && dialog.IsMovedForwardWorkProcessDialog())
            {
                // 出力リストの問番にこの問番が含まれているか
                if (outputToibanList.Contains(toiban))
                {
                    // 警告ダイアログを表示
                    _warningPresenter.ShowAlert("出力リストにある工程違いの問番を開いています。意図した問番を開いているか確認してください。");
                    //// TODO 後でダイアログを表示するサービスクラスに変更する
                    //System.Windows.Forms.MessageBox.Show(
                    //    "出力リストにある工程違いの問番を開いています。意図した問番を開いているか確認してください。",
                    //    "NengaBooster.exe",
                    //    System.Windows.Forms.MessageBoxButtons.OK,
                    //    System.Windows.Forms.MessageBoxIcon.Warning);

                    // TODO Boosterの色を警告カラーにする

                    return;
                }
            }

            // 再組版モードなら組版依頼を行う
            var openMode = await _userConfigRepository.GetNaireOpenModeAsync().ConfigureAwait(false);
            if (openMode == NaireOpenMode.Saikumi)
            {
                if (await _naireRPAService.ExecuteKumihanIraiAsync(naireWindow, dialog, behaviorConfig).ConfigureAwait(false))
                {
                    // NengaBoosterをアクティブにする
                    await _activeNengaBoosterFormService.ExecuteAsync().ConfigureAwait(false);
                }
            }

            // 問番を出力リストに追加する
            _presenter.AddToibanToCheckedList(toiban, true);
        }


    }
}