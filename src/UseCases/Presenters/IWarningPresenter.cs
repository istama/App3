using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.NengaBooster.UseCases.Presenters
{
    interface IWarningPresenter
    {
        Form Owner { get; set; }

        void ShowAlert(string errorMessage);
    }
}
