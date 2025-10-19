using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.Utils;
using IsTama.NengaBooster.Core.NengaApps;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    /// <summary>
    /// MainFormControllerが依存するフォーム群。
    /// </summary>
    sealed class FormsThatMainFormControllerDependsOn
    {
        public IUserConfigForm UserConfigForm { get; set; }
        public IUserAccountForm UserAccountForm { get; set; }
        public INengaBoosterConfigForm NengaBoosterConfigForm { get; set; }

        public FormsThatMainFormControllerDependsOn(IUserConfigForm userConfigForm, IUserAccountForm userAccountForm, INengaBoosterConfigForm nengaBoosterConfigForm)
        {
            Assert.IsNull(userConfigForm, nameof(userConfigForm));
            Assert.IsNull(userAccountForm, nameof(userAccountForm));
            Assert.IsNull(nengaBoosterConfigForm, nameof(nengaBoosterConfigForm));

            UserConfigForm = userConfigForm;
            UserAccountForm = userAccountForm;
            NengaBoosterConfigForm = nengaBoosterConfigForm;
        }
    }
}
