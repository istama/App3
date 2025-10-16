using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.Interfaces;

namespace IsTama.NengaBooster.UseCases.Interfaces
{
    interface IPrintKouseishiUseCase
    {
        Task ExecuteAsync(Toiban toiban);
        Task ExecuteAsync(List<Toiban> toibanList);
    }
}
