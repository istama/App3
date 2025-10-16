using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;

namespace IsTama.NengaBooster.UseCases.Repositories
{
    interface IBehaviorConfigRepository
    {
        Task<NaireBehaviorConfig> GetNaireBehaviorConfigAsync();
        Task<HensyuBehaviorConfig> GetHensyuBehaviorConfigAsync();
        Task<KouseishiBehaviorConfig> GetKouseishiBehaviorConfigAsync();
    }
}
