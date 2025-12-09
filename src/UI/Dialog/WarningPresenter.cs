using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.UseCases.Presenters;

namespace IsTama.NengaBooster.UI.Dialog
{
    class WarningPresenter : IWarningPresenter
    {
        public Form Owner { get; set; } = null;
        

        public WarningPresenter()
        {
        }


        public void ShowAlert(string errorMessage)
        {
            MessageBox.Show(
                Owner,
                errorMessage,
                "NengaBooster.exe",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
