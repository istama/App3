using System;
using System.Collections.Generic;
using System.Drawing;
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
            var form = new WarningForm(errorMessage);
            var screen_size = Screen.GetBounds(form);
            form.StartPosition = FormStartPosition.Manual;
            form.DesktopLocation = new Point(screen_size.Width - form.Width, 120);
            form.ShowDialog();
            //MessageBox.Show(
            //    Owner,
            //    errorMessage,
            //    "NengaBooster.exe",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Warning);
        }
    }
}
