using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.Core.Configs
{
    class InformationDetailApplicationConfig
    {
        public ApplicationBasicConfig Basic { get; set; }
        
        public string ButtonName_KouseiPage { get; set; }
        public string ButtonName_KumihanPage { get; set; }
        public string ButtonName_Close { get; set; }
    }
}
