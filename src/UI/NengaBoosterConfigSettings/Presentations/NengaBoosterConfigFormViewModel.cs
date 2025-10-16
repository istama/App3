using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.NengaBoosterConfigSettings.Presentations
{
    sealed class NengaBoosterConfigFormViewModel
    {
        public NengaBoosterConfigFormViewModelForApplicationSettings ApplicationSettings { get;  set; }
        public NengaBoosterConfigFormViewModelForBehaviorSettings BehaviorSettings { get; set; }

        public NengaBoosterConfigFormViewModel(
            NengaBoosterConfigFormViewModelForApplicationSettings applicationSettings,
            NengaBoosterConfigFormViewModelForBehaviorSettings behaviorSettings)
        {
            Assert.IsNull(applicationSettings, nameof(applicationSettings));
            Assert.IsNull(behaviorSettings, nameof(behaviorSettings));

            ApplicationSettings = applicationSettings;
            BehaviorSettings = behaviorSettings;
        }
    }
}
