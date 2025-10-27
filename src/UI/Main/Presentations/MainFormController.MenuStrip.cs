using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.NengaBooster.UseCases.NengaApps;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    partial class MainFormController
    {
        /// <summary>
        /// 動作モードの右クリックメニューの状態を読み込む。
        /// </summary>
        private async Task LoadOperationModeMenuStripCheckStateAsync()
        {
            var repos = _repositories.UserConfigRepository;

            var toibanSelectMode = await repos.GetToibanSelectModeAsync().ConfigureAwait(false);
            await SetToibanSelectModeAsync(toibanSelectMode).ConfigureAwait(false);

            var hensyuOpenMode = await repos.GetHensyuOpenModeAsync().ConfigureAwait(false);
            await SetHensyuOpenModeAsync(hensyuOpenMode).ConfigureAwait(false);

            var infoOpenMode = await repos.GetInformationOpenModeAsync().ConfigureAwait(false);
            await SetInformationOpenModeAsync(infoOpenMode).ConfigureAwait(false);

            var shouldAdd = await repos.ShouldAddToibanToCheckedListWhenInformationSearchAsync().ConfigureAwait(false);
            await SetWhetherToAddTobanToCheckedListWhenInformationOpenAsync(shouldAdd ? CheckState.Checked : CheckState.Unchecked).ConfigureAwait(false);

            var shouldUncheck = await repos.ShouldUncheckToibanFromCheckedListWhenEnterToKouseishiAsync().ConfigureAwait(false);
            await SetWhetherToUncheckToibanFromCheckedListWhenEnterToKouseishiAsync(shouldUncheck ? CheckState.Checked : CheckState.Unchecked).ConfigureAwait(false);

            var checkedListClearMode = await repos.GetToibanCheckedListClearModeAsync().ConfigureAwait(false);
            await SetToibanCheckedListClearModeAsync(checkedListClearMode).ConfigureAwait(false);
        }

        /// <summary>
        /// ２つの引数が同じ値ならCheckState.Checkedを返す。異なるならCheckState.Uncheckedを返す。
        /// </summary>
        private CheckState ReturnCheckedIfSame<T>(T value, T expect)
        {
            return value.Equals(expect) ? CheckState.Checked : CheckState.Unchecked;
        }
        /// <summary>
        /// CheckStateをCheckedとUncheckedを反転させて返す。
        /// </summary>
        private CheckState ReverseChecked(CheckState check)
        {
            return check == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
        }

        /// <summary>
        /// 問番テキストボックスを選択状態にするモードを変更する。
        /// </summary>
        public Task SetToibanSelectModeToByClickAsync()
        {
            return SetToibanSelectModeAsync(ToibanSelectMode.ByClick);
        }
        public Task SetToibanSelectModeToByWClickAsync()
        {
            return SetToibanSelectModeAsync(ToibanSelectMode.ByWClick);
        }
        private Task SetToibanSelectModeAsync(ToibanSelectMode mode)
        {
            _viewmodel.ToibanSelectMode_ByClick = ReturnCheckedIfSame(mode, ToibanSelectMode.ByClick);
            _viewmodel.ToibanSelectMode_ByWClick = ReturnCheckedIfSame(mode, ToibanSelectMode.ByWClick);
            return _repositories.UserConfigRepository.SetToibanSelectModeAsync(mode);
        }

        /// <summary>
        /// 編集を開いた時の動作モードを変更する。
        /// </summary>
        public Task SetHensyuOpenModeToMenuWindowAsync()
        {
            return SetHensyuOpenModeAsync(HensyuOpenMode.MenuWindow);
        }
        public Task SetHensyuOpenModeToTegumiWindowAsync()
        {
            return SetHensyuOpenModeAsync(HensyuOpenMode.TegumiWindow);
        }
        private Task SetHensyuOpenModeAsync(HensyuOpenMode mode)
        {
            _viewmodel.HensyuOpenMode_Menu = ReturnCheckedIfSame(mode, HensyuOpenMode.MenuWindow);
            _viewmodel.HensyuOpenMode_Tegumi = ReturnCheckedIfSame(mode, HensyuOpenMode.TegumiWindow);
            return _repositories.UserConfigRepository.SetHensyuOpenModeAsync(mode);
        }

        /// <summary>
        /// インフォメーションを開いた時の動作モードを変更する。
        /// </summary>
        public Task SetInformationOpenModeToSearchFormAsync()
        {
            return SetInformationOpenModeAsync(InformationOpenMode.SearchForm);
        }
        public Task SetInformationOpenModeToDetailWindowAsync()
        {
            return SetInformationOpenModeAsync(InformationOpenMode.DetailWindow);
        }
        public Task SetInformationOpenModeToKouseisPageAsync()
        {
            return SetInformationOpenModeAsync(InformationOpenMode.KouseiPage);
        }
        public Task SetInformationOpenModeToKumihanPageAsync()
        {
            return SetInformationOpenModeAsync(InformationOpenMode.KumihanPage);
        }
        private Task SetInformationOpenModeAsync(InformationOpenMode mode)
        {
            _viewmodel.InformationOpenMode_SearchForm = ReturnCheckedIfSame(mode, InformationOpenMode.SearchForm);
            _viewmodel.InformationOpenMode_DetailWindow = ReturnCheckedIfSame(mode, InformationOpenMode.DetailWindow);
            _viewmodel.InformationOpenMode_KouseiPage = ReturnCheckedIfSame(mode, InformationOpenMode.KouseiPage);
            _viewmodel.InformationOpenMode_KumihanPage = ReturnCheckedIfSame(mode, InformationOpenMode.KumihanPage);
            return _repositories.UserConfigRepository.SetInformationOpenModeAsync(mode);
        }

        /// <summary>
        /// インフォメーションを開いたときに問番を出力リストに追加するかどうかの設定を変更する。
        /// </summary>
        public async Task ChangeWhetherToAddTobanToCheckedListWhenInformationOpenAsync()
        {
            var checkState = ReverseChecked(_viewmodel.ShouldAddToibanToCheckedList);
            await SetWhetherToAddTobanToCheckedListWhenInformationOpenAsync(checkState).ConfigureAwait(false);
        }
        private async Task SetWhetherToAddTobanToCheckedListWhenInformationOpenAsync(CheckState checkState)
        {
            _viewmodel.ShouldAddToibanToCheckedList = checkState;
            await _repositories.UserConfigRepository.SetWhetherToAddToibanToCheckedListWhenInformationSearchAsync(checkState == CheckState.Checked);
        }

        /// <summary>
        /// 校正紙出力に送った出力リストの問番のチェックを外すかどうかの設定を変更する。
        /// </summary>
        public async Task ChangeWhetherToUncheckToibanFromCheckedListWhenEnterToKouseishiAsync()
        {
            var checkState = ReverseChecked(_viewmodel.ShouldUncheckToibanFromCheckedList);
            await SetWhetherToUncheckToibanFromCheckedListWhenEnterToKouseishiAsync(checkState).ConfigureAwait(false);
        }
        private async Task SetWhetherToUncheckToibanFromCheckedListWhenEnterToKouseishiAsync(CheckState checkState)
        {
            _viewmodel.ShouldUncheckToibanFromCheckedList = checkState;
            await _repositories.UserConfigRepository.SetWhetherToUncheckToibanFromCheckedListWhenEnterToKouseishiAsync(checkState == CheckState.Checked);
        }

        /// <summary>
        /// 出力リストをクリアしたときの動作モードを変更する。
        /// </summary>
        public Task SetToibanCheckedListClearModeToAllAsync()
        {
            return SetToibanCheckedListClearModeAsync(ToibanCheckedListClearMode.All);
        }
        public Task SetToibanCheckedListClearModeToCheckedOnlyAsync()
        {
            return SetToibanCheckedListClearModeAsync(ToibanCheckedListClearMode.CheckedOnly);
        }
        public Task SetToibanCheckedListClearModeToUncheckedOnlyAsync()
        {
            return SetToibanCheckedListClearModeAsync(ToibanCheckedListClearMode.UncheckedOnly);
        }
        private Task SetToibanCheckedListClearModeAsync(ToibanCheckedListClearMode mode)
        {
            _viewmodel.ToibanCheckedListClearMode_All = ReturnCheckedIfSame(mode, ToibanCheckedListClearMode.All);
            _viewmodel.ToibanCheckedListClearMode_CheckedOnly = ReturnCheckedIfSame(mode, ToibanCheckedListClearMode.CheckedOnly);
            _viewmodel.ToibanCheckedListClearMode_UncheckedOnly = ReturnCheckedIfSame(mode, ToibanCheckedListClearMode.UncheckedOnly);
            return _repositories.UserConfigRepository.SetToibanCheckedListClearModeAsync(mode);
        }


        /// <summary>
        /// 問番テキストボックスの問番を校正紙出力へ送る。
        /// </summary>
        public async Task EnterTextBoxToibanToKouseishiAsync()
        {
            await _usecases.PrintKouseishi.ExecuteAsync(_viewmodel.Toiban).ConfigureAwait(false);
        }

        /// <summary>
        /// 指定したインデックスの出力リストの問番を校正紙出力へ送る。
        /// </summary>
        public async Task EnterToibanToKouseishiAsyncAt(Int32 index)
        {
            if (index < 0 || index >= _viewmodel.ToibanCheckedList.Count)
                return;

            var toiban = _viewmodel.ToibanCheckedList.Skip(index).Take(1).Select(item => Toiban.Create(item.Item2)).First();
            await _usecases.PrintKouseishi.ExecuteAsync(toiban).ConfigureAwait(false);
        }

        /// <summary>
        /// 指定したインデックス以降の出力リストの問番を校正紙出力へ送る。
        /// </summary>
        public async Task EnterToibanListToKouseishiAsyncAfter(Int32 index)
        {
            if (index < 0 || index >= _viewmodel.ToibanCheckedList.Count)
                return;

            var toibanList = _viewmodel.ToibanCheckedList.Skip(index).Select(item => Toiban.Create(item.Item2)).ToList();
            await _usecases.PrintKouseishi.ExecuteAsync(toibanList).ConfigureAwait(false);
        }

        /// <summary>
        /// 問番出力リストのチェック状態をすべて反転させる。
        /// </summary>
        public void ReverseCheckStateAllInCheckedList()
        {
            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            UpdateToibanCheckedList(helper.ReverseCheckAll());
        }
    }
}
