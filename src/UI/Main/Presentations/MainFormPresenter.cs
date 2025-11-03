using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.Presenters;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    sealed partial class MainFormPresenter : IMainFormPresenter
    {
        private readonly MainFormViewModel _viewmodel;


        public MainFormPresenter(MainFormViewModel viewmodel)
        {
            Assert.IsNull(viewmodel, nameof(viewmodel));

            _viewmodel = viewmodel;
        }


        public void AddToibanToCheckedList(Toiban toiban)
        {
            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            UpdateToibanCheckedList(helper.AppendIfNothing(_viewmodel.Toiban), toiban);
        }

        public void AddToibanToCheckedList(Toiban toiban, bool check)
        {
            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            UpdateToibanCheckedList(helper.AppendOrUpdate(_viewmodel.Toiban, check), toiban);
        }

        public void UncheckToibanFromCheckedListAt(Toiban toiban)
        {
            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            UpdateToibanCheckedList(helper.SetCheckTo(toiban, false), toiban);
        }

        /// <summary>
        /// 出力リストと件数ラベルを更新する。
        /// </summary>
        private void UpdateToibanCheckedList(ToibanCheckedListHelper checkedList, Toiban indexedToiban)
        {
            _viewmodel.ToibanCheckedList = checkedList.ToRawDataList();
            _viewmodel.CheckedToibanCount = _viewmodel.ToibanCheckedList.Count(item => item.Item1).ToString("00");

            var index = checkedList.IndexOf(indexedToiban);
            if (index >= 0)
            {
                _viewmodel.ToibanCheckedListSelectedIndex = index;
            }
        }
    }
}
