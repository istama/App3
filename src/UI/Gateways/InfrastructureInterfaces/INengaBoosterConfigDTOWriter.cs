using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// NengaBoosterの設定ファイルを書き込むリポジトリ。
    /// </summary>
    interface INengaBoosterConfigDTOWriter
    {
        Task SaveAsync(NengaBoosterConfigDTO dto);
    }
}
