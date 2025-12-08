using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;

namespace IsTama.NengaBooster.UseCases.Interfaces
{
    interface IOpenNaireUseCase
    {
        Task ExecuteAsync(Toiban toiban, IEnumerable<Toiban> outputToibanList);
    }
}
