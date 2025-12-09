
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IWarningPresenter _warningPresenter;

        private readonly LoginService _loginService;
        private readonly HensyuRPAService _hensyuRPAService;
        private readonly NengaAppWindowFactory _nengaAppWindowFactory;

        private readonly IApplicationConfigRepository _applicationConfigRepository;
        private readonly IBehaviorConfigRepository _behaviorConfigRepository;
        private readonly IUserConfigRepository _userConfigRepository;


        public OpenHensyuUseCase(
            IMainFormPresenter presenter,
            IWarningPresenter warningPresenter,
            LoginService loginService,
            HensyuRPAService hensyuRPAService,
            NengaAppWindowFactory nengaAppWindowFactory,
            IApplicationConfigRepository applicationConfigRepository,
            IBehaviorConfigRepository behaviorConfigRepository,
            IUserConfigRepository userConfigRepository)
        {
            Assert.IsNull(presenter, nameof(presenter));
            Assert.IsNull(warningPresenter, nameof(warningPresenter));
            Assert.IsNull(loginService, nameof(loginService));
            Assert.IsNull(hensyuRPAService, nameof(hensyuRPAService));
            Assert.IsNull(nengaAppWindowFactory, nameof(nengaAppWindowFactory));
            Assert.IsNull(applicationConfigRepository, nameof(applicationConfigRepository));
            Assert.IsNull(behaviorConfigRepository, nameof(behaviorConfigRepository));
            Assert.IsNull(userConfigRepository, nameof(userConfigRepository));

            _presenter = presenter;
            _warningPresenter = warningPresenter;
            _loginService = loginService;
            _hensyuRPAService = hensyuRPAService;
            _nengaAppWindowFactory = nengaAppWindowFactory;
            _applicationConfigRepository = applicationConfigRepository;
            _behaviorConfigRepository = behaviorConfigRepository;
            _userConfigRepository = userConfigRepository;
        }


        public async Task ExecuteAsync(Toiban toiban, IEnumerable<Toiban> outputToibanList)
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

            // 動作モードが「手組編集画面まで開く」の場合
            var openMode = await _userConfigRepository.GetHensyuOpenModeAsync().ConfigureAwait(false);
            if (openMode == HensyuOpenMode.TegumiWindow)
            {
                // 手組編集画面を開く
                await _hensyuRPAService.OpenTegumiWindowAsync(hensyuWindow, dialog).ConfigureAwait(false);
            }

            // 問番を出力リストに追加する
            _presenter.AddToibanToCheckedList(toiban, true);
        }
    }
}