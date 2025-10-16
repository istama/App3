using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.NengaApps
{
    sealed class UserAccount
    {
        public String UserName { get; set; }
        public String Password { get; set; }

        public UserAccount(string username, string password)
        {
            UserName = username ?? string.Empty;
            Password = password ?? string.Empty;
        }

        /// <summary>
        /// ユーザー情報が保持されているならtrueを返す。
        /// </summary>
        public bool IsFilled()
        {
            return !String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password);
        }
    }
}
