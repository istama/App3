using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.UI.Gateways
{
    /// <summary>
    /// DTOオブジェクトのプロパティとViewmodelオブジェクトのプロパティを相互代入するクラス。
    /// </summary>
    class DtoAndViewModelMapper
    {
        private readonly ConfigValueParser _configValueParser;


        public DtoAndViewModelMapper(ConfigValueParser configValueParser)
        {
            Assert.IsNull(configValueParser, nameof(configValueParser));

            _configValueParser = configValueParser;
        }


        /// <summary>
        /// DTOのプロパティがすべて正しい書式で設定されているならtrueを返す。
        /// </summary>
        public bool AreAllDtoPropertyValuesValidFormat<TDto>(TDto dto, PropertyInfo[] dtoProps, PropertyInfo[] vmProps, out string errorMessage)
        {
            Assert.IsNull(dto, nameof(dto));
            Assert.IsNull(dtoProps, nameof(dtoProps));
            Assert.IsNull(vmProps, nameof(vmProps));

            errorMessage = null;

            foreach (var dtoProp in dtoProps)
            {
                var vmProp = vmProps.FirstOrDefault(vp => dtoProp.Name == vp.Name);
                if (vmProp != default)
                {
                    // DTOのプロパティが正しい書式でセットされているか
                    if (!TryConvertToViewModelValueFrom(dto, dtoProp, vmProp.PropertyType, out _, out errorMessage))
                    {
                        ErrorMessageBox.Show($"設定ファイルの {vmProp.Name} プロパティが正しい書式で設定されていません。");
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// DTOの値をViewmodelにセットする。
        /// </summary>
        public void SetDtoValueToViewModel<TDto, TVm>(TDto dto, PropertyInfo[] dtoProps, TVm vm, PropertyInfo[] vmProps)
        {
            Assert.IsNull(dto, nameof(dto));
            Assert.IsNull(dtoProps, nameof(dtoProps));
            Assert.IsNull(vm, nameof(vm));
            Assert.IsNull(vmProps, nameof(vmProps));

            // DTOのプロパティの値をViewModelにセットする
            foreach (var dtoProp in dtoProps)
            {
                var vmProp = vmProps.FirstOrDefault(p => p.Name == dtoProp.Name);
                if (vmProp != default)
                {
                    // 正しい書式の値のみviewmodelにセットする
                    if (TryConvertToViewModelValueFrom(dto, dtoProp, vmProp.PropertyType, out var value, out _))
                        vmProp.SetValue(vm, value);

                    continue;
                }
            }
        }

        /// <summary>
        /// DTOのプロパティの値をViewModelのプロパティに変換する。変換に成功したらtrueを返す。
        /// </summary>
        private bool TryConvertToViewModelValueFrom<TDto>(TDto dto, PropertyInfo dtoProp, Type vmPropType, out object value, out string errorMessage)
        {
            value = null;
            errorMessage = null;

            try
            {
                var obj = dtoProp.GetValue(dto);
                if (dtoProp.PropertyType == vmPropType)
                {
                    value = obj;
                }
                else
                {
                    value = _configValueParser.ToObjectFrom(vmPropType, obj.ToString());
                }

                return true;
            }
            catch (ArgumentException ex)
            {
                errorMessage = ex.Message;
            }

            return false;
        }

        /// <summary>
        /// VMのプロパティがすべて正しい書式で設定されているならtrueを返す。
        /// </summary>
        public bool AreAllVmPropertyValuesValidFormat<TVm>(TVm vm, PropertyInfo[] vmProps, PropertyInfo[] dtoProps, out string errorMessage)
        {
            Assert.IsNull(vm, nameof(vm));
            Assert.IsNull(vmProps, nameof(vmProps));
            Assert.IsNull(dtoProps, nameof(dtoProps));

            errorMessage = null;

            foreach (var vmProp in vmProps)
            {
                var dtoProp = dtoProps.FirstOrDefault(p => vmProp.Name == p.Name);
                if (dtoProp != default)
                {
                    // ViewModelのプロパティが正しい書式でセットされているか
                    if (!TryConvertToDtoValueFrom(vm, vmProp, dtoProp.PropertyType, out _, out errorMessage))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ViewModelの値をDTOにセットする
        /// </summary>
        public void SetViewModelValueToDto<TVm, TDto>(TVm vm, PropertyInfo[] vmProps, TDto dto, PropertyInfo[] dtoProps)
        {
            Assert.IsNull(vm, nameof(vm));
            Assert.IsNull(vmProps, nameof(vmProps));
            Assert.IsNull(dto, nameof(dto));
            Assert.IsNull(dtoProps, nameof(dtoProps));

            // ViewModelのプロパティの値をDTOにセットする
            foreach (var vmProp in vmProps)
            {
                var dtoProp = dtoProps.FirstOrDefault(p => p.Name == vmProp.Name);
                if (dtoProp != default)
                {
                    // 正しい書式の値のみDTOにセットする
                    if (TryConvertToDtoValueFrom(vm, vmProp, dtoProp.PropertyType, out var value, out _))
                        dtoProp.SetValue(dto, value);
                }
            }
        }

        /// <summary>
        /// ViewModelのプロパティの値をDTOのプロパティに変換する。変換に成功したらtrueを返す。
        /// </summary>
        private bool TryConvertToDtoValueFrom<TVm>(TVm viewmodel, PropertyInfo vmProp, Type dtoPropType, out object value, out string errorMessage)
        {
            value = null;
            errorMessage = null;

            try
            {
                var obj = vmProp.GetValue(viewmodel);
                if (vmProp.PropertyType == dtoPropType)
                {
                    value = obj;
                }
                else
                {
                    value = _configValueParser.ToStringFrom(vmProp.PropertyType, obj);
                }

                return true;
            }
            catch (ArgumentException ex)
            {
                errorMessage = ex.Message;
            }

            return false;
        }

        /// <summary>
        /// DTOのプロパティの名前がVMのプロパティにすべて定義されているならtrueを返す。
        /// </summary>
        public bool AreAllArg1PropertyNamesDefinedInArg2(PropertyInfo[] arg1Props, PropertyInfo[] arg2Props, out string undefinedPropName)
        {
            Assert.IsNull(arg1Props, nameof(arg1Props));
            Assert.IsNull(arg2Props, nameof(arg2Props));

            undefinedPropName = null;

            foreach (var arg1Prop in arg1Props)
            {
                if (arg2Props.Any(p => arg1Prop.Name == p.Name))
                    continue;

                undefinedPropName = arg1Prop.Name;
                return false;
            }

            return true;
        }

        /// <summary>
        /// DTOとViewModelの値が同じならtrueを返す。
        /// </summary>
        public bool AreVmPropertyValuesContainedInDto<TVm, TDto>(TVm vm, PropertyInfo[] vmProps, TDto dto, PropertyInfo[] dtoProps)
        {
            Assert.IsNull(vm, nameof(vm));
            Assert.IsNull(vmProps, nameof(vmProps));
            Assert.IsNull(dto, nameof(dto));
            Assert.IsNull(dtoProps, nameof(dtoProps));

            foreach (var vmProp in vmProps)
            {
                var dtoProp = dtoProps.FirstOrDefault(p => vmProp.Name == p.Name);
                if (dtoProp == default)
                    continue;

                if (!TryConvertToDtoValueFrom(vm, vmProp, dtoProp.PropertyType, out var value, out _))
                    continue;

                if (!value.Equals(dtoProp.GetValue(dto)))
                    return false;
            }

            return true;
        }
    }
}
