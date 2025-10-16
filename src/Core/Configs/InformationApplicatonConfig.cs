using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.Configs
{
    sealed class InformationApplicationConfig
    {
        public ApplicationBasicConfig Basic { get; set; }
        
        public string DialogTitlePattern { get; set; }
        public int DialogWidth { get; set; }
        public List<string> Texts_Dialog_ErrorMessage { get; set; }
        
        public Point TextBoxPoint_Toiban { get; set; }
        public string ButtonName_Open { get; set; }
        public string ButtonName_Close { get; set; }
        public string ButtonName_Detail { get; set; }
    }
}
