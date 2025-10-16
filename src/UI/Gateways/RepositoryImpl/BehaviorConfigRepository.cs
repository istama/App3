using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.NengaBooster.UseCases.Repositories;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// 年賀アプリとの動作設定のリポジトリ。
    /// </summary>
    sealed class BehaviorConfigRepositiry : IBehaviorConfigRepository
    {
        private readonly INengaBoosterConfigDTOReader _dtoReader;
        private readonly NengaBoosterConfigDtoMapper _dtoAndConfigMapper;

        private NaireBehaviorConfig _naireBehaviorConfigBuffer;
        private HensyuBehaviorConfig _hensyuBehaviorConfigBuffer;
        private KouseishiBehaviorConfig _kouseishiBehaviorConfigBuffer;

        private DateTime _lastReadTime = default;


        public BehaviorConfigRepositiry(INengaBoosterConfigDTOReader dtoReader, NengaBoosterConfigDtoMapper dtoAndConfigMapper)
        {
            Assert.IsNull(dtoReader, nameof(dtoReader));
            Assert.IsNull(dtoAndConfigMapper, nameof(dtoAndConfigMapper));

            _dtoReader = dtoReader;
            _dtoAndConfigMapper = dtoAndConfigMapper;
        }


        public async Task<NaireBehaviorConfig> GetNaireBehaviorConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _naireBehaviorConfigBuffer != null)
                return _naireBehaviorConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _naireBehaviorConfigBuffer = _dtoAndConfigMapper.ConvertToNaireBehaviorConfig(dto);

            return _naireBehaviorConfigBuffer;
        }

        public async Task<HensyuBehaviorConfig> GetHensyuBehaviorConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _hensyuBehaviorConfigBuffer != null)
                return _hensyuBehaviorConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _hensyuBehaviorConfigBuffer = _dtoAndConfigMapper.ConvertToHensyuBehaviorConfig(dto);

            return _hensyuBehaviorConfigBuffer;
        }

        public async Task<KouseishiBehaviorConfig> GetKouseishiBehaviorConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _kouseishiBehaviorConfigBuffer != null)
                return _kouseishiBehaviorConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _kouseishiBehaviorConfigBuffer = _dtoAndConfigMapper.ConvertToKouseishiBehaviorConfig(dto);

            return _kouseishiBehaviorConfigBuffer;
        }

        private bool IsNengaBoosterDtoUpdated()
        {
            return _dtoReader.IsUpdated(_lastReadTime);
        }

        private async Task<NengaBoosterConfigDTO> ReadNengaBoosterDtoAsync()
        {
            var dto = await _dtoReader.ReadAsync().ConfigureAwait(false);
            _lastReadTime = DateTime.Now;

            return dto;
        }
    }
}
