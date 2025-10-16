using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.NengaBoosterConfigSettings.Presentations
{
    sealed class NengaBoosterConfigFormPresenter
    {
        private readonly NengaBoosterConfigFormViewModel _viewmodel;
        private readonly INengaBoosterConfigDTOReader _configReader;
        private readonly INengaBoosterConfigDTOWriter _configWriter;
        private readonly NengaBoosterConfigDtoAndViewModelMapper _mapper;


        public NengaBoosterConfigFormPresenter(
            NengaBoosterConfigFormViewModel viewmodel,
            INengaBoosterConfigDTOReader configReader,
            INengaBoosterConfigDTOWriter configWriter,
            NengaBoosterConfigDtoAndViewModelMapper mapper)
        {
            Assert.IsNull(viewmodel, nameof(viewmodel));
            Assert.IsNull(configReader, nameof(configReader));
            Assert.IsNull(configWriter, nameof(configWriter));
            Assert.IsNull(mapper, nameof(mapper));

            _viewmodel = viewmodel;
            _configReader = configReader;
            _configWriter = configWriter;
            _mapper = mapper;
        }


        /// <summary>
        /// 保存された設定をフォームにロードする。
        /// </summary>
        public async Task<(bool, string)> TryLoadAsync()
        {
            // DTOのすべてのプロパティ名がviewmodelのプロパティに定義されているか
            if (!_mapper.AreAllDtoPropertyNamesDefinedInVm(out var undefinedPropName))
            {
                throw new NengaBoosterException($"{nameof(NengaBoosterConfigDTO)}.{undefinedPropName} が {nameof(NengaBoosterConfigFormViewModel)} に定義されていません。");
            }

            if (!_mapper.AreAllVmPropertyNamesDefinedInDto(out undefinedPropName))
            {
                throw new NengaBoosterException($"{nameof(NengaBoosterConfigFormViewModel)}.{undefinedPropName} が {nameof(NengaBoosterConfigDTO)} に定義されていません。");
            }

            // 設定をロードする
            var dto = await _configReader.ReadAsync().ConfigureAwait(false);
            
            // DTOのすべてのプロパティが正しい書式で設定されているか
            var validFormat = _mapper.AreAllDtoPropertyValuesValidFormat(dto, out var errorMessage);
            
            // 正しい書式で設定されていないプロパティがあってもロードを行う
            _mapper.SetDtoPropertyValuesToViewModel(dto, _viewmodel);
            
            return (validFormat, errorMessage);
        }

        /// <summary>
        /// フォームの設定を保存する。
        /// </summary>
        public async Task<(bool, string)> TrySaveAsync()
        {
            var dto = new NengaBoosterConfigDTO();

            // Viewmodelのすべてのプロパティ名がDTOのプロパティに定義されているか
            if (!_mapper.AreAllVmPropertyNamesDefinedInDto(out var undefinedPropName))
            {
                throw new NengaBoosterException($"{nameof(NengaBoosterConfigFormViewModel)}.{undefinedPropName} が {nameof(NengaBoosterConfigDTO)} に定義されていません。");
            }

            // Viewmodelのすべてのプロパティが正しい書式で設定されているか
            if (!_mapper.AreAllVmPropertyValuesValidFormat(_viewmodel, out var errorMessage))
            {
                return (false, errorMessage);
            }

            // ViewModelの値をDTOにセットする
            _mapper.SetVmPropertyValuesToDto(_viewmodel, dto);
            // 設定を保存する
            await _configWriter.SaveAsync(dto).ConfigureAwait(false);

            return (true, null);
        }

        /// <summary>
        /// ViewModelの値が更新されているならtrueを返す。
        /// </summary>
        public async Task<bool> AreViewModelValuesUpdatedAsync()
        {
            var dto = await _configReader.ReadAsync().ConfigureAwait(false);
            return !_mapper.AreEquals(dto, _viewmodel);
        }
    }
}
