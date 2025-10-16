using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.Configs
{
    /// <summary>
    /// 各年賀アプリの共通設定。
    /// </summary>
    sealed class ApplicationBasicConfig
    {
        public string ProcessName { get; set; }

        public string ApplicationName_OnNengaMenu { get; set; }

        public string WindowTitlePattern { get; set; }

        public int WindowWidth { get; set; }
    }
}
