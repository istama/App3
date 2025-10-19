using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.UserConfigSettings.Presentations
{
    sealed class UserConfigFormViewModel : ViewModelBase
    {
        public String UserName
        {
            get => base.GetProperty<String>();
            set => base.SetProperty<String>(value);
        }

        public String Password
        {
            get => base.GetProperty<String>();
            set => base.SetProperty<String>(value);
        }

        public String KeyReplaceSettingsFilePath
        {
            get => base.GetProperty<String>();
            set => base.SetProperty<String>(value);
        }

        public Boolean SelectToibanByClickChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean SelectToibanByWClickChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean OpenHensyuMenuOnlyChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean OpenHensyuTegumiWindowChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean OpenInformationSearchFormOnlyChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean OpenInformationDetailWindowChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean OpenInformationKouseiPageChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean OpenInformationKumihanPageChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean ShouldAddToibanToCheckedListWhenInformationOpenChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean ShouldUncheckToibanFromCheckedListChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean RemoveToibanAllChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean RemoveToibanCheckedChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public Boolean RemoveToibanUncheckedChecked
        {
            get => base.GetProperty<Boolean>();
            set => base.SetProperty<Boolean>(value);
        }

        public int CheckedToibanListCharSize
        {
            get => base.GetProperty<int>();
            set => base.SetProperty<int>(value);
        }
    }
}
