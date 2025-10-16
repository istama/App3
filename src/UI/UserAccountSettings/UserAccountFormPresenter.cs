using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.UserAccountSettings.Presentations
{
    sealed class UserAccountFormPresenter
    {
        private readonly UserAccount _user;

        public String UserName
        {
            get => _user.UserName;
            set => _user.UserName = value;
        }

        public String Password
        {
            get => _user.Password;
            set => _user.Password = value;
        }

        public UserAccountFormPresenter(UserAccount user)
        {
            Assert.IsNull(user, nameof(user));

            _user = user;
        }
    }
}
