using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UseCases.Interfaces;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    /// <summary>
    /// MainFormControllerが依存するユースケースクラス群。
    /// </summary>
    sealed class UseCasesThatMainFormControllerDependsOn
    {
        public IOpenNaireUseCase OpenNaire { get; set; }
        public IOpenHensyuUseCase OpenHensyu { get; set; }
        public IOpenInformationUseCase OpenInformation { get; set; }
        public IPrintKouseishiUseCase PrintKouseishi { get; set; }
        public IStartScreenSaverStopperUseCase StartScreenSaverStopper { get; set; }
        public IStopScreenSaverStopperUseCase StopScreenSaverStopper { get; set; }


        public UseCasesThatMainFormControllerDependsOn(
            IOpenNaireUseCase openNaireUseCase,
            IOpenHensyuUseCase openHensyuUseCase,
            IOpenInformationUseCase openInformationUseCase,
            IPrintKouseishiUseCase printKouseishiUseCase,
            IStartScreenSaverStopperUseCase startScreenSaverStopperUseCase,
            IStopScreenSaverStopperUseCase stopScreenSaverStopperUseCase)
        {
            Assert.IsNull(openNaireUseCase, nameof(openNaireUseCase));
            Assert.IsNull(openHensyuUseCase, nameof(openHensyuUseCase));
            Assert.IsNull(openInformationUseCase, nameof(openInformationUseCase));
            Assert.IsNull(printKouseishiUseCase, nameof(printKouseishiUseCase));
            Assert.IsNull(startScreenSaverStopperUseCase, nameof(startScreenSaverStopperUseCase));
            Assert.IsNull(stopScreenSaverStopperUseCase, nameof(stopScreenSaverStopperUseCase));

            OpenNaire = openNaireUseCase;
            OpenHensyu = openHensyuUseCase;
            OpenInformation = openInformationUseCase;
            PrintKouseishi = printKouseishiUseCase;
            StartScreenSaverStopper = startScreenSaverStopperUseCase;
            StopScreenSaverStopper = stopScreenSaverStopperUseCase;
        }
    }
}
