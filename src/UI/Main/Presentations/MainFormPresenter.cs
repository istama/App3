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
            _viewmodel.ToibanCheckedList = helper.AppendIfNothing(_viewmodel.Toiban).ToRawDataList();
        }

        public void UncheckToibanFromCheckedListAt(int index)
        {
            var helper = ToibanCheckedListHelper.Create(_viewmodel.ToibanCheckedList);
            _viewmodel.ToibanCheckedList = helper.SetCheckAt(index, false).ToRawDataList();
        }

    }
}
