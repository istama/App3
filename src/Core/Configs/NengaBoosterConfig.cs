using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.Configs
{
    sealed class NengaBoosterConfig
    {
        public NengaMenuConfig NengaMenu { get; set; }

        public NaireApplicationConfig NaireApplication { get; set; }
        public NaireBehaviorConfig NaireBehavior { get; set; }

        public HensyuApplicationConfig HensyuApplication { get; set; }
        public HensyuBehaviorConfig HensyuBehavior { get; set; }

        public InformationApplicationConfig InformationApplication { get; set; }
        public InformationDetailApplicationConfig InformationDetailApplication { get; set; }

        public KouseishiApplicationConfig KouseishiApplication { get; set; }
        public KouseishiBehaviorConfig KouseishiBehavior { get; set; }

        public LoginFormConfig LoginForm { get; set; }
        public DialogConfig Dialog { get; set; }
        public OtherConfig Other { get; set; }
    }
}
