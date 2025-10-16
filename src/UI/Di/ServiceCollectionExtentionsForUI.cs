using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.NengaBooster.UI.Main.Presentations;
using IsTama.NengaBooster.UI.Main.View;
using IsTama.NengaBooster.UI.NengaBoosterConfigSettings.Presentations;
using IsTama.NengaBooster.UI.NengaBoosterConfigSettings.View;
using IsTama.NengaBooster.UI.UserAccountSettings.View;
using IsTama.NengaBooster.UI.UserConfigSettings.Presentations;
using IsTama.NengaBooster.UI.UserConfigSettings.View;
using IsTama.NengaBooster.UseCases.Presenters;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.Utils.DependencyInjectionSimple;

namespace IsTama.NengaBooster.UI.Di
{
    static class ServiceCollectionExtentionsForUI
    {
        public static void AddUI(this IServiceCollection services)
        {
            services.AddGateways();
            services.AddMainForm();
            services.AddNengaBoosterConfigSettings();
            services.AddUserAccountSettings();
            services.AddUserConfigSettings();
        }

        private static void AddGateways(this IServiceCollection services)
        {
            services.AddSingleton<ConfigValueParser>();
            services.AddSingleton<DtoAndViewModelMapper>();
            services.AddSingleton<NengaBoosterConfigDtoMapper>();

            services.AddSingleton<IApplicationConfigRepository, ApplicationConfigRepository>();
            services.AddSingleton<IBehaviorConfigRepository, BehaviorConfigRepositiry>();

            services.AddSingleton<IOtherConfigRepository, OtherConfigRepository>();
            services.AddSingleton<UserConfigRepository>();
            services.AddSingleton<IUserConfigRepository>(provider =>
            {
                return provider.GetRequiredService<UserConfigRepository>();
            });
            services.AddSingleton<IUserConfigRepositoryExtended>(provider =>
            {
                return provider.GetRequiredService<UserConfigRepository>();
            });

        }

        private static void AddMainForm(this IServiceCollection services)
        {
            services.AddSingleton<MainFormViewModel>();
            services.AddSingleton<IMainFormPresenter, MainFormPresenter>();

            services.AddSingleton<RepositoriesThatMainFormControllerDependsOn>();
            services.AddSingleton<UseCasesThatMainFormControllerDependsOn>();
            services.AddSingleton<FormsThatMainFormControllerDependsOn>();

            services.AddSingleton<ModifierKeysStateNotification>();

            services.AddSingleton<MainFormController>();

            services.AddSingleton<MainForm>();
        }

        private static void AddNengaBoosterConfigSettings(this IServiceCollection services)
        {
            services.AddSingleton<NengaBoosterConfigDtoAndViewModelMapper>();


            services.AddSingleton<NengaBoosterConfigFormViewModelForApplicationSettings>();
            services.AddSingleton<NengaBoosterConfigFormViewModelForBehaviorSettings>();
            services.AddSingleton<NengaBoosterConfigFormViewModel>();
            services.AddSingleton<NengaBoosterConfigFormPresenter>();

            services.AddSingleton<INengaBoosterConfigForm, NengaBoosterConfigFormExecutor>();
            //services.AddSingleton<NengaBoosterConfigForm>();
        }

        private static void AddUserAccountSettings(this IServiceCollection services)
        {
            services.AddSingleton<IUserAccountForm, UserAccountFormExecutor>();
        }

        private static void AddUserConfigSettings(this IServiceCollection services)
        {
            services.AddSingleton<UserConfigDtoAndViewModelMapper>();

            services.AddSingleton<UserConfigFormViewModel>();
            services.AddSingleton<UserConfigFormPresenter>();

            services.AddSingleton<IUserConfigForm, UserConfigFormExecutor>();

            //services.AddSingleton<UserConfigForm>(provider =>
            //{
            //    return ActivatorUtilities.CreateInstance<UserConfigForm>(
            //        provider,
            //        "user_nenga_settings.json");
            //});

        }
    }
}
