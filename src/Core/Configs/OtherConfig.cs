using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.Configs
{
    sealed class OtherConfig
    {
        public string Path_UserSettingsFile { get; set; }
        public string Path_KeyReplacerExeFile { get; set; }
        public List<(DateTime, DateTime)> Texts_SSSExecutionPeriods { get; set; }
    }
}
