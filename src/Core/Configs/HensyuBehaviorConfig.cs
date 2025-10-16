using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.Configs
{
    sealed class HensyuBehaviorConfig
    {
        public int WaitTime_DialogOpen { get; set; }

        public List<string> Texts_Dialog_WorkProcessNames { get; set; }
    }
}
