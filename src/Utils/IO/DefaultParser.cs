using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// オブジェクトと文字列を相互変換可能にするパーサ。
    /// </summary>
    class DefaultParser
    {
        private readonly Dictionary<Type, Func<Object, String>> _funcs_to_convert_to_text = new Dictionary<Type, Func<Object, String>>();
        private readonly Dictionary<Type, Func<String, Object>> _funcs_to_convert_to_obj  = new Dictionary<Type, Func<String, Object>>();

        public DefaultParser()
        {
            // オブジェクト型と文字列を相互変換する関数を登録する
            Register(ToStringFromString,  ToStringFromString);
            Register(ToStringFromBoolean, ToBooleanFromString);
            Register(ToStringFromInt32,   ToInt32FromString);
            Register(ToStringFromDouble,  ToDoubleFromString);
            Register(ToStringFromPoint,   ToPointFromString);
            Register(ToStringFromSize, ToSizeFromString);
            Register(ToStringFromRectangle, ToRectangleFromString);
        }
        
        public Boolean ContainsType(Type type)
            => _funcs_to_convert_to_obj.ContainsKey(type) && _funcs_to_convert_to_text.ContainsKey(type);

        /// <summary>
        /// オブジェクトと文字列を相互変換する関数を登録する。
        /// オブジェクトの型はジェネリックで指定する。
        /// 既に登録されている型の場合は上書きされる。
        /// </summary>
        public void Register<T>(Func<T, String> func_to_convert_to_text, Func<String, T> func_to_convert_to_obj)
        {
            if (func_to_convert_to_text == null || _funcs_to_convert_to_obj == null)
                throw new ArgumentNullException();

            String ToTextWrapper(Object obj)
                => func_to_convert_to_text((T) obj);

            // すでに変換関数が登録されている型なら上書きする
            if (_funcs_to_convert_to_text.ContainsKey(typeof(T)))
                _funcs_to_convert_to_text.Remove(typeof(T));
            _funcs_to_convert_to_text.Add(typeof(T), ToTextWrapper);

            Object ToObjWrapper(String text)
                => func_to_convert_to_obj(text);

            if (_funcs_to_convert_to_obj.ContainsKey(typeof(T)))
                _funcs_to_convert_to_obj.Remove(typeof(T));
            _funcs_to_convert_to_obj.Add(typeof(T), ToObjWrapper);
        }

        /// <summary>
        /// オブジェクトをパース可能な文字列に変換する。
        /// </summary>
        public String ToStringFrom<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            if (!_funcs_to_convert_to_text.ContainsKey(typeof(T)))
                throw new ArgumentException($"{typeof(T).Name}型を文字列に変換する関数は登録されていません。");

            var converter = _funcs_to_convert_to_text[typeof(T)];
            return converter(obj);
        }

        /// <summary>
        /// オブジェクトをパース可能な文字列に変換する。
        /// </summary>
        public String ToStringFrom(Type type, Object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            if (!_funcs_to_convert_to_text.ContainsKey(type))
                throw new ArgumentException($"{type.Name}型を文字列に変換する関数は登録されていません。");

            var converter = _funcs_to_convert_to_text[type];
            return converter(obj);
        }

        /// <summary>
        /// 文字列からオブジェクトに変換する。
        /// </summary>
        public T ToObjectFrom<T>(String text)
        {
            if (!_funcs_to_convert_to_obj.ContainsKey(typeof(T)))
                throw new ArgumentException($"文字列を{typeof(T).Name}型に変換する関数は登録されていません。");

            var converter = _funcs_to_convert_to_obj[typeof(T)];
            return (T)converter(text);
        }

        /// <summary>
        /// 文字列からオブジェクトに変換する。
        /// </summary>
        public Object ToObjectFrom(Type type, String text)
        {
            if (String.IsNullOrEmpty(text))
                throw new ArgumentNullException();

            if (!_funcs_to_convert_to_obj.ContainsKey(type))
                throw new ArgumentException($"文字列を{type.Name}型に変換する関数は登録されていません。");

            var converter = _funcs_to_convert_to_obj[type];
            return converter(text);
        }


        /** 以下、デフォルトで用意している変換関数 *********************************************************************/

        private static String ToStringFromString(String str)
            => str;

        private static String ToStringFromBoolean(Boolean b)
            => b ? Boolean.TrueString : Boolean.FalseString;
        private static Boolean ToBooleanFromString(String text)
            => Boolean.TryParse(text, out var result) ? result : false;

        private static String ToStringFromInt32(Int32 i)
            => i.ToString();
        private static Int32 ToInt32FromString(String text)
            => Int32.TryParse(text, out var value) ? value : 0;

        private static String ToStringFromDouble(Double d)
            => d.ToString();
        private static Double ToDoubleFromString(String text)
            => Double.TryParse(text, out var value) ? value : 0.0;

        private static String ToStringFromPoint(Point p)
            => $"{p.X.ToString()}, {p.Y.ToString()}";
        private static Point ToPointFromString(String text)
        {
            if (String.IsNullOrEmpty(text))
                return default;

            var e = text.Split(',').Select(t => t.Trim()).ToArray();
            if (e.Length != 2)
                throw new FormatException($"Point型のプロパティの書式が不正です。 {text}");
            if (!Int32.TryParse(e[0], out var x) || !Int32.TryParse(e[1], out var y))
                throw new FormatException($"Point型のプロパティの書式が不正です。 {text}");

            return new Point(x, y);
        }

        private static String ToStringFromSize(Size s)
            => $"{s.Width.ToString()},{s.Height.ToString()}";
        private static Size ToSizeFromString(String text)
        {
            if (String.IsNullOrEmpty(text))
                return default;

            var e = text.Split(',');
            if (e.Length != 2)
                throw new FormatException($"Size型のプロパティの書式が不正です。 {text}");
            if (!Int32.TryParse(e[0], out var w) || !Int32.TryParse(e[1], out var h))
                throw new FormatException($"Size型のプロパティの書式が不正です。 {text}");

            return new Size(w, h);
        }

        private static String ToStringFromRectangle(Rectangle r)
            => $"{r.X.ToString()},{r.Y.ToString()},{r.Width.ToString()},{r.Height.ToString()}";
        private static Rectangle ToRectangleFromString(String text)
        {
            if (String.IsNullOrEmpty(text))
                return default;

            var e = text.Split(',');
            if (e.Length != 4)
                throw new FormatException($"Rectangle型のプロパティの書式が不正です。 {text}");
            if (!Int32.TryParse(e[0], out var x) || !Int32.TryParse(e[1], out var y) || !Int32.TryParse(e[2], out var w) || !Int32.TryParse(e[3], out var h))
                throw new FormatException($"Rectangle型のプロパティの書式が不正です。 {text}");

            return new Rectangle(x, y, w, h);
        }
    }
}
