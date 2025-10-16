using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UI.UserAccountSettings.Presentations;

namespace IsTama.NengaBooster.UI.UserAccountSettings.View
{
    sealed class UserAccountFormExecutor : IUserAccountForm
    {
        public DialogResult ShowDialog(Form owner, UserAccount user)
        {
            var presenter = new UserAccountFormPresenter(user);
            var form = new UserAccountForm(presenter);
            form.ShowDialog(owner);

            return form.DialogResult;
        }
    }
}
