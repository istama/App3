using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;

namespace IsTama.NengaBooster.UseCases.Repositories
{
    interface IApplicationConfigRepository
    {
        Task<NengaMenuConfig> GetNengaMenuConfigAsync();
        Task<LoginFormConfig> GetLoginFormConfigAsync();

        Task<NaireApplicationConfig> GetNaireAppilicationConfigAsync();
        Task<HensyuApplicationConfig> GetHensyuAppilicationConfigAsync();
        Task<InformationApplicationConfig> GetInformationAppilicationConfigAsync();
        Task<InformationDetailApplicationConfig> GetInformationDetailAppilicationConfigAsync();
        Task<KouseishiApplicationConfig> GetKouseishiAppilicationConfigAsync();

        Task<DialogConfig> GetNaireDialogConfigAsync();
        Task<DialogConfig> GetHensyuDialogConfigAsync();
        Task<DialogConfig> GetInformationDialogConfigAsync();
        Task<DialogConfig> GetKouseishiDialogConfigAsync();
    }
}
