using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UseCases.Interfaces;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.Utils;

namespace IsTama.NengaBooster.UseCases.SSS
{
    /// <summary>
    /// SSSを起動するユースケース。
    /// </summary>
    sealed class StartScreenSaverStopperUseCase : IStartScreenSaverStopperUseCase
    {
        private readonly ScreenSaverStopper _screenSaverStopper;
        private readonly IOtherConfigRepository _repository;


        public StartScreenSaverStopperUseCase(ScreenSaverStopper screenSaverStopper, IOtherConfigRepository repository)
        {
            Assert.IsNull(screenSaverStopper, nameof(screenSaverStopper));
            Assert.IsNull(repository, nameof(repository));

            _screenSaverStopper = screenSaverStopper;
            _repository = repository;
        }


        public async Task ExecuteAsync()
        {
            var periods = await _repository.GetScreenSaverStopperPeriodsAsync().ConfigureAwait(false);
            await _screenSaverStopper.OnAsync(periods).ConfigureAwait(false);
        }
    }
}
