using IsTama.Utils.DependencyInjectionSimple;
using IsTama.NengaBooster.Core.Di;
using IsTama.NengaBooster.Infrastructures.Di;
using IsTama.NengaBooster.UI.Di;
using IsTama.NengaBooster.UseCases.Di;
using IsTama.NengaBooster.Error;

namespace IsTama.NengaBooster.AppMain
{
    sealed class Di
    {
        public IsTama.NengaBooster.UI.Main.View.MainForm BuildMainForm()
        {
            var provider = GetServiceProvider();
            provider.DebugMode = true;

            var mainform = provider.GetRequiredService<UI.Main.View.MainForm>();
            var warningPresenter = provider.GetRequiredService<UseCases.Presenters.IWarningPresenter>();

            warningPresenter.Owner = mainform;

            return mainform;
        }

        public void TestBuild()
        {
            try
            {
                var provider = GetServiceProvider();
                provider.DebugMode = true;

                _ = provider.GetRequiredService<UI.Gateways.INengaBoosterConfigDTOReader>();
                _ = provider.GetRequiredService<UI.Gateways.INengaBoosterConfigDTOWriter>();

                _ = provider.GetRequiredService<UseCases.Repositories.IApplicationConfigRepository>();
                _ = provider.GetRequiredService<UseCases.Repositories.IBehaviorConfigRepository>();
                _ = provider.GetRequiredService<UseCases.Repositories.IUserConfigRepository>();

                _ = provider.GetRequiredService<UI.Main.Presentations.INengaBoosterConfigForm>();
                _ = provider.GetRequiredService<UI.Main.Presentations.IUserConfigForm>();
                _ = provider.GetRequiredService<Core.NengaApps.IUserAccountForm>();
                _ = provider.GetRequiredService<Core.NengaApps.LoginRPAService>();
                _ = provider.GetRequiredService<Core.NengaApps.NaireRPAService>();
                _ = provider.GetRequiredService<UseCases.Interfaces.IOpenNaireUseCase>();
                _ = provider.GetRequiredService<UI.Main.Presentations.MainFormController>();
                _ = provider.GetRequiredService<UI.Main.View.MainForm>();
            }
            catch (System.Exception ex)
            {
                ErrorMessageBox.Show(ex.ToString());
                throw;
            }
        }

        private IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddCore();
            services.AddUseCases();
            services.AddUI();
            services.AddInfrastructures();

            return services.BuildServiceProvider();
        }
    }
}
