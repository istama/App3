using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UseCases.Interfaces;
using IsTama.NengaBooster.UseCases.NengaApps;
using IsTama.NengaBooster.UseCases.SSS;
using IsTama.Utils.DependencyInjectionSimple;

namespace IsTama.NengaBooster.UseCases.Di
{
    static class ServiceCollectionExtensionsForUseCases
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddNengaApps();
            services.AddSSS();
        }

        private static void AddNengaApps(this IServiceCollection services)
        {
            services.AddSingleton<LoginService>();
            services.AddSingleton<IOpenNaireUseCase, OpenNaireUseCase>();
            services.AddSingleton<IOpenHensyuUseCase, OpenHensyuUseCase>();
            services.AddSingleton<IOpenInformationUseCase, OpenInformationUseCase>();
            services.AddSingleton<IPrintKouseishiUseCase, PrintKouseishiUseCase>();
        }

        private static void AddSSS(this IServiceCollection services)
        {
            services.AddSingleton<ScreenSaverStopper>();
            services.AddSingleton<IStartScreenSaverStopperUseCase, StartScreenSaverStopperUseCase>();
            services.AddSingleton<IStopScreenSaverStopperUseCase, StopScreenSaverStopperUseCase>();
        }
    }
}
