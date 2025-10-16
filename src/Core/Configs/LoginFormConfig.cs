using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.Configs
{
    /// <summary>
    /// ログインフォームとの連携設定。
    /// </summary>
    sealed class LoginFormConfig
    {
        public ApplicationBasicConfig Basic { get; set; }
        
        public int TextBoxIndex_UserName { get; set; }
        public int TextBoxIndex_Password { get; set; }
        public Point TextBoxPoint_UserName { get; set; }
        public Point TextBoxPoint_Password { get; set; }
        public String ButtonName_Ok { get; set; }
    }
}
