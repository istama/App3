using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.NengaApps;
using IsTama.NengaBooster.UseCases.Repositories;

namespace IsTama.NengaBooster.UI.Gateways
{
    interface IUserConfigRepositoryExtended : IUserConfigRepository
    {
        Task SetUserAccountAsync(UserAccount userAccount);

        Task<ToibanSelectMode> GetToibanSelectModeAsync();
        Task SetToibanSelectModeAsync(ToibanSelectMode mode);
        
        Task SetNaireOpenModeAsync(NaireOpenMode mode);
        Task SetHensyuOpenModeAsync(HensyuOpenMode mode);
        Task SetInformationOpenModeAsync(InformationOpenMode mode);
        Task SetWhetherToAddToibanToCheckedListWhenInformationSearchAsync(bool shouldAdd);
        Task SetWhetherToUncheckToibanFromCheckedListWhenEnterToKouseishiAsync(bool shouldUncheck);
        
        Task<ToibanCheckedListClearMode> GetToibanCheckedListClearModeAsync();
        Task SetToibanCheckedListClearModeAsync(ToibanCheckedListClearMode mode);
    }
}
