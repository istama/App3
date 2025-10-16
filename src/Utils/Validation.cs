using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// プロパティの値が正しいか判定する拡張メソッド。
    /// </summary>
    static class ValidationExtensions
    {
        /// <summary>
        /// Validation属性を継承した属性が付与されたプロパティの値が正しいかチェックする。
        /// </summary>
        public static Boolean Validation<T>(this T obj, out String error_message)
        {
            error_message = String.Empty;
            foreach (var p in obj.GetType().GetProperties())
            {
                var value = p.GetValue(obj);
                var property_name = p.Name;
                foreach (var attr in p.GetCustomAttributes(true))
                {
                    if (attr is ValidationAttribute at)
                    {
                        var (result, error_msg) = at.Validation(value, property_name);
                        if (!result)
                        {
                            error_message = error_msg;
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Property)]
    abstract class ValidationAttribute : Attribute
    {
        public abstract (Boolean, String) Validation(Object value, String property_name);
    }

    /// <summary>
    /// 値がNullでないか。
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    class NotNullAttribute : ValidationAttribute
    {
        public override (Boolean, String) Validation(Object value, String property_name)
        {
            return value != null
                ? (true, String.Empty)
                : (false, $"{property_name} の値がnullです。");
        }
    }
    /// <summary>
    /// 値がNullもしくは空文字でないか。
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    class NotNullAndEmptyAttribute : NotNullAttribute
    {
        public override  (Boolean, String) Validation(Object value, String property_name)
        {
            if (value is String text)
            {
                return !String.IsNullOrEmpty(text)
                    ? (true, String.Empty)
                    : (false, $"{property_name} の値がnullもしくは空です。");
            }

            return base.Validation(value, property_name);
        }
    }
    /// <summary>
    /// 値が指定した範囲内か。
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    class RangeAttribute : ValidationAttribute
    {
        private readonly Int32 _low_limit;
        private readonly Int32 _high_limit;
        public RangeAttribute(Int32 low_limit, Int32 high_limit)
        {
            _low_limit  = low_limit;
            _high_limit = high_limit;
        }
        public override (Boolean, String) Validation(Object value, String property_name)
        {
            if (value is Byte b)
                return Validation(b, property_name);
            if (value is Int16 w)
                return Validation(w, property_name);
            if (value is Int32 i)
                return Validation(i, property_name);
            if (value is Int64 l)
                return Validation(l, property_name);
            if (value is Single s)
                return Validation(s, property_name);
            if (value is Double d)
                return Validation(d, property_name);
            if (value is String text && Int32.TryParse(text, out var number))
                return Validation(number, property_name);

            return (false, $"{property_name} の値は {_low_limit} ～ {_high_limit} の間で設定してください。");
        }
        public (Boolean, String) Validation(Int32 value, String property_name)
        {
            return value >= _low_limit && value <= _high_limit
                ? (true, String.Empty)
                : (false, $"{property_name} の値は {_low_limit} ～ {_high_limit} の間で設定してください。");
        }
        public (Boolean, String) Validation(Double value, String property_name)
        {
            return value >= _low_limit && value <= _high_limit
                ? (true, String.Empty)
                : (false, $"{property_name} の値は {_low_limit}.0 ～ {_high_limit}.0 の間で設定してください。");
        }
    }
    /// <summary>
    /// 値が指定した値以上か。
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    class LargerThanOrEqualAttribute : ValidationAttribute
    {
        private readonly Double _low_limit;
        public LargerThanOrEqualAttribute(Double low_limit)
        {
            _low_limit = low_limit;
        }
        public override (Boolean, String) Validation(Object value, String property_name)
        {
            if (value is Byte b)
                return Validation(b, property_name);
            if (value is Int16 w)
                return Validation(w, property_name);
            if (value is Int32 i)
                return Validation(i, property_name);
            if (value is Int64 l)
                return Validation(l, property_name);
            if (value is Single s)
                return Validation(s, property_name);
            if (value is Double d)
                return Validation(d, property_name);
            if (value is String text && Int32.TryParse(text, out var number))
                return Validation(number, property_name);

            return (false, $"{property_name} の値は {_low_limit}.0 以上の値を設定してください。");
        }
        public (Boolean, String) Validation(Int32 value, String property_name)
        {
            return value >= _low_limit
                ? (true, String.Empty)
                : (false, $"{property_name} の値は {_low_limit} 以上の値を設定してください。");
        }
        public (Boolean, String) Validation(Double value, String property_name)
        {
            return value >= _low_limit
                ? (true, String.Empty)
                : (false, $"{property_name} の値は {_low_limit}.0 以上の値を設定してください。");
        }
    }
    /// <summary>
    /// 値が指定した値以下か。
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    class SmallerThanOrEqualAttribute : ValidationAttribute
    {
        private Double _high_limit;
        public SmallerThanOrEqualAttribute(Double high_limit)
        {
            _high_limit = high_limit;
        }
        public override (Boolean, String) Validation(Object value, String property_name)
        {
            if (value is Byte b)
                return Validation(b, property_name);
            if (value is Int16 w)
                return Validation(w, property_name);
            if (value is Int32 i)
                return Validation(i, property_name);
            if (value is Int64 l)
                return Validation(l, property_name);
            if (value is Single s)
                return Validation(s, property_name);
            if (value is Double d)
                return Validation(d, property_name);
            if (value is String text && Int32.TryParse(text, out var number))
                return Validation(number, property_name);

            return (false, $"{property_name} の値は {_high_limit} 以下の値を設定してください。");
        }
        public (Boolean, String) Validation(Int32 value, String property_name)
        {
            return value <= _high_limit
                ? (true, String.Empty)
                : (false, $"{property_name} の値は {_high_limit} 以下の値を設定してください。");
        }
        public (Boolean, String) Validation(Double value, String property_name)
        {
            return value >= _high_limit
                ? (true, String.Empty)
                : (false, $"{property_name} の値は {_high_limit}.0 以下の値を設定してください。");
        }
    }
    /// <summary>
    /// 値が正しいパスの書式になっているか。
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    class PathAttribute : ValidationAttribute
    {
        public override (Boolean, String) Validation(Object value, String property_name)
        {
            if (value is String text)
            {
                return text.IsValidAsPath()
                    ? (true, String.Empty)
                    : (false, $"{property_name} の値はパスの書式で設定してください。");
            }

            return (false, $"{property_name} の値はパスの書式で設定してください。");
        }
    }
    /// <summary>
    /// 値が指定した値のいずれかか。
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    class AnyAttribute : ValidationAttribute
    {
        private String[] _values;
        public AnyAttribute(params String[] values)
        {
            _values = values;
        }
        public override (Boolean, String) Validation(Object value, String property_name)
        {
            if (value is String text)
            {
                return _values.Any(v => text.Equals(value))
                    ? (true, String.Empty)
                    : (false, $"{property_name} の値は {String.Join(" ", _values)} のいずれかを設定してください。");
            }

            return (false, $"{property_name} の値は {String.Join(" ", _values)} のいずれかを設定してください。");
        }
    }

    
}
