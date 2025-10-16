using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.Utils.DependencyInjectionSimple;

namespace IsTama.NengaBooster.Core.Di
{
    static class ServiceCollectionExtensionsForCore
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddNengaApps();
        }

        private static void AddNengaApps(this IServiceCollection services)
        {
            services.AddSingleton<LoginRPAService>();
            services.AddSingleton<NaireRPAService>();
            services.AddSingleton<HensyuRPAService>();
            services.AddSingleton<InformationRPAService>();
            services.AddSingleton<KouseishiRPAService>();
            services.AddSingleton<NengaAppWindowFactory>();
        }
    }
}
