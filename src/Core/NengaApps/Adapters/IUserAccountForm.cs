using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsTama.NengaBooster.Core.NengaApps
{
    interface IUserAccountForm
    {
        DialogResult ShowDialog(Form owner, UserAccount user);
    }
}
