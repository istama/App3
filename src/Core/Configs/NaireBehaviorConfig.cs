using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.Configs
{
    /// <summary>
    /// 注文名入れアプリケーションへのRPAの動作設定。
    /// </summary>
    sealed class NaireBehaviorConfig
    {
        public int WaitTime_DialogOpen { get; set; }
        public int WaitTime_DialogOpenOnSaikumi { get; set; }
        public int WaitTime_SaikumiAlertDialogOpen { get; set; }

        public List<string> Texts_Dialog_WorkProcessNames { get; set; }
    }
}
