using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.NengaApps;

namespace IsTama.NengaBooster.UseCases.Repositories
{
    /// <summary>
    /// ユーザー設定のリポジトリ。
    /// </summary>
    interface IUserConfigRepository
    {
        Task<UserAccount> GetUserAccountAsync();
        Task<string> GetKeyReplacerSettingsFilepathAsync();

        Task<NaireOpenMode> GetNaireOpenModeAsync();
        Task<HensyuOpenMode> GetHensyuOpenModeAsync();
        Task<InformationOpenMode> GetInformationOpenModeAsync();
        Task<bool> ShouldAddToibanToCheckedListWhenInformationSearchAsync();
        Task<bool> ShouldUncheckToibanFromCheckedListWhenEnterToKouseishiAsync();

        Task<int> GetToibanCheckedListCharSize();
    }
}
