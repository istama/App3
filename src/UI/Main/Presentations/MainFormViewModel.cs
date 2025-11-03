using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    sealed partial class MainFormViewModel : ViewModelBase
    {
        public String Toiban
        {
            get => base.GetProperty<String>();
            set => base.SetProperty<String>(value);
        }

        public String Button1Name
        {
            get => base.GetProperty<String>();
            set => base.SetProperty<String>(value);
        }

        public String Button2Name
        {
            get => base.GetProperty<String>();
            set => base.SetProperty<String>(value);
        }

        public String Button3Name
        {
            get => base.GetProperty<String>();
            set => base.SetProperty<String>(value);
        }

        public List<(Boolean, String)> ToibanCheckedList 
        {
            get => base.GetProperty<List<(Boolean, String)>>();
            set => base.SetProperty<List<(Boolean, String)>>(value);
        }

        public int ToibanCheckedListSelectedIndex
        {
            get => base.GetProperty<int>();
            set => base.SetProperty<int>(value, true);
        }

        public int ToibanCheckedListCharSize
        {
            get => base.GetProperty<int>();
            set => base.SetProperty<int>(value, true);
        }

        public String CheckedToibanCount
        {
            get => base.GetProperty<String>();
            set => base.SetProperty<String>(value);
        }

        public Color FormColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color ButtonColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color ListBoxColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color MenuBarColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color LabelLeftCtrlSignalColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color LabelLeftShiftSignalColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color LabelLeftAltSignalColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color LabelRightCtrlSignalColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color LabelRightShiftSignalColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color LabelRightAltSignalColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public Color LabelSignalTextColor
        {
            get => base.GetProperty<Color>();
            set => base.SetProperty<Color>(value);
        }

        public bool StartScreenSaverStopperButtonEnabled
        {
            get => base.GetProperty<bool>();
            set => base.SetProperty<bool>(value);
        }

        public bool StopScreenSaverStopperButtonEnabled
        {
            get => base.GetProperty<bool>();
            set => base.SetProperty<bool>(value);
        }
    }
}
