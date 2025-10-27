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
    /// 年賀アプリとの連携設定のリポジトリ。
    /// </summary>
    class ApplicationConfigRepository : IApplicationConfigRepository
    {
        private readonly INengaBoosterConfigDTOReader _dtoReader;
        private readonly NengaBoosterConfigDtoMapper _dtoAndConfigMapper;

        private NengaMenuConfig _nengaMenuConfigBuffer;
        private LoginFormConfig _loginFormConfigBuffer;

        private NaireApplicationConfig _naireApplicationConfigBuffer;
        private HensyuApplicationConfig _hensyuApplicationConfigBuffer;
        private InformationApplicationConfig _infoApplicationConfigBuffer;
        private InformationDetailApplicationConfig _infoDetailApplicationConfigBuffer;
        private KouseishiApplicationConfig _kouseishiApplicationConfigBuffer;

        private DialogConfig _naireDialogConfigBuffer;
        private DialogConfig _hensyuDialogConfigBuffer;
        private DialogConfig _infoDialogConfigBuffer;
        private DialogConfig _kouseishiDialogConfigBuffer;

        private DateTime _lastReadTime = default;

        public ApplicationConfigRepository(INengaBoosterConfigDTOReader dtoReader, NengaBoosterConfigDtoMapper dtoAndConfigMapper)
        {
            Assert.IsNull(dtoReader, nameof(dtoReader));
            Assert.IsNull(dtoAndConfigMapper, nameof(dtoAndConfigMapper));

            _dtoReader = dtoReader;
            _dtoAndConfigMapper = dtoAndConfigMapper;
        }


        public async Task<NengaMenuConfig> GetNengaMenuConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _nengaMenuConfigBuffer != null)
                return _nengaMenuConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _nengaMenuConfigBuffer = _dtoAndConfigMapper.ConvertToNengaMenuConfigFrom(dto);

            return _nengaMenuConfigBuffer;
        }

        public async Task<LoginFormConfig> GetLoginFormConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _loginFormConfigBuffer != null)
                return _loginFormConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _loginFormConfigBuffer = _dtoAndConfigMapper.ConvertToLoginFormConfigFrom(dto);

            return _loginFormConfigBuffer;
        }

        public async Task<NaireApplicationConfig> GetNaireAppilicationConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _naireApplicationConfigBuffer != null)
                return _naireApplicationConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _naireApplicationConfigBuffer = _dtoAndConfigMapper.ConvertToNaireApplicationConfigFrom(dto);

            return _naireApplicationConfigBuffer;
        }

        public async Task<HensyuApplicationConfig> GetHensyuAppilicationConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _hensyuApplicationConfigBuffer != null)
                return _hensyuApplicationConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _hensyuApplicationConfigBuffer = _dtoAndConfigMapper.ConvertToHensyuApplicationConfigFrom(dto);

            return _hensyuApplicationConfigBuffer;
        }

        public async Task<InformationApplicationConfig> GetInformationAppilicationConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _infoApplicationConfigBuffer != null)
                return _infoApplicationConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _infoApplicationConfigBuffer = _dtoAndConfigMapper.ConvertToInformationApplicationConfigFrom(dto);

            return _infoApplicationConfigBuffer;
        }

        public async Task<InformationDetailApplicationConfig> GetInformationDetailAppilicationConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _infoDetailApplicationConfigBuffer != null)
                return _infoDetailApplicationConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _infoDetailApplicationConfigBuffer = _dtoAndConfigMapper.ConvertToInformationDetailApplicationConfigFrom(dto);

            // インフォメーションのプロセス名とアプリケーション名をインフォメーション詳細の設定に使用する
            var infoConfig = await GetInformationAppilicationConfigAsync().ConfigureAwait(false);
            _infoDetailApplicationConfigBuffer.Basic.ProcessName = infoConfig.Basic.ProcessName;
            _infoDetailApplicationConfigBuffer.Basic.ApplicationName_OnNengaMenu = infoConfig.Basic.ApplicationName_OnNengaMenu;

            return _infoDetailApplicationConfigBuffer;
        }

        public async Task<KouseishiApplicationConfig> GetKouseishiAppilicationConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _kouseishiApplicationConfigBuffer != null)
                return _kouseishiApplicationConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _kouseishiApplicationConfigBuffer = _dtoAndConfigMapper.ConvertToKouseishiApplicationConfigFrom(dto);

            return _kouseishiApplicationConfigBuffer;
        }

        public async Task<DialogConfig> GetNaireDialogConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _naireDialogConfigBuffer != null)
                return _naireDialogConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _naireDialogConfigBuffer = _dtoAndConfigMapper.ConvertToNaireDialogConfigFrom(dto);

            return _naireDialogConfigBuffer;
        }

        public async Task<DialogConfig> GetHensyuDialogConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _hensyuDialogConfigBuffer != null)
                return _hensyuDialogConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _hensyuDialogConfigBuffer = _dtoAndConfigMapper.ConvertToHensyuDialogConfigFrom(dto);

            return _hensyuDialogConfigBuffer;
        }

        public async Task<DialogConfig> GetInformationDialogConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _infoDialogConfigBuffer != null)
                return _infoDialogConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _infoDialogConfigBuffer = _dtoAndConfigMapper.ConvertToInformationDialogConfigFrom(dto);

            return _infoDialogConfigBuffer;
        }

        public async Task<DialogConfig> GetKouseishiDialogConfigAsync()
        {
            if (!IsNengaBoosterDtoUpdated() && _kouseishiDialogConfigBuffer != null)
                return _kouseishiDialogConfigBuffer;

            var dto = await ReadNengaBoosterDtoAsync().ConfigureAwait(false);
            _kouseishiDialogConfigBuffer = _dtoAndConfigMapper.ConvertToKouseishiDialogConfigFrom(dto);

            return _kouseishiDialogConfigBuffer;
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
