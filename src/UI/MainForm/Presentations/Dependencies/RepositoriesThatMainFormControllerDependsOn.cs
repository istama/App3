using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Main.Presentations
{
    /// <summary>
    /// MainFormControllerが依存するリポジトリ群。
    /// </summary>
    sealed class RepositoriesThatMainFormControllerDependsOn
    {
        public IUserConfigRepositoryExtended UserConfigRepository { get; set; }
        public IOtherConfigRepository OtherConfigRepository { get; set; }


        public RepositoriesThatMainFormControllerDependsOn(IUserConfigRepositoryExtended userConfigRepository, IOtherConfigRepository otherConfigRepository)
        {
            Assert.IsNull(userConfigRepository, nameof(userConfigRepository));
            Assert.IsNull(otherConfigRepository, nameof(otherConfigRepository));

            UserConfigRepository = userConfigRepository;
            OtherConfigRepository = otherConfigRepository;
        }
    }
}
