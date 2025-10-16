using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.Utils;

namespace IsTama.NengaBooster.UseCases.NengaApps
{
    sealed class LoginService
    {
        private readonly LoginRPAService _loginRPAService;
        private readonly NengaAppWindowFactory _nengaAppWindowFactory;

        private readonly IApplicationConfigRepository _applicationConfigRepository;
        private readonly IBehaviorConfigRepository _behaviorConfigRepository;
        private readonly IUserConfigRepository _userConfigRepository;

        public LoginService(
            LoginRPAService loginRPAService,
            NengaAppWindowFactory nengaAppWindowFactory,
            IApplicationConfigRepository applicationConfigRepository,
            IBehaviorConfigRepository behaviorConfigRepository,
            IUserConfigRepository userConfigRepository)
        {
            Assert.IsNull(loginRPAService, nameof(loginRPAService));
            Assert.IsNull(nengaAppWindowFactory, nameof(nengaAppWindowFactory));
            Assert.IsNull(applicationConfigRepository, nameof(applicationConfigRepository));
            Assert.IsNull(behaviorConfigRepository, nameof(behaviorConfigRepository));
            Assert.IsNull(userConfigRepository, nameof(userConfigRepository));

            _loginRPAService = loginRPAService;
            _nengaAppWindowFactory = nengaAppWindowFactory;
            _applicationConfigRepository = applicationConfigRepository;
            _behaviorConfigRepository = behaviorConfigRepository;
            _userConfigRepository = userConfigRepository;
        }

        public async Task<bool> ExecuteAsync(NengaAppWindowBasic nengaAppWindow)
        {
            var nengaMenuConfig = await _applicationConfigRepository.GetNengaMenuConfigAsync().ConfigureAwait(false);
            var nengaMenuWindow = _nengaAppWindowFactory.GetOrCreateNengaMenuWindow(nengaMenuConfig);

            var loginConfig = await _applicationConfigRepository.GetLoginFormConfigAsync().ConfigureAwait(false);
            var loginWindow = _nengaAppWindowFactory.GetOrCreateLoginFormWindow(loginConfig);

            var userAccount = await _userConfigRepository.GetUserAccountAsync().ConfigureAwait(false);

            return await _loginRPAService.LoginAsync(nengaMenuWindow, loginWindow, nengaAppWindow, userAccount).ConfigureAwait(false);
        }
    }
}
