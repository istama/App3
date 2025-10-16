using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.UseCases.Presenters;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    sealed partial class MainFormPresenter : IMainFormPresenter
    {
        //public void SetToibanSelectMode(ToibanSelectMode mode)
        //{
        //    _viewmodel.SelectToibanByClick  = GetCheckStateCheckedIfSame(ToibanSelectMode.ByClick, mode);
        //    _viewmodel.SelectToibanByWClick = GetCheckStateCheckedIfSame(ToibanSelectMode.ByWClick, mode);
        //}
        //
        //public void CheckToNaireOpenMode(NaireOpenMode mode)
        //{
        //    _viewmodel.NaireOpenMode_Normal  = GetCheckStateCheckedIfSame(NaireOpenMode.Normal, mode);
        //    _viewmodel.NaireOpenMode_Saikumi = GetCheckStateCheckedIfSame(NaireOpenMode.Saikumi, mode);
        //}
        //
        //public void CheckToHensyuOpenMode(HensyuOpenMode mode)
        //{
        //    _viewmodel.HensyuOpenMode_Menu   = GetCheckStateCheckedIfSame(HensyuOpenMode.MenuWindow, mode);
        //    _viewmodel.HensyuOpenMode_Tegumi = GetCheckStateCheckedIfSame(HensyuOpenMode.TegumiWindow, mode);
        //}
        //
        //public void CheckToShouldAddToibanToCheckedList(Boolean should)
        //{
        //    _viewmodel.ShouldAddToibanToCheckedList = should ? CheckState.Checked: CheckState.Unchecked;
        //}
        //
        //public void CheckToInformationOpenMode(InformationOpenMode mode)
        //{
        //    _viewmodel.InformationOpenMode_SearchForm   = GetCheckStateCheckedIfSame(InformationOpenMode.SearchForm, mode);
        //    _viewmodel.InformationOpenMode_DetailWindow = GetCheckStateCheckedIfSame(InformationOpenMode.DetailWindow, mode);
        //    _viewmodel.InformationOpenMode_KouseiPage   = GetCheckStateCheckedIfSame(InformationOpenMode.KouseiPage, mode);
        //    _viewmodel.InformationOpenMode_KumihanPage  = GetCheckStateCheckedIfSame(InformationOpenMode.KumihanPage, mode);
        //}
        //
        //public void CheckToPostToibanCheckedListOutputMode(PostToibanCheckedListOutputMode mode)
        //{
        //    _viewmodel.ShouldUncheckToibanFromCheckedList = mode == PostToibanCheckedListOutputMode.Uncheck ? CheckState.Checked : CheckState.Unchecked;
        //}
        
        //public void SetToibanCheckedListClearMode(ToibanCheckedListClearMode mode)
        //{
        //    _viewmodel.RemoveToibanAll           = GetCheckStateCheckedIfSame(ToibanCheckedListClearMode.All, mode);
        //    _viewmodel.RemoveToibanCheckedOnly   = GetCheckStateCheckedIfSame(ToibanCheckedListClearMode.CheckedOnly, mode);
        //    _viewmodel.RemoveToibanUncheckedOnly = GetCheckStateCheckedIfSame(ToibanCheckedListClearMode.UncheckedOnly, mode);
        //}
        
        //private CheckState GetCheckStateCheckedIfSame(object a, object b)
        //{
        //    return a == b ? CheckState.Checked : CheckState.Unchecked;
        //}

        //public void CheckToNormalModeMenu()
        //{
        //    _viewmodel.NormalModeCheckState = CheckState.Checked;
        //    _viewmodel.SaikumiModeCheckState = CheckState.Unchecked;
        //}
        //
        //public void CheckToSaikumiModeMenu()
        //{
        //    _viewmodel.NormalModeCheckState = CheckState.Unchecked;
        //    _viewmodel.SaikumiModeCheckState = CheckState.Checked;
        //}
        //
        //public void SetOperationModeColors(OperationModeColors colors)
        //{
        //    _viewmodel.FormColor = colors.FormColor;
        //    _viewmodel.ButtonColor = colors.ButtonColor;
        //
        //    _viewmodel.ListBoxColor = colors.ListBoxColor;
        //    _viewmodel.MenuBarColor = colors.MenuBarColor;
        //}
    }
}
