using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UseCases.SSS;

namespace IsTama.NengaBooster.UseCases.Repositories
{
    interface IOtherConfigRepository
    {
        Task<string> GetUserConfigFilepathAsync();
        Task<string> GetKeyReplacerSettingFilepathAsync();
        Task<ScreenSaverStopperPeriods> GetScreenSaverStopperPeriodsAsync();

        Task SetUserConfigFilepathAsync(string userConfigFilepath);
    }
}
