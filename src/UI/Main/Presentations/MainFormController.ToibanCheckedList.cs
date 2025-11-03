using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    partial class MainFormController
    {
        /// <summary>
        /// 出力リストの指定したインデックスの問番の順番を１つ上げる。
        /// </summary>
        public void RaiseSelectedToibanInCheckedListAt(Int32 index)
        {
            if (index <= 0 || index >= _viewmodel.ToibanCheckedList.Count)
                return;

            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            UpdateToibanCheckedList(helper.RaiseAt(index), index - 1);
        }

        /// <summary>
        /// 出力リストの指定したインデックスの問番の順番を１つ下げる。
        /// </summary>
        public void LowerSelectedToibanInCheckedListAt(Int32 index)
        {
            if (index < 0 || index >= _viewmodel.ToibanCheckedList.Count - 1)
                return;

            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            UpdateToibanCheckedList(helper.LowerAt(index), index + 1);
        }

        /// <summary>
        /// 出力リストの問番を校正紙出力に入力する。
        /// </summary>
        public async Task EnterToibanListToKouseishiAsync()
        {
            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            var checkedList = helper.ToCheckedToibanList();
            await _usecases.PrintKouseishi.ExecuteAsync(checkedList).ConfigureAwait(false);
        }

        /// <summary>
        /// 出力リストから指定したインデックスの問番を削除する。
        /// </summary>
        public void RemoveSelectedToibanFromCheckedListAt(Int32 index)
        {
            if (index < 0 || index >= _viewmodel.ToibanCheckedList.Count)
                return;

            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            UpdateToibanCheckedList(helper.RemoveAt(index), index);
        }

        /// <summary>
        /// 出力リストを全削除する。
        /// </summary>
        public async Task ClearToibanCheckedListAsync()
        {
            var mode = await _repositories.UserConfigRepository.GetToibanCheckedListClearModeAsync();

            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            var removed =
                  mode == ToibanCheckedListClearMode.All ? helper.RemoveAll()
                : mode == ToibanCheckedListClearMode.CheckedOnly ? helper.RemoveCheckedOnly()
                : mode == ToibanCheckedListClearMode.UncheckedOnly ? helper.RemoveUncheckedOnly()
                : throw new InvalidOperationException();

            UpdateToibanCheckedList(removed, _viewmodel.Toiban);
        }

        /// <summary>
        /// 出力リストの指定したインデックスの問番を問番テキストボックスにセットする。
        /// </summary>
        public void SetToibanToToibanTextBoxFromToibanCheckedListAt(Int32 index)
        {
            if (index < 0 || index >= _viewmodel.ToibanCheckedList.Count)
                return;

            _viewmodel.Toiban = _viewmodel.ToibanCheckedList[index].Item2;
        }

        /// <summary>
        /// 出力リストの指定したインデックスの問番のチェックを変更する。
        /// </summary>
        public void SetCheckStateToToibanCheckedList(Int32 index, CheckState checkState)
        {
            if (index < 0 || index >= _viewmodel.ToibanCheckedList.Count)
                return;

            var check = checkState == CheckState.Checked;
            var (currentState, _) = _viewmodel.ToibanCheckedList[index];
            // 変更後のチェック状態が現在と同じなら何もしない
            // この処理を入れないと無限ループになってしまう
            // なぜなら、CheckedListの状態をtrueにしてviewmodel.ToibanCheckedListを更新するとそれだけで、
            // チェックの状態を変更したイベントリスナが呼び出されてしまうため、無限にこのメソッドが呼び出され続けてしまう
            if (check == currentState)
                return;

            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);

            UpdateToibanCheckedList(helper.SetCheckAt(index, check), index);
        }

        /// <summary>
        /// 出力リストと件数ラベルと選択状態のインデックスを更新する。
        /// </summary>
        private void UpdateToibanCheckedList(ToibanCheckedListHelper checkedList, Toiban indexedToiban)
        {
            var index = -1;
            if (indexedToiban != Toiban.Empty)
            {
                index = checkedList.IndexOf(indexedToiban);
            }

            UpdateToibanCheckedList(checkedList, index);
        }

        private void UpdateToibanCheckedList(ToibanCheckedListHelper checkedList, int selectedIndex)
        {
            _viewmodel.ToibanCheckedList = checkedList.ToRawDataList();
            _viewmodel.CheckedToibanCount = _viewmodel.ToibanCheckedList.Count(item => item.Item1).ToString("00");

            if (selectedIndex < 0)
            {
                _viewmodel.ToibanCheckedListSelectedIndex = -1;
                return;
            }

            if (selectedIndex >= 0)
            {
                var count = _viewmodel.ToibanCheckedList.Count;
                if (selectedIndex >= count)
                    selectedIndex = count - 1;

                _viewmodel.ToibanCheckedListSelectedIndex = selectedIndex;
            }
        }
    }
}
