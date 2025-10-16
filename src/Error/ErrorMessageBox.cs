using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.NengaBooster.Error
{
    class ErrorMessageBox
    {
        public static void Show(string message)
        {
            MessageBox.Show(message, "NengaBooster.exe - エラー");
        }
    }
}
