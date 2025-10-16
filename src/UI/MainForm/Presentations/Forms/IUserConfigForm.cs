using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    interface IUserConfigForm
    {
        DialogResult ShowDialog(Form owner, string filepath);
    }
}
