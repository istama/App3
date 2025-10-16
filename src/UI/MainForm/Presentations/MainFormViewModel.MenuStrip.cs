using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    sealed partial class MainFormViewModel : ViewModelBase
    {
        public CheckState ToibanSelectMode_ByClick
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState ToibanSelectMode_ByWClick
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState NaireOpenMode_Normal
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState NaireOpenMode_Saikumi
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState HensyuOpenMode_Menu
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState HensyuOpenMode_Tegumi
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState InformationOpenMode_SearchForm
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState InformationOpenMode_DetailWindow
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState InformationOpenMode_KouseiPage
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState InformationOpenMode_KumihanPage
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState ShouldAddToibanToCheckedList
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState ShouldUncheckToibanFromCheckedList
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState ToibanCheckedListClearMode_All
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState ToibanCheckedListClearMode_CheckedOnly
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }

        public CheckState ToibanCheckedListClearMode_UncheckedOnly
        {
            get => base.GetProperty<CheckState>();
            set => base.SetProperty<CheckState>(value);
        }
    }
}
