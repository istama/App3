using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// ユーザー設定を読み込むリポジトリ。
    /// </summary>
    interface IUserConfigDTOReader
    {
        Task<UserConfigDTO> ReadAsync();

        //bool IsUpdated();
        bool IsUpdated(DateTime lastReadTime);
    }
}
