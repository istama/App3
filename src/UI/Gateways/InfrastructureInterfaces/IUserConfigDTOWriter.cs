using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// ユーザー設定を書き込むリポジトリ。
    /// </summary>
    interface IUserConfigDTOWriter
    {
        Task SaveAsync(UserConfigDTO dto);
    }
}
