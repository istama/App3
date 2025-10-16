using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    struct ModifierKeysPressingState
    {
        public bool IsLCtrlPressing { get; set; }
        public bool IsLShiftPressing { get; set; }
        public bool IsLAltPressing { get; set; }

        public bool IsRCtrlPressing { get; set; }
        public bool IsRShiftPressing { get; set; }
        public bool IsRAltPressing { get; set; }
    }
}

