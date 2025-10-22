using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.NengaBooster.UseCases.SSS;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Gateways
{
    sealed class OtherConfigRepository : IOtherConfigRepository
    {
        private readonly INengaBoosterConfigDTOReader _reader;
        private readonly NengaBoosterConfigDtoMapper _mapper;

        private OtherConfig _configBuffer;
        private DateTime _lastReadTime = default;


        public OtherConfigRepository(INengaBoosterConfigDTOReader reader, NengaBoosterConfigDtoMapper mapper)
        {
            Assert.IsNull(reader, nameof(reader));
            Assert.IsNull(mapper, nameof(mapper));

            _reader = reader;
            _mapper = mapper;
        }

        public async Task<string> GetUserConfigFilepathAsync()
        {
            var buffer = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return buffer.Path_UserSettingsFile;
        }
        public async Task SetUserConfigFilepathAsync(string userConfigFilepath)
        {
            Assert.IsNullOrEmpty(userConfigFilepath, nameof(userConfigFilepath));

            var buffer = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            buffer.Path_UserSettingsFile = userConfigFilepath;
        }

        public async Task<string> GetKeyReplacerExeFilepathAsync()
        {
            var buffer = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return buffer.Path_KeyReplacerExeFile;
        }

        public async Task<ScreenSaverStopperPeriods> GetScreenSaverStopperPeriodsAsync()
        {
            var buffer = await GetOrReadUserConfigAsync().ConfigureAwait(false);
            return new ScreenSaverStopperPeriods(buffer.Texts_SSSExecutionPeriods);
        }

        private async Task<OtherConfig> GetOrReadUserConfigAsync()
        {
            if (_configBuffer != null && !_reader.IsUpdated(_lastReadTime))
                return _configBuffer;

            var dto = await _reader.ReadAsync().ConfigureAwait(false);
            _lastReadTime = DateTime.Now;
            _configBuffer = _mapper.ConvertToOtherConfig(dto);

            return _configBuffer;
        }
    }
}
