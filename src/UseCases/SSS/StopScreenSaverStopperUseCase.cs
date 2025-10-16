using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UseCases.Interfaces;
using IsTama.Utils;

namespace IsTama.NengaBooster.UseCases.SSS
{
    /// <summary>
    /// SSSを停止するユースケース。
    /// </summary>
    sealed class StopScreenSaverStopperUseCase : IStopScreenSaverStopperUseCase
    {
        private readonly ScreenSaverStopper _screenSaverStopper;


        public StopScreenSaverStopperUseCase(ScreenSaverStopper screenSaverStopper)
        {
            Assert.IsNull(screenSaverStopper, nameof(screenSaverStopper));

            _screenSaverStopper = screenSaverStopper;
        }


        public void Execute()
        {
            _screenSaverStopper.Off();
        }
    }
}
