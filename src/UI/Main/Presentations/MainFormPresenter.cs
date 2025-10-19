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

        public void SetToiban(Toiban toiban)
        {
            _viewmodel.Toiban = toiban.Text;
        }

        public void SetUserName(String username)
        {
            _viewmodel.Toiban = username;
        }

        public void SetButton1Name(String name)
        {
            _viewmodel.Button1Name = name;
        }

        public void SetButton2Name(String name)
        {
            _viewmodel.Button2Name = name;
        }

        public void SetButton3Name(String name)
        {
            _viewmodel.Button3Name = name;
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

        //public void SetToibanCheckedList(ToibanCheckedList outputToibanList)
        //{
        //    _viewmodel.ToibanCheckedList = outputToibanList
        //        .Select(item => (item.Checked, item.Toiban.Text))
        //        .ToList();
        //
        //    _viewmodel.OutputToibanCount = _viewmodel.ToibanCheckedList
        //        .Count(item => item.Item1)
        //        .ToString("00");
        //}

        //public ToibanCheckedList GetToibanCheckedList()
        //{
        //    return _outputToibanListCreater.Create(_viewmodel.ToibanCheckedList);
        //}

        public void SetFormColor(Color color)
        {
            _viewmodel.FormColor = color;
        }

        public void SetMenuBarColor(Color color)
        {
            _viewmodel.MenuBarColor = color;
        }

        public void SetButtonColor(Color color)
        {
            _viewmodel.ButtonColor = color;
        }

        public void SetLabelLeftCtrlSignalColor(Color color)
        {
            _viewmodel.LabelLeftCtrlSignalColor = color;
        }
        public void SetLabelLeftShiftSignalColor(Color color)
        {
            _viewmodel.LabelLeftShiftSignalColor = color;
        }
        public void SetLabelLeftAltSignalColor(Color color)
        {
            _viewmodel.LabelLeftAltSignalColor = color;
        }
        public void SetLabelRightCtrlSignalColor(Color color)
        {
            _viewmodel.LabelRightCtrlSignalColor = color;
        }
        public void SetLabelRightShiftSignalColor(Color color)
        {
            _viewmodel.LabelRightShiftSignalColor = color;
        }
        public void SetLabelRightAltSignalColor(Color color)
        {
            _viewmodel.LabelRightAltSignalColor = color;
        }
    }
}
