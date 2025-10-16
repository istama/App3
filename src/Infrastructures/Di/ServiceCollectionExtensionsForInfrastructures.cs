using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.NengaBooster.Infrastructures.Configs;
using IsTama.NengaBooster.Infrastructures.IO;
using IsTama.NengaBooster.Infrastructures.RPA;
using IsTama.Utils;
using IsTama.Utils.DependencyInjectionSimple;

namespace IsTama.NengaBooster.Infrastructures.Di
{
    static class ServiceCollectionExtensionsForInfrastructures
    {
        public static void AddInfrastructures(this IServiceCollection services)
        {
            services.AddConfigs();
            services.AddIO();
            services.AddRPA();
        }

        private static void AddConfigs(this IServiceCollection services)
        {
            services.AddSingleton<INengaBoosterConfigDTOReader>(provider =>
            {
                var reader = provider.GetRequiredKeyedService<JsonReader>("NengaBoosterConfigJsonReader");
                return ActivatorUtilities.CreateInstance<NengaBoosterConfigDTOReader>(provider, reader);
            });
            services.AddSingleton<INengaBoosterConfigDTOWriter>(provider =>
            {
                var writer = provider.GetRequiredKeyedService<JsonWriter>("NengaBoosterConfigJsonWriter");
                return ActivatorUtilities.CreateInstance<NengaBoosterConfigDTOWriter>(provider, writer);
            });

            services.AddSingleton<IUserConfigIOFactory, UserConfigIOFactory>();

        }

        private static void AddIO(this IServiceCollection services)
        {
            services.AddSingleton<IDeserializer, JsonDeserializer>();
            services.AddSingleton<ISerializer, JsonSerializer>();

            services.AddKeyedSingleton("NengaBoosterConfigFileReader", provider =>
            {
                return ActivatorUtilities.CreateInstance<FileReader>(provider, "nenga_booster_config.json", Encoding.UTF8);
            });
            services.AddKeyedSingleton("NengaBoosterConfigJsonReader", provider =>
            {
                var reader = provider.GetRequiredKeyedService<FileReader>("NengaBoosterConfigFileReader");
                return ActivatorUtilities.CreateInstance<JsonReader>(provider, reader);
            });

            services.AddKeyedSingleton("NengaBoosterConfigFileWriter", provider =>
            {
                return ActivatorUtilities.CreateInstance<FileWriter>(provider, "nenga_booster_config.json", Encoding.UTF8);
            });
            services.AddKeyedSingleton("NengaBoosterConfigJsonWriter", provider =>
            {
                var reader = provider.GetRequiredKeyedService<FileWriter>("NengaBoosterConfigFileWriter");
                return ActivatorUtilities.CreateInstance<JsonWriter>(provider, reader);
            });
        }

        private static void AddRPA(this IServiceCollection services)
        {
            services.AddSingleton<WindowPool>();
            services.AddSingleton<IKeyboard, Keyboard_>();
            services.AddSingleton<INativeWindowFactory, NativeWindowFactory>();
        }
    }
}
