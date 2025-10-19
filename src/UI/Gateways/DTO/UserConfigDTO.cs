using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// ユーザー設定ファイルの.jsonに対応したDTOクラス。
    /// </summary>
    sealed class UserConfigDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string KeyReplaceSettingsFilePath { get; set; } = string.Empty;

        public bool SelectToibanByClickChecked { get; set; } = true;
        public bool SelectToibanByWClickChecked { get; set; } = false;

        public bool OpenHensyuMenuOnlyChecked { get; set; } = false;
        public bool OpenHensyuTegumiWindowChecked { get; set; } = true;

        public bool OpenInformationSearchFormOnlyChecked { get; set; } = true;
        public bool OpenInformationDetailWindowChecked { get; set; } = false;
        public bool OpenInformationKouseiPageChecked { get; set; } = false;
        public bool OpenInformationKumihanPageChecked { get; set; } = false;
        public bool ShouldAddToibanToCheckedListWhenInformationOpenChecked { get; set; } = false;

        public bool ShouldUncheckToibanFromCheckedListChecked { get; set; } = false;

        public bool RemoveToibanAllChecked { get; set; } = false;
        public bool RemoveToibanCheckedChecked { get; set; } = true;
        public bool RemoveToibanUncheckedChecked { get; set; } = false;

        public int CheckedToibanListCharSize { get; set; } = 9;
    }
}
