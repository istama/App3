using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.UI.Gateways;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.UserConfigSettings.Presentations
{
    /// <summary>
    /// UserConfigDTOとUserConfigViewModelのプロパティの値を相互代入するクラス。
    /// </summary>
    sealed class UserConfigDtoAndViewModelMapper
    {
        private readonly DtoAndViewModelMapper _mapper;


        public UserConfigDtoAndViewModelMapper(DtoAndViewModelMapper mapper)
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
            return GetPropertiesFrom(typeof(UserConfigDTO));
        }

        private PropertyInfo[] GetVmProperties()
        {
            return GetPropertiesFrom(typeof(UserConfigFormViewModel));
        }

        /// <summary>
        /// DTOのプロパティがすべて正しい書式で設定されているならtrueを返す。
        /// </summary>
        public bool AreAllDtoPropertyValuesValidFormat(UserConfigDTO dto, out string errorMessage)
        {
            Assert.IsNull(dto, nameof(dto));

            var dtoProps = GetDtoProperties();
            var vmProps = GetVmProperties();

            return _mapper.AreAllDtoPropertyValuesValidFormat(dto, dtoProps, vmProps, out errorMessage);
        }

        /// <summary>
        /// DTOの値をViewmodelにセットする。
        /// </summary>
        public void SetDtoPropertyValuesToViewModel(UserConfigDTO dto, UserConfigFormViewModel vm)
        {
            Assert.IsNull(dto, nameof(dto));
            Assert.IsNull(vm, nameof(vm));

            var dtoProps = GetDtoProperties();
            var vmProps = GetVmProperties();

            _mapper.SetDtoValueToViewModel(dto, dtoProps, vm, vmProps);
        }


        /// <summary>
        /// VMのプロパティがすべて正しい書式で設定されているならtrueを返す。
        /// </summary>
        public bool AreAllVmPropertyValuesValidFormat(UserConfigFormViewModel vm, out string errorMessage)
        {
            Assert.IsNull(vm, nameof(vm));

            var vmProps = GetVmProperties();
            var dtoProps = GetDtoProperties();

            return _mapper.AreAllVmPropertyValuesValidFormat(vm, vmProps, dtoProps, out errorMessage);
        }

        /// <summary>
        /// ViewModelの値をDTOにセットする
        /// </summary>
        public void SetVmPropertyValuesToDto(UserConfigFormViewModel vm, UserConfigDTO dto)
        {
            Assert.IsNull(vm, nameof(vm));
            Assert.IsNull(dto, nameof(dto));

            var vmProps = GetVmProperties();
            var dtoProps = GetDtoProperties();

            _mapper.SetViewModelValueToDto(vm, vmProps, dto, dtoProps);
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
        public bool AreEquals(UserConfigDTO dto, UserConfigFormViewModel vm)
        {
            Assert.IsNull(dto, nameof(dto));
            Assert.IsNull(vm, nameof(vm));

            var vmProps = GetVmProperties();
            var dtoProps = GetDtoProperties();

            return _mapper.AreVmPropertyValuesContainedInDto(vm, vmProps, dto, dtoProps);
        }
    }
}
