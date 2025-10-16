using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// NengaBoosterの設定ファイルを読み込むリポジトリ。
    /// </summary>
    interface INengaBoosterConfigDTOReader
    {
        /// <summary>
        /// NengaBoosterの設定を読み込む。
        /// </summary>
        Task<NengaBoosterConfigDTO> ReadAsync();

        //bool IsUpdated();
        bool IsUpdated(DateTime lastReadTime);
    }
}
