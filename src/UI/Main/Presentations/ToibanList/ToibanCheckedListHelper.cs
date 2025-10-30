using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    /// <summary>
    /// 出力リストの操作を助けるクラス。不変クラス。
    /// </summary>
    sealed class ToibanCheckedListHelper
    {
        public static readonly ToibanCheckedListHelper Empty = new ToibanCheckedListHelper();

        public static ToibanCheckedListHelper Create(IEnumerable<(bool, string)> checkAndToibanList)
        {
            Assert.IsNull(checkAndToibanList, nameof(checkAndToibanList));

            var items = checkAndToibanList
                .Where(item => Toiban.TryCreate(item.Item2, out var _))
                .Select(item => new ToibanCheckedListItem(item.Item1, item.Item2));

            return new ToibanCheckedListHelper(items);
        }


        private readonly IEnumerable<ToibanCheckedListItem> _toibanList;


        private ToibanCheckedListHelper()
        {
            _toibanList = new List<ToibanCheckedListItem>();
        }
        private ToibanCheckedListHelper(IEnumerable<ToibanCheckedListItem> items)
        {
            _toibanList = items;
        }


        /// <summary>
        /// 引数の問番がリストに存在しなければリストに追加する。
        /// </summary>
        public ToibanCheckedListHelper AppendIfNothing(Toiban toiban)
        {
            if (toiban == Toiban.Empty)
                return this;

            if (_toibanList.Any(item => item.Toiban == toiban))
                return this;

            var newItem = new ToibanCheckedListItem(true, toiban);

            return new ToibanCheckedListHelper(_toibanList.Append(newItem));
        }

        /// <summary>
        /// 引数の問番がリストに存在しなければリストに追加する。
        /// 存在するならチェック状態のみ引数の値に変更する。
        /// </summary>
        public ToibanCheckedListHelper AppendOrUpdate(Toiban toiban, bool check)
        {
            if (toiban == Toiban.Empty)
                return this;

            if (_toibanList.Any(v => v.Toiban == toiban))
            {
                var newList = _toibanList.Select(item =>
                {
                    if (item.Toiban == toiban)
                        return new ToibanCheckedListItem(check, toiban);
                    else
                        return item;
                });

                return new ToibanCheckedListHelper(newList);
            }
            else
            {
                var newItem = new ToibanCheckedListItem(toiban, check);

                return new ToibanCheckedListHelper(_toibanList.Append(newItem));
            }
        }

        /// <summary>
        /// 引数のインデックスの要素を削除する。
        /// </summary>
        public ToibanCheckedListHelper RemoveAt(Int32 idx)
        {
            if (idx < 0)
                return this;

            IEnumerable<ToibanCheckedListItem> Remove(Int32 idx_, IEnumerable<ToibanCheckedListItem> items)
            {
                var i = 0;
                foreach (var item in items)
                {
                    if (i != idx_)
                        yield return item;

                    i++;
                }
            }

            return new ToibanCheckedListHelper(Remove(idx, _toibanList));
        }

        /// <summary>
        /// リストの要素をすべて削除する。
        /// </summary>
        public ToibanCheckedListHelper RemoveAll()
            => Empty;

        /// <summary>
        /// チェックされているリストの要素をすべて削除する。
        /// </summary>
        public ToibanCheckedListHelper RemoveCheckedOnly()
        {
            return new ToibanCheckedListHelper(_toibanList.Where(item => !item.Checked));
        }

        /// <summary>
        /// チェックされていないリストの要素をすべて削除する。
        /// </summary>
        public ToibanCheckedListHelper RemoveUncheckedOnly()
        {
            return new ToibanCheckedListHelper(_toibanList.Where(item => item.Checked));
        }

        /// <summary>
        /// 引数のインデックスの要素の順番を１つ上げる。
        /// </summary>
        public ToibanCheckedListHelper RaiseAt(Int32 idx)
        {
            if (idx <= 0)
                return this;

            IEnumerable<ToibanCheckedListItem> SwapWithPrev(Int32 idx_, IEnumerable<ToibanCheckedListItem> items)
            {
                ToibanCheckedListItem prev = default;
                var i = 0;
                foreach (var item in items)
                {
                    if (i == idx_ - 1)
                    {
                        prev = item;
                    }
                    else if (i == idx_)
                    {
                        yield return item;
                        yield return prev;
                    }
                    else
                    {
                        yield return item;
                    }
                    i++;
                }
            }

            return new ToibanCheckedListHelper(SwapWithPrev(idx, _toibanList));
        }

        /// <summary>
        /// 引数のインデックスの要素の順番を１つ下げる。
        /// </summary>
        public ToibanCheckedListHelper LowerAt(Int32 idx)
        {
            if (idx < 0)
                return this;

            IEnumerable<ToibanCheckedListItem> SwapWithBack(Int32 idx_, IEnumerable<ToibanCheckedListItem> items)
            {
                ToibanCheckedListItem prev = default;
                var i = 0;
                foreach (var item in items)
                {
                    if (i == idx_)
                    {
                        prev = item;
                    }
                    else if (i == idx_ + 1)
                    {
                        yield return item;
                        yield return prev;
                    }
                    else
                    {
                        yield return item;
                    }
                    i++;
                }
            }

            return new ToibanCheckedListHelper(SwapWithBack(idx, _toibanList));
        }

        /// <summary>
        /// 引数の問番のチェックの状態をセットする。
        /// </summary>
        public ToibanCheckedListHelper SetCheckTo(Toiban toiban, bool check)
        {
            var index = 0;
            foreach (var item in _toibanList)
            {
                if (toiban == item.Toiban)
                {
                    return SetCheckAt(index, check);
                }
                index += 1;
            }
            return this;
        }

        /// <summary>
        /// 引数のインデックスの要素のチェックの状態をセットする。
        /// </summary>
        public ToibanCheckedListHelper SetCheckAt(Int32 idx, bool check)
        {
            if (idx < 0)
                return this;

            IEnumerable<ToibanCheckedListItem> SetCheck(Int32 idx_, bool check_, IEnumerable<ToibanCheckedListItem> items)
            {
                var i = 0;
                foreach (var item in items)
                {
                    if (i == idx_)
                    {
                        yield return new ToibanCheckedListItem(item.Toiban, check_);
                    }
                    else
                    {
                        yield return item;
                    }
                    i++;
                }
            }

            return new ToibanCheckedListHelper(SetCheck(idx, check, _toibanList));
        }

        /// <summary>
        /// 引数のインデックスの要素のチェックの状態を反転する。
        /// </summary>
        public ToibanCheckedListHelper ReverseCheckAt(Int32 idx)
        {
            if (idx < 0)
                return this;

            IEnumerable<ToibanCheckedListItem> ReverseCheck(Int32 idx_, IEnumerable<ToibanCheckedListItem> items)
            {
                var i = 0;
                foreach (var item in items)
                {
                    if (i == idx_)
                    {
                        yield return new ToibanCheckedListItem(item.Toiban, !item.Checked);
                    }
                    else
                    {
                        yield return item;
                    }
                    i++;
                }
            }

            return new ToibanCheckedListHelper(ReverseCheck(idx, _toibanList));
        }

        /// <summary>
        /// すべての要素のチェックの状態を反転させる。
        /// </summary>
        public ToibanCheckedListHelper ReverseCheckAll()
        {
            return new ToibanCheckedListHelper(_toibanList.Select(item =>
                new ToibanCheckedListItem(item.Toiban, !item.Checked)));
        }

        /// <summary>
        /// 生データのリストに変換する。
        /// </summary>
        public List<(bool, string)> ToRawDataList()
        {
            return _toibanList.Select(item => (item.Checked, item.Toiban.Text)).ToList();
        }

        /// <summary>
        /// チェックが入った要素の問番リストに変換する。
        /// </summary>
        public List<Toiban> ToCheckedToibanList()
        {
            return _toibanList.Where(item => item.Checked).Select(item => item.Toiban).ToList();
        }
    }
}
