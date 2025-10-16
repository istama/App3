using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.Configs
{
    /// <summary>
    /// NengaBoosterの設定ファイルに書き込むクラス。
    /// </summary>
    sealed class NengaBoosterConfigDTOWriter : INengaBoosterConfigDTOWriter
    {
        private readonly JsonWriter _writer;


        public NengaBoosterConfigDTOWriter(JsonWriter writer)
        {
            Assert.IsNull(writer, nameof(writer));

            _writer = writer;
        }


        /// <inheritdoc />
        public Task SaveAsync(NengaBoosterConfigDTO dto)
        {
            Assert.IsNull(dto, nameof(dto));

            return _writer.WriteAsync(dto);
        }
    }
}
