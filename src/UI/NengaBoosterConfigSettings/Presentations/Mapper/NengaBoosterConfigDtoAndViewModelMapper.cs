using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.NengaBoosterConfigSettings.Presentations
{
    /// <summary>
    /// NengaBoosterConfigDTOとNengaBoosterConfigViewModelのプロパティの値を相互代入するクラス。
    /// </summary>
    sealed class NengaBoosterConfigDtoAndViewModelMapper
    {
        private readonly DtoAndViewModelMapper _mapper;


        public NengaBoosterConfigDtoAndViewModelMapper(DtoAndViewModelMapper mapper)
        {
            Assert.IsNull(mapper, nameof(mapper));

            _mapper = mapper;
        }


        /// <summary>
        /// 指定した型のgetterを取得する。
        /// </summary>
        private PropertyInfo[] GetPropertiesFrom(Type type)
        {
            return type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
        }

        private PropertyInfo[] GetDtoProperties()
        {
            return GetPropertiesFrom(typeof(NengaBoosterConfigDTO));
        }

        private PropertyInfo[] GetVmProperties()
        {
            var vmaProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForApplicationSettings));
            var vmbProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForBehaviorSettings));
            return vmaProps.Concat(vmbProps).ToArray();
        }

        /// <summary>
        /// DTOのプロパティがすべて正しい書式で設定されているならtrueを返す。
        /// </summary>
        public bool AreAllDtoPropertyValuesValidFormat(NengaBoosterConfigDTO dto, out string errorMessage)
        {
            Assert.IsNull(dto, nameof(dto));

            var dtoProps = GetDtoProperties();
            var vmProps = GetVmProperties();

            return _mapper.AreAllDtoPropertyValuesValidFormat(dto, dtoProps, vmProps, out errorMessage);
        }

        /// <summary>
        /// DTOの値をViewmodelにセットする。
        /// </summary>
        public void SetDtoPropertyValuesToViewModel(NengaBoosterConfigDTO dto, NengaBoosterConfigFormViewModel vm)
        {
            Assert.IsNull(dto, nameof(dto));
            Assert.IsNull(vm, nameof(vm));

            var dtoProps = GetDtoProperties();
            var vmaProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForApplicationSettings));
            var vmbProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForBehaviorSettings));

            _mapper.SetDtoValueToViewModel(dto, dtoProps, vm.ApplicationSettings, vmaProps);
            _mapper.SetDtoValueToViewModel(dto, dtoProps, vm.BehaviorSettings, vmbProps);
        }


        /// <summary>
        /// VMのプロパティがすべて正しい書式で設定されているならtrueを返す。
        /// </summary>
        public bool AreAllVmPropertyValuesValidFormat(NengaBoosterConfigFormViewModel vm, out string errorMessage)
        {
            Assert.IsNull(vm, nameof(vm));

            var vmaProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForApplicationSettings));
            var vmbProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForBehaviorSettings));
            var dtoProps = GetDtoProperties();

            return
                _mapper.AreAllVmPropertyValuesValidFormat(vm.ApplicationSettings, vmaProps, dtoProps, out errorMessage) &&
                _mapper.AreAllVmPropertyValuesValidFormat(vm.BehaviorSettings, vmbProps, dtoProps, out errorMessage);
        }

        /// <summary>
        /// ViewModelの値をDTOにセットする
        /// </summary>
        public void SetVmPropertyValuesToDto(NengaBoosterConfigFormViewModel vm, NengaBoosterConfigDTO dto)
        {
            Assert.IsNull(vm, nameof(vm));
            Assert.IsNull(dto, nameof(dto));

            var vmaProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForApplicationSettings));
            var vmbProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForBehaviorSettings));
            var dtoProps = GetDtoProperties();

            _mapper.SetViewModelValueToDto(vm.ApplicationSettings, vmaProps, dto, dtoProps);
            _mapper.SetViewModelValueToDto(vm.BehaviorSettings, vmbProps, dto, dtoProps);
        }

        /// <summary>
        /// DTOのプロパティの名前がVMのプロパティにすべて定義されているならtrueを返す。
        /// </summary>
        public bool AreAllDtoPropertyNamesDefinedInVm(out string undefinedPropName)
        {
            var dtoProps = GetVmProperties();
            var vmProps = GetVmProperties();

            return _mapper.AreAllArg1PropertyNamesDefinedInArg2(dtoProps, vmProps, out undefinedPropName);
        }

        /// <summary>
        /// VMのプロパティの名前がDTOのプロパティにすべて定義されているならtrueを返す。
        /// </summary>
        public bool AreAllVmPropertyNamesDefinedInDto(out string undefinedPropName)
        {
            var vmProps = GetVmProperties();
            var dtoProps = GetDtoProperties();

            return _mapper.AreAllArg1PropertyNamesDefinedInArg2(vmProps, dtoProps, out undefinedPropName);
        }

        /// <summary>
        /// DTOとViewModelの値が同じならtrueを返す。
        /// </summary>
        public bool AreEquals(NengaBoosterConfigDTO dto, NengaBoosterConfigFormViewModel vm)
        {
            Assert.IsNull(dto, nameof(dto));
            Assert.IsNull(vm, nameof(vm));

            var vmaProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForApplicationSettings));
            var vmbProps = GetPropertiesFrom(typeof(NengaBoosterConfigFormViewModelForBehaviorSettings));
            var dtoProps = GetDtoProperties();

            return
                _mapper.AreVmPropertyValuesContainedInDto(vm.ApplicationSettings, vmaProps, dto, dtoProps) &&
                _mapper.AreVmPropertyValuesContainedInDto(vm.BehaviorSettings, vmbProps, dto, dtoProps);
        }
    }
}
