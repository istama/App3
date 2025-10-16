using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.Configs
{
    class UserConfigDTOWriter : IUserConfigDTOWriter
    {
        private readonly JsonWriter _writer;


        public UserConfigDTOWriter(JsonWriter writer)
        {
            Assert.IsNull(writer, nameof(writer));

            _writer = writer;
        }


        /// <inheritdoc />
        public Task SaveAsync(UserConfigDTO dto)
        {
            Assert.IsNull(dto, nameof(dto));

            return _writer.WriteAsync(dto);
        }
    }
}
